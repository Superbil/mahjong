using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Players
{
    /// <summary>
    /// ���о�
    /// </summary>
    public interface Iterator
    {
        /// <summary>
        /// �O�_�٦��U�@��
        /// </summary>
        /// <returns>���L��</returns>
        bool hasNext();
        /// <summary>
        /// ���o�U�@��
        /// </summary>
        /// <returns>����</returns>
        Object next();
    }
}
