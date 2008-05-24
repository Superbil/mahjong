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
        /// <summary>
        /// �U�P
        /// </summary>
        /// <param name="number">�P���j�p</param>
        public TenThousandBrand(int number)
        {
            this.Number = number;
            See = false;
        }
        /// <summary>
        /// �U�P
        /// </summary>
        /// <param name="number">�P���j�p</param>
        /// <param name="image">�Ϥ�</param>
        public TenThousandBrand(int number,Image image)
        {
            this.Number = number;
            See = false;
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
        /// �P�����O
        /// </summary>   
        public string getClass()
        {
            return Mahjong.Properties.Settings.Default.Characters;
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

        private int source = 0;
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

        Mahjong.Control.location from;
        /// <summary>
        /// ���Ӥ�쥴�F�o�i�P
        /// </summary>
        public Mahjong.Control.location WhoPush
        {
            get
            {
                return from;
            }
            set
            {
                from = value;
            }
        }
    }
}
