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
        Brand[] brands;
        FlowerBrand f = new FlowerBrand(0);
        RopeBrand r = new RopeBrand(0);
        TenThousandBrand ten = new TenThousandBrand(0);
        TubeBrand tube = new TubeBrand(0);
        WordBrand w = new WordBrand(0);
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
        public void setLocation(string s1,string s2,string s3,int number)
        {
            if (s1 == Mahjong.Properties.Settings.Default.East)
                ;
        }
        void go()
        {
            brands[0].team=1;
            if (brands[0].getClass()==f.getClass())
                ;
        }
    }
}