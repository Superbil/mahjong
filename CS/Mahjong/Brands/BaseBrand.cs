using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    public class BaseBrand : Brand
    {   
        /// <summary>
        /// �P
        /// </summary>
        /// <param name="number">�P���j�p</param> 
        protected BaseBrand(int number)
        {
            this.Number = number;
            See = false;
        }

        /// <summary>
        /// �P
        /// </summary>
        /// <param name="number">�P���j�p</param>
        /// <param name="image">�Ϥ�</param>
        protected BaseBrand(int number, Image image)
        {
            this.Number = number;
            this.photo = image;
            See = false;
        }

        private int Number;
        /// <summary>
        /// �P���Ȫ��j�p
        /// </summary>   
        public int getNumber()
        {
            return Number;
        }

        /// <summary>
        /// �P�����O
        /// </summary>
        public string getClass()
        {
            return "";
        }

        private bool See;
        /// <summary>
        /// �O�_�i��
        /// </summary>
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
        public int Team
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

        private int source = 1;
        /// <summary>
        /// �P������
        /// </summary>
        public int Source
        {
            get
            {
                return source;
            }
            set
            {
                source = value;
            }
        }        
    }
}
