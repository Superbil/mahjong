using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Mahjong.Forms
{
    public partial class Config : Form
    {
        public Config(Table table)
        {
            InitializeComponent();
            this.panel_color.BackColor = table.BackColor;
        }

        private void button_setup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel_color_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}