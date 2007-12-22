using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using Mahjong.Brands;
using Mahjong.Control;

namespace Mahjong.Players
{
    public class AllPlayers
    {
        /// <summary>
        /// 玩家存放
        /// </summary>
        BrandPlayer[] players;
        /// <summary>
        /// 桌面牌存放
        /// </summary>
        BrandPlayer table;
        /// <summary>
        /// 牌工廠
        /// </summary>
        BrandFactory factory;
        /// <summary>
        /// 每個玩家分多少張
        /// </summary>
        int dealnumber;
        /// <summary>
        /// 計算多少個玩家
        /// </summary>
        private int countplayers;
        /// <summary>
        /// 總牌數
        /// </summary>
        public int sumBrands;
        /// <summary>
        /// 全部玩家集合
        /// </summary>
        /// <param name="playernumber">設定有多少個玩家</param>
        /// <param name="deal">一個玩家有多少張</param>
        public AllPlayers(int playernumber,int deal)
        {
            this.players = new BrandPlayer[playernumber];
            this.table = new BrandPlayer();
            this.factory = new BrandFactory();
            this.dealnumber = deal;
            countplayers = playernumber;
            this.sumBrands = factory.SumBrands;
        }
        /// <summary>
        /// 玩家陣列
        /// </summary>
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
        /// <summary>
        /// 桌面
        /// </summary>
        public BrandPlayer Table
        {
            get
            {
                return table;
            }
        }
        /// <summary>
        /// 建立牌,並分配牌
        /// </summary>
        public void creatBrands()
        {
            factory.createBrands();
            factory.randomBrands();
            table = factory.getBrands();
            dealbrands();
        }
        /// <summary>
        /// 傳回一個玩家設定多少張
        /// </summary>
        public int Dealnumber
        {
            get
            {
                return dealnumber;
            }
        }
        /// <summary>
        /// 分配牌
        /// </summary>
        void dealbrands()
        {
            Deal deal = new Deal(dealnumber, countplayers, table);
            deal.DealBrands();
            // get Players
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = deal.getPlayer(i);
            }
            // get Table
            table = deal.getTable();
        }
    }
}
