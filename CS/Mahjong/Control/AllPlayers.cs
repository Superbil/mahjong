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
        internal uint state;
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
        int[] money;
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
        int brand_count;
        /// <summary>
        /// �@�x�h�ֿ�
        /// </summary>
        public int one_tai;

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
            this.state = (uint)lo.Winer;
            this.brand_count = 0;
            this.basic_tai = Mahjong.Properties.Settings.Default.BasicTai;
            this.one_tai = Mahjong.Properties.Settings.Default.One_Tai;
            this.teamCount = new int[playernumber];
            this.names = new string[playernumber];
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
                return brand_count;
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
        /// �Ǧ^�{�b����m
        /// </summary>
        public location State
        {
            get
            {
                switch (state)
                {
                    case 0:
                        return location.North;
                    case 1:
                        return location.East;
                    case 2:
                        return location.South;
                    case 3:
                        return location.West;
                }
                return location.East;
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
        /// ���o�γ]�w���a������
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
        /// ���o�γ]�w�{�b���a������
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
        /// �إߵP,�ä��t�P
        /// </summary>
        public void creatBrands()
        {
            factory.createBrands();
            factory.randomBrands();
            table = factory.getBrands();
            for (int i = 0; i < table.getCount(); i++)
                table.getBrand(i).WhoPush = location.Table;
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
            // 1->0->3->2->1
            state--;
            state = state % (uint)countplayers;
        }
        /// <summary>
        /// �N�P
        /// </summary>
        /// <returns>�P</returns>
        public Brand nextBrand()
        {
            if (table.getCount() <= 8) // �O�d8�i���N
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
        public void nextWiner(bool flow)
        {
            // �p�G�y���β��aĹ
            if (flow || this.lo.Winer == this.State)
            {
                this.win_Times++;
            }
            else
            {
                this.lo.next();
                this.win_Times = 1;
            }
            //�]�w�s���}�P��m
            this.lo.setPosition();
            //�s���ୱ�M�P�u�t
            this.table = new BrandPlayer();
            this.factory = new BrandFactory();
            //��s���a
            this.state = (uint)lo.Winer;
            //�]�w�P�թM���P����
            for (int i = 0; i < countplayers; i++)
                teamCount[i] = 1;
            this.brand_count = 0;
            //�M�����X�h���P
            Show_Table.clear();            

        }
        /// <summary>
        /// �]�w���a���򥻪���
        /// </summary>
        /// <param name="number"></param>
        public void setBasicMoney(int number)
        {
            for (int i = 0; i < money.Length; i++)
                money[i] = number;
        }
        /// <summary>
        /// �Y�B�I
        /// </summary>
        public void chow_pong(Brand brand,BrandPlayer player)
        {
            //NowPlayer.add(brand);
            Show_Table.remove(brand);
            set_Team(player,true);        
        }
        /// <summary>
        /// �b
        /// </summary>
        /// <param name="brand">�n�b���P</param>
        /// <param name="player">�i�H�b���P��</param>
        public void kong(Brand brand,BrandPlayer player)
        {
            Show_Table.remove(brand);
            set_Team(player, true);
        }
        /// <summary>
        /// �t�b
        /// </summary>
        /// <param name="brand">�n�b���P</param>
        /// <param name="player">�i�H�b���P��</param>
        public void DarkKong(Brand brand,BrandPlayer player)
        {
            set_Team(player, false);
        }
        /// <summary>
        /// �]�w�s�ո��X
        /// </summary>
        /// <param name="player">���a</param>
        private void set_Team(BrandPlayer player,bool isCanSee)
        {
            teamCount[state]++;
            // ��P�q�{�b���a��W���X
            for (int i = 0; i < player.getCount(); i++)
                NowPlayer.remove(player.getBrand(i));
            // ��P�]���i���åB�[�W�էO���X��[�^�{�b���a
            for (int i = 0; i < player.getCount(); i++)
            {
                player.getBrand(i).IsCanSee = isCanSee;
                player.getBrand(i).Team = teamCount[state];
                NowPlayer.add(player.getBrand(i));
            }
        }
        /// <summary>
        /// �s�C���ɪ�
        /// </summary>
        public bool Newgame_setFlower()
        {
            bool ans = false;
            int f_count = 0;
            for (int i = 0; i < NowPlayer.getCount(); i++)
                if (NowPlayer.getBrand(i).getClass() == Mahjong.Properties.Settings.Default.Flower 
                    && !NowPlayer.getBrand(i).IsCanSee) // ��P�ӥB���i��
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
        /// �{�b�����a�ɪ�
        /// </summary>
        public bool Player_setFlower(Brand brand)
        {
            NowPlayer.add(brand);
            for (int i = 0; i < NowPlayer.getCount(); i++)
                if (NowPlayer.getBrand(i).getClass() == Mahjong.Properties.Settings.Default.Flower
                    && !NowPlayer.getBrand(i).IsCanSee) // ��P�ӥB���i��
                {
                    NowPlayer.getBrand(i).IsCanSee = true;
                    NowPlayer.getBrand(i).Team = 1;
                    return true;
                }
            NowPlayer.remove(brand);
            return false;
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
            brand.WhoPush = State;
            show_table.add(brand);
        }
    }
}
