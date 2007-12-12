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
                Brand ans;
                FlowerBrand f = new FlowerBrand(0);
                RopeBrand r = new RopeBrand(0);
                TenThousandBrand t = new TenThousandBrand(0);
                TubeBrand tu = new TubeBrand(0);
                WordBrand w = new WordBrand(0);
                //牌的群組
                BrandPlayer[] brands = new BrandPlayer[5];
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
            this.player = ps.getPlayer();

            print();
        }

        /// <summary>
        /// 丟牌
        /// </summary>
        /// <returns></returns>
        public Brand getReadyBrand()
        {
            
            for (int j = 0; j < brands.Length; j++)
                brands[j] = new BrandPlayer();

            step2();
            step1();
            step3();

        //=====
        //Step4 對子或順子之牌，每張+80，並隔離
        //=====
            //
            //萬
            //
            BrandPlayer[] use_characters = new BrandPlayer[9];
            for (int j = 0; j < use_characters.Length; j++)
                use_characters[j] = new BrandPlayer();
            //將brands陣列每個數的值，丟到use_characters分類
            for (int i = 0; i < brands[1].getCount(); i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (brands[1].getBrand(i).getNumber() == j)
                        use_characters[j].add(brands[1].getBrand(i));
                }
            }

            //對子牌+80
            for (int i = 0; i < 9; i++)
            {
                if (use_characters[i].getCount() >= 3)
                    for (int j = 0; j < use_characters[i].getCount(); j++)
                    {
                        use_characters[i].getBrand(j).Source += 80;
                    }
            }

            //順子牌+

            //
            //筒
            //
            BrandPlayer[] use_Dots = new BrandPlayer[9];
            for (int j = 0; j < use_Dots.Length; j++)
                use_Dots[j] = new BrandPlayer();
            //將brands陣列每個數的值，丟到use_characters分類
            for (int i = 0; i < brands[1].getCount(); i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (brands[1].getBrand(i).getNumber() == j)
                        use_Dots[j].add(brands[1].getBrand(i));
                }
            }

            //對子牌+80
            for (int i = 0; i < 9; i++)
            {
                if (use_Dots[i].getCount() >= 3)
                    for (int j = 0; j < use_Dots[i].getCount(); j++)
                    {
                        use_Dots[i].getBrand(j).Source += 80;
                    }
            }


            print();
                    return ans;
        }

        void step2()
        { 
            //=====
            //Step2 以花色為群組，每張同花色[+(1*同組花色數)]
            //=====

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
            }

            //以花色為群組，每張同花色[+(1*同組花色數)]
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < brands[i].getCount(); j++)
                    brands[i].getBrand(j).Source += brands[i].getCount();
        }

        void step1()
        {
            //=====
            //Step1 各種花色1分(Default)、青發、紅中、白板10分
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
            BrandPlayer[] use_wordlist = new BrandPlayer[7];
            for (int j = 0; j < use_wordlist.Length; j++)
                use_wordlist[j] = new BrandPlayer();

            //將brands陣列每個數的值，丟到use_wordlist分類
            for (int i = 0; i < brands[4].getCount(); i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (brands[4].getBrand(i).getNumber() == j)
                        use_wordlist[j].add(brands[4].getBrand(i));

                }
            }

            //同字牌+80
            for (int i = 0; i < 7; i++)
            {
                if (use_wordlist[i].getCount() >= 2)
                    for (int j = 0; j < use_wordlist[i].getCount(); j++)
                    {
                        use_wordlist[i].getBrand(j).Source += 80;
                    }
            }

        }

        void print()
        {
            Iterator it;
            it = player.creatIterator();
            printplayer(it);
            
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
