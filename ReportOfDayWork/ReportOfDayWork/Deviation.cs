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
        public string DevFrom { get; }
        public string DevTo { get; }
        public uint DeviationId { get; }
        
        public Deviation(uint peopleId, uint devType, string devFrom, string devTo, uint deviationId)
        {
            PeopleId = peopleId;
            DevType = devType;
            DevFrom = devFrom;
            DevTo = devTo;
            DeviationId = deviationId;
        }
    }
}
