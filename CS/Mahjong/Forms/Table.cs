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
        int padding = 1;
        const bool DebugMode = true;
        
        public Table(ProgramControl pc)
        {
            InitializeComponent();
            this.flowLayoutBrands = new FlowLayoutPanel[5];
            this.pc = pc;                       
        }
        public void Setup(AllPlayers all)
        {
            this.all = all;
            setFlowLayout();
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
        void addNouth()
        {
            //圖片旋轉180度
            for (int i = 0; i < all.Players[(int)State.North].getCount(); i++)
                if ( all.Players[(int)State.North].getBrand(i).IsCanSee || DebugMode)
                    addimage(State.North, all.Players[(int)State.North].getBrand(i), RotateFlipType.Rotate180FlipNone);
        }
        void addEast()
        {
            //圖片旋轉270度
            for (int i = 0; i < all.Players[(int)State.East].getCount(); i++)
                if (all.Players[(int)State.East].getBrand(i).IsCanSee || DebugMode)
                    addimage(State.East, all.Players[(int)State.East].getBrand(i), RotateFlipType.Rotate270FlipNone);
        }
        void addSouth()
        {
            for (int i = 0; i < all.Players[(int)State.South].getCount(); i++)
                if (all.Players[(int)State.South].getBrand(i).IsCanSee || DebugMode)
                    addimage(State.South, all.Players[(int)State.East].getBrand(i), RotateFlipType.RotateNoneFlipNone);
        }
        void addWest()
        {
            //圖片旋轉90度
            for (int i = 0; i < all.Players[(int)State.West].getCount(); i++)
                if (all.Players[(int)State.West].getBrand(i).IsCanSee || DebugMode)
                    addimage(State.West, all.Players[(int)State.West].getBrand(i), RotateFlipType.Rotate90FlipNone);
        }
        void addTable()
        {
            for (int i = 0; i < all.Table.getCount(); i++)
                if (all.Table.getBrand(i).IsCanSee || DebugMode)
                    addimage(State.Table, all.Table.getBrand(i), RotateFlipType.RotateNoneFlipNone);
        }
        private void addimage(State state,Brand brand,RotateFlipType rotate)
        {
            Bitmap bitmap = new Bitmap(brand.image);
            // 設定牌
            BrandBox tempBrandbox = new BrandBox(brand);            
            //tempBrandbox.brand = brand;

            // 設定自動縮放
            tempBrandbox.SizeMode = PictureBoxSizeMode.AutoSize;            

            // 設定邊距            
            if (state == State.South)
                tempBrandbox.Margin = new Padding(padding);
            else
                tempBrandbox.Margin = new Padding(padding);

            // 要轉的角度
            bitmap.RotateFlip(rotate);

            // 滑鼠事件
            if (state == State.South)
            {
                tempBrandbox.MouseHover += new EventHandler(brandBox_MouseHover);
                tempBrandbox.MouseLeave += new EventHandler(brandBox_MouseLeave);
                tempBrandbox.Click += new EventHandler(brandBox_MouseClick);
            }
            else
                bitmap = ResizeBitmap(bitmap,0.8);
                        
            tempBrandbox.Image = bitmap;
            
            this.flowLayoutBrands[(int)state].Controls.Add(tempBrandbox);
            this.Update();
        }
        private Bitmap ResizeBitmap(Bitmap b, double resize)
        {
            int nWidth = Convert.ToInt16(b.Width * resize);
            int nHeight = Convert.ToInt16(b.Height * resize);
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            return result;
        }
        void brandBox_MouseHover(object sender, EventArgs e)
        {
            
        }
        void brandBox_MouseLeave(object sender, EventArgs e)
        {
            
        }
        void brandBox_MouseClick(object sender, EventArgs e)
        {
            BrandBox b = (BrandBox)sender;
            pc.makeBrand(b.brand);
        }
        // 更新圖片
        public void updateImage()
        {
            addNouth();
            addEast();
            addSouth();
            addWest();
            addTable();
        }
        public void cleanImage()
        {
            foreach (FlowLayoutPanel f in flowLayoutBrands)
                f.Controls.Clear();
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