using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Brands;
using Mahjong.Players;
using Mahjong.Forms;
using System.Windows.Forms;


namespace Mahjong.Control
{
    class TallyTest : Form
    {
        Tally f;
        public TallyTest()
        {
            BrandPlayer a = new BrandPlayer();
            a.add(new TenThousandBrand(1));
            a.add(new TenThousandBrand(2));
            a.add(new TenThousandBrand(3));

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
