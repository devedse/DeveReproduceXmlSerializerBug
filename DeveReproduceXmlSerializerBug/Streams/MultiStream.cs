using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DeveReproduceXmlSerializerBug.Streams
{
    public class MultiStream : Stream
    {
        private readonly List<Stream> _streams;

        public MultiStream(params Stream[] streams)
        {
            _streams = streams.ToList();
        }

        public override void Flush()
        {
            foreach (var stream in _streams)
            {
                stream.Flush();
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long x = 0;
            foreach (var stream in _streams)
            {
                x = stream.Seek(offset, origin);
            }
            return x;
        }

        public override void SetLength(long value)
        {
            foreach (var stream in _streams)
            {
                stream.SetLength(value);
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _streams.First(x => x.CanRead).Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            foreach (var stream in _streams)
            {
                stream.Write(buffer, offset, count);
            }
        }

        public override bool CanRead
        {
            get { return _streams.Any(x => x.CanRead); }
        }

        public override bool CanSeek { get { return _streams.TrueForAll(x => x.CanSeek); } }
        public override bool CanWrite { get { return _streams.TrueForAll(x => x.CanWrite); } }
        public override long Length => _streams.Min(x => x.Length);

        public override long Position
        {
            get => _streams.First().Position;
            set
            {
                _streams.ForEach(x => x.Position = value);
            }
        }
    }
}
