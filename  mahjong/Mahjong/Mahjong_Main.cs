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
            ProgramControl p = new ProgramControl(new Table());
            //ProgramControl p = new ProgramControl(new NewTable());
            p.showTable();
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
