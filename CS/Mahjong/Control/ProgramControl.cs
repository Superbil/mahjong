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
        bool Player_Chow_Pong_Brand;
        bool Player_Pass_Brand;

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
            Player_Chow_Pong_Brand = false;
            Player_Pass_Brand = false;
        }
        void rotateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                round();
            }
            catch (GameOverException)
            {
                MessageBox.Show("�y��");
                // �y��
                addWiner();

                RoundEnd(); 
                // �s��
                newgame2();
                               
            }
        }

        public void newgame()
        {
            table.cleanAll();
            table.ShowAll = false;
            Ai = new Level_1();
            // �]�w4�Ӫ��a,�C�ӤH16�i
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
        /// �N�P
        /// </summary>
        void touchBrand()
        {
            table.updateNowPlayer();
            // �N�P���{�b�����a
            Brand nextbrand = all.nextBrand();
            all.NowPlayer.add(nextbrand);
            //all.sortNowPlayer();
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
            else if (c.DarkKong())
            {
                if (all.state == (int)location.South)
                    toUser(nextbrand);
                else
                {
                    dark_kong_to_AI(nextbrand);
                    //if (dark_kong_to_AI(nextbrand))
                    //    touchBrand();
                }
            }
        }
        /// <summary>
        /// ���P
        /// </summary>
        /// <param name="brand">�ǳƭn�����P</param>
        bool pushToTable(Brand brand)
        {
            // ��P�q�{�b�����a��W����
            all.NowPlayer.remove(brand);
            // ���ୱ�W
            all.PushToTable(brand);
            // �Ƨǲ{�b�����a
            all.sortNowPlayer();
            // ��s�{�b���a�M�ୱ
            updatePlayer_Table();

            // �p�G�ݨS���H�n�Y�I�b
            return check_chow_pong_kong_win(brand);
            //if (check_chow_pong_kong_win(brand))
            //{
                //all.PushToTable(brand);
                //updatePlayer_Table();

                //all.next();
                //table.updateInforamation();

                //RoundEnd();                
                //rotateTimer.Start();
            //}
        }
        bool check_chow_pong_kong_win(Brand brand)
        {
            Check c;
            for (int i = 0; i < 3; i++)
            {
                all.next();
                c = new Check(brand, removeTeam);
                // ���լO�_�Q �J �b �I �Y
                if (all.State == location.South)
                {
                    if (c.Chow() || c.Pong() || c.Kong() || c.Win())
                    {
                        toUser(brand);
                        if (!Player_Pass_Brand)
                        {
                            Player_Pass_Brand = false;
                            return false;
                        }
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
                        MessageBox.Show(Mahjong.Properties.Settings.Default.Kong, all.Name[all.state].ToString());
                        all.kong(brand, c.SuccessPlayer);
                        touchBrand();                        
                        return false;
                    }
                    else if (c.Pong())
                    {
                        MessageBox.Show(Mahjong.Properties.Settings.Default.Pong, all.Name[all.state].ToString());
                        all.chow_pong(brand, c.SuccessPlayer);
                        pushToTable(getfromAI());
                        return false;
                    }
                    else if (c.Chow() && i == 2)
                    {
                        MessageBox.Show(Mahjong.Properties.Settings.Default.Chow, all.Name[all.state].ToString());
                        all.chow_pong(brand, c.SuccessPlayer);
                        pushToTable(getfromAI());
                        return false;
                    }
                }
            }
            all.next();
            return true;
        }
        /// <summary>
        /// ���@��n������
        /// </summary>
        void round()
        {
            rotateTimer.Stop();
            if (!Player_Chow_Pong_Brand)
            {
                touchBrand();
                Player_Chow_Pong_Brand = false;
            }
            if (all.State != location.South)
            {
                Ai.setPlayer(removeTeam);
                if (pushToTable(Ai.getReadyBrand()))
                {
                    all.next();
                    table.updateInforamation();
                }
                rotateTimer.Start();
            } 
            
        }

        private Brand getfromAI()
        {
            Ai.setPlayer(removeTeam);
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

        private bool dark_kong_to_AI(Brand brand)
        {
            MessageBox.Show(Mahjong.Properties.Settings.Default.Kong, all.Name[all.state].ToString());
            Check c = new Check(removeTeam);
            Ai.setPlayer(brand, removeTeam);
            if (c.Kong())
                all.DarkKong(brand, c.SuccessPlayer);
            return true;
        }
        /// <summary>
        /// ��P�ᵹ���a�A�ݬO�_�n�Y �I �b �L��
        /// </summary>
        /// <param name="brand">����ୱ�W���P</param>
        private void toUser(Brand brand)
        {
            CPK cpk = new CPK(this, brand);
            Check c = new Check(brand, removeTeam);
            cpk.Enabled_Button(c.Chow(), c.Pong(), c.Kong(), c.Win());
            if (c.Chow() || c.Pong() || c.Kong() || c.Win())
                cpk.ShowDialog();
        }
        /// <summary>
        /// �����C��
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
        /// �ϥΪ̫��U�@�i�P
        /// </summary>
        /// <param name="brand">���U���P</param>
        internal void makeBrand(Brand brand)
        {
            pushToTable(brand);
            all.next();
            Player_Chow_Pong_Brand = false;
            rotateTimer.Start();
        }
        
        /// <summary>
        /// �Y
        /// </summary>
        internal void chow(Brand brand)
        {
            Check c = new Check(brand, removeTeam);
            if (c.Chow())
                all.chow_pong(brand, c.SuccessPlayer);
            Player_Chow_Pong_Brand = true;
            updatePlayer_Table();
        }
        /// <summary>
        /// �I
        /// </summary>
        internal void pong(Brand brand)
        {
            Check c = new Check(brand, removeTeam);
            if (c.Pong())
                all.chow_pong(brand, c.SuccessPlayer);
            Player_Chow_Pong_Brand = true;
            updatePlayer_Table();
        }
        /// <summary>
        /// �b
        /// </summary>
        internal void kong(Brand brand)
        {
            Check c = new Check(brand, removeTeam);
            if (c.Kong())
                all.kong(brand, c.SuccessPlayer);
            updatePlayer_Table();
            //touchBrand();
        }
        /// <summary>
        /// �J
        /// </summary>
        internal void win()
        {
            overgame();
        }
        /// <summary>
        /// �L��
        /// </summary>
        /// <param name="brand">�P</param>
        internal void pass(Brand brand)
        {
            all.PushToTable(brand);
            table.updateTable();
            Player_Pass_Brand = true;
        }
        private void addWiner()
        {
            all.Win_Times++;
        }
        /// <summary>
        /// �{������
        /// </summary>
        public void exit()
        {
            Application.Exit();
        }
        /// <summary>
        /// About box
        /// </summary>
        public void about()
        {
            new AboutBox();
        }
        /// <summary>
        /// config Box
        /// </summary>
        public void config()
        {
            con.Dispose();
            con = new Config(table);
            con.Show();
        }
        /// <summary>
        /// �]�w��ܸ�T
        /// </summary>
        internal void setInforamtion()
        {
            inforamtion.setAllPlayers(all);
            inforamtion.Show();
        }
        /// <summary>
        /// �������w�g���X�h���P�աA�HTeam�s���ӰϤ�
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
        /// ��s�{�b�����a�M�ୱ
        /// </summary>
        void updatePlayer_Table()
        {
            table.updateNowPlayer();
            table.updateTable();
        }
        /// <summary>
        /// �s�u�]�w
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
    class NowPlayerException : Exception
    {
    }

}
