using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using Mahjong.Brands;

namespace Mahjong.Control
{
    /// <summary>
    /// �̮ୱ�W���P�ӵo�P
    /// </summary>
    class Deal
    {
        /// <summary>
        /// ���a�}�C
        /// </summary>
        BrandPlayer[] player;
        /// <summary>
        /// �p��C�@�ӭn���t�h��
        /// </summary>
        private int countbrands;
        /// <summary>
        /// �p��@�@���h�֪��a
        /// </summary>
        private int countplayer;
        /// <summary>
        /// �ୱ,�P���ӷ�
        /// </summary>
        BrandPlayer table;

        /// <summary>
        /// �غc�򥻪��a�ƶq�M���t��
        /// </summary>
        /// <param name="countbrands">�C�@�Ӫ��a���t��</param>
        /// <param name="countplayer">�@�@���h�֪��a</param>
        /// <param name="table">�ୱ���a</param>
        public Deal(int countbrands, int countplayer,BrandPlayer table)
        {
            this.countbrands = countbrands;
            this.countplayer = countplayer;
            this.player = new BrandPlayer[countplayer];
            this.table = table;
            for (int i = 0; i < countplayer; i++)
                this.player[i] = new BrandPlayer();
        }
        /// <summary>
        /// �غc�򥻪��a�ƶq�M���t��
        /// </summary>
        /// <param name="countbrands">�C�@�Ӫ��a���t��</param>
        /// <param name="table">�ୱ���a</param>
        public Deal(int countbrands, BrandPlayer table)
        {
            this.countbrands = countbrands;
            this.countplayer = 4;
            this.player = new BrandPlayer[countplayer];
            this.table = table;
            for (int i = 0; i < countplayer; i++)
                this.player[i] = new BrandPlayer();
        }
        /// <summary>
        /// ���t�P
        /// </summary>
        public void DealBrands()
        {
            BrandPlayer temp = new BrandPlayer();
            // Ū�X�ƶq���P�A�ò���
            for (int i = 0; i < countplayer * countbrands; i++)
            {
                Brand brand = table.getBrand(i);
                temp.add(brand);
                table.remove(brand);
            }
            // ��P�[�J���a
            for (int i = 0; i < temp.getCount(); i++)
                player[i % countplayer].add(temp.getBrand(i));
        }
        /// <summary>
        /// �q�ୱ�W����
        /// </summary>
        /// <param name="iterator">�ୱ���о�</param>
        /// <param name="re">�ୱ���a</param>
        /// <returns>�����᪺�ୱ���a</returns>
        BrandPlayer removefromtable(Iterator iterator, BrandPlayer re)
        {
            while (iterator.hasNext())
            {
                Brand brand = (Brand)iterator.next();
                //��P�q���a������
                re.remove(brand);
                //Console.WriteLine(">>{0}", re.remove(brand));
            }
            return re;
        }
        /// <summary>
        /// �Ǧ^���a
        /// </summary>
        public BrandPlayer[] Player
        {
            get { return player; }
        }
        /// <summary>
        /// �Ǧ^�ୱ
        /// </summary>
        public BrandPlayer Table
        {
            get { return table; }
        }
    }
}
