using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Mahjong.Brands;
using System.IO;

namespace Mahjong.Players
{
    /// <summary>
    /// ���a
    /// </summary>
    [Serializable]
    public class BrandPlayer
    {
        ArrayList brandarray;
        public BrandPlayer()
        {
            this.brandarray = new ArrayList();
        }
        public BrandPlayer(ArrayList arraylist)
        {
            this.brandarray = arraylist;
        }

        /// <summary>
        /// ��S�w���P���X
        /// </summary>
        /// <param name="brand">�P��</param>
        /// <returns>�O�_���\����</returns>
        public bool remove(Brand brand)
        {
            if (brandarray.Contains(brand))
            {
                brandarray.Remove(brand);
                return true;
            }
            else
                return false;
        }        
        /// <summary>
        /// ��P�[�J�P����
        /// </summary>
        /// <param name="brand">�P��</param>
        public void add(Brand brand)
        {
            brandarray.Add(brand);
        }
        /// <summary>
        /// �Ǧ^���h�ֵP
        /// </summary>
        /// <returns>�̭��Ҧ��P�ƭp��</returns>
        public int getCount()
        {
            return brandarray.Count;
        }
        /// <summary>
        /// ���X�S�w���P
        /// </summary>
        /// <param name="brand">�P��</param>
        /// <returns>�S�w���P</returns>
        public Brand getBrand(int number)
        {
            return (Brand)brandarray[number];
        }
        /// <summary>
        /// �M�����a
        /// </summary>
        public void clear()
        {
            brandarray.Clear();
        }
        /// <summary>
        /// �إߪ��a���о�
        /// </summary>
        /// <returns>���о�</returns>
        public Iterator creatIterator()
        {
            return new PlayerIterator(brandarray);
        }
        /// <summary>
        /// �إߪ��a���о�
        /// </summary>
        /// <param name="limitnumber">�����</param>
        /// <returns>���о�</returns>
        public Iterator creatIterator(int limitnumber)
        {
            return new PlayerIteratorLimit(brandarray,limitnumber);
        }
        ////�[�J�T�i�P
        //public void add(Brand b1,Brand b2,Brand b3)
        //{
        //    brandarray.Add(new TeamBrands(b1, b2, b3));
        //    //ArrayList temparraylist = new ArrayList(3);
        //    //temparraylist.Add(b1);
        //    //temparraylist.Add(b2);
        //    //temparraylist.Add(b3);
        //    //brandarray.Add(temparraylist);
        //}
        ////�[�J�|�i�P
        //public void add(Brand b1, Brand b2, Brand b3,Brand b4)
        //{
        //    brandarray.Add(new TeamBrands(b1, b2, b3, b4));
        //}

    }
}
