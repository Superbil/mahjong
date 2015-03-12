using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Mahjong.Forms;
using Mahjong.AIs;
using Mahjong.Brands;
using Mahjong.Players;
using Mahjong.Properties;
using System.Media;

namespace Mahjong.Control
{
    public partial class ProgramControl
    {
        /// <summary>
        /// 桌面介面
        /// </summary>
        internal Table table;
        /// <summary>
        /// 網路連線介面
        /// </summary>
        internal ChatServerForm chat;
        /// <summary>
        /// 換到下一家的計時器
        /// </summary>
        Timer roundTimer = new Timer();
        /// <summary>
        /// 全部玩家和桌面
        /// </summary>
        internal AllPlayers all;
        /// <summary>
        /// AI介面
        /// </summary>
        MahjongAI Ai = new Level_1();
        /// <summary>
        /// 設定盒
        /// </summary>
        internal Config con;
        /// <summary>
        /// 牌工廠
        /// </summary>
        internal BrandFactory factory = new BrandFactory();
        /// <summary>
        /// 吃碰牌之後是否要補牌
        /// </summary>
        internal bool Chow_Pong_Brand;
        /// <summary>
        /// 玩家按下過水
        /// </summary>
        internal bool Player_Pass_Brand;
        /// <summary>
        /// 是否要顯示提示訊息
        /// </summary>
        internal bool showMessageBox = true;
        /// <summary>
        /// 是否要播放音效
        /// </summary>
        internal bool PlayerSound = true;
        /// <summary>
        /// 聲音播放器
        /// </summary>
        SoundPlayer soundplayer = new SoundPlayer();
        /// <summary>
        /// 遊戲控制建構子
        /// </summary>
        public Brand sendoutbrand; 
        public ProgramControl(Form f)
        {      
            roundTimer.Tick += new EventHandler(rotateTimer_Tick);
            roundTimer.Interval = Settings.Default.RunRoundTime_Normal;
            table = (Table)f;
            table.pc = this;
        }
        internal void showTable()
        {
            table.ShowDialog();
        }
        /// <summary>
        /// 提示資訊是否開啟
        /// </summary>
        internal bool ShowMessageBox
        {
            set
            {
                showMessageBox = value;
            }
        }
        /// <summary>
        /// 設定延遲時間
        /// </summary>
        internal int SetDealyTime
        {
            set
            {
                roundTimer.Interval = value;
            }
        }
        /// <summary>
        /// 設定所有玩家
        /// </summary>
        public AllPlayers setAllPlayer
        {
            set
            {
                all = value;
            }
        }
        /// <summary>
        /// 現在的玩家是不是真實的玩家
        /// </summary>
        /// <returns>布林</returns>
        internal virtual bool NowPlayer_is_Real_Player
        {
            get
            {
                if (all.isPlayer[all.state])
                    return true;
                return false;
            }
        }

        /// <summary>
        /// 切換到下個玩家所要做的事情
        /// </summary>
        /// <param name="sender">時間倒數器</param>
        /// <param name="e"></param>
        protected void rotateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                round();
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
        }

        /// <summary>
        /// 玩家按下吃事件呼叫
        /// </summary>
        internal void chow(Brand brand)
        {
            CheckBrands c = new CheckBrands(brand, NowPlayer_removeTeam);
            if (c.Chow())
                if (c.ChowLength == 1)
                {
                    PlayerSort p = new PlayerSort(c.SuccessPlayer);
                    all.chow_pong(brand, p.getPlayer);
                }
                else
                {
                    if (all.State == table.place.Down)
                    {
                        ChowBrandCheck cbc = new ChowBrandCheck(c.ChowPlayer);
                        cbc.ShowDialog();
                        PlayerSort p = new PlayerSort(cbc.SelectBrandPlayer);
                        all.chow_pong(brand, p.getPlayer);
                    }
                    else
                    {
                        chat.SendObject(c.ChowPlayer);
                    }
                }
            Chow_Pong_Brand = true;
            updatePlayer_Table();
            
        }
        /// <summary>
        /// 玩家按下碰事件呼叫
        /// </summary>
        internal void pong(Brand brand)
        {
            CheckBrands c = new CheckBrands(brand, NowPlayer_removeTeam);
            if (c.Pong())
                all.chow_pong(brand, c.SuccessPlayer);
            Chow_Pong_Brand = true;
            updatePlayer_Table();
        }
        /// <summary>
        /// 玩家按下槓事件呼叫
        /// </summary>
        internal void kong(Brand brand)
        {
            CheckBrands c = new CheckBrands(brand, NowPlayer_removeTeam);
            CheckBrands d = new CheckBrands(brand, all.NowPlayer);
            if (c.Kong())
                all.kong(brand, c.SuccessPlayer);
            else if (d.Kong())
                all.kong(brand, d.SuccessPlayer);
            updatePlayer_Table();

        }
        /// <summary>
        /// 玩家按下暗槓事件呼叫
        /// </summary>
        internal void dark_kong(Brand brand)
        {
            CheckBrands c = new CheckBrands(brand, NowPlayer_removeTeam);
            CheckBrands d = new CheckBrands(NowPlayer_removeTeam);
            if (c.Kong())
                all.DarkKong(brand, c.SuccessPlayer);
            else if (d.DarkKong())
                all.DarkKong(brand, d.SuccessPlayer);
        }
        /// <summary>
        /// 玩家按下胡事件呼叫
        /// </summary>
        internal void win(Brand brand)
        {
            win_game(brand);
        }
        /// <summary>
        /// 玩家按下過水事件呼叫
        /// </summary>
        /// <param name="brand">牌</param>
        internal virtual void pass(Brand brand)
        {
            Player_Pass_Brand = true;
        }

        /// <summary>
        /// 程式結束
        /// </summary>
        internal void exit()
        {
            Application.Exit();
        }
        /// <summary>
        /// About box
        /// </summary>
        internal void about()
        {
            new AboutBox();
        }
        /// <summary>
        /// config Box
        /// </summary>
        internal void config()
        {
            con.Dispose();
            con = new Config(table);
            con.Show();
        }
        /// <summary>
        /// 設定顯示資訊
        /// </summary>
        internal virtual void setInforamtion()
        {
            table.setInforamtion();
        }
        /// <summary>
        /// 移除掉已經打出去的牌組，以牌組編號來區分
        /// </summary>
        protected BrandPlayer NowPlayer_removeTeam
        {
            get
            {
                BrandPlayer bp = new BrandPlayer();
                for (int i = 0; i < all.NowPlayer.getCount(); i++)
                    if (all.NowPlayer.getBrand(i).Team < 1)
                        bp.add(all.NowPlayer.getBrand(i));
                return bp;
            }
        }
        /// <summary>
        /// 只有牌組
        /// </summary>
        internal BrandPlayer NowPlayer_OnlyTeam
        {
            get
            {
                BrandPlayer bp = new BrandPlayer();
                for (int i = 0; i < all.NowPlayer.getCount(); i++)
                    if (all.NowPlayer.getBrand(i).Team > 1)
                        bp.add(all.NowPlayer.getBrand(i));
                return bp;
            }
        }

        /// <summary>
        /// 更新現在的玩家和桌面
        /// </summary>
        protected void updatePlayer_Table()
        {
            table.updateNowPlayer();
            table.updateTable();
        }
        /// <summary>
        /// 連線設定
        /// </summary>
        internal void onlineGame()
        {
            chat = new ChatServerForm();
            chat.PC = (PC_Network)this;
            chat.getNewGame += new newGameHandler(this.newClientGame);
            chat.getCheck += new CheckHandler(this.CheckUser);
            chat.getBrandplayers += new BrandplayersHandler(this.CheckChow);
            
            chat.startbutton.Click += new System.EventHandler(this.newServerGame);
            chat.getAllPlayer += new allPlayerHandler(this.updateAllPlayer);
            chat.Show();
        }
        private void newClientGame(object sender, EventArgs e)
        {
            //MessageBox.Show("Game start");
            //newgame();
        }
        public void getClientBrand(object sender, EventArgs e)
        {
            makeBrand((Brand)sender);
        }
        private void newServerGame(object sender, EventArgs e)
        {
            newgame();
        }
        private void CheckUser(object sender, EventArgs e)
        {
            CPK cpk = new CPK(this,(CheckUser)sender);
            cpk.Enabled_Button((CheckUser)sender);
            cpk.Network = true;
            cpk.ShowDialog();
        }
        public void CheckChow(object sender, EventArgs e)
        {
            ChowBrandCheck cbc = new ChowBrandCheck((BrandPlayer[])sender);
                       
            cbc.ShowDialog();
            chat.SendObject(cbc.SelectBrandPlayer);
        }
        public void CheckUserResult(object sender, EventArgs e)
        {
            if (((CheckUser)sender).Chow == true)
                chow(sendoutbrand);
            else if (((CheckUser)sender).Pong == true)
                pong(sendoutbrand);
            else if (((CheckUser)sender).Kong == true)
                kong(sendoutbrand);
            else if (((CheckUser)sender).Win == true)
                win(sendoutbrand);
            else if (((CheckUser)sender).DarkKong == true)
                dark_kong(sendoutbrand);
            else
                pass(sendoutbrand);
            chat.SendObject(all);
        }
        public void CheckChowResult(object sender, EventArgs e)
        {
            PlayerSort p = new PlayerSort((BrandPlayer)sender);
            all.chow_pong(sendoutbrand, p.getPlayer);
            chat.SendObject(all);
        }
        private void updateAllPlayer(object sender, EventArgs e)
        {
            this.all = (AllPlayers)sender;
            this.table.Allplayers = this.all;

            table.Setup(all);
            clientPlace();
            this.table.updateAllPlayer();
            this.table.updateTable();
            this.table.updateInforamation();
            
            //this.table.updateInforamation();
        }
        /// <summary>
        /// 從AI得到一張牌
        /// </summary>
        /// <returns></returns>
        internal Brand getfromAI()
        {
            Ai.setPlayer(NowPlayer_removeTeam);
            return Ai.getReadyBrand();
        }

        /// <summary>
        /// 把牌丟給玩家，看是否要吃 碰 槓 過水 胡
        /// </summary>
        internal virtual void toUser(CheckUser check)
        {
            CPK cpk = new CPK(this, check);

            CheckBrands c = new CheckBrands(check.Brand, NowPlayer_removeTeam);
            CheckBrands w = new CheckBrands(check.Brand, all.NowPlayer);
            cpk.Enabled_Button(check.Chow, check.Pong, check.Kong, check.DarkKong, check.Win);
            if (check.Chow || check.Pong || check.Kong || check.Win || check.DarkKong)
                cpk.ShowDialog();
        }
        internal virtual void clientPlace()
        { 
        }
    }
}
