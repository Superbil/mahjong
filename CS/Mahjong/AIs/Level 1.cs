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
            //判斷單張字牌
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
         

            //判斷單張萬牌
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


            //判斷單張筒牌
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


            //判斷單張索牌
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
