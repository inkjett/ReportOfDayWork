﻿using System;
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
                var beingAtWork =  leftFromWorkTimesOrdered.Count != 0 ?
                    ((leftFromWorkTimesOrdered.LastOrDefault()?.EventsDate - cameToWorkTimesOrdered[0].EventsDate) < Variables.halfDay ?
                        (leftFromWorkTimesOrdered.LastOrDefault()?.EventsDate - cameToWorkTimesOrdered.FirstOrDefault()?.EventsDate).ToString() :
                        (leftFromWorkTimesOrdered.LastOrDefault()?.EventsDate - cameToWorkTimesOrdered.FirstOrDefault()?.EventsDate - Variables.oneHour).ToString()) : string.Empty;
                
                //ячейка отсутсвия на рабочем месте
                var deviationsFiltered = deviations.Where(o => ((o.PeopleId == user.Id) && (o.DevFrom <= workTimes[0].EventsDate) && (workTimes[0].EventsDate <= o.DevTo))).ToList();
                var deviation = deviationsFiltered.Count != 0 ? 
                    Variables.deviationName.Select((i, index) => new { i, index }).Where(n => n.index == deviationsFiltered.FirstOrDefault()?.DevType).ToList()[0].i :
                    string.Empty; // проходим по массиву данных об отсутствии на рабочем месте, заменяем значение по индексу массива

                peopleWorkedTimes.Add(new PeopleWorkTimeDay(user.FullName, comingToWork, leavingFromWork, beingAtWork, deviation));
            }
            return peopleWorkedTimes;
        }
        //---------

        public IEnumerable<PeopleWorkedTimeInMonth> PeopleWorkTimeMonth(List<User> users, List<WorkTime> workedTimes, List<Deviation> deviations)
        {
            var result = new List<PeopleWorkedTimeInMonth>(users.Count);

            foreach (var user in users)
            {
                var groupsByPeopleId = workedTimes.GroupBy(m => m.PeopleId);

                var workedTimeInMoths = new List<WorkedTimeInDay>();
                
                foreach (var month in groupsByPeopleId)
                {
                    foreach (var workTime in month.GroupBy(x => x.EventsDate.Day))
                    {
                        var workedTimesByOrder = workTime.OrderBy(tw => tw.EventsDate).ToList();

                        //ячейка времени проведенного на работе
                        var beingAtWork = workedTimesByOrder.Count != 0 ?
                            ((workedTimesByOrder.LastOrDefault()?.EventsDate - workedTimesByOrder.FirstOrDefault()?.EventsDate) < Variables.halfDay ?
                                (workedTimesByOrder.LastOrDefault()?.EventsDate - workedTimesByOrder.FirstOrDefault()?.EventsDate) :
                                (workedTimesByOrder.LastOrDefault()?.EventsDate - workedTimesByOrder.FirstOrDefault()?.EventsDate - Variables.oneHour)) : null;

                        workedTimeInMoths.Add(new WorkedTimeInDay(workTime.First().EventsDate.Day, beingAtWork));
                    }
                }

                result.Add(new PeopleWorkedTimeInMonth(user.FullName, workedTimeInMoths));
            }

            return result;
        }

        public int GetDaysInMonth(DateTime date) => DateTime.DaysInMonth(date.Year, date.Month);

    }

    public class PeopleWorkedTimeInMonth
    {
        public string FullName { get; }

        public List<WorkedTimeInDay> WorkedTimeDaysInMonth { get; }

        public PeopleWorkedTimeInMonth(string fullName, List<WorkedTimeInDay> workedTimeDaysInMonth)
        {
            FullName = fullName;
            WorkedTimeDaysInMonth = workedTimeDaysInMonth;
        }
    }

    public class WorkedTimeInDay
    {
        public int Day { get; }

        public TimeSpan? WorkedTime { get; }

        public WorkedTimeInDay(int day, TimeSpan? workedTime)
        {
            Day = day;
            WorkedTime = workedTime;
        }
    }

}
