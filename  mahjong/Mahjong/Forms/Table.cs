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
using System.Media;

namespace Mahjong.Forms
{
    public partial class Table : Form
    {
        internal ProgramControl pc;
        private FlowLayoutPanel[] flowLayoutBrands;
        protected AllPlayers all;
        int width = (int)((double)Settings.Default.image_w * Settings.Default.ResizePercentage);
        int height = (int)((double)Settings.Default.image_h * Settings.Default.ResizePercentage);
        /// <summary>
        /// 圖片盒的內距
        /// </summary>
        protected int padding = 1;
        /// <summary>
        /// 是否顯示所有的牌
        /// </summary>
        internal bool ShowAll;
        internal bool ShowBrandInfo;
        
        bool lockuser;
        /// <summary>
        /// 作弊功能
        /// </summary>
        internal bool cheat;
        /// <summary>
        /// 牌桌位置所對應到的玩家
        /// </summary>
        internal Place place = new Place();
        /// <summary>
        /// 設定非同步執行序呼叫點
        /// </summary>
        /// <param name="flp">FlowLayoutPanel []</param>
        delegate void SetControlCallback(FlowLayoutPanel[] flp);

        public Table()
        {
            InitializeComponent();
            flowLayoutBrands = new FlowLayoutPanel[5];
            ShowAll = false;
            ShowBrandInfo = false;
            lockuser = false;
            cheat = false;
            KeyUp += new KeyEventHandler(Table_KeyUp);
            ShowMessageBox_Menu.CheckedChanged += new EventHandler(ShowMessageBox_Menu_CheckedChanged);
        }

        void ShowMessageBox_Menu_CheckedChanged(object sender, EventArgs e)
        {
            pc.ShowMessageBox = ShowMessageBox_Menu.Checked;
        }

        internal AllPlayers Allplayers
        {
            set
            {
                all = value;
            }
            get
            {
                return all;
            }
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
            if (e.KeyCode.ToString() == Settings.Default.DebugKey)
            {
                if (ShowAll)
                {
                    ShowAll = false;
                }
                else
                    ShowAll = true;
            }
            // 按下F7開啟牌的資訊顯示
            if (e.KeyCode.ToString() == Settings.Default.Debug_InformationKey)
            {
                if (ShowBrandInfo)
                    ShowBrandInfo = false;
                else
                    ShowBrandInfo = true;
            }
            // 更新桌面上的牌和其他資訊
            if (e.KeyCode.ToString() == Settings.Default.Debug_RenewKey)
            {
                cleanImage();
                addImage();
                setTitle();
                setInforamtion();
            }
            // 開新遊戲熱鍵
            if (e.KeyCode.ToString() == Settings.Default.NewGame_Key)
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
        public virtual void Setup(AllPlayers all)
        {
            this.all = all;
            this.place = all.place;
            ShowMessageBox_Menu.Checked = pc.ShowMessageBox = all.showMessageBox;
            setFlowLayout();
            setTitle();
        }

        protected void setTitle()
        {
            string s;
            s = Settings.Default.Title;
            if (ShowAll)
            {
                s += " - ";
                s += Settings.Default.Debug;
            }
            Text = s;
        }

        public virtual void setInforamtion()
        {
            pc.setInforamtion();
        }

        void SetControl(FlowLayoutPanel[] flp)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetControlCallback(SetControl), new object[] { flp });
                this.Invoke(new setFlowLayout_delegate(setFlowLayout_size));
                this.Invoke(new setFlowLayout_delegate(setFlowLayout_Dock));
                this.Invoke(new setFlowLayout_delegate(setFlowLayout_FlowDirection));
                this.Invoke(new setFlowLayout_delegate(setFlowLayout_Anchor));
                this.Invoke(new setFlowLayout_delegate(setFlowLayout_location));
            }
            else
                this.Controls.AddRange(flp);
        }
        delegate void setFlowLayout_delegate();
        private void setFlowLayout()
        {
            for (int i = 0; i < flowLayoutBrands.Length; i++)
                flowLayoutBrands[i] = new FlowLayoutPanel();
                                    
            SetControl(flowLayoutBrands);                        
            if (!this.InvokeRequired)
            {
                setFlowLayout_size();                
                setFlowLayout_Dock();
                setFlowLayout_FlowDirection();
                setFlowLayout_Anchor();
                setFlowLayout_location();
            }            
            setFlowLayout_name();
            setFlowLayout_Margin(10);
        }

        private void setFlowLayout_FlowDirection()
        {
            flowLayoutBrands[0].FlowDirection = FlowDirection.RightToLeft;
            flowLayoutBrands[1].FlowDirection = FlowDirection.BottomUp;
            flowLayoutBrands[2].FlowDirection = FlowDirection.LeftToRight;
            flowLayoutBrands[3].FlowDirection = FlowDirection.TopDown;
        }
        private void setFlowLayout_name()
        {
            flowLayoutBrands[0].Name = Settings.Default.Nouth;
            flowLayoutBrands[1].Name = Settings.Default.East;
            flowLayoutBrands[2].Name = Settings.Default.South;
            flowLayoutBrands[3].Name = Settings.Default.West;
        }
        private void setFlowLayout_size()
        {
            flowLayoutBrands[0].Size = new Size((width + padding * 2) * all.Dealnumber, height * 2 + padding * 2);
            flowLayoutBrands[1].Size = new Size(height * 2 + padding * 2, (width + padding * 2) * all.Dealnumber);
            flowLayoutBrands[2].Size = new Size((width + padding * 2) * all.Dealnumber, height * 2 + padding * 2);
            flowLayoutBrands[3].Size = new Size(height * 2 + padding * 2, (width * 2 + padding * 2) * all.Dealnumber);
            flowLayoutBrands[4].Size = new Size(width * all.Dealnumber + padding * 2, height * (all.sumBrands / all.Dealnumber) + padding * 2);
        }
        private void setFlowLayout_location()
        {
            int keepsize = 3;
            flowLayoutBrands[0].Location = new Point(keepsize * 2 + (height + padding * 2), keepsize);
            flowLayoutBrands[1].Location = new Point(
                keepsize * 3 + height + padding * 2 + (width + padding * 2) * all.Dealnumber, keepsize * 2 + height + padding * 2);
            flowLayoutBrands[2].Location = new Point(
                keepsize * 2 + height + padding * 2, keepsize * 3 + width * (all.Dealnumber + 1) + padding * 2 + height);
            flowLayoutBrands[3].Location = new Point(keepsize, keepsize * 2 + height + padding * 2);
            flowLayoutBrands[4].Location = new Point(keepsize * 2 + (height * 2 + padding * 2), keepsize * 2 + (height * 2 + padding * 2));
        }
        void setFlowLayout_Dock()
        {
            flowLayoutBrands[0].Dock = DockStyle.Top;
            flowLayoutBrands[1].Dock = DockStyle.Right;
            flowLayoutBrands[2].Dock = DockStyle.Bottom;
            flowLayoutBrands[3].Dock = DockStyle.Left;
            flowLayoutBrands[4].Dock = DockStyle.Fill;
        }
        void setFlowLayout_Anchor()
        {
            flowLayoutBrands[4].Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
        }

        private void setFlowLayout_Margin(int size)
        {
            foreach (FlowLayoutPanel f in flowLayoutBrands)
                f.Margin = new Padding(size);
        }
        protected virtual void addNouth()
        {
            //圖片旋轉180度
            addimage_player(all.Players[(int)place.Up], location.North, RotateFlipType.Rotate180FlipNone);
        }
        protected virtual void addEast()
        {
            //圖片旋轉270度
            addimage_player(all.Players[(int)place.Right], location.East, RotateFlipType.Rotate270FlipNone);
        }
        protected virtual void addSouth()
        {
            addimage_player(all.Players[(int)place.Down], location.South, RotateFlipType.RotateNoneFlipNone);
        }
        protected virtual void addWest()
        {
            //圖片旋轉90度
            addimage_player(all.Players[(int)place.Left], location.West, RotateFlipType.Rotate90FlipNone);
        }
        protected virtual void addShowTable()
        {
            addimage_player(all.Show_Table, location.Table, RotateFlipType.RotateNoneFlipNone);
        }
        protected virtual void addTable()
        {
            addimage_player(all.Table, location.Table, RotateFlipType.RotateNoneFlipNone);
        }
        void addimage_player(BrandPlayer player, location state, RotateFlipType rotate)
        {
            Iterator temp = player.creatIterator();
            addimage_iterator(temp, state, rotate);
            if (InvokeRequired)
                Invoke(new Update_delegate(Update));
            else
                Update();
        }
        delegate void Update_delegate();

        protected virtual void addimage_iterator(Iterator iterator, location state, RotateFlipType rotate)
        {
            while (iterator.hasNext())
            {
                Brand brand = (Brand)iterator.next();
                addimage(state, brand, rotate);
            }
        }
        protected virtual void addimage(location state, Brand brand, RotateFlipType rotate)
        {
            Bitmap bitmap;
            // 如果是可視的牌就設定顯示牌的圖型，否則就顯示直立的牌 Resources.upbarnd
            if (brand.IsCanSee || state == location.South || ShowAll)
                bitmap = new Bitmap(brand.image);
            else
                bitmap = new Bitmap(Resources.upbarnd);
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
                && brand.getClass() != Settings.Default.Flower
                && brand.Team < 1
                )
            {
                tempBrandbox.MouseMove += new MouseEventHandler(tempBrandbox_MouseMove);
                tempBrandbox.MouseLeave += new EventHandler(brandBox_MouseLeave);
                tempBrandbox.Click += new EventHandler(brandBox_MouseClick);

                // 作弊事件
                //if (ShowAll && ShowBrandInfo)
                //    tempBrandbox.MouseHover += new EventHandler(debug_Click);
                //else
                //    tempBrandbox.MouseHover -= new EventHandler(debug_Click);
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
            bitmap = ResizeBitmap(bitmap, Settings.Default.ResizePercentage);

            // 設定圖片      
            tempBrandbox.Image = bitmap;

            // 新增至控制項
            add_flowLayoutBrands((int)state,tempBrandbox);
        }
        delegate void add_flowLayoutBrands_delegate(int state, BrandBox brandbox);

        void add_flowLayoutBrands(int state, BrandBox brandbox)
        {
            if (flowLayoutBrands[state].InvokeRequired)
            {
                flowLayoutBrands[state].Invoke(new add_flowLayoutBrands_delegate(add_flowLayoutBrands), new object[] { state,brandbox });
            }
            else
                flowLayoutBrands[state].Controls.Add(brandbox);
        }

        protected void tempBrandbox_MouseMove(object sender, MouseEventArgs e)
        {
            BrandBox b = (BrandBox)sender;
            if (pc.NowPlayer_is_Real_Player && all.State == place.Down)
                b.BackColor = Color.Blue;           
        }

        protected void brandBox_MouseLeave(object sender, EventArgs e)
        {
            BrandBox b = (BrandBox)sender;
            b.BackColor = BackColor;
        }

        /// <summary>
        /// 作弊事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cheat_MouseClick(object sender, MouseEventArgs e)
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
            cleanImage();
            addImage();
                      
        }

        protected void debug_Click(object sender, EventArgs e)
        {
            BrandBox b = (BrandBox)sender;
            StringBuilder s = new StringBuilder();
            s.Append(b.brand.getNumber() + "," + b.brand.getClass() + "\n");
            s.Append(Settings.Default.Debug_Number);
            s.Append(": " + b.brand.getNumber() + "\n");
            s.Append(Settings.Default.Debug_Class);
            s.Append(": " + b.brand.getClass() + "\n");
            s.Append(Settings.Default.Debug_Source);
            s.Append(": " + b.brand.Source + "\n");
            s.Append(Settings.Default.Debug_Team);
            s.Append(": " + b.brand.Team + "\n");
            s.Append(Settings.Default.Debug_IsCanSee);
            s.Append(": " + b.brand.IsCanSee + "\n");
            s.Append(Settings.Default.WhoPush);
            s.Append(": " + all.getLocation.location_to_string(b.brand.WhoPush) + "\n");
            s.Append(Settings.Default.Debug_Picture);
            s.Append(":\nX:" + b.Location.X + " Y: " + b.Location.Y + "\n");
            s.Append(Settings.Default.Debug_Picture_Width);
            s.Append(": " + b.Size.Width + "\n");
            s.Append(Settings.Default.Debug_Picture_Height);
            s.Append(": " + b.Size.Height + "\n");
            MessageBox.Show(s.ToString(), Settings.Default.Debug);
        }

        public void showBrand(Brand brand)
        {            
            StringBuilder s = new StringBuilder();
            s.Append(brand.getNumber() + "," + brand.getClass() + "\n");
            s.Append(Settings.Default.Debug_Number);
            s.Append(": " + brand.getNumber() + "\n");
            s.Append(Settings.Default.Debug_Class);
            s.Append(": " + brand.getClass() + "\n");
            s.Append(Settings.Default.Debug_Source);
            s.Append(": " + brand.Source + "\n");
            s.Append(Settings.Default.Debug_Team);
            s.Append(": " + brand.Team + "\n");
            s.Append(Settings.Default.Debug_IsCanSee);
            s.Append(": " + brand.IsCanSee + "\n");
            s.Append(Settings.Default.WhoPush);
            s.Append(": " + all.getLocation.location_to_string(brand.WhoPush) + "\n");
            //s.Append(Settings.Default.Debug_Picture);
            //s.Append(":\nX:" + Location.X + " Y: " + Location.Y + "\n");
            //s.Append(Settings.Default.Debug_Picture_Width);
            //s.Append(": " + Size.Width + "\n");
            //s.Append(Settings.Default.Debug_Picture_Height);
            //s.Append(": " + Size.Height + "\n");
            MessageBox.Show(s.ToString(), Settings.Default.Debug);
        }

        /// <summary>
        /// 重繪Bitmap(縮放)
        /// </summary>
        /// <param name="b">圖型</param>
        /// <param name="resize">比率</param>
        /// <returns>圖型</returns>
        protected Bitmap ResizeBitmap(Bitmap b, double resize)
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
        protected void brandBox_MouseClick(object sender, EventArgs e)
        {
           
            BrandBox b = (BrandBox)sender;
            // 確定為玩家才發送事件
            if (pc.NowPlayer_is_Real_Player && all.State == place.Down)
            {
                PlaySound(b.brand);
                pc.makeBrand(b.brand);
            }
            b.Click -= new EventHandler(debug_Click);
        }

        /// <summary>
        /// 播放打牌聲音
        /// </summary>
        void PlaySound(Brand brand)
        {
            SoundPlayer soundplayer = new SoundPlayer();
            switch (brand.getClass())
            {
                case "萬":
                    switch (brand.getNumber())
                    {
                        case 1:
                            soundplayer.Stream = Resources.ten1s;
                            break;
                        case 2:
                            soundplayer.Stream = Resources.ten2s;
                            break;
                        case 3:
                            soundplayer.Stream = Resources.ten3s;
                            break;
                        case 4:
                            soundplayer.Stream = Resources.ten4s;
                            break;
                        case 5:
                            soundplayer.Stream = Resources.ten5s;
                            break;
                        case 6:
                            soundplayer.Stream = Resources.ten6s;
                            break;
                        case 7:
                            soundplayer.Stream = Resources.ten7s;
                            break;
                        case 8:
                            soundplayer.Stream = Resources.ten8s;
                            break;
                        case 9:
                            soundplayer.Stream = Resources.ten9s;
                            break;
                    }
                    break;
                case "索":
                    switch (brand.getNumber())
                    {
                        case 1:
                            soundplayer.Stream = Resources.rope1s;
                            break;
                        case 2:
                            soundplayer.Stream = Resources.rope2s;
                            break;
                        case 3:
                            soundplayer.Stream = Resources.rope3s;
                            break;
                        case 4:
                            soundplayer.Stream = Resources.rope4s;
                            break;
                        case 5:
                            soundplayer.Stream = Resources.rope5s;
                            break;
                        case 6:
                            soundplayer.Stream = Resources.rope6s;
                            break;
                        case 7:
                            soundplayer.Stream = Resources.rope7s;
                            break;
                        case 8:
                            soundplayer.Stream = Resources.rope8s;
                            break;
                        case 9:
                            soundplayer.Stream = Resources.rope9s;
                            break;
                    }
                    break;
                case "筒":
                    switch (brand.getNumber())
                    {
                        case 1:
                            soundplayer.Stream = Resources.tobe1s;
                            break;
                        case 2:
                            soundplayer.Stream = Resources.tobe2s;
                            break;
                        case 3:
                            soundplayer.Stream = Resources.tobe3s;
                            break;
                        case 4:
                            soundplayer.Stream = Resources.tobe4s;
                            break;
                        case 5:
                            soundplayer.Stream = Resources.tobe5s;
                            break;
                        case 6:
                            soundplayer.Stream = Resources.tobe6s;
                            break;
                        case 7:
                            soundplayer.Stream = Resources.tobe7s;
                            break;
                        case 8:
                            soundplayer.Stream = Resources.tobe8s;
                            break;
                        case 9:
                            soundplayer.Stream = Resources.tobe9s;
                            break;
                    }
                    break;
                case "字":
                    switch (brand.getNumber())
                    {
                        case 1:
                            soundplayer.Stream = Resources.word1s;
                            break;
                        case 2:
                            soundplayer.Stream = Resources.word2s;
                            break;
                        case 3:
                            soundplayer.Stream = Resources.word3s;
                            break;
                        case 4:
                            soundplayer.Stream = Resources.word4s;
                            break;
                        case 5:
                            soundplayer.Stream = Resources.word5s;
                            break;
                        case 6:
                            soundplayer.Stream = Resources.word6s;
                            break;
                        case 7:
                            soundplayer.Stream = Resources.word7s;
                            break;
                    }
                    break;
            }

            soundplayer.Play();
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

        delegate void flowLayoutBrands_Clear(FlowLayoutPanel f);

        /// <summary>
        /// 清除所有顯示的圖片
        /// </summary>
        public virtual void cleanImage()
        {
            foreach (FlowLayoutPanel f in flowLayoutBrands)
            {
                if (f.InvokeRequired)
                    f.Invoke(new flowLayoutBrands_Clear(clearFlowLayout_Control), new object[] { f });
                else
                    clearFlowLayout_Control(f);
            }
        }

        void clearFlowLayout_Control(FlowLayoutPanel f)
        {
            f.Controls.Clear();
        }
        /// <summary>
        /// 清除所有控制項
        /// </summary>
        public virtual void clearAll()
        {
            if (InvokeRequired)
                Invoke(new flowLayoutBrands_Nowplayer_Clear(clearControl));
            else
                clearControl();
        }

        void clearControl()
        {
            Controls.Clear();
        }

        delegate void flowLayoutBrands_Nowplayer_Clear();

        /// <summary>
        /// 更新現在玩家
        /// </summary>
        public virtual void updateNowPlayer()
        {
            if (InvokeRequired)
                Invoke(new flowLayoutBrands_Nowplayer_Clear(clearNowPlayer));
            else
                clearNowPlayer();
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

        protected virtual void clearNowPlayer()
        {
            flowLayoutBrands[(int)place.getRealPlace(all.State)].Controls.Clear();
        }

        protected virtual void clearFlowLayoutBrands_Table()
        {
            flowLayoutBrands[(int)location.Table].Controls.Clear();
        }

        /// <summary>
        /// 更新桌面
        /// </summary>
        public virtual void updateTable()
        {
            if (InvokeRequired)
                Invoke(new flowLayoutBrands_Nowplayer_Clear(clearFlowLayoutBrands_Table));
            else
                clearFlowLayoutBrands_Table();
            addShowTable();            
        }
        /// <summary>
        /// 更新Title和資訊盒
        /// </summary>
        public virtual void updateInforamation()
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
            pc.SetDealyTime = Settings.Default.RunRoundTime_Slow;
        }

        private void 正常ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Slow.Checked = false;
            Normal.Checked = true;
            Quick.Checked = false;
            pc.SetDealyTime = Settings.Default.RunRoundTime_Normal;
        }

        private void 非常快ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Slow.Checked = false;
            Normal.Checked = false;
            Quick.Checked = true;
            pc.SetDealyTime = Settings.Default.RunRoundTime_Quick;
        }

        private void 儲存牌局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.savegame();
        }

        private void 讀取牌局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc.loadgame();       
        }
    }
}