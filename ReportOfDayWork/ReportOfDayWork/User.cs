using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportOfDayWork
{
    public class User
    {
        public string FullName { get; } // полное имя
        public uint Id { get; }
        public uint Сardnum { get; }

        public User(string fullName, uint id, uint cardnum)
        {
            FullName = fullName;
            Id = id;
            Сardnum = cardnum;
        }
    }
}
