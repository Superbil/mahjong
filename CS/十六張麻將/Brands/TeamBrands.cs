using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// 牌堆,三張或是四張牌
    /// </summary>
    class TeamBrands : Brand
    {
        private int Number;
        private bool See;
        private Brand[] brands;
        private static string classname="Team Brands";
        public TeamBrands(Brand brand1,Brand brand2,Brand brand3)
        {
            this.Number = 3;
            brands = new Brand[] { brand1, brand2, brand3};
            See = true;
        }
        public TeamBrands(Brand brand1, Brand brand2, Brand brand3, Brand brand4)
        {
            this.Number = 4;
            brands = new Brand[] {brand1,brand2,brand3,brand4};
            See = true;
        }
        /// <summary>
        /// 牌組的值的大小
        /// </summary>   
        public int getNumber()
        {
            return Number;
        }
        /// <summary>
        /// 牌組的類別
        /// </summary>   
        public string getClass()
        {
            return classname;
        }
        public Brand getBrand(int IndexNumber)
        {
            return brands[IndexNumber];
        }
        public bool IsCanSee
        {
            get
            {
                return See;
            }
            set
            {
                See = value;
            }
        }
        private Image photo;
        /// <summary>
        /// 牌的圖片位置
        /// </summary>
        public Image image
        {
            get
            {
                return photo;
            }
            set
            {
                photo = value;
            }
        }

        internal static bool Equals(Brand brand)
        {
            //TeamBrands tbrand = new TeamBrands();
            if (brand.getClass()==classname)
                return true;
            else
                return false;
        }
    }
}
