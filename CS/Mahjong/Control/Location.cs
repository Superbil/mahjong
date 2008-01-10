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
        location countWind;
        location round;

        public Location()
        {
            countWind = location.East;
            round = location.East;
        }
        /// <summary>
        /// 風
        /// </summary>
        public location Wind
        {
            get
            {
                return countWind;
            }
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
