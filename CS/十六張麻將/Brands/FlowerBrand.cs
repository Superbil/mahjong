using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// ��P
    /// </summary>
    public class FlowerBrand : Brand
    {
        private int Number;
        private bool See;
        private Image photo;
         
        public FlowerBrand(int number)
        {
            this.Number = number;
            See = true;
        }
        public FlowerBrand(int number,Image image)
        {
            this.Number = number;
            this.photo = image;
            See = true;
        }
        /// <summary>
        /// ��P���Ȫ��j�p
        /// </summary>   
        public int getNumber()
        {
            return Number;
        }
        /// <summary>
        /// ��P�����O
        /// </summary>
        public string getClass()
        {
            return "Flower Brand";
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
