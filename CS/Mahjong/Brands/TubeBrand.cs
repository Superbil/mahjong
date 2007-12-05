using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// 筒子牌
    /// </summary>
    public class TubeBrand : Brand
    {
        private int Number;
        private bool See;
        public TubeBrand(int number)
        {
            this.Number = number;
            See = true;
        }
        public TubeBrand(int number,Image image)
        {
            this.Number = number;
            See = true;
            photo = image;
        }
        /// <summary>
        /// 筒牌的值的大小
        /// </summary>   
        public int getNumber()
        {
            return Number;
        }
        /// <summary>
        /// 筒牌的類別
        /// </summary>   
        public string getClass()
        {
            return Mahjong.Properties.Settings.Default.Dots;
        }
        public bool IsCanSee
        {
            get
            {
                return See;
            }
            set
            {
                See = value;
            }
        }
        private Image photo;
        /// <summary>
        /// 牌的圖片位置
        /// </summary>
        public Image image
        {
            get
            {
                return photo;
            }
            set
            {
                photo = value;
            }
        }
        private int teamNumber;
        /// <summary>
        /// 牌的組別
        /// </summary>
        public int Team
        {
            get
            {
                return teamNumber;
            }
            set
            {
                teamNumber = value;
            }
        }
    }
}
