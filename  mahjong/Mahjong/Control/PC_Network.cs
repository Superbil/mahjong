using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Mahjong.Brands;
using Mahjong.Forms;
using Mahjong.Properties;

namespace Mahjong.Control
{
    public class PC_Network : ProgramControl
    {
        bool check_newgame = false;
        public PC_Network(Form f,ProgramControl pc) : base(f)
        {
            table = (Table)f;
            table.pc = this;
            chat = pc.chat;
            chat.PC = this;
        }
        public override void newgame()
        {
            table.clearAll();
            // �]�w4�Ӫ��a,�C�ӤH16�i
            all = new AllPlayers(4, 16);
            all.sumBrands = factory.SumBrands;
            // �]�w�֬O���a
            IamPlayer();
            // �]�w�m�W
            setName();
            // �]�w AllPlayers
            table.Setup(all);
            // �]�w�P��Ū�쪺��m
            setupPlace();
            // �إߵP
            creatBrands();
            // �P�B���a���
            chat.SendAllPlayer(all);
            newgame_round();
        }

        private void setName()
        {
            for (int i = 0; i < chat.name.Length; i++)
                if (chat.name[i] != null && chat.name[i] != "")
                    all.Name[i] = chat.name[i];
        }

        internal void newgame_network(AllPlayers all)
        {
            this.all = all;
            if (!check_newgame)
            {
                table.Setup(all);                
                check_newgame = true;
                clientPlace();
                newgame_round();                
            }
            else
            {

            }
        }

        internal override void newgame_round()
        {
            //chat.SendAllPlayer(all);
            //MessageBox.Show(all.Name[all.state] + " Get Run!", chat.ChatName);
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
            //clientPlace();
            //base.round();
            try
            {
                //round();

                // �p�G�O�Y�θI���N�P
                if (Chow_Pong_Brand)
                    Chow_Pong_Brand = false;
                else
                    touchBrand();
                // �ثe���A�����󪱮a��
                if (!NowPlayer_isPlayer)
                    makeBrand(getfromAI());
                else
                    setInforamtion();
            }
            catch (FlowOverException)
            {
                // �y��
                MessageBox.Show(Settings.Default.FlowEnd);
                table.cleanImage();
                factory = new BrandFactory();
                all.nextWiner(true);
                // �s��
                newgame_round();
            }
            catch (ErrorBrandPlayerCountException)
            {
                MessageBox.Show(Settings.Default.ErrorBrandPlayer);
            }

            table.cleanImage();
            table.addImage();
        }

        internal override void IamPlayer()
        {
            if (chat.HowMuchPlayer >= 3)
                this.all.isPlayer[(int)location.South] = true;
            if (chat.HowMuchPlayer >= 2)
                this.all.isPlayer[(int)location.West] = true;
            if (chat.HowMuchPlayer >= 1)
                this.all.isPlayer[(int)location.North] = true;
            if (chat.HowMuchPlayer >= 0)
                this.all.isPlayer[(int)location.East] = true;
        }
        internal override bool pushToTable(Brand brand)
        {            
            
            // ��P�q�{�b�����a��W����
            all.NowPlayer.remove(brand);
            // ���ୱ�W
            all.PushToTable(brand);

            //chat.SendAllPlayer(all);
            
            //clientPlace();
            
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
        internal void clientPlace()
        {
            if (chat.Mark == "1.player")
            {
                table.place.Up = location.South;
                table.place.Right = location.West;
                table.place.Down = location.North;
                table.place.Left = location.East;
            }
            else if (chat.Mark == "2.player")
            {
                table.place.Up = location.East;
                table.place.Right = location.South;
                table.place.Down = location.West;
                table.place.Left = location.North;
            }
            if (chat.Mark == "3.player")
            {
                table.place.Up = location.North;
                table.place.Right = location.East;
                table.place.Down = location.South;
                table.place.Left = location.West;
            } 
            all.place = table.place;
        }
        internal override void makeBrand(Brand brand)
        {
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
            //roundTimer.Start();
            //chat.SendAllPlayer(all);
        }
        /// <summary>
        /// �O�_����ۤv
        /// </summary>
        internal override bool NowPlayer_isPlayer
        {            
            get
            {
                return base.NowPlayer_isPlayer;
            }
        }
        //void setInforamtion(AllPlayers all)
        //{
        //    information.setup(table, all);
        //    information.updateInformation();
        //    information.DebugMode = table.ShowAll;
        //    information.Show();
        //}
    }
}
