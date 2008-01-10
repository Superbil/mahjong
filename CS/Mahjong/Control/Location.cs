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
        location countWind;
        location round;

        public Location()
        {
            countWind = location.East;
            round = location.East;
        }
        /// <summary>
        /// ��
        /// </summary>
        public location Wind
        {
            get
            {
                return countWind;
            }
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
        public void next()
        {
            if (round == location.West)
            {
                add(round);
                add(countWind);
            }
            else
                add(round);
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
