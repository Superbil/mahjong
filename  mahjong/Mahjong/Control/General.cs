using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Control
{
    [Serializable]
    public class General
    {
        public string Name;	// 武將姓名
        public string Photo;	// 武將相片
        public int Loyality;	// 忠誠度
        public int Wisdom;	// 智力
        public int Strength;	// 武力
        public int Diplomacy;	// 政治
        public bool Ride;	// 騎兵
        public bool Archery;	// 弓箭
        public bool Sail;		// 水兵


        public General(string name, string photo, int loy, int wis, int str, int dip, bool ride, bool arch, bool sail)
        {
            this.Name = name;
            this.Photo = photo;
            this.Loyality = loy;
            this.Wisdom = wis;
            this.Strength = str;
            this.Diplomacy = dip;
            this.Ride = ride;
            this.Archery = arch;
            this.Sail = sail;
        }
    }

}
