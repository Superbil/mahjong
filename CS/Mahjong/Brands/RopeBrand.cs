using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// ���l�P
    /// </summary>
    public class RopeBrand : Brand
    {
        private int Number;
        private bool See;
        /// <summary>
        /// ���P
        /// </summary>
        /// <param name="number">�P���j�p</param>
        public RopeBrand(int number)
        {
            this.Number = number;
            See = false;
        }
        /// <summary>
        /// ���P
        /// </summary>
        /// <param name="number">�P���j�p</param>
        /// <param name="image">�Ϥ���m</param>
        public RopeBrand(int number, Image image)
        {
            this.Number = number;
            See = false;
            photo = image;
        }
        /// <summary>
        /// ���P���Ȫ��j�p
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
            return Mahjong.Properties.Settings.Default.Bamboos;
        }
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

        /// <summary>
        /// �P������
        /// </summary>
        private int source = 0;
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
