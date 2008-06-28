using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.AIs;
using Mahjong.Brands;

namespace Mahjong.Control
{

    public partial class ProgramControl
    {
        /// <summary>
        /// �}�s�C��(�Ĥ@��)
        /// </summary>
        public void newgame()
        {
            table.cleanAll();            
            // �]�w4�Ӫ��a,�C�ӤH16�i
            all = new AllPlayers(4, 16);
            all.sumBrands = factory.SumBrands;
            IamPlayer();
            table.Setup(all);
            newgame_round();
        }
        /// <summary>
        /// �}�s��
        /// </summary>
        void newgame_round()
        {
            creatBrands();
            table.addImage();
            setInforamtion();
            Chow_Pong_Brand = false;
            Player_Pass_Brand = false;
            // �ɪ�
            for (int i = 0; i < 4; i++)
            {
                // �ɪ�
                all.Newgame_setFlower();
                // �Ƨ�
                all.sortNowPlayer();
                // ��s
                table.updateNowPlayer();
                // �U�@�a
                all.next();
            }
            roundTimer.Start();
        }
        /// <summary>
        /// �إߵP,�ä��t�P
        /// </summary>
        public void creatBrands()
        {
            factory = new BrandFactory();
            factory.createBrands();
            factory.randomBrands();
            all.Table = factory.getBrands();
            for (int i = 0; i < all.Table.getCount(); i++)
                all.Table.getBrand(i).WhoPush = location.Table;
            dealbrands();
        }

        /// <summary>
        /// ���t�P
        /// </summary>
        void dealbrands()
        {
            Deal deal = new Deal(all.Dealnumber, all.CountPlayer, all.Table);
            deal.DealBrands();
            // get Players
            all.Players = deal.Player;
            // get Table
            all.Table = deal.Table;
        }
    }
}
