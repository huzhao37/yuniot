#region Code File Comment

// SOLUTION   ： 云统物联网V3
// PROJECT    ： Yunt.Redis
// FILENAME   ： Command.cs
// AUTHOR     ： soft-zh
// CREATE TIME： 2018-3-14 9:42
// COPYRIGHT  ： 版权所有 (C) 云统信息科技有限公司 http://www.unitoon.cn/ 2018~2020

#endregion

#region using namespace

using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using ProtoBuf;
using ProtoBuf.Meta;

#endregion

namespace Yunt.Redis
{
    public class Command : IDisposable
    {
        [ThreadStatic] private static StringEncoding _mEncodeHandler = null;
        private readonly Stream _stream;

        public int Count = 0;

        private byte[] _mData;

        public Command()
        {
            _mData = BufferPool.Single.Pop();
            _stream = new MemoryStream(_mData);
        }

        internal static StringEncoding EncodeHandler => _mEncodeHandler ?? (_mEncodeHandler = new StringEncoding(1024*64));

        public void Dispose()
        {
            lock (this)
            {
                if (_mData != null)
                    BufferPool.Single.Push(_mData);
                _mData = null;
            }
        }

        public void Add(string data)
        {
            var count = EncodeHandler.Encode(data);
            Add(EncodeHandler.Buffer, 0, count);
        }

        public void Add(byte[] data)
        {
            Add(data, 0, data.Length);
        }

        public void Add(byte[] data, int offset, int count)
        {
            var header = "$" + count;
            var headerdata = Encoding.ASCII.GetBytes(header);
            _stream.Write(headerdata, 0, headerdata.Length);
            _stream.Write(Utils.Eof, 0, 2);
            _stream.Write(data, offset, count);
            _stream.Write(Utils.Eof, 0, 2);
            Count++;
        }

        public int ToData(byte[] result)
        {
            try
            {
                string header = $"*{Count}\r\n";
                var headerdata = Encoding.ASCII.GetBytes(header);
                Buffer.BlockCopy(headerdata, 0, result, 0, headerdata.Length);
                var length = (int) _stream.Position;
                _stream.Position = 0;
                _stream.Read(result, headerdata.Length, length);
                return headerdata.Length + length;
            }
            catch (Exception e)
            {
                throw new Exception("buffer overflow buffer max size:" + BufferPool.DEFAULT_BUFFERLENGTH, e);
            }
        }

        public void AddJson(object obj)
        {
            Add(JsonConvert.SerializeObject(obj));
        }

        public void AddProtobuf(object obj)
        {
            var data = BufferPool.Single.Pop();
            try
            {
                var stream = new MemoryStream(data);
                RuntimeTypeModel.Default.Serialize(stream, obj);
                Add(data, 0, (int) stream.Position);
            }
            finally
            {
                BufferPool.Single.Push(data);
            }
        }
        
    }
}