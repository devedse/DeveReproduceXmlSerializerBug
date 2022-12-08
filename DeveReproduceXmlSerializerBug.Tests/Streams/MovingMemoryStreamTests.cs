using DeveReproduceXmlSerializerBug.Streams;
using System.IO;
using Xunit;

namespace DeveReproduceXmlSerializerBug.Tests.Streams
{
    public class MovingMemoryStreamTests
    {
        [Fact]
        public void WorksWithTextBiggerThenBuffer()
        {
            MovingMemoryHelper(1024, 1024, 4, "Hello", "Bye", "A");
        }

        [Fact]
        public void WorksWithTextSmallerThenBuffer()
        {
            MovingMemoryHelper(1024, 1024, 8, "Hello", "Bye", "A");
        }

        [Fact]
        public void WorksWithSmallStreamWriterAndReaderBufferSizes()
        {
            MovingMemoryHelper(1, 1, 4, "Hello", "Bye", "Abbbrrrr");
        }

        [Fact]
        public void WorksWithSmallStreamWriterAndReaderBufferSizes2()
        {
            MovingMemoryHelper(10, 10, 80, "Hello", "Bye", "Abbbrrrr");
        }

        [Fact]
        public void WorksWith1Sizes()
        {
            MovingMemoryHelper(1, 1, 1, "Hello", "Bye", "Abbbrrrr");
        }

        [Fact]
        public void WorksWith8Sizes()
        {
            MovingMemoryHelper(8, 8, 8, "Hello", "Bye", "Abbbrrrr");
        }

        [Fact]
        public void WorksWith8SizesAnd8LengthTexts()
        {
            MovingMemoryHelper(8, 8, 8, "HelloAAA", "HelloBBB", "Abbbrrrr");
        }

        private void MovingMemoryHelper(int streamWriterBufferSize, int streamReaderBufferSize, int movingMemoryStreamBufferSize, params string[] stringsToTest)
        {
            //Arrange
            var mov = new MovingMemoryStream(movingMemoryStreamBufferSize);
            var writer = new StreamWriter(mov, bufferSize: streamWriterBufferSize) { AutoFlush = true };
            var reader = new StreamReader(mov, bufferSize: streamReaderBufferSize);

            //Act
            foreach (var s in stringsToTest)
            {
                writer.WriteLine(s);
            }

            //Assert
            foreach (var s in stringsToTest)
            {
                var line = reader.ReadLine();
                Assert.Equal(s, line);
            }

            Assert.Null(reader.ReadLine());
        }

        [Fact]
        public void CustomTest1()
        {
            for (int x = 1; x < 10; x++)
            {
                for (int y = 1; y < 10; y++)
                {
                    for (int z = 1; z < 10; z++)
                    {
                        CustomTestSet(x, y, z);
                    }
                }
            }
        }

        private void CustomTestSet(int streamWriterBufferSize, int streamReaderBufferSize, int movingMemoryStreamBufferSize)
        {
            //Arrange
            var mov = new MovingMemoryStream(movingMemoryStreamBufferSize);
            var writer = new StreamWriter(mov, bufferSize: streamWriterBufferSize) { AutoFlush = true };
            var reader = new StreamReader(mov, bufferSize: streamReaderBufferSize);

            //Act and Assert
            writer.Write("H");
            writer.WriteLine("ello");
            writer.WriteLine("Bye");

            Assert.Equal("Hello", reader.ReadLine());
            Assert.Equal("Bye", reader.ReadLine());
            Assert.Null(reader.ReadLine());

            writer.Write("Mic");
            writer.Write("roso");
            writer.Write("ft");
            writer.WriteLine(" Windows");

            Assert.Equal("Microsoft Windows", reader.ReadLine());
        }
    }
}
