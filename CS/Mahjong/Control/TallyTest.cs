using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Brands;
using Mahjong.Players;
using Mahjong.Forms;
using System.Windows.Forms;


namespace Mahjong.Control
{
    class TallyTest
    {
        Tally f;
        public TallyTest()
        {
            BrandPlayer a = new BrandPlayer();
            a.add(new TubeBrand(2));
            a.add(new TubeBrand(2));
            a.add(new TubeBrand(2));
            a.add(new TubeBrand(2));
            a.add(new TubeBrand(3));
            a.add(new TubeBrand(3));
            a.add(new TubeBrand(3));
            a.add(new TubeBrand(6));
            a.add(new TubeBrand(6));
            a.add(new WordBrand(5));
            a.add(new WordBrand(5));
            a.add(new WordBrand(5));
            a.add(new WordBrand(5));
            a.add(new WordBrand(6));
            a.add(new WordBrand(6));
            a.add(new WordBrand(6));
            a.add(new WordBrand(7));
            a.add(new WordBrand(7));
            a.add(new WordBrand(7));
           
            
            







            f = new Tally();
            
            f.setPlayer(a);
            f.setLocation(
                Mahjong.Properties.Settings.Default.East,
                Mahjong.Properties.Settings.Default.East,
                Mahjong.Properties.Settings.Default.East,
                0);
            
            f.ShowDialog();

        }

    }
}
