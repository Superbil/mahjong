using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using Mahjong.Brands;

namespace Mahjong.Control
{
    /// <summary>
    /// 胡牌測試
    /// </summary>
    class CheckTest
    {
        public CheckTest()
        {
            BrandPlayer a = new BrandPlayer();
            //a.add(new TenThousandBrand(5));
            //a.add(new TenThousandBrand(6));
            //a.add(new TenThousandBrand(7));
            //a.add(new TenThousandBrand(6));
            //a.add(new TenThousandBrand(7));
            //a.add(new TenThousandBrand(8));

            //a.add(new RopeBrand(4));
            //a.add(new RopeBrand(5));
            //a.add(new RopeBrand(6));

            a.add(new TubeBrand(2));
            a.add(new TubeBrand(2));
            a.add(new TubeBrand(2));
            //a.add(new TubeBrand(2));
            //a.add(new TubeBrand(5));
            //a.add(new TubeBrand(8));
            //a.add(new TubeBrand(8));

            //FlowerBrand f = new FlowerBrand(1);
            //f.Team = 1;
            //a.add(f);

            //WordBrand r = new WordBrand(9);
            ////r.Team = 2;
            //a.add(r);
            //a.add(r);
            //a.add(r);
            ////a.add(r);

            //TubeBrand t = new TubeBrand(9);
            //t.Team = 3;
            //a.add(t);
            //a.add(t);
            //a.add(t);

            //a.add(new TubeBrand(2));
            //a.add(new TubeBrand(2));
            //a.add(new TubeBrand(3));
            //a.add(new TubeBrand(3));
            //a.add(new TubeBrand(3));
            //a.add(new TubeBrand(3));
            //a.add(new TubeBrand(9));
            //a.add(new TubeBrand(9));
            //WordBrand w = new WordBrand(6);
            //w.Team = 2;
            //a.add(w);
            //a.add(w);
            //a.add(w);
            //a.add(w);

            //Brand b = new TubeBrand(2);
            //Check c = new Check(b,a);
            printplayer(a);
            Check c = new Check(new TubeBrand(2), a);
            a.add(new TubeBrand(2));
            Check d = new Check(a);
            //if (c.Win())
            //{
            //    Console.WriteLine("有胡！！");
            //    printplayer(c.SuccessPlayer);
            //}
            if (c.Chow())
            {
                Console.WriteLine("===========");
                Console.WriteLine("\n有吃");
                Console.WriteLine(c.ChowLength);
                printplayer(c.SuccessPlayer);
                printplayer(c.ChowPlayer[0]);
                printplayer(c.ChowPlayer[1]);
                printplayer(c.ChowPlayer[2]);
                Console.WriteLine("===========");
            }
            if (c.Pong())
            {
                Console.WriteLine("\n有碰");
                printplayer(c.SuccessPlayer);
            }
            if (c.Kong())
            {
                Console.WriteLine("\n有槓");
                printplayer(c.SuccessPlayer);
            }
            if (d.DarkKong())
            {
                Console.WriteLine("\n有暗槓");
                printplayer(c.SuccessPlayer);
            }

            if (//!c.Win() &&
                !c.Chow() && !c.Pong() && !c.Kong() && !c.DarkKong())
                Console.WriteLine("\n都沒");
            printplayer(a);
        }
        void printplayer(BrandPlayer player)
        {
            Console.WriteLine("\n=== Player ===");
            Iterator temp = player.creatIterator();
            print(temp);
        }
        private void print(Iterator iterator)
        {
            //Console.WriteLine();
            while (iterator.hasNext())
            {
                Brand brand = (Brand)iterator.next();
                Console.Write("{0}{1}\t", brand.getNumber(), brand.getClass());
            }
        }
    }
}
