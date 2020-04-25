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
    public partial class ConnectionSettings : Form
    {
        DatebBaseConnection connectionSettings = new DatebBaseConnection();
        public ConnectionSettings()
        {
            InitializeComponent();
            textBox4.Text = connectionSettings.IP;
            textBox1.Text = connectionSettings.pathToDB;
            textBox2.Text = connectionSettings.User;
            textBox3.Text = connectionSettings.Password;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSettings.Props saveSettings = new SaveSettings.Props();
            saveSettings.writteXML();
        }
    }
}
