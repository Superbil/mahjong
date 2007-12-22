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
    enum State
    {
        /// <summary>
        /// 北
        /// </summary>
        North = 0,
        /// <summary>
        /// 東
        /// </summary>
        East = 1,
        /// <summary>
        /// 南
        /// </summary>
        South = 2,
        /// <summary>
        /// 西
        /// </summary>
        West = 3,
        /// <summary>
        /// 桌面
        /// </summary>
        Table = 4
    }
    public partial class Table : Form
    {
        ProgramControl pc;
        private FlowLayoutPanel[] flowLayoutBrands;
        AllPlayers all;
        int width = Mahjong.Properties.Settings.Default.image_w;
        int height = Mahjong.Properties.Settings.Default.image_h;
        int padding = 2;
        Graphics graphics;

        public Table(ProgramControl pc,AllPlayers all)
        {
            InitializeComponent();
            this.flowLayoutBrands = new FlowLayoutPanel[5];
            this.pc = pc;
            this.all = all;
            setFlowLayout();
            graphics = this.CreateGraphics();            
        }
        private void setFlowLayout()
        {
            for (int i = 0; i < flowLayoutBrands.Length; i++)
                flowLayoutBrands[i] = new FlowLayoutPanel();
            this.Controls.AddRange(flowLayoutBrands);
            setFlowLayout_location(5);
            setFlowLayout_name();
            setFlowLayout_size();
            //setFlowLayout_Margin(10);
            //setFlowLayout_Dock();
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
            this.flowLayoutBrands[0].Size = new Size((width + padding * 2) * all.Dealnumber, height + padding * 2);
            this.flowLayoutBrands[1].Size = new Size(height + padding * 2, (width + padding * 2) * all.Dealnumber);
            this.flowLayoutBrands[2].Size = new Size((width + padding * 2) * all.Dealnumber, height + padding * 2);
            this.flowLayoutBrands[3].Size = new Size(height + padding * 2, (width + padding * 2) * all.Dealnumber);
            this.flowLayoutBrands[4].Size = new Size((width + padding * 2) * all.Dealnumber, (height + padding * 2) * all.sumBrands /all.Dealnumber);
        }
        private void setFlowLayout_location(int keepsize)
        {
            this.flowLayoutBrands[0].Location = new Point(keepsize * 2 + (height + padding * 2), keepsize);
            this.flowLayoutBrands[1].Location = new Point(keepsize * 3 + height + padding * 2 + (width + padding * 2) * all.Dealnumber, keepsize * 2 + height + padding * 2);
            this.flowLayoutBrands[2].Location = new Point(keepsize * 2 + (height + padding * 2), keepsize * 3 + (height + padding * 2) * (all.Dealnumber + 1));
            this.flowLayoutBrands[3].Location = new Point(keepsize, keepsize * 2 + height + padding * 2);
            this.flowLayoutBrands[4].Location = new Point(keepsize * 2 + (height + padding * 2), keepsize * 2 + (height + padding * 2));
        }
        void setFlowLayout_Dock()
        {
            this.flowLayoutBrands[0].Dock = DockStyle.Top;
            this.flowLayoutBrands[1].Dock = DockStyle.Right;
            this.flowLayoutBrands[2].Dock = DockStyle.Bottom;
            this.flowLayoutBrands[3].Dock = DockStyle.Left;
            this.flowLayoutBrands[4].Dock = DockStyle.None;            
        }
        private void setFlowLayout_Margin(int size)
        {
            for (int i = 0; i < flowLayoutBrands.Length;i++ )
                this.flowLayoutBrands[i].Margin = new Padding(size);
        }
        void addNouth(Image image)
        {
            //圖片旋轉180度
            addimage(State.North, image, RotateFlipType.Rotate180FlipNone);
        }
        void addEast(Image image)
        {
            //圖片旋轉270度
            addimage(State.East, image, RotateFlipType.Rotate270FlipNone);
        }
        void addSouth(Image image)
        {
            addimage(State.South, image, RotateFlipType.RotateNoneFlipNone);
        }
        void addWest(Image image)
        {
            //圖片旋轉90度
            addimage(State.West, image, RotateFlipType.Rotate90FlipNone);
        }
        void addTable(Image image)
        {
            addimage(State.Table, image, RotateFlipType.RotateNoneFlipNone);
        }
        private void addimage(State state,Image image,RotateFlipType rotate)
        {
            Bitmap bitmap = new Bitmap(image);
            PictureBox tempPicturebox = new PictureBox();
            // 設定自動縮放
            tempPicturebox.SizeMode = PictureBoxSizeMode.AutoSize;            

            // 設定邊距
            tempPicturebox.Margin = new Padding(padding);

            // 要轉的角度
            bitmap.RotateFlip(rotate);            
            tempPicturebox.Image = bitmap;
            if (state == State.South)
            {
                tempPicturebox.MouseHover += new EventHandler(pictureBox_MouseHover);
                tempPicturebox.MouseLeave += new EventHandler(pictureBox_MouseLeave);
            }

            this.flowLayoutBrands[(int)state].Controls.Add(tempPicturebox);
            this.Update();
        }
        void pictureBox_MouseHover(object sender, EventArgs e)
        {
            //MessageBox.Show("The method or operation is not implemented.");
            //PictureBox p = (PictureBox)sender;
            //int x = p.Location.X;
            //int y = p.Location.Y;
            //y += 20;
            //p.Location = new Point(x,y);
        }
        void pictureBox_MouseLeave(object sender, EventArgs e)
        {

        }
        public void updateImage()
        {
            for (int i = 0; i < all.Players[(int)State.North].getCount(); i++)
                addNouth(all.Players[0].getBrand(i).image);

            for (int i = 0; i < all.Players[(int)State.East].getCount(); i++)
                addEast(all.Players[1].getBrand(i).image);

            for (int i = 0; i < all.Players[(int)State.South].getCount(); i++)
                addSouth(all.Players[2].getBrand(i).image);

            for (int i = 0; i < all.Players[(int)State.West].getCount(); i++)
                addWest(all.Players[3].getBrand(i).image);            

            for (int i = 0; i < all.Table.getCount(); i++)
                addTable(all.Table.getBrand(i).image);
        }
        private void 新遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.newgame();
        }

        private void 開新遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.onlineGame();
        }

        private void 設定ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pc.config();
        }

        private void 關於ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pc.about();  
        }

        private void 結束ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pc.exit();
        }
    }
}