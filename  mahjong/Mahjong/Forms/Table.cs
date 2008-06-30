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
    public partial class Table : Form
    {
        internal ProgramControl pc;
        private FlowLayoutPanel[] flowLayoutBrands;
        AllPlayers all;
        int width = (int)((double)Mahjong.Properties.Settings.Default.image_w * Mahjong.Properties.Settings.Default.ResizePercentage);
        int height = (int)((double)Mahjong.Properties.Settings.Default.image_h * Mahjong.Properties.Settings.Default.ResizePercentage);
        int padding = 1;
        internal bool ShowAll;
        internal bool ShowBrandInfo;
        Bitmap arrow;
        bool lockuser;
        /// <summary>
        /// 作弊功能
        /// </summary>
        internal bool cheat;
        /// <summary>
        /// 牌桌位置所對應到的玩家
        /// </summary>
        internal Place place = new Place();

        public Table(ProgramControl pc)
        {
            InitializeComponent();
            this.flowLayoutBrands = new FlowLayoutPanel[5];
            this.pc = pc;
            this.pc.table = this;
            this.ShowAll = false;
            this.ShowBrandInfo = false;
            this.lockuser = false;
            this.cheat = false;
            this.KeyUp += new KeyEventHandler(Table_KeyUp);
            this.ShowMessageBox_Menu.CheckedChanged += new EventHandler(ShowMessageBox_Menu_CheckedChanged);
        }

        void ShowMessageBox_Menu_CheckedChanged(object sender, EventArgs e)
        {
            pc.ShowMessageBox = ShowMessageBox_Menu.Checked;
        }
        /// <summary>
        /// 鎖定使用者
        /// </summary>
        internal bool LockUser
        {
            set
            {
                lockuser = value;
            }
            get
            {
                return lockuser;
            }
        }
        void Table_KeyUp(object sender, KeyEventArgs e)
        {
            // 按下F8開啟 Debug
            if (e.KeyCode.ToString() == Mahjong.Properties.Settings.Default.DebugKey)
            {
                if (ShowAll)
                {
                    ShowAll = false;
                }
                else
                    ShowAll = true;
            }
            // 按下F7開啟牌的資訊顯示
            if (e.KeyCode.ToString() == Mahjong.Properties.Settings.Default.Debug_InformationKey)
            {
                if (ShowBrandInfo)
                    ShowBrandInfo = false;
                else
                    ShowBrandInfo = true;
            }
            // 更新桌面上的牌和其他資訊
            if (e.KeyCode.ToString() == Mahjong.Properties.Settings.Default.Debug_RenewKey)
            {
                cleanImage();
                addImage();
                setTitle();
                setInforamtion();
            }
            // 開新遊戲熱鍵
            if (e.KeyCode.ToString() == Mahjong.Properties.Settings.Default.NewGame_Key)
            {
                pc.newgame();
            }
            // 作弊
            if (e.KeyCode == Keys.F12)
            {
                if (!cheat)
                    cheat = true;
                else
                    cheat = false;
            }
        }
        /// <summary>
        /// 設定或取得 是否"顯示提示"
        /// </summary>
        internal bool SetCheck
        {
            get
            {
                return ShowMessageBox_Menu.Checked;

            }
            set
            {
                ShowMessageBox_Menu.Checked = value;
            }
        }
        /// <summary>
        /// 設定玩家
        /// </summary>
        /// <param name="all">AllPlayers</param>
        public void Setup(AllPlayers all)
        {
            this.all = all;
            this.place = all.place;
            ShowMessageBox_Menu.Checked = this.pc.ShowMessageBox = this.all.showMessageBox;
            arrow = Mahjong.Properties.Resources.a;
            setFlowLayout();
            setTitle();
        }

        private void setTitle()
        {
            string s;
            s = Mahjong.Properties.Settings.Default.Title;
            if (ShowAll)
            {
                s += " - ";
                s += Mahjong.Properties.Settings.Default.Debug;
            }
            this.Text = s;
        }

        private void setInforamtion()
        {
            pc.setInforamtion();
        }

        private void setFlowLayout()
        {
            for (int i = 0; i < flowLayoutBrands.Length; i++)
                flowLayoutBrands[i] = new FlowLayoutPanel();
            this.Controls.AddRange(flowLayoutBrands);
            setFlowLayout_location(3);
            setFlowLayout_name();
            setFlowLayout_size();
            setFlowLayout_Margin(10);
            setFlowLayout_Dock();
            //this.flowLayoutBrands[4].BackColor = Color.Blue;
            setFlowLayout_FlowDirection();
            this.flowLayoutBrands[4].Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
        }

        private void setFlowLayout_FlowDirection()
        {
            this.flowLayoutBrands[0].FlowDirection = FlowDirection.RightToLeft;
            this.flowLayoutBrands[1].FlowDirection = FlowDirection.BottomUp;
            this.flowLayoutBrands[2].FlowDirection = FlowDirection.LeftToRight;
            this.flowLayoutBrands[3].FlowDirection = FlowDirection.TopDown;
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
            this.flowLayoutBrands[0].Size = new Size((width + padding * 2) * all.Dealnumber, height * 2 + padding * 2);
            this.flowLayoutBrands[1].Size = new Size(height * 2 + padding * 2, (width + padding * 2) * all.Dealnumber);
            this.flowLayoutBrands[2].Size = new Size((width + padding * 2) * all.Dealnumber, height * 2 + padding * 2);
            this.flowLayoutBrands[3].Size = new Size(height * 2 + padding * 2, (width * 2 + padding * 2) * all.Dealnumber);
            this.flowLayoutBrands[4].Size = new Size(width * all.Dealnumber + padding * 2, height * (all.sumBrands / all.Dealnumber) + padding * 2);
        }
        private void setFlowLayout_location(int keepsize)
        {
            this.flowLayoutBrands[0].Location = new Point(keepsize * 2 + (height + padding * 2), keepsize);
            this.flowLayoutBrands[1].Location = new Point(
                keepsize * 3 + height + padding * 2 + (width + padding * 2) * all.Dealnumber, keepsize * 2 + height + padding * 2);
            this.flowLayoutBrands[2].Location = new Point(
                keepsize * 2 + height + padding * 2, keepsize * 3 + width * (all.Dealnumber + 1) + padding * 2 + height);
            this.flowLayoutBrands[3].Location = new Point(keepsize, keepsize * 2 + height + padding * 2);
            this.flowLayoutBrands[4].Location = new Point(keepsize * 2 + (height * 2 + padding * 2), keepsize * 2 + (height * 2 + padding * 2));
        }
        void setFlowLayout_Dock()
        {
            this.flowLayoutBrands[0].Dock = DockStyle.Top;
            this.flowLayoutBrands[1].Dock = DockStyle.Right;
            this.flowLayoutBrands[2].Dock = DockStyle.Bottom;
            this.flowLayoutBrands[3].Dock = DockStyle.Left;
            this.flowLayoutBrands[4].Dock = DockStyle.Fill;
        }
        private void setFlowLayout_Margin(int size)
        {
            foreach (FlowLayoutPanel f in flowLayoutBrands)
                f.Margin = new Padding(size);
        }
        void addNouth()
        {
            //圖片旋轉180度
            addimage_player(all.Players[(int)place.Up], location.North, RotateFlipType.Rotate180FlipNone);
        }
        void addEast()
        {
            //圖片旋轉270度
            addimage_player(all.Players[(int)place.Right], location.East, RotateFlipType.Rotate270FlipNone);
        }
        void addSouth()
        {
            addimage_player(all.Players[(int)place.Down], location.South, RotateFlipType.RotateNoneFlipNone);
        }
        void addWest()
        {
            //圖片旋轉90度
            addimage_player(all.Players[(int)place.Left], location.West, RotateFlipType.Rotate90FlipNone);
        }
        void addShowTable()
        {
            addimage_player(all.Show_Table, location.Table, RotateFlipType.RotateNoneFlipNone);
        }
        void addTable()
        {
            addimage_player(all.Table, location.Table, RotateFlipType.RotateNoneFlipNone);
        }
        void addimage_player(BrandPlayer player, location state, RotateFlipType rotate)
        {
            Iterator temp = player.creatIterator();
            addimage_iterator(temp, state, rotate);
            this.Update();
        }
        private void addimage_iterator(Iterator iterator, location state, RotateFlipType rotate)
        {
            while (iterator.hasNext())
            {
                Brand brand = (Brand)iterator.next();
                addimage(state, brand, rotate);
            }
        }
        private void addimage(location state, Brand brand, RotateFlipType rotate)
        {
            Bitmap bitmap;
            // 如果是可視的牌就設定顯示牌的圖型，否則就顯示直立的牌 Mahjong.Properties.Resources.upbarnd
            if (brand.IsCanSee || state == location.South || ShowAll)
                bitmap = new Bitmap(brand.image);
            else
                bitmap = new Bitmap(Mahjong.Properties.Resources.upbarnd);
            // 設定牌
            BrandBox tempBrandbox = new BrandBox(brand);

            // 設定自動縮放
            tempBrandbox.SizeMode = PictureBoxSizeMode.AutoSize;

            // 設定邊距            
            tempBrandbox.Margin = new Padding(0);
            tempBrandbox.Padding = new Padding(padding);

            // 要轉的角度
            bitmap.RotateFlip(rotate);

            // 提示
            if (ShowAll && ShowBrandInfo)
                tempBrandbox.Click += new EventHandler(debug_Click);

            // 滑鼠事件
            if (
                state == location.South
                && brand.getClass() != Mahjong.Properties.Settings.Default.Flower
                && brand.Team < 1
                //&& all.State == location.South
                )
            {
                tempBrandbox.MouseMove += new MouseEventHandler(tempBrandbox_MouseMove);
                tempBrandbox.MouseLeave += new EventHandler(brandBox_MouseLeave);
                tempBrandbox.Click += new EventHandler(brandBox_MouseClick);

                if (ShowAll && ShowBrandInfo)
                    tempBrandbox.MouseHover += new EventHandler(debug_Click);
                else
                    tempBrandbox.MouseHover -= new EventHandler(debug_Click);
            }
            else if (brand.Team >= 1)
            {
                tempBrandbox.BackColor = Color.DarkGreen;
            }
            else if (cheat && state != location.South)
            {
                tempBrandbox.MouseClick += new MouseEventHandler(cheat_MouseClick);
            }
            else
            {
                tempBrandbox.Click -= new EventHandler(brandBox_MouseClick);
                tempBrandbox.MouseClick -= new MouseEventHandler(cheat_MouseClick);

            }
            bitmap = ResizeBitmap(bitmap, Mahjong.Properties.Settings.Default.ResizePercentage);

            // 設定圖片      
            tempBrandbox.Image = bitmap;

            // 新增至控制項
            this.flowLayoutBrands[(int)state].Controls.Add(tempBrandbox);
            this.Update();
        }

        void tempBrandbox_MouseMove(object sender, MouseEventArgs e)
        {
            BrandBox b = (BrandBox)sender;
            b.BackColor = Color.Blue;            
        }

        void brandBox_MouseLeave(object sender, EventArgs e)
        {
            BrandBox b = (BrandBox)sender;
            b.BackColor = this.BackColor;
        }

        /// <summary>
        /// 作弊事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cheat_MouseClick(object sender, MouseEventArgs e)
        {
            BrandBox b = (BrandBox)sender;
            if (all.Show_Table.remove(b.brand))
            {
                all.Show_Table.add(all.NowPlayer.getBrand(all.NowPlayer.getCount() - 1));
                all.Show_Table.getBrand(all.Show_Table.getCount() - 1).IsCanSee = true;
            }
            else if (all.Table.remove(b.brand))
            {
                all.Table.add(all.NowPlayer.getBrand(all.NowPlayer.getCount() - 1));
                all.Table.getBrand(all.Show_Table.getCount() - 1).IsCanSee = false;
            }
            else if (all.Players[0].remove(b.brand))
                all.Players[0].add(all.NowPlayer.getBrand(all.NowPlayer.getCount() - 1));                
            else if (all.Players[1].remove(b.brand))
                all.Players[1].add(all.NowPlayer.getBrand(all.NowPlayer.getCount() - 1));
            else if (all.Players[2].remove(b.brand))
                all.Players[2].add(all.NowPlayer.getBrand(all.NowPlayer.getCount() - 1));
            else if (all.Players[3].remove(b.brand))
                all.Players[3].add(all.NowPlayer.getBrand(all.NowPlayer.getCount() - 1));

            all.NowPlayer.remove(all.NowPlayer.getBrand(all.NowPlayer.getCount() - 1));
            all.NowPlayer.add(b.brand);
            this.cleanImage();
            this.addImage();            
        }

        void debug_Click(object sender, EventArgs e)
        {
            BrandBox b = (BrandBox)sender;
            StringBuilder s = new StringBuilder();
            s.Append(b.brand.getNumber() + "," + b.brand.getClass() + "\n");
            s.Append(Mahjong.Properties.Settings.Default.Debug_Number);
            s.Append(": " + b.brand.getNumber() + "\n");
            s.Append(Mahjong.Properties.Settings.Default.Debug_Class);
            s.Append(": " + b.brand.getClass() + "\n");
            s.Append(Mahjong.Properties.Settings.Default.Debug_Source);
            s.Append(": " + b.brand.Source + "\n");
            s.Append(Mahjong.Properties.Settings.Default.Debug_Team);
            s.Append(": " + b.brand.Team + "\n");
            s.Append(Mahjong.Properties.Settings.Default.Debug_IsCanSee);
            s.Append(": " + b.brand.IsCanSee + "\n");
            s.Append(Mahjong.Properties.Settings.Default.WhoPush);
            s.Append(": " + all.getLocation.location_to_string(b.brand.WhoPush) + "\n");
            s.Append(Mahjong.Properties.Settings.Default.Debug_Picture);
            s.Append(":\nX:" + b.Location.X + " Y: " + b.Location.Y + "\n");
            s.Append(Mahjong.Properties.Settings.Default.Debug_Picture_Width);
            s.Append(": " + b.Size.Width + "\n");
            s.Append(Mahjong.Properties.Settings.Default.Debug_Picture_Height);
            s.Append(": " + b.Size.Height + "\n");            
            MessageBox.Show(s.ToString(), Mahjong.Properties.Settings.Default.Debug);
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

        /// <summary>
        /// 按下一張牌的事件
        /// </summary>
        /// <param name="sender">BrandBox</param>
        /// <param name="e"></param>
        void brandBox_MouseClick(object sender, EventArgs e)
        {
            BrandBox b = (BrandBox)sender;
            flowLayoutBrands[(int)place.getRealPlace(all.State)].Controls.Remove(b);
            pc.makeBrand(b.brand);
            b.Click -= new EventHandler(debug_Click);
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
            addShowTable();
            if (ShowAll)
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
        /// <summary>
        /// 更新現在玩家
        /// </summary>
        public void updateNowPlayer()
        {
            flowLayoutBrands[(int)place.getRealPlace(all.State)].Controls.Clear();
            switch (place.getRealPlace(all.State))
            {
                case location.East:
                    addEast();
                    break;
                case location.North:
                    addNouth();
                    break;
                case location.West:
                    addWest();
                    break;
                case location.South:
                    addSouth();
                    break;
            }
        }
        /// <summary>
        /// 更新桌面
        /// </summary>
        public void updateTable()
        {
            flowLayoutBrands[(int)location.Table].Controls.Clear();
            addShowTable();            
        }
        /// <summary>
        /// 更新Title和資訊盒
        /// </summary>
        public void updateInforamation()
        {
            setTitle();
            setInforamtion();
            all.showMessageBox = ShowMessageBox_Menu.Checked;
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
            //pc.config();
        }

        private void 關於ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pc.about();
        }

        private void 結束ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pc.exit();
        }

        private void ShowMessageBox_Menu_Click(object sender, EventArgs e)
        {
            if (ShowMessageBox_Menu.Checked)
                ShowMessageBox_Menu.Checked = false;
            else
                ShowMessageBox_Menu.Checked = true;
        }

        private void 非常慢ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Slow.Checked = true;
            Normal.Checked = false;
            Quick.Checked = false;
            pc.SetDealyTime = Mahjong.Properties.Settings.Default.RunRoundTime_Slow;
        }

        private void 正常ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Slow.Checked = false;
            Normal.Checked = true;
            Quick.Checked = false;
            pc.SetDealyTime = Mahjong.Properties.Settings.Default.RunRoundTime_Normal;
        }

        private void 非常快ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Slow.Checked = false;
            Normal.Checked = false;
            Quick.Checked = true;
            pc.SetDealyTime = Mahjong.Properties.Settings.Default.RunRoundTime_Quick;
        }

        private void 儲存牌局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.savefile();
        }

        private void 讀取牌局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.openfile();       
        }
    }
}