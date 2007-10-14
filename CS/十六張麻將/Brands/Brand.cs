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
        /// <returns>���O</returns>
        string getClass();
        /// <summary>
        /// �P���Ȫ��j�p
        /// </summary>
        /// <returns>�j�p</returns>       
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
        int team
        {
            get;
            set;
        }
    }
}
