using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Quartz;
using Quartz.Impl;
using Yunt.Common;
using Yunt.Dtsc.Domain.Model;

namespace Yunt.Dtsc.Core
{
   public class JobHelper
    {
        private static readonly Dictionary<string, JobKey> Dictionary = new Dictionary<string, JobKey>();
        /// <summary>
        /// 将类型添加到Job队列并启动
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="dt">时间点</param>
        /// <param name="param">参数</param>
        public static void JoinToQuartz(Type type, DateTimeOffset dt, Dictionary<string, object> param = null)
        {
            var obj = Activator.CreateInstance(type);
            if (obj is ISchedulingJob)
            {
                var tmp = obj as ISchedulingJob;
                var cron = tmp.Cron;
                var name = tmp.JobName;
                var cancel = tmp.CancellationSource;
                
                //if(Dictionary.ContainsKey(name))
                //    DelteToQuartz(type);

                var jobDetail = JobBuilder.Create(type)
                                          .WithIdentity(name)
                                          .Build();


                if (param != null)
                    foreach (var dic in param)
                        jobDetail.JobDataMap.Add(dic.Key, dic.Value);

                ITrigger jobTrigger;
                if (tmp.IsSingle)
                {
                    jobTrigger = TriggerBuilder.Create()
                                               .WithIdentity(name + "Trigger")
                                               .StartAt(dt)
                                               .Build();
                }
                else
                {
                    jobTrigger = TriggerBuilder.Create()
                                                .WithIdentity(name + "Trigger")
                                                .StartNow()
                                                .WithCronSchedule(cron)
                                                .Build();
                }

                Dictionary.TryAdd(name, jobDetail.Key);
                StdSchedulerFactory.GetDefaultScheduler(cancel.Token).Result.ScheduleJob(jobDetail, jobTrigger, cancel.Token);
                StdSchedulerFactory.GetDefaultScheduler(cancel.Token).Result.Start(cancel.Token);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Logger.Info($"->任务模块{name}_{tmp.Version}被装载...");
            }
        }

        /// <summary>
        /// 将类型从Job队列中删除
        /// </summary>
        /// <param name="type">类型</param>
        public static void DelteToQuartz(Type type)
        {
            var obj = Activator.CreateInstance(type);
            if (obj is ISchedulingJob)
            {
                var tmp = obj as ISchedulingJob;
                var cancel = tmp.CancellationSource;
                var name = tmp.JobName;
                var jobKey = Dictionary[name];
                StdSchedulerFactory.GetDefaultScheduler(cancel.Token).Result.DeleteJob(jobKey, cancel.Token);
                Logger.Info($"->任务模块{name}_{tmp.Version}被删除...");
            }
        }
        /// <summary>
        /// 从Job队列中暂停类型
        /// </summary>
        /// <param name="type">类型</param>
        public static void PauseToQuartz(Type type)
        {
            var obj = Activator.CreateInstance(type);
            if (obj is ISchedulingJob)
            {
                var tmp = obj as ISchedulingJob;
                var cancel = tmp.CancellationSource;
                var name = tmp.JobName;
                var jobKey = Dictionary[name];
                StdSchedulerFactory.GetDefaultScheduler(cancel.Token).Result.PauseJob(jobKey, cancel.Token);
                Logger.Info($"->任务模块{name}被暂停...");
            }
        }
        /// <summary>
        /// 从Job队列中恢复类型
        /// </summary>
        /// <param name="type">类型</param>
        public static void ResumeToQuartz(Type type)
        {
            var obj = Activator.CreateInstance(type);
            if (obj is ISchedulingJob)
            {
                var tmp = obj as ISchedulingJob;
                var cancel = tmp.CancellationSource;
                var name = tmp.JobName;
                var jobKey = Dictionary[name];
                StdSchedulerFactory.GetDefaultScheduler(cancel.Token).Result.ResumeJob(jobKey, cancel.Token);
                Logger.Info($"->任务模块{name}被恢复启动...");
            }
        }
        /// <summary>
        /// 删除压缩文件
        /// </summary>
        /// <param name="jobid"></param>
        public static bool DeleteZip(int jobid)
        {
            var zip = TbZip.Find("JobID", jobid);
            if (zip != null)
                return TbZip.Delete(zip)>0;
            return false;
        }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="jobid"></param>
        /// <param name="nodeid"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static string CreateFile(int jobid,int nodeid)
        {
            var path = Path.GetFullPath(".\\..\\Yunt.Jobs");
            if (!Directory.Exists(path + nodeid))
            {
                Directory.CreateDirectory(path + nodeid);
                if (!Directory.Exists(path + nodeid + jobid))
                {
                    Directory.CreateDirectory(path + nodeid + jobid);
                    if (!Directory.Exists(path + nodeid + jobid+"zip"))
                    {
                        Directory.CreateDirectory(path + nodeid + jobid + "zip");
                    }
                }
            }
            return path + nodeid + jobid ;

        }

        public static void DownLoadZip(int jobid,string path)
        {
            var zip = TbZip.Find("JobID", jobid);
            if (zip != null)
            {
                File.WriteAllBytes(path +"zip" + zip.Zipfilename, zip.Zipfile);
                CompressHelper.UnCompress(path+ "zip" + zip.Zipfilename, path);
                //删除zip缓存
                Directory.Delete(path + "zip" + zip.Zipfilename);
            }
        }
    }
}
