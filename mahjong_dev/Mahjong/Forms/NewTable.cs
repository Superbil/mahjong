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
            // �p�G�O�i�����P�N�]�w��ܵP���ϫ��A�_�h�N��ܪ��ߪ��P Resources.upbarnd
            if (brand.IsCanSee || state == location.South || ShowAll)
                bitmap = new Bitmap(brand.image);
            else
                bitmap = new Bitmap(Resources.upbarnd);
            // �]�w�P
            BrandBox tempBrandbox = new BrandBox(brand);

            // �]�w�۰��Y��
            tempBrandbox.SizeMode = PictureBoxSizeMode.AutoSize;

            // �]�w��Z            
            tempBrandbox.Margin = new Padding(0);
            tempBrandbox.Padding = new Padding(padding);

            // �n�઺����
            bitmap.RotateFlip(rotate);

            // ����
            if (ShowAll && ShowBrandInfo)
                tempBrandbox.Click += new EventHandler(debug_Click);

            // �ƹ��ƥ�
            if (
                state == location.South
                && brand.getClass() != Settings.Default.Flower
                && brand.Team < 1
                )
            {
                tempBrandbox.MouseMove += new MouseEventHandler(tempBrandbox_MouseMove);
                tempBrandbox.MouseLeave += new EventHandler(brandBox_MouseLeave);
                tempBrandbox.Click += new EventHandler(brandBox_MouseClick);

                // �@���ƥ�
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

            // �]�w�Ϥ�
            tempBrandbox.Image = bitmap;

            // �s�W�ܱ��
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
        /// ��s���a�W��
        /// </summary>
        private void updateName()
        {
            lab_Up_Name.Text = all.Name[place.getRealPlace_Up].ToString();
            lab_Right_Name.Text = all.Name[place.getRealPlace_Right].ToString();
            lab_Down_Name.Text = all.Name[place.getRealPlace_Down].ToString();
            lab_Left_Name.Text = all.Name[place.getRealPlace_Left].ToString();
        }
        /// <summary>
        /// ��s���a������T
        /// </summary>
        private void updateMoney()
        {
            lab_Up_Money.Text = all.Money[place.getRealPlace_Up].ToString();
            lab_Right_Money.Text = all.Money[place.getRealPlace_Right].ToString();
            lab_Down_money.Text = all.Money[place.getRealPlace_Down].ToString();
            lab_Left_Money.Text = all.Money[place.getRealPlace_Left].ToString();
        }
        /// <summary>
        /// ��s�{�b���a��T�[�W�ĴX���M�C��
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
        /// �[�W�s�X�ԴX
        /// </summary>
        /// <param name="win">����</param>
        /// <returns>�s�X�ԴX�A�Y��1���N��ܬ����a</returns>
        private string wind_Times_to_string(int win)
        {
            if (win == 1)
            {
                return " ���a";
            }
            else
            {
                return " �s" + win.ToString() + "��" + win.ToString();
            }
        }
    }
}