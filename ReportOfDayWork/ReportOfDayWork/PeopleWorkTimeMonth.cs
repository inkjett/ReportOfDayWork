﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportOfDayWork
{
    public class PeopleWorkTimeMonth
    {
        public string FullName { get; set; }
        public int DayCount { get; }


        public PeopleWorkTimeMonth(string fullName, int dayCount)
        {
            FullName = fullName;
        }
    }
}
