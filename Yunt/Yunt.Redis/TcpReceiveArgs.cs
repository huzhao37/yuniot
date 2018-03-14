#region Code File Comment

// SOLUTION   ： 云统物联网V3
// PROJECT    ： Yunt.Redis
// FILENAME   ： TcpReceiveArgs.cs
// AUTHOR     ： soft-zh
// CREATE TIME： 2018-3-14 9:42
// COPYRIGHT  ： 版权所有 (C) 云统信息科技有限公司 http://www.unitoon.cn/ 2018~2020

#endregion

#region using namespace

using System;
//********************************************************
// 	Copyright © henryfan 2013		 
//	Email:		henryfan@msn.com	
//	HomePage:	http://www.ikende.com		
//	CreateTime:	2013/6/15 14:55:21
//********************************************************	 

#endregion

namespace Yunt.Redis
{
    public class TcpReceiveArgs : EventArgs
    {
        public TcpClient Client { get; internal set; }

        public byte[] Data { get; internal set; }

        public int Offset { get; internal set; }

        public int Count { get; internal set; }

        public byte[] ToArray()
        {
            var result = new byte[Count];
            Buffer.BlockCopy(Data, Offset, result, 0, Count);
            return result;
        }

        public void CopyTo(byte[] data, int start = 0)
        {
            Buffer.BlockCopy(Data, Offset, data, start, Count);
        }
    }
}