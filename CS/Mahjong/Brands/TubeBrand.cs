using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// 筒子牌
    /// </summary>
    public class TubeBrand : BaseBrand
    {
        /// <summary>
        /// 筒牌
        /// </summary>
        /// <param name="number">牌面大小</param>
        public TubeBrand(int number) : base(number)
        {
        }
        /// <summary>
        /// 筒牌
        /// </summary>
        /// <param name="number">牌面大小</param>
        /// <param name="image">圖片</param>
        public TubeBrand(int number,Image image) : base(number,image)
        {
        }
        /// <summary>
        /// 牌的類別
        /// </summary>   
        public new string getClass()
        {
            return Mahjong.Properties.Settings.Default.Dots;
        }
    }
}
