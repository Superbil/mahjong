using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// 索子牌
    /// </summary>
    public class RopeBrand : BaseBrand
    {
        /// <summary>
        /// 索牌
        /// </summary>
        /// <param name="number">牌面大小</param>
        public RopeBrand(int number) : base(number)
        {
        }
        /// <summary>
        /// 索牌
        /// </summary>
        /// <param name="number">牌面大小</param>
        /// <param name="image">圖片位置</param>
        public RopeBrand(int number, Image image) : base(number,image)
        {
        }
        /// <summary>
        /// 牌的類別
        /// </summary>   
        public new string getClass()
        {
            return Mahjong.Properties.Settings.Default.Bamboos;
        }
    }
}
