using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Mahjong.Brands;
using Mahjong.Forms;
using Mahjong.Properties;
using System.Threading;

namespace Mahjong.Control
{
    public class PC_Network : ProgramControl
    {
        internal bool check_newgame = false;
        CheckUser checkuser;
        bool get_CheckUser = false;
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
            chat.SendObject(all);
            check_newgame = true;
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
                setInforamtion();
                //newgame_round();                
            }
            else
            {
                table.Allplayers = all;
                table.cleanImage();
                table.addImage();
                setInforamtion();
            }
        }

        internal void getBrand(Brand brand)
        {
            if (myTurn)
            {
                CPK cpk = new CPK(this, brand);
                cpk.Network = true;
                cpk.Checkuser = checkuser;
                CheckBrands c = new CheckBrands(brand, NowPlayer_removeTeam);
                CheckBrands w = new CheckBrands(brand, all.NowPlayer);
                cpk.Enabled_Button(checkuser.Chow, checkuser.Pong, checkuser.Kong, checkuser.DarkKong, checkuser.Win);
                if (checkuser.Chow || checkuser.Pong || checkuser.Kong || checkuser.Win || checkuser.DarkKong)
                    cpk.ShowDialog();

                chat.SendObject(cpk.Checkuser);
            }
            else if (iAmServer)
            {
                makeBrand(brand);
            }
        }

        internal void getCheckUser(CheckUser check)
        {            
            checkuser = check;
            if (iAmServer)
                get_CheckUser = true;
        }

        internal override void newgame_round()
        {
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

            // �P�B���a���
            if (iAmServer)
                chat.SendObject(all);

            //if (all.State == table.place.Down)
            if (iAmServer)
                round();
            //roundTimer.Start();
        }

        internal override void round()
        {            
            //clientPlace();
            //base.round();
            try
            {
                // �p�G�O�Y�θI���N�P
                if (Chow_Pong_Brand)
                    Chow_Pong_Brand = false;
                else
                    touchBrand();

                // �ثe���A�����󪱮a��
                if (!NowPlayer_is_Real_Player)
                    makeBrand(getfromAI());
                else
                    setInforamtion();

                // �P�B���a���
                chat.SendObject(all);
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

        internal override void makeBrand(Brand brand)
        {
            if (myTurn)
                all.Players[(int)table.place.getRealPlace(all.State)].remove(brand);
            
            if (myTurn)
                chat.SendObject(brand);
            // ��P����ୱ�W�ݬO�_���H�n �J �b �I �Y
            // �Y���ߴN��ܨS���H�n�A�����ߴN��ܳQ�H����
            
            
                if (pushToTable(brand))
                {
                    // ���U�@�ӤH
                    all.next();
                    // ��s��T��
                    setInforamtion();
                    // �P�B��Allplayer
                    chat.SendObject(all);
                }
                // �p�ɾ����s�Ұ�
                //roundTimer.Start();
                //chat.SendAllPlayer(all);
                // �O���O�{�b�����a
                //if (all.State == table.place.Down)
                if (iAmServer)
                    round();
            
        }

        internal override bool pushToTable(Brand brand)
        {

            // ��P�q�{�b�����a��W����
            all.NowPlayer.remove(brand);
            // ���ୱ�W
            all.PushToTable(brand);
            // �P�B���a
            chat.SendObject(all);

            // �Ƨǲ{�b�����a
            all.sortNowPlayer();
            // ��s�{�b���a�M�ୱ            
            //updatePlayer_Table();
            table.cleanImage();
            table.addImage();
            setInforamtion();
            //// �p�G���Oserver
            //if (!iAmServer)
            //    chat.SendObject(brand);
            if (iAmServer)
                // �ݬO�_���H�n �J �b �I �Y
                return check_chow_pong_kong_win(brand);
            else
                return false;
        }

        internal override void toUser(Brand brand, bool chow, bool pong, bool kong, bool darkkong, bool win)
        {
            if (iAmServer)
            {
                chat.SendObject(all);
                chat.SendObject(new CheckUser(chow, pong, kong, darkkong, win, false));
                chat.SendObject(brand);
                while (true)
                {
                    if (get_CheckUser)
                        break;
                }
                runCheckUser(brand);
            }
            else
                base.toUser(brand, chow, pong, kong, darkkong, win);
        }

        void runCheckUser(Brand brand)
        {
            if (checkuser.Chow)
                chow(brand);
            else if (checkuser.Pong)
                pong(brand);
            else if (checkuser.Kong)
                kong(brand);
            else if (checkuser.DarkKong)
                dark_kong(brand);
            else if (checkuser.Win)
                win(brand);
            else if (checkuser.Pass)
                pass(brand);
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

        //delegate void setInformation_delegate();

        internal override void setInforamtion()
        {
            this.information.setup(table,all);            
            this.information.updateInformation();
            //if (this.information.InvokeRequired)
            //    this.information.Invoke(new setInformation_delegate(showInformation));
            //else
            //    showInformation();
            //this.information.Show();
            
        }

        void showInformation()
        {
            this.information.Show();
        }

        bool myTurn
        {
            get
            {
                return all.State == table.place.Down;
            }
        }
        bool iAmServer
        {
            get
            {
                return chat.Mark == "Server";
            }
        }
    }
}
