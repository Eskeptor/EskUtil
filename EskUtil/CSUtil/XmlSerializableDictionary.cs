// ======================================================================================================
// File Name        : XmlUtil.cs
// Project          : CSUtil
// Last Update      : 2026.04.21 - yc.jeon (Eskeptor)
// Origin Source    : https://learn.microsoft.com/en-us/archive/blogs/amolgote/xml-serializable-dictionary?WT.mc_id=DT-MVP-4038148
// ======================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Esk.GearForge.CSUtil
{
    [XmlRoot("SDictionary")]
    public class XmlSerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        private const string ITEM = "item";
        private const string KEY = "key";
        private const string VALUE = "value";

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlSerializableDictionary&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        public XmlSerializableDictionary()
            : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlSerializableDictionary&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="info">A <see cref="SerializationInfo"></see> object containing the information required to serialize the <see cref="Dictionary`2"></see >.</param>
        /// <param name="context"> A <see cref="StreamingContext"></see> structure containing the source and destination of the serialized stream associated with the <see cref="Dictionary`2"></see>.</param>
        protected XmlSerializableDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        /// <summary>
        /// This property is reserved, apply the <see cref="XmlSchemaProviderAttribute"></see> to the class instead.
        /// </summary>
        /// <returns>
        /// An <see cref="XmlSchema"></see> that describes the XML representation of the object that is produced by the <see cref="WriteXml(System.Xml.XmlWriter)"></see> method and consumed by the <see cref="ReadXml(System.Xml.XmlReader)"></see> method.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="XmlReader"></see> stream from which the object is deserialized.</param>
        public void ReadXml(XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
            {
                return;
            }

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement(ITEM);

                reader.ReadStartElement(KEY);
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement(VALUE);
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="XmlWriter"></see> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            foreach (TKey key in Keys)
            {
                writer.WriteStartElement(ITEM);

                writer.WriteStartElement(KEY);
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement(VALUE);
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }
    }
}
