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
            //Variables.ArrayOfPeopleWorkTimeDay = dataProcessing.PeopleWorkTimeDay(Variables.ArrayOfUsers, Variables.ArrayOfWorkTime, Variables.ArrayOfDeviation);
            Variables.ArrayOfPeopleWorkTimeMonth = dataProcessing.PeopleWorkTimeMonth(Variables.ArrayOfUsers, Variables.ArrayOfWorkTime, Variables.ArrayOfDeviation).ToList();

        }

        private void button2_Click(object sender, EventArgs e) // Кнопка - Настройки подключения
        {
            ConnectionSettingsForm fr = new ConnectionSettingsForm();
            fr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = Variables.ArrayOfPeopleWorkTimeMonth;
            dataGridView1.RowCount = Variables.ArrayOfPeopleWorkTimeMonth.Count;
            dataGridView1.ColumnCount = Variables.ArrayOfPeopleWorkTimeMonth.FirstOrDefault().WorkedTimeDaysInMonth.Count + 1;
            for (var i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                for (var j = 0; j < dataGridView1.ColumnCount -1; j++)
                {
                    if (j == 0)
                    {
                        dataGridView1.Columns[j].HeaderCell.Value = "ФИО";
                        dataGridView1.Rows[i].Cells[j].Value = Variables.ArrayOfPeopleWorkTimeMonth[i].FullName;
                    }
                    else
                    {
                        dataGridView1.Columns[j].HeaderCell.Value = j.ToString();
                        dataGridView1.Rows[i].Cells[j].Value = Variables.ArrayOfPeopleWorkTimeMonth[i].WorkedTimeDaysInMonth[j].WorkedTime;
                    }
                }
            }
            /*dataGridView1.DataSource = Variables.ArrayOfPeopleWorkTimeDay;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.Columns[0].Width = 180;
            dataGridView1.Columns[1].Width = 90;
            dataGridView1.Columns[2].Width = 90;
            dataGridView1.Columns[3].Width = 90;
            dataGridView1.Columns[4].Width = 90;
            //dataProcessing.ShowInDataGrid(Variables.ArrayOfUsers,3);*/
        }
    }
}
