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
        /// <summary>
        /// �ୱ����
        /// </summary>
        Table table;
        /// <summary>
        /// �����s�u����
        /// </summary>
        ChatServerForm chat;
        /// <summary>
        /// ����U�@�a���p�ɾ�
        /// </summary>
        Timer roundTimer;
        /// <summary>
        /// �������a�M�ୱ
        /// </summary>
        AllPlayers all;
        /// <summary>
        /// AI����
        /// </summary>
        MahjongAI Ai;
        /// <summary>
        /// ��T��
        /// </summary>
        Information information;
        /// <summary>
        /// �]�w��
        /// </summary>
        Config con;
        /// <summary>
        /// �Y�I�P����O�_�n�ɵP
        /// </summary>
        bool Chow_Pong_Brand;
        /// <summary>
        /// ���a���U�L��
        /// </summary>
        bool Player_Pass_Brand;
        /// <summary>
        /// �O�_�n��ܴ��ܰT��
        /// </summary>
        bool showMessageBox;

        public ProgramControl()
        {
            InitializeComponent();
            setup();
            table.ShowDialog();
        }
        void setup()
        {
            roundTimer = new Timer();
            table = new Table(this);
            information = new Information();
            con = new Config(table);
            roundTimer.Tick += new EventHandler(rotateTimer_Tick);
            showMessageBox = table.SetCheck;
        }
        void rotateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                round();
            }
            catch (FlowOverException)
            {
                // �y��
                MessageBox.Show(Mahjong.Properties.Settings.Default.FlowEnd);
                table.cleanImage();
                all.nextWiner(true);
                // �s��
                newgame2();
            }
            catch (ErrorBrandPlayerCountException)
            {
                MessageBox.Show(Mahjong.Properties.Settings.Default.ErrorBrandPlayer);
            }
        }
        /// <summary>
        /// �}�s�C��(�Ĥ@��)
        /// </summary>
        public void newgame()
        {
            table.cleanAll();
            Ai = new Level_1();
            // �]�w4�Ӫ��a,�C�ӤH16�i
            all = new AllPlayers(4, 16);
            table.Setup(all);
            roundTimer.Interval = Mahjong.Properties.Settings.Default.RunRoundTimes;
            newgame2();
        }
        /// <summary>
        /// �}�s��
        /// </summary>
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
            roundTimer.Start();
        }
        /// <summary>
        /// �N�P
        /// </summary>
        void touchBrand()
        {
            table.updateNowPlayer();
            // �N�P���{�b�����a
            Brand nextbrand = all.nextBrand();
            // �ɪ�å[�W�@�i�P
            if (all.Player_setFlower(nextbrand))
            {
                touchBrand();
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
                    if (showMessageBox)
                        MessageBox.Show(Mahjong.Properties.Settings.Default.TouchWin, all.Name[all.state].ToString());
                    win_game();
                }
                    // ��P�t�b(�N�즳�t�b�M��P���N���t�b)
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
                            table.updateNowPlayer();
                            touchBrand();
                        }
                    }
                    else
                    {
                        if (showMessageBox)
                            MessageBox.Show(Mahjong.Properties.Settings.Default.DarkKong, all.Name[all.state].ToString());
                        if (kong.Kong())
                            all.DarkKong(nextbrand, kong.SuccessPlayer);
                        if (darkkong.DarkKong())
                            all.DarkKong(nextbrand, darkkong.SuccessPlayer);
                        all.NowPlayer.add(nextbrand);
                        table.updateNowPlayer();
                        touchBrand();
                    }
                }
                    // ���I����A�b 
                else if (teamKong.Kong())
                {
                    if (all.State == location.South)
                    {
                        toUser(nextbrand, false, false, teamKong.Kong(), false, false);
                        if (Player_Pass_Brand)
                            Player_Pass_Brand = false;
                        else
                        {
                            table.updateNowPlayer();
                            touchBrand();
                        }
                    }
                    else
                    {
                        if (showMessageBox)
                            MessageBox.Show(Mahjong.Properties.Settings.Default.Kong, all.Name[all.state].ToString());
                        all.kong(nextbrand, darkkong.SuccessPlayer);
                        table.updateNowPlayer();
                        touchBrand();
                    }
                }
                else
                {
                    // ��P�[�J���a
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
            // �p�ɾ�����
            roundTimer.Stop();

            // �p�G�O�Y�θI���N�P
            if (Chow_Pong_Brand)
                Chow_Pong_Brand = false;
            else
                touchBrand();
            // �ثe���A�����󪱮a��
            if (all.State != location.South)
            {
                // ��P����ୱ�W�ݬO�_���H�n �J �b �I �Y
                if (pushToTable(getfromAI()))
                {
                    // ���U�@�ӤH
                    all.next();
                    setInforamtion();
                }
                // �p�ɾ����s�Ұ�
                roundTimer.Start();
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
            // �ݬO�_���H�n �J �b �I �Y
            return check_chow_pong_kong_win(brand);
        }
        /// <summary>
        /// �O�_���H�n �J �b �I �Y
        /// </summary>
        /// <param name="brand">���X��ୱ���P</param>
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
                    // �p�G�O���a
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
                        if (showMessageBox)
                            MessageBox.Show(Mahjong.Properties.Settings.Default.Win, all.Name[all.state].ToString());
                        win_game();
                        return false;
                    }
                }
            }
            all.next();

            // ���լO�_�Q �b �I
            for (int i = 0; i < 3; i++)
            {
                all.next();
                Check c = new Check(brand, NowPlayer_removeTeam);
                Check w = new Check(brand, all.NowPlayer);
                Ai.setPlayer(brand, all.NowPlayer);                
                // �p�G�O���a
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
                    // �b
                    if (c.Kong() && Ai.Kong)
                    {
                        setInforamtion();
                        if (showMessageBox)
                            MessageBox.Show(Mahjong.Properties.Settings.Default.Kong, all.Name[all.state].ToString());
                        all.kong(brand, c.SuccessPlayer);
                        Chow_Pong_Brand = false;
                        updatePlayer_Table();
                        return false;
                    }
                    // �I
                    else if (c.Pong() && Ai.Pong)
                    {
                        setInforamtion();
                        if (showMessageBox)
                            MessageBox.Show(Mahjong.Properties.Settings.Default.Pong, all.Name[all.state].ToString());
                        all.chow_pong(brand, c.SuccessPlayer);
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
                    // �p�G�O���a
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
                        if (showMessageBox)
                            MessageBox.Show(Mahjong.Properties.Settings.Default.Chow, all.Name[all.state].ToString());
                        all.chow_pong(brand, c.SuccessPlayer);
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
        /// ��P�ᵹ���a�A�ݬO�_�n�Y �I �b �L�� �J
        /// </summary>
        private void toUser(Brand brand, bool chow,bool pong,bool kong,bool darkkong,bool win)
        {
            CPK cpk = new CPK(this, brand);
            Check c = new Check(brand, NowPlayer_removeTeam);
            Check w = new Check(all.NowPlayer);
            cpk.Enabled_Button(chow, pong, kong, darkkong, win);
            if (chow || pong || kong || win || darkkong)
                cpk.ShowDialog();
        }

        /// <summary>
        /// �����C��
        /// </summary>
        void win_game()
        {
            //�M���ୱ�W���P
            table.cleanImage();
            //����j��
            roundTimer.Stop();
            //�]�w�P���i���ç�s
            table.ShowAll = true;
            table.addImage();
            //�I�s�x�ƭp��
            Tally t = new Tally();
            t.setLocation(all.getLocation(), all.Win_Times);
            t.setPlayer(all);
            t.ShowDialog();
            //�M���ୱ�W���P
            table.cleanImage();
            //����U�@�Ӳ�
            all.nextWiner(false);
            //�]�w�������P�������
            table.ShowAll = false;
            //�}�s���C��
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
            roundTimer.Start();
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
            win_game();
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
        /// <summary>
        /// ���ܸ�T�O�_�}��
        /// </summary>
        internal bool ShowMessageBox
        {
            set
            {
                showMessageBox = value;
            }
        }
    }
    /// <summary>
    /// �y���ҥ~
    /// </summary>
    class FlowOverException : Exception
    {
    }
    /// <summary>
    /// �ۤ��ҥ~
    /// </summary>
    class ErrorBrandPlayerCountException : Exception
    {
    }
}
