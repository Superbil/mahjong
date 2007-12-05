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
            //�P�_��i�r�P
            BrandPlayer[] tp = new BrandPlayer[7];
            for (int j = 0; j < tp.Length; j++)
                tp[j] = new BrandPlayer();

            for (int i = 0; i < player.getCount(); i++)
            {
                if (player.getBrand(i).getClass() == w.getClass())
                {                    
                    for (int j = 0; j < tp.Length; j++)
                        if (player.getBrand(i).getNumber() == j+1)
                            tp[j].add(player.getBrand(i));
                }
            }
            for (int j = 0; j < tp.Length; j++)
                if (tp[j].getCount() == 1)
                    return tp[j].getBrand(0);
         

            //�P�_��i�U�P
            BrandPlayer[] alone_TenThousandBrand = new BrandPlayer[9];
            for (int j = 0; j < alone_TenThousandBrand.Length; j++)
                alone_TenThousandBrand[j] = new BrandPlayer();

            for (int i = 0; i < player.getCount(); i++)
            {
                if (player.getBrand(i).getClass() == w.getClass())
                {
                    for (int j = 0; j < alone_TenThousandBrand.Length; j++)
                        if (player.getBrand(i).getNumber() == j + 1)
                            alone_TenThousandBrand[j].add(player.getBrand(i));
                }
            }
            for (int j = 0; j < tp.Length; j++)
                if (alone_TenThousandBrand[j].getCount() == 1)
                    return alone_TenThousandBrand[j].getBrand(0);


            //�P�_��i���P
            BrandPlayer[] alone_TubeBrand = new BrandPlayer[9];
            for (int j = 0; j < alone_TubeBrand.Length; j++)
                alone_TubeBrand[j] = new BrandPlayer();

            for (int i = 0; i < player.getCount(); i++)
            {
                if (player.getBrand(i).getClass() == w.getClass())
                {
                    for (int j = 0; j < alone_TenThousandBrand.Length; j++)
                        if (player.getBrand(i).getNumber() == j + 1)
                            alone_TubeBrand[j].add(player.getBrand(i));
                }
            }
            for (int j = 0; j < tp.Length; j++)
                if (alone_TubeBrand[j].getCount() == 1)
                    return alone_TubeBrand[j].getBrand(0);


            //�P�_��i���P
            BrandPlayer[] alone_RopeBrand = new BrandPlayer[9];
            for (int j = 0; j < alone_RopeBrand.Length; j++)
                alone_RopeBrand[j] = new BrandPlayer();

            for (int i = 0; i < player.getCount(); i++)
            {
                if (player.getBrand(i).getClass() == w.getClass())
                {
                    for (int j = 0; j < alone_RopeBrand.Length; j++)
                        if (player.getBrand(i).getNumber() == j + 1)
                            alone_RopeBrand[j].add(player.getBrand(i));
                }
            }
            for (int j = 0; j < tp.Length; j++)
                if (alone_RopeBrand[j].getCount() == 1)
                    return alone_RopeBrand[j].getBrand(0);



            return ans;
        }


        void print()
        {
            Iterator it;
            it = player.creatIterator();
            printplayer(it);
            //Console.WriteLine("==>{0}{1}",ans.getNumber(),ans.getClass());
        }
        private void printplayer(Iterator iterator)
        {
            Console.WriteLine();
            while (iterator.hasNext())
            {
                Brand brand = (Brand)iterator.next();
                Console.Write("{0}{1}\t", brand.getNumber(), brand.getClass());
            }
            Console.WriteLine();
        }
    }
}
