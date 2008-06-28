using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.AIs;
using Mahjong.Brands;

namespace Mahjong.Control
{

    public partial class ProgramControl
    {
        /// <summary>
        /// 開新遊戲(第一次)
        /// </summary>
        public void newgame()
        {
            table.cleanAll();            
            // 設定4個玩家,每個人16張
            all = new AllPlayers(4, 16);
            all.sumBrands = factory.SumBrands;
            IamPlayer();
            table.Setup(all);
            newgame_round();
        }
        /// <summary>
        /// 開新莊
        /// </summary>
        void newgame_round()
        {
            creatBrands();
            table.addImage();
            setInforamtion();
            Chow_Pong_Brand = false;
            Player_Pass_Brand = false;
            // 補花
            for (int i = 0; i < 4; i++)
            {
                // 補花
                all.Newgame_setFlower();
                // 排序
                all.sortNowPlayer();
                // 更新
                table.updateNowPlayer();
                // 下一家
                all.next();
            }
            roundTimer.Start();
        }
        /// <summary>
        /// 建立牌,並分配牌
        /// </summary>
        public void creatBrands()
        {
            factory = new BrandFactory();
            factory.createBrands();
            factory.randomBrands();
            all.Table = factory.getBrands();
            for (int i = 0; i < all.Table.getCount(); i++)
                all.Table.getBrand(i).WhoPush = location.Table;
            dealbrands();
        }

        /// <summary>
        /// 分配牌
        /// </summary>
        void dealbrands()
        {
            Deal deal = new Deal(all.Dealnumber, all.CountPlayer, all.Table);
            deal.DealBrands();
            // get Players
            all.Players = deal.Player;
            // get Table
            all.Table = deal.Table;
        }
    }
}
