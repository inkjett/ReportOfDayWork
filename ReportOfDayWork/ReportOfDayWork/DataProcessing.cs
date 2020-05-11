using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ReportOfDayWork
{
    class DataProcessing
    {

        public List<PeopleWorkTimeDay> PeopleWorkTimeDay(List<User> arrayOfuser, List<WorkTime> arrayOfWorkTime, List<Deviation> deviations) // метод сборки массива об отработанном времени за день
        {
            List<PeopleWorkTimeDay> arrayOfPeopleWorkTime = new List<PeopleWorkTimeDay>();
            for (var u = 0; u < arrayOfuser.Count; u++) //идем по массиву пользователей
            {
                arrayOfPeopleWorkTime.Add(new PeopleWorkTimeDay(arrayOfuser[u].FullName, null, null, null, null));// записываем ФИО пользователя
                for (var w = 0; w < arrayOfWorkTime.Count; w++) // идем по массиву с данными об отработаном времени 
                {
                    if ((Variables.inOffice.ToList().IndexOf(arrayOfWorkTime[w].ReaderId) != -1) && (arrayOfuser[u].Сardnum == arrayOfWorkTime[w].Cardnum))// проверка прихода на работу денные о считывателях берем из массива inOffice
                    {                        
                        arrayOfPeopleWorkTime[u].ComingToWork = arrayOfWorkTime[w].EventsDate;// добавляем время прихода на работу     
                        for (var j = 0; j < arrayOfWorkTime.Count; j++)// ищем выход с работы
                        {
                            if ((arrayOfWorkTime[w].PeopleId == arrayOfWorkTime[j].PeopleId) && (arrayOfWorkTime[w].EventsDate < arrayOfWorkTime[j].EventsDate) && (Variables.inOffice.ToList().IndexOf(arrayOfWorkTime[j].ReaderId) != -1)) // удаляем лишние временные метки между приходом на работу и уходом с работы
                            {
                                arrayOfWorkTime.RemoveAt(j);
                                j--;
                            }
                            else if ((arrayOfWorkTime[w].PeopleId == arrayOfWorkTime[j].PeopleId) && (Variables.outOffice.ToList().IndexOf(arrayOfWorkTime[j].ReaderId) != -1)) // время выхода с работы данные о считывателях берем из массива outOffice
                            {
                                arrayOfPeopleWorkTime[arrayOfPeopleWorkTime.Count - 1].LeavingWork = arrayOfWorkTime[j].EventsDate;// время ухода с работы
                                arrayOfPeopleWorkTime[arrayOfPeopleWorkTime.Count - 1].BeingAtWork = (arrayOfWorkTime[j].EventsDate - arrayOfWorkTime[w].EventsDate) < Variables.halfDay ? (arrayOfWorkTime[j].EventsDate - arrayOfWorkTime[w].EventsDate) : (arrayOfWorkTime[j].EventsDate - arrayOfWorkTime[w].EventsDate - Variables.oneHour); // вычисление времени проведенного на работе
                            }
                        }
                    }
                }
                for (var d = 0; d < deviations.Count; d++)//ищем не находится ли данный пользователь в отпуске итд
                {
                    if ((arrayOfuser[u].Id == deviations[d].PeopleId) && (deviations[d].DevFrom <= arrayOfWorkTime[0].EventsDate) && (arrayOfWorkTime[0].EventsDate <= deviations[d].DevTo)) // проверяем на id прользователя и временные рамки
                    {
                        arrayOfPeopleWorkTime[u].Deviation = Variables.deviationName.Select((i,index)=> new {i, index }).Where(n=>n.index == deviations[d].DevType).ToList()[0].i; // проходим по массиву данных об отсутствии на рабочем месте
                        break;
                    }
                }
            }
            return arrayOfPeopleWorkTime;
        }
        //---------


    }
}
