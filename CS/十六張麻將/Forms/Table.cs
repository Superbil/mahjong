using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mahjong.Forms;
using Mahjong.Control;

namespace Mahjong.Forms
{
    public partial class Table : Form
    {
        ProgramControl pc;
        //private System.Windows.Forms.PictureBox [,] pictureBox;
        //private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        
        public Table()
        {
            InitializeComponent();
            pc = new ProgramControl();
            
            //this.flowLayoutPanel1.Location = new System.Drawing.Point(83, 498);
            //this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            //this.flowLayoutPanel1.Size = new System.Drawing.Size(577, 47);
            //this.flowLayoutPanel1.TabIndex = 2;
            //pc.Controls = this.Controls;
        }
        
        private void 關於ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.about();            
        }

        private void 結束ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.exit();
        }

        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.config();
        }

        private void 開新局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.newgame();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pc.help();
        }
    }
}