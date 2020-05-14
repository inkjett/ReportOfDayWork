using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ReportOfDayWork
{
    public static class Variables
    {
        public static TimeSpan halfDay = new TimeSpan(4,0,0); // 4 часа, используется как половина рабочего дня
        public static TimeSpan oneHour = new TimeSpan(1, 0, 0); // 1 час
        public static List<ConnectionSettings> connectionSettings = new List<ConnectionSettings>(); // настройки поключения к БД
        public static List<User> ArrayOfUsers = new List<User>(); // массив пользователей
        public static List<Deviation> ArrayOfDeviation = new List<Deviation>(); // массив отсутсвия на рабочем месте
        public static List<WorkTime> ArrayOfWorkTime = new List<WorkTime>(); // массив отработанного времени (выгрузка из БД)
        public static List<PeopleWorkTimeDay> ArrayOfPeopleWorkTimeDay = new List<PeopleWorkTimeDay>(); // массив отработанного времени одного дня
        public static List<List<string>> ArrayOfPeopleWorkTimeMonth = new List<List<string>>(); //массив отработанного времени в месяц
        public static string[] deviationName = new string[5] {"больничный", "отпуск", "командировка", "удаленная работа", "отгул"}; // причины отсутсвия на рабочем месте
        public static uint[] inOffice = new uint[4] { 35, 17, 26, 3}; // считыватели на приход в офис 35, 17, 26 - ИнфтехМск, 3 - ИнфтехУфа
        public static uint[] outOffice = new uint[4] { 43, 25, 34, 13 }; // считыватели на уход из офиса 43, 25, 34 - ИнфтехМск, 13 - ИнфтехУфа

    }
}
 