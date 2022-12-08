using System;
using System.IO;
using System.Linq;

namespace DeveReproduceXmlSerializerBug.Streams
{
    public class MovingMemoryStreamData
    {
        public byte[] Buffer { get; }
        public int Length { get; set; }
        public int Position { get; set; }

        public int RemainingRoom => Buffer.Length - Length;
        public bool HasBeenCompletelyRead => Position == Buffer.Length;

        public MovingMemoryStreamData(int bufferSize)
        {
            Buffer = new byte[bufferSize];
        }

        public int WriteData(byte[] source, int offset, int count)
        {
            var bytesToWrite = Math.Min(RemainingRoom, count);
            Array.Copy(source, offset, Buffer, Length, bytesToWrite);
            Length += bytesToWrite;
            return bytesToWrite;
        }

        public int ReadData(byte[] dest, int offset, int count)
        {
            var bytesToRead = Math.Min(Length - Position, count);
            Array.Copy(Buffer, Position, dest, offset, bytesToRead);
            Position += bytesToRead;
            return bytesToRead;
        }

        public string ContentAsString
        {
            get
            {
                using (var reader = new StreamReader(new MemoryStream(Buffer.Skip(Position).Take(Length).ToArray())))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
