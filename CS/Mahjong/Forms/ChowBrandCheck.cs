using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mahjong.Players;

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
            addimage_to_FlowLayout(flowLayout1, player[0]);
            addimage_to_FlowLayout(flowLayout2, player[1]);
            addimage_to_FlowLayout(flowLayout3, player[2]);
        }

        private void flowLayout1_Paint(object sender, PaintEventArgs e)
        {
            ans_check = 0;
            this.Close();
        }

        private void flowLayout2_Paint(object sender, PaintEventArgs e)
        {
            ans_check = 1;
            this.Close();
        }

        private void flowLayout3_Paint(object sender, PaintEventArgs e)
        {
            ans_check = 2;
            this.Close();
        }
        /// <summary>
        /// 取得按下的玩家
        /// </summary>
        public BrandPlayer SelectBrandPlayer
        {
            get
            {
                return player[ans_check];
            }
        }

        void addimage_to_FlowLayout(FlowLayoutPanel flow,BrandPlayer player)
        {
            for (int i = 0; i < player.getCount(); i++)
            {
                Bitmap bitmap = new Bitmap(player.getBrand(i).image);
                BrandBox b = new BrandBox(player.getBrand(i));

                bitmap = ResizeBitmap(bitmap, Mahjong.Properties.Settings.Default.ResizePercentage);

                b.Image = bitmap;
                flow.Controls.Add(b);
            }        
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