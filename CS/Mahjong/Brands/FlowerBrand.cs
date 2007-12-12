using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// ��P
    /// </summary>
    public class FlowerBrand : BaseBrand
    {
        /// <summary>
        /// ��P
        /// </summary>
        /// <param name="number">�P���j�p</param> 
        public FlowerBrand(int number) : base(number)
        {            
        }
        /// <summary>
        /// ��P
        /// </summary>
        /// <param name="number">�P���j�p</param>
        /// <param name="image">�Ϥ�</param>
        public FlowerBrand(int number,Image image) : base(number,image)
        {            
        }
        /// <summary>
        /// �P�����O
        /// </summary>
        public new string getClass()
        {
            return Mahjong.Properties.Settings.Default.Flower;
        }
    }
}
