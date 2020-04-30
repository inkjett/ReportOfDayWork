using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportOfDayWork
{
    class WorkTime
    {
        public DateTime Eventsdate { get; }
        public uint Cardnum { get; }        
        public uint Readerid { get; }
        public uint Peopleid { get; }        
        public string FullName { get; }

        public WorkTime(DateTime eventsdate, uint cardnum, uint readerid, uint peopleid, string fullName)
        {
            Eventsdate = eventsdate;
            Cardnum = cardnum;
            Readerid = readerid;
            Peopleid = peopleid;
            FullName = fullName;
        }



    }
}
