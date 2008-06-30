using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Control
{
    [Serializable]
    /// <summary>
    /// 桌面對應到的真實玩家位置
    /// </summary>
    public class Place
    {
        /// <summary>
        /// 坐在上面的玩家
        /// </summary>
        public location Up;
        /// <summary>
        /// 坐在下面的玩家
        /// </summary>
        public location Down;
        /// <summary>
        /// 坐在右邊的玩家
        /// </summary>
        public location Right;
        /// <summary>
        /// 坐在左邊的玩家
        /// </summary>
        public location Left;
        /// <summary>
        /// 傳回真實的位置
        /// </summary>
        /// <param name="lo">位置</param>
        /// <returns>正整數</returns>
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
        /// 傳回上面真實的位置
        /// </summary>
        public uint getRealPlace_Up
        {
            get
            {
                return (uint)Up;
            }
        }
        /// <summary>
        /// 傳回右邊真實的位置
        /// </summary>
        public uint getRealPlace_Right
        {
            get
            {
                return (uint)Right;
            }
        }
        /// <summary>
        /// 傳回下面真實的位置
        /// </summary>
        public uint getRealPlace_Down
        {
            get
            {
                return (uint)Down;
            }
        }
        /// <summary>
        /// 傳回左邊真實的位置
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
