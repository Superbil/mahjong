using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Mahjong.Forms;

namespace Mahjong.Control
{
    class Mahjong_Main
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Table());
            //測試牌和產生牌
            //new BrandsTest();
            //測試AI
            new AiTest();
            //測試台數計算
            //new TallyTest();
        }
    }
}
