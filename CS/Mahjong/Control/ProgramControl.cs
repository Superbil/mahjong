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
        Information information;
        Config con;
        bool Chow_Pong_Brand;
        bool Player_Pass_Brand;
        bool Player_Kong_Brand;


        public ProgramControl()
        {
            InitializeComponent();
            setup();
            table.ShowDialog();
        }
        void setup()
        {
            rotateTimer = new Timer();
            table = new Table(this);
            information = new Information();
            con = new Config(table);
            rotateTimer.Tick += new EventHandler(rotateTimer_Tick);
        }
        void rotateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                round();
            }
            catch (GameOverException)
            {
                // 流局
                MessageBox.Show(Mahjong.Properties.Settings.Default.FlowEnd);
                table.cleanImage();
                all.Show_Table.clear();
                all.nextWiner(true);     
                //RoundEnd();
                // 新局
                newgame2();
            }
            catch (ErrorBrandPlayerCountException)
            {
                MessageBox.Show("相公");
            }
        }

        public void newgame()
        {
            table.cleanAll();
            Ai = new Level_1();
            // 設定4個玩家,每個人16張
            all = new AllPlayers(4, 16);
            table.Setup(all);
            rotateTimer.Interval = Mahjong.Properties.Settings.Default.RunRoundTimes;   
            newgame2();
        }
        void newgame2()
        {
            all.creatBrands();
            table.addImage();
            Chow_Pong_Brand = false;
            Player_Pass_Brand = false;            
            // 補花
            for (int i = 0; i < 4; i++)
            {
                if (all.Newgame_setFlower())
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
            setInforamtion();
            rotateTimer.Start();
        }
        /// <summary>
        /// 摸牌
        /// </summary>
        void touchBrand()
        {
            bool Patch_Flow = false;
            bool Dark_Kong = false;            
            table.updateNowPlayer();
            // 摸牌給現在的玩家
            Brand nextbrand = all.nextBrand();
            // 補花並加上一張牌
            if (all.Player_setFlower(nextbrand))
            {
                touchBrand();
                //all.sortNowPlayer();
                table.updateNowPlayer();
                //Patch_Flow = true;       
            }
            else
            {
                // 是否胡牌或槓牌(手牌加摸到的牌)
                Check win = new Check(nextbrand, all.NowPlayer);
                // 除去顯示牌看是否有暗槓(移除牌組的牌加摸到的牌)
                Check kong = new Check(nextbrand, NowPlayer_removeTeam);
                // 除去顯示或打出的牌看是否有暗槓
                Check darkkong = new Check(NowPlayer_removeTeam);
                // 只有牌組和摸進來的牌做比較
                Check teamKong = new Check(nextbrand, NowPlayer_OnlyTeam);
                if (win.Win())
                {
                    all.NowPlayer.add(nextbrand);
                    table.updateNowPlayer();
                    MessageBox.Show(Mahjong.Properties.Settings.Default.TouchWin, all.Name[all.state].ToString());                    
                    overgame();
                }
                else if (darkkong.DarkKong() || kong.Kong())
                {
                    if (all.State == location.South)
                    {
                        Brand br = null;
                        if (darkkong.DarkKong())
                        {
                            br = darkkong.SuccessPlayer.getBrand(0);
                        }
                        else if (kong.Kong())
                        {
                            br = kong.SuccessPlayer.getBrand(0);
                        }

                        toUser(br, false, false, false, kong.Kong() || darkkong.DarkKong(), false);
                        if (Player_Pass_Brand)
                            Player_Pass_Brand = false;
                        else
                        {
                            //all.NowPlayer.add(nextbrand);
                            table.updateNowPlayer();
                            touchBrand();
                        }
                    }                    
                    else
                    {
                        MessageBox.Show(Mahjong.Properties.Settings.Default.DarkKong, all.Name[all.state].ToString());
                        all.DarkKong(nextbrand, darkkong.SuccessPlayer);
                        all.NowPlayer.add(nextbrand);
                        table.updateNowPlayer();
                        touchBrand();
                    }
                }
                else if (teamKong.Kong())
                {
                    if (all.State == location.South)
                    {
                        toUser(nextbrand, false, false, teamKong.Kong(), false, false);
                        if (Player_Pass_Brand)
                            Player_Pass_Brand = false;
                        else
                        {
                            //all.NowPlayer.add(nextbrand);
                            table.updateNowPlayer();
                            touchBrand();
                        }
                    }
                    else
                    {
                        MessageBox.Show(Mahjong.Properties.Settings.Default.Kong, all.Name[all.state].ToString());
                        all.kong(nextbrand, darkkong.SuccessPlayer);
                        //all.NowPlayer.add(nextbrand);
                        table.updateNowPlayer();
                        touchBrand();
                    }
                }
                else
                {
                    all.NowPlayer.add(nextbrand);
                    table.updateNowPlayer();
                }                
            }
            
        }
        /// <summary>
        /// 打一圈要做的事
        /// </summary>
        void round()
        {
            rotateTimer.Stop();

            if (Chow_Pong_Brand)
                Chow_Pong_Brand = false;                 
            else
                touchBrand();

            if (all.State != location.South)
            {
                if (pushToTable(getfromAI()))
                {
                    all.next();
                    setInforamtion();
                }
                rotateTimer.Start();
            }
            else
                setInforamtion();
            
        }
        /// <summary>
        /// 打牌
        /// </summary>
        /// <param name="brand">準備要打的牌</param>
        bool pushToTable(Brand brand)
        {
            // 把牌從現在的玩家手上移除
            all.NowPlayer.remove(brand);
            // 放到桌面上
            all.PushToTable(brand);
            // 排序現在的玩家
            all.sortNowPlayer();
            // 更新現在玩家和桌面
            updatePlayer_Table();
            // 看沒有人要吃碰槓
            return check_chow_pong_kong_win(brand);
        }
        /// <summary>
        /// 看沒有人要吃碰槓
        /// </summary>
        /// <param name="brand">打出的牌</param>
        /// <returns>是否被拿走了</returns>
        bool check_chow_pong_kong_win(Brand brand)
        {   
            // 有沒有人要胡
            for (int i = 0; i < 3; i++)
            {
                all.next();
                Check w = new Check(brand, all.NowPlayer);
                Ai.setPlayer(brand, all.NowPlayer);
                if (w.Win())
                {
                    if (all.State == location.South)
                    {
                        toUser(brand, false, false, false, false, true);
                        if (Player_Pass_Brand)
                            Player_Pass_Brand = false;
                        else
                            return false;
                    }
                    else if (Ai.Win)
                    {
                        MessageBox.Show(Mahjong.Properties.Settings.Default.Win, all.Name[all.state].ToString());
                        overgame();
                        return false;
                    }
                }
            }
            all.next();

            for (int i = 0; i < 3; i++)
            {
                all.next();    
                Check c = new Check(brand, NowPlayer_removeTeam);
                Check w = new Check(brand, all.NowPlayer);
                Ai.setPlayer(brand, all.NowPlayer);
                // 測試是否被 槓 碰
                if (all.State == location.South)
                {
                    if (c.Pong() || c.Kong())
                    {
                        toUser(brand, (c.Chow() && i == 0), c.Pong(), c.Kong(), false, w.Win());
                        if (Player_Pass_Brand)
                            Player_Pass_Brand = false;
                        else
                            return false;                        
                    }
                }
                else
                {                    
                    if (c.Kong() && Ai.Kong)
                    {
                        setInforamtion();
                        MessageBox.Show(Mahjong.Properties.Settings.Default.Kong, all.Name[all.state].ToString());
                        all.kong(brand, c.SuccessPlayer);
                        //touchBrand();
                        Chow_Pong_Brand = false;
                        updatePlayer_Table();
                        return false;
                    }
                    else if (c.Pong() && Ai.Pong)
                    {
                        setInforamtion();
                        MessageBox.Show(Mahjong.Properties.Settings.Default.Pong, all.Name[all.state].ToString());
                        all.chow_pong(brand, c.SuccessPlayer);
                        //pushToTable(getfromAI());
                        updatePlayer_Table();
                        Chow_Pong_Brand = true;

                        return false;
                    }
                }
            }
            all.next();

            // 有沒有人要吃
            for (int i = 0; i < 3; i++)
            {
                all.next();
                Check c = new Check(brand, NowPlayer_removeTeam);
                Check w = new Check(brand, all.NowPlayer);
                Ai.setPlayer(brand, all.NowPlayer);
                if (c.Chow() && i == 0)
                {
                    if (all.State == location.South)
                    {
                        toUser(brand, (c.Chow() && i == 0), c.Pong(), c.Kong(), false, w.Win());
                        if (Player_Pass_Brand)
                            Player_Pass_Brand = false;
                        else
                            return false;
                    }
                    else if (Ai.Chow)
                    {
                        setInforamtion();
                        MessageBox.Show(Mahjong.Properties.Settings.Default.Chow, all.Name[all.state].ToString());
                        all.chow_pong(brand, c.SuccessPlayer);
                        //pushToTable(getfromAI());
                        updatePlayer_Table();
                        Chow_Pong_Brand = true;

                        return false;
                    }
                }

            }
            all.next();

            return true;
        }
        
        /// <summary>
        /// 從AI得到一張牌
        /// </summary>
        /// <returns></returns>
        private Brand getfromAI()
        {
            Ai.setPlayer(NowPlayer_removeTeam);
            return Ai.getReadyBrand();
        }

        /// <summary>
        ///  一圈結束
        /// </summary>
        private void RoundEnd()
        {            
            all.Show_Table.clear();
        }

        /// <summary>
        /// 把牌丟給玩家，看是否要吃 碰 槓 過水 胡
        /// </summary>
        private void toUser(Brand brand,bool chow,bool pong,bool kong,bool darkkong,bool win)
        {
            CPK cpk = new CPK(this, brand);
            Check c = new Check(brand, NowPlayer_removeTeam);
            Check w = new Check(all.NowPlayer);
            cpk.Enabled_Button(chow,pong,kong,darkkong,win);
            if (chow || pong || kong || win||darkkong)
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

            table.cleanImage();

            all.nextWiner(false);
            all.Show_Table.clear();

            table.ShowAll = false;
            newgame2();
        }
        
        /// <summary>
        /// 使用者按下一張牌
        /// </summary>
        /// <param name="brand">按下的牌</param>
        internal void makeBrand(Brand brand)
        {
            if (pushToTable(brand))
            {
                all.next();
                setInforamtion();
            }
            //else
            //    pushToTable(getfromAI());

            //Chow_Pong_Brand = false;
            rotateTimer.Start();
        }
        
        /// <summary>
        /// 吃
        /// </summary>
        internal void chow(Brand brand)
        {
            Check c = new Check(brand, NowPlayer_removeTeam);
            if (c.Chow())
                all.chow_pong(brand, c.SuccessPlayer);
            Chow_Pong_Brand = true;
            updatePlayer_Table();
        }
        /// <summary>
        /// 碰
        /// </summary>
        internal void pong(Brand brand)
        {
            Check c = new Check(brand, NowPlayer_removeTeam);
            if (c.Pong())
                all.chow_pong(brand, c.SuccessPlayer);
            Chow_Pong_Brand = true;
            updatePlayer_Table();
        }
        /// <summary>
        /// 槓
        /// </summary>
        internal void kong(Brand brand)
        {
            Check c = new Check(brand, NowPlayer_removeTeam);
            Check d = new Check(brand, all.NowPlayer);
            if (c.Kong())
                all.kong(brand, c.SuccessPlayer);
            else if (d.Kong())
                all.kong(brand, d.SuccessPlayer);
            updatePlayer_Table();
            Player_Kong_Brand = true;
        }
        /// <summary>
        /// 暗槓
        /// </summary>
        internal void dark_kong(Brand brand)
        {
            Check c = new Check(brand, NowPlayer_removeTeam);
            Check d = new Check(NowPlayer_removeTeam);
            if (c.Kong())
                all.DarkKong(brand, c.SuccessPlayer);
            if (d.DarkKong())
                all.DarkKong(brand, d.SuccessPlayer);       
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
            Player_Pass_Brand = true;
        }
        /// <summary>
        /// 程式結束
        /// </summary>
        internal void exit()
        {
            Application.Exit();
        }
        /// <summary>
        /// About box
        /// </summary>
        internal void about()
        {
            new AboutBox();
        }
        /// <summary>
        /// config Box
        /// </summary>
        internal void config()
        {
            con.Dispose();
            con = new Config(table);
            con.Show();
        }
        /// <summary>
        /// 設定顯示資訊
        /// </summary>
        internal void setInforamtion()
        {
            information.setAllPlayers(all);
            information.DebugMode = table.ShowAll;
            information.Show();
        }
        /// <summary>
        /// 移除掉已經打出去的牌組，以牌組編號來區分
        /// </summary>
        BrandPlayer NowPlayer_removeTeam
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
        /// 只有牌組
        /// </summary>
        BrandPlayer NowPlayer_OnlyTeam
        {
            get
            {
                BrandPlayer bp = new BrandPlayer();
                for (int i = 0; i < all.NowPlayer.getCount(); i++)
                    if (all.NowPlayer.getBrand(i).Team > 1)
                        bp.add(all.NowPlayer.getBrand(i));
                return bp;
            }
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
    }
    class GameOverException : Exception
    {
    }
    class ErrorBrandPlayerCountException : Exception
    {
    }
}
