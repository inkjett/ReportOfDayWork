using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace ReportOfDayWork
{
    class DatebBaseConnection
    {
        public string IP { get; set; } = "127.0.0.1";
        public string pathToDB { get; set; } = "C:\\GUARDE\\db\\guarde.fdb";
        public string User { get; set; } = "SYSDBA";
        public string Password { get; set; } = "masterkey";
        public FbConnection fb;
        public FbDataReader fbData;

        public FbDataReader ReadData(string queryString)// метод вычитывания данных из бд 
        {
            string connectingString = "character set = WIN1251; initial catalog = " + IP + ":" + @"" + pathToDB + "; user id = " + User + "; password = " + Password + "; ";
            fb = new FbConnection(connectingString); // записываем строку соединения
            if (fb.State != ConnectionState.Open)
            {                
                try
                {                  
                    fb.Open(); // подключаемся к БД
                }
                catch (Exception e)
                {
                    //SQL.MessageHelper.GetInstance().SetMessage(e.Message);
                }
            }
            FbTransaction fbt = fb.BeginTransaction(); //  начинаем транзакцию данных из БД
            FbCommand SelectSQL = new FbCommand(queryString, fb); //запрос
            SelectSQL.Transaction = fbt;
            FbDataReader reader = SelectSQL.ExecuteReader();
            //SelectSQL.Dispose(); //в документации написано, что ОЧЕНЬ рекомендуется убивать объекты этого типа, если они больше не нужны
            return reader;
        }
    }




}

