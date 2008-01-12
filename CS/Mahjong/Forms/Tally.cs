using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mahjong.Players;
using Mahjong.Brands;
using Mahjong.Control;

namespace Mahjong.Forms
{
    public partial class Tally : Form
    {
        Players.BrandPlayer x = new BrandPlayer();
        Players.BrandPlayer z = new BrandPlayer();
        int tally = 0;
        int temp = 0;
        bool index = false;
        // 局、莊、方位
        Location l;
        // 連莊次數
        int win_count;
        AllPlayers all;

        public Tally()
        {
            InitializeComponent();
        }
        public void setPlayer(AllPlayers all)
        {
            this.all = all;
            x = all.NowPlayer;
        }
        public void setPlayer(BrandPlayer player)
        {
            x = player;
        }
        /// <summary>
        /// 設定方位
        /// </summary>
        /// <param name="l">方位</param>
        /// <param name="number">連莊次數</param>
        public void setLocation(Location l, int number)
        {
            this.l = l;
            this.win_count = number;
        }
        void go()
        {
            //brands[0].Team = 1;
            //if (brands[0].getClass() == Mahjong.Properties.Settings.Default.Flower) ;

            //for (int i = 0; i < brands.Length; i++)
            //    textBox1.Text = textBox1.Text + brands[i].getNumber() + brands[i].getClass() + " ";
            four();
            three();
            two();
            white();
            green();
            red();
            test();
            bigthree();
            east();
            south();
            west();
            nouth();
            bighappy();
            gangzi();
            samecolor(z);
            oldman();
            //samecarve();
            ponpon();
            //sky();
            labeltally.Text = tally.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ponpon()
        {
            int i = 0, count = 0;
            while (i < z.getCount() - 2)
            {
                if (z.getBrand(i).getClass() == z.getBrand(i + 1).getClass() && z.getBrand(i).getNumber() == z.getBrand(i + 1).getNumber() && z.getBrand(i).getClass() == z.getBrand(i + 2).getClass() && z.getBrand(i).getNumber() == z.getBrand(i + 2).getNumber())
                {
                    if (z.getBrand(i + 3).getClass() == z.getBrand(i).getClass() && z.getBrand(i + 3).getNumber() == z.getBrand(i).getNumber())
                        i++;
                    count++;
                }
                labeltally.Text = tally.ToString();
                if (count == 5)
                {
                    textBox1.Text += "碰碰胡\r\n";
                    tally += 4;
                    break;
                }
                i++;
            }
        }

        void gangzi()
        {
            int count = 0;
            for (int i = 0; i < z.getCount() - 3; i++)

                if (z.getBrand(i).getClass() == z.getBrand(i + 1).getClass() && z.getBrand(i).getNumber() == z.getBrand(i + 1).getNumber() && z.getBrand(i).getClass() == z.getBrand(i + 2).getClass() && z.getBrand(i).getNumber() == z.getBrand(i + 2).getNumber() && z.getBrand(i).getClass() == z.getBrand(i + 3).getClass() && z.getBrand(i).getNumber() == z.getBrand(i + 3).getNumber())
                    count++;

            if (count == 3)
            {
                textBox1.Text += "三槓子\r\n";
                tally += 2;

            }
            else if (count == 4)
            {
                textBox1.Text += "四槓子\r\n";
                tally += 6;

            }


        }
        void green()
        {
            WordBrand w = new WordBrand(0);
            for (int i = 0; i < z.getCount() - 2; i++)

                //if (brands[i].getClass() == w.getClass() && brands[i].getNumber() == 6 && brands[i + 1].getClass() == w.getClass() && brands[i + 1].getNumber() == 6 && brands[i + 2].getClass() == w.getClass() && brands[i + 2].getNumber() == 6)
                if (z.getBrand(i).getClass() == w.getClass() && z.getBrand(i).getNumber() == 6 && z.getBrand(i + 1).getClass() == w.getClass() && z.getBrand(i + 1).getNumber() == 6 && z.getBrand(i + 2).getClass() == w.getClass() && z.getBrand(i + 2).getNumber() == 6)
                {
                    if (z.getBrand(i + 3).getClass() == w.getClass() && z.getBrand(i + 3).getNumber() == 6)
                        i++;
                    //i += 2;
                    //tally += 1;
                    temp += 1;
                    textBox1.Text += "青發\r\n";
                }
            //textBox1.Text += temp.ToString();
            if (temp == 2)
                tally += 1;
            //else if (brands[i+1].getClass() == w.getClass() && brands[i+1].getNumber() == 6 && brands[i + 2].getClass() == w.getClass() && brands[i + 2].getNumber() == 6)
            //{
            //    temp += 2;
            //}

        }
        void red()
        {
            WordBrand w = new WordBrand(0);
            for (int i = 0; i < z.getCount() - 2; i++)
                if (z.getBrand(i).getClass() == w.getClass() && z.getBrand(i).getNumber() == 7 && z.getBrand(i + 1).getClass() == w.getClass() && z.getBrand(i + 1).getNumber() == 7 && z.getBrand(i + 2).getClass() == w.getClass() && z.getBrand(i + 2).getNumber() == 7)
                //if (brands[i].getClass() == w.getClass() && brands[i].getNumber() == 7 && brands[i + 1].getClass() == w.getClass() && brands[i + 1].getNumber() == 7 && brands[i + 2].getClass() == w.getClass() && brands[i + 2].getNumber() == 7)
                {
                    if (z.getBrand(i + 3).getClass() == w.getClass() && z.getBrand(i + 3).getNumber() == 7)
                        i++;
                    //i += 2;
                    //tally += 1;
                    temp += 1;
                    textBox1.Text += "紅中\r\n";
                }
            if (temp == 3)
                tally += 1;
            //else if (brands[i+1].getClass() == w.getClass() && brands[i+1].getNumber() == 7 && brands[i + 2].getClass() == w.getClass() && brands[i + 2].getNumber() == 7)
            //{
            //    temp += 2;
            //}

        }
        void white()
        {
            WordBrand w = new WordBrand(0);
            for (int i = 0; i < z.getCount() - 2; i++)
                if (z.getBrand(i).getClass() == w.getClass() && z.getBrand(i).getNumber() == 5 && z.getBrand(i + 1).getClass() == w.getClass() && z.getBrand(i + 1).getNumber() == 5 && z.getBrand(i + 2).getClass() == w.getClass() && z.getBrand(i + 2).getNumber() == 5)
                //if (brands[i].getClass() == w.getClass() && brands[i].getNumber() == 5 && brands[i + 1].getClass() == w.getClass() && brands[i + 1].getNumber() == 5 && brands[i + 2].getClass() == w.getClass() && brands[i + 2].getNumber() == 5)
                {
                    if (z.getBrand(i + 3).getClass() == w.getClass() && z.getBrand(i + 3).getNumber() == 5)
                        i++;

                    //i += 2;
                    //tally += 1;
                    temp += 1;
                    textBox1.Text += "白板\r\n";
                }
            if (temp == 1)
                tally += 1;
            //else if (brands[i+1].getClass() == w.getClass() && brands[i+1].getNumber() == 5 && brands[i + 2].getClass() == w.getClass() && brands[i + 2].getNumber() == 5)
            //{
            //    temp += 2;
            //}

        }
        void bigthree()
        {
            if (temp == 3)
            {
                textBox1.Text = "大三元\r\n";
                tally += 5;
            }
            else if (temp == 2 && z.getBrand(z.getCount() - 1).getClass() == "字" && (z.getBrand(z.getCount() - 1).getNumber() == 5 || z.getBrand(z.getCount() - 1).getNumber() == 6 || z.getBrand(z.getCount() - 1).getNumber() == 7))
            {
                textBox1.Text = "小三元\r\n";
                tally += 2;
            }
            temp = 0;
        }

        void east()
        {
            WordBrand w = new WordBrand(0);
            for (int i = 0; i < z.getCount() - 2; i++)
                if (z.getBrand(i).getClass() == w.getClass() && z.getBrand(i).getNumber() == 1 && z.getBrand(i + 1).getClass() == w.getClass() && z.getBrand(i + 1).getNumber() == 1 && z.getBrand(i + 2).getClass() == w.getClass() && z.getBrand(i + 2).getNumber() == 1)
                {
                    if (z.getBrand(i + 3).getClass() == w.getClass() && z.getBrand(i + 3).getNumber() == 1)
                        i++;
                    temp += 1;
                }
            //tally += 1;


        }
        void south()
        {
            WordBrand w = new WordBrand(0);
            for (int i = 0; i < z.getCount() - 2; i++)
                if (z.getBrand(i).getClass() == w.getClass() && z.getBrand(i).getNumber() == 2 && z.getBrand(i + 1).getClass() == w.getClass() && z.getBrand(i + 1).getNumber() == 2 && z.getBrand(i + 2).getClass() == w.getClass() && z.getBrand(i + 2).getNumber() == 2)
                {
                    if (z.getBrand(i + 3).getClass() == w.getClass() && z.getBrand(i + 3).getNumber() == 2)
                        i++;
                    temp += 1;
                }
            //tally += 1;

        }
        void west()
        {
            WordBrand w = new WordBrand(0);
            for (int i = 0; i < z.getCount() - 2; i++)
                if (z.getBrand(i).getClass() == w.getClass() && z.getBrand(i).getNumber() == 3 && z.getBrand(i + 1).getClass() == w.getClass() && z.getBrand(i + 1).getNumber() == 3 && z.getBrand(i + 2).getClass() == w.getClass() && z.getBrand(i + 2).getNumber() == 3)
                {
                    if (z.getBrand(i + 3).getClass() == w.getClass() && z.getBrand(i + 3).getNumber() == 3)
                        i++;
                    temp += 1;
                }
            //tally += 1;

        }
        void nouth()
        {
            WordBrand w = new WordBrand(0);
            for (int i = 0; i < z.getCount() - 2; i++)
                if (z.getBrand(i).getClass() == w.getClass() && z.getBrand(i).getNumber() == 4 && z.getBrand(i + 1).getClass() == w.getClass() && z.getBrand(i + 1).getNumber() == 4 && z.getBrand(i + 2).getClass() == w.getClass() && z.getBrand(i + 2).getNumber() == 4)
                {
                    if (z.getBrand(i + 3).getClass() == w.getClass() && z.getBrand(i + 3).getNumber() == 4)
                        i++;
                    temp += 1;
                }
            //tally += 1;

        }
        void bighappy()
        {
            if (temp == 4)
            {
                textBox1.Text = "大四喜\r\n";
                tally += 16;
            }
            else if (temp == 3)
            {
                textBox1.Text = "小四喜\r\n";
                tally += 8;
            }
            temp = 0;
        }

        void samecolor(BrandPlayer q)
        {
            int num = 0;
            bool index = false;
            for (int i = 0; i < q.getCount(); i++)
            {
                if (q.getBrand(i).getClass() == "字")
                    num++;
            }
            if (num == q.getCount())
            {
                textBox1.Text += "字一色\r\n";
                tally += 8;
            }
            num = 0;

            for (int i = 0; i < q.getCount() - 1; i++)
                if (q.getBrand(i).getClass() == q.getBrand(i + 1).getClass() && q.getBrand(i).getClass() != "字")
                    num++;
            if (num == q.getCount() - 1)
            {
                textBox1.Text += "清一色\r\n";
                tally += 8;
            }
            num = 0;

            /*  for (int i = 0; i < q.getCount(); i++)
              {
                  if (q.getBrand(i).getClass() == "字")
                  {   
                      index=true;
                      q.remove(q.getBrand(i));
                      i-=1;
                  }
              }
              if(index==true)
              {
                  for (int i = 0; i < q.getCount() - 1; i++)
                  {
                      if (q.getBrand(i).getClass() == q.getBrand(i + 1).getClass() && q.getBrand(i).getClass() != "字")
                          num++;
                  }       
                      if (num == q.getCount() - 1)
                      {
                          textBox1.Text += "混一色\r\n";
                          tally += 8;
                      }
                          num = 0;
              }*/
        }

        void oldman()
        {
            int num = 0;
            for (int i = 0; i < z.getCount(); i++)
            {
                if (z.getBrand(i).getClass() != "字" && z.getBrand(i).getNumber() != 1 && z.getBrand(i).getNumber() != 9)
                    num++;
            }
            if (num == z.getCount())
            {
                textBox1.Text += "斷么九\r\n";
                tally += 1;
            }
            num = 0;

            for (int i = 0; i < z.getCount(); i++)
                if (z.getBrand(i).getNumber() == 1 || z.getBrand(i).getNumber() == 9)
                    num++;
            if (num == z.getCount())
            {
                textBox1.Text += "清老頭\r\n";
                tally += 8;
            }
            num = 0;

            for (int i = 0; i < z.getCount(); i++)
            {
                if (z.getBrand(i).getClass() == "字" || z.getBrand(i).getNumber() == 1 || z.getBrand(i).getNumber() == 9)
                    num++;
            }
            if (num == z.getCount())
            {
                textBox1.Text += "混老頭\r\n";
                tally += 4;
            }
            num = 0;
        }

        void samecarve()
        {
            int num = 0;
            for (int i = 0; i < z.getCount() - 2; i++)
                if (z.getBrand(i).getNumber() == z.getBrand(i + 1).getNumber() && z.getBrand(i).getNumber() == z.getBrand(i + 2).getNumber() && (((z.getBrand(i).getClass() == "萬") && z.getBrand(i).getClass() == "筒") && z.getBrand(i).getClass() == "條"))
                    num++;
            if (num == 8)
            {
                textBox1.Text += "三色同刻\r\n";
                tally += 2;
            }
        }

        void sky()
        {

            labeltally.Text = "16";
        }

        private void Tally_Load(object sender, EventArgs e)
        {
            go();
        }

        void four()
        {

            if (x.getCount() > 17)
                for (int i = 0; i < x.getCount() - 3; i++)
                {
                    if (x.getBrand(i).getClass() == x.getBrand(i + 1).getClass() && x.getBrand(i + 2).getClass() == x.getBrand(i).getClass() && x.getBrand(i + 3).getClass() == x.getBrand(i).getClass() && x.getBrand(i).getNumber() == x.getBrand(i + 1).getNumber() && x.getBrand(i + 2).getNumber() == x.getBrand(i).getNumber() && x.getBrand(i + 3).getNumber() == x.getBrand(i).getNumber())
                    {

                        for (int j = 0; j < 4; j++)
                        {
                            z.add(x.getBrand(i));
                            x.remove(x.getBrand(i));
                        }
                        i -= 1;
                    }
                }
        }
        void three()
        {
            for (int i = 0; i < x.getCount() - 2; i++)
            {
                if (x.getBrand(i).getClass() == x.getBrand(i + 1).getClass() && x.getBrand(i + 2).getClass() == x.getBrand(i).getClass() && x.getBrand(i).getNumber() == x.getBrand(i + 1).getNumber() && x.getBrand(i + 2).getNumber() == x.getBrand(i).getNumber())
                {

                    for (int j = 0; j < 3; j++)
                    {
                        z.add(x.getBrand(i));
                        x.remove(x.getBrand(i));
                    }
                    i -= 1;
                }
            }

        }
        void two()
        {
            for (int i = 0; i < 2; i++)
            {
                z.add(x.getBrand(0));
                x.remove(x.getBrand(0));
            }
        }
        void test()
        {
            for (int i = 0; i < z.getCount(); i++)
                textBox1.Text += z.getBrand(i).getNumber() + z.getBrand(i).getClass() + " ";

        }
    }
}


