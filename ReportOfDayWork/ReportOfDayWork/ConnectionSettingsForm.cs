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
    public partial class ConnectionSettingsForm : Form
    {
        
        public ConnectionSettingsForm()
        {
            InitializeComponent();
            DatebBaseConnection connectionSettings = new DatebBaseConnection();
            textBox4.Text = Variables.connectionSettings[0].IP;
            textBox1.Text = Variables.connectionSettings[0].PathToDB;
            textBox2.Text = Variables.connectionSettings[0].User;
            textBox3.Text = Variables.connectionSettings[0].Password;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variables.connectionSettings[0].IP = textBox4.Text;
            Variables.connectionSettings[0].PathToDB = textBox1.Text;
            Variables.connectionSettings[0].User = textBox2.Text;
            Variables.connectionSettings[0].Password = textBox3.Text;
            SaveSettings saveSettings = new SaveSettings();
            saveSettings.writteXML();
            Close();
        }
    }
}
