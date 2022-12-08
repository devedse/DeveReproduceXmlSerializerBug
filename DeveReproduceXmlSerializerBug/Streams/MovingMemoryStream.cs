using System;
using System.Collections.Concurrent;
using System.IO;

namespace DeveReproduceXmlSerializerBug.Streams
{
    public class MovingMemoryStream : Stream
    {
        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => int.MaxValue;

        //This does not make sense as we have a different read and write position
        public override long Position { get => 0; set => throw new NotImplementedException(); }

        public int BufferSize { get; }

        public MovingMemoryStream(int bufferSize = 1024)
        {
            BufferSize = bufferSize;
        }

        private ConcurrentQueue<MovingMemoryStreamData> _datas = new ConcurrentQueue<MovingMemoryStreamData>();
        private MovingMemoryStreamData? _last;

        public override int Read(byte[] buffer, int offset, int count)
        {
            int bytesRead = 0;
            while (bytesRead < count)
            {
                //var val = datas.Skip(itemBeingRead).FirstOrDefault();
                var worked = _datas.TryPeek(out var data);
                if (!worked || data == null)
                {
                    return bytesRead;
                }

                var bytesReadHere = data.ReadData(buffer, offset + bytesRead, count - bytesRead);
                bytesRead += bytesReadHere;

                if (data.HasBeenCompletelyRead)
                {
                    if (!_datas.TryDequeue(out var _))
                    {
                        throw new InvalidOperationException("Unrecoverable error");
                    }
                }

                if (bytesReadHere == 0)
                {
                    return bytesRead;
                }
            }
            return bytesRead;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            int bytesWritten = 0;
            while (bytesWritten < count)
            {
                if (_last != null)
                {
                    var bytesWrittenHere = _last.WriteData(buffer, offset + bytesWritten, count - bytesWritten);
                    bytesWritten += bytesWrittenHere;
                }

                if (_last == null || _last.RemainingRoom == 0)
                {
                    var newData = new MovingMemoryStreamData(BufferSize);
                    _datas.Enqueue(newData);
                    _last = newData;
                }
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Flush()
        {

        }
    }
}
