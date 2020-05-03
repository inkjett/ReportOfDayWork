using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ReportOfDayWork
{
    public static class Variables
    {
        public static TimeSpan halfDay = new TimeSpan(4,0,0); // пол дня 4 часа
        public static TimeSpan oneHour = new TimeSpan(1, 0, 0); // пол дня 4 часа
        public static List<ConnectionSettings> connectionSettings = new List<ConnectionSettings>(); // настройки поключения к БД
        public static List<User> ArrayOfUsers = new List<User>(); // массив пользователей
        public static List<Deviation> ArrayOfDeviation = new List<Deviation>(); // массив отсутсвия на рабочем месте
        public static List<WorkTime> ArrayOfWorkTime = new List<WorkTime>(); // массив отработанного времени (выгрузка из БД) ----- сделана для отладки
        public static List<PeopleWorkTime> ArrayOfPeopleWorkTime = new List<PeopleWorkTime>(); // массив отработанного времени одного дня

    }
}
