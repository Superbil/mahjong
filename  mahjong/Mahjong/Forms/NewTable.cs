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

        private void 結束ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 新遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.newgame();
        }

        private void 儲存牌局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.savegame();
        }

        private void 讀取牌局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.loadgame();
        }

        private void 新增網路遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 加入網路遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 慢速ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Slow.Checked = true;
            Normal.Checked = false;
            Quick.Checked = false;
            pc.SetDealyTime = Settings.Default.RunRoundTime_Slow;
        }

        private void 正常ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Slow.Checked = false;
            Normal.Checked = true;
            Quick.Checked = false;
            pc.SetDealyTime = Settings.Default.RunRoundTime_Normal;
        }

        private void 超快速ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Slow.Checked = false;
            Normal.Checked = false;
            Quick.Checked = true;
            pc.SetDealyTime = Settings.Default.RunRoundTime_Quick;
        }

        private void 關於這個麻將遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.about();
        }
    }
}