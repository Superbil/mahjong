using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using Mahjong.Properties;
using Mahjong.Brands;

namespace Mahjong.Control
{
    static class ResourcesTool
    {
        static public Image getImage(string playername)
        {
            switch (playername)
            {
                case "林宗水":
                    return Resources.m1;
                case "吳昆賢":

                    return Resources.m2;
                case "陳怡欣":

                    return Resources.g1;
                case "張惠娟":
                    return Resources.g2;
                
                default:
                    return null;
            }
        }
       
        static public Image getImage(Brand brand)
        {
            switch (brand.getClass())
            {
                case "萬":
                    switch (brand.getNumber())
                    {
                        case 1:
                            return Resources.ten1;

                        case 2:
                            return Resources.ten2;

                        case 3:
                            return Resources.ten3;

                        case 4:
                            return Resources.ten4;

                        case 5:
                            return Resources.ten5;

                        case 6:
                            return Resources.ten6;

                        case 7:
                            return Resources.ten7;

                        case 8:
                            return Resources.ten8;

                        case 9:
                            return Resources.ten9;
                        default:
                            return null;
                    }

                case "索":
                    switch (brand.getNumber())
                    {
                        case 1:
                            return Resources.rope1;

                        case 2:
                            return Resources.rope2;

                        case 3:
                            return Resources.rope3;

                        case 4:
                            return Resources.rope4;

                        case 5:
                            return Resources.rope5;

                        case 6:
                            return Resources.rope6;

                        case 7:
                            return Resources.rope7;

                        case 8:
                            return Resources.rope8;

                        case 9:
                            return Resources.rope9;
                        default:
                            return null;
                    }

                case "筒":
                    switch (brand.getNumber())
                    {
                        case 1:
                            return Resources.tobe1;

                        case 2:
                            return Resources.tobe2;

                        case 3:
                            return Resources.tobe3;

                        case 4:
                            return Resources.tobe4;

                        case 5:
                            return Resources.tobe5;

                        case 6:
                            return Resources.tobe6;

                        case 7:
                            return Resources.tobe7;

                        case 8:
                            return Resources.tobe8;

                        case 9:
                            return Resources.tobe9;
                        default:
                            return null;
                    }

                case "字":
                    switch (brand.getNumber())
                    {
                        case 1:
                            return Resources.word1;

                        case 2:
                            return Resources.word2;

                        case 3:
                            return Resources.word3;

                        case 4:
                            return Resources.word4;

                        case 5:
                            return Resources.word5;

                        case 6:
                            return Resources.word6;

                        case 7:
                            return Resources.word7;
                        default:
                            return null;
                    }
                case "花":
                    switch (brand.getNumber())
                    {
                        case 1:
                            return Resources.flower1;

                        case 2:
                            return Resources.flower2;

                        case 3:
                            return Resources.flower3;

                        case 4:
                            return Resources.flower4;

                        case 5:
                            return Resources.flower5;

                        case 6:
                            return Resources.flower6;

                        case 7:
                            return Resources.flower7;
                        case 8:
                            return Resources.flower8;
                        default:
                            return null;
                    }

                default:
                    return null;
            }
        }
        static public UnmanagedMemoryStream getSound(Brand brand)
        {
            switch (brand.getClass())
            {
                case "萬":
                    switch (brand.getNumber())
                    {
                        case 1:
                            return Resources.ten1s;

                        case 2:
                            return Resources.ten2s;

                        case 3:
                            return Resources.ten3s;

                        case 4:
                            return Resources.ten4s;

                        case 5:
                            return Resources.ten5s;

                        case 6:
                            return Resources.ten6s;

                        case 7:
                            return Resources.ten7s;

                        case 8:
                            return Resources.ten8s;

                        case 9:
                            return Resources.ten9s;
                        default:
                            return null;
                    }
                    
                case "索":
                    switch (brand.getNumber())
                    {
                        case 1:
                            return Resources.rope1s;

                        case 2:
                            return Resources.rope2s;

                        case 3:
                            return Resources.rope3s;

                        case 4:
                            return Resources.rope4s;

                        case 5:
                            return Resources.rope5s;

                        case 6:
                            return Resources.rope6s;

                        case 7:
                            return Resources.rope7s;

                        case 8:
                            return Resources.rope8s;

                        case 9:
                            return Resources.rope9s;
                        default:
                            return null;
                    }
                    
                case "筒":
                    switch (brand.getNumber())
                    {
                        case 1:
                            return Resources.tobe1s;

                        case 2:
                            return Resources.tobe2s;

                        case 3:
                            return Resources.tobe3s;

                        case 4:
                            return Resources.tobe4s;

                        case 5:
                            return Resources.tobe5s;

                        case 6:
                            return Resources.tobe6s;

                        case 7:
                            return Resources.tobe7s;

                        case 8:
                            return Resources.tobe8s;

                        case 9:
                            return Resources.tobe9s;
                        default:
                            return null;
                    }
                    
                case "字":
                    switch (brand.getNumber())
                    {
                        case 1:
                            return Resources.word1s;

                        case 2:
                            return Resources.word2s;

                        case 3:
                            return Resources.word3s;

                        case 4:
                            return Resources.word4s;

                        case 5:
                            return Resources.word5s;

                        case 6:
                            return Resources.word6s;

                        case 7:
                            return Resources.word7s;
                        default:
                            return null;
                    }
                    
                default:
                    return null;
            }


        }
    }
}
