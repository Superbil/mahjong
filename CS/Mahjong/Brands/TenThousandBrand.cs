using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// �U�r�P
    /// </summary>
    public class TenThousandBrand : Brand
    {
        private int Number;
        private bool See;
        public TenThousandBrand(int number)
        {
            this.Number = number;
            See = true;
        }
        public TenThousandBrand(int number,Image image)
        {
            this.Number = number;
            See = true;
            photo = image;
        }
        /// <summary>
        /// �U�r�P���Ȫ��j�p
        /// </summary>   
        public int getNumber()
        {
            return Number;
        }
        /// <summary>
        /// �U�r�P�����O
        /// </summary>   
        public string getClass()
        {
            return Mahjong.Properties.Settings.Default.Characters;
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
        /// �P���Ϥ���m
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
        private int teamNumber;
        /// <summary>
        /// �P���էO
        /// </summary>
        public int team
        {
            get
            {
                return teamNumber;
            }
            set
            {
                teamNumber = value;
            }
        }
    }
}
