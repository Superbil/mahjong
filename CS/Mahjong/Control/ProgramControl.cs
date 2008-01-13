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
            // ���Table ����
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
                // �y��
                addWiner();
                // �s��
                newgame2();
                //RoundEnd();                
            }
            //catch (ArgumentOutOfRangeException)
            //{
            //    MessageBox.Show("���a�O�Ū�");
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
            // �]�w4�Ӫ��a,�C�ӤH16�i
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
            table.updateInforamation();
            rotateTimer.Start();
        }
        /// <summary>
        /// �N�P
        /// </summary>
        void touchBrand()
        {
            rotateTimer.Stop();
            table.updateNowPlayer();
            // �N�P���{�b�����a
            Brand nextbrand = all.nextBrand();
            all.NowPlayer.add(nextbrand);
            all.sortNowPlayer();
            table.updateNowPlayer();
            // �ɪ�ç�s
            if (all.setFlower())
            {
                all.sortNowPlayer();
                table.updateNowPlayer();
            }
            // �O�_�J�P
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
        /// ���P
        /// </summary>
        /// <param name="brand">�ǳƭn�����P</param>
        void pushToTable(Brand brand)
        {
            // ��P�q�{�b�����a��W����
            all.NowPlayer.remove(brand);
            // �Ƨǲ{�b�����a
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
                // ���լO�_�Q �J �b �I �Y
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
        ///  �@�鵲��
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

        void CPK_Check(Brand brand,BrandPlayer player) // ���a�����s
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
        /// �s�u�]�w
        /// </summary>
        public void onlineGame()
        {
            chat = new ChatServerForm();            
            chat.Show();
        }
        /// <summary>
        /// �ϥΪ̫��U�@�i�P
        /// </summary>
        /// <param name="brand">���U���P</param>
        public void makeBrand(Brand brand)
        {
            Player_Push_Brand = true;
            pushToTable(brand);
            rotateTimer.Start();            
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
            //Player_Push_Brand = true;
            //rotateTimer.Start();
            //table.updateNowPlayer();
            //touchBrand();
            updatePlayer_Table();
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
            //Player_Push_Brand = true;
            //rotateTimer.Start();
            //table.updateNowPlayer();
            //touchBrand();
            updatePlayer_Table();

        }
        /// <summary>
        /// �b
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
        /// �J
        /// </summary>
        public void win()
        {
            overgame();      
        }
        /// <summary>
        /// �L��
        /// </summary>
        /// <param name="brand">�P</param>
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
