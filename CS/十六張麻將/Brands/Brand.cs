using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// 牌的介面
    /// </summary>
    public interface Brand
    {
        /// <summary>
        /// 牌的類別
        /// </summary>
        /// <returns>類別</returns>
        string getClass();
        /// <summary>
        /// 牌的值的大小
        /// </summary>
        /// <returns>大小</returns>       
        int getNumber();
        /// <summary>
        /// 牌是否可視
        /// </summary> 
        bool IsCanSee
        {
            get;
            set;
        }
        /// <summary>
        /// 牌的圖片位置
        /// </summary>  
        Image image
        {
            get;
            set;
        }
        /// <summary>
        /// 牌的組別
        /// </summary>
        int team
        {
            get;
            set;
        }
    }
}
