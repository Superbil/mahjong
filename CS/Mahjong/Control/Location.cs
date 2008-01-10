using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Control
{
    public enum location
    {
        /// <summary>
        /// �_
        /// </summary>
        North = 0 ,
        /// <summary>
        /// �F
        /// </summary>
        East = 1,
        /// <summary>
        /// �n
        /// </summary>
        South = 2,
        /// <summary>
        /// ��
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
        /// ��
        /// </summary>
        public location Round
        {
            get
            {
                return round;
            }
        }
        /// <summary>
        /// ��
        /// </summary>
        public location Position
        {
            get
            {
                return position;
            }
        }
        /// <summary>
        /// �U�@��
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
