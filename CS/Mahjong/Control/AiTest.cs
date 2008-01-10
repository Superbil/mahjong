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
            //printplayer(player);
            //SimpleAI sa = new SimpleAI();
            //sa.setPlayer(player[0]);
            //PlayerSort bs = new PlayerSort(player[0]);
            //player[0] = bs.getPlayer();

            //PlayerSort bs = new PlayerSort(player[0]);

            PlayerSort bs = new PlayerSort(player[0],new FlowerBrand(0),new TenThousandBrand(0),new RopeBrand(0),new  TubeBrand(0),new WordBrand(0));
            player[0] = bs.getPlayer();
            Level_1 l = new Level_1();
            l.setPlayer(player[0]);
            l.getReadyBrand();
            
            //printplayer(player);
            //Level_1 l = new Level_1();
            //BrandPlayer test = new BrandPlayer();
            //test.add(new WordBrand(1));
            //test.add(new WordBrand(1));
            //test.add(new WordBrand(1));
            //test.add(new WordBrand(2));
            //test.add(new WordBrand(3));
            //test.add(new WordBrand(3));
            //test.add(new WordBrand(3));
            //test.add(new WordBrand(5));
            //l.setPlayer(test);

            l.setPlayer(player[0]);
            
            Brand t = l.getReadyBrand();
            Console.WriteLine("==>{0}{1}", t.getNumber(), t.getClass());
            //printplayer(player);

            //sa.getReadyBrand();
            //Console.WriteLine();
            //Console.WriteLine("Ready Brand is {0},{1}", l.getReadyBrand().getClass(), l.getReadyBrand().getNumber());
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
                Console.Write("{0}{1}\t", brand.getNumber(), brand.getClass() );
            }
        }
    }
}
