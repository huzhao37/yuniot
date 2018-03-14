#region Code File Comment

// SOLUTION   ： 云统物联网V3
// PROJECT    ： Yunt.Redis
// FILENAME   ： LineBuffer.cs
// AUTHOR     ： soft-zh
// CREATE TIME： 2018-3-14 9:42
// COPYRIGHT  ： 版权所有 (C) 云统信息科技有限公司 http://www.unitoon.cn/ 2018~2020

#endregion

#region using namespace

using System.Collections.Concurrent;
using System.Text;

#endregion

namespace Yunt.Redis
{
    public class LineBuffer
    {
        private static readonly ConcurrentStack<LineBuffer> LineBUfferPool = new ConcurrentStack<LineBuffer>();
        private readonly byte[] mBuffer = new byte[256*30];//30

        private int mCount = 0;

        private int mIndex = 0;

        public static LineBuffer Pop()
        {
            LineBuffer buffer = null;
            if (!LineBUfferPool.TryPop(out buffer))
                return new LineBuffer();
            buffer.Reset();
            return buffer;
        }

        public static void Push(LineBuffer value)
        {
            LineBUfferPool.Push(value);
        }

        public bool Import(byte[] data, ref int offset, ref int count)
        {
            while (count > 0)
            {
                mBuffer[mIndex] = data[offset];
                mCount++;
                offset++;
                count--;
                if (mCount > 2)
                {
                    if (mBuffer[mIndex] == Utils.Eof[1] && mBuffer[mIndex - 1] == Utils.Eof[0])
                        return true;
                }
                mIndex++;
            }
            return false;
        }

        public string GetLineString()
        {
            return Encoding.UTF8.GetString(mBuffer, 0, mCount);
        }

        public void Reset()
        {
            mIndex = 0;
            mCount = 0;
        }
    }
}