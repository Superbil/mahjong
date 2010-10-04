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
        InputName inputname = new InputName();
        public NewTable()
        {
            InitializeComponent();
        }

        public override void Setup(AllPlayers all)
        {
            this.place = all.place;
            ShowMessageBox_Menu.Checked = pc.ShowMessageBox = all.showMessageBox;
            setTitle();

            inputname.allplayers = all;
            inputname.ShowDialog();
            this.all = inputname.allplayers;
        }

        FlowLayoutPanel fl_Hand_from_location(location state)
        {
            if (state == location.North)
                return fl_Up_Hand;
            else if (state == location.West)
                return fl_Left_Hand;
            else if (state == location.South)
                return fl_Down_Hand;
            else if (state == location.East)
                return fl_Right_Hand;
            else
                return fl_ShowTable;
        }

        FlowLayoutPanel fl_Show_from_location(location state)
        {
            if (state == location.North)
                return fl_Up_Show;
            else if (state == location.West)
                return fl_Left_Show;
            else if (state == location.South)
                return fl_Down_Show;
            else if (state == location.East)
                return fl_Right_Show;
            else
                return fl_Table;
        }

        protected override void addimage(location state, Brand brand, RotateFlipType rotate)
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
            add_flowLayoutBrands(state, tempBrandbox);
        }

        private void add_flowLayoutBrands(location state, BrandBox brandbox)
        {
            if (state == location.Table && brandbox.brand.IsCanSee == false)
                fl_Table.Controls.Add(brandbox);
            else if (brandbox.brand.IsCanSee == true && brandbox.brand.Team >= 1)
                fl_Show_from_location(state).Controls.Add(brandbox);
            else
                fl_Hand_from_location(state).Controls.Add(brandbox);            
        }

        protected override void clearNowPlayer()
        {
            fl_Hand_from_location(place.getRealPlace(all.State)).Controls.Clear();
            fl_Show_from_location(place.getRealPlace(all.State)).Controls.Clear();
        }

        protected override void clearFlowLayoutBrands_Table()
        {
            fl_Hand_from_location(location.Table).Controls.Clear();
        }

        public override void clearAll()
        {
            fl_Hand_from_location(location.North).Controls.Clear();
            fl_Hand_from_location(location.East).Controls.Clear();
            fl_Hand_from_location(location.South).Controls.Clear();
            fl_Hand_from_location(location.West).Controls.Clear();
            fl_Hand_from_location(location.Table).Controls.Clear();

            fl_Show_from_location(location.North).Controls.Clear();
            fl_Show_from_location(location.East).Controls.Clear();
            fl_Show_from_location(location.South).Controls.Clear();
            fl_Show_from_location(location.West).Controls.Clear();
            
            fl_Table.Controls.Clear();
        }
        public override void cleanImage()
        {
            clearAll();
        }

        public override void setInforamtion()
        {
            updateMoney();
            updateName();
            updateTitle();
            updateNowPlayer_Information();
        }

        private void updateTitle()
        {
            string s = all.getLocation.ToString();
            s += " - (";
            s += all.Brand_Count.ToString();
            s += "/";
            s += all.Table.getCount();
            s += ")";
            this.Text = s;
        }
        /// <summary>
        /// 更新玩家名稱
        /// </summary>
        private void updateName()
        {
            lab_Up_Name.Text = all.Name[place.getRealPlace_Up].ToString();
            lab_Right_Name.Text = all.Name[place.getRealPlace_Right].ToString();
            lab_Down_Name.Text = all.Name[place.getRealPlace_Down].ToString();
            lab_Left_Name.Text = all.Name[place.getRealPlace_Left].ToString();
        }
        /// <summary>
        /// 更新玩家金錢資訊
        /// </summary>
        private void updateMoney()
        {
            lab_Up_Money.Text = all.Money[place.getRealPlace_Up].ToString();
            lab_Right_Money.Text = all.Money[place.getRealPlace_Right].ToString();
            lab_Down_money.Text = all.Money[place.getRealPlace_Down].ToString();
            lab_Left_Money.Text = all.Money[place.getRealPlace_Left].ToString();
        }
        /// <summary>
        /// 更新現在玩家資訊加上第幾莊和顏色
        /// </summary>
        private void updateNowPlayer_Information()
        {
            Color c = Color.Yellow;
            tableLayoutPanel_Down_Personal.BackColor = Color.DarkSeaGreen;
            tableLayoutPanel_Left_Personal.BackColor = Color.DarkSeaGreen;
            tableLayoutPanel_Right_Personal.BackColor = Color.DarkSeaGreen;
            tableLayoutPanel_Up_Personal.BackColor = Color.DarkSeaGreen;

            if (all.State == place.Right)
                tableLayoutPanel_Right_Personal.BackColor = c;
            else if (all.State == place.Down)
                tableLayoutPanel_Down_Personal.BackColor = c;
            else if (all.State == place.Left)
                tableLayoutPanel_Left_Personal.BackColor = c;
            else if (all.State == place.Up)
                tableLayoutPanel_Up_Personal.BackColor = c;

            if (all.getLocation.Winer == place.Up)
            {
                lab_Up_Name.Text += wind_Times_to_string(all.win_Times);
            }
            else if (all.getLocation.Winer == place.Right)
            {
                lab_Right_Name.Text += wind_Times_to_string(all.win_Times);
            }
            else if (all.getLocation.Winer == place.Down)
            {
                lab_Down_Name.Text += wind_Times_to_string(all.win_Times);
            }
            else if (all.getLocation.Winer == place.Left)
            {
                lab_Left_Name.Text += wind_Times_to_string(all.win_Times);
            }
        }
        /// <summary>
        /// 加上連幾拉幾
        /// </summary>
        /// <param name="win">次數</param>
        /// <returns>連幾拉幾，若為1次就顯示為莊家</returns>
        private string wind_Times_to_string(int win)
        {
            if (win == 1)
            {
                return " 莊家";
            }
            else
            {
                return " 連" + win.ToString() + "拉" + win.ToString();
            }
        }
    }
}