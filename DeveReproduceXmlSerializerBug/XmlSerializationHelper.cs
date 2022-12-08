using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DeveReproduceXmlSerializerBug
{
    public static class XmlSerializationHelper
    {
        public static byte[] RemoveUtf8BomCharacters(this byte[] byteArray)
        {
            if (byteArray == null)
            {
                return byteArray;
            }

            var bom = Encoding.UTF8.GetPreamble();

            if (byteArray[0] == bom[0] && byteArray[1] == bom[1] && byteArray[2] == bom[2])
            {
                byteArray = byteArray.Skip(3).ToArray();
            }

            return byteArray;
        }

        public static T Deserialize<T>(string serialized)
        {
            using StringReader stringReader = new(serialized);
            XmlSerializer xmlSerializer = new(typeof(T));

            return (T)xmlSerializer.Deserialize(stringReader);
        }

        public static string SerializeToString(object toSerialize)
        {
            Utf8StringWriter stringWriter = new();

            using (stringWriter)
            {
                XmlSerializer serializer = new(toSerialize.GetType());
                serializer.Serialize(stringWriter, toSerialize);

                return stringWriter.ToString();
            }
        }

        public static Stream SerializeToStream(object toSerialize)
        {
            MemoryStream memoryStream = new();

            XmlSerializer serializer = new(toSerialize.GetType());
            serializer.Serialize(memoryStream, toSerialize);

            memoryStream.Flush();
            memoryStream.Position = 0;

            return memoryStream;
        }

        public static Stream SerializeToStream(XmlDocument toSerialize)
        {
            MemoryStream memoryStream = new();

            memoryStream.Write(Encoding.UTF8.GetBytes(toSerialize.InnerXml));

            memoryStream.Flush();
            memoryStream.Position = 0;

            return memoryStream;
        }
    }

    /// <summary>
    /// The default StringWriter always writes in Unicode/UTF16, which is not what we want in SAGE. Our partners and customers expect UTF8 encoding (without BOM).
    /// </summary>
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => new UTF8Encoding(false);
    }
}
