using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mahjong.Forms;
using Mahjong.Control;
using Mahjong.Players;
using Mahjong.Brands;
using Mahjong.Properties;

namespace Mahjong.Forms
{
    public partial class NewTable : Table
    {
        public NewTable()
        {
            InitializeComponent();
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void �s�C��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.newgame();
        }

        private void �x�s�P��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.savegame();
        }

        private void Ū���P��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.loadgame();
        }

        private void �s�W�����C��ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void �[�J�����C��ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void �C�tToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Slow.Checked = true;
            Normal.Checked = false;
            Quick.Checked = false;
            pc.SetDealyTime = Settings.Default.RunRoundTime_Slow;
        }

        private void ���`ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Slow.Checked = false;
            Normal.Checked = true;
            Quick.Checked = false;
            pc.SetDealyTime = Settings.Default.RunRoundTime_Normal;
        }

        private void �W�ֳtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Slow.Checked = false;
            Normal.Checked = false;
            Quick.Checked = true;
            pc.SetDealyTime = Settings.Default.RunRoundTime_Quick;
        }

        private void ����o�ӳ±N�C��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.about();
        }
    }
}