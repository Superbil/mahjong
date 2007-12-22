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

namespace Mahjong.Control
{
    public partial class ProgramControl : UserControl
    {
        AboutBox ab;
        Table table;
        ChatServerForm chat;
        Timer rotateTimer = null;
        AllPlayers all;
        public ProgramControl()
        {
            InitializeComponent();
            run();
        }
        private void run()
        {
            //�]�w4�Ӫ��a,�C�ӤH16�i
            all = new AllPlayers(4, 16);
            
            //���Table ����
            table = new Table(this, all);
            //table.Setup(this, all);
            table.ShowDialog();
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
            Config con = new Config();
        }
        public void newgame()                                                                                                                                                                       
        {           
            all.creatBrands();
            table.updateImage();
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
    }
}
