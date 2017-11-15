using LocalAngle.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalAngle.WindowsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            specialEventBindingSource.DataSource = SpecialEvent.SearchNear( new Postcode("IP3 9SJ"),15, OAuthCredentials.Default);
        }
    }
}
