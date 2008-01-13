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
        Brand brand_temp;
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
                touchBrand();
            }
            catch (GameOverException)
            {
                // 流局
                addWiner();
                // 新局
                newgame2();
                //RoundEnd();                
            }
            //catch (ArgumentOutOfRangeException)
            //{
            //    MessageBox.Show("玩家是空的");
            //}
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
            rotateTimer.Interval = 1000;
            rotateTimer.Tick += new EventHandler(rotateTimer_Tick);
            newgame2();
        }
        void newgame2()
        {
            all.creatBrands();
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
            table.updateInforamation();
            rotateTimer.Start();
        }
        /// <summary>
        /// 摸牌
        /// </summary>
        void touchBrand()
        {
            rotateTimer.Stop();
            table.updateNowPlayer();
            // 摸牌給現在的玩家
            Brand nextbrand = all.nextBrand();
            all.NowPlayer.add(nextbrand);
            all.sortNowPlayer();
            table.updateNowPlayer();
            // 補花並更新
            if (all.setFlower())
            {
                all.sortNowPlayer();
                table.updateNowPlayer();
            }
            // 是否胡牌
            Check c = new Check(all.NowPlayer);
            //Brand b;
            if (c.Win())
                RoundEnd();
            else if (c.BlackKong())
            {
                if (all.state == (int)location.South)
                    toUser(nextbrand, all.NowPlayer);
                else
                {
                    if (black_kong_to_AI(nextbrand, all.NowPlayer))
                        touchBrand();
                }                
            }
            else
                pushToTable(nextbrand);
        }
        /// <summary>
        /// 打牌
        /// </summary>
        /// <param name="brand">準備要打的牌</param>
        void pushToTable(Brand brand)
        {
            // 把牌從現在的玩家手上移除
            all.NowPlayer.remove(brand);
            // 排序現在的玩家
            all.sortNowPlayer();
            table.updateNowPlayer();

            if (check_chow_pong_kong_win(brand)||Player_Push_Brand)
            {
                all.PushToTable(brand);
                //table.updateTable();
                updatePlayer_Table();
                all.next();
                table.updateInforamation();
                RoundEnd();
                rotateTimer.Start();
                //touchBrand();
                Player_Push_Brand = false;
            }

        }
        bool check_chow_pong_kong_win(Brand brand)
        {
            Check c;
            for (int i = 0; i < 4; i++)
            {
                all.next();
                c = new Check(brand, all.NowPlayer);
                // 測試是否被 胡 槓 碰 吃
                if (all.state == (int)location.South)
                {
                    toUser(brand, all.NowPlayer);
                    table.updateInforamation();
                    table.updateNowPlayer();
                    return false;
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
                        //rotateTimer.Start();
                        touchBrand();
                        return false;
                    }
                    else if (c.Pong())
                    {
                        all.chow_pong(brand, c.SuccessPlayer);
                        pushToTable(getfromAI());
                        return false;
                    }
                    else if (c.Chow())
                    {
                        all.chow_pong(brand, c.SuccessPlayer);
                        pushToTable(getfromAI());
                        return false;
                    }
                }
            }
            return true;
        }

        private Brand getfromAI()
        {
            Ai.setPlayer(all.NowPlayer);
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

        private bool black_kong_to_AI(Brand brand, BrandPlayer player)
        {
            Ai.setPlayer(brand,player);
            if (Ai.getReadyBrandPlayer().getCount() != 0)
                all.BlackKong(brand, player);
            //Ai.getReadyBrand();
            return true;
        }

        private void toUser(Brand brand,BrandPlayer player)
        {
            CPK_Check(brand,player);
            // Lister user to Make Brand
        }        

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

        void CPK_Check(Brand brand,BrandPlayer player) // 玩家的按鈕
        {
            rotateTimer.Stop();
            //MessageBox.Show(brand.getNumber() + brand.getClass());
            brand_temp = brand;
            CPK cpk = new CPK(this,brand);            
            Check c = new Check(brand,player);            
            cpk.Enabled_Button(c.Chow(), c.Pong(), c.Kong(), c.Win());
            if (c.Chow() || c.Pong() || c.Kong() || c.Win())
                cpk.Show();
        }
        void updatePlayer_Table()
        {
            table.updateNowPlayer();
            table.updateTable();
        }
        /// <summary>
        /// 連線設定
        /// </summary>
        public void onlineGame()
        {
            chat = new ChatServerForm();            
            chat.Show();
        }
        /// <summary>
        /// 使用者按下一張牌
        /// </summary>
        /// <param name="brand">按下的牌</param>
        public void makeBrand(Brand brand)
        {
            Player_Push_Brand = true;
            pushToTable(brand);
            rotateTimer.Start();            
        }
        /// <summary>
        /// 設定顯示資訊
        /// </summary>
        public void setInforamtion()
        {
            inforamtion.setAllPlayers(all);
            inforamtion.Show();
        }
        /// <summary>
        /// 吃
        /// </summary>
        public void chow()
        {
            Check c = new Check(brand_temp, all.NowPlayer);
            if (c.Chow())
                all.chow_pong(brand_temp,c.SuccessPlayer);
            //all.next();
            //Player_Push_Brand = true;
            //rotateTimer.Start();
            //table.updateNowPlayer();
            //touchBrand();
            updatePlayer_Table();
        }
        /// <summary>
        /// 碰
        /// </summary>
        public void pong()
        {
            Check c = new Check(brand_temp,all.NowPlayer);
            if (c.Pong())
                all.chow_pong(brand_temp,c.SuccessPlayer);
            //all.next();
            //Player_Push_Brand = true;
            //rotateTimer.Start();
            //table.updateNowPlayer();
            //touchBrand();
            updatePlayer_Table();

        }
        /// <summary>
        /// 槓
        /// </summary>
        public void kong()
        {
            Check c = new Check(brand_temp,all.NowPlayer);
            if (c.Kong())
                all.kong(brand_temp,c.SuccessPlayer);
            //all.next();
            //table.updateNowPlayer();
            updatePlayer_Table();
            //Player_Push_Brand = true;
            //rotateTimer.Start();
            touchBrand();
        }
        /// <summary>
        /// 胡
        /// </summary>
        public void win()
        {
            overgame();      
        }
        /// <summary>
        /// 過水
        /// </summary>
        /// <param name="brand">牌</param>
        public void pass(Brand brand)
        {
            all.PushToTable(brand);
            //table.updateTable();
            updatePlayer_Table();
            //touchBrand();
            //rotateTimer.Start();
        }

    }
    class GameOverException : Exception
    {
    }
    class NowPlayerException : Exception
    {
    }

}
