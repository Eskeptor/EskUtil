// ======================================================================================================
// File Name        : FtpClient.cs
// Project          : CSUtil
// Last Update      : 2026.07.27 - yc.jeon
// ======================================================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

// .NET 9 (SYSLIB0014) 에서는 권장하지 않는 방식
// (추후에 FluentFTP로 변경 필요)
#pragma warning disable SYSLIB0014

namespace Esk.GearForge.CSUtil
{
    /// <summary>
    /// FTP Client 클래스 <br/>
    /// 
    /// 사용법 (Instant) <br/>
    ///   Server 존재 여부 확인법 <br/>
    ///   FtpClient.FtpResult result = FtpClient.InstantCheckServer(new FtpClient.Settings("ftp://example.com", "user", "password")); <br/>
    ///   
    ///   File Upload (FTP 폴더 경로가 없을 시 폴더 생성도 동시에 수행) <br/>
    ///   FtpClient.FtpResult result = FtpClient.InstantUploadFile(SettingData, @"C:\local\file.txt", "remote/file.txt"); <br/>
    ///   FtpClient.FtpResult[] results = FtpClient.InstantUploadFiles(SettingData,
    ///     new string[] { @"C:\local\file1.txt", @"C:\local\file2.txt" },
    ///     new string[] { "remote/file1.txt", "remote/file2.txt" }); <br/>
    ///   
    ///   File Download <br/>
    ///   FtpClient.FtpResult result = FtpClient.InstantDownloadFile(SettingData, "remote/file.txt", @"C:\local\file.txt"); <br/>
    ///   FtpClient.FtpResult[] results = FtpClient.InstantDownloadFiles(SettingData,
    ///     new string[] { "remote/file1.txt", "remote/file2.txt" },
    ///     new string[] { @"C:\local\file1.txt", @"C:\local\file2.txt" }); <br/>
    ///   
    ///   Folder Create (하위 폴더도 생성) <br/>
    ///   FtpClient.FtpResult result = FtpClient.InstantCreateDirectory(SettingData, "remote/folder"); <br/>
    ///   
    /// 사용법 (Instance) <br/>
    ///   FtpClient ftpClient = new FtpClient(new FtpClient.Settings("ftp://example.com", "user", "password")); <br/>
    ///   
    ///   Server 존재 여부 확인법 <br/>
    ///   FtpClient.FtpResult result = ftpClient.CheckServer(); <br/>
    ///   
    ///   File Upload (FTP 폴더 경로가 없을 시 폴더 생성도 동시에 수행) <br/>
    ///   FtpClient.FtpResult result = ftpClient.UploadFile(@"C:\local\file.txt", "remote/file.txt"); <br/>
    ///   FtpClient.FtpResult[] results = ftpClient.UploadFiles(new string[] { @"C:\local\file1.txt", @"C:\local\file2.txt" },
    ///     new string[] { "remote/file1.txt", "remote/file2.txt" }); <br/>
    ///   
    ///   File Download <br/>
    ///   FtpClient.FtpResult result = ftpClient.DownloadFile("remote/file.txt", @"C:\local\file.txt"); <br/>
    ///   FtpClient.FtpResult[] results = ftpClient.DownloadFiles(new string[] { "remote/file1.txt", "remote/file2.txt" },
    ///     new string[] { @"C:\local\file1.txt", @"C:\local\file2.txt" }); <br/>
    ///   
    ///   Folder Create (하위 폴더도 생성) <br/>
    ///   FtpClient.FtpResult result = ftpClient.CreateDirectory("remote/folder"); <br/>
    /// </summary>
    public class FtpClient
    {
        public enum FtpResult
        {
            /// <summary>
            /// 성공
            /// </summary>
            Success = 0,
            /// <summary>
            /// 업로드할 파일이 존재하지 않음
            /// </summary>
            UploadFileNotFound = 0x10000000,
            /// <summary>
            /// 업로드 실패
            /// </summary>
            UploadFailed,
            /// <summary>
            /// 다운로드할 파일이 존재하지 않음
            /// </summary>
            DownloadFileNotFound,
            /// <summary>
            /// 다운로드 실패
            /// </summary>
            DownloadFileFailed,
            /// <summary>
            /// FTP 설정값이 잘못됨
            /// </summary>
            InvalidSettings,
            /// <summary>
            /// 보안(권한) 예외 발생
            /// </summary>
            SecurityException,
            /// <summary>
            /// 서버 URL 형식이 잘못됨
            /// </summary>
            UriFormatException,
            /// <summary>
            /// FTP 서버와 통신 중 진짜 에러 발생(폴더 경로가 포함된 경우 폴더 생성 실패인 경우에도 발생)
            /// </summary>
            WebException,
            /// <summary>
            /// 명령 수행 중 일반 예외 발생
            /// </summary>
            Exception,
            /// <summary>
            /// 서버를 찾을 수 없음
            /// </summary>
            ServerNotFound,
        }

        /// <summary>
        /// FTP 설정값 클래스
        /// </summary>
        public class Settings
        {
            /// <summary>
            /// FTP 서버 URL (ex: ftp://example.com)
            /// </summary>
            public string Url { get; }
            /// <summary>
            /// User ID
            /// </summary>
            public string User { get; }
            /// <summary>
            /// User Password
            /// </summary>
            public string Password { get; }
            /// <summary>
            /// 명령에 대한 Timeout 시간 (기본값: 15000ms)
            /// </summary>
            public int Timeout { get; }

            /// <summary>
            /// 생성자
            /// </summary>
            /// <param name="url">FTP 서버 URL (ex: ftp://example.com)</param>
            /// <param name="user">User ID</param>
            /// <param name="password">User Password</param>
            /// <param name="timeout">명령에 대한 Timeout 시간 (기본값: 15000ms)</param>
            public Settings(string url, string user, string password, int timeout = 15000)
            {
                Url = url;
                User = user;
                Password = password;
                Timeout = timeout;
            }

            /// <summary>
            /// 설정값 검증
            /// </summary>
            /// <returns></returns>
            public bool CheckValidation()
            {
                if (string.IsNullOrWhiteSpace(Url))
                {
                    return false;
                }
                if (!Url.StartsWithOrdinal("ftp://"))
                {
                    return false;
                }
                return true;
            }
        }

        private static readonly char[] DIR_TOKENS = new char[] { '/', '\\' };

        /// <summary>
        /// FTP 설정 데이터
        /// </summary>
        public Settings SettingData { get; }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="settings">FTP 설정 데이터</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public FtpClient(Settings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            SettingData = settings;
            if (!SettingData.CheckValidation())
            {
                throw new ArgumentException("Invalid settings(URL)");
            }
        }

        /// <summary>
        /// 서버가 존재하는지 확인하는 함수
        /// </summary>
        /// <returns>
        /// Success: 업로드 성공 <br/>
        /// InvalidSettings: FTP 설정값이 잘못됨 <br/>
        /// ServerNotFound: FTP 서버를 찾을 수 없음 <br/>
        /// </returns>
        public FtpResult CheckServer()
        {
            return InstantCheckServer(SettingData);
        }

        /// <summary>
        /// 서버가 존재하는지 확인하는 함수
        /// </summary>
        /// <param name="settings">FTP 설정값</param>
        /// <returns>
        /// Success: 업로드 성공 <br/>
        /// InvalidSettings: FTP 설정값이 잘못됨 <br/>
        /// ServerNotFound: FTP 서버를 찾을 수 없음 <br/>
        /// </returns>
        public static FtpResult InstantCheckServer(Settings settings)
        {
            if (!settings.CheckValidation())
            {
                return FtpResult.InvalidSettings;
            }

            FtpResult result = FtpResult.ServerNotFound;
            try
            {
                FtpWebRequest req = (FtpWebRequest)WebRequest.Create(settings.Url);
                req.KeepAlive = false;
                req.Method = WebRequestMethods.Ftp.ListDirectory;
                req.Timeout = settings.Timeout;
                req.UsePassive = true;
                if (!string.IsNullOrWhiteSpace(settings.User) &&
                    !string.IsNullOrWhiteSpace(settings.Password))
                {
                    req.Credentials = new NetworkCredential(settings.User, settings.Password);
                }

                using (FtpWebResponse res = (FtpWebResponse)req.GetResponse())
                {
                    result = FtpResult.Success;
                }
            }
            catch { }
            return result;
        }

        /// <summary>
        /// 파일을 업로드하는 함수
        /// </summary>
        /// <param name="sourceFilePath">업로드할 파일의 실제 경로 (전체경로)</param>
        /// <param name="destFilePath">업로드할 파일이 저장될 위치 (폴더경로 포함)</param>
        /// <returns>
        /// Success: 업로드 성공 <br/>
        /// UploadFileNotFound: 업로드할 파일이 존재하지 않음 <br/>
        /// InvalidSettings: FTP 설정값이 잘못됨 <br/>
        /// SecurityException: 보안(권한) 예외 발생 <br/>
        /// UriFormatException: URL 형식이 잘못됨 <br/>
        /// WebException: FTP 서버와 통신 중 진짜 에러 발생(폴더 경로가 포함된 경우 폴더 생성 실패인 경우에도 발생) <br/>
        /// Exception: 일반 예외 발생
        /// </returns>
        public FtpResult UploadFile(string sourceFilePath, string destFilePath)
        {
            return InstantUploadFile(SettingData, sourceFilePath, destFilePath);
        }

        /// <summary>
        /// 파일을 업로드하는 함수
        /// </summary>
        /// <param name="settings">FTP 설정값</param>
        /// <param name="sourceFilePath">업로드할 파일의 실제 경로 (전체경로)</param>
        /// <param name="destFilePath">업로드할 파일이 저장될 위치 (폴더경로 포함)</param>
        /// <returns>
        /// Success: 업로드 성공 <br/>
        /// UploadFileNotFound: 업로드할 파일이 존재하지 않음 <br/>
        /// InvalidSettings: FTP 설정값이 잘못됨 <br/>
        /// SecurityException: 보안(권한) 예외 발생 <br/>
        /// UriFormatException: URL 형식이 잘못됨 <br/>
        /// WebException: FTP 서버와 통신 중 진짜 에러 발생(폴더 경로가 포함된 경우 폴더 생성 실패인 경우에도 발생) <br/>
        /// Exception: 일반 예외 발생
        /// </returns>
        public static FtpResult InstantUploadFile(Settings settings, string sourceFilePath, string destFilePath)
        {
            if (string.IsNullOrWhiteSpace(sourceFilePath) ||
                !File.Exists(sourceFilePath))
            {
                return FtpResult.UploadFileNotFound;
            }
            if (!settings.CheckValidation())
            {
                return FtpResult.InvalidSettings;
            }

            FtpResult result = FtpResult.UploadFailed;
            string onlyFileName = Path.GetFileName(sourceFilePath);
            string targetPath;
            if (string.IsNullOrEmpty(destFilePath))
            {
                targetPath = $"{settings.Url}/{onlyFileName}";
            }
            else
            {
                string dir = Path.GetDirectoryName(destFilePath);
                if (!string.IsNullOrEmpty(dir))
                {
                    result = InstantCreateDirectory(settings, dir);
                    if (result != FtpResult.Success)
                    {
                        return result;
                    }
                }
                targetPath = $"{settings.Url}/{destFilePath}";
            }

            try
            {
                FtpWebRequest req = (FtpWebRequest)WebRequest.Create(targetPath);
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.Timeout = settings.Timeout;
                req.UseBinary = true;
                if (!string.IsNullOrWhiteSpace(settings.User) &&
                    !string.IsNullOrWhiteSpace(settings.Password))
                {
                    req.Credentials = new NetworkCredential(settings.User, settings.Password);
                }

                FileInfo fileInfo = new FileInfo(sourceFilePath);
                req.ContentLength = fileInfo.Length;

                using (FileStream fileStream = File.OpenRead(sourceFilePath))
                using (Stream reqStream = req.GetRequestStream())
                {
                    fileStream.CopyTo(reqStream);
                }

                using (FtpWebResponse res = (FtpWebResponse)req.GetResponse())
                {
#if DEBUG
                    Debug.WriteLine($"Upload File Complete, status {res.StatusDescription}");
#endif
                    result = FtpResult.Success;
                }
            }
            catch (SecurityException se)
            {
#if DEBUG
                Debug.WriteLine(se.Message);
#endif
                result = FtpResult.SecurityException;
            }
            catch (UriFormatException ufe)
            {
#if DEBUG
                Debug.WriteLine(ufe.Message);
#endif
                result = FtpResult.UriFormatException;
            }
            catch (WebException we)
            {
#if DEBUG
                Debug.WriteLine(we.Message);
#endif
                result = FtpResult.WebException;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                result = FtpResult.Exception;
            }
            return result;
        }

        /// <summary>
        /// 1개 이상의 파일을 업로드하는 함수
        /// </summary>
        /// <param name="sourceFilePaths">업로드할 파일의 실제 경로 (전체경로)</param>
        /// <param name="destFilePaths">업로드할 파일이 저장될 위치 (폴더경로 포함)</param>
        /// <returns>
        /// Success: 업로드 성공 <br/>
        /// UploadFileNotFound: 업로드할 파일이 존재하지 않음 <br/>
        /// InvalidSettings: FTP 설정값이 잘못됨 <br/>
        /// SecurityException: 보안(권한) 예외 발생 <br/>
        /// UriFormatException: URL 형식이 잘못됨 <br/>
        /// WebException: FTP 서버와 통신 중 진짜 에러 발생(폴더 경로가 포함된 경우 폴더 생성 실패인 경우에도 발생) <br/>
        /// Exception: 일반 예외 발생
        /// </returns>
        public FtpResult[] UploadFiles(string[] sourceFilePaths, string[] destFilePaths)
        {
            return InstantUploadFiles(SettingData, sourceFilePaths, destFilePaths);
        }

        /// <summary>
        /// 1개 이상의 파일을 업로드하는 함수
        /// </summary>
        /// <param name="settings">FTP 설정값</param>
        /// <param name="sourceFilePaths">업로드할 파일의 실제 경로 (전체경로)</param>
        /// <param name="destFilePaths">업로드할 파일이 저장될 위치 (폴더경로 포함)</param>
        /// <returns>
        /// Success: 업로드 성공 <br/>
        /// UploadFileNotFound: 업로드할 파일이 존재하지 않음 <br/>
        /// InvalidSettings: FTP 설정값이 잘못됨 <br/>
        /// SecurityException: 보안(권한) 예외 발생 <br/>
        /// UriFormatException: URL 형식이 잘못됨 <br/>
        /// WebException: FTP 서버와 통신 중 진짜 에러 발생(폴더 경로가 포함된 경우 폴더 생성 실패인 경우에도 발생) <br/>
        /// Exception: 일반 예외 발생
        /// </returns>
        public static FtpResult[] InstantUploadFiles(Settings settings, string[] sourceFilePaths, string[] destFilePaths)
        {
            bool isInvalidFiles = sourceFilePaths == null || destFilePaths == null || sourceFilePaths.Length != destFilePaths.Length ||
                sourceFilePaths.Length == 0;
            if (isInvalidFiles)
            {
                return new FtpResult[1] { FtpResult.UploadFileNotFound };
            }
            if (!settings.CheckValidation())
            {
                return new FtpResult[1] { FtpResult.InvalidSettings };
            }

            FtpResult[] results = new FtpResult[sourceFilePaths.Length];
            for (int i = 0; i < sourceFilePaths.Length; ++i)
            {
                results[i] = InstantUploadFile(settings, sourceFilePaths[i], destFilePaths[i]);
            }
            return results;
        }

        /// <summary>
        /// 1개 이상의 파일을 업로드하는 함수
        /// </summary>
        /// <param name="sourceFilePaths">업로드할 파일의 실제 경로 (전체경로)</param>
        /// <param name="destFilePaths">업로드할 파일이 저장될 위치 (폴더경로 포함)</param>
        /// <returns>
        /// Success: 업로드 성공 <br/>
        /// UploadFileNotFound: 업로드할 파일이 존재하지 않음 <br/>
        /// InvalidSettings: FTP 설정값이 잘못됨 <br/>
        /// SecurityException: 보안(권한) 예외 발생 <br/>
        /// UriFormatException: URL 형식이 잘못됨 <br/>
        /// WebException: FTP 서버와 통신 중 진짜 에러 발생(폴더 경로가 포함된 경우 폴더 생성 실패인 경우에도 발생) <br/>
        /// Exception: 일반 예외 발생
        /// </returns>
        public FtpResult[] UploadFiles(IList<string> sourceFilePaths, IList<string> destFilePaths)
        {
            return InstantUploadFiles(SettingData, sourceFilePaths, destFilePaths);
        }

        /// <summary>
        /// 1개 이상의 파일을 업로드하는 함수
        /// </summary>
        /// <param name="settings">FTP 설정값</param>
        /// <param name="sourceFilePaths">업로드할 파일의 실제 경로 (전체경로)</param>
        /// <param name="destFilePaths">업로드할 파일이 저장될 위치 (폴더경로 포함)</param>
        /// <returns>
        /// Success: 업로드 성공 <br/>
        /// UploadFileNotFound: 업로드할 파일이 존재하지 않음 <br/>
        /// InvalidSettings: FTP 설정값이 잘못됨 <br/>
        /// SecurityException: 보안(권한) 예외 발생 <br/>
        /// UriFormatException: URL 형식이 잘못됨 <br/>
        /// WebException: FTP 서버와 통신 중 진짜 에러 발생(폴더 경로가 포함된 경우 폴더 생성 실패인 경우에도 발생) <br/>
        /// Exception: 일반 예외 발생
        /// </returns>
        public static FtpResult[] InstantUploadFiles(Settings settings, IList<string> sourceFilePaths, IList<string> destFilePaths)
        {
            bool isInvalidFiles = sourceFilePaths == null || destFilePaths == null ||
                sourceFilePaths.Count != destFilePaths.Count || sourceFilePaths.Count == 0;
            if (isInvalidFiles)
            {
                return new FtpResult[1] { FtpResult.UploadFileNotFound };
            }
            if (!settings.CheckValidation())
            {
                return new FtpResult[1] { FtpResult.InvalidSettings };
            }

            FtpResult[] results = new FtpResult[sourceFilePaths.Count];
            for (int i = 0; i < sourceFilePaths.Count; ++i)
            {
                results[i] = InstantUploadFile(settings, sourceFilePaths[i], destFilePaths[i]);
            }
            return results;
        }

        /// <summary>
        /// 파일을 다운로드하는 함수
        /// </summary>
        /// <param name="sourceFilePath">다운로드할 파일의 경로 (FTP 기본경로 제외)</param>
        /// <param name="destFilePath">다운로드하여 저장할 위치 (전체경로와 파일 이름 포함)</param>
        /// <returns>
        /// Success: 다운로드 성공 <br/>
        /// InvalidSettings: FTP 설정값이 잘못됨 <br/>
        /// SecurityException: 보안(권한) 예외 발생 <br/>
        /// UriFormatException: URL 형식이 잘못됨 <br/>
        /// DownloadFileNotFound: 다운로드할 파일이 존재하지 않음 <br/>
        /// WebException: FTP 서버와 통신 중 진짜 에러 발생 <br/>
        /// Exception: 일반 예외 발생
        /// </returns>
        public FtpResult DownloadFile(string sourceFilePath, string destFilePath)
        {
            return InstantDownloadFile(SettingData, sourceFilePath, destFilePath);
        }

        /// <summary>
        /// 파일을 다운로드하는 함수
        /// </summary>
        /// <param name="settings">FTP 설정값</param>
        /// <param name="sourceFilePath">다운로드할 파일의 경로 (FTP 기본경로 제외)</param>
        /// <param name="destFilePath">다운로드하여 저장할 위치 (전체경로와 파일 이름 포함)</param>
        /// <returns>
        /// Success: 다운로드 성공 <br/>
        /// InvalidSettings: FTP 설정값이 잘못됨 <br/>
        /// SecurityException: 보안(권한) 예외 발생 <br/>
        /// UriFormatException: URL 형식이 잘못됨 <br/>
        /// DownloadFileNotFound: 다운로드할 파일이 존재하지 않음 <br/>
        /// WebException: FTP 서버와 통신 중 진짜 에러 발생 <br/>
        /// Exception: 일반 예외 발생
        /// </returns>
        public static FtpResult InstantDownloadFile(Settings settings, string sourceFilePath, string destFilePath)
        {
            if (!settings.CheckValidation())
            {
                return FtpResult.InvalidSettings;
            }
            string localDirectory = Path.GetDirectoryName(Path.GetFullPath(destFilePath));
            if (!Directory.Exists(localDirectory))
            {
                Directory.CreateDirectory(localDirectory);
            }

            FtpResult result;
            string targetPath = $"{settings.Url}/{sourceFilePath}";

            try
            {
                FtpWebRequest req = (FtpWebRequest)WebRequest.Create(targetPath);
                req.Method = WebRequestMethods.Ftp.DownloadFile;

                req.UseBinary = true;
                req.Timeout = settings.Timeout;

                if (!string.IsNullOrWhiteSpace(settings.User) && !string.IsNullOrWhiteSpace(settings.Password))
                {
                    req.Credentials = new NetworkCredential(settings.User, settings.Password);
                }

                using (FtpWebResponse res = (FtpWebResponse)req.GetResponse())
                using (Stream responseStream = res.GetResponseStream())
                using (FileStream fileStream = File.Create(destFilePath))
                {
                    responseStream.CopyTo(fileStream);
                }

                result = FtpResult.Success;
            }
            catch (SecurityException se)
            {
#if DEBUG
                Debug.WriteLine(se.Message);
#endif
                result = FtpResult.SecurityException;
            }
            catch (UriFormatException ufe)
            {
#if DEBUG
                Debug.WriteLine(ufe.Message);
#endif
                result = FtpResult.UriFormatException;
            }
            catch (WebException we)
            {
                if (we.Response is FtpWebResponse ftpRes)
                {
#if DEBUG
                    Debug.WriteLine($"Web Exception (FTP): [{ftpRes.StatusCode}] {ftpRes.StatusDescription}");
#endif
                    if (ftpRes.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        return FtpResult.DownloadFileNotFound;
                    }
                }
                else
                {
#if DEBUG
                    Debug.WriteLine(we.Message);
#endif
                }
                result = FtpResult.WebException;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                result = FtpResult.Exception;
            }

            return result;
        }

        /// <summary>
        /// 1개 이상의 파일을 다운로드하는 함수
        /// </summary>
        /// <param name="sourceFilePaths">다운로드할 파일의 경로 (FTP 기본경로 제외)</param>
        /// <param name="destFilePaths">다운로드하여 저장할 위치 (전체경로와 파일 이름 포함)</param>
        /// <returns>
        /// Success: 다운로드 성공 <br/>
        /// InvalidSettings: FTP 설정값이 잘못됨 <br/>
        /// SecurityException: 보안(권한) 예외 발생 <br/>
        /// UriFormatException: URL 형식이 잘못됨 <br/>
        /// DownloadFileNotFound: 다운로드할 파일이 존재하지 않음 <br/>
        /// WebException: FTP 서버와 통신 중 진짜 에러 발생 <br/>
        /// Exception: 일반 예외 발생
        /// </returns>
        public FtpResult[] DownloadFiles(string[] sourceFilePaths, string[] destFilePaths)
        {
            return InstantDownloadFiles(SettingData, sourceFilePaths, destFilePaths);
        }

        /// <summary>
        /// 1개 이상의 파일을 다운로드하는 함수
        /// </summary>
        /// <param name="settings">FTP 설정값</param>
        /// <param name="sourceFilePaths">다운로드할 파일의 경로 (FTP 기본경로 제외)</param>
        /// <param name="destFilePaths">다운로드하여 저장할 위치 (전체경로와 파일 이름 포함)</param>
        /// <returns>
        /// Success: 다운로드 성공 <br/>
        /// InvalidSettings: FTP 설정값이 잘못됨 <br/>
        /// SecurityException: 보안(권한) 예외 발생 <br/>
        /// UriFormatException: URL 형식이 잘못됨 <br/>
        /// DownloadFileNotFound: 다운로드할 파일이 존재하지 않음 <br/>
        /// WebException: FTP 서버와 통신 중 진짜 에러 발생 <br/>
        /// Exception: 일반 예외 발생
        /// </returns>
        public static FtpResult[] InstantDownloadFiles(Settings settings, string[] sourceFilePaths, string[] destFilePaths)
        {
            bool isInvalidFiles = sourceFilePaths == null || destFilePaths == null ||
                sourceFilePaths.Length != destFilePaths.Length || sourceFilePaths.Length == 0;
            if (isInvalidFiles)
            {
                return new FtpResult[1] { FtpResult.DownloadFileNotFound };
            }
            if (!settings.CheckValidation())
            {
                return new FtpResult[1] { FtpResult.InvalidSettings };
            }
            FtpResult[] results = new FtpResult[sourceFilePaths.Length];
            for (int i = 0; i < sourceFilePaths.Length; ++i)
            {
                results[i] = InstantDownloadFile(settings, sourceFilePaths[i], destFilePaths[i]);
            }
            return results;
        }

        /// <summary>
        /// 1개 이상의 파일을 다운로드하는 함수
        /// </summary>
        /// <param name="sourceFilePaths">다운로드할 파일의 경로 (FTP 기본경로 제외)</param>
        /// <param name="destFilePaths">다운로드하여 저장할 위치 (전체경로와 파일 이름 포함)</param>
        /// <returns>
        /// Success: 다운로드 성공 <br/>
        /// InvalidSettings: FTP 설정값이 잘못됨 <br/>
        /// SecurityException: 보안(권한) 예외 발생 <br/>
        /// UriFormatException: URL 형식이 잘못됨 <br/>
        /// DownloadFileNotFound: 다운로드할 파일이 존재하지 않음 <br/>
        /// WebException: FTP 서버와 통신 중 진짜 에러 발생 <br/>
        /// Exception: 일반 예외 발생
        /// </returns>
        public FtpResult[] DownloadFiles(IList<string> sourceFilePaths, IList<string> destFilePaths)
        {
            return InstantDownloadFiles(SettingData, sourceFilePaths, destFilePaths);
        }

        /// <summary>
        /// 1개 이상의 파일을 다운로드하는 함수
        /// </summary>
        /// <param name="settings">FTP 설정값</param>
        /// <param name="sourceFilePaths">다운로드할 파일의 경로 (FTP 기본경로 제외)</param>
        /// <param name="destFilePaths">다운로드하여 저장할 위치 (전체경로와 파일 이름 포함)</param>
        /// <returns>
        /// Success: 다운로드 성공 <br/>
        /// InvalidSettings: FTP 설정값이 잘못됨 <br/>
        /// SecurityException: 보안(권한) 예외 발생 <br/>
        /// UriFormatException: URL 형식이 잘못됨 <br/>
        /// DownloadFileNotFound: 다운로드할 파일이 존재하지 않음 <br/>
        /// WebException: FTP 서버와 통신 중 진짜 에러 발생 <br/>
        /// Exception: 일반 예외 발생
        /// </returns>
        public static FtpResult[] InstantDownloadFiles(Settings settings, IList<string> sourceFilePaths, IList<string> destFilePaths)
        {
            bool isInvalidFiles = sourceFilePaths == null || destFilePaths == null ||
                sourceFilePaths.Count != destFilePaths.Count || sourceFilePaths.Count == 0;
            if (isInvalidFiles)
            {
                return new FtpResult[1] { FtpResult.DownloadFileNotFound };
            }
            if (!settings.CheckValidation())
            {
                return new FtpResult[1] { FtpResult.InvalidSettings };
            }
            FtpResult[] results = new FtpResult[sourceFilePaths.Count];
            for (int i = 0; i < sourceFilePaths.Count; ++i)
            {
                results[i] = InstantDownloadFile(settings, sourceFilePaths[i], destFilePaths[i]);
            }
            return results;
        }

        /// <summary>
        /// 폴더를 생성하는 함수
        /// </summary>
        /// <param name="dirPath">폴더 경로 (ex: A/B/C)</param>
        /// <returns>
        /// Success: 폴더 생성 성공 <br/>
        /// WebException: 폴더 생성 중 진짜 에러 발생 <br/>
        /// Exception: 일반 예외 발생
        /// </returns>
        public FtpResult CreateDirectory(string dirPath)
        {
            return InstantCreateDirectory(SettingData, dirPath);
        }

        /// <summary>
        /// 폴더를 생성하는 함수
        /// </summary>
        /// <param name="settings">FTP 설정값</param>
        /// <param name="dirPath">폴더 경로 (ex: A/B/C)</param>
        /// <returns>
        /// Success: 폴더 생성 성공 <br/>
        /// WebException: 폴더 생성 중 진짜 에러 발생 <br/>
        /// Exception: 일반 예외 발생
        /// </returns>
        public static FtpResult InstantCreateDirectory(Settings settings, string dirPath)
        {
            string[] subFolders = dirPath.SplitRemoveEmpty(DIR_TOKENS);
            string currentPath = settings.Url;

            foreach (string folder in subFolders)
            {
                currentPath = $"{currentPath}/{folder}";

                try
                {
                    FtpWebRequest req = (FtpWebRequest)WebRequest.Create(currentPath);
                    req.Method = WebRequestMethods.Ftp.MakeDirectory;
                    req.Timeout = settings.Timeout;

                    if (!string.IsNullOrWhiteSpace(settings.User) &&
                        !string.IsNullOrWhiteSpace(settings.Password))
                    {
                        req.Credentials = new NetworkCredential(settings.User, settings.Password);
                    }

                    // 폴더 생성 명령 실행
                    using (FtpWebResponse res = (FtpWebResponse)req.GetResponse())
                    {
#if DEBUG
                        Debug.WriteLine($"Success: {currentPath}");
#endif
                    }
                }
                catch (WebException we)
                {
                    if (we.Response is FtpWebResponse ftpRes)
                    {
                        if (ftpRes.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                        {
#if DEBUG
                            Debug.WriteLine($"Already exist: {currentPath}");
#endif
                            continue;
                        }
                    }

#if DEBUG
                    Debug.WriteLine(we.Message);
#endif
                    return FtpResult.WebException;
                }
                catch (Exception ex)
                {
#if DEBUG
                    Debug.WriteLine(ex.Message);
#endif
                    return FtpResult.Exception;
                }
            }

            return FtpResult.Success;
        }
    }
}
#pragma warning restore SYSLIB0014
