using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Control
{
    /// <summary>
    /// �ˬd�ϥΪ̫��U���ӥ\��
    /// </summary>
    [Serializable]
    public class CheckUser
    {
        /// <summary>
        /// �Y
        /// </summary>
        public bool Chow;
        /// <summary>
        /// �I
        /// </summary>
        public bool Pong;
        /// <summary>
        /// �b
        /// </summary>
        public bool Kong;
        /// <summary>
        /// �t�b
        /// </summary>
        public bool DarkKong;
        /// <summary>
        /// �J
        /// </summary>
        public bool Win;
        /// <summary>
        /// �L��
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
