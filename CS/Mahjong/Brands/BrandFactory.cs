using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using System.Collections;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// 牌工廠 
    /// </summary>
    public class BrandFactory
    {
        /// <summary>
        /// 亂數
        /// </summary>
        Random randomNumber;
        /// <summary>
        /// 玩家
        /// </summary>
        BrandPlayer player;
        /// <summary>
        /// 花牌的張數
        /// </summary>
        int countFlowerBrand;
        /// <summary>
        /// 花牌的個數
        /// </summary>
        int pieceFlowerBrand;
        /// <summary>
        /// 索牌的張數
        /// </summary>
        int countRopeBrand;
        /// <summary>
        /// 索牌的個數
        /// </summary>
        int pieceRopeBrand;
        /// <summary>
        /// 筒牌的張數
        /// </summary>
        int countTubeBrand;
        /// <summary>
        /// 筒牌的個數
        /// </summary>
        int pieceTubeBrand;
        /// <summary>
        /// 牌組的張數
        /// </summary>
        int countTenThousandBrand;
        /// <summary>
        /// 牌組的個數
        /// </summary>
        int pieceTenThousandBrand;
        /// <summary>
        /// 字牌的張數
        /// </summary>
        int countWordBrand;
        /// <summary>
        /// 字牌的個數
        /// </summary>
        int pieceWordBrand;
        /// <summary>
        /// 亂數表格
        /// </summary>
        ArrayList randomTable;
        ArrayList FlowerbrandArray;
        ArrayList RopebrandArray;
        ArrayList TenthousandbrandArray;
        ArrayList TobebrandArray;
        ArrayList WordbrandArray;
        public BrandFactory()
        {
            player = new BrandPlayer();
            countFlowerBrand = 8;
            pieceFlowerBrand = 1;
            countRopeBrand = 9;
            pieceRopeBrand = 4;
            countTubeBrand = 9;
            pieceTubeBrand = 4;
            countTenThousandBrand = 9;
            pieceTenThousandBrand = 4;
            countWordBrand = 7;
            pieceWordBrand = 4;
            creatImageArray();
        }
        /// <summary>
        /// 建立牌組
        /// </summary>
        public void createBrands()
        {
            creatFlowerBrand();
            creatRopeBrand();
            creatTenThousandBrand();
            creatTubeBrand();
            creatWordBrand();
        }
        /// <summary>
        /// 利用亂數把牌打亂
        /// </summary>
        public void randomBrands()
        {
            creatRandomTable();

            BrandPlayer tempplayer = new BrandPlayer();
            //依照亂數表格逐一把牌放入玩家中
            for (int i = 0; i < this.player.getCount(); i++)
                tempplayer.add( this.player.getBrand( (int)randomTable[i] ) );
            this.player = tempplayer;
        }
        /// <summary>
        /// 建立花牌
        /// </summary>
        private void creatFlowerBrand()
        {
            for (int j = 0; j < pieceFlowerBrand; j++)
                for (int i = 0; i < countFlowerBrand; i++)
                    this.player.add(new FlowerBrand(i + 1,(Image)FlowerbrandArray[i]));
        }
        /// <summary>
        /// 建立索牌
        /// </summary>
        private void creatRopeBrand()
        {
            for (int j = 0; j < pieceRopeBrand; j++)
                for (int i = 0; i < countRopeBrand; i++)
                    this.player.add(new RopeBrand(i + 1,(Image)RopebrandArray[i]));
        }
        /// <summary>
        /// 建立筒牌
        /// </summary>
        private void creatTubeBrand()
        {
            for (int j = 0; j < pieceTubeBrand; j++)
                for (int i = 0; i < countTubeBrand; i++)
                    this.player.add(new TubeBrand(i + 1, (Image)TobebrandArray[i]));
        }
        /// <summary>
        /// 建立萬字牌
        /// </summary>
        private void creatTenThousandBrand()
        {
            for (int j = 0; j < pieceTenThousandBrand; j++)
                for (int i = 0; i < countTenThousandBrand; i++)
                    this.player.add(new TenThousandBrand(i + 1, (Image)TenthousandbrandArray[i]));
        }
        /// <summary>
        /// 建立字牌
        /// </summary>
        private void creatWordBrand()
        {
            for (int j = 0; j < pieceWordBrand; j++)
                for (int i = 0; i < countWordBrand; i++)
                    this.player.add(new WordBrand(i + 1,(Image)WordbrandArray[i]));
        }
        /// <summary>
        /// 測試亂數表
        /// </summary>
        public void PrintRadomTable()
        {
            creatRandomTable();
            Console.WriteLine("Make Random Table");
            for (int i = 0; i < randomTable.Count; i++)
                Console.Write("[{0}]\t", (int)randomTable[i]);
            randomTable.Sort();
            Console.WriteLine("\nSort Random Table");
            for (int i = 0; i < randomTable.Count; i++)
                Console.Write("[{0}]\t", (int)randomTable[i]);
        }
        /// <summary>
        /// 建立亂數表
        /// </summary>
        private void creatRandomTable()
        {
            randomNumber = new Random(System.DateTime.Now.Millisecond);
            randomTable = new ArrayList();
            randomTable.Capacity = countFlowerBrand * pieceFlowerBrand +
                                  countRopeBrand * pieceRopeBrand +
                                  countTenThousandBrand * pieceTenThousandBrand +
                                  countTubeBrand * pieceTubeBrand +
                                  countWordBrand * pieceWordBrand;         

            for (int i = 0; i < randomTable.Capacity; i++)
                randomTable.Add(makeRandomNumber(randomNumber.Next(randomTable.Capacity))) ;
        }
        /// <summary>
        /// 得到牌工廠的玩家
        /// </summary>
        /// <returns>傳回玩家</returns>
        public BrandPlayer getBrands()
        {
            return this.player;
        }
        /// <summary>
        /// 產生不重複的亂數
        /// </summary>
        /// <param name="number">初始值</param>
        /// <returns>不重複亂數值</returns>
        private int makeRandomNumber(int number)
        {
            for (int i = 0; i < randomTable.Count;i++ )
                if (randomTable.Contains(number))
                    number = randomNumber.Next(randomTable.Capacity);
            return number;
        }
        /// <summary>
        /// 建立圖片陣列列表
        /// </summary>
        public void creatImageArray()
        {
            FlowerbrandArray = new ArrayList();
            creatImageArray_Flower();
            RopebrandArray = new ArrayList();
            creatImageArray_Rope();
            TenthousandbrandArray = new ArrayList();
            creatImageArray_TenThousand();
            TobebrandArray = new ArrayList();
            creatImageArray_Tobe();
            WordbrandArray = new ArrayList();
            creatImageArray_Word();
        }
        void creatImageArray_Flower()
        {
            FlowerbrandArray.Add(new Bitmap(Mahjong.Properties.Resources.flower1));
            FlowerbrandArray.Add(Mahjong.Properties.Resources.flower2);
            FlowerbrandArray.Add(Mahjong.Properties.Resources.flower3);
            FlowerbrandArray.Add(Mahjong.Properties.Resources.flower4);
            FlowerbrandArray.Add(Mahjong.Properties.Resources.flower5);
            FlowerbrandArray.Add(Mahjong.Properties.Resources.flower6);
            FlowerbrandArray.Add(Mahjong.Properties.Resources.flower7);
            FlowerbrandArray.Add(Mahjong.Properties.Resources.flower8);
        }
        void creatImageArray_Rope()
        {
            RopebrandArray.Add(Mahjong.Properties.Resources.rope1);
            RopebrandArray.Add(Mahjong.Properties.Resources.rope2);
            RopebrandArray.Add(Mahjong.Properties.Resources.rope3);
            RopebrandArray.Add(Mahjong.Properties.Resources.rope4);
            RopebrandArray.Add(Mahjong.Properties.Resources.rope5);
            RopebrandArray.Add(Mahjong.Properties.Resources.rope6);
            RopebrandArray.Add(Mahjong.Properties.Resources.rope7);
            RopebrandArray.Add(Mahjong.Properties.Resources.rope8);
            RopebrandArray.Add(Mahjong.Properties.Resources.rope9);
        }
        void creatImageArray_TenThousand()
        {
            TenthousandbrandArray.Add(Mahjong.Properties.Resources.ten1);
            TenthousandbrandArray.Add(Mahjong.Properties.Resources.ten2);
            TenthousandbrandArray.Add(Mahjong.Properties.Resources.ten3);
            TenthousandbrandArray.Add(Mahjong.Properties.Resources.ten4);
            TenthousandbrandArray.Add(Mahjong.Properties.Resources.ten5);
            TenthousandbrandArray.Add(Mahjong.Properties.Resources.ten6);
            TenthousandbrandArray.Add(Mahjong.Properties.Resources.ten7);
            TenthousandbrandArray.Add(Mahjong.Properties.Resources.ten8);
            TenthousandbrandArray.Add(Mahjong.Properties.Resources.ten9);
        }
        void creatImageArray_Tobe()
        {
            TobebrandArray.Add(Mahjong.Properties.Resources.tobe1);
            TobebrandArray.Add(Mahjong.Properties.Resources.tobe2);
            TobebrandArray.Add(Mahjong.Properties.Resources.tobe3);
            TobebrandArray.Add(Mahjong.Properties.Resources.tobe4);
            TobebrandArray.Add(Mahjong.Properties.Resources.tobe5);
            TobebrandArray.Add(Mahjong.Properties.Resources.tobe6);
            TobebrandArray.Add(Mahjong.Properties.Resources.tobe7);
            TobebrandArray.Add(Mahjong.Properties.Resources.tobe8);
            TobebrandArray.Add(Mahjong.Properties.Resources.tobe9);
        }
        void creatImageArray_Word()
        {
            WordbrandArray.Add(Mahjong.Properties.Resources.word1);
            WordbrandArray.Add(Mahjong.Properties.Resources.word2);
            WordbrandArray.Add(Mahjong.Properties.Resources.word3);
            WordbrandArray.Add(Mahjong.Properties.Resources.word4);
            WordbrandArray.Add(Mahjong.Properties.Resources.word5);
            WordbrandArray.Add(Mahjong.Properties.Resources.word6);
            WordbrandArray.Add(Mahjong.Properties.Resources.word7);
        }
        void creatImage(ArrayList array, string str)
        {
            array.Add(new Bitmap(str));
        }
    }
}
