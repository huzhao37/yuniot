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

        public static string JobPath;
        /// <summary>
        /// 将类型添加到Job队列并启动
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="job">job</param>
        /// <param name="param">参数</param>
        public static void JoinToQuartz(Type type, TbJob job, Dictionary<string, object> param = null)
        {
            var obj = Activator.CreateInstance(type);
            if (!(obj is ISchedulingJob)) return;
            var tmp = obj as ISchedulingJob;
            var cron = job.Cron;//tmp.Cron;
            var name = job.Name;//tmp.JobName;
            var cancel = tmp.CancellationSource;

            var jobDetail = JobBuilder.Create(type)
                .WithIdentity(name)
                .Build();

            if (param != null)
                foreach (var dic in param)
                    jobDetail.JobDataMap.Add(dic.Key, dic.Value);


            ITrigger jobTrigger;

            if (job.Single == 1)//tmp.IsSingle
            {


                jobTrigger = TriggerBuilder.Create()
                    .WithIdentity(name + "Trigger")
                    .StartAt(DateTimeOffset.Now)
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
            Logger.Info($"->任务模块{name}-{job.Version}被装载...");
        }

        /// <summary>
        /// 将类型从Job队列中删除
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="job">job</param>
        public static void DelteToQuartz(Type type, TbJob job)
        {
            var obj = Activator.CreateInstance(type);
            if (!(obj is ISchedulingJob)) return;
            var tmp = obj as ISchedulingJob;
            var cancel = tmp.CancellationSource;
            var name = job.Name;//tmp.JobName;
            if (!Dictionary.ContainsKey(name))
            {
                Logger.Warn($"->任务模块{name}-{job.Version}不存在");
                return;
            }

            var jobKey = Dictionary[name];
            StdSchedulerFactory.GetDefaultScheduler(cancel.Token).Result.DeleteJob(jobKey, cancel.Token);
            Dictionary.Remove(name);
            Logger.Info($"->任务模块{name}-{job.Version}被删除...");
        }
        /// <summary>
        /// 从Job队列中暂停类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="job">job</param>
        public static void PauseToQuartz(Type type, TbJob job)
        {
            var obj = Activator.CreateInstance(type);
            if (!(obj is ISchedulingJob)) return;
            var tmp = obj as ISchedulingJob;
            var cancel = tmp.CancellationSource;
            var name = job.Name;//tmp.JobName;
            if (!Dictionary.ContainsKey(name))
            {
                Logger.Warn($"->任务模块{name}-{job.Version}不存在");
                return;
            }
            var jobKey = Dictionary[name];
            StdSchedulerFactory.GetDefaultScheduler(cancel.Token).Result.PauseJob(jobKey, cancel.Token);
            Logger.Info($"->任务模块{name}被暂停...");
        }
        /// <summary>
        /// 从Job队列中恢复类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="job">job</param>
        public static void ResumeToQuartz(Type type, TbJob job)
        {
            var obj = Activator.CreateInstance(type);
            if (!(obj is ISchedulingJob)) return;
            var tmp = obj as ISchedulingJob;
            var cancel = tmp.CancellationSource;
            var name = job.Name;//tmp.JobName;
            var jobKey = Dictionary[name];
            if (!Dictionary.ContainsKey(name))
            {
                Logger.Warn($"->任务模块{name}-{job.Version}不存在");
                return;
            }
            StdSchedulerFactory.GetDefaultScheduler(cancel.Token).Result.ResumeJob(jobKey, cancel.Token);
            Logger.Info($"->任务模块{name}被恢复启动...");
        }
        /// <summary>
        /// 删除压缩文件
        /// </summary>
        /// <param name="jobid"></param>
        public static bool DeleteZip(int jobid)
        {
            var zip = TbZip.Find("JobID", jobid);
            return zip?.Delete() > 0;
        }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="jobid"></param>
        /// <param name="nodeid"></param>
        /// <returns></returns>
        public static string CreateFile(int jobid, int nodeid)
        {
            var path = Path.GetFullPath(JobPath);
            if (Directory.Exists(path + "\\" + nodeid)) return path + "\\" + nodeid + "\\" + jobid;
            Directory.CreateDirectory(path + "\\" + nodeid);
            if (Directory.Exists(path + "\\" + nodeid + "\\" + jobid)) return path + "\\" + nodeid + "\\" + jobid;
            Directory.CreateDirectory(path + "\\" + nodeid + "\\" + jobid);
            if (!Directory.Exists(path + "\\" + nodeid + "\\" + jobid + "\\" + "zip"))
            {
                Directory.CreateDirectory(path + "\\" + nodeid + "\\" + jobid + "\\" + "zip");
            }
            return path + "\\" + nodeid + "\\" + jobid;

        }

        public static void DownLoadZip(int jobid, string path)
        {
            var zip = TbZip.FindAll("JobID", jobid).OrderByDescending(e => e.Time).FirstOrDefault();
            if (zip == null) return;
            var dir = new DirectoryInfo(path + "\\zip");
            if (!File.Exists(path + "\\zip"))
                dir.Create();
            File.WriteAllBytes(path + "\\zip" + "\\" + zip.Zipfilename, zip.Zipfile);
            CompressHelper.UnCompress(path + "\\zip" + "\\" + zip.Zipfilename, path);
            //删除zip缓存
            File.Delete(path + "\\" + "zip" + "\\" + zip.Zipfilename);
        }

        public static Type GetJobType(int jobid)
        {
            var job = TbJob.Find("ID", jobid);
            if (job == null)
                return null;
            var path = Path.GetFullPath(JobPath) + "\\" + job.NodeID + "\\" + jobid + "\\" + job.Name + ".dll";
            if (!File.Exists(path))
                Logger.Error($"请检查{job.Name}是否为实际的任务名称");
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
                new TbError()
                {
                    Createtime = DateTime.Now.TimeSpan(),
                    JobID = jobid,
                    Msg = $"Warn:{e.Message}"
                }.SaveAsync();

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
            var path = Path.GetFullPath(JobPath) + "\\" + job.NodeID + "\\" + jobid;
            FileEx.DelectDir(path);
            //File.Delete(path);

        }

        public static string GetJobPath(int jobid)
        {
            var job = TbJob.Find("ID", jobid);
            if (job == null)
                return null;
            return Path.GetFullPath(JobPath) + "\\" + job.NodeID + "\\" + jobid;
        }
        /// <summary>
        /// 监听Redis执行命令
        /// </summary>
        /// <param name="command"></param>
        public static void Excute(TbCommand command)
        {
            var success = 0;
            var job = TbJob.Find("ID", command.Jobid);
            if (job == null)
                return;

            Type jobtype;
            var type = (CommandType)command.Commandtype;
            switch (type)
            {
                case CommandType.Pause:
                    jobtype = GetJobType(command.Jobid);
                    if (jobtype == null)
                        return;
                    PauseToQuartz(jobtype, job);
                    job.State = 0;
                    //job.Lastedend = DateTime.Now.TimeSpan();
                    success = job.SaveAsync() ? 1 : 0;
                    break;

                case CommandType.Delete:
                    jobtype = GetJobType(command.Jobid);
                    if (jobtype == null)
                        return;
                    if (job.State == 0)
                        DelteToQuartz(jobtype, job);
                    DeleteDll(job.ID);
                    if (job.Delete() > 0)
                    {
                        var zip = TbZip.Find("JobID", command.Jobid);
                        zip?.Delete();
                        success = 1;
                    }
                    break;
                case CommandType.Stop:
                    jobtype = GetJobType(command.Jobid);
                    if (jobtype == null)
                        return;
                    DelteToQuartz(jobtype, job);
                    job.State = 0;
                    //job.Lastedend = DateTime.Now.TimeSpan();
                    job.SaveAsync();
                    success = 1;
                    break;
                case CommandType.Start:
                    if (job.State == 1)
                    {
                        jobtype = GetJobType(command.Jobid);
                        PauseToQuartz(jobtype, job);
                        File.Delete(GetJobPath(job.ID) + "zip");
                    }
                    if (Dictionary.ContainsKey(job.Name))
                    {
                        jobtype = GetJobType(command.Jobid);
                        ResumeToQuartz(jobtype, job);
                        job.State = 1;
                        //job.Runcount++;
                        //job.Lastedstart = DateTime.Now.TimeSpan();
                        success = job.SaveAsync() ? 1 : 0;
                        break;
                    }
                    var path = CreateFile(job.ID, job.NodeID);
                    DownLoadZip(job.ID, path);
                    var json = new Dictionary<string, object>();
                    if (!job.Datamap.IsNullOrWhiteSpace())
                        json = job.Datamap.ToJsonEntity<Dictionary<string, object>>();
                    if (!json.ContainsKey("jobid"))
                        json.Add("jobid", job.ID);
                    JoinToQuartz(GetJobType(command.Jobid), job, json);
                    job.State = 1;
                    //job.Runcount++;
                    //job.Lastedstart = DateTime.Now.TimeSpan();
                    success = job.SaveAsync() ? 1 : 0;
                    break;
                case CommandType.ReStart:
                    //var jsons = new Dictionary<string, object>();
                    //if (!job.Datamap.IsNullOrWhiteSpace())
                    //    jsons = job.Datamap.ToJsonEntity<Dictionary<string, object>>();
                    //if (!jsons.ContainsKey("jobid"))
                    //    jsons.Add("jobid", job.ID);
                    //jobtype = GetJobType(command.Jobid);
                    //ResumeToQuartz(jobtype);                   
                    //job.State = 1;
                    //job.Runcount++;
                    //job.Lastedstart = DateTime.Now.TimeSpan();
                    //success = job.SaveAsync() ? 1 : 0;
                    break;
                default:
                    break;
            }
            command.Success = success;

        }

        public static void ResumeJob(IEnumerable<TbJob> jobs)
        {
            if (!jobs.Any()) return;
            foreach (var tb_job in jobs)
            {
                var job = tb_job;
                var jobtype = GetJobType(job.ID);
                var json = new Dictionary<string, object>();
                if (!job.Datamap.IsNullOrWhiteSpace())
                    json = job.Datamap.ToJsonEntity<Dictionary<string, object>>();
                if (!json.ContainsKey("jobid"))
                    json.Add("jobid", job.ID);
                JoinToQuartz(jobtype, job, json);
                //job.Runcount++;
                //job.Lastedstart = DateTime.Now.TimeSpan();
                job.SaveAsync();
            }
        }
    }
}
