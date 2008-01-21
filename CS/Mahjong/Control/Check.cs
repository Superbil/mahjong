using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using Mahjong.Brands;

namespace Mahjong.Control
{
    /// <summary>
    /// 牌的狀態檢查
    /// </summary>
    class Check
    {
        BrandPlayer x = new BrandPlayer();
        BrandPlayer a = new BrandPlayer();
        BrandPlayer b = new BrandPlayer();
        BrandPlayer c = new BrandPlayer();
        BrandPlayer d = new BrandPlayer();
        Brand brand;
        BrandPlayer ans_player = new BrandPlayer();
        BrandPlayer[] chow_player = new BrandPlayer[3];
        uint chow_index = 0;
        /// <summary>
        /// 牌的狀態檢查 暗槓
        /// </summary>
        /// <param name="player">牌玩家</param>
        public Check(BrandPlayer player)
        {
            for (int i = 0; i < player.getCount(); i++)
                if (player.getBrand(i).getClass() != Mahjong.Properties.Settings.Default.Flower)
                    x.add(player.getBrand(i));
            for (int i = 0; i < chow_player.Length; i++)
                chow_player[i] = new BrandPlayer();
        }
        /// <summary>
        /// 牌的狀態檢查 吃 碰 槓 胡
        /// </summary>
        /// <param name="otherbrand">進來的牌</param>
        /// <param name="player">成立的玩家組合</param>
        public Check(Brand otherbrand, BrandPlayer player)
        {
            for (int i = 0; i < player.getCount(); i++)
                if (player.getBrand(i).getClass() != Mahjong.Properties.Settings.Default.Flower)
                    x.add(player.getBrand(i));
            this.brand = otherbrand;
            for (int i = 0; i < chow_player.Length; i++)
                chow_player[i] = new BrandPlayer();
        }
        /// <summary>
        /// 吃 成立
        /// </summary>
        /// <returns>是/否</returns>
        public bool Chow()
        {
            
            bool chow_bool = false;
            ans_player.clear();
            if (brand != null && brand.getClass() !=Mahjong.Properties.Settings.Default.Wordtiles )
                for (int i = 0; i < x.getCount() - 1; i++)
                    for (int j = i + 1; j < x.getCount() - 1; j++)
                    {
                        if ( // 345
                            brand.getClass() == x.getBrand(i).getClass() &&
                            brand.getNumber() + 1 == x.getBrand(i).getNumber() &&
                            brand.getClass() == x.getBrand(j).getClass() &&
                            brand.getNumber() + 2 == x.getBrand(j).getNumber())
                        {
                            //ans_player.add(brand);
                            //ans_player.add(x.getBrand(i));
                            //ans_player.add(x.getBrand(j));
                            chow_player[chow_index].add(brand);
                            chow_player[chow_index].add(x.getBrand(i));
                            chow_player[chow_index].add(x.getBrand(j));
                            chow_index++;
                            chow_bool = true;
                        }
                        if ( // 234
                            brand.getClass() == x.getBrand(i).getClass() &&
                            brand.getNumber() - 1 == x.getBrand(i).getNumber() &&
                            brand.getClass() == x.getBrand(j).getClass() &&
                            brand.getNumber() + 1 == x.getBrand(j).getNumber())
                        {
                            //ans_player.add(brand);
                            //ans_player.add(x.getBrand(i));
                            //ans_player.add(x.getBrand(j));
                            chow_player[chow_index].add(brand);
                            chow_player[chow_index].add(x.getBrand(i));
                            chow_player[chow_index].add(x.getBrand(j));
                            chow_index++;
                            chow_bool = true;
                        }
                        else if ( // 123
                       brand.getClass() == x.getBrand(i).getClass() &&
                       brand.getNumber() - 2 == x.getBrand(i).getNumber() &&
                       brand.getClass() == x.getBrand(j).getClass() &&
                       brand.getNumber() - 1 == x.getBrand(j).getNumber())
                        {
                            //ans_player.add(brand);
                            //ans_player.add(x.getBrand(i));
                            //ans_player.add(x.getBrand(j));
                            chow_player[chow_index].add(brand);
                            chow_player[chow_index].add(x.getBrand(i));
                            chow_player[chow_index].add(x.getBrand(j));
                            chow_index++;
                            chow_bool = true;
                        }
                    }
            return chow_bool;
        }
        /// <summary>
        /// 成立的吃的牌組
        /// </summary>
        public BrandPlayer[] ChowPlayer
        {
            get
            {
                return chow_player;
            }
        }
        /// <summary>
        /// 傳回吃成立有多少組
        /// </summary>
        public int ChowLength
        {
            get
            {
                return (int)chow_index;
            }
        }
        /// <summary>
        /// 碰 成立
        /// </summary>
        /// <returns>是/否</returns>
        public bool Pong()
        {
            ans_player.clear();
            if (brand != null)
                for (int i = 0; i < x.getCount() - 1; i++)
                    if (
                        brand.getClass() == x.getBrand(i).getClass() &&
                        brand.getNumber() == x.getBrand(i).getNumber() &&
                        brand.getClass() == x.getBrand(i + 1).getClass() &&
                        brand.getNumber() == x.getBrand(i + 1).getNumber()
                        )
                    {
                        ans_player.add(brand);
                        ans_player.add(x.getBrand(i));
                        ans_player.add(x.getBrand(i + 1));
                        return true;
                    }
            return false;
        }
        /// <summary>
        /// 暗槓 成立
        /// </summary>
        /// <returns>是/否</returns>
        public bool DarkKong()
        {
            ans_player.clear();
            for (int i = 0; i < x.getCount() - 3; i++)
                if (
                    x.getBrand(i).getClass() == x.getBrand(i + 1).getClass() &&
                    x.getBrand(i).getNumber() == x.getBrand(i + 1).getNumber() &&
                    x.getBrand(i).getClass() == x.getBrand(i + 2).getClass() &&
                    x.getBrand(i).getNumber() == x.getBrand(i + 2).getNumber() &&
                    x.getBrand(i).getClass() == x.getBrand(i + 3).getClass() &&
                    x.getBrand(i).getNumber() == x.getBrand(i + 3).getNumber()
                    )
                {
                    ans_player.add(x.getBrand(i));
                    ans_player.add(x.getBrand(i+1));
                    ans_player.add(x.getBrand(i+2));
                    ans_player.add(x.getBrand(i+3));
                    return true;
                }
            return false;
        }
        /// <summary>
        /// 槓 成立
        /// </summary>
        /// <returns>是/否</returns>
        public bool Kong()
        {
            ans_player.clear();
            if (brand != null)
            {
                for (int i = 0; i < x.getCount() - 2; i++)
                    if (
                        brand.getClass() == x.getBrand(i).getClass() &&
                        brand.getNumber() == x.getBrand(i).getNumber() &&
                        brand.getClass() == x.getBrand(i + 1).getClass() &&
                        brand.getNumber() == x.getBrand(i + 1).getNumber() &&
                        brand.getClass() == x.getBrand(i + 2).getClass() &&
                        brand.getNumber() == x.getBrand(i + 2).getNumber()
                        )
                    {
                        ans_player.add(brand);
                        ans_player.add(x.getBrand(i));
                        ans_player.add(x.getBrand(i + 1));
                        ans_player.add(x.getBrand(i + 2));
                        return true;
                    }
                return false;
            }
            else
                return false;

        }
        /// <summary>
        /// 成功的牌組
        /// </summary>
        /// <returns></returns>
        public BrandPlayer SuccessPlayer
        {
            get
            {
                PlayerSort p = new PlayerSort(ans_player);
                ans_player = p.getPlayer();
                return ans_player;
            }
        }
        private void brand_2()
        {
            // 對子的牌組
            for (int i = 0; i < x.getCount() - 1; i++)
                if (x.getBrand(i).getClass() == x.getBrand(i + 1).getClass() && x.getBrand(i).getNumber() == x.getBrand(i + 1).getNumber())
                {
                    if (b.getCount() == 0)
                    {
                        b.add(x.getBrand(i));
                        b.add(x.getBrand(i));
                    }
                    else if (x.getBrand(i + 1).getClass() != b.getBrand(b.getCount() - 1).getClass() ||
                        x.getBrand(i + 1).getNumber() != b.getBrand(b.getCount() - 1).getNumber())
                    {
                        b.add(x.getBrand(i));
                        b.add(x.getBrand(i));
                    }
                }
        }
        private void bradn_3()
        {
            for (int i = 0; i < x.getCount() - 2; i++)
                for (int j = i + 1; j < x.getCount() - 1; j++)
                    for (int k = j + 1; k < x.getCount(); k++)
                        if (x.getBrand(k).getClass() != Mahjong.Properties.Settings.Default.Wordtiles && //順子的牌組
                            x.getBrand(i).getClass() == x.getBrand(j).getClass() &&
                            x.getBrand(i).getNumber() == x.getBrand(j).getNumber() - 1 &&
                            x.getBrand(j).getClass() == x.getBrand(k).getClass() &&
                            x.getBrand(j).getNumber() == x.getBrand(k).getNumber() - 1)
                        {
                            if (a.getCount() == 0)
                            {
                                a.add(x.getBrand(i));
                                a.add(x.getBrand(j));
                                a.add(x.getBrand(k));
                            }
                            else if (x.getBrand(i) != a.getBrand(a.getCount() - 3) &&
                                x.getBrand(j) != a.getBrand(a.getCount() - 2) &&
                                x.getBrand(k) != a.getBrand(a.getCount() - 1))
                            {
                                a.add(x.getBrand(i));
                                a.add(x.getBrand(j));
                                a.add(x.getBrand(k));
                            }
                        } // 順子的牌組結束
                        else if (x.getBrand(i).getClass() == x.getBrand(j).getClass() &&
                            x.getBrand(i).getNumber() == x.getBrand(j).getNumber() &&
                            x.getBrand(j).getClass() == x.getBrand(k).getClass() &&
                            x.getBrand(j).getNumber() == x.getBrand(k).getNumber())
                        { // 碰的牌組
                            if (a.getCount() == 0)
                            {
                                a.add(x.getBrand(i));
                                a.add(x.getBrand(j));
                                a.add(x.getBrand(k));
                            }
                            else if (x.getBrand(i) != a.getBrand(a.getCount() - 3) &&
                                x.getBrand(j) != a.getBrand(a.getCount() - 2) &&
                                x.getBrand(k) != a.getBrand(a.getCount() - 1))
                            {
                                a.add(x.getBrand(i));
                                a.add(x.getBrand(j));
                                a.add(x.getBrand(k));
                            }
                        } // 碰的牌組
        }
        private void bradn_4()
        {
            if (x.getCount() > 31)
                throw new ErrorBrandPlayerCountException();
            if (x.getCount() >= 17)
            {
                for (int i = 0; i < x.getCount() - 3; i++)
                    if (x.getBrand(i).getClass() == x.getBrand(i + 1).getClass() &&
                                 x.getBrand(i).getNumber() == x.getBrand(i + 1).getNumber() &&
                                 x.getBrand(i).getClass() == x.getBrand(i + 2).getClass() &&
                                 x.getBrand(i).getNumber() == x.getBrand(i + 2).getNumber() &&
                                 x.getBrand(i).getClass() == x.getBrand(i + 3).getClass() &&
                                 x.getBrand(i).getNumber() == x.getBrand(i + 3).getNumber() &&
                                 x.getBrand(i).Team > 1)
                    { // 碰的牌組
                        if (a.getCount() == 0)
                        {
                            a.add(x.getBrand(i));
                            a.add(x.getBrand(i + 1));
                            a.add(x.getBrand(i + 2));
                            x.remove(x.getBrand(i));
                            //  a.add(x.getBrand(i + 3));
                        }
                        else if (//x.getBrand(i) != a.getBrand(a.getCount() - 4) &&
                            x.getBrand(i + 1) != a.getBrand(a.getCount() - 3) &&
                            x.getBrand(i + 2) != a.getBrand(a.getCount() - 2) &&
                            x.getBrand(i + 3) != a.getBrand(a.getCount() - 1))
                        {
                            a.add(x.getBrand(i));
                            a.add(x.getBrand(i + 1));
                            a.add(x.getBrand(i + 2));
                            x.remove(x.getBrand(i));
                            // a.add(x.getBrand(i + 3));
                        }
                    } // 碰的牌組
            }
        }
        /// <summary>
        /// 胡牌 成立
        /// </summary>
        /// <returns>是/否</returns>
        public bool Win()
        {
            if (brand != null)
            {
                x.add(brand);
                PlayerSort d = new PlayerSort(x);
                x = d.getPlayer();
            }
            brand_2();
            bradn_4();
            bradn_3();
            
            // 組合測試
            // a 三支
            // b 兩隻
            // c 組合
            int count = 0;
            for (int i = 0; i < a.getCount(); i += 3)
            {
                for (int j = i + 3; j < a.getCount(); j += 3)
                {
                    for (int k = j + 3; k < a.getCount(); k += 3)
                    {
                        for (int l = k + 3; l < a.getCount(); l += 3)
                        {
                            for (int m = l + 3; m < a.getCount(); m += 3)
                            {
                                for (int n = 0; n < b.getCount(); n += 2)
                                {
                                    c.clear();
                                    c.add(a.getBrand(i));
                                    c.add(a.getBrand(i + 1));
                                    c.add(a.getBrand(i + 2));
                                    c.add(a.getBrand(j));
                                    c.add(a.getBrand(j + 1));
                                    c.add(a.getBrand(j + 2));
                                    c.add(a.getBrand(k));
                                    c.add(a.getBrand(k + 1));
                                    c.add(a.getBrand(k + 2));
                                    c.add(a.getBrand(l));
                                    c.add(a.getBrand(l + 1));
                                    c.add(a.getBrand(l + 2));
                                    c.add(a.getBrand(m));
                                    c.add(a.getBrand(m + 1));
                                    c.add(a.getBrand(m + 2));
                                    c.add(b.getBrand(n));
                                    c.add(b.getBrand(n + 1));

                                    PlayerSort d = new PlayerSort(c);
                                    c = d.getPlayer();
                                    //牌的比對
                                    //完全成立代表胡牌
                                    for (int o = 0; o < x.getCount(); o++)
                                    {
                                        if (c.getBrand(o).getClass() == x.getBrand(o).getClass() &&
                                            c.getBrand(o).getNumber() == x.getBrand(o).getNumber())
                                        {
                                            count = o;
                                            continue;
                                        }
                                        else
                                            break;
                                    }
                                    if (count == x.getCount() - 1)
                                    {
                                        if (brand != null)
                                            x.remove(brand);
                                        return true; // 成立
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (brand != null)
                x.remove(brand);
            return false;
        }

    }
}
