using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Mahjong.Forms;
using Mahjong.Brands;
using Mahjong.Players;
using Mahjong.AIs;

namespace Mahjong.Control
{
    public partial class ProgramControl : UserControl
    {
        Table table;
        ChatServerForm chat;
        Timer rotateTimer;
        AllPlayers all;
        MahjongAI Ai;
        Information inforamtion;        

        public ProgramControl()
        {
            InitializeComponent();
            setup();
            table.ShowDialog();        
        }
        void setup()
        {
            rotateTimer = new Timer();
            // ���Table ����
            table = new Table(this);
            inforamtion = new Information();
            Ai = new Level_1();
        }
        void rotateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                playgame();
            }
            catch (ArrayTypeMismatchException)
            {
                overgame();
            }
        }
        public void exit()
        {
            Application.Exit();
        }
        public void about()
        {
            new AboutBox();
        }
        public void config()
        {
            Config con = new Config(table);
            con.Show();
        }
        public void newgame()
        {
            //table.ShowAll = true; //��ܩҦ����P
            table.cleanAll();
            // �]�w4�Ӫ��a,�C�ӤH16�i
            all = new AllPlayers(4, 16);
            table.ShowAll = false;
            rotateTimer.Interval = 5000;
            rotateTimer.Tick += new EventHandler(rotateTimer_Tick);
            table.Setup(all);
            all.creatBrands();
            table.addImage();
            // �ɪ�
            for (int i = 0; i < 4; i++)
            {
                all.setFlower();
                all.sortNowPlayer();
                all.next();
                updatePlayer_Table();
            }
            
            updatePlayer_Table();
            rotateTimer.Start();
        }
        void playgame()
        {
            // �N�P���{�b�����a
            Brand nextbrand = all.nextBrand();
            all.NowPlayer.add(nextbrand);
            all.sortNowPlayer();
            updatePlayer_Table();
            // �ɪ�
            all.setFlower();
            updatePlayer_Table();
            // �O�_�J�P
            Check c = new Check(all.NowPlayer);
            if (c.Win())
                overgame();
            else if (c.Kong()) // �t�b
                kong();
            else
            {
                if (all.state == 2) // �H
                {
                    rotateTimer.Stop();
                    // ��ܦY�I�b�J�����s
                    CPK cpk = new CPK(this);
                    cpk.Enabled_Button(c.Chow(),c.Pong(),c.Kong(),c.Win());
                    if (c.Chow() || c.Pong() || c.Kong() || c.Win())
                        cpk.Show();
                }
                else
                {
                    if (c.Kong()) // �Q�b
                        ;
                    else if (c.Pong()) // �Q�I
                        ;
                    else if (c.Chow()) // �Q�Y
                        ;
                    else
                    {
                        Ai.setPlayer(all.NowPlayer);
                        pushToTable(Ai.getReadyBrand());
                    }
                }
            }

        }

        private void overgame()
        {
            table.cleanImage();
            rotateTimer.Stop();
            table.ShowAll = true;
            table.addImage();
            Tally t = new Tally();
            t.setLocation(all.getLocation(), all.Win_Times);
            t.setPlayer(all);
            t.ShowDialog();
        }
        void pushToTable(Brand brand)
        {
            rotateTimer.Stop();
            all.PushToTable(brand);
            updatePlayer_Table();
            rotateTimer.Start();
            all.next();
        }
        void updatePlayer_Table()
        {
            table.updateNowPlayer();
            table.updateTable();
        }
        /// <summary>
        /// �s�u�]�w
        /// </summary>
        public void onlineGame()
        {
            chat = new ChatServerForm();
            chat.ShowDialog();
        }
        /// <summary>
        /// �H���U�@�i�P
        /// </summary>
        /// <param name="brand">���U���P</param>
        public void makeBrand(Brand brand)
        {
            pushToTable(brand);
            rotateTimer.Start();
        }
        /// <summary>
        /// �]�w��ܸ�T
        /// </summary>
        public void setInforamtion()
        {
            inforamtion.setAllPlayers(all);
            inforamtion.Show();
        }
        /// <summary>
        /// �Y
        /// </summary>
        public void chow()
        {
            //all.chow_pong();
        }
        /// <summary>
        /// �I
        /// </summary>
        public void pong()
        {
            //all.chow_pong();
        }
        /// <summary>
        /// �b
        /// </summary>
        public void kong()
        {
            //all.kong();
        }
        /// <summary>
        /// �J
        /// </summary>
        public void win()
        {
            overgame();
        }
    }
}
