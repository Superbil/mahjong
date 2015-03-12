using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mahjong.Players;
using Mahjong.Control;


namespace Mahjong.Forms
{
    public partial class ChowBrandCheck : Form
    {
        BrandPlayer[] player;
        int ans_check;

        public ChowBrandCheck(BrandPlayer[] player)
        {
            InitializeComponent();
            this.player = player;
        }

        private void ChowBrandCheck_Load(object sender, EventArgs e)
        {
            addimage_to_FlowLayout(flowLayout1, player[0], new EventHandler(F1_Click));
            addimage_to_FlowLayout(flowLayout2, player[1], new EventHandler(F2_Click));
            addimage_to_FlowLayout(flowLayout3, player[2], new EventHandler(F3_Click));
        }

        /// <summary>
        /// 取得按下的牌組
        /// </summary>
        public BrandPlayer SelectBrandPlayer
        {
            get
            {
                return player[ans_check];
            }
        }

        void addimage_to_FlowLayout(FlowLayoutPanel flow,BrandPlayer player,EventHandler ev)
        {
            for (int i = 0; i < player.getCount(); i++)
            {
                Bitmap bitmap = new Bitmap(ResourcesTool.getImage(player.getBrand(i)));
                BrandBox b = new BrandBox(player.getBrand(i));

                b.SizeMode = PictureBoxSizeMode.AutoSize;

                bitmap = ResizeBitmap(bitmap, Mahjong.Properties.Settings.Default.ResizePercentage);
                b.Click += ev;

                b.Image = bitmap;
                flow.Controls.Add(b);
            }        
        }

        void F1_Click(object sender, EventArgs e)
        {
            ans_check = 0;
            this.Close();
        }
        void F2_Click(object sender, EventArgs e)
        {
            ans_check = 1;
            this.Close();
        }
        void F3_Click(object sender, EventArgs e)
        {
            ans_check = 2;
            this.Close();
        }
        /// <summary>
        /// 重繪Bitmap(縮放)
        /// </summary>
        /// <param name="b">圖型</param>
        /// <param name="resize">比率</param>
        /// <returns>圖型</returns>
        private Bitmap ResizeBitmap(Bitmap b, double resize)
        {
            int nWidth = Convert.ToInt16(b.Width * resize);
            int nHeight = Convert.ToInt16(b.Height * resize);
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            return result;
        }
    }
}