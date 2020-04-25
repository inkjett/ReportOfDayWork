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
        DataProcessing DataProc = new DataProcessing();
        public MainForm()
        {
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e) // Кнопка - Загрузить данные из БД
        {
            DataProc.ArrayOfUsers = DataProc.GetUsers(1);
        }

        private void button2_Click(object sender, EventArgs e) // Кнопка - Настройки подключения
        {
            ConnectionSettings fr = new ConnectionSettings();
            fr.Show();
        }
    }
}
