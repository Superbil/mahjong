using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// 萬字牌
    /// </summary>
    public class TenThousandBrand : Brand
    {
        private int Number;
        private bool See;
        public TenThousandBrand(int number)
        {
            this.Number = number;
            See = true;
        }
        public TenThousandBrand(int number,Image image)
        {
            this.Number = number;
            See = true;
            photo = image;
        }
        /// <summary>
        /// 萬字牌的值的大小
        /// </summary>   
        public int getNumber()
        {
            return Number;
        }
        /// <summary>
        /// 萬字牌的類別
        /// </summary>   
        public string getClass()
        {
            return Mahjong.Properties.Settings.Default.Characters;
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
        public int team
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
