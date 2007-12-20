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
        /// <summary>
        /// �r�P
        /// </summary>
        /// <param name="number">�P���j�p</param>
        public WordBrand(int number)
        {
            this.Number = number;
            See = false;
        }
        /// <summary>
        /// �r�P
        /// </summary>
        /// <param name="number">�P���j�p</param>
        /// <param name="image">�Ϥ�</param>
        public WordBrand(int number,Image image)
        {
            this.Number = number;
            See = false;
            photo = image;
        }
        /// <summary>
        /// �r�P����
        /// </summary>
        /// <returns></returns>
        public int getNumber()
        {
            return Number;
        }
        /// <summary>
        /// �P�����O
        /// </summary>
        /// <returns></returns>
        public string getClass()
        {
            return Mahjong.Properties.Settings.Default.Wordtiles;
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
