using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Mahjong.Brands;
using Mahjong.Forms;
using Mahjong.Properties;

namespace Mahjong.Control
{
    public class PC_Network : ProgramControl
    {
        //Timer update = new Timer();
        internal bool check_newgame = false;
        public PC_Network(Form f,ProgramControl pc) : base(f)
        {
            table = (Table)f;
            table.pc = this;
            chat = pc.chat;
            chat.PC = this;
            //information = new Information();
            //update.Tick += new EventHandler(update_Tick);
            //update.Interval = 1;
        }

        //void update_Tick(object sender, EventArgs e)
        //{
        //    update.Stop();
        //    table.cleanImage();
        //    table.addImage();
        //    setInforamtion(all);
        //}
        public override void newgame()
        {
            table.clearAll();
            // 設定4個玩家,每個人16張
            all = new AllPlayers(4, 16);
            all.sumBrands = factory.SumBrands;
            // 設定誰是玩家
            IamPlayer();
            // 設定姓名
            setName();
            // 設定 AllPlayers
            table.Setup(all);
            // 設定牌桌讀到的位置
            setupPlace();
            // 建立牌
            creatBrands();
            // 同步玩家資料
            chat.SendObject(all);
            check_newgame = true;
            newgame_round();
        }

        private void setName()
        {
            for (int i = 0; i < chat.name.Length; i++)
                if (chat.name[i] != null && chat.name[i] != "")
                    all.Name[i] = chat.name[i];
        }

        internal void newgame_network(AllPlayers all)
        {
            this.all = all;
            if (!check_newgame)
            {
                table.Setup(all);                
                check_newgame = true;
                clientPlace();                
                setInforamtion();
                newgame_round();                
            }
            else
            {
                table.Allplayers = all;
                table.cleanImage();
                table.addImage();
                setInforamtion();
            }
        }

        internal void getBrand(Brand brand)
        {

        }

        internal void getCheckUser(CheckUser check)
        {

        }

        internal override void newgame_round()
        {
            //MessageBox.Show(all.Name[all.state] + " Get Run!", chat.ChatName);
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

            // 同步玩家資料
            chat.SendObject(all);

            //if (all.State == table.place.Down)
            round();
            //roundTimer.Start();
        }

        internal override void round()
        {            
            //clientPlace();
            //base.round();
            try
            {
                // 如果是吃或碰不摸牌
                if (Chow_Pong_Brand)
                    Chow_Pong_Brand = false;
                else
                    touchBrand();

                // 目前狀態不等於玩家時
                if (!NowPlayer_is_Real_Player)
                    makeBrand(getfromAI());
                else
                    setInforamtion();

                // 同步玩家資料
                chat.SendObject(all);
            }
            catch (FlowOverException)
            {
                // 流局
                MessageBox.Show(Settings.Default.FlowEnd);
                table.cleanImage();
                factory = new BrandFactory();
                all.nextWiner(true);
                // 新局
                newgame_round();
            }
            catch (ErrorBrandPlayerCountException)
            {
                MessageBox.Show(Settings.Default.ErrorBrandPlayer);
            }

            table.cleanImage();
            table.addImage();
        }

        internal override void IamPlayer()
        {
            if (chat.HowMuchPlayer >= 3)
                this.all.isPlayer[(int)location.South] = true;
            if (chat.HowMuchPlayer >= 2)
                this.all.isPlayer[(int)location.West] = true;
            if (chat.HowMuchPlayer >= 1)
                this.all.isPlayer[(int)location.North] = true;
            if (chat.HowMuchPlayer >= 0)
                this.all.isPlayer[(int)location.East] = true;
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
                // 同步化Allplayer
                chat.SendObject(all);
            }
            // 計時器重新啟動
            //roundTimer.Start();
            //chat.SendAllPlayer(all);
            // 是不是現在的玩家
            //if (all.State == table.place.Down)
            round();
        }

        internal override bool pushToTable(Brand brand)
        {            
            
            // 把牌從現在的玩家手上移除
            all.NowPlayer.remove(brand);
            // 放到桌面上
            all.PushToTable(brand);
            // 同步玩家
            chat.SendObject(all);
            
            // 排序現在的玩家
            all.sortNowPlayer();
            // 更新現在玩家和桌面            
            //updatePlayer_Table();
            table.cleanImage();
            table.addImage();
            setInforamtion();            
            
            // 看是否有人要 胡 槓 碰 吃
            return check_chow_pong_kong_win(brand);
        }
        internal override void setupPlace()
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
        internal void clientPlace()
        {
            if (chat.Mark == "1.player")
            {
                table.place.Up = location.South;
                table.place.Right = location.West;
                table.place.Down = location.North;
                table.place.Left = location.East;
            }
            else if (chat.Mark == "2.player")
            {
                table.place.Up = location.East;
                table.place.Right = location.South;
                table.place.Down = location.West;
                table.place.Left = location.North;
            }
            if (chat.Mark == "3.player")
            {
                table.place.Up = location.North;
                table.place.Right = location.East;
                table.place.Down = location.South;
                table.place.Left = location.West;
            } 
            all.place = table.place;
        }

        /// <summary>
        /// 是否輪到自己
        /// </summary>
        internal override bool NowPlayer_is_Real_Player
        {            
            get
            {
                return base.NowPlayer_is_Real_Player;
            }
        }

        delegate void setInformation_delegate();

        internal override void setInforamtion()
        {
            this.information.setup(table,all);            
            this.information.updateInformation();
            //if (this.information.InvokeRequired)
            //    this.information.Invoke(new setInformation_delegate(showInformation));
            //else
            //    showInformation();
            //this.information.Show();
            
        }

        void showInformation()
        {
            this.information.Show();
        }
    }
}
