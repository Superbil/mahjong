using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace Mahjong.Control
{
    class BTest
    {       
        
        public BTest()
        {
            //byte[] b = new byte[100];
            //Stream s = new s;
            //s.Write(b,0,10);
            

        }
        public static byte[] ReadFully(Stream stream)
        {
            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);                    
                }
            }
        }
    }
}
