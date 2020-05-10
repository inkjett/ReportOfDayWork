using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportOfDayWork
{
    public class Deviation
    {

        public uint PeopleId { get; }
        public uint DevType { get; }
        public DateTime DevFrom { get; }
        public DateTime DevTo { get; }
        public uint DeviationId { get; }
        
        public Deviation(uint peopleId, uint devType, DateTime devFrom, DateTime devTo, uint deviationId)
        {
            PeopleId = peopleId;
            DevType = devType;
            DevFrom = devFrom;
            DevTo = devTo;
            DeviationId = deviationId;
        }
    }
}
