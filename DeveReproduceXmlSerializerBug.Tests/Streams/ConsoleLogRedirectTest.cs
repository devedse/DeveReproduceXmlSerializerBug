using DeveReproduceXmlSerializerBug.Streams;
using System;
using System.IO;
using Xunit;

namespace DeveReproduceXmlSerializerBug.Tests.Streams
{
    public class ConsoleLogRedirectTest
    {
        [Fact]
        public void RedirectsConsoleOutput()
        {
            var originalOutWriter = Console.Out;
            try
            {
                var originalOut = Console.OpenStandardOutput();
                var movingMemoryStream = new MovingMemoryStream();

                var multiOut = new MultiStream(originalOut, movingMemoryStream);
                var writer = new StreamWriter(multiOut) { AutoFlush = true };

                Console.SetOut(writer);

                Console.WriteLine("Test");

                var reader = new StreamReader(movingMemoryStream);
                var line1 = reader.ReadLine();

                Assert.Equal("Test", line1);
            }
            finally
            {
                Console.SetOut(originalOutWriter);
            }
        }
    }
}
