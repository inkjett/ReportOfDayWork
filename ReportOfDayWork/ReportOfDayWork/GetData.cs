using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportOfDayWork
{
    class GetData
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
        //-------------

        public List<Deviation> GetDeviation() // метод получения данные о отсутсвии на рабочем месте
        {
            List<Deviation> arrayOfDeviation = new List<Deviation>();            
            try
            {
                ConnectToDB = new DatebBaseConnection();
                ConnectToDB.fbData = ConnectToDB.ReadData("SELECT deviation.peopleid, deviation.devtype, deviation.devfrom,deviation.devto,deviation.deviationid from deviation");
                while (ConnectToDB.fbData.Read()) //пока не прочли все данные выполняем.
                {
                    arrayOfDeviation.Add(new Deviation(uint.Parse(ConnectToDB.fbData.GetString(0)), uint.Parse(ConnectToDB.fbData.GetString(1)),Convert.ToDateTime(ConnectToDB.fbData.GetString(2)), Convert.ToDateTime(ConnectToDB.fbData.GetString(3)), uint.Parse(ConnectToDB.fbData.GetString(4))));
                }
            }
            catch (Exception y)
            {
                //MessageBox.Show(y.Message, "Сообщение", MessageBoxButtons.OK);
            }
            return arrayOfDeviation;
        }
        //-------------

        public List<WorkTime> GetPeopleWorkTime(string dateStart, string dateEnd, Int16 DEPID) // метод получения данных о нахождении на рабочем месте
        {
            List<WorkTime> arrayOfWorkTime = new List<WorkTime>();            
            try
            {
                ConnectToDB = new DatebBaseConnection();
                ConnectToDB.fbData = ConnectToDB.ReadData("SELECT DISTINCT events.eventsdate AS events_eventsdate,events.cardnum AS events_cardnum, events.readerid AS events_readerid, cards.peopleid AS cards_peopleid FROM events INNER JOIN cards on events.cardnum = cards.cardnum INNER JOIN people on people.peopleid = cards.peopleid WHERE events.eventsdate >= '" + dateStart + " 00:00:00' AND events.eventsdate <= '"+ dateEnd + " 23:59:59' AND people.depid =" + DEPID.ToString());
                while (ConnectToDB.fbData.Read()) //пока не прочли все данные выполняем.
                {
                    arrayOfWorkTime.Add(new WorkTime(DateTime.Parse(ConnectToDB.fbData.GetString(0)), uint.Parse(ConnectToDB.fbData.GetString(1)), uint.Parse(ConnectToDB.fbData.GetString(2)), uint.Parse(ConnectToDB.fbData.GetString(3))));
                }
            }
            catch (Exception y)
            {
                //MessageBox.Show(y.Message, "Сообщение", MessageBoxButtons.OK);
            }                                                               
            return arrayOfWorkTime;
        }
        //-------------

    }
}
