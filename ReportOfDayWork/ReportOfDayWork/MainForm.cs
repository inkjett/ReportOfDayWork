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
            Variables.connectionSettings.Add(new ConnectionSettings("127.0.0.1", "C:\\GUARDE\\db\\guarde.fdb", "SYSDBA", "masterkey"));
            SaveSettings saveSettings = new SaveSettings();
            saveSettings.readerXML();
            
        }

        private void button1_Click(object sender, EventArgs e) // Кнопка - Загрузить данные из БД
        {
            DataProc.ArrayOfUsers = DataProc.GetUsers(1);
        }

        private void button2_Click(object sender, EventArgs e) // Кнопка - Настройки подключения
        {
            ConnectionSettingsForm fr = new ConnectionSettingsForm();
            fr.Show();
        }
    }
}
