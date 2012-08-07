using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using Mahjong.Brands;
using Mahjong.Control;
using System.Windows.Forms;

namespace Mahjong.Control
{
    [Serializable]
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
        /// 準備要顯示的桌面牌
        /// </summary>
        BrandPlayer show_table;
        /// <summary>
        /// 每個玩家分多少張
        /// </summary>
        int dealnumber;
        /// <summary>
        /// 計算多少個玩家
        /// </summary>
        int countplayers;
        /// <summary>
        /// 總牌數
        /// </summary>
        internal int sumBrands;
        /// <summary>
        /// 目前玩家
        /// </summary>
        internal uint state;
        /// <summary>
        /// 玩家組別計算
        /// </summary>
        int[] teamCount;
        /// <summary>
        /// 上一張牌
        /// </summary>
        internal Brand lastBrand;
        /// <summary>
        /// 方位
        /// </summary>
        Location lo;
        /// <summary>
        /// 玩家所有的錢
        /// </summary>
        int[] money;
        /// <summary>
        /// 連莊次數
        /// </summary>
        internal int win_Times;
        /// <summary>
        /// 玩家姓名
        /// </summary>
        string[] names;
        /// <summary>
        /// 底台
        /// </summary>
        public int basic_tai;
        /// <summary>
        /// 第幾次摸牌
        /// </summary>
        int brand_count;
        /// <summary>
        /// 一台多少錢
        /// </summary>
        public int one_tai;
        /// <summary>
        /// 是否是玩家
        /// </summary>
        public bool[] isPlayer;
        /// <summary>
        /// 儲存對應到的玩家的真實位置
        /// </summary>
        public Place place;
        /// <summary>
        /// 是否顯示提示
        /// </summary>
        public bool showMessageBox;
        /// <summary>
        /// 全部玩家集合
        /// </summary>
        /// <param name="playernumber">設定有多少個玩家</param>
        /// <param name="deal">一個玩家有多少張</param>
        public AllPlayers(int playernumber, int deal)
        {
            this.players = new BrandPlayer[playernumber];
            this.isPlayer = new bool[playernumber];
            this.lo = new Location();
            this.table = new BrandPlayer();
            this.show_table = new BrandPlayer();
            this.dealnumber = deal;
            this.countplayers = playernumber;
            this.state = (uint)lo.Winer;
            this.brand_count = 0;
            this.basic_tai = Mahjong.Properties.Settings.Default.BasicTai;
            this.one_tai = Mahjong.Properties.Settings.Default.One_Tai;
            this.teamCount = new int[playernumber];
            this.names = new string[playernumber];
            this.showMessageBox = true;
            this.place = new Place();
            for (int i = 0; i < playernumber; i++)
                teamCount[i] = 1;
            money = new int[playernumber];
            setBasicMoney(Mahjong.Properties.Settings.Default.Money);
            win_Times = 1;
            names[0] = Mahjong.Properties.Settings.Default.Player_North;
            names[1] = Mahjong.Properties.Settings.Default.Player_East;
            names[2] = Mahjong.Properties.Settings.Default.Player_South;
            names[3] = Mahjong.Properties.Settings.Default.Player_West;
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
            set
            {
                table = value;
            }
        }
        /// <summary>
        /// 第幾次摸牌
        /// </summary>
        public int Brand_Count
        {
            get
            {
                return brand_count;
            }
        }
        /// <summary>
        /// 顯示的桌面
        /// </summary>
        public BrandPlayer Show_Table
        {
            get
            {
                return show_table;
            }
        }
        /// <summary>
        /// 玩家姓名
        /// </summary>
        public string[] Name
        {
            set
            {
                names = value;
            }
            get
            {
                return names;
            }
        }
        /// <summary>
        /// 現在的玩家
        /// </summary>
        public BrandPlayer NowPlayer
        {
            get
            {
                return players[state];
            }
        }
        /// <summary>
        /// 傳回現在的位置
        /// </summary>
        public location State
        {
            get
            {
                return Location.getlocation(state);
            }
        }
        /// <summary>
        /// 連莊次數
        /// </summary>
        public int Win_Times
        {
            set
            {
                win_Times = value;
            }
            get
            {
                return win_Times;
            }
        }
        /// <summary>
        /// 取得或設定玩家的金錢
        /// </summary>
        public int[] Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
            }
        }
        /// <summary>
        /// 取得或設定現在玩家的金錢
        /// </summary>
        public int NowPlayerMoney
        {
            get
            {
                return money[state];
            }
            set
            {
                money[state] = value;
            }
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
        /// 傳回方位
        /// </summary>
        /// <returns>方位</returns>
        public Location getLocation
        {
            get
            {
                return lo;
            }
        }
        public int CountPlayer
        {
            get
            {
                return countplayers;
            }
        }
        /// <summary>
        /// 換下一家
        /// </summary>
        public void next()
        {
            // 1->0->3->2->1
            state--;
            state = state % (uint)countplayers;
        }
        /// <summary>
        /// 摸牌
        /// </summary>
        /// <returns>牌</returns>
        public Brand nextBrand()
        {
            if (table.getCount() <= 16) // 保留16張不摸
                throw new FlowOverException();
            else
            {
                Brand b = nextTableBrand();
                brand_count++;
                return b;
            }
        }
        Brand nextTableBrand()
        {
            Brand b = table.getBrand(0);
            table.remove(b);
            lastBrand = b;
            return b;
        }

        /// <summary>
        /// 下一莊
        /// </summary>
        /// <param name="flow">是否流局</param>
        public void nextWiner(bool flow)
        {
            // 如果流局或莊家贏
            if (flow || this.lo.Winer == this.State)
            {
                this.win_Times++;
            }
            else
            {
                this.lo.next();
                this.win_Times = 1;
            }
            //設定新的開牌位置
            this.lo.setPosition();
            //新的桌面
            this.table = new BrandPlayer();
            //更新莊家
            this.state = (uint)lo.Winer;
            //設定牌組
            for (int i = 0; i < countplayers; i++)
                teamCount[i] = 1;
            //打牌次數
            this.brand_count = 0;
            //清除打出去的牌
            Show_Table.clear();

        }
        /// <summary>
        /// 設定玩家的基本金錢
        /// </summary>
        /// <param name="number"></param>
        public void setBasicMoney(int number)
        {
            for (int i = 0; i < money.Length; i++)
                money[i] = number;
        }
        /// <summary>
        /// 吃、碰
        /// </summary>
        public void chow_pong(Brand brand, BrandPlayer player)
        {
            Show_Table.remove(brand);
            set_Team(player, true);
        }
        /// <summary>
        /// 槓
        /// </summary>
        /// <param name="brand">要槓的牌</param>
        /// <param name="player">可以槓的牌組</param>
        public void kong(Brand brand, BrandPlayer player)
        {
            Show_Table.remove(brand);
            set_Team(player, true);
        }
        /// <summary>
        /// 暗槓
        /// </summary>
        /// <param name="brand">要槓的牌</param>
        /// <param name="player">可以槓的牌組</param>
        public void DarkKong(Brand brand, BrandPlayer player)
        {
            set_Team(player, false);
        }
        /// <summary>
        /// 設定群組號碼
        /// </summary>
        /// <param name="player">玩家</param>
        private void set_Team(BrandPlayer player, bool isCanSee)
        {
            teamCount[state]++;
            // 把牌從現在玩家手上移出
            for (int i = 0; i < player.getCount(); i++)
                NowPlayer.remove(player.getBrand(i));
            // 把牌設為可視並且加上組別號碼後加回現在玩家
            for (int i = 0; i < player.getCount(); i++)
            {
                player.getBrand(i).IsCanSee = isCanSee;
                player.getBrand(i).Team = teamCount[state];
                NowPlayer.add(player.getBrand(i));
            }
        }
        /// <summary>
        /// 新遊戲補花
        /// </summary>
        public void Newgame_setFlower()
        {
            int f_count = 0;
            for (int i = 0; i < NowPlayer.getCount(); i++)
                if (NowPlayer.getBrand(i).getClass() == Mahjong.Properties.Settings.Default.Flower
                    && !NowPlayer.getBrand(i).IsCanSee) // 花牌而且不可見
                {
                    NowPlayer.getBrand(i).IsCanSee = true;
                    NowPlayer.getBrand(i).Team = 1;
                    f_count++;
                }
            // 補上少的牌數
            for (int i = 0; i < f_count; i++)
                NowPlayer.add(nextTableBrand());
        }
        /// <summary>
        /// 現在的玩家補花
        /// </summary>
        public bool Player_setFlower(Brand brand)
        {
            NowPlayer.add(brand);
            for (int i = 0; i < NowPlayer.getCount(); i++)
                if (NowPlayer.getBrand(i).getClass() == Mahjong.Properties.Settings.Default.Flower
                    && !NowPlayer.getBrand(i).IsCanSee) // 花牌而且不可見
                {
                    NowPlayer.getBrand(i).IsCanSee = true;
                    NowPlayer.getBrand(i).Team = 1;
                    return true;
                }
            NowPlayer.remove(brand);
            return false;
        }
        /// <summary>
        /// 現在的玩家排序
        /// </summary>
        public void sortNowPlayer()
        {
            PlayerSort ps = new PlayerSort(players[state]);
            players[state] = ps.getPlayer;
        }
        /// <summary>
        /// 把牌打到桌面上
        /// </summary>
        /// <param name="brand"></param>
        public void PushToTable(Brand brand)
        {
            brand.IsCanSee = true;
            brand.WhoPush = State;
            show_table.add(brand);
        }
        
    }
}
