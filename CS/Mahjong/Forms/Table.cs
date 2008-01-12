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
        public bool ShowAll;
        Bitmap arrow;
        
        public Table(ProgramControl pc)
        {
            InitializeComponent();
            this.flowLayoutBrands = new FlowLayoutPanel[5];
            this.pc = pc;
            ShowAll = false;
            this.KeyUp += new KeyEventHandler(Table_KeyUp);
        }

        void Table_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F8")
                ;
        }
        public void Setup(AllPlayers all)
        {
            this.all = all;
            arrow = Mahjong.Properties.Resources.a;
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
            setFlowLayout_Dock();
            //this.flowLayoutBrands[4].BackColor=;
        }
        private void setFlowLayout_name()
        {
            this.flowLayoutBrands[0].Name = Mahjong.Properties.Settings.Default.Nouth;
            this.flowLayoutBrands[1].Name = Mahjong.Properties.Settings.Default.East;
            this.flowLayoutBrands[2].Name = Mahjong.Properties.Settings.Default.South;
            this.flowLayoutBrands[3].Name = Mahjong.Properties.Settings.Default.West;
        }
        private void setFlowLayout_size()
        {
            this.flowLayoutBrands[0].Size = new Size((width + padding * 2) * all.Dealnumber, height + padding * 2);
            this.flowLayoutBrands[1].Size = new Size(height + padding * 2, (width + padding * 2) * all.Dealnumber);
            this.flowLayoutBrands[2].Size = new Size((width + padding * 2) * all.Dealnumber, height + padding * 2);
            this.flowLayoutBrands[3].Size = new Size(height + padding * 2, (width * 2 + padding * 2) * all.Dealnumber);
            this.flowLayoutBrands[4].Size = new Size(width * all.Dealnumber + padding * 2, height * (all.sumBrands / all.Dealnumber) + padding * 2);
        }
        private void setFlowLayout_location(int keepsize)
        {
            this.flowLayoutBrands[0].Location = new Point(keepsize * 2 + (height + padding * 2), keepsize);
            this.flowLayoutBrands[1].Location = new Point(keepsize * 3 + height + padding * 2 + (width + padding * 2) * all.Dealnumber, keepsize * 2 + height + padding * 2);
            this.flowLayoutBrands[2].Location = new Point(keepsize * 2 + height + padding * 2, keepsize * 3 + height * (all.Dealnumber + 1) + padding * 2);
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
            addimage_player(all.Players[(int)State.North], State.North, RotateFlipType.Rotate180FlipNone);
        }
        void addEast()
        {
            //圖片旋轉270度
            addimage_player(all.Players[(int)State.East], State.East, RotateFlipType.Rotate270FlipNone);
        }
        void addSouth()
        {
            addimage_player(all.Players[(int)State.South], State.South, RotateFlipType.RotateNoneFlipNone);
        }
        void addWest()
        {
            //圖片旋轉90度
            addimage_player(all.Players[(int)State.West], State.West, RotateFlipType.Rotate90FlipNone);
        }
        void addTable()
        {
            addimage_player(all.Show_Table, State.Table, RotateFlipType.RotateNoneFlipNone);
        }
        void addimage_player(BrandPlayer player, State state, RotateFlipType rotate)
        {   
                Iterator temp = player.creatIterator();
                addimage_iterator(temp,state,rotate);
        }
        private void addimage_iterator(Iterator iterator, State state, RotateFlipType rotate)
        {
            while (iterator.hasNext())
            {
                Brand brand = (Brand)iterator.next();
                addimage(state, brand, rotate);
            }
        }
        private void addimage(State state,Brand brand,RotateFlipType rotate) 
        {
            Bitmap bitmap;
            // 如果是可視的牌就設定顯示牌的圖型，否則就顯示直立的牌 Mahjong.Properties.Resources.upbarnd
            if (brand.IsCanSee || state == State.South || ShowAll)
                bitmap = new Bitmap(brand.image);
            else
                bitmap = new Bitmap(Mahjong.Properties.Resources.upbarnd);
            // 設定牌
            BrandBox tempBrandbox = new BrandBox(brand);            

            // 設定自動縮放
            tempBrandbox.SizeMode = PictureBoxSizeMode.AutoSize;            

            // 設定邊距            
            //if (state == State.South)
            //    tempBrandbox.Margin = new Padding(padding);
            //else
                tempBrandbox.Margin = new Padding(padding);

            // 要轉的角度
            bitmap.RotateFlip(rotate);

            // 提示
            if (ShowAll)
                tempBrandbox.Click += new EventHandler(tempBrandbox_Click);

            // 滑鼠事件
            if (state == State.South &&
                brand.getClass()!=Mahjong.Properties.Settings.Default.Flower)
            {
                //tempBrandbox.MouseHover += new EventHandler(brandBox_MouseHover);
                tempBrandbox.MouseLeave += new EventHandler(brandBox_MouseLeave);
                tempBrandbox.Click += new EventHandler(brandBox_MouseClick);
            }
            else
                bitmap = ResizeBitmap(bitmap,1);
                        
            tempBrandbox.Image = bitmap;
            
            this.flowLayoutBrands[(int)state].Controls.Add(tempBrandbox);
            this.Update();
        }

        void tempBrandbox_Click(object sender, EventArgs e)
        {
            BrandBox b = (BrandBox)sender;
            StringBuilder s = new StringBuilder();
            s.Append(b.brand.getNumber() + "," + b.brand.getClass() + "\n");
            s.Append("值: " + b.brand.getNumber() + "\n");
            s.Append("類別: " + b.brand.getClass() + "\n");
            s.Append("分數: " + b.brand.Source + "\n");
            s.Append("組別: " + b.brand.Team + "\n");
            s.Append("圖片盒:\nX:" + b.Location.X + " Y: " + b.Location.Y + "\n");
            s.Append("長: "+b.Size.Width+" 高: "+b.Size.Height+"\n");
            MessageBox.Show(s.ToString());
        }
        /// <summary>
        /// 重繪Bitmap(縮放)
        /// </summary>
        /// <param name="b">圖型</param>
        /// <param name="resize">比率</param>
        /// <returns>圖型</returns>
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
            BrandBox b = (BrandBox)sender;
            PictureBox temp = new PictureBox();
            temp.Location = new Point(b.Location.X, b.Location.Y - arrow.Height);
            //temp.l = b.Location.X;
            //temp.Location.Y = b.Location.Y - arrow.Height;
            temp.Image = arrow;
            this.Controls.Add(temp);
            this.Update();
        }
        void brandBox_MouseLeave(object sender, EventArgs e)
        {
            ;
        }
        void brandBox_MouseClick(object sender, EventArgs e)
        {
            BrandBox b = (BrandBox)sender;
            pc.makeBrand(b.brand);
        }
        /// <summary>
        /// 新增玩家、桌面圖片
        /// </summary> 
        public void addImage()
        {
            addNouth();
            addEast();
            addSouth();
            addWest();
            addTable();
        }
        /// <summary>
        /// 清除所有顯示的圖片
        /// </summary>
        public void cleanImage()
        {
            foreach (FlowLayoutPanel f in flowLayoutBrands)
                f.Controls.Clear();
        }
        /// <summary>
        /// 清除所有控制項
        /// </summary>
        public void cleanAll()
        {
            this.Controls.Clear();
        }
        public void updateNowPlayer()
        {
            switch (all.state)
            {
                case 1:
                    flowLayoutBrands[all.state].Controls.Clear();
                    addEast();
                    break;
                case 0:
                    flowLayoutBrands[all.state].Controls.Clear();
                    addNouth();
                    break;
                case 3:
                    flowLayoutBrands[all.state].Controls.Clear();
                    addWest();
                    break;
                case 2:
                    flowLayoutBrands[all.state].Controls.Clear();
                    addSouth();
                    break;                
            }
        }
        public void updateTable()
        {
            flowLayoutBrands[(int)State.Table].Controls.Clear();
            addTable();
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