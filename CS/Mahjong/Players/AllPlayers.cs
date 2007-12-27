using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using Mahjong.Brands;
using Mahjong.Control;

namespace Mahjong.Players
{
    enum State
    {
        /// <summary>
        /// 北
        /// </summary>
        North = 0,
        /// <summary>
        /// 東
        /// </summary>
        East = 1,
        /// <summary>
        /// 南
        /// </summary>
        South = 2,
        /// <summary>
        /// 西
        /// </summary>
        West = 3,
        /// <summary>
        /// 桌面
        /// </summary>
        Table = 4
    }
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
        /// 目前玩家
        /// </summary>
        int state;
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
            this.state = 0;
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
        public BrandPlayer NowPlalyer()
        {
            return players[state];
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
                players[i] = deal.getPlayer(i);
            // get Table
            table = deal.getTable();
        }
        public void next()
        {
            if (state % countplayers == 0)
                state = 0;
            else
                state += 1;
        }
        /// <summary>
        /// 吃
        /// </summary>
        public void chow(Brand brand,BrandPlayer chowplayer)
        {

        }
        /// <summary>
        /// 碰
        /// </summary>
        public void pong(Brand brand,BrandPlayer pongplayer)
        {

        }
        /// <summary>
        /// 槓
        /// </summary>
        public void kong(Brand brand,BrandPlayer kongplayer)
        {

        }
    }
}
