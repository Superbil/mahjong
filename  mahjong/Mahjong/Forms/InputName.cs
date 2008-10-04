using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mahjong.Control;

namespace Mahjong.Forms
{
    public partial class InputName : Form
    {
        public InputName()
        {
            InitializeComponent();
        }
        // Temp AllPlayers
        AllPlayers all;
        public AllPlayers allplayers
        {
            set
            {
                all = value;
            }

            get
            {
                return all;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_N.Text != "")
                all.Name[0] = textBox_N.Text;
            if (textBox_E.Text != "")
                all.Name[1] = textBox_E.Text;
            if (textBox_S.Text != "")
                all.Name[2] = textBox_S.Text;
            if (textBox_W.Text != "")
                all.Name[3] = textBox_W.Text;
            this.Close();
        }
    }
}