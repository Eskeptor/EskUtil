// ======================================================================================================
// File Name        : XmlUtil.cs
// Project          : CSUtil
// Last Update      : 2026.04.21 - yc.jeon (Eskeptor)
// ======================================================================================================

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Esk.GearForge.CSUtil
{
    /// <summary>
    /// XML 관련 유틸리티
    /// </summary>
    public static class XmlUtil
    {
        /// <summary>
        /// XmlSerializer 캐시
        /// </summary>
        /// <remarks>
        /// [MOD][2026.02.11 - yc.jeon] <br/>
        /// 1) XmlSerializer의 extraTypes 파라미터가 들어가는 생성자의 경우 내부적으로 캐시를 하지 않아 메모리 누수가 발생하기 때문에 캐싱하여 사용 <br/>
        /// 관련 자료: https://learn.microsoft.com/ko-kr/dotnet/api/system.xml.serialization.xmlserializer.-ctor?view=net-8.0#system-xml-serialization-xmlserializer-ctor(system-type-system-type()) <br/>
        /// 관련 자료: https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-xml-serialization-xmlserializer#dynamically-generated-assemblies <br/>
        /// 관련 자료: https://stackoverflow.com/questions/23897145/memory-leak-using-streamreader-and-xmlserializer
        /// </remarks>
        private static readonly ConcurrentDictionary<string, XmlSerializer> _serializerCache =
            new ConcurrentDictionary<string, XmlSerializer>();

        /// <summary>
        /// 데이터를 Xml로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="dirPath">Xml이 저장될 폴더의 경로</param>
        /// <param name="fileName">Xml파일명</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">Xml화 할 원본 데이터</param>
        /// <param name="extraTypes">Xml Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        public static bool SerializeXml(string dirPath, string fileName, Type type, object data, Type[] extraTypes = null)
        {
            if (string.IsNullOrWhiteSpace(dirPath))
            {
                Debug.WriteLine($"[Esk.GearForge.CSUtil.XmlUtil:SerializeXML] Failed: {nameof(dirPath)} is null or empty.");
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

                XmlSerializer xmlSerializer = GetSerializer(type, extraTypes);
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
                Debug.WriteLine($"[Esk.GearForge.CSUtil.XmlUtil:SerializeXML] Exception: {ex}");
                result = false;
            }

            return result;
        }
        /// <summary>
        /// 데이터를 Xml로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="dirPath">Xml이 저장될 폴더의 경로</param>
        /// <param name="fileName">Xml파일명</param>
        /// <param name="data">Xml화 할 원본 데이터</param>
        /// <param name="extraTypes">Xml Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        /// <remarks>[NEW][2026.02.13 - yc.jeon]</remarks>
        public static bool SerializeXml<T>(string dirPath, string fileName, T data, Type[] extraTypes = null)
        {
            return SerializeXml(dirPath, fileName, typeof(T), data, extraTypes);
        }

        /// <summary>
        /// 데이터를 Xml로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일 경로(전체 Full 경로)</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">Xml화 할 원본 데이터</param>
        /// <param name="extraTypes">Xml Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        public static bool SerializeXml(string filePath, Type type, object data, Type[] extraTypes = null)
        {
            bool result = true;

            try
            {
                XmlSerializer xmlSerializer = GetSerializer(type, extraTypes);
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
                Debug.WriteLine($"[Esk.GearForge.CSUtil.XmlUtil:SerializeXML] Exception: {ex}");
                result = false;
            }

            return result;
        }
        /// <summary>
        /// 데이터를 Xml로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일 경로(전체 Full 경로)</param>
        /// <param name="data">Xml화 할 원본 데이터</param>
        /// <param name="extraTypes">Xml Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        /// <remarks>[NEW][2026.02.13 - yc.jeon]</remarks>
        public static bool SerializeXml<T>(string filePath, T data, Type[] extraTypes = null)
        {
            return SerializeXml(filePath, typeof(T), data, extraTypes);
        }

        /// <summary>
        /// 데이터를 Xml로 변환후 암호화하여 저장하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일 경로(전체 Full 경로)</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">Xml화 할 원본 데이터</param>
        /// <param name="extraTypes">Xml Serialize에서 사용할 추가적 Types (기본값: null)</param>
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
                Debug.WriteLine($"[Esk.GearForge.CSUtil.XmlUtil:SerializeEncryptXml] Exception: {ex}");
                return false;
            }

            return true;
        }
        /// <summary>
        /// 데이터를 Xml로 변환후 암호화하여 저장하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일 경로(전체 Full 경로)</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">Xml화 할 원본 데이터</param>
        /// <param name="extraTypes">Xml Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        /// <remarks>[NEW][2026.02.13 - yc.jeon]</remarks>
        public static bool SerializeEncryptXml<T>(string filePath, T data, Type[] extraTypes = null)
        {
            return SerializeEncryptXml(filePath, typeof(T), data, extraTypes);
        }

        /// <summary>
        /// 데이터를 Xml 형식의 String으로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">Xml화 할 원본 데이터</param>
        /// <param name="extraTypes">Xml Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>Xml 형식을 String으로 변환한 값</returns>
        public static string SerializeXmlString(Type type, object data, Type[] extraTypes = null)
        {
            string xmlString = string.Empty;

            try
            {
                XmlSerializer xmlSerializer = GetSerializer(type, extraTypes);
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
                Debug.WriteLine($"[Esk.GearForge.CSUtil.XmlUtil:SerializeXmlString] Exception: {ex}");
                xmlString = string.Empty;
            }

            return xmlString;
        }
        /// <summary>
        /// 데이터를 Xml 형식의 String으로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="data">Xml화 할 원본 데이터</param>
        /// <param name="extraTypes">Xml Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>Xml 형식을 String으로 변환한 값</returns>
        /// <remarks>[NEW][2026.02.13 - yc.jeon]</remarks>
        public static string SerializeXmlString<T>(T data, Type[] extraTypes = null)
        {
            return SerializeXmlString(typeof(T), data, extraTypes);
        }

        /// <summary>
        /// 데이터를 Xml로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일 경로(전체 Full 경로)</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">Xml화 할 원본 데이터</param>
        /// <param name="fileShare">Stream의 FileShare 옵션</param>
        /// <param name="retry">실패했을 때 재시도 횟수</param>
        /// <param name="delay">실패했을 때 재시도 딜레이 (msec)</param>
        /// <param name="extraTypes">Xml Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        public static bool SerializeXml(
            string filePath,
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
                    XmlSerializer xmlSerializer = GetSerializer(type, extraTypes);
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
                    Debug.WriteLine($"[Esk.GearForge.CSUtil.XmlUtil:SerializeXML] Exception: {ex}");
                    result = false;
                    ++curRetry;

                    Thread.Sleep(delay);
                }
            }

            return result;
        }
        /// <summary>
        /// 데이터를 Xml로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일 경로(전체 Full 경로)</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">Xml화 할 원본 데이터</param>
        /// <param name="fileShare">Stream의 FileShare 옵션</param>
        /// <param name="retry">실패했을 때 재시도 횟수</param>
        /// <param name="delay">실패했을 때 재시도 딜레이 (msec)</param>
        /// <param name="extraTypes">Xml Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        /// <remarks>[NEW][2026.02.13 - yc.jeon]</remarks>
        public static bool SerializeXml<T>(
            string filePath,
            T data,
            FileShare fileShare,
            int retry = 5,
            int delay = 50,
            Type[] extraTypes = null)
        {
            return SerializeXml(filePath, typeof(T), data, fileShare, retry, delay, extraTypes);
        }

        /// <summary>
        /// 비동기로 데이터를 Xml로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일 경로(전체 Full 경로)</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">Xml화 할 원본 데이터</param>
        /// <param name="extraTypes">Xml Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        public static async Task<bool> SerializeXmlAsync(
            string filePath,
            Type type,
            object data,
            Type[] extraTypes = null)
        {
            bool result = true;

            try
            {
                XmlSerializer xmlSerializer = GetSerializer(type, extraTypes);
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
                Debug.WriteLine($"[Esk.GearForge.CSUtil.XmlUtil:SerializeXMLAsync] Exception: {ex}");
                result = false;
            }

            return result;
        }
        /// <summary>
        /// 비동기로 데이터를 Xml로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일 경로(전체 Full 경로)</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">Xml화 할 원본 데이터</param>
        /// <param name="extraTypes">Xml Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        /// <remarks>[NEW][2026.02.13 - yc.jeon]</remarks>
        public static async Task<bool> SerializeXmlAsync<T>(
            string filePath,
            T data,
            Type[] extraTypes = null)
        {
            return await SerializeXmlAsync(filePath, typeof(T), data, extraTypes);
        }

        /// <summary>
        /// 데이터를 Xml 형식의 String으로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">Xml화 할 원본 데이터</param>
        /// <param name="extraTypes">Xml Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>Xml 형식을 String으로 변환한 값</returns>
        public static async Task<string> SerializeXmlStringAsync(
            Type type,
            object data,
            Type[] extraTypes = null)
        {
            string xmlString = string.Empty;

            try
            {
                XmlSerializer xmlSerializer = GetSerializer(type, extraTypes);
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
                Debug.WriteLine($"[Esk.GearForge.CSUtil.XmlUtil:SerializeXmlStringAsync] Exception: {ex}");
            }

            return xmlString;
        }
        /// <summary>
        /// 데이터를 Xml 형식의 String으로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="data">Xml화 할 원본 데이터</param>
        /// <param name="extraTypes">Xml Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>Xml 형식을 String으로 변환한 값</returns>
        /// <remarks>[NEW][2026.02.13 - yc.jeon]</remarks>
        public static async Task<string> SerializeXmlStringAsync<T>(T data, Type[] extraTypes = null)
        {
            return await SerializeXmlStringAsync(typeof(T), data, extraTypes);
        }

        /// <summary>
        /// 데이터를 Xml로 변환후 암호화하여 저장하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일 경로(전체 Full 경로)</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">Xml화 할 원본 데이터</param>
        /// <param name="extraTypes">Xml Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        public static async Task<bool> SerializeEncryptXmlAsync(
            string filePath,
            Type type,
            object data,
            Type[] extraTypes = null)
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
                Debug.WriteLine($"[Esk.GearForge.CSUtil.XmlUtil:SerializeEncryptXmlAsync] Exception: {ex}");
                return false;
            }

            return true;
        }
        /// <summary>
        /// 데이터를 Xml로 변환후 암호화하여 저장하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일 경로(전체 Full 경로)</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">Xml화 할 원본 데이터</param>
        /// <param name="extraTypes">Xml Serialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        /// <remarks>[NEW][2026.02.13 - yc.jeon]</remarks>
        public static async Task<bool> SerializeEncryptXmlAsync<T>(
            string filePath,
            T data,
            Type[] extraTypes = null)
        {
            return await SerializeEncryptXmlAsync(filePath, typeof(T), data, extraTypes);
        }

        /// <summary>
        /// Xml을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일명(전체 경로)</param>
        /// <param name="type">데이터 타입</param>
        /// <param name="extraTypes">Xml Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>불러온 Xml 데이터</returns>
        public static object DeserializeXml(
            string filePath,
            Type type,
            Type[] extraTypes = null)
        {
            FileInfo info = new FileInfo(filePath);
            object data = null;

            if (!info.Exists)
            {
                string log = $"[Esk.GearForge.CSUtil.XmlUtil:DeserializeXML] Failed: {nameof(filePath)}({filePath}) is not exist.";
                Debug.WriteLine(log);
                return null;
            }

            try
            {
                XmlSerializer xmlSerializer = GetSerializer(type, extraTypes);
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
                Debug.WriteLine($"[Esk.GearForge.CSUtil.XmlUtil:DeserializeXML] Exception: {ex}");
            }

            return data;
        }
        /// <summary>
        /// Xml을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일명(전체 경로)</param>
        /// <param name="data">불러온 Xml 데이터</param>
        /// <param name="extraTypes">Xml Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        /// <remarks>[NEW][2026.02.13 - yc.jeon]</remarks>
        public static bool DeserializeXml<T>(string filePath, out T data, Type[] extraTypes = null)
        {
            if (DeserializeXml(filePath, typeof(T), extraTypes) is T orgData)
            {
                data = orgData;
                return true;
            }

            data = default;
            return false;
        }

        /// <summary>
        /// 암호화 된 Xml을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일명(전체 경로)</param>
        /// <param name="type">데이터 타입</param>
        /// <param name="extraTypes">Xml Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>불러온 Xml 데이터</returns>
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
                Debug.WriteLine($"[Esk.GearForge.CSUtil.XmlUtil:DeserializeDecryptXml] Exception: {ex}");
                return null;
            }

            return DeserializeXmlString(decrypt, type, extraTypes);
        }
        /// <summary>
        /// 암호화 된 Xml을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일명(전체 경로)</param>
        /// <param name="data">불러온 Xml 데이터</param>
        /// <param name="extraTypes">Xml Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        /// <remarks>[NEW][2026.02.13 - yc.jeon]</remarks>
        public static bool DeserializeDecryptXml<T>(string filePath, out T data, Type[] extraTypes = null)
        {
            if (DeserializeDecryptXml(filePath, typeof(T), extraTypes) is T orgData)
            {
                data = orgData;
                return true;
            }
            data = default;
            return false;
        }

        /// <summary>
        /// String 형식의 Xml 데이터를 클래스 데이터화하는 함수
        /// </summary>
        /// <param name="xmlString">String 형식의 Xml 데이터</param>
        /// <param name="type">데이터 타입</param>
        /// <param name="extraTypes">Xml Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>클래스로 변경된 데이터</returns>
        public static object DeserializeXmlString(string xmlString, Type type, Type[] extraTypes = null)
        {
            object data = null;
            try
            {
                XmlSerializer xmlSerializer = GetSerializer(type, extraTypes);
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
                Debug.WriteLine($"[Esk.GearForge.CSUtil.XmlUtil:DeserializeXmlString] Exception: {ex}");
            }

            return data;
        }
        /// <summary>
        /// String 형식의 Xml 데이터를 클래스 데이터화하는 함수
        /// </summary>
        /// <param name="xmlString">String 형식의 Xml 데이터</param>
        /// <param name="data">String 형식의 Xml 데이터를 클래스화한 데이터</param>
        /// <param name="extraTypes">Xml Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        /// <remarks>[NEW][2026.02.13 - yc.jeon]</remarks>
        public static bool DeserializeXmlString<T>(string xmlString, out T data, Type[] extraTypes = null)
        {
            if (DeserializeXmlString(xmlString, typeof(T), extraTypes) is T orgData)
            {
                data = orgData;
                return true;
            }
            data = default;
            return false;
        }

        /// <summary>
        /// Xml을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일명(전체 경로)</param>
        /// <param name="type">데이터 타입</param>
        /// <param name="fileShare">Stream의 FileShare 옵션</param>
        /// <param name="retry">실패했을 때 재시도 횟수</param>
        /// <param name="delay">실패했을 때 재시도 딜레이 (msec)</param>
        /// <param name="extraTypes">Xml Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>불러온 Xml 데이터</returns>
        public static object DeserializeXml(
            string filePath,
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
                string log = $"[Esk.GearForge.CSUtil.XmlUtil:DeserializeXML] Failed: {nameof(filePath)}({filePath}) is not exist.";
                Debug.WriteLine(log);
                return null;
            }

            int curRetry = 0;
            while (curRetry < retry)
            {
                try
                {
                    XmlSerializer xmlSerializer = GetSerializer(type, extraTypes);
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
                    Debug.WriteLine($"[Esk.GearForge.CSUtil.XmlUtil:DeserializeXML] Exception: {ex}");
                    ++curRetry;
                    Thread.Sleep(delay);
                }
            }

            return data;
        }
        /// <summary>
        /// Xml을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일명(전체 경로)</param>
        /// <param name="data">불러온 Xml 데이터</param>
        /// <param name="fileShare">Stream의 FileShare 옵션</param>
        /// <param name="retry">실패했을 때 재시도 횟수</param>
        /// <param name="delay">실패했을 때 재시도 딜레이 (msec)</param>
        /// <param name="extraTypes">Xml Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// true: 성공 <br/>
        /// false: 실패
        /// </returns>
        /// <remarks>[NEW][2026.02.13 - yc.jeon]</remarks>
        public static bool DeserializeXml<T>(
            string filePath,
            out T data,
            FileShare fileShare,
            int retry = 5,
            int delay = 50,
            Type[] extraTypes = null)
        {
            if (DeserializeXml(filePath, typeof(T), fileShare, retry, delay, extraTypes) is T orgData)
            {
                data = orgData;
                return true;
            }
            data = default;
            return false;
        }

        /// <summary>
        /// 비동기로 Xml을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일명(전체 경로)</param>
        /// <param name="type">데이터 타입</param>
        /// <param name="extraTypes">Xml Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>불러온 Xml 데이터</returns>
        public static async Task<object> DeserializeXmlAsync(
            string filePath,
            Type type,
            Type[] extraTypes = null)
        {
            FileInfo info = new FileInfo(filePath);
            object data = null;

            if (!info.Exists)
            {
                string log = $"[Esk.GearForge.CSUtil.XmlUtil:DeserializeXMLAsync] Failed: {nameof(filePath)}({filePath}) is not exist.";
                Debug.WriteLine(log);
                return null;
            }

            try
            {
                XmlSerializer xmlSerializer = GetSerializer(type, extraTypes);
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
                Debug.WriteLine($"[Esk.GearForge.CSUtil.XmlUtil:DeserializeXMLAsync] Exception: {ex}");
            }

            return data;
        }
        /// <summary>
        /// 비동기로 Xml을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일명(전체 경로)</param>
        /// <param name="extraTypes">Xml Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// (result, data) <br/>
        /// result: true: 성공 / false: 실패 <br/>
        /// data: 불러온 Xml 데이터 <br/>
        /// </returns>
        /// <remarks>[NEW][2026.02.13 - yc.jeon]</remarks>
        public static async Task<(bool, T)> DeserializeXmlAsync<T>(
            string filePath,
            Type[] extraTypes = null)
        {
            if (await DeserializeXmlAsync(filePath, typeof(T), extraTypes) is T orgData)
            {
                return (true, orgData);
            }
            return (false, default);
        }

        /// <summary>
        /// 비동기로 String 형식의 Xml 데이터를 클래스 데이터화하는 함수
        /// </summary>
        /// <param name="xmlString">String 형식의 Xml 데이터</param>
        /// <param name="type">데이터 타입</param>
        /// <param name="extraTypes">Xml Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>클래스로 변경된 데이터</returns>
        public static async Task<object> DeserializeXmlStringAsync(
            string xmlString,
            Type type,
            Type[] extraTypes = null)
        {
            object data = null;
            try
            {
                XmlSerializer xmlSerializer = GetSerializer(type, extraTypes);
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
                Debug.WriteLine($"[Esk.GearForge.CSUtil.XmlUtil:DeserializeXmlStringAsync] Exception: {ex}");
            }

            return data;
        }
        /// <summary>
        /// 비동기로 String 형식의 Xml 데이터를 클래스 데이터화하는 함수
        /// </summary>
        /// <param name="xmlString">String 형식의 Xml 데이터</param>
        /// <param name="extraTypes">Xml Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// (result, data) <br/>
        /// result: true: 성공 / false: 실패 <br/>
        /// data: String 형식의 Xml 데이터를 클래스로 변경한 데이터 <br/>
        /// </returns>
        /// <remarks>[NEW][2026.02.13 - yc.jeon]</remarks>
        public static async Task<(bool, T)> DeserializeXmlStringAsync<T>(
            string xmlString,
            Type[] extraTypes = null)
        {
            if (await DeserializeXmlStringAsync(xmlString, typeof(T), extraTypes) is T orgData)
            {
                return (true, orgData);
            }
            return (false, default);
        }

        /// <summary>
        /// 암호화 된 Xml을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일명(전체 경로)</param>
        /// <param name="type">데이터 타입</param>
        /// <param name="extraTypes">Xml Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>불러온 Xml 데이터</returns>
        public static async Task<object> DeserializeDecryptXmlAsync(
            string filePath,
            Type type,
            Type[] extraTypes = null)
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
                Debug.WriteLine($"[Esk.GearForge.CSUtil.XmlUtil:DeserializeDecryptXmlAsync] Exception: {ex}");
                return null;
            }

            return await DeserializeXmlStringAsync(decrypt, type, extraTypes);
        }
        /// <summary>
        /// 암호화 된 Xml을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">Xml 파일명(전체 경로)</param>
        /// <param name="extraTypes">Xml Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>
        /// (result, data) <br/>
        /// result: true: 성공 / false: 실패 <br/>
        /// data: 불러온 Xml 데이터 <br/>
        /// </returns>
        /// <remarks>[NEW][2026.02.13 - yc.jeon]</remarks>
        public static async Task<(bool, T)> DeserializeDecryptXmlAsync<T>(
            string filePath,
            Type[] extraTypes = null)
        {
            if (await DeserializeDecryptXmlAsync(filePath, typeof(T), extraTypes) is T orgData)
            {
                return (true, orgData);
            }
            return (false, default);
        }

        /// <summary>
        /// Serializer를 가져오는 함수
        /// </summary>
        /// <param name="type"></param>
        /// <param name="extraTypes"></param>
        /// <returns>XmlSerializer</returns>
        /// <remarks>[NEW][2026.02.11 - yc.jeon]</remarks>
        private static XmlSerializer GetSerializer(Type type, Type[] extraTypes)
        {
            if (extraTypes == null ||
                extraTypes.Length == 0)
            {
                return new XmlSerializer(type);
            }

            StringBuilder stringBuilder = new StringBuilder(type.FullName);
            foreach (Type extraType in extraTypes)
            {
                stringBuilder.Append('|').Append(extraType.FullName);
            }
            string key = stringBuilder.ToString();
            return _serializerCache.GetOrAdd(key, _ => new XmlSerializer(type, extraTypes));
        }
    }
}
