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
        /// ���a�s��
        /// </summary>
        BrandPlayer[] players;
        /// <summary>
        /// �ୱ�P�s��
        /// </summary>
        BrandPlayer table;
        /// <summary>
        /// �P�u�t
        /// </summary>
        BrandFactory factory;
        /// <summary>
        /// �C�Ӫ��a���h�ֱi
        /// </summary>
        int dealnumber;
        /// <summary>
        /// �p��h�֭Ӫ��a
        /// </summary>
        private int countplayers;
        /// <summary>
        /// �`�P��
        /// </summary>
        public int sumBrands;
        /// <summary>
        /// �ثe���a
        /// </summary>
        int state;
        /// <summary>
        /// ���a�էO�p��
        /// </summary>
        int[] teamCount;
        /// <summary>
        /// �������a���X
        /// </summary>
        /// <param name="playernumber">�]�w���h�֭Ӫ��a</param>
        /// <param name="deal">�@�Ӫ��a���h�ֱi</param>
        public AllPlayers(int playernumber,int deal)
        {
            this.players = new BrandPlayer[playernumber];
            this.table = new BrandPlayer();
            this.factory = new BrandFactory();
            this.dealnumber = deal;
            countplayers = playernumber;
            this.sumBrands = factory.SumBrands;
            this.state = 0;
            this.teamCount = new int[playernumber];
            for (int i = 0; i < playernumber;i++ )
                teamCount[i]=0;
        }
        /// <summary>
        /// ���a�}�C
        /// </summary>
        public BrandPlayer[] Players
        {
            get
            {
                return players;
            }
        }
        /// <summary>
        /// �ୱ
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
        /// �إߵP,�ä��t�P
        /// </summary>
        public void creatBrands()
        {
            factory.createBrands();
            factory.randomBrands();
            table = factory.getBrands();
            dealbrands();
        }
        /// <summary>
        /// �Ǧ^�@�Ӫ��a�]�w�h�ֱi
        /// </summary>
        public int Dealnumber
        {
            get
            {
                return dealnumber;
            }
        }
        /// <summary>
        /// ���t�P
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
        /// ���U�@�a
        /// </summary>
        public void next()
        {
            if (state % countplayers == 0)
                state = 0;
            else
                state += 1;
        }
        ///
        public BrandPlayer nowPlayer
        {
            get
            {
                return players[state];
            }
        }
        /// <summary>
        /// �Y�I�ƥ�
        /// </summary>
        public event EventHandler<BrandPlayerEvent> Chow_Pong_Event;
        /// <summary>
        /// �Y�B�I
        /// </summary>
        public void chow_pong(Brand brand,BrandPlayer player)
        {
            set_TeamCount(player);
            
        }
        /// <summary>
        /// �b�ƥ�
        /// </summary>
        public event EventHandler<BrandPlayerEvent> Kong_Event;
        /// <summary>
        /// �b
        /// </summary>
        public void kong(Brand brand,BrandPlayer player)
        {
            set_TeamCount(player);
            
        }
        /// <summary>
        /// �]�w�s�ո��X
        /// </summary>
        /// <param name="player"></param>
        private void set_TeamCount(BrandPlayer player)
        {
            teamCount[state] += 1;
            for (int i = 0; i < player.getCount(); i++)
                players[state].remove(player.getBrand(i));
            for (int i = 0; i < player.getCount(); i++)
            {
                player.getBrand(i).IsCanSee = true;
                player.getBrand(i).Team = teamCount[state];
                players[state].add(player.getBrand(i));
            }
        }
    }
}
