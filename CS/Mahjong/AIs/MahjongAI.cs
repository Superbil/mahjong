using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Brands;
using Mahjong.Players;

namespace Mahjong.AIs
{
    public interface MahjongAI
    {
        /// <summary>
        /// 設定玩家
        /// </summary>
        void setPlayer(BrandPlayer player);
        /// <summary>
        /// 取得準備要出手的牌
        /// </summary>
        /// <returns>牌</returns>
        Brand getReadyBrand();
    }
}
