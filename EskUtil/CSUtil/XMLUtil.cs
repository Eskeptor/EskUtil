// ======================================================================================================
// File Name        : XMLUtil.cs
// Project          : CSUtil
// Last Update      : 2024.01.27 - yc.jeon
// ======================================================================================================

using System.Diagnostics;
using System.Xml.Serialization;

namespace CSUtil
{
    /// <summary>
    /// XML 관련 유틸리티
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
        public static void SerializeXML(string dirPath, string fileName, Type type, object data)
        {
            if (dirPath.Length == 0)
            {
                dirPath = $".{Path.DirectorySeparatorChar}";
            }

            if (dirPath.LastIndexOf(Path.DirectorySeparatorChar) != dirPath.Length - 1)
            {
                dirPath += Path.DirectorySeparatorChar;
            }

            DirectoryInfo info = new DirectoryInfo(dirPath);

            if (!info.Exists)
            {
                info.Create();
            }

            using (Stream stream = new FileStream(Path.Combine(dirPath, fileName), FileMode.Create, FileAccess.Write))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                xmlSerializer.Serialize(stream, data, ns);
            }
        }

        /// <summary>
        /// 데이터를 XML로 변환하여 저장하는 함수
        /// </summary>
        /// <param name="filePath">XML 파일 경로(전체 Full 경로)</param>
        /// <param name="type">원본 object 타입</param>
        /// <param name="data">XML화 할 원본 데이터</param>
        public static void SerializeXML(string filePath, Type type, object data)
        {
            using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                xmlSerializer.Serialize(stream, data, ns);
            }
        }

        /// <summary>
        /// XML을 불러와서 데이터화하는 함수
        /// </summary>
        /// <param name="filePath">XML파일명(전체 경로)</param>
        /// <param name="type">데이터 타입</param>
        /// <returns></returns>
        public static object? DeserializeXML(string filePath, Type type)
        {
            FileInfo info = new FileInfo(filePath);
            object? data = null;

            if (!info.Exists)
            {
                return null;
            }

            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(type);
                    data = xmlSerializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                //LogManager.Instance.WriteLog("Exception", $"DeserializeXML Failed : {ex}");
            }

            return data;
        }
    }
}
