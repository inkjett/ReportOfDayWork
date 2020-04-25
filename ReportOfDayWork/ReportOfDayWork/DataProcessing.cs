using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportOfDayWork
{
    class DataProcessing
    {
        public List<User> ArrayOfUsers;
        public List<User> GetUsers(Int16 DEPID)//метод формирования массива пользователей
        {
            List<User> arrayOfUsers = new List<User>();
            DatebBaseConnection ConnectToDB = new DatebBaseConnection();
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
    }
}
