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
            a.add(new TubeBrand(1));
            a.add(new TubeBrand(1));
            a.add(new TubeBrand(1));
            a.add(new TubeBrand(1));
            a.add(new TubeBrand(2));
            a.add(new TubeBrand(2));
            a.add(new TubeBrand(2));
            a.add(new TubeBrand(2));
            a.add(new TubeBrand(3));
            a.add(new TubeBrand(3));
            a.add(new TubeBrand(3));
            a.add(new TubeBrand(3));
            a.add(new TubeBrand(9));
            a.add(new TubeBrand(9));
            a.add(new WordBrand(6));
            a.add(new WordBrand(6));
            a.add(new WordBrand(6));
            //Brand b = new TubeBrand(2);
            //Check c = new Check(b,a);
            Check c = new Check (a);
            if (c.Win())
                Console.WriteLine("有胡！！");
            if (c.Chow())
                Console.WriteLine("有吃");
            if (c.Pong())
                Console.WriteLine("有碰");
            if (c.Kong())
                Console.WriteLine("有槓");
            if (c.BlackKong())
                Console.WriteLine("有暗槓");

            if (!c.Win() && !c.Chow() && !c.Pong() && !c.Kong())
                Console.WriteLine("都沒");
        }
    }
}
