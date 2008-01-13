using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using Mahjong.Brands;

namespace Mahjong.Control
{
    /// <summary>
    /// �J�P����
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
                Console.WriteLine("���J�I�I");
            if (c.Chow())
                Console.WriteLine("���Y");
            if (c.Pong())
                Console.WriteLine("���I");
            if (c.Kong())
                Console.WriteLine("���b");
            if (c.BlackKong())
                Console.WriteLine("���t�b");

            if (!c.Win() && !c.Chow() && !c.Pong() && !c.Kong())
                Console.WriteLine("���S");
        }
    }
}
