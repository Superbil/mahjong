using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// 花牌
    /// </summary>
    public class FlowerBrand : Brand
    {
        private int Number;
        private bool See;
        private Image photo;
        /// <summary>
        /// 花牌
        /// </summary>
        /// <param name="number">牌面大小</param> 
        public FlowerBrand(int number)
        {
            this.Number = number;
            See = false;
        }
        /// <summary>
        /// 花牌
        /// </summary>
        /// <param name="number">牌面大小</param>
        /// <param name="image">圖片</param>
        public FlowerBrand(int number,Image image)
        {
            this.Number = number;
            this.photo = image;
            See = false;
        }
        /// <summary>
        /// 花牌的值的大小
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
            return Mahjong.Properties.Settings.Default.Flower;
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

        /// <summary>
        /// 牌的分數
        /// </summary>
        private int source=0;
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

    }
}
