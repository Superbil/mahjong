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
            createPlayer();
        }
        private void createPlayer()
        {
            for(int i = 0 ; i < countplayer ; i++ )
            {
                this.player[i] = new BrandPlayer();
            }
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
            createPlayer();
        }
        /// <summary>
        /// ���t�P
        /// </summary>
        /// <returns>�O�_���t���\</returns>
        public void DealBrands()
        {
            Iterator iterator_temp;
            iterator_temp = table.creatIterator(countbrands * countplayer);
            table = removefromtable(iterator_temp, table);
            iterator_temp = table.creatIterator(countbrands * countplayer);
            dealtoplayer(iterator_temp);                       
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
        /// <param name="number">�ĴX�Ӫ��a</param>
        /// <returns>���a</returns>
        public BrandPlayer getPlayer(int number)
        {
            if (number > countplayer)
                return null;
            else
                return this.player[number];
        }
        /// <summary>
        /// �q�ୱ��P�s�W�쪱�a
        /// </summary>
        /// <param name="iterator">�ୱ���о�</param>
        /// <returns>�O�_���\</returns>
        void dealtoplayer(Iterator iterator)
        {
            while (iterator.hasNext())
                for (int i = 0; i < countbrands * countplayer; i++)
                    player[i % countplayer].add((Brand)iterator.next());
        }
    }
}