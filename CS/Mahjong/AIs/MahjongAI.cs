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
        /// �]�w���a
        /// </summary>
        void setPlayer(BrandPlayer player);
        /// <summary>
        /// ���o�ǳƭn�X�⪺�P
        /// </summary>
        /// <returns>�P</returns>
        Brand getReadyBrand();
    }
}
