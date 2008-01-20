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
                // �y��
                MessageBox.Show(Mahjong.Properties.Settings.Default.FlowEnd);
                table.cleanImage();
                all.Show_Table.clear();
                all.nextWiner(true);     
                //RoundEnd();
                // �s��
                newgame2();
            }
            catch (ErrorBrandPlayerCountException)
            {
                MessageBox.Show("�ۤ�");
            }
        }

        public void newgame()
        {
            table.cleanAll();
            Ai = new Level_1();
            // �]�w4�Ӫ��a,�C�ӤH16�i
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
            // �ɪ�
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
        /// �N�P
        /// </summary>
        void touchBrand()
        {
            bool Patch_Flow = false;
            bool Dark_Kong = false;            
            table.updateNowPlayer();
            // �N�P���{�b�����a
            Brand nextbrand = all.nextBrand();
            // �ɪ�å[�W�@�i�P
            if (all.Player_setFlower(nextbrand))
            {
                touchBrand();
                //all.sortNowPlayer();
                table.updateNowPlayer();
                //Patch_Flow = true;       
            }
            else
            {
                // �O�_�J�P�κb�P(��P�[�N�쪺�P)
                Check win = new Check(nextbrand, all.NowPlayer);
                // ���h��ܵP�ݬO�_���t�b(�����P�ժ��P�[�N�쪺�P)
                Check kong = new Check(nextbrand, NowPlayer_removeTeam);
                // ���h��ܩΥ��X���P�ݬO�_���t�b
                Check darkkong = new Check(NowPlayer_removeTeam);
                // �u���P�թM�N�i�Ӫ��P�����
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
        /// ���@��n������
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
            // �ݨS���H�n�Y�I�b
            return check_chow_pong_kong_win(brand);
        }
        /// <summary>
        /// �ݨS���H�n�Y�I�b
        /// </summary>
        /// <param name="brand">���X���P</param>
        /// <returns>�O�_�Q�����F</returns>
        bool check_chow_pong_kong_win(Brand brand)
        {   
            // ���S���H�n�J
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
                // ���լO�_�Q �b �I
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

            // ���S���H�n�Y
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
        /// �qAI�o��@�i�P
        /// </summary>
        /// <returns></returns>
        private Brand getfromAI()
        {
            Ai.setPlayer(NowPlayer_removeTeam);
            return Ai.getReadyBrand();
        }

        /// <summary>
        ///  �@�鵲��
        /// </summary>
        private void RoundEnd()
        {            
            all.Show_Table.clear();
        }

        /// <summary>
        /// ��P�ᵹ���a�A�ݬO�_�n�Y �I �b �L�� �J
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

            table.cleanImage();

            all.nextWiner(false);
            all.Show_Table.clear();

            table.ShowAll = false;
            newgame2();
        }
        
        /// <summary>
        /// �ϥΪ̫��U�@�i�P
        /// </summary>
        /// <param name="brand">���U���P</param>
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
        /// �Y
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
        /// �I
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
        /// �b
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
        /// �t�b
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
            Player_Pass_Brand = true;
        }
        /// <summary>
        /// �{������
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
        /// �]�w��ܸ�T
        /// </summary>
        internal void setInforamtion()
        {
            information.setAllPlayers(all);
            information.DebugMode = table.ShowAll;
            information.Show();
        }
        /// <summary>
        /// �������w�g���X�h���P�աA�H�P�սs���ӰϤ�
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
        /// �u���P��
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
    class ErrorBrandPlayerCountException : Exception
    {
    }
}
