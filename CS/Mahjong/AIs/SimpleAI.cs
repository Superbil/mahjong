using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using Mahjong.Brands;
using System.Collections;

namespace Mahjong.AIs
{
    public class SimpleAI : MahjongAI
    {
        private BrandPlayer brandplayer;
        FlowerBrand f = new FlowerBrand(0);
        RopeBrand r = new RopeBrand(0);
        TubeBrand tu = new TubeBrand(0);
        WordBrand w = new WordBrand(0);
        Brand[] arrayBrands;
        #region MahjongAI 成員

        public void setPlayer(BrandPlayer player)
        {
            this.brandplayer = player;
            arrayBrands = new Brand[brandplayer.getCount()];
            setup();
        }

        private void setup()
        {
            for (int i = 0; i < brandplayer.getCount(); i++)
                arrayBrands[i] = brandplayer.getBrand(i);
        }

        public Brand getReadyBrand()
        {
            return brandplayer.getBrand(0);
        }

        #endregion

    }
}
