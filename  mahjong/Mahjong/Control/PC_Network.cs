using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Brands;
using System.Windows.Forms;
using Mahjong.Forms;

namespace Mahjong.Control
{
    public class PC_Network : ProgramControl
    {
        internal override void makeBrand(Brand brand)
        {
            MessageBox.Show("hello");
            //sendAll(all);
            //chat.send(this.all);
        }
        internal override void IamPlayer()
        {
            base.IamPlayer();
        }

    }
}
