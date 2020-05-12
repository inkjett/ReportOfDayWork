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
                for (var w = 0; w < arrayOfWorkTime.Count; w++) // идем по массиву с данными об отработаном времени 
                {
                    if ((Variables.inOffice.ToList().IndexOf(arrayOfWorkTime[w].ReaderId) != -1) && (arrayOfuser[u].Сardnum == arrayOfWorkTime[w].Cardnum))// проверка прихода на работу денные о считывателях берем из массива inOffice
                    {                        
                        arrayOfPeopleWorkTime[u+1].ComingToWork = arrayOfWorkTime[w].EventsDate.ToLongTimeString();// добавляем время прихода на работу     
                        for (var j = 0; j < arrayOfWorkTime.Count; j++)// ищем выход с работы
                        {
                            if ((arrayOfWorkTime[w].PeopleId == arrayOfWorkTime[j].PeopleId) && (arrayOfWorkTime[w].EventsDate < arrayOfWorkTime[j].EventsDate) && (Variables.inOffice.ToList().IndexOf(arrayOfWorkTime[j].ReaderId) != -1)) // удаляем лишние временные метки между приходом на работу и уходом с работы
                            {
                                arrayOfWorkTime.RemoveAt(j);
                                j--;
                            }
                            else if ((arrayOfWorkTime[w].PeopleId == arrayOfWorkTime[j].PeopleId) && (Variables.outOffice.ToList().IndexOf(arrayOfWorkTime[j].ReaderId) != -1)) // время выхода с работы данные о считывателях берем из массива outOffice
                            {
                                arrayOfPeopleWorkTime[arrayOfPeopleWorkTime.Count - 1].LeavingWork = arrayOfWorkTime[j].EventsDate.ToLongTimeString();// время ухода с работы
                                arrayOfPeopleWorkTime[arrayOfPeopleWorkTime.Count - 1].BeingAtWork = (arrayOfWorkTime[j].EventsDate - arrayOfWorkTime[w].EventsDate) < Variables.halfDay ? (arrayOfWorkTime[j].EventsDate - arrayOfWorkTime[w].EventsDate).ToString() : (arrayOfWorkTime[j].EventsDate - arrayOfWorkTime[w].EventsDate - Variables.oneHour).ToString(); // вычисление времени проведенного на работе
                            }
                        }
                    }
                }
                for (var d = 0; d < deviations.Count; d++)//ищем не находится ли данный пользователь в отпуске итд
                {
                    if ((arrayOfuser[u].Id == deviations[d].PeopleId) && (deviations[d].DevFrom <= arrayOfWorkTime[0].EventsDate) && (arrayOfWorkTime[0].EventsDate <= deviations[d].DevTo)) // проверяем на id прользователя и временные рамки
                    {
                        arrayOfPeopleWorkTime[u+1].Deviation = Variables.deviationName.Select((i,index)=> new {i, index }).Where(n=>n.index == deviations[d].DevType).ToList()[0].i; // проходим по массиву данных об отсутствии на рабочем месте
                        break;
                    }
                }
            }
            return arrayOfPeopleWorkTime;
        }
        //---------


        public DataGridView DataToGrid(List<PeopleWorkTimeDay> peopleWorkTimeDay)
        {
            DataGridView dataGridView = new DataGridView();

            dataGridView.RowCount = peopleWorkTimeDay.Count;
            dataGridView.ColumnCount = 5;
            dataGridView.Columns[0].Visible = true;
            dataGridView.Columns[0].Width = 180;
            dataGridView.Columns[1].Width = 60;
            dataGridView.Columns[2].Width = 60;
            dataGridView.Columns[3].Width = 60;
            dataGridView.Columns[4].Width = 60;


            for (int i = 0; i < peopleWorkTimeDay.Count; i++)
            {
                dataGridView.Rows[i].Cells[0].Value = String.Format("{0}", peopleWorkTimeDay[i].FullName);
                dataGridView.Rows[i].Cells[1].Value = String.Format("{0}", peopleWorkTimeDay[i].ComingToWork);
            }

            return dataGridView;
        }




    }
}
