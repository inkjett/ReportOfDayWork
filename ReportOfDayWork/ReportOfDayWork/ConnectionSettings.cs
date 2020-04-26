using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportOfDayWork
{
    public class ConnectionSettings
    {
        public string IP { get; set; }
        public string PathToDB { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public ConnectionSettings(string ip, string pathToDB, string user, string password)
        {
            IP = ip;
            PathToDB = pathToDB;
            User = user;
            Password = password;
        }


    }
}
