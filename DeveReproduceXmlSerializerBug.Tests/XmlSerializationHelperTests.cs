using DeveReproduceXmlSerializerBug.Tests.TestModels;
using FluentAssertions;
using System.IO;
using System.Text;
using Xunit;

namespace DeveReproduceXmlSerializerBug.Tests
{
    public class XmlSerializationHelperTests
    {
        [Fact]
        public void TestSerializationWithStrings()
        {
            var toSerialize = new Nomination_Document();
            var expectedString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Nomination_Document xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"urn:test.eu:blah:testtest123:document:5:1\"><creationDateTime>0001-01-01T00:00:00</creationDateTime></Nomination_Document>";

            var serialized = XmlSerializationHelper.SerializeToString(toSerialize);
            serialized.Should().Be(expectedString);

            var nominationDocument = XmlSerializationHelper.Deserialize<Nomination_Document>(serialized);
            nominationDocument.Should().BeEquivalentTo(toSerialize);

            var result = Encoding.UTF8.GetBytes(nominationDocument.AsXmlDocument().InnerXml);
            result.Should().NotContain(Encoding.UTF8.GetPreamble());

            var stringResult = Encoding.UTF8.GetString(result);
            stringResult.Should().Be(expectedString);
        }

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

        [Fact]
        public void TestBomRemoval()
        {
            var utf8Bom = Encoding.UTF8.GetPreamble();
            var utf16Bom = Encoding.Unicode.GetPreamble();

            var utf8String = Encoding.UTF8.GetString(utf8Bom) + "Some text";
            var utf16String = Encoding.Unicode.GetString(utf16Bom) + "Some text";

            utf8String.Should().NotBe("Some text");
            utf16String.Should().NotBe("Some text");

            var newUtf8String = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(utf8String).RemoveUtf8BomCharacters());
            var newUtf16String = Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(utf16String).RemoveUtf8BomCharacters());

            newUtf8String.Should().Be("Some text");
            utf16String.Should().Be(newUtf16String, "because this is UTF16 which doesn't contain any UTF8 BOM characters");
        }

        [Fact]
        public void TestBomRemovalWithNullValue()
        {
            byte[] byteArray = null;
            byteArray.RemoveUtf8BomCharacters().Should().BeNull();
        }

        [Fact]
        public void TestBomsAsString()
        {
            var utf8Bom = Encoding.UTF8.GetPreamble();
            var utf16Bom = Encoding.Unicode.GetPreamble();

            var utf8BomAsUtf8String = Encoding.UTF8.GetString(utf8Bom);
            var utf8BomAsUtf16String = Encoding.Unicode.GetString(utf8Bom);

            var utf16BomAsUtf8String = Encoding.UTF8.GetString(utf16Bom);
            var utf16BomAsUtf16String = Encoding.Unicode.GetString(utf16Bom);

            utf8BomAsUtf8String.Should().NotBe(string.Empty);
            utf8BomAsUtf8String.Should().HaveLength(1); //1 BOM character
            utf8BomAsUtf8String.Should().Be(utf16BomAsUtf16String);

            utf16BomAsUtf16String.Should().NotBe(string.Empty);
            utf16BomAsUtf16String.Should().HaveLength(1); //1 BOM character
            utf16BomAsUtf16String.Should().Be(utf8BomAsUtf8String);

            utf16BomAsUtf8String.Should().NotBe(string.Empty);
            utf16BomAsUtf8String.Should().HaveLength(2); //Garbled BOM characters
            Encoding.UTF8.GetBytes(utf16BomAsUtf8String).Should().BeEquivalentTo(new byte[] { 0xEF, 0xBF, 0xBD, 0xEF, 0xBF, 0xBD });

            utf8BomAsUtf16String.Should().NotBe(string.Empty);
            utf8BomAsUtf16String.Should().HaveLength(2); //Garbled BOM characters
            Encoding.Unicode.GetBytes(utf8BomAsUtf16String).Should().BeEquivalentTo(new byte[] { 0xEF, 0xBB, 0xFD, 0xFF });
        }

        [Fact]
        public void TestStrings()
        {
            var utf8BomAsUtf8String = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            var text = "My awesome text";

            var utf8Bytes = Encoding.UTF8.GetBytes(text);
            var utf16Bytes = Encoding.Unicode.GetBytes(text);

            var utf8BytesWithUtf8String = Encoding.UTF8.GetBytes(utf8BomAsUtf8String + text);
            var utf16BytesWithUtf8String = Encoding.Unicode.GetBytes(utf8BomAsUtf8String + text);

            utf8Bytes.Should().NotBeEquivalentTo(utf16Bytes); //Encoding differs
            utf8BytesWithUtf8String.Should().NotBeEquivalentTo(utf16BytesWithUtf8String); //Encoding still differs

            var utf8String = Encoding.UTF8.GetString(utf8BytesWithUtf8String);
            var utf16String = Encoding.Unicode.GetString(utf16BytesWithUtf8String);

            utf8String.Should().Be(utf8BomAsUtf8String + text);
            utf16String.Should().Be(utf8BomAsUtf8String + text);
        }

        [Fact]
        public void TestBoms()
        {
            var ourUtf8Bom = new byte[] { 0xEF, 0xBB, 0xBF };
            var defaultUtf8Bom = Encoding.UTF8.GetPreamble(); //Length of 3: {0xEF, 0xBB, 0xBF}
            var defaultUtf16Bom = Encoding.Unicode.GetPreamble(); //Lenght of 2: {0xFF, 0xFE}

            ourUtf8Bom.Should().BeEquivalentTo(defaultUtf8Bom);

            defaultUtf8Bom.Should().NotBeEquivalentTo(defaultUtf16Bom);
        }
    }
}
