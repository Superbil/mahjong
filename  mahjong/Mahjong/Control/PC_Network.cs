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
        public PC_Network(Form f,ProgramControl pc) : base(f)
        {            
            table = (Table)f;
            information = pc.information;
            table.pc = this;
            chat = pc.chat;
            //all = pc.all;
            chat.PC = this;
        }
        public override void newgame()
        {
            table.cleanAll();
            // �]�w4�Ӫ��a,�C�ӤH16�i
            all = new AllPlayers(4, 16);
            all.sumBrands = factory.SumBrands;
            // �]�w�֬O���a
            IamPlayer();
            // �]�w AllPlayers
            table.Setup(all);
            // �]�w�P��Ū�쪺��m
            setupPlace();
            
            creatBrands();
            chat.SendAllPlayer(all);
            newgame_round();
        }

        internal override void newgame_round()
        {
            //chat.SendAllPlayer(all);
            //MessageBox.Show("Run!",chat.ChatName);
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
            //roundTimer.Start();
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

            //chat.SendAllPlayer(all);
            
            updatePlace();
            
            // �Ƨǲ{�b�����a
            //this.chat.AllPlayer.sortNowPlayer();
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
        internal override void makeBrand(Brand brand)
        {
            //lock (this)
            //{
                chat.SendAllPlayer(all);

                all.Players[(int)all.place.getRealPlace(all.State)].remove(brand);
                // ��P����ୱ�W�ݬO�_���H�n �J �b �I �Y
                // �Y���ߴN��ܨS���H�n�A�����ߴN��ܳQ�H����
                if (pushToTable(brand))
                {
                    // ���U�@�ӤH
                    all.next();
                    // ��s��T��
                    setInforamtion();
                }
                // �p�ɾ����s�Ұ�
                roundTimer.Start(); 
            //}
        }

    }
}
