using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Yunt.Common
{
   public class FileEx
    {
        /// <summary>
        /// 从一个目录将其内容移动到另一目录  
        /// </summary>
        /// <param name="directorySource">源目录</param>
        /// <param name="directoryTarget">目标目录</param>
        public static void MoveFolderTo(string directorySource, string directoryTarget)
        {
            //检查是否存在目的目录  
            if (!Directory.Exists(directoryTarget))
            {
                Directory.CreateDirectory(directoryTarget);
            }
            //先来移动文件  
            DirectoryInfo directoryInfo = new DirectoryInfo(directorySource);
            FileInfo[] files = directoryInfo.GetFiles();
            //移动所有文件  
            foreach (FileInfo file in files)
            {
                //如果自身文件在运行，不能直接覆盖，需要重命名之后再移动  
                if (File.Exists(Path.Combine(directoryTarget, file.Name)))
                {
                    if (File.Exists(Path.Combine(directoryTarget, file.Name + ".bak")))
                    {
                        File.Delete(Path.Combine(directoryTarget, file.Name + ".bak"));
                    }
                    File.Move(Path.Combine(directoryTarget, file.Name), Path.Combine(directoryTarget, file.Name + ".bak"));

                }
                file.MoveTo(Path.Combine(directoryTarget, file.Name));

            }
            //最后移动目录  
            DirectoryInfo[] directoryInfoArray = directoryInfo.GetDirectories();
            foreach (DirectoryInfo dir in directoryInfoArray)
            {
                MoveFolderTo(Path.Combine(directorySource, dir.Name), Path.Combine(directoryTarget, dir.Name));
            }
        }

        /// <summary>
        /// 从一个目录将其内容移动到另一目录  
        /// </summary>
        /// <param name="directorySource">源目录</param>
        /// <param name="directoryTarget">目标目录</param>
        public static void CopyFolderTo(string directorySource, string directoryTarget)
        {
            //检查是否存在目的目录  
            if (!Directory.Exists(directoryTarget))
            {
                Directory.CreateDirectory(directoryTarget);
            }
            //先来移动文件  
            DirectoryInfo directoryInfo = new DirectoryInfo(directorySource);
            FileInfo[] files = directoryInfo.GetFiles();
            //移动所有文件  
            foreach (FileInfo file in files)
            {
                var path = Path.Combine(directoryTarget, file.Name);
                //直接覆盖??
                if (File.Exists(path))
                {                 
                    // File.Delete(Path.Combine(directoryTarget, file.Name));
                    
                    //File.Copy(Path.Combine(directorySource, file.Name), Path.Combine(directoryTarget, file.Name));

                }
                else
                {
                    file.CopyTo(path);

                }
       

            }
            ////最后移动目录  
            //DirectoryInfo[] directoryInfoArray = directoryInfo.GetDirectories();
            //foreach (DirectoryInfo dir in directoryInfoArray)
            //{
            //    MoveFolderTo(Path.Combine(directorySource, dir.Name), Path.Combine(directoryTarget, dir.Name));
            //}
        }
        /// <summary>
        /// 加载根目录下所有dll文件
        /// </summary>
        public static void TryLoadAssembly()
        {
            var entry = Assembly.GetEntryAssembly();
            //找到当前执行文件所在路径
            var dir = Path.GetDirectoryName(entry.Location);
            var entryName = entry.GetName().Name;
            //获取执行文件同一目录下的其他dll
            foreach (var dll in Directory.GetFiles(dir, "*.dll"))
            {
                if (entryName.Equals(Path.GetFileNameWithoutExtension(dll)))
                {
                    continue;
                }
                //非程序集类型的关联load时会报错
                try
                {
                    AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);
                }
                catch (Exception ex)
                {
                    Common.Logger.Exception(ex);
                    Environment.Exit(1);
                }
            }
        }

        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
