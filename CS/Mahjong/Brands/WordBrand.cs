using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// �r�P
    /// </summary>
    public class WordBrand : Brand
    {
        private int Number;
        private bool See;
        public WordBrand(int number)
        {
            this.Number = number;
            See = true;
        }
        public WordBrand(int number,Image image)
        {
            this.Number = number;
            See = true;
            photo = image;
        }
        public int getNumber()
        {
            return Number;
        }
        public string getClass()
        {
            return Mahjong.Properties.Settings.Default.Wordtiles;
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
