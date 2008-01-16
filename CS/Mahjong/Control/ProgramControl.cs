using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Mahjong.Forms;
using Mahjong.Brands;
using Mahjong.Players;
using Mahjong.AIs;
using System.Diagnostics;

namespace Mahjong.Control
{
    public partial class ProgramControl : UserControl
    {
        Table table;
        ChatServerForm chat;
        Timer rotateTimer;
        AllPlayers all;
        MahjongAI Ai;
        Information inforamtion;
        Config con;
        bool Player_Push_Brand;

        public ProgramControl()
        {
            InitializeComponent();
            setup();
            table.ShowDialog();
        }
        void setup()
        {
            rotateTimer = new Timer();
            // 顯示Table 介面
            table = new Table(this);
            inforamtion = new Information();
            //Ai = new Level_1();
            con = new Config(table);
            Player_Push_Brand = false;
        }
        void rotateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //touchBrand();
                round();
            }
            catch (GameOverException)
            {
                // 流局
                addWiner();

                RoundEnd(); 
                // 新局
                newgame2();
                               
            }
        }

        private void addWiner()
        {
            all.Win_Times++;
        }
        public void exit()
        {
            Application.Exit();
        }
        public void about()
        {
            new AboutBox();
        }
        public void config()
        {
            con.Dispose();
            con = new Config(table);
            con.Show();
        }
        public void newgame()
        {
            table.cleanAll();
            table.ShowAll = false;
            Ai = new Level_1();
            // 設定4個玩家,每個人16張
            all = new AllPlayers(4, 16);
            table.Setup(all);
            rotateTimer.Interval = 15;
            rotateTimer.Tick += new EventHandler(rotateTimer_Tick);
            newgame2();
        }
        void newgame2()
        {
            all.creatBrands();
            //table.cleanImage();
            table.addImage();
            // 補花
            for (int i = 0; i < 4; i++)
            {
                if (all.setFlower())
                {
                    all.sortNowPlayer();
                    table.updateNowPlayer();
                }
                all.next();
            }
            for (int i = 0; i < 4; i++)
            {
                all.sortNowPlayer();
                table.updateNowPlayer();
                all.next();
            }
            table.updateInforamation();
            rotateTimer.Start();
        }
        /// <summary>
        /// 摸牌
        /// </summary>
        void touchBrand()
        {
            table.updateNowPlayer();
            // 摸牌給現在的玩家
            Brand nextbrand = all.nextBrand();
            all.NowPlayer.add(nextbrand);
            //all.sortNowPlayer();
            table.updateNowPlayer();
            // 補花並更新
            if (all.setFlower())
            {
                all.sortNowPlayer();
                table.updateNowPlayer();
            }
            // 是否胡牌
            Check c = new Check(removeTeam);
            //Brand b;
            if (c.Win())
                RoundEnd();
            else if (c.BlackKong())
            {
                if (all.state == (int)location.South)
                    toUser(nextbrand);
                else
                {
                    if (black_kong_to_AI(nextbrand))
                        touchBrand();
                }
            }
            //else
            //    pushToTable(nextbrand);
        }
        /// <summary>
        /// 打牌
        /// </summary>
        /// <param name="brand">準備要打的牌</param>
        void pushToTable(Brand brand)
        {
            // 把牌從現在的玩家手上移除
            all.NowPlayer.remove(brand);
            // 放到桌面上
            all.PushToTable(brand);
            // 排序現在的玩家
            all.sortNowPlayer();
            // 更新現在玩家和桌面
            updatePlayer_Table();

            // 如果沒有人要吃碰槓，就把牌打到桌面
            if (check_chow_pong_kong_win(brand,0) || Player_Push_Brand)
            {
                //all.PushToTable(brand);
                //updatePlayer_Table();

                //all.next();
                //table.updateInforamation();

                //RoundEnd();                
                Player_Push_Brand = false;
                //rotateTimer.Start();
            }
        }
        bool check_chow_pong_kong_win(Brand brand,int nowstate)
        {
            Check c;
            for (int i = nowstate; i < 3; i++)
            {
                all.next();
                c = new Check(brand, removeTeam);
                // 測試是否被 胡 槓 碰 吃
                if (all.State == location.South)
                {
                    if (c.Chow() || c.Pong() || c.Kong() || c.Win())
                    {
                        toUser(brand);
                        return false;
                    }
                }
                else
                {
                    if (c.Win())
                    {
                        overgame();
                        return false;
                    }
                    else if (c.Kong())
                    {
                        all.kong(brand, c.SuccessPlayer);
                        touchBrand();
                        return false;
                    }
                    else if (c.Pong())
                    {
                        all.chow_pong(brand, c.SuccessPlayer);
                        pushToTable(getfromAI());
                        return false;
                    }
                    else if (c.Chow() && i == 0)
                    {
                        all.chow_pong(brand, c.SuccessPlayer);
                        pushToTable(getfromAI());
                        return false;
                    }
                }
            }
            all.next();
            return true;
        }

        void round()
        {
            rotateTimer.Stop();
            touchBrand();
            if (all.State != location.South)
            {
                Ai.setPlayer(all.NowPlayer);
                pushToTable(Ai.getReadyBrand());
                all.next();
                table.updateInforamation();
                rotateTimer.Start();
            }            
            
        }

        private Brand getfromAI()
        {
            Ai.setPlayer(removeTeam);
            return Ai.getReadyBrand();
        }
        /// <summary>
        ///  一圈結束
        /// </summary>
        private void RoundEnd()
        {
            addWiner();
            all.nextRound(true);
        }

        private bool black_kong_to_AI(Brand brand)
        {
            Check c = new Check(removeTeam);
            Ai.setPlayer(brand, removeTeam);
            //if (Ai.getReadyBrandPlayer().getCount() != null && c.Kong())
            if (c.Kong())
                all.BlackKong(brand, c.SuccessPlayer);
            return true;
        }
        /// <summary>
        /// 把牌丟給玩家，看是否要吃 碰 槓 過水
        /// </summary>
        /// <param name="brand">打到桌面上的牌</param>
        private void toUser(Brand brand)
        {
            // Lister user to Make Brand
            //rotateTimer.Stop();
            CPK cpk = new CPK(this, brand);
            Check c = new Check(brand, removeTeam);
            cpk.Enabled_Button(c.Chow(), c.Pong(), c.Kong(), c.Win());
            if (c.Chow() || c.Pong() || c.Kong() || c.Win())
                cpk.ShowDialog();
        }
        /// <summary>
        /// 結束遊戲
        /// </summary>
        void overgame()
        {
            table.cleanImage();
            rotateTimer.Stop();
            table.ShowAll = true;
            table.addImage();
            Tally t = new Tally();
            t.setLocation(all.getLocation(), all.Win_Times);
            t.setPlayer(all);
            t.ShowDialog();
            RoundEnd();
        }
        /// <summary>
        /// 更新現在的玩家和桌面
        /// </summary>
        void updatePlayer_Table()
        {
            table.updateNowPlayer();
            table.updateTable();
        }
        /// <summary>
        /// 連線設定
        /// </summary>
        internal void onlineGame()
        {
            chat = new ChatServerForm();
            chat.Show();
        }
        /// <summary>
        /// 使用者按下一張牌
        /// </summary>
        /// <param name="brand">按下的牌</param>
        internal void makeBrand(Brand brand)
        {
            Player_Push_Brand = true;
            pushToTable(brand);
            all.next();
            rotateTimer.Start();
        }
        /// <summary>
        /// 設定顯示資訊
        /// </summary>
        internal void setInforamtion()
        {
            inforamtion.setAllPlayers(all);
            inforamtion.Show();
        }
        /// <summary>
        /// 移除掉已經打出去的牌組，以Team編號來區分
        /// </summary>
        BrandPlayer removeTeam
        {
            get
            {
                BrandPlayer bp = new BrandPlayer();
                for (int i = 0; i < all.NowPlayer.getCount(); i++)
                    if (all.NowPlayer.getBrand(i).Team < 1)
                        bp.add(all.NowPlayer.getBrand(i));
                return bp;
            }
        }
        /// <summary>
        /// 吃
        /// </summary>
        internal void chow(Brand brand)
        {
            Check c = new Check(brand, removeTeam);
            if (c.Chow())
                all.chow_pong(brand, c.SuccessPlayer);
            //Player_Push_Brand = true;
            updatePlayer_Table();
        }
        /// <summary>
        /// 碰
        /// </summary>
        internal void pong(Brand brand)
        {
            Check c = new Check(brand, removeTeam);
            if (c.Pong())
                all.chow_pong(brand, c.SuccessPlayer);
            //Player_Push_Brand = true;
            updatePlayer_Table();
        }
        /// <summary>
        /// 槓
        /// </summary>
        internal void kong(Brand brand)
        {
            Check c = new Check(brand, removeTeam);
            if (c.Kong())
                all.kong(brand, c.SuccessPlayer);
            updatePlayer_Table();
            //Player_Push_Brand = true;
            touchBrand();
        }
        /// <summary>
        /// 胡
        /// </summary>
        internal void win()
        {
            overgame();
        }
        /// <summary>
        /// 過水
        /// </summary>
        /// <param name="brand">牌</param>
        internal void pass(Brand brand)
        {
            all.PushToTable(brand);
            table.updateTable();
            Player_Push_Brand = false;
            rotateTimer.Start();
        }

    }
    class GameOverException : Exception
    {
    }
    class NowPlayerException : Exception
    {
    }

}
