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
        Players.BrandPlayer y = new BrandPlayer();
        Players.BrandPlayer r = new BrandPlayer();
        int tally = 0;
        int temp = 0;
        bool index = false;
        // 局、莊、方位
        Location l;
        // 連莊次數
        int win_count;
        AllPlayers all;
        int sume=(int)Mahjong.Properties.Settings.Default.Money,
            sums=(int)Mahjong.Properties.Settings.Default.Money,
            sumw=(int)Mahjong.Properties.Settings.Default.Money,
            sumn=(int)Mahjong.Properties.Settings.Default.Money;
           
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
            removeflower();
            //winagain();
            //four();
            //three();
            //two();
            flower();
            white();
            green();
            red();
            bigthree();
            east();
            south();
            west();
            nouth();
            bighappy();
            gangzi();
            samecolor(y);
            oldman();
            //samecarve();
            ponpon();
            //lose();
            //winmyself();
            //sky();
            allplayer();
            scoree();
            scores();
            scorew();
            scoren();
            sum(scoree(),scores(),scorew(),scoren());
            labeltally.Text = tally.ToString();
            //test();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ponpon()
        {
            int i = 0, count = 0;
            while (i < y.getCount() - 2)
            {
                if (y.getBrand(i).getClass() == y.getBrand(i + 1).getClass() && y.getBrand(i).getNumber() == y.getBrand(i + 1).getNumber() && y.getBrand(i).getClass() == y.getBrand(i + 2).getClass() && y.getBrand(i).getNumber() == y.getBrand(i + 2).getNumber())
                {
                    if (y.getBrand(i + 3).getClass() == y.getBrand(i).getClass() && y.getBrand(i + 3).getNumber() == y.getBrand(i).getNumber())
                        i++;
                    count++;
                }
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
            for (int i = 0; i < y.getCount() - 3; i++)

                if (y.getBrand(i).getClass() == y.getBrand(i + 1).getClass() && y.getBrand(i).getNumber() == y.getBrand(i + 1).getNumber() && y.getBrand(i).getClass() == y.getBrand(i + 2).getClass() && y.getBrand(i).getNumber() == y.getBrand(i + 2).getNumber() && y.getBrand(i).getClass() == y.getBrand(i + 3).getClass() && y.getBrand(i).getNumber() == y.getBrand(i + 3).getNumber())
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
            for (int i = 0; i < y.getCount() - 2; i++)

                //if (brands[i].getClass() == w.getClass() && brands[i].getNumber() == 6 && brands[i + 1].getClass() == w.getClass() && brands[i + 1].getNumber() == 6 && brands[i + 2].getClass() == w.getClass() && brands[i + 2].getNumber() == 6)
                if (y.getBrand(i).getClass() == w.getClass() && y.getBrand(i).getNumber() == 6 && y.getBrand(i + 1).getClass() == w.getClass() && y.getBrand(i + 1).getNumber() == 6 && y.getBrand(i + 2).getClass() == w.getClass() && y.getBrand(i + 2).getNumber() == 6)
                {
                    if (y.getBrand(i + 3).getClass() == w.getClass() && y.getBrand(i + 3).getNumber() == 6)
                        i++;
                    //i += 2;
                    tally += 1;
                    temp += 1;
                    textBox1.Text += "青發\r\n";
                }
                else if (y.getBrand(i).getClass() == w.getClass() && y.getBrand(i).getNumber() == 6 && y.getBrand(i + 1).getClass() == w.getClass() && y.getBrand(i + 1).getNumber() == 6)
                {
                    index = true;
                }
            //textBox1.Text += temp.ToString();
           // if (temp == 2)
             //   tally += 1;
            //else if (brands[i+1].getClass() == w.getClass() && brands[i+1].getNumber() == 6 && brands[i + 2].getClass() == w.getClass() && brands[i + 2].getNumber() == 6)
            //{
            //    temp += 2;
            //}

        }
        void red()
        {
            WordBrand w = new WordBrand(0);
            for (int i = 0; i < y.getCount() - 2; i++)
                if (y.getBrand(i).getClass() == w.getClass() && y.getBrand(i).getNumber() == 7 && y.getBrand(i + 1).getClass() == w.getClass() && y.getBrand(i + 1).getNumber() == 7 && y.getBrand(i + 2).getClass() == w.getClass() && y.getBrand(i + 2).getNumber() == 7)
                //if (brands[i].getClass() == w.getClass() && brands[i].getNumber() == 7 && brands[i + 1].getClass() == w.getClass() && brands[i + 1].getNumber() == 7 && brands[i + 2].getClass() == w.getClass() && brands[i + 2].getNumber() == 7)
                {
                    if (y.getBrand(i + 3).getClass() == w.getClass() && y.getBrand(i + 3).getNumber() == 7)
                        i++;
                    //i += 2;
                    tally += 1;
                    temp += 1;
                    textBox1.Text += "紅中\r\n";
                }
                else if (y.getBrand(i).getClass() == w.getClass() && y.getBrand(i).getNumber() == 7 && y.getBrand(i + 1).getClass() == w.getClass() && y.getBrand(i + 1).getNumber() == 7)
                {
                    index = true;
                }
            //if (temp == 3)
              //  tally += 1;
            //else if (brands[i+1].getClass() == w.getClass() && brands[i+1].getNumber() == 7 && brands[i + 2].getClass() == w.getClass() && brands[i + 2].getNumber() == 7)
            //{
            //    temp += 2;
            //}

        }
        void white()
        {
            WordBrand w = new WordBrand(0);
            for (int i = 0; i < y.getCount() - 2; i++)
                if (y.getBrand(i).getClass() == w.getClass() && y.getBrand(i).getNumber() == 5 && y.getBrand(i + 1).getClass() == w.getClass() && y.getBrand(i + 1).getNumber() == 5 && y.getBrand(i + 2).getClass() == w.getClass() && y.getBrand(i + 2).getNumber() == 5)
                //if (brands[i].getClass() == w.getClass() && brands[i].getNumber() == 5 && brands[i + 1].getClass() == w.getClass() && brands[i + 1].getNumber() == 5 && brands[i + 2].getClass() == w.getClass() && brands[i + 2].getNumber() == 5)
                {
                    if (y.getBrand(i + 3).getClass() == w.getClass() && y.getBrand(i + 3).getNumber() == 5)
                        i++;

                    //i += 2;
                    tally += 1;
                    temp += 1;
                    textBox1.Text += "白板\r\n";
                }
                else if (y.getBrand(i).getClass() == w.getClass() && y.getBrand(i).getNumber() == 5 && y.getBrand(i + 1).getClass() == w.getClass() && y.getBrand(i + 1).getNumber() == 5)
                {
                    index = true;
                }
            //if (temp == 1)
              //  tally += 1;
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
            else if (temp == 2 && index==true)
            {
                textBox1.Text = "小三元\r\n";
                tally += 2;
            }
            temp = 0;
            index = false;
        }

        void east()
        {
            WordBrand w = new WordBrand(0);
            for (int i = 0; i < y.getCount() - 2; i++)
                if (y.getBrand(i).getClass() == w.getClass() && y.getBrand(i).getNumber() == 1 && y.getBrand(i + 1).getClass() == w.getClass() && y.getBrand(i + 1).getNumber() == 1 && y.getBrand(i + 2).getClass() == w.getClass() && y.getBrand(i + 2).getNumber() == 1)
                {
                    if (y.getBrand(i + 3).getClass() == w.getClass() && y.getBrand(i + 3).getNumber() == 1)
                        i++;
                        temp += 1;
                        if (all.State == location.East)
                        {
                            textBox1.Text += "東風位\r\n";
                            tally += 1;
                        }
                        if (l.Round == location.East)
                        {
                            textBox1.Text += "東風局\r\n";
                            tally += 1;
                        }

                }
                else if (y.getBrand(i).getClass() == w.getClass() && y.getBrand(i).getNumber() == 1 && y.getBrand(i + 1).getClass() == w.getClass() && y.getBrand(i + 1).getNumber() == 1)
                {
                    index = true;
                }
                                
            //tally += 1;


        }
        void south()
        {
            WordBrand w = new WordBrand(0);
            for (int i = 0; i < y.getCount() - 2; i++)
                if (y.getBrand(i).getClass() == w.getClass() && y.getBrand(i).getNumber() == 2 && y.getBrand(i + 1).getClass() == w.getClass() && y.getBrand(i + 1).getNumber() == 2 && y.getBrand(i + 2).getClass() == w.getClass() && y.getBrand(i + 2).getNumber() == 2)
                {
                    if (y.getBrand(i + 3).getClass() == w.getClass() && y.getBrand(i + 3).getNumber() == 2)
                        i++;
                    temp += 1;
                    if (all.State == location.South)
                    {
                        textBox1.Text += "南風位\r\n";
                        tally += 1;
                    }
                    if (l.Round == location.South)
                    {
                        textBox1.Text += "南風局\r\n";
                        tally += 1;
                    }
                }
                else if (y.getBrand(i).getClass() == w.getClass() && y.getBrand(i).getNumber() == 2 && y.getBrand(i + 1).getClass() == w.getClass() && y.getBrand(i + 1).getNumber() == 2)
                {
                    index = true;
                }
            //tally += 1;

        }
        void west()
        {
            WordBrand w = new WordBrand(0);
            for (int i = 0; i < y.getCount() - 2; i++)
                if (y.getBrand(i).getClass() == w.getClass() && y.getBrand(i).getNumber() == 3 && y.getBrand(i + 1).getClass() == w.getClass() && y.getBrand(i + 1).getNumber() == 3 && y.getBrand(i + 2).getClass() == w.getClass() && y.getBrand(i + 2).getNumber() == 3)
                {
                    if (y.getBrand(i + 3).getClass() == w.getClass() && y.getBrand(i + 3).getNumber() == 3)
                        i++;
                    temp += 1;
                    if (all.State == location.West)
                    {
                        textBox1.Text += "西風位\r\n";
                        tally += 1;
                    }
                    if (l.Round == location.West)
                    {
                        textBox1.Text += "西風局\r\n";
                        tally += 1;
                    }
                }
                else if (y.getBrand(i).getClass() == w.getClass() && y.getBrand(i).getNumber() == 3 && y.getBrand(i + 1).getClass() == w.getClass() && y.getBrand(i + 1).getNumber() == 3)
                {
                    index = true;
                }
            //tally += 1;

        }
        void nouth()
        {
            WordBrand w = new WordBrand(0);
            for (int i = 0; i < y.getCount() - 2; i++)
                if (y.getBrand(i).getClass() == w.getClass() && y.getBrand(i).getNumber() == 4 && y.getBrand(i + 1).getClass() == w.getClass() && y.getBrand(i + 1).getNumber() == 4 && y.getBrand(i + 2).getClass() == w.getClass() && y.getBrand(i + 2).getNumber() == 4)
                {
                    if (y.getBrand(i + 3).getClass() == w.getClass() && y.getBrand(i + 3).getNumber() == 4)
                        i++;
                    temp += 1;
                    if (all.State == location.North)
                    {
                        textBox1.Text += "北風位\r\n";
                        tally += 1;
                    }
                    if (l.Round == location.North)
                    {
                        textBox1.Text += "北風局\r\n";
                        tally += 1;
                    }
                }
                else if (y.getBrand(i).getClass() == w.getClass() && y.getBrand(i).getNumber() == 4 && y.getBrand(i + 1).getClass() == w.getClass() && y.getBrand(i + 1).getNumber() == 4)
                {
                    index = true;
                }
            //tally += 1;

        }
        void bighappy()
        {
            if (temp == 4)
            {
                textBox1.Text += "大四喜\r\n";
                tally += 16;
            }
            else if (temp == 3 && index==true)
            {
                textBox1.Text += "小四喜\r\n";
                tally += 8;
            }
            temp = 0;
            index = false;
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

              /*for (int i = 0; i < q.getCount(); i++)
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
            for (int i = 0; i < y.getCount(); i++)
            {
                if (y.getBrand(i).getClass() != "字" && y.getBrand(i).getNumber() != 1 && y.getBrand(i).getNumber() != 9)
                    num++;
            }
            if (num == y.getCount())
            {
                textBox1.Text += "斷么九\r\n";
                tally += 1;
            }
            num = 0;

            for (int i = 0; i < y.getCount(); i++)
                if (y.getBrand(i).getNumber() == 1 || y.getBrand(i).getNumber() == 9)
                    num++;
            if (num == y.getCount())
            {
                textBox1.Text += "清老頭\r\n";
                tally += 8;
            }
            num = 0;

            /*for (int i = 0; i < y.getCount(); i++)
            {
                if (y.getBrand(i).getClass() == "字" || y.getBrand(i).getNumber() == 1 || y.getBrand(i).getNumber() == 9)
                    num++;
            }
            if (num == y.getCount())
            {
                textBox1.Text += "混老頭\r\n";
                tally += 4;
            }
            num = 0;*/
        }

        void samecarve()
        {
            int num = 0;
            for (int i = 0; i < y.getCount() - 2; i++)
                if (y.getBrand(i).getNumber() == y.getBrand(i + 1).getNumber() && y.getBrand(i).getNumber() == y.getBrand(i + 2).getNumber() && (((y.getBrand(i).getClass() == "萬") && y.getBrand(i).getClass() == "筒") && y.getBrand(i).getClass() == "條"))
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

        void removeflower()
        {
            for (int i = 0; i < x.getCount(); i++)
            {
                if (x.getBrand(i).getClass() != "花")
                {
                    y.add(x.getBrand(i));
                }
                else
                {
                    r.add(x.getBrand(i));
                }
            }
        }

        void flower()
        {
            FlowerBrand f = new FlowerBrand(0);
            if(all.State == l.Position)
            {
                if (f.getNumber() == 1)
                {
                    textBox1.Text += "1花\r\n";
                    tally += 1;
                }
                if (f.getNumber() == 5)
                {
                    textBox1.Text += "5花\r\n";
                    tally += 1;
                }
            }
            else if (all.State == l.nextPosition)
            {
                if (f.getNumber() == 2)
                {
                    textBox1.Text += "2花\r\n";
                    tally += 1;
                }
                if (f.getNumber() == 6)
                {
                    textBox1.Text += "6花\r\n";
                    tally += 1;
                }
            }
            else if (all.State == l.nextPosition)
            {
                if (f.getNumber() == 3)
                {
                    textBox1.Text += "3花\r\n";
                    tally += 1;
                }
                if (f.getNumber() == 7)
                {
                    textBox1.Text += "7花\r\n";
                    tally += 1;
                }
            }
            else if (all.State == l.nextPosition)
            {
                if (f.getNumber() == 4)
                {
                    textBox1.Text += "4花\r\n";
                    tally += 1;
                }
                if (f.getNumber() == 8)
                {
                    textBox1.Text += "8花\r\n";
                    tally += 1;
                }
            }
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
            for (int i = 0; i < y.getCount(); i++)
                textBox1.Text += y.getBrand(i).getNumber() + y.getBrand(i).getClass() + " ";

        }

        void allplayer()
        {
            player1.Text = all.Name[(int)location.East];
            player2.Text = all.Name[(int)location.South];
            player3.Text = all.Name[(int)location.West];
            player4.Text = all.Name[(int)location.North];
        }

       void sum(int es,int ss,int ws,int ns)
       {
           
           sume += es;
           sums += ss;
           sumw += ws;
           sumn += ns;
           sum1.Text = sume.ToString();
           sum2.Text = sums.ToString();
           sum3.Text = sumw.ToString();
           sum4.Text = sumn.ToString();
       }

        int scoree()
        {
            int es = 0;
            //t= (all.basic_tai + tally) * 10;
            if (all.State == location.East)
            {
                es = (all.basic_tai + tally) * 10;
                score1.Text = es.ToString();
            }
            else if (all.Show_Table.getBrand(all.Show_Table.getCount() - 1).WhoPush == location.East)
            {
                es = (all.basic_tai + tally) * -10;
                score1.Text = es.ToString();
            }
            else if(all.State==location.East)
            {
                es = (all.basic_tai + tally) * 10;
                score1.Text = es.ToString();
            }
            score1.Text = es.ToString();
            return es;
        }

        int scores()
        {
            int ss = 0;// t = (all.basic_tai + tally) * 10;
            if (all.State == location.South && all.Show_Table.getBrand(all.Show_Table.getCount() - 1).WhoPush != location.South)
            {
                ss = (all.basic_tai + tally) * 10;
                score2.Text = ss.ToString();
            }
            else if (all.Show_Table.getBrand(all.Show_Table.getCount() - 1).WhoPush == location.South)
            {
                ss = (all.basic_tai + tally) * -10;
                score2.Text = ss.ToString();
            }
            else if (all.State == location.South)
            {
                ss = (all.basic_tai + tally) * 10;
                score2.Text = ss.ToString();
            }
            score2.Text = ss.ToString();

            return ss;
        }

        int scorew()
        {
            int ws = 0;// t = (all.basic_tai + tally) * 10;
            if (all.State == location.West && all.Show_Table.getBrand(all.Show_Table.getCount() - 1).WhoPush != location.West)
            {
                ws = (all.basic_tai + tally) * 10;
                score3.Text = ws.ToString();
            }
            else if (all.Show_Table.getBrand(all.Show_Table.getCount() - 1).WhoPush == location.West)
            {
                ws = (all.basic_tai + tally) * -10;
                score3.Text = ws.ToString();
            }
            else if (all.State == location.West)
            {
                ws = (all.basic_tai + tally) * 10;
                score3.Text = ws.ToString();
            }
           
            score3.Text = ws.ToString();
            return ws;
        }

        int scoren()
        {
            int ns = 0;// t = (all.basic_tai + tally) * 10;
            if (all.State == location.North && all.Show_Table.getBrand(all.Show_Table.getCount() - 1).WhoPush != location.North)
            {
                ns = (all.basic_tai + tally) * 10;
                score4.Text = ns.ToString();
            }
            else if (all.Show_Table.getBrand(all.Show_Table.getCount() - 1).WhoPush == location.North)
            {
                ns = (all.basic_tai + tally) * -10;
                score4.Text = ns.ToString();
            }
            else if (all.State == location.North)
            {
                ns = (all.basic_tai + tally) * 10;
                score4.Text = ns.ToString();
            }
            score4.Text = ns.ToString();
            return ns;
        }


        void winagain()
        {
            string s="";
            if (l.Position == l.Winer && all.Win_Times > 1)
            {
                s = "莊家連" + win_count.ToString() + "拉" + win_count.ToString() + "\r\n";
                textBox1.Text += s;
                tally += win_count * 2;
            }                
        }

        bool winmyself()
        {
            if (all.State == l.Winer)
            {
                textBox1.Text += "莊家\r\n";
                tally += 1;
                return true;
            }
            return false;
        }

        bool lose()
        { 
            if(all.Show_Table.getBrand(all.Show_Table.getCount()-1).WhoPush == l.Winer)
            {
                textBox1.Text += "莊家放槍\r\n";
                tally -= 1;
                return true;
            }
            return false;
        }
    }
}


