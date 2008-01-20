using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Control
{
    /// <summary>
    /// 方位 北 東 南 西
    /// </summary>
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
        Random r = new Random();
        /// <summary>
        /// 方位建構子預設 東風東 開始
        /// </summary>
        public Location()
        {
            round = location.East;
            winer = location.East;
            setPosition();
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
        /// 設定開牌位置
        /// </summary>
        public void setPosition()
        {
            switch (r.Next(3))
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
        /// 風(莊家)
        /// </summary>
        public location Winer
        {
            get
            {
                return winer;
            }
        }
        /// <summary>
        /// 下一個方位 E->S->W->N
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
        /// 轉換方位到字串
        /// </summary>
        /// <param name="lo">方位</param>
        /// <returns>字串</returns>
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
        /// 把方位轉換成字串傳回
        /// </summary>
        /// <returns>字串</returns>
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
    }
}
