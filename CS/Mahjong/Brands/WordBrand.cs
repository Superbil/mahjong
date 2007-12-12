using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// 字牌
    /// </summary>
    public class WordBrand : BaseBrand
    {
        /// <summary>
        /// 字牌
        /// </summary>
        /// <param name="number">牌面大小</param>
        public WordBrand(int number) : base(number)
        {
        }
        /// <summary>
        /// 字牌
        /// </summary>
        /// <param name="number">牌面大小</param>
        /// <param name="image">圖片</param>
        public WordBrand(int number,Image image) : base(number,image)
        {
        }
        /// <summary>
        /// 牌的類別
        /// </summary>
        /// <returns></returns>
        public new string getClass()
        {
            return Mahjong.Properties.Settings.Default.Wordtiles;
        }
    }
}
