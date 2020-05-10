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
        public uint Cardnum { get; }        // номер карты
        public uint ReaderId { get; }// номер считывателя
        public uint PeopleId { get; }// номер пользоватлетя в БД        

        public WorkTime(DateTime eventsdate, uint cardnum, uint readerid, uint peopleid)
        {
            EventsDate = eventsdate;
            Cardnum = cardnum;
            ReaderId = readerid;
            PeopleId = peopleid;
        }
    }
}
