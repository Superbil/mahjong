using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Control
{
    [Serializable]
    /// <summary>
    /// �ୱ�����쪺�u�ꪱ�a��m
    /// </summary>
    public class Place
    {
        /// <summary>
        /// ���b�W�������a
        /// </summary>
        public location Up;
        /// <summary>
        /// ���b�U�������a
        /// </summary>
        public location Down;
        /// <summary>
        /// ���b�k�䪺���a
        /// </summary>
        public location Right;
        /// <summary>
        /// ���b���䪺���a
        /// </summary>
        public location Left;
        /// <summary>
        /// �Ǧ^�u�ꪺ��m
        /// </summary>
        /// <param name="lo">��m</param>
        /// <returns>�����</returns>
        public location getRealPlace(location lo)
        {
            if (lo == location.North)
                return Up;
            else if (lo == location.South)
                return Down;
            else if (lo == location.East)
                return Right;
            else if (lo == location.West)
                return Left;
            
            return location.Table;
        }
        /// <summary>
        /// �Ǧ^�W���u�ꪺ��m
        /// </summary>
        public uint getRealPlace_Up
        {
            get
            {
                return (uint)Up;
            }
        }
        /// <summary>
        /// �Ǧ^�k��u�ꪺ��m
        /// </summary>
        public uint getRealPlace_Right
        {
            get
            {
                return (uint)Right;
            }
        }
        /// <summary>
        /// �Ǧ^�U���u�ꪺ��m
        /// </summary>
        public uint getRealPlace_Down
        {
            get
            {
                return (uint)Down;
            }
        }
        /// <summary>
        /// �Ǧ^����u�ꪺ��m
        /// </summary>
        public uint getRealPlace_Left
        {
            get
            {
                return (uint)Left;
            }
        }
    }
}
