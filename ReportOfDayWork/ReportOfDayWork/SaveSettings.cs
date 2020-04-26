using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace ReportOfDayWork
{
    public class PropsFields
    {
        public string XMLFileName = Environment.CurrentDirectory + @"\settings.xml";
        public string IP = "";
        public string PathToDB = "";
        public string User = "";
        public string Password = "";
    }

    public class SaveSettings // класс работы с настройками
    {
        DatebBaseConnection connectionSettings = new DatebBaseConnection();        
        public PropsFields Fields;
        FileStream filetwich;
        public SaveSettings()
        {
            Fields = new PropsFields();
        }
        public void CopyItemsToSer() // копирование полей с настройками из текущих настроек подключения
        {
            Fields.IP = Variables.connectionSettings[0].IP;
            Fields.PathToDB = Variables.connectionSettings[0].PathToDB;
            Fields.User = Variables.connectionSettings[0].User;
            Fields.Password = Variables.connectionSettings[0].Password;
        }
        public void CopyItemsToProgramm() // копирование полей с настройками из текущих настроек подключения
        {
            Variables.connectionSettings[0].IP = Fields.IP;
            Variables.connectionSettings[0].PathToDB = Fields.PathToDB;
            Variables.connectionSettings[0].User = Fields.User;
            Variables.connectionSettings[0].Password = Fields.Password;
        }
        public void writteXML()
        {
            var props = new SaveSettings();
            CopyItemsToSer();
            XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
            TextWriter writer = new StreamWriter(Fields.XMLFileName);
            ser.Serialize(writer, Fields);
            writer.Close();
        }
        public void readerXML()
        {            
            if (File.Exists(Fields.XMLFileName))
            {
                XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
                TextReader reader = new StreamReader(Fields.XMLFileName);
                try
                {                    
                    Fields = ser.Deserialize(reader) as PropsFields;
                    CopyItemsToProgramm();
                    reader.Close();
                }
                catch // если при чтении настроек файла произошла ошибка пересоздаем файл 
                {
                    reader.Close();// закрываем работу с файлом 
                    FileInfo fileInf = new FileInfo(Fields.XMLFileName); // добавляем путь для удаления
                    fileInf.Delete(); // удаляем неправильный файл 
                    filetwich = File.Create(Fields.XMLFileName); // создаем новый
                    filetwich.Close();
                    writteXML();// генерируем настройки в новый файл 
                    // добавить генерацию сообщения
                }                
            }
            else
            {
                filetwich = File.Create(Fields.XMLFileName);
                filetwich.Close();                
                writteXML();   
            }
        }
    }
       
}
