using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Mahjong.Brands;
using Mahjong.Forms;

namespace Mahjong.Control
{
    public class PC_Network : ProgramControl
    {
        public PC_Network(ProgramControl pc)
        {
            this.table = pc.table;
            this.information = pc.information;
            this.table.pc = pc;
            this.chat = pc.chat;
            this.all = pc.all;
        }
        public override void newgame()
        {
            table.cleanAll();
            // 設定4個玩家,每個人16張
            all = chat.AllPlayer;
            all.sumBrands = factory.SumBrands;
            // 設定誰是玩家
            IamPlayer();
            // 設定 AllPlayers
            table.Setup(all);
            // 設定牌桌讀到的位置
            setupPlace();
            newgame_round();
        }

        internal override void round()
        {            
            updatePlace();
            base.round();
            table.cleanImage();
            table.addImage();                        
        }

        internal override void IamPlayer()
        {
            this.all.isPlayer[(int)location.West] = true;
            this.all.isPlayer[(int)location.South] = true;
            this.all.isPlayer[(int)location.East] = true;
            this.all.isPlayer[(int)location.North] = true;
        }
        internal override bool pushToTable(Brand brand)
        {            
            
            // 把牌從現在的玩家手上移除
            all.NowPlayer.remove(brand);
            // 放到桌面上
            all.PushToTable(brand);

            chat.SendAllPlayer(all);
            updatePlace();
            
            // 排序現在的玩家
            this.chat.AllPlayer.sortNowPlayer();
            // 更新現在玩家和桌面
            
            //updatePlayer_Table();
            table.cleanImage();
            table.addImage();
            setInforamtion();            
            
            // 看是否有人要 胡 槓 碰 吃
            return check_chow_pong_kong_win(brand);
            //return base.pushToTable(brand);
        }
        internal override void setupPlace()
        {
            //uint temp = this.all.state;
            //this.all.state = (int)location.East;
            
            //this.table.place.Down = all.State;
            //this.all.next();
            //this.table.place.Right = all.State;
            //this.all.next();
            //this.table.place.Up = all.State;
            //this.all.next();
            //this.table.place.Left = all.State;
            //this.all.next();
            //this.all.place = this.table.place;

            //this.all.state = temp;
            base.setupPlace();
        }
        internal void updatePlace()
        {
            this.table.place.Down = all.State;
            this.all.next();
            this.table.place.Right = all.State;
            this.all.next();
            this.table.place.Up = all.State;
            this.all.next();
            this.table.place.Left = all.State;
            this.all.next();
            this.all.place = this.table.place;
        }
    }
}
