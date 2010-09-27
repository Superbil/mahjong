using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Control
{
    /// <summary>
    /// ��� �_ �F �n ��
    /// </summary>
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
        West = 3,
        /// <summary>
        /// �ୱ
        /// </summary>
        Table = 4
    }
    [Serializable]
    public class Location
    {
        location position;
        location round;
        location winer;
        Random r = new Random();
        /// <summary>
        /// ���غc�l�w�] �F���F �}�l
        /// </summary>
        public Location()
        {
            round = location.East;
            winer = location.East;
            setPosition();
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
        /// �]�w�}�P��m
        /// </summary>
        public void setPosition()
        {
            switch (r.Next(4))
            {
                case 0:
                    position = location.North;
                    break;
                case 1:
                    position = location.East;
                    break;
                case 2:
                    position = location.South;
                    break;
                case 3:
                    position = location.West;
                    break;
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
        /// ��(���a)
        /// </summary>
        public location Winer
        {
            get
            {
                return winer;
            }
        }
        /// <summary>
        /// �U�@�� E->S->W->N
        /// </summary>
        public void next()
        {
            if (winer == location.North)
            {
                winer = add(winer);
                round = add(round);
            }
            else
                winer = add(winer);
        }
        /// <summary>
        /// �U�@��
        /// </summary>
        public void nextPosition()
        {
            position = add(position);
        }
        location add(location lo)
        {
            if (lo == location.East)
                return location.South;
            else if (lo == location.South)
                return location.West;
            else if (lo == location.West)
                return location.North;
            else if (lo == location.North)
                return location.East;
            else
                return location.East;
        }
        /// <summary>
        /// �ഫ����r��
        /// </summary>
        /// <param name="lo">���</param>
        /// <returns>�r��</returns>
        internal string location_to_string(location lo)
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
        /// <summary>
        /// �����ഫ���r��Ǧ^
        /// </summary>
        /// <returns>�r��</returns>
        public override string ToString()
        {
            string temp="";
            temp += location_to_string(round);
            temp += Mahjong.Properties.Settings.Default.Round;
            temp += location_to_string(winer);
            temp += " ";
            temp += location_to_string(position);
            temp += Mahjong.Properties.Settings.Default.Position;
            return temp;
        }
        /// <summary>
        /// �Ʀr�ഫ�����
        /// </summary>
        /// <param name="state">�Ʀr</param>
        /// <returns>���</returns>
        public static location getlocation(uint state)
        {
            switch (state)
            {
                case 0:
                    return location.North;
                case 1:
                    return location.East;
                case 2:
                    return location.South;
                case 3:
                    return location.West;
            }
            return location.East;
        }
    }
}
