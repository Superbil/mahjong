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
        /// ���ε{�����D�n�i�J�I�C
        /// </summary>
        [STAThread]
        static void Main()
        {
            // �{������
            ProgramControl pc = new ProgramControl();
            //Table table = new Table(pc);
            //table.ShowDialog();
            // ���յP�M���͵P
            //new BrandsTest();
            // ����AI
            //new AiTest();
            // ���եx�ƭp��
            //new TallyTest();
            // ���լO�_�J�P
            //new CheckTest();
            // ���դ��P
            //new DealTest();
        }
    }
}
