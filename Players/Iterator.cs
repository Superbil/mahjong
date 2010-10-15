using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Players
{
    /// <summary>
    /// 反覆器
    /// </summary>
    public interface Iterator
    {
        /// <summary>
        /// 是否還有下一個
        /// </summary>
        /// <returns>布林值</returns>
        bool hasNext();
        /// <summary>
        /// 取得下一個
        /// </summary>
        /// <returns>物件</returns>
        Object next();
    }
}
