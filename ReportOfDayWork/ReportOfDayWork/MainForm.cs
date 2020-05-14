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
        GetData getData = new GetData();
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
            Variables.ArrayOfUsers = getData.GetUsers(1); // загружаем массив пользователей, 
            Variables.ArrayOfDeviation = getData.GetDeviation(); // загружаем массив отсутствия не рабочем месте
            Variables.ArrayOfWorkTime = getData.GetPeopleWorkTime("04.02.2020", "04.02.2020", 1); // получение данных об отработанном времени за период  + выбор департамента
            Variables.ArrayOfPeopleWorkTimeDay = dataProcessing.PeopleWorkTimeDay(Variables.ArrayOfUsers, Variables.ArrayOfWorkTime, Variables.ArrayOfDeviation);
            //Variables.ArrayOfPeopleWorkTimeMonth = dataProcessing.PeopleWorkTimeMonth(Variables.ArrayOfUsers, Variables.ArrayOfWorkTime, Variables.ArrayOfDeviation);

        }

        private void button2_Click(object sender, EventArgs e) // Кнопка - Настройки подключения
        {
            ConnectionSettingsForm fr = new ConnectionSettingsForm();
            fr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {            
            dataGridView1.DataSource = Variables.ArrayOfPeopleWorkTimeDay;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.Columns[0].Width = 180;
            dataGridView1.Columns[1].Width = 90;
            dataGridView1.Columns[2].Width = 90;
            dataGridView1.Columns[3].Width = 90;
            dataGridView1.Columns[4].Width = 90;
            //dataProcessing.ShowInDataGrid(Variables.ArrayOfUsers,3);
        }
    }
}
