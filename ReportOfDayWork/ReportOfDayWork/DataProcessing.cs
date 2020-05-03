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
        public List<User> ArrayOfUsers;
        DatebBaseConnection ConnectToDB = new DatebBaseConnection();

        public List<User> GetUsers(Int16 DEPID)//метод формирования массива пользователей
        {
            List<User> arrayOfUsers = new List<User>();            
            try
            {
                ConnectToDB = new DatebBaseConnection();
                ConnectToDB.fbData = ConnectToDB.ReadData("SELECT people.lname||' '||people.fname||' '||people.sname, people.peopleid,cards.cardnum, people.depid FROM cards INNER JOIN people ON(people.peopleid = CARDS.peopleid) where (people.depid != 29) AND (people.depid =" + DEPID.ToString() + ")");               
                while (ConnectToDB.fbData.Read()) //пока не прочли все данные выполняем.
                {
                    arrayOfUsers.Add(new User(ConnectToDB.fbData.GetString(0).ToString(), uint.Parse(ConnectToDB.fbData.GetString(1)), uint.Parse(ConnectToDB.fbData.GetString(2))));
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message, "Сообщение", MessageBoxButtons.OK);
            }
            return arrayOfUsers;
        }


        public List<Deviation> GetDeviation() // метод получения данные о отсутсвии на рабочем месте
        {
            List<Deviation> arrayOfDeviation = new List<Deviation>();            
            try
            {
                ConnectToDB = new DatebBaseConnection();
                ConnectToDB.fbData = ConnectToDB.ReadData("SELECT deviation.peopleid, deviation.devtype, deviation.devfrom,deviation.devto,deviation.deviationid from deviation");
                while (ConnectToDB.fbData.Read()) //пока не прочли все данные выполняем.
                {
                    arrayOfDeviation.Add(new Deviation(uint.Parse(ConnectToDB.fbData.GetString(0)), uint.Parse(ConnectToDB.fbData.GetString(1)), ConnectToDB.fbData.GetString(2), ConnectToDB.fbData.GetString(2), uint.Parse(ConnectToDB.fbData.GetString(4))));
                }
            }
            catch (Exception y)
            {
                //MessageBox.Show(y.Message, "Сообщение", MessageBoxButtons.OK);
            }
            return arrayOfDeviation;
        }


        public List<PeopleWorkTime> GetPeopleWorkTime(string Date, Int16 DEPID) // метод получения данные о отсутсвии на рабочем месте
        {
            List<WorkTime> arrayOfWorkTime = new List<WorkTime>();
            List<PeopleWorkTime> arrayOfPeopleWorkTime = new List<PeopleWorkTime>();
            try
            {
                ConnectToDB = new DatebBaseConnection();
                ConnectToDB.fbData = ConnectToDB.ReadData("SELECT DISTINCT events.eventsdate AS events_eventsdate,events.cardnum AS events_cardnum, events.readerid AS events_readerid, cards.peopleid AS cards_peopleid, people.lname||' '||people.fname||' '||people.sname AS FIO FROM events INNER JOIN cards on events.cardnum = cards.cardnum INNER JOIN people on people.peopleid = cards.peopleid WHERE events.eventsdate >= '" + Date + " 00:00:00' AND events.eventsdate <= '"+ Date + " 23:59:59' AND people.depid =" + DEPID.ToString());
                while (ConnectToDB.fbData.Read()) //пока не прочли все данные выполняем.
                {
                    arrayOfWorkTime.Add(new WorkTime(DateTime.Parse(ConnectToDB.fbData.GetString(0)), uint.Parse(ConnectToDB.fbData.GetString(1)), uint.Parse(ConnectToDB.fbData.GetString(2)), uint.Parse(ConnectToDB.fbData.GetString(3)), ConnectToDB.fbData.GetString(4)));
                }
                for (var i = 0; i < arrayOfWorkTime.Count; i++)// цикл поиска по времени прихода и ухода
                {
                    if (arrayOfWorkTime[i].ReaderId == 3)// проверка прихода на работу 3-вход 13-выход
                    {
                        arrayOfPeopleWorkTime.Add(new PeopleWorkTime(arrayOfWorkTime[i].FullName, arrayOfWorkTime[i].EventsDate.ToLongTimeString(), null, null, null)); //Добавляем строку с пользователем                    
                        for (var j = 0; j < arrayOfWorkTime.Count; j++)// ищем выход с работы
                        {
                            if ((arrayOfWorkTime[i].PeopleId == arrayOfWorkTime[j].PeopleId) && (arrayOfWorkTime[i].EventsDate < arrayOfWorkTime[j].EventsDate) && (arrayOfWorkTime[j].ReaderId == 3)) // удаляем лишние временные метки между приходом на работу и уходом с работы
                            {
                                arrayOfWorkTime.RemoveAt(j);
                                j--;
                            }
                            else if ((arrayOfWorkTime[i].PeopleId == arrayOfWorkTime[j].PeopleId) && (arrayOfWorkTime[j].ReaderId == 13)) // время выхода с работы
                            {
                                arrayOfPeopleWorkTime[arrayOfPeopleWorkTime.Count - 1].LeavingWork = arrayOfWorkTime[j].EventsDate.ToLongTimeString();// время ухода с работы
                                arrayOfPeopleWorkTime[arrayOfPeopleWorkTime.Count - 1].BeingAtWork = Convert.ToString( (arrayOfWorkTime[j].EventsDate - arrayOfWorkTime[i].EventsDate) < Variables.halfDay?(arrayOfWorkTime[j].EventsDate - arrayOfWorkTime[i].EventsDate):(arrayOfWorkTime[j].EventsDate - arrayOfWorkTime[i].EventsDate - Variables.oneHour)); // время проведенное на работе
                            }
                        }
                    }

                }
            }
            catch (Exception y)
            {
                //MessageBox.Show(y.Message, "Сообщение", MessageBoxButtons.OK);
            }                                                               
            return arrayOfPeopleWorkTime;
        }
    }
}
