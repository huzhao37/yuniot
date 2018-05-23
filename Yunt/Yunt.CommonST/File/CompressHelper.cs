using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Readers;

namespace Yunt.Common
{
   public class CompressHelper
    { 
        /// <summary>
      /// 通用解压 支持rar,zip
      /// </summary>
      /// <param name="compressfilepath"></param>
      /// <param name="uncompressdir"></param>
        public static void UnCompress(string compressfilepath, string uncompressdir)
        {
            string ext = Path.GetExtension(compressfilepath).ToLower();
            if (ext == ".rar")
                UnRar(compressfilepath, uncompressdir);
            else if (ext == ".zip")
                UnZip(compressfilepath, uncompressdir);
        }
        /// <summary>
        /// 解压rar
        /// </summary>
        /// <param name="compressfilepath"></param>
        /// <param name="uncompressdir"></param>
        private static void UnRar(string compressfilepath, string uncompressdir)
        {
            using (Stream stream = File.OpenRead(compressfilepath))
            {
                using (var reader = ReaderFactory.Open(stream))
                {
                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory)
                        {
                            var options = new ExtractionOptions(){ExtractFullPath = true,Overwrite = true};
                            reader.WriteEntryToDirectory(uncompressdir, options);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 解压zip
        /// </summary>
        /// <param name="compressfilepath"></param>
        /// <param name="uncompressdir"></param>
        public static void UnZip(string compressfilepath, string uncompressdir)
        {
            using (var stream = File.OpenRead(compressfilepath))
            {
                var reader = ReaderFactory.Open(stream);
                while (reader.MoveToNextEntry())
                {
                    if (reader.Entry.IsDirectory) continue;
                    //排除调试文件
                    if (reader.Entry.Key.EndsWith(".pdb"))
                        continue;
                    var options = new ExtractionOptions() { ExtractFullPath = true, Overwrite = true };
                    reader.WriteEntryToDirectory(uncompressdir, options);
                }
            }
            //using (var archive = ArchiveFactory.Open(compressfilepath))
            //{
            //    foreach (var entry in archive.Entries)
            //    {
            //        //排除调试文件
            //        if(entry.Key.EndsWith(".pdb"))
            //            continue;
            //        if (!entry.IsDirectory)
            //        {
            //            var options = new ExtractionOptions() { ExtractFullPath = true, Overwrite = true };
            //            entry.WriteToDirectory(uncompressdir, options);
            //        }
            //    }
            //}
        }
    }
}
