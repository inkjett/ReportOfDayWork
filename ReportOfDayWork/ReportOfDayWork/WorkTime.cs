using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportOfDayWork
{
    public class WorkTime
    {
        public DateTime EventsDate { get; }
        public uint Cardnum { get; }        
        public uint ReaderId { get; }
        public uint PeopleId { get; }        
        public string FullName { get; }

        public WorkTime(DateTime eventsdate, uint cardnum, uint readerid, uint peopleid, string fullName)
        {
            EventsDate = eventsdate;
            Cardnum = cardnum;
            ReaderId = readerid;
            PeopleId = peopleid;
            FullName = fullName;
        }
    }
}
