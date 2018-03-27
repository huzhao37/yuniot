#region Code File Comment

// SOLUTION   ： 云统物联网V3
// PROJECT    ： Yunt.Redis
// FILENAME   ： BufferPool.cs
// AUTHOR     ： soft-zh
// CREATE TIME： 2018-3-14 9:42
// COPYRIGHT  ： 版权所有 (C) 云统信息科技有限公司 http://www.unitoon.cn/ 2018~2020

#endregion

#region using namespace

using System;
using System.Collections.Concurrent;
using Yunt.Common;

#endregion

namespace Yunt.Redis
{
    public class BufferPool
    {
        public static int DEFAULT_BUFFERLENGTH=1024*200;//15//接收缓存区默认大小为1024*1024*2（目前项目已经超过默认大小）

        public static BufferPool mSingle;

        private readonly int mBufferLength = 1024;

        private readonly ConcurrentStack<byte[]> mPools = new ConcurrentStack<byte[]>();

        public BufferPool(int count, int bufferlength)
        {
            try
            {
                for (var i = 0; i < count; i++)
                {
                    mPools.Push(new byte[bufferlength]);//30?26
                }
                mBufferLength = bufferlength;
            }
            catch (Exception ex)
            {
                Logger.Warn("堆栈内存分配不足：{0}",ex.Message);
            }

        }

        public static  BufferPool Single
        {
            get
            {
                if (mSingle == null)
                {
                    mSingle = new BufferPool(1, DEFAULT_BUFFERLENGTH);//20  31   ------->(堆栈数*堆栈大小=15*15M)
                }
                return mSingle;
            }
        }

        public byte[] Pop()
        {
            byte[] result = null;
            if (mPools.TryPop(out result))
            {
                return result;
            }
            else
            {
                return new byte[mBufferLength];
            }
        }

        public void Push(byte[] data)
        {
            mPools.Push(data);
        }
    }
}