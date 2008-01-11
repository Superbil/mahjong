using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using Mahjong.Brands;

namespace Mahjong.Control
{
    /// <summary>
    /// ­JµP´ú¸Õ
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
            Check c = new Check(a);
            if (c.Win())
                Console.WriteLine("¦³­J¡I¡I");
        }
    }
}
