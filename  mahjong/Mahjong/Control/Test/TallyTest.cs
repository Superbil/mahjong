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
            a.add(new TubeBrand(1));
            a.add(new TubeBrand(1));
            a.add(new TubeBrand(1));
            //a.add(new TubeBrand(1));

            a.add(new RopeBrand(1));
            a.add(new RopeBrand(1));
            a.add(new RopeBrand(1));
            //a.add(new RopeBrand(1));

            a.add(new TenThousandBrand(1));
            a.add(new TenThousandBrand(1));
            a.add(new TenThousandBrand(1));
            //a.add(new TenThousandBrand(1));

            a.add(new TubeBrand(9));
            a.add(new TubeBrand(9));
            a.add(new TubeBrand(9));
            //a.add(new TubeBrand(9));

            a.add(new RopeBrand(9));
            a.add(new RopeBrand(9));
            a.add(new RopeBrand(9));

            //a.add(new WordBrand(4));
            a.add(new WordBrand(4));
            a.add(new WordBrand(4));

            ////a.add(new WordBrand(1));
            //a.add(new WordBrand(1));
            //a.add(new WordBrand(1));
            //a.add(new WordBrand(1));

            ////a.add(new WordBrand(2));
            //a.add(new WordBrand(2));
            //a.add(new WordBrand(2));
            //a.add(new WordBrand(2));

            ////a.add(new WordBrand(3));
            //a.add(new WordBrand(3));
            //a.add(new WordBrand(3));
            //a.add(new WordBrand(3));

            f = new Tally();

            Location l = new Location();
            AllPlayers all = new AllPlayers(4,16);
            WordBrand w = new WordBrand(9);
            w.WhoPush = location.East;
            all.Show_Table.add(w);
            f.setPlayer(all);
            f.setPlayer(a);
            //l.next_Winer();
            f.setLocation(l,0);
            
            f.ShowDialog();

        }

    }
}
