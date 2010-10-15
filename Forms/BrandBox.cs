using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Mahjong.Brands;

namespace Mahjong.Forms
{
    /// <summary>
    /// ���Ѧs��P��PictureBox
    /// </summary>
    public class BrandBox : PictureBox
    {
        Brand savebrand;
        public BrandBox(Brand val)
        {
            savebrand = val;
        }
        /// <summary>
        /// �P
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
