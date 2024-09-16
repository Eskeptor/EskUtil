// ======================================================================================================
// File Name        : XMLUtil.cs
// Project          : CSUtil
// Last Update      : 2024.09.16 - yc.jeon
// ======================================================================================================

using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CSUtil
{
    /// <summary>
    /// XML 관련 유틸
    /// </summary>
    public static class XMLUtil
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
        public static bool SerializeXML(string dirPath, string fileName, Type type, object data, Type[]? extraTypes = null)
        {
            if (string.IsNullOrEmpty(dirPath))
            {
                return false;
            }

            if (dirPath.Length == 0)
            {
                dirPath = $".{Path.DirectorySeparatorChar}";
            }
            if (dirPath.LastIndexOf(Path.DirectorySeparatorChar) != dirPath.Length - 1)
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

                using (StreamWriter stream = new StreamWriter(Path.Combine(dirPath, fileName)))
                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    xmlSerializer.Serialize(stream, data, ns);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
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
        public static bool SerializeXML(string filePath, Type type, object data, Type[]? extraTypes = null)
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
                    xmlSerializer.Serialize(stream, data, ns);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                result = false;
            }

            return result;
        }

        /// <summary>
        /// XML을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">XML파일명(전체 경로)</param>
        /// <param name="type">데이터 타입</param>
        /// <param name="extraTypes">XML Deserialize에서 사용할 추가적 Types (기본값: null)</param>
        /// <returns>불러온 XML 데이터</returns>
        public static object? DeserializeXML(string filePath, Type type, Type[]? extraTypes = null)
        {
            FileInfo info = new FileInfo(filePath);
            object? data = null;

            if (!info.Exists)
            {
                return null;
            }

            try
            {
                XmlSerializer xmlSerializer = extraTypes != null ? new XmlSerializer(type, extraTypes) : new XmlSerializer(type);
                XmlReaderSettings settings = new XmlReaderSettings()
                {
                    DtdProcessing = DtdProcessing.Prohibit,
                };

                using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (XmlReader xmlReader = XmlReader.Create(stream, settings))
                {
                    data = xmlSerializer.Deserialize(xmlReader);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return data;
        }
    }
}
