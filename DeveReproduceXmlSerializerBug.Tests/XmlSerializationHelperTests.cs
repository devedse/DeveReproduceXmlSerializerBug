using DeveReproduceXmlSerializerBug.Tests.TestModels;
using FluentAssertions;
using System.IO;
using Xunit;

namespace DeveReproduceXmlSerializerBug.Tests
{
    public class XmlSerializationHelperTests
    {

        [Fact]
        public void TestSerializationWithStreams()
        {
            var toSerialize = new Nomination_Document();
            var expectedString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Nomination_Document xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"urn:test.eu:blah:testtest123:document:5:1\"><creationDateTime>0001-01-01T00:00:00</creationDateTime></Nomination_Document>";

            var serializedStream = XmlSerializationHelper.SerializeToStream(toSerialize);
            using var streamReader = new StreamReader(serializedStream);
            var serialized = streamReader.ReadToEnd();
            serialized.Should().Be(expectedString);

            var nominationDocument = XmlSerializationHelper.Deserialize<Nomination_Document>(serialized);
            nominationDocument.Should().BeEquivalentTo(toSerialize);

            var xmlDocument = toSerialize.AsXmlDocument();

            var serializedStreamFromXmlDocument = XmlSerializationHelper.SerializeToStream(xmlDocument);
            using var xmlDocStreamReader = new StreamReader(serializedStreamFromXmlDocument);
            var serializedXmlDoc = xmlDocStreamReader.ReadToEnd();
            serializedXmlDoc.Should().Be(expectedString);
        }
    }
}
