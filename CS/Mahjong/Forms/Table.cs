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

namespace Mahjong.Forms
{
    public partial class Table : Form
    {
        ProgramControl pc;
        private FlowLayoutPanel[] flowLayoutBrands;
        AllPlayers all;
        int width = Mahjong.Properties.Settings.Default.image_w;
        int height = Mahjong.Properties.Settings.Default.image_h;
        public Table(ProgramControl pc,AllPlayers all)
        {
            InitializeComponent();
            this.flowLayoutBrands = new FlowLayoutPanel[5];
            this.pc = pc;
            this.all = all;
            for (int i=0; i < 5; i++)
            {
                flowLayoutBrands[i] = new FlowLayoutPanel();                
                this.Controls.Add(flowLayoutBrands[i]);
                //flowLayoutBrands[i].BackColor = System.Drawing.Color.Yellow;
            }

            setFlowLayout_location(10);
            setFlowLayout_name();
            setFlowLayout_size();
            //flowLayoutBrands[4].BackColor = Color.Blue;
            width = width + 15;
            height = height + 15;
        }
        private void setFlowLayout_name()
        {
            this.flowLayoutBrands[0].Name = Mahjong.Properties.Settings.Default.Nouth;
            this.flowLayoutBrands[1].Name = Mahjong.Properties.Settings.Default.East;
            this.flowLayoutBrands[2].Name = Mahjong.Properties.Settings.Default.South;
            this.flowLayoutBrands[3].Name = Mahjong.Properties.Settings.Default.West;
            this.flowLayoutBrands[4].Name = Mahjong.Properties.Settings.Default.Table;
        }
        private void setFlowLayout_size()
        {
            this.flowLayoutBrands[0].Size = new Size(width * all.Dealnumber, height);
            this.flowLayoutBrands[1].Size = new Size(width+10, height * all.Dealnumber);
            this.flowLayoutBrands[2].Size = new Size(width * all.Dealnumber, height);
            this.flowLayoutBrands[3].Size = new Size(width+10, height * all.Dealnumber);
            this.flowLayoutBrands[4].Size = new Size(width * all.Dealnumber, height * all.Dealnumber);
        }
        private void setFlowLayout_location(int keepsize)
        {
            int w=0, h=20;
            this.flowLayoutBrands[0].Location = new Point(keepsize * 2 + width+w, keepsize+h);
            this.flowLayoutBrands[1].Location = new Point(keepsize * 3 + width * (all.Dealnumber + 1)+w, keepsize * 2 + height+h);
            this.flowLayoutBrands[2].Location = new Point(keepsize * 2 + width+w, keepsize * 3 + height * (all.Dealnumber+1) +h);
            this.flowLayoutBrands[3].Location = new Point(keepsize+w, keepsize * 2 + height+h);
            this.flowLayoutBrands[4].Location = new Point(keepsize * 2+w + width, keepsize * 2 + height+h);
        }
        void addNouth(Image image)
        {
            //圖片旋轉180度
            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            PictureBox tempPicturebox = new PictureBox();
            tempPicturebox.Size = new Size(image.Width,image.Height);
            tempPicturebox.Image = image;
            tempPicturebox.BackColor = Color.Blue;
            this.flowLayoutBrands[0].Controls.Add(tempPicturebox);
        }
        void addEast(Image image)
        {
            //圖片旋轉270度
            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            PictureBox tempPicturebox = new PictureBox();
            tempPicturebox.Size = new Size(image.Height,image.Width);
            tempPicturebox.Image = image;
            tempPicturebox.BackColor = Color.Blue;
            this.flowLayoutBrands[1].Controls.Add(tempPicturebox);
        }
        void addSouth(Image image)
        {
            PictureBox tempPicturebox = new PictureBox();
            tempPicturebox.Size = new Size(image.Width, image.Height);
            tempPicturebox.Image = image;
            this.flowLayoutBrands[2].Controls.Add(tempPicturebox);
        }
        void addWest(Image image)
        {
            //圖片旋轉90度
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            PictureBox tempPicturebox = new PictureBox();
            tempPicturebox.Size = new Size(image.Height,image.Width);
            
            tempPicturebox.Image = image;
            this.flowLayoutBrands[3].Controls.Add(tempPicturebox);
        }
        private void addTable(Image image)
        {
            image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
            PictureBox tempPicturebox = new PictureBox();
            tempPicturebox.Size = new Size(image.Width, image.Height);
            tempPicturebox.Image = image;
            this.flowLayoutBrands[4].Controls.Add(tempPicturebox);
        }
        public void updateImage()
        {
            for (int i = 0; i < all.Players[0].getCount(); i++)
                addNouth(all.Players[0].getBrand(i).image);

            for (int i = 0; i < all.Players[1].getCount(); i++)
                addEast(all.Players[1].getBrand(i).image);

            for (int i = 0; i < all.Players[2].getCount(); i++)
                addSouth(all.Players[2].getBrand(i).image);

            for (int i = 0; i < all.Players[3].getCount(); i++)
                addWest(all.Players[3].getBrand(i).image);

            for (int i = 0; i < all.Table.getCount(); i++)
                addTable(all.Table.getBrand(i).image);            
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

        private void 開新伺服器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.onlineGame();
        }
    }
}