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
        Table table;
        bool Debug = false;

        public Information()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 更新資訊
        /// </summary>
        /// <param name="all">AllPlayers</param>
        public void updateInformation()
        {
            updateMoney();
            updateName();
            updateTitle();
            updateNowPlayer();
        }
        /// <summary>
        /// 設定
        /// </summary>
        /// <param name="table">Table</param>
        /// <param name="all">Allplayers</param>
        public void setup(Table table,AllPlayers all)
        {
            this.table = table;
            this.all = all;
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

            if (all.State == table.place.Right)
                Right_label.BackColor = c;
            else if (all.State == table.place.Down)
                Down_label.BackColor = c;
            else if (all.State == table.place.Left)
                Left_label.BackColor = c;
            else if (all.State == table.place.Up)
                Up_label.BackColor = c;

            if (all.getLocation.Winer == table.place.Up)
            {
                Up_label.Text += "(";
                Up_label.Text += all.Win_Times;
                Up_label.Text += ")";
            }
            else if (all.getLocation.Winer == table.place.Right)
            {
                Right_label.Text += "(";
                Right_label.Text += all.Win_Times;
                Right_label.Text += ")";
            }
            else if (all.getLocation.Winer == table.place.Down)
            {
                Down_label.Text += "(";
                Down_label.Text += all.Win_Times;
                Down_label.Text += ")";
            }
            else if (all.getLocation.Winer == table.place.Left)
            {                
                Left_label.Text += "(";
                Left_label.Text += all.Win_Times;
                Left_label.Text += ")";
            }

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

        private void updateName()
        {
            if (Debug)
            {
                Up_label.Text = all.Name[table.place.getRealPlace_Up].ToString() + "\n~" + all.Players[table.place.getRealPlace_Up].getCount().ToString();
                Right_label.Text = all.Name[table.place.getRealPlace_Right].ToString() + "\n~" + all.Players[table.place.getRealPlace_Right].getCount().ToString();
                Down_label.Text = all.Name[table.place.getRealPlace_Down].ToString() + "\n~" + all.Players[table.place.getRealPlace_Down].getCount().ToString();
                Left_label.Text = all.Name[table.place.getRealPlace_Left].ToString() + "\n~" + all.Players[table.place.getRealPlace_Left].getCount().ToString();
            }
            else
            {
                Up_label.Text = all.Name[table.place.getRealPlace_Up].ToString();
                Right_label.Text = all.Name[table.place.getRealPlace_Right].ToString();
                Down_label.Text = all.Name[table.place.getRealPlace_Down].ToString();
                Left_label.Text = all.Name[table.place.getRealPlace_Left].ToString();
            }
        }
        void updateMoney()
        {
            Up_money.Text = all.Money[table.place.getRealPlace_Up].ToString();
            Right_money.Text = all.Money[table.place.getRealPlace_Right].ToString();
            Down_money.Text = all.Money[table.place.getRealPlace_Down].ToString();
            Left_money.Text = all.Money[table.place.getRealPlace_Left].ToString();
        }
    }
}