// ======================================================================================================
// File Name        : XMLUtil.cs
// Project          : CSUtil
// Last Update      : 2025.05.19 - yc.jeon
// ======================================================================================================

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CSUtil
{
    /// <summary>
    /// XML 관련 유틸리티
    /// </summary>
    public static class XmlUtil
    {
        /// <summary>
        /// 데이터를 XML로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="dirPath">XML이 저장될 폴더의 경로</param>
        /// <param name="fileName">XML파일명</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">XML화 할 원본 데이터</param>
        /// <param name="extraTypes">XML Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        public static bool SerializeXml(string dirPath, string fileName, Type type, object data, Type[] extraTypes = null)
        {
            if (string.IsNullOrWhiteSpace(dirPath))
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:SerializeXml] Failed: {nameof(dirPath)} is null or empty.");
                return false;
            }

            if (dirPath.Length == 0)
            {
                dirPath = $".{Path.DirectorySeparatorChar}";
            }
            else if (dirPath.LastIndexOf(Path.DirectorySeparatorChar) != dirPath.Length - 1)
            {
                dirPath += Path.DirectorySeparatorChar;
            }

            bool result = true;
            try
            {
                DirectoryInfo info = new DirectoryInfo(dirPath);

                if (!info.Exists)
                {
                    info.Create();
                }

                XmlSerializer xmlSerializer = extraTypes != null ? new XmlSerializer(type, extraTypes) : new XmlSerializer(type);
                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    Encoding = Encoding.UTF8,
                    OmitXmlDeclaration = false,
                };
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                using (FileStream stream = new FileStream(Path.Combine(dirPath, fileName), FileMode.Create, FileAccess.Write, FileShare.Read))
                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    xmlSerializer.Serialize(stream, data, ns);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:SerializeXml] Exception: {ex}");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 데이터를 XML로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="filePath">XML 파일 경로(전체 Full 경로)</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">XML화 할 원본 데이터</param>
        /// <param name="extraTypes">XML Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        public static bool SerializeXml(string filePath, Type type, object data, Type[] extraTypes = null)
        {
            bool result = true;

            try
            {
                XmlSerializer xmlSerializer = extraTypes != null ? new XmlSerializer(type, extraTypes) : new XmlSerializer(type);
                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    Encoding = new UTF8Encoding(false),
                    OmitXmlDeclaration = false,
                };
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    xmlSerializer.Serialize(stream, data, ns);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:SerializeXml] Exception: {ex}");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 데이터를 XML로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="filePath">XML 파일 경로(전체 Full 경로)</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">XML화 할 원본 데이터</param>
        /// <param name="fileShare">Stream의 FileShare 옵션</param>
        /// <param name="retry">실패했을 때 재시도 횟수</param>
        /// <param name="delay">실패했을 때 재시도 딜레이 (msec)</param>
        /// <param name="extraTypes">XML Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        public static bool SerializeXml(string filePath,
            Type type,
            object data,
            FileShare fileShare,
            int retry = 5,
            int delay = 50,
            Type[] extraTypes = null)
        {
            bool result = true;
            int curRetry = 0;

            while (curRetry < retry)
            {
                try
                {
                    XmlSerializer xmlSerializer = extraTypes != null ? new XmlSerializer(type, extraTypes) : new XmlSerializer(type);
                    XmlWriterSettings settings = new XmlWriterSettings()
                    {
                        Indent = true,
                        Encoding = new UTF8Encoding(false),
                        OmitXmlDeclaration = false,
                    };
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");

                    using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, fileShare))
                    using (XmlWriter writer = XmlWriter.Create(stream, settings))
                    {
                        xmlSerializer.Serialize(stream, data, ns);
                    }
                    result = true;
                    break;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[CSUtil.XmlUtil:SerializeXml] Exception: {ex}");
                    result = false;
                    ++curRetry;

                    Thread.Sleep(delay);
                }
            }

            return result;
        }

        /// <summary>
        /// 비동기로 데이터를 XML로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="filePath">XML 파일 경로(전체 Full 경로)</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">XML화 할 원본 데이터</param>
        /// <param name="extraTypes">XML Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        public static async Task<bool> SerializeXmlAsync(string filePath, Type type, object data, Type[] extraTypes = null)
        {
            bool result = true;

            try
            {
                XmlSerializer xmlSerializer = extraTypes != null ? new XmlSerializer(type, extraTypes) : new XmlSerializer(type);
                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    Encoding = new UTF8Encoding(false),
                    OmitXmlDeclaration = false,
                };
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                using (StreamWriter stream = new StreamWriter(filePath))
                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    await Task.Run(() => xmlSerializer.Serialize(stream, data, ns));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:SerializeXmlAsync] Exception: {ex}");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 데이터를 XML 형식의 String으로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">XML화 할 원본 데이터</param>
        /// <param name="extraTypes">XML Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns></returns>
        public static string SerializeXmlString(Type type, object data, Type[] extraTypes = null)
        {
            string xmlString = string.Empty;

            try
            {
                XmlSerializer xmlSerializer = extraTypes != null ? new XmlSerializer(type, extraTypes) : new XmlSerializer(type);
                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    Encoding = new UTF8Encoding(false),
                    OmitXmlDeclaration = false,
                };
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                using (StringWriter stringWriter = new StringWriter())
                using (XmlWriter writer = XmlWriter.Create(stringWriter, settings))
                {
                    xmlSerializer.Serialize(stringWriter, data, ns);
                    xmlString = stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:SerializeXmlString] Exception: {ex}");
                xmlString = string.Empty;
            }

            return xmlString;
        }

        /// <summary>
        /// 데이터를 XML 형식의 String으로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">XML화 할 원본 데이터</param>
        /// <param name="extraTypes">XML Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns></returns>
        public static async Task<string> SerializeXmlStringAsync(Type type, object data, Type[] extraTypes = null)
        {
            string xmlString = string.Empty;

            try
            {
                XmlSerializer xmlSerializer = extraTypes != null ? new XmlSerializer(type, extraTypes) : new XmlSerializer(type);
                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    Encoding = new UTF8Encoding(false),
                    OmitXmlDeclaration = false,
                };
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                using (StringWriter stringWriter = new StringWriter())
                using (XmlWriter writer = XmlWriter.Create(stringWriter, settings))
                {
                    xmlString = await Task.Run(() =>
                    {
                        xmlSerializer.Serialize(stringWriter, data, ns);
                        return stringWriter.ToString();
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:SerializeXmlStringAsync] Exception: {ex}");
            }

            return xmlString;
        }

        /// <summary>
        /// 데이터를 XML로 변환후 암호화하여 저장하는 함수
        /// </summary>
        /// <param name="filePath">XML 파일 경로(전체 Full 경로)</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">XML화 할 원본 데이터</param>
        /// <param name="extraTypes">XML Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        public static bool SerializeEncryptXml(string filePath, Type type, object data, Type[] extraTypes = null)
        {
            string xmlString = SerializeXmlString(type, data, extraTypes);
            if (string.IsNullOrEmpty(xmlString))
            {
                return false;
            }

            try
            {
                string encrypt = CryptoUtil.Encrypt(xmlString);
                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(encrypt);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:SerializeEncryptXml] Exception: {ex}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 데이터를 XML로 변환후 암호화하여 저장하는 함수
        /// </summary>
        /// <param name="filePath">XML 파일 경로(전체 Full 경로)</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">XML화 할 원본 데이터</param>
        /// <param name="extraTypes">XML Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        public static async Task<bool> SerializeEncryptXmlAsync(string filePath, Type type, object data, Type[] extraTypes = null)
        {
            string xmlString = await SerializeXmlStringAsync(type, data, extraTypes);
            if (string.IsNullOrEmpty(xmlString))
            {
                return false;
            }

            try
            {
                string encrypt = CryptoUtil.Encrypt(xmlString);
                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    await writer.WriteAsync(encrypt);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:SerializeEncryptXmlAsync] Exception: {ex}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// XML을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">XML파일명(전체 경로)</param>
        /// <param name="type">데이터 타입</param>
        /// <param name="extraTypes">XML Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>불러온 XML 데이터</returns>
        public static object DeserializeXml(string filePath, Type type, Type[] extraTypes = null)
        {
            FileInfo info = new FileInfo(filePath);
            object data = null;

            if (!info.Exists)
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:DeserializeXml] Failed: {nameof(filePath)}({filePath}) is not exist.");
                return null;
            }

            try
            {
                XmlSerializer xmlSerializer = extraTypes != null ? new XmlSerializer(type, extraTypes) : new XmlSerializer(type);
                XmlReaderSettings settings = new XmlReaderSettings()
                {
                    DtdProcessing = DtdProcessing.Prohibit,
                };

                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (XmlReader xmlReader = XmlReader.Create(stream, settings))
                {
                    data = xmlSerializer.Deserialize(xmlReader);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:DeserializeXml] Exception: {ex}");
            }

            return data;
        }

        /// <summary>
        /// XML을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">XML파일명(전체 경로)</param>
        /// <param name="type">데이터 타입</param>
        /// <param name="fileShare">Stream의 FileShare 옵션</param>
        /// <param name="retry">실패했을 때 재시도 횟수</param>
        /// <param name="delay">실패했을 때 재시도 딜레이 (msec)</param>
        /// <param name="extraTypes">XML Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>불러온 XML 데이터</returns>
        public static object DeserializeXml(string filePath,
            Type type,
            FileShare fileShare,
            int retry = 5,
            int delay = 50,
            Type[] extraTypes = null)
        {
            FileInfo info = new FileInfo(filePath);
            object data = null;

            if (!info.Exists)
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:DeserializeXml] Failed: {nameof(filePath)}({filePath}) is not exist.");
                return null;
            }

            int curRetry = 0;
            while (curRetry < retry)
            {
                try
                {
                    XmlSerializer xmlSerializer = extraTypes != null ? new XmlSerializer(type, extraTypes) : new XmlSerializer(type);
                    XmlReaderSettings settings = new XmlReaderSettings()
                    {
                        DtdProcessing = DtdProcessing.Prohibit,
                    };

                    using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, fileShare))
                    using (XmlReader xmlReader = XmlReader.Create(stream, settings))
                    {
                        data = xmlSerializer.Deserialize(xmlReader);
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[CSUtil.XmlUtil:DeserializeXml] Exception: {ex}");
                    ++curRetry;
                    Thread.Sleep(delay);
                }
            }

            return data;
        }

        /// <summary>
        /// 비동기로 XML을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">XML파일명(전체 경로)</param>
        /// <param name="type">데이터 타입</param>
        /// <param name="extraTypes">XML Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>불러온 XML 데이터</returns>
        public static async Task<object> DeserializeXmlAsync(string filePath, Type type, Type[] extraTypes = null)
        {
            FileInfo info = new FileInfo(filePath);
            object data = null;

            if (!info.Exists)
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:DeserializeXmlAsync] Failed: {nameof(filePath)}({filePath}) is not exist.");
                return null;
            }

            try
            {
                XmlSerializer xmlSerializer = extraTypes != null ? new XmlSerializer(type, extraTypes) : new XmlSerializer(type);
                XmlReaderSettings settings = new XmlReaderSettings()
                {
                    DtdProcessing = DtdProcessing.Prohibit,
                };

                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (XmlReader xmlReader = XmlReader.Create(stream, settings))
                {
                    data = await Task.Run(() => xmlSerializer.Deserialize(xmlReader));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:DeserializeXmlAsync] Exception: {ex}");
            }

            return data;
        }

        /// <summary>
        /// String 형식의 XML 데이터를 클래스 데이터화하는 함수
        /// </summary>
        /// <param name="xmlString">String 형식의 XML 데이터</param>
        /// <param name="type">데이터 타입</param>
        /// <param name="extraTypes">XML Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>클래스로 변경된 데이터</returns>
        public static object DeserializeXmlString(string xmlString, Type type, Type[] extraTypes = null)
        {
            object data = null;
            try
            {
                XmlSerializer xmlSerializer = extraTypes != null ? new XmlSerializer(type, extraTypes) : new XmlSerializer(type);
                XmlReaderSettings settings = new XmlReaderSettings()
                {
                    DtdProcessing = DtdProcessing.Prohibit,
                };

                using (StringReader stringReader = new StringReader(xmlString))
                using (XmlReader xmlReader = XmlReader.Create(stringReader, settings))
                {
                    data = xmlSerializer.Deserialize(xmlReader);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:DeserializeXmlString] Exception: {ex}");
            }

            return data;
        }

        /// <summary>
        /// 비동기로 String 형식의 XML 데이터를 클래스 데이터화하는 함수
        /// </summary>
        /// <param name="xmlString">String 형식의 XML 데이터</param>
        /// <param name="type">데이터 타입</param>
        /// <param name="extraTypes">XML Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>클래스로 변경된 데이터</returns>
        public static async Task<object> DeserializeXmlStringAsync(string xmlString, Type type, Type[] extraTypes = null)
        {
            object data = null;
            try
            {
                XmlSerializer xmlSerializer = extraTypes != null ? new XmlSerializer(type, extraTypes) : new XmlSerializer(type);
                XmlReaderSettings settings = new XmlReaderSettings()
                {
                    DtdProcessing = DtdProcessing.Prohibit,
                };

                using (StringReader stringReader = new StringReader(xmlString))
                using (XmlReader xmlReader = XmlReader.Create(stringReader, settings))
                {
                    data = await Task.Run(() => xmlSerializer.Deserialize(xmlReader));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:DeserializeXmlStringAsync] Exception: {ex}");
            }

            return data;
        }

        /// <summary>
        /// 암호화 된 XML을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">XML파일명(전체 경로)</param>
        /// <param name="type">데이터 타입</param>
        /// <param name="extraTypes">XML Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>불러온 XML 데이터</returns>
        public static object DeserializeDecryptXml(string filePath, Type type, Type[] extraTypes = null)
        {
            string decrypt = string.Empty;
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string encrypt = reader.ReadToEnd();
                    decrypt = CryptoUtil.Decrypt(encrypt);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:DeserializeDecryptXml] Exception: {ex}");
                return null;
            }

            return DeserializeXmlString(decrypt, type, extraTypes);
        }

        /// <summary>
        /// 암호화 된 XML을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">XML파일명(전체 경로)</param>
        /// <param name="type">데이터 타입</param>
        /// <param name="extraTypes">XML Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>불러온 XML 데이터</returns>
        public static async Task<object> DeserializeDecryptXmlAsync(string filePath, Type type, Type[] extraTypes = null)
        {
            string decrypt = string.Empty;
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string encrypt = await reader.ReadToEndAsync();
                    decrypt = CryptoUtil.Decrypt(encrypt);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CSUtil.XmlUtil:DeserializeDecryptXmlAsync] Exception: {ex}");
                return null;
            }

            return await DeserializeXmlStringAsync(decrypt, type, extraTypes);
        }
    }
}
