using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportOfDayWork
{
    public class PeopleWorkTime
    {
        public string FullName { get; set; }
        public string ComingToWork { get; set; } // добавляем ? - Using a question mark (?) after the type or using the generic style Nullable т.е. можем выставлять DateTime - null
        public string LeavingWork { get; set; }
        public string BeingAtWork { get; set; }
        public string Deviation { get; set; }

        public PeopleWorkTime(string fullName, string comingToWork, string leavingWork, string beingAtWork, string deviation)
        {
            FullName = fullName;
            ComingToWork = comingToWork;
            LeavingWork = leavingWork;
            BeingAtWork = beingAtWork;
            Deviation = deviation;

        }
    }
}
