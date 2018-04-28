using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NewLife.Serialization;
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
                if (!Dictionary.ContainsKey(name))
                {
                    Logger.Info($"->任务模块{name}_{tmp.Version}不存在");
                    return;
                }
               
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
                if (!Dictionary.ContainsKey(name))
                {
                    Logger.Info($"->任务模块{name}_{tmp.Version}不存在");
                    return;
                }
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
                if (!Dictionary.ContainsKey(name))
                {
                    Logger.Info($"->任务模块{name}_{tmp.Version}不存在");
                    return;
                }
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
        /// <returns></returns>
        public static string CreateFile(int jobid,int nodeid)
        {
            var path = Path.GetFullPath(".\\..\\Yunt.Jobs");
            if (!Directory.Exists(path + nodeid))
            {
                Directory.CreateDirectory(path + nodeid);
                if (!Directory.Exists(path + "\\" + nodeid +"\\"+ jobid))
                {
                    Directory.CreateDirectory(path + "\\" + nodeid + "\\" + jobid);
                    if (!Directory.Exists(path + "\\" + nodeid + "\\" + jobid + "\\" + "zip"))
                    {
                        Directory.CreateDirectory(path + "\\" + nodeid + "\\" + jobid + "\\" + "zip");
                    }
                }
            }
            return path + "\\" + nodeid + "\\" + jobid ;

        }

        public static void DownLoadZip(int jobid,string path)
        {
            var zip = TbZip.Find("JobID", jobid);
            if (zip != null)
            {
                File.WriteAllBytes(path +"zip" + zip.Zipfilename, zip.Zipfile);
                CompressHelper.UnCompress(path+ "\\" + "zip" + "\\" + zip.Zipfilename, path);
                //删除zip缓存
                Directory.Delete(path + "\\" + "zip" + "\\" + zip.Zipfilename);
            }
        }

        public static Type GetJobType(int jobid)
        { 
            var job = TbJob.Find("ID", jobid);
            if(job==null)
                return null;
            var path = Path.GetFullPath(".\\..\\Yunt.Jobs") + "\\" + job.NodeID+ "\\" + jobid;

            try
            {
                using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    var asm = Assembly.Load(fs.ReadBytes());

                    foreach (var type in asm.GetTypes().Where(i => i.BaseType == typeof(JobBase)))
                    {
                        return type;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Warn(e.Message);
                using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    var asm = Assembly.Load(fs.ReadBytes());

                    foreach (var type in asm.GetTypes().Where(i => i.BaseType == typeof(JobBase)))
                    {
                        return type;
                    }
                }
            }
            return null;
        }

        public static void DeleteDll(int jobid)
        {
            var job = TbJob.Find("ID", jobid);
            if (job == null)
                return;
            var path = Path.GetFullPath(".\\..\\Yunt.Jobs") + "\\" + job.NodeID + "\\" + jobid;

            File.Delete(path);
            
        }

        public static string GetJobPath(int jobid)
        {
            var job = TbJob.Find("ID", jobid);
            if (job == null)
                return null;
            return  Path.GetFullPath(".\\..\\Yunt.Jobs") + "\\" + job.NodeID + "\\" + jobid;
        }
        /// <summary>
        /// 监听Redis执行命令
        /// </summary>
        /// <param name="command"></param>
        public static void Excute(TbCommand command)
        {
            var success = 0;
            var job = TbJob.Find("ID", command.Jobid);            
            if(job==null)
                return;
            var jobtype = GetJobType(command.Jobid);
            if (jobtype == null)
                return;
            var type = (CommandType)command.Commandtype;
            switch (type)
            {
                case CommandType.Delete:
                    if(job.State==1)
                        DelteToQuartz(jobtype);
                    if (TbJob.Delete(job) > 0)
                    {
                        var zip = TbZip.Find("JobID", command.Jobid);
                        if (zip != null)
                            TbZip.Delete(zip);
                        DeleteDll(job.ID);
                        success = 1;
                    }
                    break;
                case CommandType.Stop:                 
                    PauseToQuartz(jobtype);
                    job.State = 0;
                    success = job.SaveAsync()?1:0;
                    break;
                case CommandType.Start:
                    if (job.State == 1)
                    {
                        PauseToQuartz(jobtype);
                        File.Delete(GetJobPath(job.ID)+"zip");                      
                    }                  
                    var path=CreateFile(job.ID, job.NodeID);
                    DownLoadZip(job.ID,path+"zip");
                    var json =job.Datamap.ToJsonEntity<Dictionary<string,object>>();
                    JoinToQuartz(GetJobType(command.Jobid),Convert.ToDateTime(job.Cron), json);
                    job.State = 1;
                    job.Lastedstart = DateTime.Now.TimeSpan();                   
                    success = job.SaveAsync() ? 1 : 0;
                    break;
                case CommandType.ReStart:
                    break;
                default:
                    break;
            }
            command.Success = success;

        }
    }
}
