using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LocalAngle.Windows;

namespace LocalAngle.Editor.Windows
{
    public partial class MdiMain : Form
    {
        public MdiMain()
        {
            InitializeComponent();
        }

        private void eventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new EventEditor();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
