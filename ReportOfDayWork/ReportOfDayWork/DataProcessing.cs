using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ReportOfDayWork
{
    class DataProcessing
    {
        public List<PeopleWorkTimeDay> PeopleWorkTimeDay(List<User> arrayOfuser, List<WorkTime> arrayOfWorkTime, List<Deviation> deviations) // метод сборки массива об отработанном времени за день
        {
            List<PeopleWorkTimeDay> arrayOfPeopleWorkTime = new List<PeopleWorkTimeDay>();
            arrayOfPeopleWorkTime.Add(new PeopleWorkTimeDay("ФИО сотрудника", "Время прихода сотрудника", "Время ухода сотрудника", "Время проведенное в офисе", "Причина отсутствия"));
            for (var u = 0; u < arrayOfuser.Count; u++) //идем по массиву пользователей
            {
                arrayOfPeopleWorkTime.Add(new PeopleWorkTimeDay(arrayOfuser[u].FullName, null, null, null, null));// записываем ФИО пользователя

                //ячейка времени прихода на работу
                var ComeToWork = arrayOfWorkTime.Where(o => (o.PeopleId == arrayOfuser[u].Id) && (Variables.inOffice.ToList().IndexOf(o.ReaderId) != -1)).ToList(); // пользователь
                ComeToWork.OrderBy(o => o.EventsDate);//сортировка
                arrayOfPeopleWorkTime[u + 1].ComingToWork = ComeToWork.Count!=0? ComeToWork[0].EventsDate.ToLongTimeString():"";// добавляем время прихода на работу 
                
                //ячейка времени ухода с работы
                var LeaveWork = arrayOfWorkTime.Where(o => (o.PeopleId == arrayOfuser[u].Id) && (Variables.outOffice.ToList().IndexOf(o.ReaderId) != -1)).ToList();
                LeaveWork.OrderBy(o => o.EventsDate);//сортировка
                arrayOfPeopleWorkTime[u + 1].LeavingWork = LeaveWork.Count != 0 ? LeaveWork[LeaveWork.Count-1].EventsDate.ToLongTimeString() : "";// добавляем время прихода на работу 
                
                //ячейка времени проведенного на работе
                arrayOfPeopleWorkTime[arrayOfPeopleWorkTime.Count - 1].BeingAtWork = (ComeToWork.Count != 0) && (LeaveWork.Count != 0) ? ((LeaveWork[LeaveWork.Count - 1].EventsDate - ComeToWork[0].EventsDate) < Variables.halfDay ? (LeaveWork[LeaveWork.Count - 1].EventsDate - ComeToWork[0].EventsDate).ToString() : (LeaveWork[LeaveWork.Count - 1].EventsDate - ComeToWork[0].EventsDate - Variables.oneHour).ToString()):"";
                
                //ячейка отсутсвия на рабочем месте
                var deviation = deviations.Where(o => ((o.PeopleId == arrayOfuser[u].Id) && (o.DevFrom <= arrayOfWorkTime[0].EventsDate) && (arrayOfWorkTime[0].EventsDate <= o.DevTo))).ToList();
                arrayOfPeopleWorkTime[u + 1].Deviation = deviation.Count != 0 ? Variables.deviationName.Select((i, index) => new { i, index }).Where(n => n.index == deviation[0].DevType).ToList()[0].i : arrayOfPeopleWorkTime[u + 1].Deviation = ""; // проходим по массиву данных об отсутствии на рабочем месте, заменяем значение по индексу массива

        }
            return arrayOfPeopleWorkTime;
        }
        //---------

        public List<List<string>> PeopleWorkTimeMonth(List<User> arrayOfuser, List<WorkTime> arrayOfWorkTime, List<Deviation> deviations)
        {

            // в разработке
            List<List<string>> arrayOfPeopleWorkTime = new List<List<string>>();
            List<string> row = new List<string>();
            row = new List<string>();
            arrayOfPeopleWorkTime.Add(row);

            arrayOfPeopleWorkTime[0].Add("ФИО сотрудника");
            var lastDay = DateTime.DaysInMonth(arrayOfWorkTime[0].EventsDate.Year, arrayOfWorkTime[0].EventsDate.Month); // последний день месяца            
            //arrayOfPeopleWorkTime[0].Select (o=> o.Count() <= lastDay).ToList(new DateTime(arrayOfWorkTime[0].EventsDate.Year, arrayOfWorkTime[0].EventsDate.Month, 1).ToShortDateString());
            //arrayOfPeopleWorkTime[0].Select(o => { if(o.Count() <= lastDay) lastDay++; });
            arrayOfPeopleWorkTime[0].AddRange(Enumerable.Range(1, lastDay).Select(o => Convert.ToString(new DateTime(arrayOfWorkTime[0].EventsDate.Year, arrayOfWorkTime[0].EventsDate.Month, o).ToShortDateString())));
            for (int t = 0; t < lastDay; t++)//добавление дат в первую строку
            {
                arrayOfPeopleWorkTime[0].Add(Convert.ToString(new DateTime(arrayOfWorkTime[0].EventsDate.Year, arrayOfWorkTime[0].EventsDate.Month, t + 1).ToShortDateString()));
            }




            return arrayOfPeopleWorkTime;
        }

    }
}
