using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mahjong.Players;
using Mahjong.Brands;

namespace Mahjong.Forms
{
    public partial class Tally : Form
    {
        Players.BrandPlayer x = new BrandPlayer();
        string y;
        Brand[] brands;
  

        public Tally()
        {
            InitializeComponent();
        }
        public void setPlayer(BrandPlayer player)
        {
            brands = new Brand[player.getCount()];
            for (int i = 0; i < player.getCount(); i++)
                brands[i] = player.getBrand(i);
        }
        /// <summary>
        /// 設定方位
        /// </summary>
        /// <param name="s1">局</param>
        /// <param name="s2">莊</param>
        /// <param name="s3">方位</param>
        /// <param name="number">連莊次數</param>
        public void setLocation(string s1,string s2,string s3,int number)
        {
            if (s1 == Mahjong.Properties.Settings.Default.East);
        }
        void go()
        {
            brands[0].Team=1;
            if (brands[0].getClass()==Mahjong.Properties.Settings.Default.Flower);

            //if (brands[0] == brands[1]);i
            //    y = brands[0].getNumber().ToString()+brands[0].getClass()+"碰";
            for (int i = 0; i < brands.Length; i++)
            textBox1.Text=textBox1.Text+ brands[i].getNumber() + brands[i].getClass() +" ";
            //Console.Write("{0},{1} " ,brands[i].getClass(),brands[i].getNumber());
            ponpon();
            //sky();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ponpon()
        {
            int i = 0, count=0;
            while(i < brands.Length - 2)
            {

                if (brands[i].getClass() == brands[i + 1].getClass() && brands[i + 2].getClass() == brands[i].getClass() && brands[i].getNumber() == brands[i + 1].getNumber() && brands[i + 2].getNumber() == brands[i].getNumber())
                    count++;
                
                labeltally.Text = "4";
                if(count==5)
                textBox1.Text = "碰碰胡";
            i++;
            }
            green();
        }

        void green()
        {
            WordBrand w = new WordBrand(0);
            int i=0;
            if (brands[i].getClass() == w.getClass() && brands[i].getNumber() == 7)
            {
                labeltally.Text = "+1"; 
            }
        }


        private void Tally_Load(object sender, EventArgs e)
        {
            
            go();
        }
        void sky()
        {

            labeltally.Text = "24";
        }
    }
}