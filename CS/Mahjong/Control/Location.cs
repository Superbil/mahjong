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
        location winer;

        public Location()
        {
            position = location.East;
            round = location.East;
            winer = location.East;
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
        /// 莊家
        /// </summary>
        public location Winer
        {
            get
            {
                return winer;
            }
        }
        /// <summary>
        /// 下一莊
        /// </summary>
        public void next_Winer()
        {
            add(winer);
        }
        /// <summary>
        /// 下一個方位
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
        /// <summary>
        /// 位置轉換成字串
        /// </summary>
        /// <param name="lo">位置</param>
        /// <returns>字串</returns>
        string location_to_string(location lo)
        {
            if (lo == location.East)
                return Mahjong.Properties.Settings.Default.East;
            else if (lo == location.South)
                return Mahjong.Properties.Settings.Default.South;
            else if (lo == location.West)
                return Mahjong.Properties.Settings.Default.West;
            else if (lo == location.North)
                return Mahjong.Properties.Settings.Default.Nouth;
            else
                return "";
        }
        public override string ToString()
        {
            string temp="";
            temp += location_to_string(round);
            temp += Mahjong.Properties.Settings.Default.Round;
            temp += location_to_string(position);
            temp += Mahjong.Properties.Settings.Default.Position;
            return temp;
        }
    }
}
