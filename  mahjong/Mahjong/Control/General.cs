using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Control
{
    [Serializable]
    public class General
    {
        public string Name;	// �Z�N�m�W
        public string Photo;	// �Z�N�ۤ�
        public int Loyality;	// ���۫�
        public int Wisdom;	// ���O
        public int Strength;	// �Z�O
        public int Diplomacy;	// �F�v
        public bool Ride;	// �M�L
        public bool Archery;	// �}�b
        public bool Sail;		// ���L


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
