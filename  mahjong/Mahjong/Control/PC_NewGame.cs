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
        public virtual void newgame()
        {
            table.cleanAll();            
            // 設定4個玩家,每個人16張
            all = new AllPlayers(4, 16);
            all.sumBrands = factory.SumBrands;
            // 設定誰是玩家
            IamPlayer();
            // 設定 AllPlayers
            table.Setup(all);
            // 設定牌桌讀到的位置
            setupPlace();
            newgame_round();
        }

        /// <summary>
        /// 設定牌桌對應到的玩家位置(單機)
        /// </summary>
        internal virtual void setupPlace()
        {
            table.place.Up = location.North;
            table.place.Right = location.East;
            table.place.Down = location.South;
            table.place.Left = location.West;
            all.place = table.place;
        }
        /// <summary>
        /// 設定我是玩家
        /// </summary>
        internal virtual void IamPlayer()
        {
            all.isPlayer[(int)location.South] = true;
        }
        /// <summary>
        /// 開新莊
        /// </summary>
        internal void newgame_round()
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
