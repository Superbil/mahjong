using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Mahjong.Brands;

namespace Mahjong.Forms
{
    /// <summary>
    /// 提供存放牌的PictureBox
    /// </summary>
    public class BrandBox : PictureBox
    {
        Brand savebrand;
        public BrandBox(Brand val)
        {
            savebrand = val;
        }
        /// <summary>
        /// 牌
        /// </summary>
        public Brand brand
        {
            set
            {
                savebrand = value;
            }
            get
            {
                return savebrand;
            }
        }
    }
}
