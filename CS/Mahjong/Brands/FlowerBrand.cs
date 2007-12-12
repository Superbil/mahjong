using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// 花牌
    /// </summary>
    public class FlowerBrand : BaseBrand
    {
        /// <summary>
        /// 花牌
        /// </summary>
        /// <param name="number">牌面大小</param> 
        public FlowerBrand(int number) : base(number)
        {            
        }
        /// <summary>
        /// 花牌
        /// </summary>
        /// <param name="number">牌面大小</param>
        /// <param name="image">圖片</param>
        public FlowerBrand(int number,Image image) : base(number,image)
        {            
        }
        /// <summary>
        /// 牌的類別
        /// </summary>
        public new string getClass()
        {
            return Mahjong.Properties.Settings.Default.Flower;
        }
    }
}
