using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1
{
    public partial class TbTask
    {
        public int Id { get; set; }
        public string Taskname { get; set; }
        public int Categoryid { get; set; }
        public int Nodeid { get; set; }
        public DateTime Taskcreatetime { get; set; }
        public DateTime Taskupdatetime { get; set; }
        public DateTime Tasklaststarttime { get; set; }
        public DateTime Tasklastendtime { get; set; }
        public DateTime Tasklasterrortime { get; set; }
        public int Taskerrorcount { get; set; }
        public long Taskruncount { get; set; }
        public int Taskcreateuserid { get; set; }
        public byte Taskstate { get; set; }
        public int Taskversion { get; set; }
        public string Taskappconfigjson { get; set; }
        public string Taskcron { get; set; }
        public string Taskmainclassdllfilename { get; set; }
        public string Taskmainclassnamespace { get; set; }
        public string Taskremark { get; set; }
    }
}
