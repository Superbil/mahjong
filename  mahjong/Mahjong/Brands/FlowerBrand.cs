using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace Mahjong.Brands
{
    [Serializable]
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

        private UnmanagedMemoryStream wave;
        /// <summary>
        /// 牌的聲音位置
        /// </summary>
        public UnmanagedMemoryStream sound
        {
            get
            {
                return wave;
            }
            set
            {
                wave = value;
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
