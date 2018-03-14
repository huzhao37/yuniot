#region Code File Comment

// SOLUTION   ： 云统物联网V3
// PROJECT    ： Yunt.Redis
// FILENAME   ： BufferPool.cs
// AUTHOR     ： soft-zh
// CREATE TIME： 2018-3-14 9:42
// COPYRIGHT  ： 版权所有 (C) 云统信息科技有限公司 http://www.unitoon.cn/ 2018~2020

#endregion

using System;
using System.Text;
using System.Threading;



namespace Yunt.Redis
{
    public class Statistics : IStatistics
    {
        public string Name { get; set; }

        public long Total;

        public long Hit;

        public Statistics(string name)
        {
            this.Name = name;
        }

        public void Show()
        {
            if (Total > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("实体缓存<{0,-20}>", Name);
                sb.AppendFormat("总次数{0,7:n0}", Total);
                if (Hit > 0) sb.AppendFormat("，命中{0,7:n0}（{1,6:P02}）", Hit, (Double)Hit / Total);
                //  if (Shoot2 > 0) sb.AppendFormat("，二级命中{0,3:n0}（{1,6:P02}）", Shoot2, (Double)Shoot2 / Total);

                 Common.Logger.Info(sb.ToString());
            }
        }

        public void AddHit()
        {
            Interlocked.Increment(ref Hit);
        }
        public void AddTotal()
        {
            Interlocked.Increment(ref Total);
        }

        public void Reset()
        {
            Interlocked.Add(ref Total, -1 * Total);
            Interlocked.Add(ref Hit, -1 * Hit);
        }
    }
}