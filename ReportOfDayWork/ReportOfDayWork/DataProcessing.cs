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


        public List<Deviation> GetDeviation()
        {
            List<Deviation> arrayOfDeviation = new List<Deviation>();
            
            try
            {
                ConnectToDB = new DatebBaseConnection();
                ConnectToDB.fbData = ConnectToDB.ReadData("SELECT deviation.peopleid, deviation.devtype, deviation.devfrom,deviation.devto,deviation.deviationid from deviation");
                while (ConnectToDB.fbData.Read()) //пока не прочли все данные выполняем... //select_result = select_result + reader.GetInt32(0 ).ToString() + ", " + reader.GetString(1) + "\n";
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











    }
}
