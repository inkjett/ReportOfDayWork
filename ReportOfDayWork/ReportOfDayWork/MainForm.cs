using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportOfDayWork
{
    public partial class MainForm : Form
    {
        DataProcessing dataProcessing = new DataProcessing();
        public MainForm()
        {
            InitializeComponent();
            Variables.connectionSettings.Add(new ConnectionSettings("127.0.0.1", "C:\\GUARDE\\db\\guarde.fdb", "SYSDBA", "masterkey"));// добавляем стандартные настройки в массив с настройками 
            SaveSettings saveSettings = new SaveSettings();
            saveSettings.readerXML(); // читаем настройки из файла            
        }

        private void button1_Click(object sender, EventArgs e) // Кнопка - Загрузить данные из БД
        {
            Variables.ArrayOfUsers = dataProcessing.GetUsers(1); // загружаем массив пользователей
            Variables.ArrayOfDeviation = dataProcessing.GetDeviation(); // загружаем массив отсутствия не рабочем месте
        }

        private void button2_Click(object sender, EventArgs e) // Кнопка - Настройки подключения
        {
            ConnectionSettingsForm fr = new ConnectionSettingsForm();
            fr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Variables.ArrayOfDeviation;
           //dataProcessing.ShowInDataGrid(Variables.ArrayOfUsers,3);
        }
    }
}
