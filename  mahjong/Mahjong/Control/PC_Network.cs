using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Mahjong.Brands;
using Mahjong.Forms;
using Mahjong.Properties;
using System.Threading;

namespace Mahjong.Control
{
    public class PC_Network : ProgramControl
    {
        internal bool check_newgame = false;
        CheckUser checkuser;
        bool get_CheckUser = false;
        public PC_Network(Form f,ProgramControl pc) : base(f)
        {
            table = (Table)f;
            table.pc = this;
            chat = pc.chat;
            chat.PC = this;
        }

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
            }
            else
            {
                table.Allplayers = all;
                table.cleanImage();
                table.addImage();
                setInforamtion();
            }
        }
        internal void getInt(int i)
        {
            MessageBox.Show(i.ToString());
        }
        internal void getBrand(Brand brand)
        {
            if (myTurn)
            {
                CPK cpk = new CPK(this, brand);
                cpk.Network = true;
                cpk.Checkuser = checkuser;
                CheckBrands c = new CheckBrands(brand, NowPlayer_removeTeam);
                CheckBrands w = new CheckBrands(brand, all.NowPlayer);
                cpk.Enabled_Button(checkuser.Chow, checkuser.Pong, checkuser.Kong, checkuser.DarkKong, checkuser.Win);
                //if (checkuser.Chow || checkuser.Pong || checkuser.Kong || checkuser.Win || checkuser.DarkKong)
                    cpk.ShowDialog();

                chat.SendObject(cpk.Checkuser);
            }
            else if (iAmServer)
            {
                makeBrand(brand);
            }
        }

        internal void getCheckUser(CheckUser check)
        {            
            checkuser = check;
            if (iAmServer)
            {
                get_CheckUser = true;
                //Monitor.Pulse(this);
            }
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
            if (iAmServer)
                chat.SendObject(all);

            //if (all.State == table.place.Down)
            if (iAmServer)
                round();
            //roundTimer.Start();
        }

        internal override void round()
        {            
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
                // 建立牌
                creatBrands();
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
        
        internal override void makeBrand(Brand brand)
        {            
            //if (!iAmServer)
            //chat.SendObject(123);
            //if (iAmServer)
            //{
            //    all.Players[(int)all.place.getRealPlace(all.State)].remove(brand);
            //}

            if (myTurn && !iAmServer)
                chat.SendObject(brand);
            // 把牌打到桌面上看是否有人要 胡 槓 碰 吃
            // 若成立就表示沒有人要，不成立就表示被人拿走

            if (iAmServer)
                if (pushToTable(brand))
                {
                    // 換下一個人
                    all.next();
                    // 更新資訊盒
                    setInforamtion();
                    // 同步化Allplayer
                    //chat.SendObject(all);
                }
            //chat.SendAllPlayer(all);
            // 是不是Server
            if (iAmServer)
                round();
            
        }

        internal override bool pushToTable(Brand brand)
        {

            // 把牌從現在的玩家手上移除
            //all.NowPlayer.remove(brand);
            //MessageBox.Show(all.NowPlayer.removeBrand(brand).ToString());
            all.NowPlayer.removeBrand(brand);
            //table.showBrand(brand);
            //MessageBox.Show(all.NowPlayer.remove(brand).ToString());
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
            //// 如果不是server
            //if (!iAmServer)
            //    chat.SendObject(brand);

            // 看是否有人要 胡 槓 碰 吃
            return check_chow_pong_kong_win(brand);
        }

        internal override void toUser(Brand brand, bool chow, bool pong, bool kong, bool darkkong, bool win)
        {
            if (myTurn)
                base.toUser(brand, chow, pong, kong, darkkong, win);
            else
            {
                chat.SendObject(all);
                chat.SendObject(new CheckUser(chow, pong, kong, darkkong, win, false));
                chat.SendObject(brand);
                while (true)
                {
                    if (get_CheckUser)
                        break;
                }
                //lock (this)
                //{
                //    MessageBox.Show("Lock!");
                //    //Monitor.Wait(this);
                //}
                runCheckUser(brand);


                get_CheckUser = false;
                chat.SendObject(all);
            }
        }

        void runCheckUser(Brand brand)
        {
            if (checkuser.Chow)
                chow(brand);
            else if (checkuser.Pong)
                pong(brand);
            else if (checkuser.Kong)
                kong(brand);
            else if (checkuser.DarkKong)
                dark_kong(brand);
            else if (checkuser.Win)
                win(brand);
            else if (checkuser.Pass)
                pass(brand);
        }

        internal override void pass(Brand brand)
        {
            base.pass(brand);
            get_CheckUser = false;
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

        //delegate void setInformation_delegate();

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

        bool myTurn
        {
            get
            {
                return all.State == table.place.Down;
            }
        }
        bool iAmServer
        {
            get
            {
                return chat.Mark == "Server";
            }
        }
    }
}
