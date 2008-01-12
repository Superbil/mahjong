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
        location winer;

        public Location()
        {
            position = location.East;
            round = location.East;
            winer = location.East;
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
        /// ���a
        /// </summary>
        public location Winer
        {
            get
            {
                return winer;
            }
        }
        /// <summary>
        /// �U�@��
        /// </summary>
        public void next_Winer()
        {
            add(winer);
        }
        /// <summary>
        /// �U�@�Ӥ��
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
        /// ��m�ഫ���r��
        /// </summary>
        /// <param name="lo">��m</param>
        /// <returns>�r��</returns>
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
