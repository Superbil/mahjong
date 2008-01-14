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
        /// ���a�s��
        /// </summary>
        BrandPlayer[] players;
        /// <summary>
        /// �ୱ�P�s��
        /// </summary>
        BrandPlayer table;
        /// <summary>
        /// �ǳƭn��ܪ��ୱ�P
        /// </summary>
        BrandPlayer show_table;
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
        int countplayers;
        /// <summary>
        /// �`�P��
        /// </summary>
        internal int sumBrands;
        /// <summary>
        /// �ثe���a
        /// </summary>
        public int state;
        /// <summary>
        /// ���a�էO�p��
        /// </summary>
        int[] teamCount;
        /// <summary>
        /// �W�@�i�P
        /// </summary>
        internal Brand lastBrand;
        /// <summary>
        /// ���
        /// </summary>
        Location lo;
        /// <summary>
        /// ���a�Ҧ�����
        /// </summary>
        double[] money;
        /// <summary>
        /// �s������
        /// </summary>
        int win_Times;
        /// <summary>
        /// ���a�m�W
        /// </summary>
        string[] names;
        /// <summary>
        /// ���x
        /// </summary>
        public int basic_tai;
        /// <summary>
        /// �ĴX���N�P
        /// </summary>
        int barnd_count;

        /// <summary>
        /// �������a���X
        /// </summary>
        /// <param name="playernumber">�]�w���h�֭Ӫ��a</param>
        /// <param name="deal">�@�Ӫ��a���h�ֱi</param>
        public AllPlayers(int playernumber,int deal)
        {
            this.players = new BrandPlayer[playernumber];
            this.lo = new Location();
            this.table = new BrandPlayer();
            this.show_table = new BrandPlayer();
            this.factory = new BrandFactory();            
            this.dealnumber = deal;
            this.countplayers = playernumber;
            this.sumBrands = factory.SumBrands;
            this.state = 1;
            this.barnd_count = 0;
            this.basic_tai = Mahjong.Properties.Settings.Default.BasicTai;            
            this.teamCount = new int[playernumber];
            this.names = new string[playernumber];
            for (int i = 0; i < playernumber;i++ )
                teamCount[i]=0;            
            money = new double[playernumber];
            setBasicMoney(Mahjong.Properties.Settings.Default.Money);
            win_Times = 0;
            names[0] = Mahjong.Properties.Settings.Default.Player1;
            names[1] = Mahjong.Properties.Settings.Default.Player2;
            names[2] = Mahjong.Properties.Settings.Default.Player3;
            names[3] = Mahjong.Properties.Settings.Default.Player4;
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
        /// <summary>
        /// �ĴX���N�P
        /// </summary>
        public int Brand_Count
        {
            get
            {
                return barnd_count;
            }
        }
        /// <summary>
        /// ��ܪ��ୱ
        /// </summary>
        public BrandPlayer Show_Table
        {
            get
            {
                return show_table;
            }
        }
        /// <summary>
        /// ���a�m�W
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
        /// �{�b�����a
        /// </summary>
        public BrandPlayer NowPlayer
        {
            get
            {
                return players[state];
            }
        }
        /// <summary>
        /// �s������
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
        /// �Ǧ^���a������
        /// </summary>
        public double[] Money
        {
            get
            {
                return money;
            }
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
            players = deal.Player;
            // get Table
            table = deal.Table;
        }
        /// <summary>
        /// ���U�@�a
        /// </summary>
        public void next()
        {
            //switch (state)
            //{
            //    case 0:
            //        state = 3;
            //        break;
            //    case 1:
            //        state = 0;
            //        break;
            //    case 2:
            //        state = 1;
            //        break;
            //    case 3:
            //        state = 2;
            //        break;                     
            //}
            state++;
            if (state % countplayers == 0)
                state = 0;
        }
        /// <summary>
        /// �N�P
        /// </summary>
        /// <returns>�P</returns>
        public Brand nextBrand()
        {
            if (table.getCount() < 8) // �O�d8�i���N
                throw new GameOverException();
            else
            {
                Brand b = nextTableBrand();
                barnd_count++;
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
        /// �Ǧ^���
        /// </summary>
        /// <returns>���</returns>
        public Location getLocation()
        {
            return lo;
        }
        /// <summary>
        /// �U�@��
        /// </summary>
        public void nextRound(bool aby)
        {
            if (aby)
                lo.next();
            this.table = new BrandPlayer();
            this.factory = new BrandFactory();
            this.state = 0;
            for (int i = 0; i < countplayers; i++)
                teamCount[i] = 0;   
        }
        /// <summary>
        /// �]�w���a���򥻪���
        /// </summary>
        /// <param name="number"></param>
        public void setBasicMoney(double number)
        {
            for (int i = 0; i < money.Length; i++)
                money[i] = number;
        }
        /// <summary>
        /// �Y�B�I
        /// </summary>
        public void chow_pong(Brand brand,BrandPlayer player)
        {
            NowPlayer.add(brand);
            set_Team(player,true);
            //lastBrand = brand;            
        }
        /// <summary>
        /// �b
        /// </summary>
        public void kong(Brand brand,BrandPlayer player)
        {
            NowPlayer.add(brand);
            set_Team(player, true);
            // �b�n�ɤ@�i
            //NowPlayer.add(nextBrand());
        }
        public void BlackKong(Brand brand,BrandPlayer player)
        {
            NowPlayer.add(brand);
            set_Team(player, false);
            //NowPlayer.add(nextBrand());
        }
        /// <summary>
        /// �]�w�s�ո��X
        /// </summary>
        /// <param name="player">���a</param>
        private void set_Team(BrandPlayer player,bool isCanSee)
        {
            teamCount[state]++;
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
        /// �{�b�����a�ɪ�
        /// </summary>
        public bool setFlower()
        {
            bool ans = false;
            int f_count = 0;
            for (int i = 0; i < NowPlayer.getCount(); i++)
                if (NowPlayer.getBrand(i).getClass() == Mahjong.Properties.Settings.Default.Flower &&
                    !NowPlayer.getBrand(i).IsCanSee) // ��P�ӥB���i��
                {
                    NowPlayer.getBrand(i).IsCanSee = true;
                    NowPlayer.getBrand(i).Team = 1;
                    f_count++;
                    ans = true;
                }
            // �ɤW�֪��P��
            for (int i = 0; i < f_count; i++)
                NowPlayer.add( nextTableBrand() );
            return ans;
        }
        /// <summary>
        /// �{�b�����a�Ƨ�
        /// </summary>
        public void sortNowPlayer()
        {
            PlayerSort bs = new PlayerSort(players[state]);
            players[state] = bs.getPlayer();
        }
        /// <summary>
        /// ��P����ୱ�W
        /// </summary>
        /// <param name="brand"></param>
        public void PushToTable(Brand brand)
        {
            brand.IsCanSee = true;
            //NowPlayer.remove(brand);
            show_table.add(brand);
        }
    }
}
