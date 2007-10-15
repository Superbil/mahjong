using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Mahjong.Brands;
using System.IO;

namespace Mahjong.Players
{
    /// <summary>
    /// 玩家
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
        /// 把特定的牌移出
        /// </summary>
        /// <param name="brand">牌類</param>
        /// <returns>是否成功移除</returns>
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
        /// 把牌加入牌面區
        /// </summary>
        /// <param name="brand">牌類</param>
        public void add(Brand brand)
        {
            brandarray.Add(brand);
        }
        /// <summary>
        /// 傳回有多少牌
        /// </summary>
        /// <returns>裡面所有牌數計數</returns>
        public int getCount()
        {
            return brandarray.Count;
        }
        /// <summary>
        /// 取出特定的牌
        /// </summary>
        /// <param name="brand">牌類</param>
        /// <returns>特定的牌</returns>
        public Brand getBrand(int number)
        {
            return (Brand)brandarray[number];
        }
        /// <summary>
        /// 清除玩家
        /// </summary>
        public void clear()
        {
            brandarray.Clear();
        }
        /// <summary>
        /// 建立玩家反覆器
        /// </summary>
        /// <returns>反覆器</returns>
        public Iterator creatIterator()
        {
            return new PlayerIterator(brandarray);
        }
        /// <summary>
        /// 建立玩家反覆器
        /// </summary>
        /// <param name="limitnumber">限制值</param>
        /// <returns>反覆器</returns>
        public Iterator creatIterator(int limitnumber)
        {
            return new PlayerIteratorLimit(brandarray,limitnumber);
        }
        ////加入三張牌
        //public void add(Brand b1,Brand b2,Brand b3)
        //{
        //    brandarray.Add(new TeamBrands(b1, b2, b3));
        //    //ArrayList temparraylist = new ArrayList(3);
        //    //temparraylist.Add(b1);
        //    //temparraylist.Add(b2);
        //    //temparraylist.Add(b3);
        //    //brandarray.Add(temparraylist);
        //}
        ////加入四張牌
        //public void add(Brand b1, Brand b2, Brand b3,Brand b4)
        //{
        //    brandarray.Add(new TeamBrands(b1, b2, b3, b4));
        //}

    }
}
