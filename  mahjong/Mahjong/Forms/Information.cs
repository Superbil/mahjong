using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mahjong.Players;
using Mahjong.Control;

namespace Mahjong.Forms
{
    public partial class Information : Form
    {
        AllPlayers all;
        bool Debug = false;

        public Information()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 設定 AllPlayers
        /// </summary>
        /// <param name="all">AllPlayers</param>
        public void setAllPlayers(AllPlayers all)
        {
            this.all = all;
            updateMoney();
            updateName();
            updateTitle();
            updateNowPlayer();
        }
        /// <summary>
        /// 除錯模式
        /// </summary>
        public bool DebugMode
        {
            get
            {
                return Debug;
            }
            set
            {
                Debug = value;
            }
        }

        private void Information_Load(object sender, EventArgs e)
        {
            updateMoney();
            updateName();
            updateTitle();
            updateNowPlayer();
        }

        private void updateNowPlayer()
        {
            Color c = Color.Yellow;
            Right_label.BackColor = Color.Coral;
            Down_label.BackColor = Color.Coral;
            Left_label.BackColor = Color.Coral;
            Up_label.BackColor = Color.Coral;

            if (all.state == (int)location.East)
                Right_label.BackColor = c;
            else if (all.state == (int)location.South)
                Down_label.BackColor = c;
            else if (all.state == (int)location.West)
                Left_label.BackColor = c;
            else if (all.state == (int)location.North)
                Up_label.BackColor = c;

            if (all.getLocation().Winer == location.North)
            {
                Up_label.Text += "(";
                Up_label.Text += all.Win_Times;
                Up_label.Text += ")";
            }
            else if (all.getLocation().Winer == location.East)
            {
                Right_label.Text += "(";
                Right_label.Text += all.Win_Times;
                Right_label.Text += ")";
            }
            else if (all.getLocation().Winer == location.South)
            {
                Down_label.Text += "(";
                Down_label.Text += all.Win_Times;
                Down_label.Text += ")";
            }
            else if (all.getLocation().Winer == location.West)
            {                
                Left_label.Text += "(";
                Left_label.Text += all.Win_Times;
                Left_label.Text += ")";
            }

        }

        private void updateTitle()
        {
            string s = all.getLocation().ToString();
            s += " - (";
            s += all.Brand_Count.ToString();
            s += "/";
            s += all.Table.getCount();
            s += ")";
            this.Text = s;
        }

        private void updateName()
        {
            if (Debug)
            {
                Up_label.Text = all.Name[(int)location.North].ToString() + "~" + all.Players[(int)location.North].getCount().ToString();
                Right_label.Text = all.Name[(int)location.East].ToString() + "~" + all.Players[(int)location.East].getCount().ToString();
                Down_label.Text = all.Name[(int)location.South].ToString() + "~" + all.Players[(int)location.South].getCount().ToString();
                Left_label.Text = all.Name[(int)location.West].ToString() + "~" + all.Players[(int)location.West].getCount().ToString();
            }
            else
            {
                Up_label.Text = all.Name[(int)location.North].ToString();
                Right_label.Text = all.Name[(int)location.East].ToString();
                Down_label.Text = all.Name[(int)location.South].ToString();
                Left_label.Text = all.Name[(int)location.West].ToString();
            }
        }
        void updateMoney()
        {
            Up_money.Text = all.Money[(int)location.North].ToString();
            Right_money.Text = all.Money[(int)location.East].ToString();
            Down_money.Text = all.Money[(int)location.South].ToString();
            Left_money.Text = all.Money[(int)location.West].ToString();
        }
    }
}