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
                    if ((arrayOfWorkTime[w].ReaderId == 3) && (arrayOfuser[u].Сardnum == arrayOfWorkTime[w].Cardnum))// проверка прихода на работу 3-вход 13-выход
                    {
                        arrayOfPeopleWorkTime[u].ComingToWork = arrayOfWorkTime[w].EventsDate;// добавляем время прихода на работу     
                        for (var j = 0; j < arrayOfWorkTime.Count; j++)// ищем выход с работы
                        {
                            if ((arrayOfWorkTime[w].PeopleId == arrayOfWorkTime[j].PeopleId) && (arrayOfWorkTime[w].EventsDate < arrayOfWorkTime[j].EventsDate) && (arrayOfWorkTime[j].ReaderId == 3)) // удаляем лишние временные метки между приходом на работу и уходом с работы
                            {
                                arrayOfWorkTime.RemoveAt(j);
                                j--;
                            }
                            else if ((arrayOfWorkTime[w].PeopleId == arrayOfWorkTime[j].PeopleId) && (arrayOfWorkTime[j].ReaderId == 13)) // время выхода с работы
                            {
                                arrayOfPeopleWorkTime[arrayOfPeopleWorkTime.Count - 1].LeavingWork = arrayOfWorkTime[j].EventsDate;// время ухода с работы
                                arrayOfPeopleWorkTime[arrayOfPeopleWorkTime.Count - 1].BeingAtWork = (arrayOfWorkTime[j].EventsDate - arrayOfWorkTime[w].EventsDate) < Variables.halfDay ? (arrayOfWorkTime[j].EventsDate - arrayOfWorkTime[w].EventsDate) : (arrayOfWorkTime[j].EventsDate - arrayOfWorkTime[w].EventsDate - Variables.oneHour); // вычисление времени проведенного на работе
                            }
                        }
                    }
                }
                for (var d = 0; d < deviations.Count; d++)//ищем не находится ли данный пользователь в отпуске итд
                {
                    if ((arrayOfuser[u].Id == deviations[d].PeopleId) && (deviations[d].DevFrom <= arrayOfWorkTime[0].EventsDate) && (arrayOfWorkTime[0].EventsDate <= deviations[d].DevTo))
                    {
                        switch (deviations[d].DevType)
                        {
                            case 0:
                                arrayOfPeopleWorkTime[u].Deviation = "больничный";
                                break;
                            case 1:
                                arrayOfPeopleWorkTime[u].Deviation = "отпуск";
                                break;
                            case 2:
                                arrayOfPeopleWorkTime[u].Deviation = "командировка";
                                break;
                            case 3:
                                arrayOfPeopleWorkTime[u].Deviation = "удаленная работа";
                                break;
                            case 4:
                                arrayOfPeopleWorkTime[u].Deviation = "отгул";
                                break;
                        }
                        break;
                    }
                }

            }
            return arrayOfPeopleWorkTime;
        }




    }
}
