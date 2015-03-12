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
        /// �ୱ����
        /// </summary>
        internal Table table;
        /// <summary>
        /// �����s�u����
        /// </summary>
        internal ChatServerForm chat;
        /// <summary>
        /// ����U�@�a���p�ɾ�
        /// </summary>
        Timer roundTimer = new Timer();
        /// <summary>
        /// �������a�M�ୱ
        /// </summary>
        internal AllPlayers all;
        /// <summary>
        /// AI����
        /// </summary>
        MahjongAI Ai = new Level_1();
        /// <summary>
        /// �]�w��
        /// </summary>
        internal Config con;
        /// <summary>
        /// �P�u�t
        /// </summary>
        internal BrandFactory factory = new BrandFactory();
        /// <summary>
        /// �Y�I�P����O�_�n�ɵP
        /// </summary>
        internal bool Chow_Pong_Brand;
        /// <summary>
        /// ���a���U�L��
        /// </summary>
        internal bool Player_Pass_Brand;
        /// <summary>
        /// �O�_�n��ܴ��ܰT��
        /// </summary>
        internal bool showMessageBox = true;
        /// <summary>
        /// �O�_�n���񭵮�
        /// </summary>
        internal bool PlayerSound = true;
        /// <summary>
        /// �n������
        /// </summary>
        SoundPlayer soundplayer = new SoundPlayer();
        /// <summary>
        /// �C������غc�l
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
        /// ���ܸ�T�O�_�}��
        /// </summary>
        internal bool ShowMessageBox
        {
            set
            {
                showMessageBox = value;
            }
        }
        /// <summary>
        /// �]�w����ɶ�
        /// </summary>
        internal int SetDealyTime
        {
            set
            {
                roundTimer.Interval = value;
            }
        }
        /// <summary>
        /// �]�w�Ҧ����a
        /// </summary>
        public AllPlayers setAllPlayer
        {
            set
            {
                all = value;
            }
        }
        /// <summary>
        /// �{�b�����a�O���O�u�ꪺ���a
        /// </summary>
        /// <returns>���L</returns>
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
        /// ������U�Ӫ��a�ҭn�����Ʊ�
        /// </summary>
        /// <param name="sender">�ɶ��˼ƾ�</param>
        /// <param name="e"></param>
        protected void rotateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                round();
            }
            catch (FlowOverException)
            {
                // �y��
                MessageBox.Show(Settings.Default.FlowEnd);
                table.cleanImage();
                factory = new BrandFactory();
                all.nextWiner(true);                
                // �s��
                newgame_round();
            }
            catch (ErrorBrandPlayerCountException)
            {
                MessageBox.Show(Settings.Default.ErrorBrandPlayer);
            }
        }

        /// <summary>
        /// ���a���U�Y�ƥ�I�s
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
        /// ���a���U�I�ƥ�I�s
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
        /// ���a���U�b�ƥ�I�s
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
        /// ���a���U�t�b�ƥ�I�s
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
        /// ���a���U�J�ƥ�I�s
        /// </summary>
        internal void win(Brand brand)
        {
            win_game(brand);
        }
        /// <summary>
        /// ���a���U�L���ƥ�I�s
        /// </summary>
        /// <param name="brand">�P</param>
        internal virtual void pass(Brand brand)
        {
            Player_Pass_Brand = true;
        }

        /// <summary>
        /// �{������
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
        /// �]�w��ܸ�T
        /// </summary>
        internal virtual void setInforamtion()
        {
            table.setInforamtion();
        }
        /// <summary>
        /// �������w�g���X�h���P�աA�H�P�սs���ӰϤ�
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
        /// �u���P��
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
        /// ��s�{�b�����a�M�ୱ
        /// </summary>
        protected void updatePlayer_Table()
        {
            table.updateNowPlayer();
            table.updateTable();
        }
        /// <summary>
        /// �s�u�]�w
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
        /// �qAI�o��@�i�P
        /// </summary>
        /// <returns></returns>
        internal Brand getfromAI()
        {
            Ai.setPlayer(NowPlayer_removeTeam);
            return Ai.getReadyBrand();
        }

        /// <summary>
        /// ��P�ᵹ���a�A�ݬO�_�n�Y �I �b �L�� �J
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
