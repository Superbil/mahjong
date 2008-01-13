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
            // ���Table ����
            table = new Table(this);
            inforamtion = new Information();
            Ai = new Level_1();
            Player_Push_Brand = false;
            //cpk = new CPK(this);
        }
        void rotateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                playgame();
            }
            catch (GameOverException)
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
            //table.ShowAll = true; //��ܩҦ����P
            table.cleanAll();
            // �]�w4�Ӫ��a,�C�ӤH16�i
            all = new AllPlayers(4, 16);
            table.ShowAll = false;
            rotateTimer.Interval = 50;
            rotateTimer.Tick += new EventHandler(rotateTimer_Tick);
            table.Setup(all);
            all.creatBrands();
            table.addImage();
            // �ɪ�
            for (int i = 0; i < 4; i++)
            {
                if (all.setFlower())
                {
                    all.sortNowPlayer();
                    table.updateNowPlayer();
                }
                all.next();
            }
            
            updatePlayer_Table();
            rotateTimer.Start();
        }
        void playgame()
        {            
            
            //updatePlayer_Table();
            table.updateNowPlayer();
            // �N�P���{�b�����a
            Brand nextbrand = all.nextBrand();
            all.NowPlayer.add(nextbrand);

            all.sortNowPlayer();
            table.updateTable();
            // �ɪ�ç�s
            if (all.setFlower())
            {
                all.sortNowPlayer();
                table.updateNowPlayer();
            }
            // �O�_�J�P
            Check c = new Check(all.NowPlayer);
            //else if (c.BlackKong()) // �t�b
            //    blackkong();

            if (all.state == 2) // �H
            {
                rotateTimer.Stop();
            }
            else
            {
                Ai.setPlayer(all.NowPlayer);
                pushToTable(Ai.getReadyBrand());
            }
            

        }

        private void blackkong()
        {
            Check c = new Check(all.NowPlayer);
            if (c.Kong())
                all.kong(c.SuccessPlayer);
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
        }
        void pushToTable(Brand brand)
        {
            all.NowPlayer.remove(brand);
            all.sortNowPlayer();
            table.updateNowPlayer();

            rotateTimer.Stop();

            if (check_chow_pong_kong_win(brand)||Player_Push_Brand)
            {
                all.PushToTable(brand);
                updatePlayer_Table();
                all.next();
                rotateTimer.Start();
            }            
            
        }
        bool check_chow_pong_kong_win(Brand brand)
        {            
            Check c;
            for (int i = 0; i < 4; i++)
            {
                all.next();
                c = new Check(brand, all.NowPlayer);
                // ���լO�_ �Y �I �b
                if (c.Win() || c.Kong() || c.Pong() || c.Chow())
                {
                    if (all.state == 2)
                    {
                        MessageBox.Show(c.Chow().ToString()+c.Pong().ToString()+c.Kong().ToString()+brand.getNumber()+brand.getClass());
                        CPK_Check(brand, all.NowPlayer);
                        return false;
                    }
                    else
                    {
                        //if (c.Win())
                        //    overgame();
                        //break;
                        //return false;
                    }
                }
            }
            return true;
            //all.next();
        }
        void CPK_Check(Brand brand,BrandPlayer player) // ���a�����s
        {
            rotateTimer.Stop();
            //MessageBox.Show(brand.getNumber() + brand.getClass());
            brand_temp = brand;
            CPK cpk = new CPK(this);            
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
        /// �s�u�]�w
        /// </summary>
        public void onlineGame()
        {
            chat = new ChatServerForm();            
            chat.Show();
        }
        /// <summary>
        /// �H���U�@�i�P
        /// </summary>
        /// <param name="brand">���U���P</param>
        public void makeBrand(Brand brand)
        {
            pushToTable(brand);
            rotateTimer.Start();
            Player_Push_Brand = false;
        }
        /// <summary>
        /// �]�w��ܸ�T
        /// </summary>
        public void setInforamtion()
        {
            inforamtion.setAllPlayers(all);
            inforamtion.Show();
        }
        /// <summary>
        /// �Y
        /// </summary>
        public void chow()
        {
            Check c = new Check(brand_temp, all.NowPlayer);
            if (c.Chow())
                all.chow_pong(brand_temp,c.SuccessPlayer);
            //all.next();
            Player_Push_Brand = true;
            //rotateTimer.Start();
            table.updateNowPlayer();
        }
        /// <summary>
        /// �I
        /// </summary>
        public void pong()
        {
            Check c = new Check(brand_temp,all.NowPlayer);
            if (c.Pong())
                all.chow_pong(brand_temp,c.SuccessPlayer);
            //all.next();
            Player_Push_Brand = true;
            //rotateTimer.Start();
            table.updateNowPlayer();

        }
        /// <summary>
        /// �b
        /// </summary>
        public void kong()
        {
            Check c = new Check(brand_temp,all.NowPlayer);
            if (c.Kong())
                all.kong(c.SuccessPlayer);
            //all.next();
            table.updateNowPlayer();
            Player_Push_Brand = true;
            //rotateTimer.Start();
        }
        /// <summary>
        /// �J
        /// </summary>
        public void win()
        {
            overgame();      
        }
    }
    class GameOverException : Exception
    {
    }
    class NowPlayerException : Exception
    {
    }

}
