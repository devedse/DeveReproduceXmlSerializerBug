using System;
using System.Xml;
using System.Xml.Schema;

namespace DeveReproduceXmlSerializerBug
{
    public static class XmlDocumentExtensions
    {
        public static XmlDocument AsXmlDocument<T>(this T input, XmlSchemaSet schemas = null)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.GetType() == typeof(XmlDocument))
            {
                throw new ArgumentException("Input is already an XmlDocument");
            }

            using var memoryStream = XmlSerializationHelper.SerializeToStream(input);

            var settings = new XmlReaderSettings
            {
                IgnoreWhitespace = true,
            };
            if (schemas != null)
            {
                settings.ValidationEventHandler += new ValidationEventHandler((_, e) => throw e.Exception);
                settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                settings.Schemas = schemas;
                settings.ValidationType = System.Xml.ValidationType.Schema;
            }

            using var xmlReader = XmlReader.Create(memoryStream, settings);
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlReader);

            return xmlDocument;
        }
    }
}
