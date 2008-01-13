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
        public Information()
        {
            InitializeComponent();
        }
        public void setAllPlayers(AllPlayers all)
        {
            this.all = all;
            updateMoney();
            updateName();
            updateTitle();
            updateNowPlayer();
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
        }

        private void updateTitle()
        {
            string s = all.getLocation().ToString();
            s += "- (";
            s += all.Brand_Count.ToString();
            s += ")";
            this.Text = s;
        }

        private void updateName()
        {
            Up_label.Text = all.Name[(int)location.North].ToString();
            Right_label.Text = all.Name[(int)location.East].ToString();
            Down_label.Text = all.Name[(int)location.South].ToString();
            Left_label.Text = all.Name[(int)location.West].ToString();
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