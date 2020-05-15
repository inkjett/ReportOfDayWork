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
        public List<PeopleWorkTimeDay> PeopleWorkTimeDay(List<User> users, List<WorkTime> workTimes, List<Deviation> deviations) // метод сборки массива об отработанном времени за день
        {
            var peopleWorkedTimes = new List<PeopleWorkTimeDay>(users.Count);

            foreach (var user in users)
            {
                //ячейка времени прихода на работу
                var cameToWorkTimesOrdered = workTimes.Where(o => (o.PeopleId == user.Id) && (Variables.inOffice.Contains(o.ReaderId)))
                    .OrderBy(o => o.EventsDate).ToList(); // пользователь
                var comingToWork = cameToWorkTimesOrdered.Count != 0 ?
                    cameToWorkTimesOrdered.FirstOrDefault()?.EventsDate.ToLongTimeString() :
                    string.Empty;// добавляем время прихода на работу 

                //ячейка времени ухода с работы
                var leftFromWorkTimesOrdered = workTimes.Where(o => (o.PeopleId == user.Id) && (Variables.outOffice.Contains(o.ReaderId)))
                    .OrderBy(o => o.EventsDate).ToList();
                var leavingFromWork = leftFromWorkTimesOrdered.Count != 0 ?
                    leftFromWorkTimesOrdered.LastOrDefault()?.EventsDate.ToLongTimeString() : string.Empty;// добавляем время прихода на работу 

                //ячейка времени проведенного на работе
                var beingAtWork = (cameToWorkTimesOrdered.Count != 0) && (leftFromWorkTimesOrdered.Count != 0) ?
                    ((leftFromWorkTimesOrdered.LastOrDefault()?.EventsDate - cameToWorkTimesOrdered[0].EventsDate) < Variables.halfDay ?
                        (leftFromWorkTimesOrdered.LastOrDefault()?.EventsDate - cameToWorkTimesOrdered.FirstOrDefault()?.EventsDate).ToString() :
                        (leftFromWorkTimesOrdered.LastOrDefault()?.EventsDate - cameToWorkTimesOrdered.FirstOrDefault()?.EventsDate - Variables.oneHour).ToString()) : string.Empty;

                //ячейка отсутсвия на рабочем месте
                var deviationsFiltered = deviations.Where(o => ((o.PeopleId == user.Id) && (o.DevFrom <= workTimes[0].EventsDate) && (workTimes[0].EventsDate <= o.DevTo))).ToList();
                var devaition = deviationsFiltered.Count != 0 ?
                    Variables.deviationName.Select((i, index) => new { i, index }).Where(n => n.index == deviationsFiltered.FirstOrDefault()?.DevType).ToList()[0].i :
                    string.Empty; // проходим по массиву данных об отсутствии на рабочем месте, заменяем значение по индексу массива

                peopleWorkedTimes.Add(new PeopleWorkTimeDay(user.FullName, comingToWork, leavingFromWork, beingAtWork, devaition));
            }
            return peopleWorkedTimes;
        }
        //---------

        public List<List<string>> PeopleWorkTimeMonth(List<User> arrayOfuser, List<WorkTime> arrayOfWorkTime, List<Deviation> deviations)
        {

            // в разработке
            List<List<string>> arrayOfPeopleWorkTime = new List<List<string>>();
            List<string> row = new List<string>();
            row = new List<string>();
            arrayOfPeopleWorkTime.Add(row);
            var temp = new DateTime(arrayOfWorkTime[0].EventsDate.Year, arrayOfWorkTime[0].EventsDate.Month, 1).ToLongDateString();
            arrayOfPeopleWorkTime[0].Add("ФИО сотрудника");
            var lastDay = DateTime.DaysInMonth(arrayOfWorkTime[0].EventsDate.Year, arrayOfWorkTime[0].EventsDate.Month); // последний день месяца            

            //arrayOfPeopleWorkTime[0].AddRange(Enumerable.Range(1, lastDay).Select(o => Convert.ToString(new DateTime(arrayOfWorkTime[0].EventsDate.Year, arrayOfWorkTime[0].EventsDate.Month, o).ToShortDateString())).ToList());
            arrayOfPeopleWorkTime[0].AddRange(Enumerable.Range(1, lastDay).Select(o => (new DateTime(arrayOfWorkTime[0].EventsDate.Year, arrayOfWorkTime[0].EventsDate.Month, o).ToLongDateString())));
            /*for (int t = 0; t < lastDay; t++)//добавление дат в первую строку
            {
                arrayOfPeopleWorkTime[0].Add(Convert.ToString(new DateTime(arrayOfWorkTime[0].EventsDate.Year, arrayOfWorkTime[0].EventsDate.Month, t + 1).ToShortDateString()));
            }*/




            return arrayOfPeopleWorkTime;
        }

    }
}
