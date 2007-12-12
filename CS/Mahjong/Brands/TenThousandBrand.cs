using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// �U�r�P
    /// </summary>
    public class TenThousandBrand : BaseBrand
    {
        /// <summary>
        /// �U�P
        /// </summary>
        /// <param name="number">�P���j�p</param>
        public TenThousandBrand(int number) : base(number)
        {
        }
        /// <summary>
        /// �U�P
        /// </summary>
        /// <param name="number">�P���j�p</param>
        /// <param name="image">�Ϥ�</param>
        public TenThousandBrand(int number,Image image) : base(number,image)
        {
        }
        /// <summary>
        /// �P�����O
        /// </summary>   
        public new string getClass()
        {
            return Mahjong.Properties.Settings.Default.Characters;
        }
    }
}
