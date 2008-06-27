using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Brands;
using System.Windows.Forms;

namespace Mahjong.Control
{
    public class PC_Network : ProgramControl
    {
        public PC_Network()
        {
            
        }
        internal override void makeBrand(Brand brand)
        {
            MessageBox.Show("hello");
        }

    }
}
