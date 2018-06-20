
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.Common
{
    public static class Extention
    {
        #region static byte method
        /// <summary>
        /// C# byte数组合并(（二进制数组合并）
        /// </summary>
        /// <param name="srcArray1">待合并数组1</param>
        /// <param name="srcArray2">待合并数组2</param>
        /// <returns>合并后的数组</returns>
        public static byte[] CombomBinaryArray(byte[] srcArray1, byte[] srcArray2)
        {
            //根据要合并的两个数组元素总数新建一个数组
            byte[] newArray = new byte[srcArray1.Length + srcArray2.Length];
            //把第一个数组复制到新建数组
            Array.Copy(srcArray1, 0, newArray, 0, srcArray1.Length);
            //把第二个数组复制到新建数组
            Array.Copy(srcArray2, 0, newArray, srcArray1.Length, srcArray2.Length);

            return newArray;
        }
        /// <summary>
        /// 获取请求id buffer
        /// </summary>
        /// 6个字节，不够高位补充
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] GetRequestIDBuffer(string str)
        {
            byte[] idbuff1 = StringToByteArray(str);
            int length = idbuff1.Length;
            if (length > 6)
                return null;
            byte[] idbuffer2 = new byte[6 - length];
            byte[] idbuffer = CombomBinaryArray(idbuff1, idbuffer2);
            return idbuffer;
        }
        /// <summary>
        /// 截取指定位置长度的byte数组;
        /// </summary>
        /// <param name="original">原始byte数组</param>
        /// <param name="sourceIndex">原值byte数组开始位置</param>
        /// <param name="destIndex">目标数组开始位置，一般为0</param>
        /// <param name="length">截取byte数组长度</param>
        /// <returns></returns>
        public static byte[] ByteCapture(byte[] original, ref int sourceIndex, int destIndex, int length)
        {
            byte[] bt = new byte[length];
            Array.Copy(original, sourceIndex, bt, destIndex, length);
            sourceIndex += length;

            return bt;
        }
        /// <summary>
        /// 将byte数组b转为一个整数,字节数组的低位是整型的低字节位,直接转;
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int byteToInt(byte[] b)
        {
            int iOutcome = 0;
            byte bLoop;
            int length = b.Length;
            for (int i = 0; i < length; i++)
            {
                bLoop = b[i];
                iOutcome += (bLoop & 0xFF) << (8 * i);
            }
            return iOutcome;
        }
        /// <summary>
        /// int 转 byte[2]
        /// </summary>
        /// <param name="value">int值</param>
        /// <returns></returns>
        public static byte[] IntToBytes2(int value)
        {
            byte[] src = new byte[2];
            src[1] = (byte)((value >> 8) & 0xFF);
            src[0] = (byte)(value & 0xFF);
            return src;
        }
        /// <summary>
        /// int 转 byte[1]
        /// </summary>
        /// <param name="value">int值</param>
        /// <returns></returns>
        public static byte[] IntToBytes(int value)
        {
            byte[] src = new byte[1];
           // src[1] = (byte)((value >> 8) & 0xFF);
            src[0] = (byte)(value & 0xFF);
            return src;
        }
        /// <summary>
        /// int 转 byte[4]
        /// </summary>
        /// <param name="value">int值</param>
        /// <returns></returns>
        public static byte[] IntToBytes4(int value)
        {
            byte[] src = new byte[4];
            src[3] = (byte)((value >> 24) & 0xFF);
            src[2] = (byte)((value >> 16) & 0xFF);
            src[1] = (byte)((value >> 8) & 0xFF);
            src[0] = (byte)(value & 0xFF);
            return src;   
        }
        public static uint byteToUInt(byte[] b)
        {
            uint iOutcome = 0;
            byte bLoop;
            int length = b.Length;
            for (int i = 0; i < length; i++)
            {
                bLoop = b[i];
                iOutcome += (uint)(bLoop & 0xFF) << (8 * i);
            }
            return iOutcome;
        }
        /// <summary>
        /// 4个字节表示时间，第一个字节的高两位位标志位，不算做数据;
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int byteToTime(byte[] b)
        {
            int t = 0;
            t = (b[0] & 0x3F) << 24;
            t += b[1] << 16;
            t += b[2] << 8;
            t += b[3];
            t *= 60;

            return t;
        }
        /// <summary>
        /// 时间unix转成byte[4]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] TimeTobyte(int value)
        {
            byte[] buffer = new byte[4];

            value /= 60;
            buffer[0] = BitConverter.GetBytes((value >> 24) | 0X80)[0];
            buffer[1] = BitConverter.GetBytes(value >> 16)[0];
            buffer[2] = BitConverter.GetBytes(value >> 8)[0];
            buffer[3] = BitConverter.GetBytes(value)[0];

            return buffer;
        }
        /// <summary>
        /// 时间unix转成byte[4]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] TimeTobyte4(this DateTime value)
        {
            var year =Convert.ToInt32(value.Year.ToString().Substring(2));
            var month =value.Month;
            var day = value.Day;
            var hour = value.Hour;

            var buffer = IntToBytes(year);
            buffer = CombomBinaryArray(buffer, IntToBytes(month));
            buffer = CombomBinaryArray(buffer, IntToBytes(day));
            buffer = CombomBinaryArray(buffer, IntToBytes(hour));
            return buffer;
        }
        /// <summary>
        /// 12位拼凑成3个字节，12*2 = 3*8;
        /// 返回两个值;
        /// </summary>
        /// <param name="b"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static int[] byteToInt12(byte[] b)
        {
            int[] values = new int[2];

            int total = byteToInt(b);
            values[0] = total & 0XFFF;
            values[1] = total >> 12;

            return values;
        }
        /// <summary>
        /// 温度 有符号数转化;
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int TempTemperatureTranster(int source)
        {
            int des = source;
            uint temp = (uint)source;
            if ((temp & 0x800) != 0)
                temp = (temp | 0xfffff000);
            des = (int)temp;
            return des;
        }
        /// <summary>
        /// 8个1位拼凑成1个字节， 1*8 = 1*8；
        /// 返回8个值;
        /// ex: 0x55;
        /// </summary>
        /// <param name="b"></param>
        public static int[] byteToInt1(byte b)
        {
            int[] values = new int[8];

            string str = Convert.ToString(b, 2).PadLeft(8, '0');

            values[0] = int.Parse(str.Substring(7, 1));
            values[1] = int.Parse(str.Substring(6, 1));
            values[2] = int.Parse(str.Substring(5, 1));
            values[3] = int.Parse(str.Substring(4, 1));
            values[4] = int.Parse(str.Substring(3, 1));
            values[5] = int.Parse(str.Substring(2, 1));
            values[6] = int.Parse(str.Substring(1, 1));
            values[7] = int.Parse(str.Substring(0, 1));

            return values;
        }
        /// <summary>
        /// byte转成HEX 字符串;
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static string ByteToHexString(byte data)
        {
            StringBuilder sb = new StringBuilder(1 * 3);
            sb.Append(Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }
        /// <summary>
        /// byte[]数组转成HEX 字符串;
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));

            return sb.ToString().ToUpper();
        }
        /// <summary>
        /// byte[]数组转成HEX 字符串;
        /// 高地位互换;
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString2(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(Convert.ToString(data[data.Length - i - 1], 16).PadLeft(2, '0'));
            }

            return sb.ToString().ToUpper();
        }
        /// <summary>
        /// HEX字符串转byte[]
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        /// <summary> 
        /// 字符串转16进制字节数组; 
        /// </summary> 
        /// <param name="hexString"></param> 
        /// <returns></returns> 
        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        /// <summary>
        /// 高低位互换;字符串转16进制;
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] strToToHexByte2(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[returnBytes.Length - i - 1] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 累加和校验
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public static string SumCheck(byte[] bs)
        {
            //var num = 0;
            ////所有字节累加
            //for (var i = 0; i < bs.Length; i++)
            //{
            //    num = (num + i) % 0xFFFF;
            //}
            //var ret = (byte)(num & 0xff);//只要最后一个字节
            //return ret;
            int result = 0;
            foreach (byte b in bs)
            {
                result += b;
            }
            return Convert.ToString(result % 256,16);
        }
        #endregion


        #region ASC码转换


        /// <summary>
        /// 十六进制字符串转ASC码
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static string HexStringToAsc(string hex)
        {
            var iValue = Convert.ToInt32(hex, 16); // 16进制->10进制
            var bs = BitConverter.GetBytes(iValue); //int->byte[]
            return Encoding.ASCII.GetString(bs);   //byte[]-> ASCII
        }


        /// <summary>
        /// 将一条十六进制bytes数组转换为ASCII
        /// </summary>
        /// <param name="bt">一条十六进制数组</param>
        /// <returns>返回一条ASCII码</returns>
        public static string BytesToASCII(byte[] bt)
        {
            var lin = "";
            for (var i = 0; i < bt.Length; i++)
            {
                lin = lin + bt[i] + " ";
            }


            var ss = lin.Trim().Split(new char[] { ' ' });
            var c = new char[ss.Length];
            int a;
            for (var i = 0; i < c.Length; i++)
            {
                a = Convert.ToInt32(ss[i]);
                c[i] = Convert.ToChar(a);
            }

            var b = new string(c);
            return b;
        }

        /// <summary>
        /// 根据小数点位置划分
        /// </summary>
        /// <param name="position">小数点位置</param>
        /// <returns></returns>
        public static Tuple<byte[], byte[]> BytesSplit(int position, byte[] sourceBytes, int length)
        {
            var bufferIndex = 0;
            var desBytes1 = ByteCapture(sourceBytes, ref bufferIndex, 0, length - position);
            var desBytes2 = ByteCapture(sourceBytes, ref bufferIndex, 0, position);
            return new Tuple<byte[], byte[]>(desBytes1, desBytes2);
        }

        #endregion


        #region 奇偶检验

        /// <summary>
        /// 对byte逐位异或进行奇校验并返回校验结果
        /// </summary>
        /// <param name="AByte">要取bit值的byte，一个byte有8个bit</param>
        /// <returns>如果byte里有奇数个1则返回true，如果有偶数个1则返回false</returns>
        public static bool OddParityCheck(byte AByte)
        {
            return getBit(AByte, 0) ^ getBit(AByte, 1) ^ getBit(AByte, 2) ^ getBit(AByte, 3)
                   ^ getBit(AByte, 4) ^ getBit(AByte, 5) ^ getBit(AByte, 6) ^ getBit(AByte, 7);
        }

        /// <summary>
        /// 对byte逐位异或进行偶校验并返回校验结果
        /// </summary>
        /// <param name="AByte">要取bit值的byte，一个byte有8个bit</param>
        /// <returns>如果byte里有偶数个1则返回true，如果有奇数个1则返回false</returns>
        public static bool EvenParityCheck(byte AByte)
        {
            return !getBit(AByte, 0) ^ getBit(AByte, 1) ^ getBit(AByte, 2) ^ getBit(AByte, 3)
                   ^ getBit(AByte, 4) ^ getBit(AByte, 5) ^ getBit(AByte, 6) ^ getBit(AByte, 7);
        }

        /// <summary>
        /// 取一个byte中的第几个bit的值，
        /// 实在查不到c#有什么方法，才动手写了这个函数 -_-#
        /// </summary>
        /// <param name="AByte">要取bit值的byte，一个byte有8个bit</param>
        /// <param name="iIndex">在byte中要取bit的位置，一个byte从左到右的位置分别是0,1,2,3,4,5,6,7</param>
        /// <returns>返回bit的值，不知道C#中bit用什么表示，似乎bool就是bit，就用它来代替bit吧</returns>
        public static bool getBit(byte AByte, int iIndex)
        {
            //MessageBox.Show((AByte >> (7 - iIndex) & 1).ToString());
            //将要取的bit右移到第一位，再与1与运算将其它位置0
            return (AByte >> (7 - iIndex) & 1) != 0 ? true : false;
        }

        #endregion
    }
}
