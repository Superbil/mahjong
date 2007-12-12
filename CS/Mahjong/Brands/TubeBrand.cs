using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// ���l�P
    /// </summary>
    public class TubeBrand : BaseBrand
    {
        /// <summary>
        /// ���P
        /// </summary>
        /// <param name="number">�P���j�p</param>
        public TubeBrand(int number) : base(number)
        {
        }
        /// <summary>
        /// ���P
        /// </summary>
        /// <param name="number">�P���j�p</param>
        /// <param name="image">�Ϥ�</param>
        public TubeBrand(int number,Image image) : base(number,image)
        {
        }
        /// <summary>
        /// �P�����O
        /// </summary>   
        public new string getClass()
        {
            return Mahjong.Properties.Settings.Default.Dots;
        }
    }
}
