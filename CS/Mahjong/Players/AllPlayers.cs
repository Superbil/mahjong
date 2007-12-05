using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using Mahjong.Brands;
using Mahjong.Control;

namespace Mahjong.Players
{
    class AllPlayers
    {
        /// <summary>
        /// ���a�s��
        /// </summary>
        BrandPlayer[] players;
        /// <summary>
        /// �ୱ�P�s��
        /// </summary>
        BrandPlayer table;
        BrandFactory factory;

        public AllPlayers(int playernumber)
        {
            players = new BrandPlayer[playernumber];
            table = new BrandPlayer();
            factory = new BrandFactory();
        }
        public BrandPlayer[] Players
        {
            get
            {
                return players;
            }
            set
            {
                players = value;
            }
        }
        public BrandPlayer Table
        {
            get
            {
                return table;
            }
        }
        public void creatBrands()
        {
            factory.createBrands();
            factory.randomBrands();
            table = factory.getBrands();
        }
        void dealbrands()
        {
            Deal deal = new Deal(16, table);
            deal.DealBrands();

            for (int i = 0; i < 4; i++)
            {
                player[i] = deal.getPlayer(i);
            }
        }
    }
}
