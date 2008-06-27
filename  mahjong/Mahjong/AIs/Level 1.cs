using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using Mahjong.Brands;
using System.Collections;

namespace Mahjong.AIs
{
    /// <summary>
    /// 定義變數
    /// </summary>
    class Level_1 : MahjongAI
    {
        private BrandPlayer player;
        Brand input_brand;
        Brand ans;
        //FlowerBrand f = new FlowerBrand(0);
        //RopeBrand r = new RopeBrand(0);
        //TenThousandBrand t = new TenThousandBrand(0);
        //TubeBrand tu = new TubeBrand(0);
        //WordBrand w = new WordBrand(0);
        //牌的群組
        BrandPlayer[] brands = new BrandPlayer[5];
        bool firsttime = true;
        /// <summary>
        /// 設定牌組
        /// </summary>
        /// <param name="player"></param>
        public void setPlayer(BrandPlayer player)
        {
            PlayerSort ps = new PlayerSort(player,
                new FlowerBrand(0),
                new TenThousandBrand(0),
                new TubeBrand(0),
                new RopeBrand(0),
                new WordBrand(0));
            this.player = ps.getPlayer;

            for (int j = 0; j < brands.Length; j++)
                brands[j] = new BrandPlayer();
            
            step0();
            step1();
            step2();
            step3();
            step4();
            step5();
            step6();
            step7();
        }
        public void setPlayer(Brand brand, BrandPlayer player)
        {
            PlayerSort ps = new PlayerSort (
                player,
                new FlowerBrand(0),
                new TenThousandBrand(0),
                new TubeBrand(0),
                new RopeBrand(0),
                new WordBrand(0)
                );
            this.player = ps.getPlayer;
            this.input_brand = brand;

            for (int j = 0; j < brands.Length; j++)
                brands[j] = new BrandPlayer();

            step0();
            step1();
            step2();
            step3();
            step4();
            step5();
            step6();
            step7();
        }

        /// <summary>
        /// 丟牌
        /// </summary>
        /// <returns></returns>
        public Brand getReadyBrand()
        {
            ans = player.getBrand(0);
            for (int i = 1; i < player.getCount(); i++)
            {
                if (player.getBrand(i).Source < ans.Source && player.getBrand(i).getClass() != Mahjong.Properties.Settings.Default.Flower)
                    ans = player.getBrand(i);
            }
            //print();
            return ans;
        }
        public BrandPlayer getReadyBrandPlayer()
        {
            return null;
        }

        public bool Chow
        {
            get { return true; }
        }

        public bool Pong
        {
            get { return true; }
        }

        public bool Kong
        {
            get { return true; }
        }

        public bool Win
        {
            get { return true; }
        }

        void step0()
        {
            for (int i = 0; i < player.getCount(); i++)
            {
                //計算各種花色個數
                if (player.getBrand(i).getClass() == Mahjong.Properties.Settings.Default.Flower)
                    brands[0].add(player.getBrand(i));
                else if (player.getBrand(i).getClass() == Mahjong.Properties.Settings.Default.Characters)
                    brands[1].add(player.getBrand(i));
                else if (player.getBrand(i).getClass() == Mahjong.Properties.Settings.Default.Dots)
                    brands[2].add(player.getBrand(i));
                else if (player.getBrand(i).getClass() == Mahjong.Properties.Settings.Default.Bamboos)
                    brands[3].add(player.getBrand(i));
                else if (player.getBrand(i).getClass() == Mahjong.Properties.Settings.Default.Wordtiles)
                    brands[4].add(player.getBrand(i));
                player.getBrand(i).Source = 0;

                //if (player.getBrand(i).getClass() == Mahjong.Properties.Settings.Default.Flower)
                //    player.getBrand(i).Source = 9548;
            }
        }
        void step1()
        {
            //=====
            //Step1 以花色為群組，每張同花色[+(1*同組花色數)]
            //=====
            //以花色為群組，每張同花色[+(1*同組花色數)]
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < brands[i].getCount(); j++)
                    brands[i].getBrand(j).Source += brands[i].getCount();
        }

        void step2()
        {
            //=====
            //Step2 各種花色1分(Default)、青發、紅中、白板10分
            //=====
            for (int i = 0; i < brands[4].getCount(); i++)
            {
                if (brands[4].getBrand(i).getNumber() == 5)
                    brands[4].getBrand(i).Source += 10;
                if (brands[4].getBrand(i).getNumber() == 6)
                    brands[4].getBrand(i).Source += 10;
                if (brands[4].getBrand(i).getNumber() == 7)
                    brands[4].getBrand(i).Source += 10;
            }
        }

        void step3()
        {
            //=====
            //Step3 兩張同字牌+80
            //=====
            //宣告陣列處理[東、南、西、北、白板、青發、紅中]
            addscore(brands[4], 80, 2);
        }

        void step4()
        {
            //=====
            //Step4 刻子或順子之牌，每張+80，並隔離
            //=====
            addscore_順子(brands[1], 80);
            addscore(brands[1], 80, 3);
            addscore_順子(brands[2], 80);
            addscore(brands[2], 80, 3);
            addscore_順子(brands[3], 80);
            addscore(brands[3], 80, 3);

        }

        void step5()
        {
            //=====
            //Step5 兩張牌相同(對子)，每張+30
            //=====
            addscore(brands[1], 30, 2);
            addscore(brands[2], 30, 2);
            addscore(brands[3], 30, 2);
        }

        void step6()
        {

            //=====
            //Step6 以圖案為群組，一組花色有9張，各別加上0,5,10,15,20,15,10,5,0
            //=====
            int[] const_value = new int[9];
            const_value[0] = 0;
            const_value[1] = 5;
            const_value[2] = 10;
            const_value[3] = 15;
            const_value[4] = 20;
            const_value[5] = 15;
            const_value[6] = 10;
            const_value[7] = 5;
            const_value[8] = 0;

            //
            //萬
            //
            BrandPlayer[] step6_characters = new BrandPlayer[9];
            for (int j = 0; j < step6_characters.Length; j++)
                step6_characters[j] = new BrandPlayer();
            //將brands陣列每個數的值，丟到use_characters分類
            for (int i = 0; i < brands[1].getCount(); i++)
            {
                for (int j = 1; j <= step6_characters.Length; j++)
                {
                    if (brands[1].getBrand(i).getNumber() == j)
                        step6_characters[j - 1].add(brands[1].getBrand(i));

                }
            }

            for (int i = 0; i < step6_characters.Length; i++)
            {
                if (step6_characters[i].getCount() >= 1)
                    for (int j = 0; j < step6_characters[i].getCount(); j++)
                        step6_characters[i].getBrand(j).Source += const_value[i];
            }

            //
            //筒
            //
            BrandPlayer[] step6_Dots = new BrandPlayer[9];
            for (int j = 0; j < step6_Dots.Length; j++)
                step6_Dots[j] = new BrandPlayer();
            //將brands陣列每個數的值，丟到use_characters分類
            for (int i = 0; i < brands[2].getCount(); i++)
            {
                for (int j = 1; j <= step6_Dots.Length; j++)
                {
                    if (brands[2].getBrand(i).getNumber() == j)
                        step6_Dots[j - 1].add(brands[2].getBrand(i));

                }
            }

            for (int i = 0; i < step6_Dots.Length; i++)
            {
                if (step6_Dots[i].getCount() >= 1)
                    for (int j = 0; j < step6_Dots[i].getCount(); j++)
                        step6_Dots[i].getBrand(j).Source += const_value[i];
            }

            //
            //條
            //
            BrandPlayer[] step6_Bamboos = new BrandPlayer[9];
            for (int j = 0; j < step6_Bamboos.Length; j++)
                step6_Bamboos[j] = new BrandPlayer();
            //將brands陣列每個數的值，丟到use_characters分類
            for (int i = 0; i < brands[3].getCount(); i++)
            {
                for (int j = 1; j <= step6_Bamboos.Length; j++)
                {
                    if (brands[3].getBrand(i).getNumber() == j)
                        step6_Bamboos[j - 1].add(brands[3].getBrand(i));

                }
            }

            for (int i = 0; i < step6_Bamboos.Length; i++)
            {
                if (step6_Bamboos[i].getCount() >= 1)
                    for (int j = 0; j < step6_Bamboos[i].getCount(); j++)
                        step6_Bamboos[i].getBrand(j).Source += const_value[i];
            }
        }


        void step7()
        {
            //=====
            //Step7 順聽一張+20、聽二張+40，再減兩張牌數值差距(*5)
            //=====
            addscore_for_step7(brands[1]);
            addscore_for_step7(brands[2]);
            addscore_for_step7(brands[3]);
        }

        void addscore_順子(BrandPlayer brandclass, int score)
        {
            BrandPlayer[] temp = new BrandPlayer[9];
            for (int j = 0; j < temp.Length; j++)
                temp[j] = new BrandPlayer();
            //將brands陣列每個數的值，丟到use_Dots分類
            for (int i = 0; i < brandclass.getCount(); i++)
                for (int j = 1; j <= temp.Length; j++)
                    if (brandclass.getBrand(i).getNumber() == j)
                        temp[j - 1].add(brandclass.getBrand(i));

            for (int i = 0; i < temp.Length - 2; i++)
            {
                if (temp[i].getCount() >= 1)
                    if (temp[i + 1].getCount() >= 1)
                        if (temp[i + 2].getCount() >= 1)
                        {
                            for (int j = 0; j < temp[i].getCount(); j++)
                            {
                                temp[i].getBrand(j).Source += score;
                            }
                            for (int j = 0; j < temp[i + 1].getCount(); j++)
                            {
                                temp[i + 1].getBrand(j).Source += score;
                            }
                            for (int j = 0; j < temp[i + 2].getCount(); j++)
                            {
                                temp[i + 2].getBrand(j).Source += score;
                            }
                        }
            }
        }

        void addscore(BrandPlayer brandclass, int score, int compare)
        {
            BrandPlayer[] temp = new BrandPlayer[9];
            for (int j = 0; j < temp.Length; j++)
                temp[j] = new BrandPlayer();
            //將brands陣列每個數的值，丟到use_Dots分類
            for (int i = 0; i < brandclass.getCount(); i++)
                for (int j = 1; j <= temp.Length; j++)
                    if (brandclass.getBrand(i).getNumber() == j)
                        temp[j - 1].add(brandclass.getBrand(i));

            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].getCount() >= compare)
                    for (int j = 0; j < temp[i].getCount(); j++)
                    {
                        temp[i].getBrand(j).Source += score;
                    }
            }
        }

        void addscore_for_step7(BrandPlayer b)
        {
            BrandPlayer[] temp = new BrandPlayer[9];
            for (int j = 0; j < temp.Length; j++)
                temp[j] = new BrandPlayer();
            //將brands陣列每個數的值，丟到step7_characters分類
            for (int i = 0; i < b.getCount(); i++)
            {
                for (int j = 1; j <= temp.Length; j++)
                {
                    if (b.getBrand(i).getNumber() == j)
                        temp[j - 1].add(b.getBrand(i));
                }
            }
            //聽二張為順牌+40，並減掉二張牌差異數(*5)
            for (int i = 1; i < temp.Length - 2; i++)
            {
                if (temp[i - 1].getCount() == 0)
                    if (temp[i].getCount() >= 1)
                        if (temp[i + 1].getCount() >= 1)
                            if (temp[i + 2].getCount() == 0)
                            {
                                for (int j = 0; j < temp[i].getCount(); j++)
                                {
                                    temp[i].getBrand(j).Source += 40;
                                    temp[i].getBrand(j).Source -= 5;
                                }
                                for (int j = 0; j < temp[i + 1].getCount(); j++)
                                {
                                    temp[i + 1].getBrand(j).Source += 40;
                                    temp[i + 1].getBrand(j).Source -= 5;
                                }
                            }
            }
            //聽一張為順牌+20，並減掉二張牌差異數(*5)
            for (int i = 0; i < temp.Length - 2; i++)
            {
                if (temp[i].getCount() >= 1)
                    if (temp[i + 1].getCount() == 0)
                        if (temp[i + 2].getCount() == 1)
                        {
                            for (int j = 0; j < temp[i].getCount(); j++)
                            {
                                temp[i].getBrand(j).Source += 20;
                                temp[i].getBrand(j).Source -= 10;
                            }
                            for (int j = 0; j < temp[i + 2].getCount(); j++)
                            {
                                temp[i + 2].getBrand(j).Source += 20;
                                temp[i + 2].getBrand(j).Source -= 10;
                            }
                        }
            }
            //聽一張為順牌的例外情況：(1、2)和(8、9)
            for (int i = 0; i < temp.Length - 1; i = i + 7)
            {
                if (i <= 1)
                {
                    if (temp[i].getCount() >= 1)
                        if (temp[i + 1].getCount() >= 1)
                            if (temp[i + 2].getCount() == 0)
                            {
                                for (int j = 0; j < temp[i].getCount(); j++)
                                {
                                    temp[i].getBrand(j).Source += 20;
                                    temp[i].getBrand(j).Source -= 5;
                                }
                                for (int j = 0; j < temp[i + 1].getCount(); j++)
                                {
                                    temp[i + 1].getBrand(j).Source += 20;
                                    temp[i + 1].getBrand(j).Source -= 5;
                                }
                            }
                }
                if (i >= 1)
                {
                    if (temp[i - 1].getCount() == 0)
                        if (temp[i].getCount() >= 1)
                            if (temp[i + 1].getCount() >= 1)
                            {
                                for (int j = 0; j < temp[i].getCount(); j++)
                                {
                                    temp[i].getBrand(j).Source += 20;
                                    temp[i].getBrand(j).Source -= 5;
                                }
                                for (int j = 0; j < temp[i+1].getCount(); j++)
                                {
                                    temp[i + 1].getBrand(j).Source += 20;
                                    temp[i + 1].getBrand(j).Source -= 5;
                                }
                            }
                }
            }
        }

        void print()
        {
            Iterator it;
            it = player.creatIterator();
            printplayer(it);
            Console.WriteLine("{0}{1}-({2})", ans.getNumber(), ans.getClass(), ans.Source);
        }
        private void printplayer(Iterator iterator)
        {
            Console.WriteLine();
            while (iterator.hasNext())
            {
                Brand brand = (Brand)iterator.next();
                Console.Write("{0}{1}({2})\t", brand.getNumber(), brand.getClass(), brand.Source);
            }
            Console.WriteLine();
        }
    }
}
