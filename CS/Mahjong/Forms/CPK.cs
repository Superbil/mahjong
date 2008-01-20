using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mahjong.Control;
using Mahjong.Brands;

namespace Mahjong.Forms
{
    public partial class CPK : Form
    {
        ProgramControl pc;
        Brand brand;
        public CPK(ProgramControl pc,Brand brand)
        {
            InitializeComponent();
            this.pc = pc;
            this.brand = brand;
        }

        private void CPK_Load(object sender, EventArgs e)
        {
            Chow.Text = Mahjong.Properties.Settings.Default.Chow;
            Pong.Text = Mahjong.Properties.Settings.Default.Pong;
            Kong.Text = Mahjong.Properties.Settings.Default.Kong;
            Win.Text = Mahjong.Properties.Settings.Default.Win;
            Pass.Text = Mahjong.Properties.Settings.Default.Pass;
            DarkKong.Text = Mahjong.Properties.Settings.Default.DarkKong;

            if (brand.getClass() == Mahjong.Properties.Settings.Default.Wordtiles)
            {
                WordBrand w = (WordBrand)brand;
                this.Text = w.getWordClass();
            }
            else
                this.Text = brand.getNumber() + brand.getClass();
        }
        /// <summary>
        /// ³]©w«ö¶sª¬ºA
        /// </summary>
        /// <param name="chow">¦Y«ö¶s</param>
        /// <param name="pong">¸I«ö¶s</param>
        /// <param name="kong">ºb«ö¶s</param>
        /// <param name="win">­J«ö¶s</param>
        public void Enabled_Button(bool chow,bool pong,bool kong,bool darkong,bool win)
        {
            this.Chow.Enabled = chow;
            this.Pong.Enabled = pong;
            this.Kong.Enabled = kong;
            this.Win.Enabled = win;
            this.DarkKong.Enabled = darkong;
        }

        private void Chow_Click(object sender, EventArgs e)
        {
            pc.chow(brand);
            this.Close();
        }

        private void Pong_Click(object sender, EventArgs e)
        {
            pc.pong(brand);
            this.Close();
        }

        private void Kong_Click(object sender, EventArgs e)
        {
            pc.kong(brand);
            this.Close();
        }

        private void Win_Click(object sender, EventArgs e)
        {
            pc.win();
            this.Close();
        }

        private void Pass_Click(object sender, EventArgs e)
        {
            pc.pass(brand);
            this.Close();
        }

        private void DarkKong_Click(object sender, EventArgs e)
        {
            pc.dark_kong(brand);
            this.Close();
        }        
    }
}