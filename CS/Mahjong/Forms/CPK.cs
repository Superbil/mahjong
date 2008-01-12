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
    public partial class CPK : Form
    {
        ProgramControl pc;
        public CPK(ProgramControl pc)
        {
            InitializeComponent();
            this.pc = pc;
        }

        private void CPK_Load(object sender, EventArgs e)
        {
            Chow.Text = Mahjong.Properties.Settings.Default.Chow;
            Pong.Text = Mahjong.Properties.Settings.Default.Pong;
            Kong.Text = Mahjong.Properties.Settings.Default.Kong;
            Win.Text = Mahjong.Properties.Settings.Default.Win;
        }
        /// <summary>
        /// ³]©w«ö¶sª¬ºA
        /// </summary>
        /// <param name="chow">¦Y«ö¶s</param>
        /// <param name="pong">¸I«ö¶s</param>
        /// <param name="kong">ºb«ö¶s</param>
        /// <param name="win">­J«ö¶s</param>
        public void Enabled_Button(bool chow,bool pong,bool kong,bool win)
        {
            this.Chow.Enabled = chow;
            this.Pong.Enabled = pong;
            this.Kong.Enabled = kong;
            this.Win.Enabled = win;
        }

        private void Chow_Click(object sender, EventArgs e)
        {
            pc.chow();
            this.Close();
        }

        private void Pong_Click(object sender, EventArgs e)
        {
            pc.pong();
            this.Close();
        }

        private void Kong_Click(object sender, EventArgs e)
        {
            pc.kong();
            this.Close();
        }

        private void Win_Click(object sender, EventArgs e)
        {
            pc.win();
            this.Close();
        }
    }
}