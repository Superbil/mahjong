using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// �r�P
    /// </summary>
    public class WordBrand : BaseBrand
    {
        /// <summary>
        /// �r�P
        /// </summary>
        /// <param name="number">�P���j�p</param>
        public WordBrand(int number) : base(number)
        {
        }
        /// <summary>
        /// �r�P
        /// </summary>
        /// <param name="number">�P���j�p</param>
        /// <param name="image">�Ϥ�</param>
        public WordBrand(int number,Image image) : base(number,image)
        {
        }
        /// <summary>
        /// �P�����O
        /// </summary>
        /// <returns></returns>
        public new string getClass()
        {
            return Mahjong.Properties.Settings.Default.Wordtiles;
        }
    }
}
