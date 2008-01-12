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
        AboutBox ab;
        Table table;
        ChatServerForm chat;
        Timer rotateTimer;
        AllPlayers all;
        MahjongAI Ai;

        public ProgramControl()
        {
            InitializeComponent();
            rotateTimer = new Timer();            
            //���Table ����
            table = new Table(this);
            table.ShowDialog();
            //Ai = new Level_1();
        }
        void rotateTimer_Tick(object sender, EventArgs e)
        {
            playgame();
        }
        public void exit()
        {
            Application.Exit();
        }
        public void about()
        {
            ab = new AboutBox();
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
            rotateTimer.Interval = 1000;
            rotateTimer.Tick += new EventHandler(rotateTimer_Tick);
            table.Setup(all);   
            all.creatBrands();
            table.addImage();            
            // �ɪ�
            for (int i = 0; i < 4; i++)
            {
                //MessageBox.Show(all.state.ToString());
                all.setFlower();
                all.sortNowPlayer();
                all.next();
                updatePlayer_Table();                
            }
            updatePlayer_Table();
            //playgame();
            rotateTimer.Start();
        }
        void playgame()
        {
            //rotateTimer.Start();
            // �N�P���{�b�����a
            Brand nextbrand = all.nextBrand();
            if (nextbrand == null)
                overgame();
            else
            {
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
                    ;
                else
                {
                    if (all.state == 2) // �H
                        rotateTimer.Stop();
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
                            Ai = new Level_1();
                            Ai.setPlayer(all.NowPlayer);
                            Brand b = Ai.getReadyBrand();
                            pushToTable(b);
                            
                            all.next();
                        }
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
        void pushToTable (Brand brand)
        {            
            //b.IsCanSee = true;
            //all.NowPlayer.remove(b);
            //all.Table.add(b);
            rotateTimer.Stop();
            //MessageBox.Show(brand.getNumber() + brand.getClass());
            all.PushToTable(brand);
            updatePlayer_Table();
            rotateTimer.Start();
        }
        void updatePlayer_Table()
        {
            table.updateNowPlayer();
            table.updateTable();
        }
        private void print(Iterator iterator)
        {
            Console.WriteLine();
            while (iterator.hasNext())
            {
                Brand brand = (Brand)iterator.next();
                Console.Write("{0},{1}\n", brand.getClass(), brand.getNumber());
            }
        }
        public void onlineGame()
        {
            chat= new ChatServerForm();
            chat.ShowDialog();
        }
        public void makeBrand(Brand brand)
        {            
            pushToTable(brand);
            rotateTimer.Start();
            all.next();
        }
    }
}
