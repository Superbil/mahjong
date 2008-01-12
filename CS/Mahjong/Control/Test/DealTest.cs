using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using Mahjong.Brands;

namespace Mahjong.Control
{
    class DealTest
    {
        public DealTest()
        {
            BrandPlayer[] player = new BrandPlayer[4];
            BrandPlayer table = new BrandPlayer();

            BrandFactory x = new BrandFactory();
            x.createBrands();
            x.randomBrands();

            table = x.getBrands();
            Console.WriteLine("共有: {0}",table.getCount());
            // 印出洗好的牌
            printplayer(table,"亂數牌");

            PlayerSort bbs = new PlayerSort(table);
            BrandPlayer sort_table = bbs.getPlayer();
            printplayer(sort_table,"亂數排序回去");

            // 分配牌
            Deal deal = new Deal(16,table);
            deal.DealBrands();
            player = deal.Player;

            // 印出全部的玩家
            printplayer(player);
            
            BrandPlayer check = new BrandPlayer();

            foreach (BrandPlayer b in player)
                for (int i = 0; i < b.getCount(); i++)
                    check.add(b.getBrand(i));
            for (int i = 0; i < table.getCount(); i++)
                check.add(table.getBrand(i));
            PlayerSort bs = new PlayerSort(check);
            check = bs.getPlayer();
            Console.WriteLine("\n共有: {0}",check.getCount());
            printplayer(check,"重新檢查");
        }
        void printplayer(BrandPlayer[] player)
        {
            for (int i = 0; i < player.Length; i++)
            {
                int y = i + 1;
                printplayer(player[i], y.ToString());
            }
        }
        void printplayer(BrandPlayer player,string val)
        {
                Console.WriteLine("\n=== Player {0}===",val);
                Iterator temp = player.creatIterator();
                print(temp);
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
        void add_to_player(BrandPlayer player,BrandPlayer add)
        {
            Iterator temp = player.creatIterator();
            add_to_player_iterator(temp,add);
        }
        private void add_to_player_iterator(Iterator iterator,BrandPlayer player)
        {
            while (iterator.hasNext())
            {
                Brand brand = (Brand)iterator.next();
                player.add(brand);
            }
        }
    }
}
