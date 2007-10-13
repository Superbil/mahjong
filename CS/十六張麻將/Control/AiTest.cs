using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using Mahjong.AIs;
using Mahjong.Brands;

namespace Mahjong.Control
{
    class AiTest
    {
        public AiTest()
        {
            BrandPlayer[] player = new BrandPlayer[4];
            BrandPlayer table = new BrandPlayer();

            BrandFactory x = new BrandFactory();
            x.createBrands();
            x.randomBrands();

            table = x.getBrands();

            Deal deal = new Deal(16,table);
            deal.DealBrands();

            for (int i = 0; i < 4; i++)
            {
                player[i] = deal.getPlayer(i);
            }
            printplayer(player);
            SimpleAI sa = new SimpleAI(player[0]);
            
            sa.getReadyBrand();
            //Console.WriteLine();
            Console.WriteLine("Ready Brand is {0},{1}", sa.getReadyBrand().getClass(), sa.getReadyBrand().getNumber());
        }
        void printplayer(BrandPlayer[] player)
        {
            for (int i = 0; i < player.Length; i++)
            {
                Console.WriteLine("\n===Player {0}===", i+1);
                Iterator temp = player[i].creatIterator();
                print(temp);
            }
        }
        private void print(Iterator iterator)
        {
            //Console.WriteLine();
            while (iterator.hasNext())
            {
                Brand brand = (Brand)iterator.next();
                Console.Write("{0},{1}\t", brand.getClass(), brand.getNumber());
            }
        }
    }
}
