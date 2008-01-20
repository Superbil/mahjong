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
        /// <summary>
        /// 萬牌
        /// </summary>
        /// <param name="number">牌面大小</param>
        public TenThousandBrand(int number)
        {
            this.Number = number;
            See = false;
        }
        /// <summary>
        /// 萬牌
        /// </summary>
        /// <param name="number">牌面大小</param>
        /// <param name="image">圖片</param>
        public TenThousandBrand(int number,Image image)
        {
            this.Number = number;
            See = false;
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
        /// 牌的類別
        /// </summary>   
        public string getClass()
        {
            return Mahjong.Properties.Settings.Default.Characters;
        }
        /// <summary>
        /// 是否可見
        /// </summary>
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

        private int source = 0;
        /// <summary>
        /// 牌的分數
        /// </summary>
        public int Source
        {
            get
            {
                return source;
            }
            set
            {
                source = value;
            }
        }

        Mahjong.Control.location from;
        /// <summary>
        /// 那個方位打了這張牌
        /// </summary>
        public Mahjong.Control.location WhoPush
        {
            get
            {
                return from;
            }
            set
            {
                from = value;
            }
        }
    }
}
