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
    class BrandBox : PictureBox
    {
        Brand savebrand;
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
