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
            step4();
            step5();
            step6();







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

        void step4()
        {
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
                for (int j = 1; j <= use_characters.Length; j++)
                {
                    if (brands[1].getBrand(i).getNumber() == j)
                        use_characters[j - 1].add(brands[1].getBrand(i));
                }
            }

            //��l�P+80
            for (int i = 0; i < use_characters.Length; i++)
            {
                if (use_characters[i].getCount() >= 3)
                    for (int j = 0; j < use_characters[i].getCount(); j++)
                    {
                        use_characters[i].getBrand(j).Source += 80;
                    }
            }

            //���l�P+80
            for (int i = 0; i < use_characters.Length - 2; i++)
            {
                if (use_characters[i].getCount() >= 1)
                    if (use_characters[i + 1].getCount() >= 1)
                        if (use_characters[i + 2].getCount() >= 1)
                            for (int j = 0; j < use_characters[i].getCount(); j++)
                            {
                                use_characters[i].getBrand(j).Source += 80;
                                use_characters[i + 1].getBrand(j).Source += 80;
                                use_characters[i + 2].getBrand(j).Source += 80;
                            }
            }


            //
            //��
            //
            BrandPlayer[] use_Dots = new BrandPlayer[9];
            for (int j = 0; j < use_Dots.Length; j++)
                use_Dots[j] = new BrandPlayer();
            //�Nbrands�}�C�C�Ӽƪ��ȡA���use_Dots����
            for (int i = 0; i < brands[2].getCount(); i++)
            {
                for (int j = 1; j <= use_Dots.Length; j++)
                {
                    if (brands[2].getBrand(i).getNumber() == j)
                        use_Dots[j - 1].add(brands[2].getBrand(i));
                }
            }

            //��l�P+80
            for (int i = 0; i < use_Dots.Length; i++)
            {
                if (use_Dots[i].getCount() >= 3)
                    for (int j = 0; j < use_Dots[i].getCount(); j++)
                    {
                        use_Dots[i].getBrand(j).Source += 80;
                    }
            }

            //���l�P+80
            for (int i = 0; i < use_Dots.Length - 2; i++)
            {
                if (use_Dots[i].getCount() >= 1)
                    if (use_Dots[i + 1].getCount() >= 1)
                        if (use_Dots[i + 2].getCount() >= 1)
                            for (int j = 0; j < use_Dots[i].getCount(); j++)
                            {
                                use_Dots[i].getBrand(j).Source += 80;
                                use_Dots[i + 1].getBrand(j).Source += 80;
                                use_Dots[i + 2].getBrand(j).Source += 80;
                            }
            }


            //
            //��
            //
            BrandPlayer[] use_Bamboos = new BrandPlayer[9];
            for (int j = 0; j < use_Bamboos.Length; j++)
                use_Bamboos[j] = new BrandPlayer();
            //�Nbrands�}�C�C�Ӽƪ��ȡA���use_Bamboos����
            for (int i = 0; i < brands[3].getCount(); i++)
            {
                for (int j = 1; j < use_Bamboos.Length; j++)
                {
                    if (brands[3].getBrand(i).getNumber() == j)
                        use_Bamboos[j - 1].add(brands[3].getBrand(i));
                }
            }

            //��l�P+80
            for (int i = 0; i < use_Bamboos.Length; i++)
            {
                if (use_Bamboos[i].getCount() >= 3)
                    for (int j = 0; j < use_Bamboos[i].getCount(); j++)
                    {
                        use_Bamboos[i].getBrand(j).Source += 80;
                    }
            }

            //���l�P+80
            for (int i = 0; i < use_Bamboos.Length - 2; i++)
            {
                if (use_Bamboos[i].getCount() >= 1)
                    if (use_Bamboos[i + 1].getCount() >= 1)
                        if (use_Bamboos[i + 2].getCount() >= 1)
                            for (int j = 0; j < use_Bamboos[i].getCount(); j++)
                            {
                                use_Bamboos[i].getBrand(j).Source += 80;
                                use_Bamboos[i + 1].getBrand(j).Source += 80;
                                use_Bamboos[i + 2].getBrand(j).Source += 80;
                            }
            }
        }

        void step5()
        {
            //=====
            //Step5 ��i�P�ۦP(��l)�A�C�i+30
            //=====
            //
            //�U
            //
            BrandPlayer[] use_2characters = new BrandPlayer[9];
            for (int j = 0; j < use_2characters.Length; j++)
                use_2characters[j] = new BrandPlayer();
            //�Nbrands�}�C�C�Ӽƪ��ȡA���use_2characters����
            for (int i = 0; i < brands[1].getCount(); i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (brands[1].getBrand(i).getNumber() == j)
                        use_2characters[j].add(brands[1].getBrand(i));
                }
            }

            for (int i = 0; i < use_2characters.Length; i++)
            {
                if (use_2characters[i].getCount() >= 2)
                    for (int j = 0; j < use_2characters[i].getCount(); j++)
                    {
                        use_2characters[i].getBrand(j).Source += 30;
                    }
            }


            //
            //��
            //
            BrandPlayer[] use_2Dots = new BrandPlayer[9];
            for (int j = 0; j < use_2Dots.Length; j++)
                use_2Dots[j] = new BrandPlayer();
            //�Nbrands�}�C�C�Ӽƪ��ȡA���use_2dots����
            for (int i = 0; i < brands[2].getCount(); i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (brands[2].getBrand(i).getNumber() == j)
                        use_2Dots[j].add(brands[2].getBrand(i));
                }
            }

            for (int i = 0; i < use_2Dots.Length; i++)
            {
                if (use_2Dots[i].getCount() >= 2)
                    for (int j = 0; j < use_2Dots[i].getCount(); j++)
                    {
                        use_2Dots[i].getBrand(j).Source += 30;
                    }
            }


            //
            //��
            //
            BrandPlayer[] use_2Bamboos = new BrandPlayer[9];
            for (int j = 0; j < use_2Bamboos.Length; j++)
                use_2Bamboos[j] = new BrandPlayer();
            //�Nbrands�}�C�C�Ӽƪ��ȡA���use_2Bamboos����
            for (int i = 0; i < brands[2].getCount(); i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (brands[3].getBrand(i).getNumber() == j)
                        use_2Bamboos[j].add(brands[3].getBrand(i));
                }
            }

            for (int i = 0; i < use_2Bamboos.Length; i++)
            {
                if (use_2Bamboos[i].getCount() >= 2)
                    for (int j = 0; j < use_2Bamboos[i].getCount(); j++)
                    {
                        use_2Bamboos[i].getBrand(j).Source += 30;
                    }
            }
        }

        void step6()
        {

            //=====
            //Step6 �H�Ϯ׬��s�աA�@�ժ�⦳9�i�A�U�O�[�W0,5,10,15,20,15,10,5,0
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
            //�U
            //
            BrandPlayer[] step6_characters = new BrandPlayer[9];
            for (int j = 0; j < step6_characters.Length; j++)
                step6_characters[j] = new BrandPlayer();
            //�Nbrands�}�C�C�Ӽƪ��ȡA���use_characters����
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
            //��
            //
            BrandPlayer[] step6_Dots = new BrandPlayer[9];
            for (int j = 0; j < step6_Dots.Length; j++)
                step6_Dots[j] = new BrandPlayer();
            //�Nbrands�}�C�C�Ӽƪ��ȡA���use_characters����
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
            //��
            //
            BrandPlayer[] step6_Bamboos = new BrandPlayer[9];
            for (int j = 0; j < step6_Bamboos.Length; j++)
                step6_Bamboos[j] = new BrandPlayer();
            //�Nbrands�}�C�C�Ӽƪ��ȡA���use_characters����
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
