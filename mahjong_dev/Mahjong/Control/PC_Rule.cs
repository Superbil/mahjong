using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Mahjong.Brands;
using Mahjong.Forms;
using Mahjong.Players;
using System.Threading;
using Mahjong.Properties;
using System.Media;

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
            if (!NowPlayer_is_Real_Player)
            {
                if(PlayerSound)
                    table.PlaySound(getfromAI());
                makeBrand(getfromAI());                
            }
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
                // ��s��T��
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
                CheckBrands win = new CheckBrands(nextbrand, all.NowPlayer);
                // ���h��ܵP�ݬO�_���t�b(�����P�ժ��P�[�N�쪺�P)
                CheckBrands kong = new CheckBrands(nextbrand, NowPlayer_removeTeam);
                // ���h��ܩΥ��X���P�ݬO�_���t�b
                CheckBrands darkkong = new CheckBrands(NowPlayer_removeTeam);
                // �u���P�թM�N�i�Ӫ��P�����
                CheckBrands teamKong = new CheckBrands(nextbrand, NowPlayer_OnlyTeam);
                if (win.Win())
                {
                    // ��s��T��
                    setInforamtion();
                    if (showMessageBox)
                        ShowMessage(Settings.Default.TouchWin);
                    win_game(nextbrand);
                }
                // ��P�t�b(�N�즳�t�b�M��P���N���t�b)
                else if (darkkong.DarkKong() || kong.Kong())
                {
                    // �p�G�O���a
                    if (NowPlayer_is_Real_Player)
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
                            ShowMessage(Mahjong.Properties.Settings.Default.DarkKong);
                        if (kong.Kong())
                        {
                            if (PlayerSound)
                            {
                                soundplayer.Stream = Resources.kong;
                                soundplayer.Play();
                            }
                            all.DarkKong(nextbrand, kong.SuccessPlayer);
                        }
                        else if (darkkong.DarkKong())
                        {
                            if (PlayerSound)
                            {
                                soundplayer.Stream = Resources.kong;
                                soundplayer.Play();
                            }
                            all.DarkKong(nextbrand, darkkong.SuccessPlayer);
                        }
                        table.updateNowPlayer();
                        touchBrand();
                    }
                }
                // ���I����A�b 
                else if (teamKong.Kong())
                {
                    if (NowPlayer_is_Real_Player)
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
                        if (PlayerSound)
                        {
                            soundplayer.Stream = Resources.kong;
                            soundplayer.Play();
                        }
                        if (showMessageBox)
                            ShowMessage(Mahjong.Properties.Settings.Default.Kong);
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
        /// <returns>�O�_�ॴ��ୱ�W</returns>
        internal bool check_chow_pong_kong_win(Brand brand)
        {
            // �O�_���H�n �J
            if (check_win(brand))
                return false;
            // �O�_���H�n �I �b
            if (check_pong_kong(brand))
                return false;
            // �O�_���H�n �Y
            if (check_chow(brand))
                return false;
            // ���\����ୱ�W
            return true;
        }

        private bool check_chow(Brand brand)
        {
            // ���S���H�n�Y
            for (int i = 0; i < 3; i++)
            {
                all.next();
                CheckBrands c = new CheckBrands(brand, NowPlayer_removeTeam);
                CheckBrands w = new CheckBrands(brand, all.NowPlayer);
                Ai.setPlayer(brand, all.NowPlayer);
                // �u���U�a��Y
                if (c.Chow() && i == 0)
                {
                    // �p�G�O�u�ꪱ�a
                    if (NowPlayer_is_Real_Player)
                    {
                        // �O���O���b�������a
                        //if (all.State == table.place.Down)
                        //{
                            toUser(brand, (c.Chow() && i == 0), c.Pong(), c.Kong(), false, w.Win());
                            if (Player_Pass_Brand)
                                Player_Pass_Brand = false;
                            else
                                return true;
                        //}
                        //else
                        //    return true;
                    }
                    else if (Ai.Chow)
                    {
                        setInforamtion();
                        if (PlayerSound)
                        {
                            soundplayer.Stream = Resources.chow;
                            soundplayer.Play();
                        }
                        if (showMessageBox)
                            ShowMessage(Mahjong.Properties.Settings.Default.Chow);
                        all.chow_pong(brand, c.SuccessPlayer);                        
                        updatePlayer_Table();
                        Chow_Pong_Brand = true;
                        return true;
                    }
                }

            }
            all.next();
            return false;
        }

        private bool check_pong_kong(Brand brand)
        {
            // ���լO�_�Q �b �I
            for (int i = 0; i < 3; i++)
            {
                all.next();
                CheckBrands c = new CheckBrands(brand, NowPlayer_removeTeam);
                CheckBrands w = new CheckBrands(brand, all.NowPlayer);
                Ai.setPlayer(brand, all.NowPlayer);
                // �p�G�O���a
                if (NowPlayer_is_Real_Player)
                {
                    if (c.Pong() || c.Kong())
                    {
                        // �O���O���b�������a
                        //if (all.State == table.place.Down)
                        //{
                            toUser(brand, (c.Chow() && i == 0), c.Pong(), c.Kong(), false, w.Win());
                            if (Player_Pass_Brand)
                                Player_Pass_Brand = false;
                            else
                                return true;
                        //}
                        //else
                        //    return true;
                    }
                }
                else
                {
                    // �b
                    if (c.Kong() && Ai.Kong)
                    {
                        setInforamtion();
                        if (PlayerSound)
                        {
                            soundplayer.Stream = Resources.kong;
                            soundplayer.Play();
                        }
                        if (showMessageBox)
                            ShowMessage(Mahjong.Properties.Settings.Default.Kong);
                        all.kong(brand, c.SuccessPlayer);
                        Chow_Pong_Brand = false;
                        updatePlayer_Table();
                        return true;
                    }
                    // �I
                    else if (c.Pong() && Ai.Pong)
                    {
                        setInforamtion();
                        if (PlayerSound)
                        {
                            soundplayer.Stream = Resources.pon;
                            soundplayer.Play();
                        }
                        if (showMessageBox)
                            ShowMessage(Mahjong.Properties.Settings.Default.Pong);
                        all.chow_pong(brand, c.SuccessPlayer);
                        updatePlayer_Table();
                        Chow_Pong_Brand = true;

                        return true;
                    }
                }
            }
            all.next();
            return false;
        }

        private bool check_win(Brand brand)
        {
            // ���S���H�n�J
            for (int i = 0; i < 3; i++)
            {
                all.next();
                CheckBrands w = new CheckBrands(brand, all.NowPlayer);
                Ai.setPlayer(brand, all.NowPlayer);
                if (w.Win())
                {
                    // �p�G�O���a
                    if (NowPlayer_is_Real_Player)
                    {
                        // �O���O���b�������a
                        //if (all.State == table.place.Down)
                        //{
                            toUser(brand, false, false, false, false, true);
                            // �p�G���a���U�L�� �N���L
                            if (Player_Pass_Brand)
                                Player_Pass_Brand = false;
                            // �Y���O���U�L���N�Ǧ^ ����
                            else
                                return true;
                        //}
                        //else
                        //    return true;
                    }
                    else if (Ai.Win)
                    {
                        // ��s��T��
                        setInforamtion();
                        if (PlayerSound)
                        {
                            soundplayer.Stream = Resources.win;
                            soundplayer.Play();
                        }
                        if (showMessageBox)
                            ShowMessage(Mahjong.Properties.Settings.Default.Win);
                        win_game(brand);
                        return true;
                    }
                }
            }
            all.next();
            return false;
        }

        private void ShowMessage(string print_word)
        {
            MessageBox.Show(print_word, all.Name[all.state].ToString());
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
            factory = new BrandFactory();
        }
    }
}
