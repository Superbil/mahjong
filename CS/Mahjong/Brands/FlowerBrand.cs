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
         
        public FlowerBrand(int number)
        {
            this.Number = number;
            See = true;
        }
        public FlowerBrand(int number,Image image)
        {
            this.Number = number;
            this.photo = image;
            See = true;
        }
        /// <summary>
        /// 花牌的值的大小
        /// </summary>   
        public int getNumber()
        {
            return Number;
        }
        /// <summary>
        /// 花牌的類別
        /// </summary>
        public string getClass()
        {
            return "Flower Brand";
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
