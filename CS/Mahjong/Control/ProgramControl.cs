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
            rotateTimer.Interval = 500;
            //���Table ����
            table = new Table(this);
            table.ShowDialog();
            rotateTimer.Tick += new EventHandler(rotateTimer_Tick);
            Ai = new Level_1();
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
            table.cleanAll();
            // �]�w4�Ӫ��a,�C�ӤH16�i
            all = new AllPlayers(4, 16);            
            table.Setup(all);   
            all.creatBrands();
            table.addImage();
            rotateTimer.Start();
            //playgame();
        }
        void playgame()
        {
            // �ɪ�
            all.setFlower();            
            table.updateNowPlayer();
            table.updateTable();
            // �N�P���{�b�����a
            all.NowPlayer.add(all.nextBrand());
            if (all.state == 2)
               ;
            else
            {
                //Ai.setPlayer(all.NowPlayer);
                pushToTable(Ai.getReadyBrand());
            }
        }
        void pushToTable(Brand brand)
        {
            Ai.setPlayer(all.NowPlayer);

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
            StringBuilder str = new StringBuilder();
            str.Append(brand.getNumber());
            str.Append(brand.getClass());
            Console.WriteLine(str.ToString());
            MessageBox.Show(str.ToString());
        }
    }
}
