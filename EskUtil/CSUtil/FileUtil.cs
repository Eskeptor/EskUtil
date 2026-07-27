// ======================================================================================================
// File Name        : FileUtil.cs
// Project          : CSUtil
// Last Update      : 2026.07.27 - yc.jeon (Eskeptor)
// ======================================================================================================

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Esk.GearForge.CSUtil
{
    public static class FileUtil
    {
        /// <summary>
        /// 파일의 타입
        /// </summary>
        public enum FileTypes
        {
            File = 0,
            Folder
        }
        /// <summary>
        /// ReadFile 사용 시 파일을 Read 하는 방식
        /// </summary>
        public enum ReadTypes
        {
            ReadToEnd = 0,
            ReadAllText,
            ReadLines,
        }
        /// <summary>
        /// WriteFile 사용 시 파일을 Write 하는 방식
        /// </summary>
        public enum WriteTypes
        {
            Write = 0,
            WriteAllText,
        }
        /// <summary>
        /// 현재 C#의 환경
        /// </summary>
        public enum EnvironmentTypes
        {
            UNIVERSAL_1 = 0,
            UNIVERSAL_2,
            WINFORM,
            WPF,
        }

        /// <summary>
        /// 파일 또는 폴더 이름 변경 함수
        /// </summary>
        /// <param name="path">원본 파일 또는 폴더가 존재하는 경로</param>
        /// <param name="orgName">원본 이름</param>
        /// <param name="chgName">변경 이름</param>
        /// <returns>
        /// true : 성공 <br/>
        /// false : 실패
        /// </returns>
        public static bool RenameFile(string path, string orgName, string chgName, FileTypes type)
        {
            bool result = false;

            try
            {
                string fullPath = Path.Combine(path, orgName);
                bool isExist = (type == FileTypes.File) ? File.Exists(fullPath) : Directory.Exists(fullPath);

                if (isExist)
                {
                    string newFullPath = Path.Combine(path, chgName);
                    if (type == FileTypes.File)
                    {
                        File.Move(fullPath, newFullPath);
                    }
                    else
                    {
                        Directory.Move(fullPath, newFullPath);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FileUtil:FileRename] Exception: {ex}");
            }

            return result;
        }

        /// <summary>
        /// 파일의 데이터를 string으로 읽어오는 함수
        /// </summary>
        /// <param name="filePath">읽어올 파일의 이름(전체 경로)</param>
        /// <param name="readData">읽어온 데이터</param>
        /// <param name="type">Read 방식</param>
        /// <param name="encoding">파일의 Encoding 형식</param>
        /// <returns>
        /// true : 성공 <br/>
        /// false : 실패
        /// </returns>
        public static bool ReadFile(string filePath, out string readData, ReadTypes type = ReadTypes.ReadToEnd, Encoding encoding = null)
        {
            readData = string.Empty;

            if (string.IsNullOrWhiteSpace(filePath))
            {
                Debug.WriteLine($"[FileUtil:ReadFile] Failed: {nameof(filePath)} is null or empty.");
                return false;
            }
            if (!File.Exists(filePath))
            {
                Debug.WriteLine($"[FileUtil:ReadFile] Failed: {nameof(filePath)}({filePath}) is not exist.");
                return false;
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            bool result = false;
            try
            {
                switch (type)
                {
                    case ReadTypes.ReadToEnd:
                        {
                            // 다른 프로세스가 접근 중이어도 읽기 동작 가능하도록 수정
                            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
                            using (StreamReader streamReader = new StreamReader(fileStream, encoding))
                            {
                                readData = streamReader.ReadToEnd();
                            }
                            result = true;
                        }
                        break;
                    case ReadTypes.ReadAllText:
                        {
                            readData = File.ReadAllText(filePath, encoding);
                            result = true;
                        }
                        break;
                    case ReadTypes.ReadLines:
                        {
                            StringBuilder stringBuilder = new StringBuilder(128);
                            // 다른 프로세스가 접근 중이어도 읽기 동작 가능하도록 수정
                            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
                            using (StreamReader streamReader = new StreamReader(fileStream, encoding))
                            {
                                string line = string.Empty;
                                while ((line = streamReader.ReadLine()) != null)
                                {
                                    stringBuilder.Append(line);
                                }
                            }
                            readData = stringBuilder.ToString();
                            result = true;
                        }
                        break;
                    default:
                        throw new ArgumentException("Invalid ReadTypes", nameof(type));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FileUtil:ReadFile] Exception: {ex}");
                readData = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// 파일에 데이터(string)를 쓰는 함수 (모든 내용을 덮어씀)
        /// </summary>
        /// <param name="filePath">쓸 파일의 이름(전체 경로)</param>
        /// <param name="writeData">쓸 데이터(string)</param>
        /// <param name="type">Write 방식</param>
        /// <param name="encoding">데이터의 Encoding</param>
        /// <returns>
        /// true : 성공 <br/>
        /// false : 실패
        /// </returns>
        public static bool WriteFile(string filePath, string writeData, WriteTypes type = WriteTypes.Write, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Debug.WriteLine($"[FileUtil:WriteFile] Failed: {nameof(filePath)} is null or empty.");
                return false;
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            bool result = false;
            try
            {
                switch (type)
                {
                    case WriteTypes.Write:
                        {
                            // 다른 프로세스가 접근 중이어도 쓰기 동작 가능하도록 수정
                            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                            using (StreamWriter streamWriter = new StreamWriter(fileStream, encoding))
                            {
                                streamWriter.Write(writeData);
                            }
                            result = true;
                        }
                        break;
                    case WriteTypes.WriteAllText:
                        {
                            File.WriteAllText(filePath, writeData);
                            result = true;
                        }
                        break;
                    default:
                        throw new ArgumentException("Invalid WriteTypes", nameof(type));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FileUtil:WriteFile] Exception: {ex}");
            }

            return result;
        }

        /// <summary>
        /// 파일이 열려있는지 확인하는 함수
        /// </summary>
        /// <param name="filePath">확인할 파일의 경로(전체 경로)</param>
        /// <returns>
        /// true : 파일이 열려 있음 <br/>
        /// false : 파일이 열려 있지 않음
        /// </returns>
        public static bool IsFileOpen(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Debug.WriteLine($"[FileUtil:IsFileOpen] Failed: {nameof(filePath)} is null or empty.");
                return false;
            }
            if (!File.Exists(filePath))
            {
                Debug.WriteLine($"[FileUtil:IsFileOpen] Failed: {nameof(filePath)}({filePath}) is not exist.");
                return false;
            }

            try
            {
                using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {

                }
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 현재 실행 경로를 반환하는 함수
        /// </summary>
        /// <param name="type">C# 환경 타입</param>
        /// <returns>
        /// 현재 실행 경로 <br/>
        /// 공백이 나오는 경우는 환경 타입이 잘못된 경우임
        /// </returns>
        public static string GetCurrentPath(EnvironmentTypes type)
        {
            switch (type)
            {
                case EnvironmentTypes.UNIVERSAL_1:
                    return Directory.GetCurrentDirectory();
                case EnvironmentTypes.UNIVERSAL_2:
                    return System.Environment.CurrentDirectory;
#if WINFORM
                case EnvironmentTypes.WINFORM:
                    return System.Windows.Forms.Application.StartupPath;
#endif
                case EnvironmentTypes.WPF:
                    return AppDomain.CurrentDomain.BaseDirectory;
                default:
                    return Directory.GetCurrentDirectory();
            }
        }

        /// <summary>
        /// 파일의 Version 정보를 받아오는 함수 <br/>
        /// </summary>
        /// <param name="filePath">파일의 경로 (전체 경로)</param>
        /// <param name="isIncludeBuildDate">파일 빌드 날짜 포함 유무</param>
        /// <returns>
        /// <paramref name="isIncludeBuildDate"/>이 false인 경우: 파일버전 <br/>
        /// <paramref name="isIncludeBuildDate"/>이 true인 경우: 파일버전 build (x64 or x86) yyyy-MM-dd HH:mm:ss.fff <br/>
        /// </returns>
        public static string GetFileVersion(string filePath, bool isIncludeBuildDate = true)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Debug.WriteLine($"[FileUtil:GetFileVersion] Failed: {nameof(filePath)} is null or empty.");
                return string.Empty;
            }
            if (!File.Exists(filePath))
            {
                Debug.WriteLine($"[FileUtil:GetFileVersion] Failed: {nameof(filePath)}({filePath}) is not exist.");
                return string.Empty;
            }

            FileInfo fileInfo = new FileInfo(filePath);
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(filePath);

            return isIncludeBuildDate ?
                $"{fileVersionInfo.FileVersion} build {fileInfo.LastWriteTime:yyyy-MM-dd HH:mm:ss.fff}" :
                fileVersionInfo.FileVersion;
        }

        /// <summary>
        /// 현재 실행 파일의 Version 정보를 받아오는 함수 <br/>
        /// </summary>
        /// <param name="isIncludeBuildDate">파일 빌드 날짜 포함 유무</param>
        /// <returns>
        /// <paramref name="isIncludeBuildDate"/>이 false인 경우: 파일버전 <br/>
        /// <paramref name="isIncludeBuildDate"/>이 true인 경우: 파일버전 build (x64 or x86) yyyy-MM-dd HH:mm:ss.fff <br/>
        /// </returns>
        public static string GetFileVersion(bool isIncludeBuildDate = true)
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            string exePath = assembly.Location;
            Version version = assembly.GetName().Version;

            if (isIncludeBuildDate)
            {
                DateTime lastWriteTime = File.GetLastWriteTime(exePath);
                string env = IntPtr.Size == 8 ? "x64" : "x86";
                return $"{version} build ({env}) {lastWriteTime:yyyy-MM-dd HH:mm:ss.fff}";
            }
            else
            {
                return version.ToString();
            }
        }

        /// <summary>
        /// 파일의 이름과 Version 정보를 받아오는 함수 <br/>
        /// </summary>
        /// <param name="filePath">파일의 경로 (전체 경로)</param>
        /// <param name="isIncludeBuildDate">파일 빌드 날짜 포함 유무</param>
        /// <returns>
        /// <paramref name="isIncludeBuildDate"/>이 false인 경우: 파일명 (파일버전) <br/>
        /// <paramref name="isIncludeBuildDate"/>이 true인 경우: 파일명 (파일버전 build (x64 or x86) yyyy-MM-dd HH:mm:ss.fff) <br/>
        /// </returns>
        public static string GetFileNameWithVersion(string filePath, bool isIncludeBuildDate = true)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Debug.WriteLine($"[FileUtil:GetFileNameWithVersion] Failed: {nameof(filePath)} is null or empty.");
                return string.Empty;
            }
            if (!File.Exists(filePath))
            {
                Debug.WriteLine($"[FileUtil:GetFileNameWithVersion] Failed: {nameof(filePath)}({filePath}) is not exist.");
                return string.Empty;
            }

            AssemblyName assemblyName = AssemblyName.GetAssemblyName(filePath);
            if (isIncludeBuildDate)
            {
                DateTime lastWriteTime = File.GetLastWriteTime(filePath);
                string env = IntPtr.Size == 8 ? "x64" : "x86";
                return $"{assemblyName.Name} ({assemblyName.Version} build ({env}) {lastWriteTime:yyyy-MM-dd HH:mm:ss.fff})";
            }
            else
            {
                return $"{assemblyName.Name} ({assemblyName.Version})";
            }
        }

        /// <summary>
        /// 현재 실행 파일의 이름과 Version 정보를 받아오는 함수 <br/>
        /// </summary>
        /// <param name="isIncludeBuildDate">파일 빌드 날짜 포함 유무</param>
        /// <returns>
        /// <paramref name="isIncludeBuildDate"/>이 false인 경우: 파일명 (파일버전) <br/>
        /// <paramref name="isIncludeBuildDate"/>이 true인 경우: 파일명 (파일버전 build (x64 or x86) yyyy-MM-dd HH:mm:ss.fff) <br/>
        /// </returns>
        public static string GetFileNameWithVersion(bool isIncludeBuildDate = true)
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            string exePath = assembly.Location;
            AssemblyProductAttribute fileNameAttribute = (AssemblyProductAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyProductAttribute));
            AssemblyFileVersionAttribute fileVersionAttribute = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
            if (isIncludeBuildDate)
            {
                DateTime lastWriteTime = File.GetLastWriteTime(exePath);
                string env = IntPtr.Size == 8 ? "x64" : "x86";
                return $"{fileNameAttribute.Product} ({fileVersionAttribute.Version} build ({env}) {lastWriteTime:yyyy-MM-dd HH:mm:ss.fff})";
            }
            else
            {
                return $"{fileNameAttribute.Product} ({fileVersionAttribute.Version})";
            }
        }

        /// <summary>
        /// 파일의 내용을 변경하는 함수
        /// </summary>
        /// <param name="filePath">변경할 파일의 전체경로</param>
        /// <param name="prevContext">변경할 내용</param>
        /// <param name="newContext">변경된 내용</param>
        /// <returns>
        /// true: 변경 성공 <br/>
        /// false: 변경 실패
        /// </returns>
        public static bool ChangeContext(string filePath, string prevContext, string newContext)
        {
            if (string.IsNullOrEmpty(prevContext))
            {
                Debug.WriteLine($"[FileUtil:ChangeContext] Failed: {nameof(prevContext)} is null or empty.");
                return false;
            }
            if (!File.Exists(filePath))
            {
                Debug.WriteLine($"[FileUtil:ChangeContext] Failed: {nameof(filePath)}({filePath}) is not exist.");
                return false;
            }

            string tempFile = $"{filePath}_tempFile";
            using (StreamReader sr = new StreamReader(filePath))
            using (StreamWriter sw = new StreamWriter(tempFile, false))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    line = line.Replace(prevContext, newContext);
                    sw.WriteLine(line);

                    line = sr.ReadLine();
                }
            }

            bool isComplete;
            try
            {
                File.Delete(filePath);
                File.Move(tempFile, filePath);
                isComplete = File.Exists(filePath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FileUtil:ChangeContext] Exception: {ex}");
                isComplete = false;
            }

            return isComplete;
        }

        /// <summary>
        /// 파일을 복사하는 함수
        /// </summary>
        /// <param name="sourceFilePath">복사 대상 파일의 전체 경로</param>
        /// <param name="copiedFilePath">복사 위치 전체 경로</param>
        /// <param name="overwrite">파일이 존재하는 경우 덮어쓸지 유무</param>
        /// <returns>
        /// true: 복사 성공 <br/>
        /// false: 복사 실패(IOException, UnauthorizedAccessException, Exception 발생)
        /// </returns>
        public static bool CopyFile(string sourceFilePath, string copiedFilePath, bool overwrite)
        {
            try
            {
                File.Copy(sourceFilePath, copiedFilePath, overwrite: overwrite);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FileUtil:CopyFile] Exception: {ex}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 특정 디렉토리에 있는 모든 파일을 삭제하는 함수 <br/>
        /// </summary>
        /// <param name="directoryPath">파일을 삭제하려는 디렉터리의 전체 경로</param>
        /// <returns></returns>
        /// <remarks>[NEW][2026.03.30 - yc.jeon]</remarks>
        public static bool DeleteFilesInDirectory(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                Debug.WriteLine($"[FileUtil:DeleteFilesInDirectory] Failed: {nameof(directoryPath)} is null or empty.");
                return false;
            }
            if (!Directory.Exists(directoryPath))
            {
                Debug.WriteLine($"[FileUtil:DeleteFilesInDirectory] Failed: {nameof(directoryPath)}({directoryPath}) is not exist.");
                return false;
            }
            try
            {
                string[] files = Directory.GetFiles(directoryPath);
                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FileUtil:DeleteFilesInDirectory] Exception: {ex}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 파일 경로인지 확인하는 함수
        /// </summary>
        /// <param name="path">확인할 파일 경로</param>
        /// <param name="useFileExists">파일 존재 여부를 확인할지 여부</param>
        /// <returns>
        /// true: 파일 경로임 <br/>
        /// false: 파일 경로가 아님 (파일이 존재하는지 여부는 useFileExists 값에 따라 다름) <br/>
        /// </returns>
        /// <remarks>[NEW][2026.05.09 - yc.jeon]</remarks>
        public static bool IsFilePath(string path, bool useFileExists)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                Debug.WriteLine($"[FileUtil:IsFilePath] Failed: {nameof(path)} is null or empty.");
                return false;
            }
            bool isFilePath;
            try
            {
                bool isPath = (path.Length >= 3 && char.IsLetter(path[0]) && path[1] == ':' && path[2] == '\\')
                    || path.StartsWithOrdinal(@"\\");
                if (useFileExists)
                {
                    isPath = isPath && File.Exists(path);
                }
                isFilePath = isPath;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FileUtil:IsFilePath] Exception: {ex}");
                return false;
            }
            return isFilePath;
        }
    }
}
