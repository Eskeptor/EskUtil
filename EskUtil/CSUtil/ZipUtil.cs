// ======================================================================================================
// File Name        : ZipUtil.cs
// Project          : CSUtil
// Last Update      : 2025.05.19 - yc.jeon
// ======================================================================================================

using System.IO;

namespace CSUtil
{
    /// <summary>
    /// 압축 관련 유틸리티 (기본 zip)
    /// </summary>
    public static class ZipUtil
    {
        public enum ZipResult
        {
            /// <summary>
            /// 성공
            /// </summary>
            Success = 0,
            /// <summary>
            /// Source Path가 실제로 존재하지 않아 비정상 종료
            /// </summary>
            FailZipDirNotExist = -1,
            /// <summary>
            /// Source Path 문자열이 비어있어 비정상 종료
            /// </summary>
            FailEmptySourcePath = -2,
            /// <summary>
            /// 이미 존재하는 폴더 또는 파일 삭제에 실패하여 비정상 종료 <br/>
            /// (삭제를 해야 압축 또는 압축해제를 할 수 있음)
            /// </summary>
            FailExistFileDeleteFail = -3,
            /// <summary>
            /// 압축 실패로 인한 비정상 종료
            /// </summary>
            FailZip = -4,
            /// <summary>
            /// 압축 해제 실패로 인한 비정상 종료
            /// </summary>
            FailUnzip = -5,
            /// <summary>
            /// 압축에 다루려는 Source가 Zip파일이 아니어서 비정상 종료
            /// </summary>
            FailNotZipFile = -6,
        }

        private const string Extension = "zip";

        /// <summary>
        /// 압축하는 함수
        /// </summary>
        /// <param name="targetDir">압축하려는 폴더의 경로</param>
        /// <param name="savePath">압축한 파일을 저장할 경로</param>
        /// <param name="isBackup">압축한 후에 압축전 파일을 백업할 지 유무 (기본값: false)</param>
        /// <returns>
        /// ZipResult.FailEmptySourcePath: <paramref name="targetDir"/>가 비어있을 때 <br/>
        /// ZipResult.FailZipDirNotExist: <paramref name="targetDir"/>가 존재하지 않을 때 <br/>
        /// ZipResult.FailExistFileDeleteFail: 압축한 후에 이전 파일을 지우지 못했을 때 <br/>
        /// ZipResult.FailZip: 압축 도중 예외가 발생하여 압축에 실패한 경우 <br/>
        /// ZipResult.Success: 성공
        /// </returns>
        public static ZipResult Zip(string targetDir, string savePath, bool isBackup = false)
        {
            if (string.IsNullOrEmpty(targetDir))
            {
                return ZipResult.FailEmptySourcePath;
            }

            if (!Directory.Exists(targetDir))
            {
                return ZipResult.FailZipDirNotExist;
            }

            if (!File.Exists(savePath))
            {
                savePath = $"{targetDir}.{Extension}";
            }

            if (File.Exists(savePath))
            {
                try
                {
                    File.Delete(savePath);
                }
                catch
                {
                    return ZipResult.FailExistFileDeleteFail;
                }
            }

            try
            {
                System.IO.Compression.ZipFile.CreateFromDirectory(targetDir, savePath);

                if (!isBackup)
                {
                    try
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(targetDir);
                        directoryInfo.Delete(true);
                    }
                    catch
                    {
                        return ZipResult.FailExistFileDeleteFail;
                    }
                }
            }
            catch
            {
                return ZipResult.FailZip;
            }

            return ZipResult.Success;
        }

        /// <summary>
        /// 압축을 해제하는 함수
        /// </summary>
        /// <param name="targetFile">압축 파일의 경로</param>
        /// <param name="targetDir">압축 해제할 경로</param>
        /// <returns>
        /// ZipResult.FailEmptySourcePath: <paramref name="targetFile"/> 경로가 비어있는 경우 <br/>
        /// ZipResult.FailZipDirNotExist: <paramref name="targetFile"/> 파일이 존재하지 않은 경우 <br/>
        /// ZipResult.FailNotZipFile: <paramref name="targetFile"/> 파일이 압축파일이 아닌 경우 <br/>
        /// ZipResult.FailUnzip: 압축 해제 도중에 예외가 발생한 경우 <br/>
        /// ZipResult.Success: 성공
        /// </returns>
        public static ZipResult Unzip(string targetFile, string targetDir = "")
        {
            if (string.IsNullOrEmpty(targetFile))
            {
                return ZipResult.FailEmptySourcePath;
            }

            if (!File.Exists(targetFile))
            {
                return ZipResult.FailZipDirNotExist;
            }

            string ext = targetFile.Substring(targetFile.LastIndexOfOrdinal(".") + 1);
            if (!ext.EqualsIgnoreCase(Extension))
            {
                return ZipResult.FailNotZipFile;
            }

            if (string.IsNullOrEmpty(targetDir))
            {
                targetDir = targetFile.Substring(0, targetFile.LastIndexOfOrdinal("."));
            }

            try
            {
                System.IO.Compression.ZipFile.ExtractToDirectory(targetFile, targetDir);
            }
            catch
            {
                return ZipResult.FailUnzip;
            }

            return ZipResult.Success;
        }
    }
}
