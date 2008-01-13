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
            Ai = new Level_1();
        }
        void rotateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                playgame();
            }
            catch (ArrayTypeMismatchException)
            {
                overgame();
            }
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
            Config con = new Config(table);
            con.Show();
        }
        public void newgame()
        {
            //table.ShowAll = true; //顯示所有的牌
            table.cleanAll();
            // 設定4個玩家,每個人16張
            all = new AllPlayers(4, 16);
            table.ShowAll = false;
            rotateTimer.Interval = 5000;
            rotateTimer.Tick += new EventHandler(rotateTimer_Tick);
            table.Setup(all);
            all.creatBrands();
            table.addImage();
            // 補花
            for (int i = 0; i < 4; i++)
            {
                all.setFlower();
                all.sortNowPlayer();
                all.next();
                updatePlayer_Table();
            }
            
            updatePlayer_Table();
            rotateTimer.Start();
        }
        void playgame()
        {
            // 摸牌給現在的玩家
            Brand nextbrand = all.nextBrand();
            all.NowPlayer.add(nextbrand);
            all.sortNowPlayer();
            updatePlayer_Table();
            // 補花
            all.setFlower();
            updatePlayer_Table();
            // 是否胡牌
            Check c = new Check(all.NowPlayer);
            if (c.Win())
                overgame();
            else if (c.Kong()) // 暗槓
                kong();
            else
            {
                if (all.state == 2) // 人
                {
                    rotateTimer.Stop();
                    // 顯示吃碰槓胡的按鈕
                    CPK cpk = new CPK(this);
                    cpk.Enabled_Button(c.Chow(),c.Pong(),c.Kong(),c.Win());
                    if (c.Chow() || c.Pong() || c.Kong() || c.Win())
                        cpk.Show();
                }
                else
                {
                    if (c.Kong()) // 被槓
                        ;
                    else if (c.Pong()) // 被碰
                        ;
                    else if (c.Chow()) // 被吃
                        ;
                    else
                    {
                        Ai.setPlayer(all.NowPlayer);
                        pushToTable(Ai.getReadyBrand());
                    }
                }
            }

        }

        private void overgame()
        {
            table.cleanImage();
            rotateTimer.Stop();
            table.ShowAll = true;
            table.addImage();
            Tally t = new Tally();
            t.setLocation(all.getLocation(), all.Win_Times);
            t.setPlayer(all);
            t.ShowDialog();
        }
        void pushToTable(Brand brand)
        {
            rotateTimer.Stop();
            all.PushToTable(brand);
            updatePlayer_Table();
            rotateTimer.Start();
            all.next();
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
            chat.ShowDialog();
        }
        /// <summary>
        /// 人按下一張牌
        /// </summary>
        /// <param name="brand">按下的牌</param>
        public void makeBrand(Brand brand)
        {
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
            //all.chow_pong();
        }
        /// <summary>
        /// 碰
        /// </summary>
        public void pong()
        {
            //all.chow_pong();
        }
        /// <summary>
        /// 槓
        /// </summary>
        public void kong()
        {
            //all.kong();
        }
        /// <summary>
        /// 胡
        /// </summary>
        public void win()
        {
            overgame();
        }
    }
}
