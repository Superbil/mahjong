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
        /// 打一圈要做的事
        /// </summary>
        internal virtual void round()
        {            
            // 計時器停止
            roundTimer.Stop();

            // 如果是吃或碰不摸牌
            if (Chow_Pong_Brand)
                Chow_Pong_Brand = false;
            else
                touchBrand();
            // 目前狀態不等於玩家時
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
        /// 使用者/AI丟出一張牌
        /// </summary>
        /// <param name="brand">牌</param>
        internal virtual void makeBrand(Brand brand)
        {
            // 把牌打到桌面上看是否有人要 胡 槓 碰 吃
            // 若成立就表示沒有人要，不成立就表示被人拿走
            if (pushToTable(brand))
            {
                // 換下一個人
                all.next();
                // 更新資訊盒
                setInforamtion();
            }
            // 計時器重新啟動
            roundTimer.Start();            
        }

        /// <summary>
        /// 摸牌
        /// </summary>
        internal void touchBrand()
        {
            table.updateNowPlayer();
            // 摸牌給現在的玩家
            Brand nextbrand = all.nextBrand();
            // 補花並加上一張牌
            if (all.Player_setFlower(nextbrand))
            {
                touchBrand();
            }
            else
            {
                // 是否胡牌或槓牌(手牌加摸到的牌)
                CheckBrands win = new CheckBrands(nextbrand, all.NowPlayer);
                // 除去顯示牌看是否有暗槓(移除牌組的牌加摸到的牌)
                CheckBrands kong = new CheckBrands(nextbrand, NowPlayer_removeTeam);
                // 除去顯示或打出的牌看是否有暗槓
                CheckBrands darkkong = new CheckBrands(NowPlayer_removeTeam);
                // 只有牌組和摸進來的牌做比較
                CheckBrands teamKong = new CheckBrands(nextbrand, NowPlayer_OnlyTeam);
                if (win.Win())
                {
                    // 更新資訊盒
                    setInforamtion();
                    if (showMessageBox)
                        ShowMessage(Settings.Default.TouchWin);
                    win_game(nextbrand);
                }
                // 手牌暗槓(摸到有暗槓和手牌中就有暗槓)
                else if (darkkong.DarkKong() || kong.Kong())
                {
                    // 如果是玩家
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
                        // 如果玩家按下過水 就跳過
                        if (Player_Pass_Brand)
                            Player_Pass_Brand = false;
                        else
                        {
                            table.updateNowPlayer();
                            touchBrand();
                        }
                    }
                    // 如果不是玩家
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
                // 明碰之後再槓 
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
                    // 把牌加入玩家
                    all.NowPlayer.add(nextbrand);
                    table.updateNowPlayer();
                }
            }

        }

        /// <summary>
        /// 打牌
        /// </summary>
        /// <param name="brand">準備要打的牌</param>
        internal virtual bool pushToTable(Brand brand)
        {
            
            // 把牌從現在的玩家手上移除
            all.NowPlayer.remove(brand);
            // 放到桌面上
            all.PushToTable(brand);
            // 排序現在的玩家
            all.sortNowPlayer();
            // 更新現在玩家和桌面
            updatePlayer_Table();
            // 看是否有人要 胡 槓 碰 吃
            return check_chow_pong_kong_win(brand);
        }
        /// <summary>
        /// 是否有人要 胡 槓 碰 吃
        /// </summary>
        /// <param name="brand">打出到桌面的牌</param>
        /// <returns>是否能打到桌面上</returns>
        internal bool check_chow_pong_kong_win(Brand brand)
        {
            // 是否有人要 胡
            if (check_win(brand))
                return false;
            // 是否有人要 碰 槓
            if (check_pong_kong(brand))
                return false;
            // 是否有人要 吃
            if (check_chow(brand))
                return false;
            // 成功打到桌面上
            return true;
        }

        private bool check_chow(Brand brand)
        {
            // 有沒有人要吃
            for (int i = 0; i < 3; i++)
            {
                all.next();
                CheckBrands c = new CheckBrands(brand, NowPlayer_removeTeam);
                CheckBrands w = new CheckBrands(brand, all.NowPlayer);
                Ai.setPlayer(brand, all.NowPlayer);
                // 只有下家能吃
                if (c.Chow() && i == 0)
                {
                    // 如果是真實玩家
                    if (NowPlayer_is_Real_Player)
                    {
                        // 是不是正在玩的玩家
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
            // 測試是否被 槓 碰
            for (int i = 0; i < 3; i++)
            {
                all.next();
                CheckBrands c = new CheckBrands(brand, NowPlayer_removeTeam);
                CheckBrands w = new CheckBrands(brand, all.NowPlayer);
                Ai.setPlayer(brand, all.NowPlayer);
                // 如果是玩家
                if (NowPlayer_is_Real_Player)
                {
                    if (c.Pong() || c.Kong())
                    {
                        // 是不是正在玩的玩家
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
                    // 槓
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
                    // 碰
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
            // 有沒有人要胡
            for (int i = 0; i < 3; i++)
            {
                all.next();
                CheckBrands w = new CheckBrands(brand, all.NowPlayer);
                Ai.setPlayer(brand, all.NowPlayer);
                if (w.Win())
                {
                    // 如果是玩家
                    if (NowPlayer_is_Real_Player)
                    {
                        // 是不是正在玩的玩家
                        //if (all.State == table.place.Down)
                        //{
                            toUser(brand, false, false, false, false, true);
                            // 如果玩家按下過水 就跳過
                            if (Player_Pass_Brand)
                                Player_Pass_Brand = false;
                            // 若不是按下過水就傳回 失敗
                            else
                                return true;
                        //}
                        //else
                        //    return true;
                    }
                    else if (Ai.Win)
                    {
                        // 更新資訊盒
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
        /// 結束遊戲
        /// </summary>
        internal void win_game(Brand brand)
        {
            //清除桌面上的牌
            table.cleanImage();
            //停止迴圈
            roundTimer.Stop();
            //把最後那張牌加入玩家手牌
            all.NowPlayer.add(brand);
            //設定牌為可視並更新
            table.ShowAll = true;
            table.addImage();
            //呼叫台數計算
            Tally t = new Tally();
            t.setLocation(all.getLocation, all.Win_Times);
            t.setPlayer(all);
            t.ShowDialog();
            //清除桌面上的牌
            table.cleanImage();
            //換到下一個莊
            all.nextWiner(false);
            //設定全部的牌為不顯示
            table.ShowAll = false;
            //開新的遊戲
            newgame_round();
            factory = new BrandFactory();
        }
    }
}
