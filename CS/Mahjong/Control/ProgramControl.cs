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
        BrandFactory brandfactory;
        AboutBox ab;
        Table table;
        ChatServerForm chat;
        Timer rotateTimer = null;
        System.Windows.Forms.Control control;
        public ProgramControl()
        {
            InitializeComponent();
            table = new Table();
        }
        public void run()
        {
            table.ShowDialog();
            test();
        }
        public void exit()
        {
            Application.Exit();
        }
        public void about()
        {
            ab = new AboutBox();
        }
        public void help()
        {

        }
        public void newgame()                                                                                                                                                                       
        {
            BrandPlayer a = new BrandPlayer();
            BrandPlayer[] player = new BrandPlayer[4];

            BrandFactory x = new BrandFactory(a);
            x.createBrands();
            x.randomBrands();

            a = x.getBrands();

            Iterator ai = a.creatIterator();
            print(ai);

            Console.WriteLine("Over");
            Console.ReadLine();

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
        void test()
        {
            //ArrayList a = new ArrayList();
            
            
            
            //new BrandsTest();
            //imageList1.Draw

        }
        public void onlineGame()
        {
            chat= new ChatServerForm();
            chat.Show();
        }
    }
}
