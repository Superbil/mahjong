using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// �P������
    /// </summary>
    public interface Brand
    {
        /// <summary>
        /// �P�����O
        /// </summary>
        string getClass();
        /// <summary>
        /// �P���Ȫ��j�p
        /// </summary>
        int getNumber();
        /// <summary>
        /// �P�O�_�i��
        /// </summary> 
        bool IsCanSee
        {
            get;
            set;
        }
        /// <summary>
        /// �P���Ϥ���m
        /// </summary>  
        Image image
        {
            get;
            set;
        }
        /// <summary>
        /// �P���էO
        /// </summary>
        int Team
        {
            get;
            set;
        }
        /// <summary>
        /// �P������
        /// </summary>
        int Source
        {
            get;
            set;
        }
    }
}
