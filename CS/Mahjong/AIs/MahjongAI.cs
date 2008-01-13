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
        /// <param name="player">玩家</param>
        void setPlayer(BrandPlayer player);
        /// <summary>
        /// 設定和牌
        /// </summary>
        /// <param name="brand">牌</param>
        /// <param name="player">玩家</param>
        void setPlayer(Brand brand,BrandPlayer player);
        /// <summary>
        /// 取得準備要出手的牌
        /// </summary>
        /// <returns>牌</returns>
        Brand getReadyBrand();
        /// <summary>
        /// 取得準備好的牌組
        /// </summary>
        /// <returns></returns>
        BrandPlayer getReadyBrandPlayer();
    }
}
