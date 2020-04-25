using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace ReportOfDayWork
{
    class SaveSettings
    {

        public partial class PropsFields
        {
            public string XMLFileName = Environment.CurrentDirectory + @"\settings.xml";
            public string IP = "";
            public string pathToDB = "";
            public string User = "";
            public string Password = "";
        }
        public partial class Props // класс работы с настройками
        {
            DatebBaseConnection connectionSettings = new DatebBaseConnection();
            public PropsFields Fields;
            public Props()
            {
                Fields = new PropsFields();
            }
            public void CopyItemsToSer() // копирование полей с настройками из текущих настроек подключения
            {
                Fields.IP = connectionSettings.IP;
                Fields.pathToDB = connectionSettings.pathToDB;
                Fields.User = connectionSettings.User;
                Fields.Password = connectionSettings.Password;
            }
            public void CopyItemsToProgramm() // копирование полей с настройками из текущих настроек подключения
            {
                connectionSettings.IP = Fields.IP;
                connectionSettings.pathToDB = Fields.pathToDB;
                connectionSettings.User = Fields.User;
                connectionSettings.Password = Fields.Password;
            }
            public void writteXML()
            {
                var props = new Props();
                CopyItemsToSer();
                XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
                TextWriter writer = new StreamWriter(Fields.XMLFileName);
                ser.Serialize(writer, Fields);
                writer.Close();
            }
            public void readerXML()
            {
                var props = new Props();
                if (File.Exists(Fields.XMLFileName))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
                    TextReader reader = new StreamReader(Fields.XMLFileName);
                    Fields = ser.Deserialize(reader) as PropsFields;
                    CopyItemsToProgramm();
                }
                else
                {
                    File.Create(Fields.XMLFileName);
                    writteXML();
                }

            }
        }







    }
}
