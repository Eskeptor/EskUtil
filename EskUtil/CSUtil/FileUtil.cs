// ======================================================================================================
// File Name        : FileUtil.cs
// Project          : CSUtil
// Last Update      : 2024.01.27 - yc.jeon
// ======================================================================================================

using System.Diagnostics;
using System.Text;

namespace CSUtil
{
    /// <summary>
    /// 파일 관련 유틸리티
    /// </summary>
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
        public static bool FileRename(string path, string orgName, string chgName, FileTypes type)
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
                Console.WriteLine(ex.ToString());
#if DEBUG
                Debug.WriteLine(ex.ToString());
#endif
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
        public static bool ReadFile(string filePath, out string readData, ReadTypes type = ReadTypes.ReadToEnd, Encoding? encoding = null)
        {
            readData = string.Empty;

            if (string.IsNullOrEmpty(filePath))
            {
                return false;
            }

            encoding ??= Encoding.UTF8;

            if (!File.Exists(filePath))
            {
                return false;
            }

            bool result = false;

            try
            {
                switch (type)
                {
                    case ReadTypes.ReadToEnd:
                        {
                            using (StreamReader streamReader = new StreamReader(filePath, encoding))
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
                            StringBuilder stringBuilder = new StringBuilder();
                            using (StreamReader streamReader = new StreamReader(filePath, encoding))
                            {
                                string line = string.Empty;
                                while ((line = streamReader.ReadLine()!) != null)
                                {
                                    stringBuilder.Append(line);
                                }
                            }
                            readData = stringBuilder.ToString();
                            result = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                readData = ex.ToString();
#if DEBUG
                Debug.WriteLine(readData);
#endif
            }

            return result;
        }

        /// <summary>
        /// 파일에 데이터(string)를 쓰는 함수
        /// </summary>
        /// <param name="filePath">쓸 파일의 이름(전체 경로)</param>
        /// <param name="writeData">쓸 데이터(string)</param>
        /// <param name="type">Write 방식</param>
        /// <param name="encoding">데이터의 Encoding</param>
        /// <returns>
        /// true : 성공 <br/>
        /// false : 실패
        /// </returns>
        public static bool WriteFile(string filePath, string writeData, WriteTypes type = WriteTypes.Write, Encoding? encoding = null)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return false;
            }

            encoding ??= Encoding.UTF8;

            bool result = false;

            try
            {
                switch (type)
                {
                    case WriteTypes.Write:
                        {
                            using (StreamWriter streamWriter = new StreamWriter(filePath, false, encoding))
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
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.ToString());
#endif
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
            if (string.IsNullOrEmpty(filePath))
            {
                return false;
            }

            if (!File.Exists(filePath))
            {
                return false;
            }

            try
            {
                using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
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
                //case EnvironmentTypes.WINFORM:
                //    return System.Windows.Forms.Application.StartupPath;
                case EnvironmentTypes.WPF:
                    return AppDomain.CurrentDomain.BaseDirectory;
            }

            return string.Empty;
        }
    }
}
