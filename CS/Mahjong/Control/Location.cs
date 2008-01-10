using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Control
{
    public enum location
    {
        /// <summary>
        /// 北
        /// </summary>
        North = 0 ,
        /// <summary>
        /// 東
        /// </summary>
        East = 1,
        /// <summary>
        /// 南
        /// </summary>
        South = 2,
        /// <summary>
        /// 西
        /// </summary>
        West = 3
    }
    public class Location
    {
        location position;
        location round;

        public Location()
        {
            position = location.East;
            round = location.East;
        }
        /// <summary>
        /// 局
        /// </summary>
        public location Round
        {
            get
            {
                return round;
            }
        }
        /// <summary>
        /// 位
        /// </summary>
        public location Position
        {
            get
            {
                return position;
            }
        }
        /// <summary>
        /// 下一莊
        /// </summary>
        public void next()
        {
            if (position == location.West)
            {
                add(round);
                add(position);
            }
            else
                add(position);
        }
        void add(location lo)
        {
            if (lo == location.East)
                lo = location.North;
            else if (lo == location.North)
                lo = location.South;
            else if (lo == location.South)
                lo = location.West;
            else
                lo = location.East;
        }
    }
}
