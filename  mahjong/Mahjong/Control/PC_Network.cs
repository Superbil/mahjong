using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Mahjong.Brands;
using Mahjong.Forms;

namespace Mahjong.Control
{
    public class PC_Network : ProgramControl
    {
        public PC_Network(ProgramControl pc)
        {
            this.table = pc.table;
            this.information = pc.information;
            this.table.pc = pc;
            this.chat = pc.chat;
            this.all = pc.all;
        }
        public override void newgame()
        {
            table.cleanAll();
            // �]�w4�Ӫ��a,�C�ӤH16�i
            all = chat.AllPlayer;
            all.sumBrands = factory.SumBrands;
            // �]�w�֬O���a
            IamPlayer();
            // �]�w AllPlayers
            table.Setup(all);
            // �]�w�P��Ū�쪺��m
            setupPlace();
            newgame_round();
        }

        internal override void round()
        {            
            updatePlace();
            base.round();
            table.cleanImage();
            table.addImage();                        
        }

        internal override void IamPlayer()
        {
            this.all.isPlayer[(int)location.West] = true;
            this.all.isPlayer[(int)location.South] = true;
            this.all.isPlayer[(int)location.East] = true;
            this.all.isPlayer[(int)location.North] = true;
        }
        internal override bool pushToTable(Brand brand)
        {            
            
            // ��P�q�{�b�����a��W����
            all.NowPlayer.remove(brand);
            // ���ୱ�W
            all.PushToTable(brand);

            chat.SendAllPlayer(all);
            updatePlace();
            
            // �Ƨǲ{�b�����a
            this.chat.AllPlayer.sortNowPlayer();
            // ��s�{�b���a�M�ୱ
            
            //updatePlayer_Table();
            table.cleanImage();
            table.addImage();
            setInforamtion();            
            
            // �ݬO�_���H�n �J �b �I �Y
            return check_chow_pong_kong_win(brand);
            //return base.pushToTable(brand);
        }
        internal override void setupPlace()
        {
            //uint temp = this.all.state;
            //this.all.state = (int)location.East;
            
            //this.table.place.Down = all.State;
            //this.all.next();
            //this.table.place.Right = all.State;
            //this.all.next();
            //this.table.place.Up = all.State;
            //this.all.next();
            //this.table.place.Left = all.State;
            //this.all.next();
            //this.all.place = this.table.place;

            //this.all.state = temp;
            base.setupPlace();
        }
        internal void updatePlace()
        {
            this.table.place.Down = all.State;
            this.all.next();
            this.table.place.Right = all.State;
            this.all.next();
            this.table.place.Up = all.State;
            this.all.next();
            this.table.place.Left = all.State;
            this.all.next();
            this.all.place = this.table.place;
        }
    }
}
