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
        private BrandPlayer p0;
                Brand[] array1;
                ArrayList flower;
                ArrayList rope;
                ArrayList ten;
                ArrayList tube;
                ArrayList word;
                Brand ans;
                FlowerBrand f = new FlowerBrand(0);
                RopeBrand r = new RopeBrand(0);
                TenThousandBrand t = new TenThousandBrand(0);
                TubeBrand tu = new TubeBrand(0);
                WordBrand w = new WordBrand(0);
                int iflower=0;
                int irope=0;
                int iten=0;
                int itube=0;
                int iword=0;

        /// <summary>
        /// 設定牌組
        /// </summary>
        /// <param name="player"></param>
        public void setPlayer(BrandPlayer player)
        {
            this.p0 = player;
            array1 = new Brand[p0.getCount()];
            flower = new ArrayList();
            rope = new ArrayList();
            ten = new ArrayList();
            tube = new ArrayList();
            word = new ArrayList();

            for (int i = 0; i < p0.getCount(); i++)
                array1[i] = p0.getBrand(i);

            sort();
            printtest(flower);
            printtest(rope);
            printtest(ten);
            printtest(tube);
            printtest(word);
        }

        /// <summary>
        /// 摸牌
        /// </summary>
        /// <returns></returns>
        public Brand getReadyBrand()
        {
            return ans;
        }

        /// <summary>
        /// 手牌分類
        /// </summary>
        private void sort()
        {
            for (int i = 0; i < p0.getCount(); i++)
            {
                if (array1[i].getClass() == f.getClass())
                {
                    flower.Add(array1[i]);
                }

                else if (array1[i].getClass() == r.getClass())
                {
                    rope.Add(array1[i]);
                }

                else if (array1[i].getClass() == t.getClass())
                {
                    ten.Add(array1[i]);
                }

                else if (array1[i].getClass() == tu.getClass())
                {
                    tube.Add(array1[i]);
                }

                else if (array1[i].getClass() == w.getClass())
                {
                    word.Add(array1[i]);
                }

                sortArray(flower);
                sortArray(rope);
                sortArray(ten);
                sortArray(tube);
                sortArray(word);
            }
        }

        /// <summary>
        /// 手牌排序
        /// </summary>
        /// <param name="BrandTemp"></param>
        void sortArray(ArrayList BrandTemp)
        {
            int i;
            int j;
            Object temp;
            if (BrandTemp.Count > 1)
            {
                for (i = (BrandTemp.Count - 1); i >= 0; i--)
                {
                    for (j = 1; j <= i; j++)
                    {
                        Brand first=(Brand)BrandTemp[j-1];
                        Brand secend=(Brand)BrandTemp[j];
                        if (first.getNumber() > secend.getNumber())
                        {
                            temp = BrandTemp[j - 1];
                            BrandTemp[j - 1] = BrandTemp[j];
                            BrandTemp[j] = temp;
                        }
                    }
                }
            }
        }

        void printtest(ArrayList test)
        {
            if (test.Count > 0)
                for (int i = 0; i < test.Count; i++)
                {
                    Brand temp=(Brand)test[i];
                    Console.WriteLine("print : {0},{1}", temp.getClass(), temp.getNumber());
                }
        }
    }
}
