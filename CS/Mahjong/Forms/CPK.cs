using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Mahjong.Forms
{
    public partial class CPK : Form
    {
        public CPK()
        {
            InitializeComponent();
        }

        private void CPK_Load(object sender, EventArgs e)
        {
            Chow.Text = Mahjong.Properties.Settings.Default.Chow;
            Pong.Text = Mahjong.Properties.Settings.Default.Pong;
            Kong.Text = Mahjong.Properties.Settings.Default.Kong;
            Win.Text = Mahjong.Properties.Settings.Default.Win;
        }

        private void Chow_Click(object sender, EventArgs e)
        {

        }

        private void Pong_Click(object sender, EventArgs e)
        {

        }

        private void Kong_Click(object sender, EventArgs e)
        {

        }

        private void Win_Click(object sender, EventArgs e)
        {

        }
    }
}