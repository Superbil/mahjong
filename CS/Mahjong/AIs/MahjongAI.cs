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
        /// <param name="player">���a</param>
        void setPlayer(BrandPlayer player);
        /// <summary>
        /// �]�w�M�P
        /// </summary>
        /// <param name="brand">�P</param>
        /// <param name="player">���a</param>
        void setPlayer(Brand brand,BrandPlayer player);
        /// <summary>
        /// ���o�ǳƭn�X�⪺�P
        /// </summary>
        /// <returns>�P</returns>
        Brand getReadyBrand();
        /// <summary>
        /// ���o�ǳƦn���P��
        /// </summary>
        /// <returns></returns>
        BrandPlayer getReadyBrandPlayer();
    }
}
