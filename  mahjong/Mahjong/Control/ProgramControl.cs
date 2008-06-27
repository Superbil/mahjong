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
        internal Timer roundTimer = new Timer();
        /// <summary>
        /// �������a�M�ୱ
        /// </summary>
        internal AllPlayers all;
        /// <summary>
        /// AI����
        /// </summary>
        internal MahjongAI Ai = new Level_1();
        /// <summary>
        /// ��T��
        /// </summary>
        internal Information information = new Information();
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
        internal bool showMessageBox;
        /// <summary>
        /// �C������غc�l
        /// </summary>
        public ProgramControl()
        {
            table = new Table(this);        
            roundTimer.Tick += new EventHandler(rotateTimer_Tick);
            showMessageBox = true;
            roundTimer.Interval = Mahjong.Properties.Settings.Default.RunRoundTime_Normal;            
            table.ShowDialog();
            //this.table = table;
        }
        /// <summary>
        /// ������U�Ӫ��a�ҭn�����Ʊ�
        /// </summary>
        /// <param name="sender">�ɶ��˼ƾ�</param>
        /// <param name="e"></param>
        internal void rotateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                round();
            }
            catch (FlowOverException)
            {
                // �y��
                MessageBox.Show(Mahjong.Properties.Settings.Default.FlowEnd);
                table.cleanImage();
                factory = new BrandFactory();
                all.nextWiner(true);                
                // �s��
                newgame_round();
            }
            catch (ErrorBrandPlayerCountException)
            {
                MessageBox.Show(Mahjong.Properties.Settings.Default.ErrorBrandPlayer);
            }
        }
        /// <summary>
        /// �ϥΪ̫��U�@�i�P
        /// </summary>
        /// <param name="brand">���U���P</param>
        internal virtual void makeBrand(Brand brand)
        {
            // ��P����ୱ�W�ݬO�_���H�n �J �b �I �Y
            if (pushToTable(brand))
            {
                // ���U�@�ӤH
                all.next();
                setInforamtion();
            }
            // �p�ɾ����s�Ұ�
            roundTimer.Start();
        }

        /// <summary>
        /// ���a���U�Y�ƥ�I�s
        /// </summary>
        internal void chow(Brand brand)
        {
            Check c = new Check(brand, NowPlayer_removeTeam);
            if (c.Chow())
                if (c.ChowLength == 1)
                {
                    PlayerSort p = new PlayerSort(c.SuccessPlayer);
                    all.chow_pong(brand, p.getPlayer);
                }
                else
                {
                    ChowBrandCheck cbc = new ChowBrandCheck(c.ChowPlayer);
                    cbc.ShowDialog();
                    PlayerSort p = new PlayerSort(cbc.SelectBrandPlayer);
                    all.chow_pong(brand, p.getPlayer);
                }
            Chow_Pong_Brand = true;
            updatePlayer_Table();
        }
        /// <summary>
        /// ���a���U�I�ƥ�I�s
        /// </summary>
        internal void pong(Brand brand)
        {
            Check c = new Check(brand, NowPlayer_removeTeam);
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
            Check c = new Check(brand, NowPlayer_removeTeam);
            Check d = new Check(brand, all.NowPlayer);
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
            Check c = new Check(brand, NowPlayer_removeTeam);
            Check d = new Check(NowPlayer_removeTeam);
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
        internal void pass(Brand brand)
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
        internal void setInforamtion()
        {
            information.setAllPlayers(all);
            information.DebugMode = table.ShowAll;
            information.Show();
        }
        /// <summary>
        /// �������w�g���X�h���P�աA�H�P�սs���ӰϤ�
        /// </summary>
        internal BrandPlayer NowPlayer_removeTeam
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
        void updatePlayer_Table()
        {
            table.updateNowPlayer();
            table.updateTable();
        }
        /// <summary>
        /// �s�u�]�w
        /// </summary>
        internal void onlineGame()
        {
            table.Close();
            chat = new ChatServerForm();
            chat.Show();            
            //table = new Table(new PC_Network());
            //new PC_Network(table);
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

    }
}
