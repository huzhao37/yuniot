using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Yunt.Common;

namespace Yunt.MQ.RabbitMqExtention
{
    public static class BufferConverter
    {
        /// <summary>
        /// 获取locker buffer;
        /// </summary>
        /// <param name="embeddedDeviceId">嵌入式设备ID</param>
        /// <param name="dataformId">数据表单ID</param>
        /// <param name="controlDeviceId">控制设备ID</param>
        /// <param name="dt">锁死时间</param>
        /// <param name="OnForever">允许永远允许</param>
        /// <returns></returns>
        public static byte[] GetLockerbytes(string embeddedDeviceId, int dataformId, string controlDeviceId, DateTime dt, bool OnForever)
        {
            byte[] buffer = new byte[17];
            byte[] temp;

            //设备ID(6个字节)
            //string idstr = "010203040506";
            temp = Extention.GetRequestIDBuffer(embeddedDeviceId);

            //表单结构ID C001 (2个字节)
            //byte[] formbuffer = new byte[] { 01, 0xC0 };
            byte[] formbuffer = Extention.IntToBytes2(dataformId);
            temp = Extention.CombomBinaryArray(temp, formbuffer);

            //表单个数 (1个字节)
            byte[] countbuffer = new byte[] { 01 };
            temp = Extention.CombomBinaryArray(temp, countbuffer);

            //电机ID (4个字节)
            byte[] motorbuffer = Extention.strToToHexByte2(controlDeviceId);
            temp = Extention.CombomBinaryArray(temp, motorbuffer);

            //时间 (4个字节)
            byte[] timebuffer = new byte[4];
            if (OnForever)
                timebuffer = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
            else
            {
                int unix = Unix.ConvertDateTimeInt(dt);
                timebuffer = Extention.TimeTobyte(unix);
            }
            temp = Extention.CombomBinaryArray(temp, timebuffer);

            buffer = temp;
            temp = formbuffer = countbuffer = motorbuffer = timebuffer = null;

            return buffer;
        }
        /// <summary>
        /// 获取开关控制 buffer
        /// </summary>
        /// <param name="requestId">请求ID</param>
        /// <param name="motorId">电机ID</param>
        /// <param name="cmdValue">命令数值</param>
        /// <param name="address">命令地址</param>
        /// <returns></returns>
        public static byte[] GetSwitchbytes(int requestId, int motorId, int cmdValue, int address)
        {
            byte[] buffer = new byte[17];
            byte[] temp;

            //设备ID(6个字节)
            string idstr = requestId.ToString();
            temp = new byte[6] { 01, 02, 03, 04, 05, 06 };
            //temp = Extention.GetRequestIDBuffer(idstr);

            //表单结构ID C002 (2个字节)
            byte[] formbuffer = new byte[] { 02, 0xC0 };
            temp = Extention.CombomBinaryArray(temp, formbuffer);

            //表单个数 (1个字节)
            byte[] countbuffer = new byte[] { 01 };
            temp = Extention.CombomBinaryArray(temp, countbuffer);

            //电机ID (4个字节)
            //测试 motorId=1
            motorId = 1;
            byte[] motorbuffer = BitConverter.GetBytes(motorId);
            motorbuffer[2] = 0xF0;
            motorbuffer[3] = 0x90;
            temp = Extention.CombomBinaryArray(temp, motorbuffer);

            //控制数据地址
            byte[] addtemp = BitConverter.GetBytes(address);
            byte[] addbuffer = new byte[2] { addtemp[0], addtemp[1] };
            temp = Extention.CombomBinaryArray(temp, addbuffer);
            
            //控制数据
            byte[] valuetemp = BitConverter.GetBytes(cmdValue);
            byte[] valuebuffer = new byte[2] { valuetemp[0], valuetemp[1] };
            temp = Extention.CombomBinaryArray(temp, valuebuffer);

            buffer = temp;
            temp = formbuffer = countbuffer = motorbuffer = addtemp = addbuffer = valuebuffer = valuetemp = null;

            return buffer;
        }
    }
}