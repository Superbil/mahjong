using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Brands;
using Mahjong.Players;
using System.Collections;

namespace Mahjong.Players
{
    /// <summary>
    /// 排序牌
    /// </summary>
    class PlayerSort
    {
        /// <summary>
        /// 共有多少類別
        /// </summary>
        const int CountClass=5;
        /// <summary>
        /// 輸入玩家
        /// </summary>
        BrandPlayer inputPlayer;
        /// <summary>
        /// 暫存玩家
        /// </summary>
        BrandPlayer[] tempPlayers= new BrandPlayer[CountClass];
        /// <summary>
        /// 不需要排序的牌組
        /// </summary>
        BrandPlayer teamBrands;
        /// <summary>
        /// 排序類別的標準
        /// </summary>
        Brand[] BrandClass = new Brand[CountClass];
        /// <summary>
        /// 排序結果
        /// </summary>
        BrandPlayer ans = new BrandPlayer();
        BrandPlayer show = new BrandPlayer();
        BrandPlayer team = new BrandPlayer();
        /// <summary>
        /// 排序牌的建構子
        /// </summary>
        /// <param name="player">玩家</param>
        public PlayerSort(BrandPlayer player)
        {
            this.inputPlayer = player;
            for (int i = 0; i < tempPlayers.Length; i++)
                tempPlayers[i] = new BrandPlayer();
            teamBrands = new BrandPlayer();
            BrandClass[0] = new FlowerBrand(0);
            BrandClass[1] = new TenThousandBrand(0);
            BrandClass[2] = new RopeBrand(0);
            BrandClass[3] = new TubeBrand(0);
            BrandClass[4] = new WordBrand(0);

            getBrands( inputPlayer.creatIterator() );
            sortPlayer();
            //sortTeam();
            compose();
        }


        /// <summary>
        /// 排序牌的建構子
        /// </summary>
        /// <param name="player">玩家</param>
        /// <param name="b1">牌類別1</param>
        /// <param name="b2">牌類別2</param>
        /// <param name="b3">牌類別3</param>
        /// <param name="b4">牌類別4</param>
        /// <param name="b5">牌類別5</param>
        public PlayerSort(BrandPlayer player,Brand b1,Brand b2,Brand b3,Brand b4,Brand b5)
        {
            this.inputPlayer = player;
            for (int i = 0; i < tempPlayers.Length; i++)
                tempPlayers[i] = new BrandPlayer();
            teamBrands = new BrandPlayer();
            BrandClass[0] = b1;
            BrandClass[1] = b2;
            BrandClass[2] = b3;
            BrandClass[3] = b4;
            BrandClass[4] = b5;

            getBrands(inputPlayer.creatIterator());
            sortPlayer();
            //sortTeam();
            compose();
        }
        /// <summary>
        /// 得到排序完的玩家
        /// </summary>
        /// <returns>玩家</returns>
        public BrandPlayer getPlayer()
        {
            return ans;
        }
        /// <summary>
        /// 結果組合輸出
        /// </summary>
        private void compose()
        {
            for (int i = 0; i < teamBrands.getCount(); i++)
                ans.add(teamBrands.getBrand(i));
            for (int i = 0; i < tempPlayers.Length; i++)
                for (int j = 0; j < tempPlayers[i].getCount(); j++)
                    ans.add(tempPlayers[i].getBrand(j));
            for (int i = 0; i < teamBrands.getCount(); i++)
                ans.remove(teamBrands.getBrand(i));
        }
        /// <summary>
        /// 排序玩家
        /// </summary>
        private void sortPlayer()
        {
            for (int i = 0; i < BrandClass.Length; i++ )
                if(tempPlayers[i].getCount() > 1 )
                    tempPlayers[i] = ButtleSort(tempPlayers[i]);
        }
        /// <summary>
        /// 排序牌組
        /// </summary>
        private void sortTeam()
        {
            if (teamBrands.getCount() > 1)
            {
                // 設定最大的team號碼
                int team_count = 0;
                for (int i = 0; i < teamBrands.getCount(); i++)
                    if (teamBrands.getBrand(i).Team > team_count)
                        team_count = teamBrands.getBrand(i).Team;
                // 設定新的陣列以最大的team號碼
                BrandPlayer[] b = new BrandPlayer[team_count];
                for (int i = 0; i < b.Length; i++)
                    b[i] = new BrandPlayer();

                for (int i = 0; i < teamBrands.getCount(); i++)
                    b[teamBrands.getBrand(i).Team-1].add(teamBrands.getBrand(i));

                for (int i = 0; i < b.Length; i++)
                    b[i] = ButtleSort(b[i]);

                teamBrands.clear();
                for (int i = 0; i < b.Length; i++)
                    for (int j = 0; j < b[i].getCount(); j++)
                        teamBrands.add(b[i].getBrand(j));
            }
        }
        private BrandPlayer ButtleSort(BrandPlayer player)
        {      
            Brand[] a= new Brand[player.getCount()];
            for (int i = 0; i < a.Length; i++ )
                a[i] = player.getBrand(i);

            Brand temp_brand;
            for (int i = (player.getCount() - 1); i >= 0; i--)
            {
                for (int j = 1; j <= i; j++)
                {
                    Brand first = a[j - 1];
                    Brand second = a[j];
                    if (first.getNumber() > second.getNumber())
                    {
                        temp_brand = a[j - 1];
                        a[j - 1] = a[j];
                        a[j] = temp_brand;
                    }
                }
            }
            player.clear();
            for (int i = 0; i < a.Length; i++ )
                player.add(a[i]);
            return player;
        }
        private void getBrands(Iterator iterator)
        {
            while (iterator.hasNext())
            {
                Brand brandtemp = (Brand)iterator.next();
                for (int i=0; i < tempPlayers.Length ; i++ )
                    if (brandtemp.getClass()==BrandClass[i].getClass())
                        tempPlayers[i].add(brandtemp);
                if (brandtemp.Team > 0)
                    teamBrands.add(brandtemp);
            }
        }
    }
}
