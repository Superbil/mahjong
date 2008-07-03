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
        public PC_Network(Form f,ProgramControl pc) : base(f)
        {
            table = (Table)f;
            information = pc.information;
            table.pc = this;
            chat = pc.chat;
            //all = pc.all;
            chat.PC = this;
        }
        public override void newgame()
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
            
            creatBrands();
            // 同步玩家資料
            chat.SendAllPlayer(all);
            

            newgame_round();
            //newgame_network();
        }

        internal void newgame_network()
        {
            //MessageBox.Show("Setup Table");
            table.Setup(all);
            //setupPlace();
            table.addImage();
            //setInforamtion();
            //newgame_round();
        }

        internal override void newgame_round()
        {
            //chat.SendAllPlayer(all);
            all = chat.returnallplayer();
            table.Setup(all);
            MessageBox.Show(all.Name[all.state]+" Get Run!",chat.ChatName);
            //updatePlace();
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

            //chat.SendAllPlayer(all);
            
            updatePlace();
            
            // 排序現在的玩家
            //this.chat.AllPlayer.sortNowPlayer();
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
            //this.all.state = temp;
            base.setupPlace();
            //table.place.Up = location.North;
            //table.place.Right = location.East;
            //table.place.Down = location.South;
            //table.place.Left = location.West;
            //all.place = table.place;
        }
        internal void updatePlace()
        {
            //this.table.place.Down = all.State;
            //this.all.next();
            //this.table.place.Right = all.State;
            //this.all.next();
            //this.table.place.Up = all.State;
            //this.all.next();
            //this.table.place.Left = all.State;
            //this.all.next();
            //this.all.place = this.table.place;
            if (all.Name[all.state]==chat.ChatName)
            {
                table.place.Down = location.South;
                
            }

            table.place = all.place;
        }
        internal override void makeBrand(Brand brand)
        {
                all.Players[(int)all.place.getRealPlace(all.State)].remove(brand);
                // 把牌打到桌面上看是否有人要 胡 槓 碰 吃
                // 若成立就表示沒有人要，不成立就表示被人拿走
                if (pushToTable(brand))
                {
                    // 換下一個人
                    all.next();
                    // 更新資訊盒
                    setInforamtion();
                }
                // 計時器重新啟動
                //roundTimer.Start();
                chat.SendAllPlayer(all);
        }

    }
}
