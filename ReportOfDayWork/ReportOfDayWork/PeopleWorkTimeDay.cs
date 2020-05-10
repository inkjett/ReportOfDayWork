using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportOfDayWork
{
    public class PeopleWorkTimeDay
    {
        public string FullName { get; set; }
        public DateTime ?ComingToWork { get; set; } // добавляем ? - Using a question mark (?) after the type or using the generic style Nullable т.е. можем выставлять DateTime - null
        public DateTime ?LeavingWork { get; set; }
        public TimeSpan ?BeingAtWork { get; set; }
        public string Deviation { get; set; }

        public PeopleWorkTimeDay(string fullName, DateTime ?comingToWork, DateTime ?leavingWork, TimeSpan ?beingAtWork, string deviation)
        {
            FullName = fullName;
            ComingToWork = comingToWork;
            LeavingWork = leavingWork;
            BeingAtWork = beingAtWork;
            Deviation = deviation;

        }
    }
}
