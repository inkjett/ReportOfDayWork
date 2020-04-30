using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ReportOfDayWork
{
    public static class Variables
    {
        public static List<ConnectionSettings> connectionSettings = new List<ConnectionSettings>(); // настройки поключения к БД
        public static List<User> ArrayOfUsers = new List<User>(); // массив пользователей
        public static List<Deviation> ArrayOfDeviation = new List<Deviation>(); // массив отсутсвия на рабочем месте


    }
}
