using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using Mahjong.Brands;
using Mahjong.Control;

namespace Mahjong.Players
{
    [Serializable]
    public class AllPlayers
    {
        /// <summary>
        /// 產
        /// </summary>
        BrandPlayer[] players;
        /// <summary>
        /// 礟
        /// </summary>
        BrandPlayer table;
        /// <summary>
        /// 礟紅
        /// </summary>
        BrandFactory factory;
        /// <summary>
        /// –產だぶ眎
        /// </summary>
        int dealnumber;
        /// <summary>
        /// 璸衡ぶ產
        /// </summary>
        private int countplayers;
        /// <summary>
        /// 羆礟计
        /// </summary>
        public int sumBrands;
        /// <summary>
        /// ヘ玡產
        /// </summary>
        int state;
        /// <summary>
        /// 產舱璸衡
        /// </summary>
        int[] teamCount;
        /// <summary>
        /// 眎礟
        /// </summary>
        Brand lastBrand;
        /// <summary>
        /// よ
        /// </summary>
        Location lo;

        /// <summary>
        /// 场產栋
        /// </summary>
        /// <param name="playernumber">砞﹚Τぶ產</param>
        /// <param name="deal">產Τぶ眎</param>
        public AllPlayers(int playernumber,int deal)
        {
            this.players = new BrandPlayer[playernumber];
            this.lo = new Location();
            this.table = new BrandPlayer();
            this.factory = new BrandFactory();
            this.dealnumber = deal;
            this.countplayers = playernumber;
            this.sumBrands = factory.SumBrands;
            this.state = 0;
            this.teamCount = new int[playernumber];
            for (int i = 0; i < playernumber;i++ )
                teamCount[i]=0;            
        }
        /// <summary>
        /// 產皚
        /// </summary>
        public BrandPlayer[] Players
        {
            get
            {
                return players;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public BrandPlayer Table
        {
            get
            {
                return table;
            }
        }
        /// <summary>
        /// 瞷產
        /// </summary>
        public BrandPlayer NowPlayer
        {
            get
            {
                return players[state];
            }
        }
        /// <summary>
        /// ミ礟,だ皌礟
        /// </summary>
        public void creatBrands()
        {
            factory.createBrands();
            factory.randomBrands();
            table = factory.getBrands();
            dealbrands();
        }
        /// <summary>
        /// 肚產砞﹚ぶ眎
        /// </summary>
        public int Dealnumber
        {
            get
            {
                return dealnumber;
            }
        }
        /// <summary>
        /// だ皌礟
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
        /// <summary>
        /// 传產
        /// </summary>
        public void next()
        {
            if (state % countplayers == 0)
                state = 0;
            else
                state += 1;
        }
        /// <summary>
        /// 篘礟
        /// </summary>
        /// <returns>礟</returns>
        public Brand nextBrand()
        {
            if (table.getCount() == 0)
                return null;
            else
            {
                Brand b = table.getBrand(0);
                table.remove(b);
                lastBrand = b;
                return b;
            }            
        }
        /// <summary>
        /// 肚よ
        /// </summary>
        /// <returns>よ</returns>
        public Location location()
        {
            return lo;
        }
        public void nextRound()
        {
            lo.next();
            this.table = new BrandPlayer();
            this.factory = new BrandFactory();
            this.state = 0;
            for (int i = 0; i < countplayers; i++)
                teamCount[i] = 0;   
        }
        /// <summary>
        /// 窱
        /// </summary>
        public void chow_pong(Brand brand,BrandPlayer player)
        {
            set_Team(player,true);
            lastBrand = brand;            
        }
        /// <summary>
        /// 篵
        /// </summary>
        public void kong(BrandPlayer player)
        {            
            // 篵,程眎礟单肚ㄓ礟杠穞篵
            if (lastBrand==player.getBrand(0))
                set_Team(player, true);
            else // 穞篵
                set_Team(player, false);
            // 篵璶干眎
            NowPlayer.add(nextBrand());
        }
        /// <summary>
        /// 砞﹚竤舱腹絏
        /// </summary>
        /// <param name="player">產</param>
        private void set_Team(BrandPlayer player,bool isCanSee)
        {
            teamCount[state] += 1;
            for (int i = 0; i < player.getCount(); i++)
                NowPlayer.remove(player.getBrand(i));
            for (int i = 0; i < player.getCount(); i++)
            {
                player.getBrand(i).IsCanSee = isCanSee;
                player.getBrand(i).Team = teamCount[state];
                NowPlayer.add(player.getBrand(i));
            }
        }
        /// <summary>
        /// 干
        /// </summary>
        /// <param name="player">產</param>
        public void setFlower()
        {
            int t_count = 0;
            for (int i = 0; i < NowPlayer.getCount(); i++)
                if (NowPlayer.getBrand(i).getClass() == Mahjong.Properties.Settings.Default.Flower)
                {
                    NowPlayer.getBrand(i).IsCanSee = true;
                    NowPlayer.getBrand(i).Team = 0;
                    t_count++;
                }
            // 干ぶ礟计
            for (int i = 0; i < t_count; i++)
                NowPlayer.add( nextBrand() );
        }
    }
}
