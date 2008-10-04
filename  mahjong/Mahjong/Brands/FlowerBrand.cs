using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace Mahjong.Brands
{
    [Serializable]
    /// <summary>
    /// ��P
    /// </summary>
    public class FlowerBrand : Brand
    {
        private int Number;
        private bool See;
        private Image photo;
        /// <summary>
        /// ��P
        /// </summary>
        /// <param name="number">�P���j�p</param> 
        public FlowerBrand(int number)
        {
            this.Number = number;
            See = false;
        }
        /// <summary>
        /// ��P
        /// </summary>
        /// <param name="number">�P���j�p</param>
        /// <param name="image">�Ϥ�</param>
        public FlowerBrand(int number,Image image)
        {
            this.Number = number;
            this.photo = image;
            See = false;
        }
        /// <summary>
        /// ��P���Ȫ��j�p
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
            return Mahjong.Properties.Settings.Default.Flower;
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

        private UnmanagedMemoryStream wave;
        /// <summary>
        /// �P���n����m
        /// </summary>
        public UnmanagedMemoryStream sound
        {
            get
            {
                return wave;
            }
            set
            {
                wave = value;
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
