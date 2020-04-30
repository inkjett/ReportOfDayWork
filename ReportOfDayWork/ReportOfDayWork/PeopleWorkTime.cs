using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportOfDayWork
{
    public class WorkTime
    {
        public string FullName { get; }
        public DateTime ComingToWork { get; }
        public DateTime LeavingWork { get; }
        public DateTime BeingAtWork { get; }
        public string Deviation { get; }
        
        public WorkTime(string fullName, DateTime comingToWork, DateTime leavingWork, DateTime beingAtWork, string deviation)
        {
            FullName = fullName;
            ComingToWork = comingToWork;
            LeavingWork = leavingWork;
            BeingAtWork = beingAtWork;
            Deviation = deviation;
        }
    }
}
