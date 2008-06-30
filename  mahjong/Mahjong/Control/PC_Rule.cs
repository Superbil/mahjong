using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Mahjong.Brands;
using Mahjong.Forms;
using Mahjong.Players;

namespace Mahjong.Control
{
    public partial class ProgramControl
    {        
        /// <summary>
        /// ���@��n������
        /// </summary>
        internal virtual void round()
        {
            // �p�ɾ�����
            roundTimer.Stop();

            // �p�G�O�Y�θI���N�P
            if (Chow_Pong_Brand)
                Chow_Pong_Brand = false;
            else
                touchBrand();
            // �ثe���A�����󪱮a��
            if (!NowPlayer_isPlayer)
                makeBrand(getfromAI());
            else
                setInforamtion();
        }

        /// <summary>
        /// �ϥΪ�/AI��X�@�i�P
        /// </summary>
        /// <param name="brand">�P</param>
        internal virtual void makeBrand(Brand brand)
        {
            // ��P����ୱ�W�ݬO�_���H�n �J �b �I �Y
            // �Y���ߴN��ܨS���H�n�A�����ߴN��ܳQ�H����
            if (pushToTable(brand))
            {
                // ���U�@�ӤH
                all.next();
                setInforamtion();
            }
            // �p�ɾ����s�Ұ�
            roundTimer.Start();
        }

        /// <summary>
        /// �N�P
        /// </summary>
        internal void touchBrand()
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
                    // ��s��T��
                    setInforamtion();
                    if (showMessageBox)
                        MessageBox.Show(Mahjong.Properties.Settings.Default.TouchWin, all.Name[all.state].ToString());
                    win_game(nextbrand);
                }
                // ��P�t�b(�N�즳�t�b�M��P���N���t�b)
                else if (darkkong.DarkKong() || kong.Kong())
                {
                    // �p�G�O���a
                    if (NowPlayer_isPlayer)
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
                        // �p�G���a���U�L�� �N���L
                        if (Player_Pass_Brand)
                            Player_Pass_Brand = false;
                        else
                        {
                            table.updateNowPlayer();
                            touchBrand();
                        }
                    }
                    // �p�G���O���a
                    else
                    {
                        if (showMessageBox)
                            MessageBox.Show(Mahjong.Properties.Settings.Default.DarkKong, all.Name[all.state].ToString());
                        if (kong.Kong())
                            all.DarkKong(nextbrand, kong.SuccessPlayer);
                        else if (darkkong.DarkKong())
                            all.DarkKong(nextbrand, darkkong.SuccessPlayer);
                        table.updateNowPlayer();
                        touchBrand();
                    }
                }
                // ���I����A�b 
                else if (teamKong.Kong())
                {
                    if (NowPlayer_isPlayer)
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
        /// ���P
        /// </summary>
        /// <param name="brand">�ǳƭn�����P</param>
        internal virtual bool pushToTable(Brand brand)
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
        internal bool check_chow_pong_kong_win(Brand brand)
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
                    if (NowPlayer_isPlayer)
                    {
                        toUser(brand, false, false, false, false, true);
                        // �p�G���a���U�L�� �N���L
                        if (Player_Pass_Brand)
                            Player_Pass_Brand = false;
                        // �Y���O���U�L���N�Ǧ^ ����
                        else
                            return false;
                    }
                    else if (Ai.Win)
                    {
                        // ��s��T��
                        setInforamtion();
                        if (showMessageBox)
                            MessageBox.Show(Mahjong.Properties.Settings.Default.Win, all.Name[all.state].ToString());
                        win_game(brand);
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
                if (NowPlayer_isPlayer)
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
                    if (NowPlayer_isPlayer)
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
        /// �����C��
        /// </summary>
        internal void win_game(Brand brand)
        {
            //�M���ୱ�W���P
            table.cleanImage();
            //����j��
            roundTimer.Stop();
            //��̫ᨺ�i�P�[�J���a��P
            all.NowPlayer.add(brand);
            //�]�w�P���i���ç�s
            table.ShowAll = true;
            table.addImage();
            //�I�s�x�ƭp��
            Tally t = new Tally();
            t.setLocation(all.getLocation, all.Win_Times);
            t.setPlayer(all);
            t.ShowDialog();
            //�M���ୱ�W���P
            table.cleanImage();
            //����U�@�Ӳ�
            all.nextWiner(false);
            //�]�w�������P�������
            table.ShowAll = false;
            //�}�s���C��
            newgame_round();
            this.factory = new BrandFactory();
        }
    }
}
