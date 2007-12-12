using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    public class BaseBrand : Brand
    {   
        /// <summary>
        /// 牌
        /// </summary>
        /// <param name="number">牌面大小</param> 
        protected BaseBrand(int number)
        {
            this.Number = number;
            See = false;
        }

        /// <summary>
        /// 牌
        /// </summary>
        /// <param name="number">牌面大小</param>
        /// <param name="image">圖片</param>
        protected BaseBrand(int number, Image image)
        {
            this.Number = number;
            this.photo = image;
            See = false;
        }

        private int Number;
        /// <summary>
        /// 牌的值的大小
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
            return "";
        }

        private bool See;
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

        private int source = 1;
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
    }
}
