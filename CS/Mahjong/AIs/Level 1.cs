using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using Mahjong.Brands;
using System.Collections;

namespace Mahjong.AIs
{
    /// <summary>
    /// �w�q�ܼ�
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
                //�P���s��
                BrandPlayer[] brands = new BrandPlayer[5];
        /// <summary>
        /// �]�w�P��
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
        /// ��P
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
        //Step4 ��l�ζ��l���P�A�C�i+80�A�ùj��
        //=====
            //
            //�U
            //
            BrandPlayer[] use_characters = new BrandPlayer[9];
            for (int j = 0; j < use_characters.Length; j++)
                use_characters[j] = new BrandPlayer();
            //�Nbrands�}�C�C�Ӽƪ��ȡA���use_characters����
            for (int i = 0; i < brands[1].getCount(); i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (brands[1].getBrand(i).getNumber() == j)
                        use_characters[j].add(brands[1].getBrand(i));
                }
            }

            //��l�P+80
            for (int i = 0; i < 9; i++)
            {
                if (use_characters[i].getCount() >= 3)
                    for (int j = 0; j < use_characters[i].getCount(); j++)
                    {
                        use_characters[i].getBrand(j).Source += 80;
                    }
            }

            //���l�P+

            //
            //��
            //
            BrandPlayer[] use_Dots = new BrandPlayer[9];
            for (int j = 0; j < use_Dots.Length; j++)
                use_Dots[j] = new BrandPlayer();
            //�Nbrands�}�C�C�Ӽƪ��ȡA���use_characters����
            for (int i = 0; i < brands[1].getCount(); i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (brands[1].getBrand(i).getNumber() == j)
                        use_Dots[j].add(brands[1].getBrand(i));
                }
            }

            //��l�P+80
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
            //Step2 �H��⬰�s�աA�C�i�P���[+(1*�P�ժ���)]
            //=====

            for (int i = 0; i < player.getCount(); i++)
            {
                //�p��U�ت��Ӽ�
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

            //�H��⬰�s�աA�C�i�P���[+(1*�P�ժ���)]
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < brands[i].getCount(); j++)
                    brands[i].getBrand(j).Source += brands[i].getCount();
        }

        void step1()
        {
            //=====
            //Step1 �U�ت��1��(Default)�B�C�o�B�����B�ժO10��
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
            //Step3 ��i�P�r�P+80
            //=====
            //�ŧi�}�C�B�z[�F�B�n�B��B�_�B�ժO�B�C�o�B����]
            BrandPlayer[] use_wordlist = new BrandPlayer[7];
            for (int j = 0; j < use_wordlist.Length; j++)
                use_wordlist[j] = new BrandPlayer();

            //�Nbrands�}�C�C�Ӽƪ��ȡA���use_wordlist����
            for (int i = 0; i < brands[4].getCount(); i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (brands[4].getBrand(i).getNumber() == j)
                        use_wordlist[j].add(brands[4].getBrand(i));

                }
            }

            //�P�r�P+80
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
