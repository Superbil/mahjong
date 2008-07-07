using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Control
{
    /// <summary>
    /// 檢查使用者按下那個功能
    /// </summary>
    [Serializable]
    public class CheckUser
    {
        /// <summary>
        /// 吃
        /// </summary>
        public bool Chow;
        /// <summary>
        /// 碰
        /// </summary>
        public bool Pong;
        /// <summary>
        /// 槓
        /// </summary>
        public bool Kong;
        /// <summary>
        /// 暗槓
        /// </summary>
        public bool DarkKong;
        /// <summary>
        /// 胡
        /// </summary>
        public bool Win;
        /// <summary>
        /// 過水
        /// </summary>
        public bool Pass;

        public CheckUser(bool chow,bool pong,bool kong,bool darkkong,bool win,bool pass)
        {
            Chow = chow;
            Pong = pong;
            Kong = kong;
            DarkKong = darkkong;
            Win = win;
            Pass = pass;
        }
    }
}
