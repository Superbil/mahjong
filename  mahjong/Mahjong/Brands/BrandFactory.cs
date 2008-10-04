using System;
using System.Text;
using System.Collections;
using System.Drawing;
using Mahjong.Players;
using Mahjong.Properties;
using System.IO;

namespace Mahjong.Brands
{
    /// <summary>
    /// 礟紅 
    /// </summary>
    public class BrandFactory
    {
        /// <summary>
        /// 睹计
        /// </summary>
        Random randomNumber;
        /// <summary>
        /// 產
        /// </summary>
        BrandPlayer player;
        /// <summary>
        /// 礟眎计
        /// </summary>
        int countFlowerBrand;
        /// <summary>
        /// 礟计
        /// </summary>
        int pieceFlowerBrand;
        /// <summary>
        /// 礟眎计
        /// </summary>
        int countRopeBrand;
        /// <summary>
        /// 礟计
        /// </summary>
        int pieceRopeBrand;
        /// <summary>
        /// 旦礟眎计
        /// </summary>
        int countTubeBrand;
        /// <summary>
        /// 旦礟计
        /// </summary>
        int pieceTubeBrand;
        /// <summary>
        /// 窾礟眎计
        /// </summary>
        int countTenThousandBrand;
        /// <summary>
        /// 窾礟计
        /// </summary>
        int pieceTenThousandBrand;
        /// <summary>
        /// 礟眎计
        /// </summary>
        int countWordBrand;
        /// <summary>
        /// 礟计
        /// </summary>
        int pieceWordBrand;
        /// <summary>
        /// 睹计
        /// </summary>
        ArrayList randomTable;
        ArrayList FlowerbrandArray;
        ArrayList RopebrandArray;
        ArrayList TenthousandbrandArray;
        ArrayList TobebrandArray;
        ArrayList WordbrandArray;
        /// <summary>
        /// 羆礟计
        /// </summary>
        int sumBrands;
        public BrandFactory()
        {
            this.player = new BrandPlayer();
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
            sumBrands = countFlowerBrand * pieceFlowerBrand +
                        countRopeBrand * pieceRopeBrand +
                        countTenThousandBrand * pieceTenThousandBrand +
                        countTubeBrand * pieceTubeBrand +
                        countWordBrand * pieceWordBrand;
        }
        /// <summary>
        /// ミ礟舱
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
        /// ノ睹计р礟ゴ睹
        /// </summary>
        public void randomBrands()
        {
            creatRandomTable();

            BrandPlayer tempplayer = new BrandPlayer();
            //ㄌ酚睹计硋р礟產い
            for (int i = 0; i < this.player.getCount(); i++)
                tempplayer.add( this.player.getBrand( (int)randomTable[i] ) );
            this.player = tempplayer;
        }
        /// <summary>
        /// ミ礟
        /// </summary>
        private void creatFlowerBrand()
        {
            for (int j = 0; j < pieceFlowerBrand; j++)
                for (int i = 0; i < countFlowerBrand; i++)
                    this.player.add(new FlowerBrand(i + 1,(Image)FlowerbrandArray[i]));
        }
        /// <summary>
        /// ミ礟
        /// </summary>
        private void creatRopeBrand()
        {
            for (int j = 0; j < pieceRopeBrand; j++)
                for (int i = 0; i < countRopeBrand; i++)
                    this.player.add(new RopeBrand(i + 1,(Image)RopebrandArray[i]));
        }
        /// <summary>
        /// ミ旦礟
        /// </summary>
        private void creatTubeBrand()
        {
            for (int j = 0; j < pieceTubeBrand; j++)
                for (int i = 0; i < countTubeBrand; i++)
                    this.player.add(new TubeBrand(i + 1, (Image)TobebrandArray[i]));
        }
        /// <summary>
        /// ミ窾礟
        /// </summary>
        private void creatTenThousandBrand()
        {
            for (int j = 0; j < pieceTenThousandBrand; j++)
                for (int i = 0; i < countTenThousandBrand; i++)
                    this.player.add(new TenThousandBrand(i + 1, (Image)TenthousandbrandArray[i]));
        }
        /// <summary>
        /// ミ礟
        /// </summary>
        private void creatWordBrand()
        {
            for (int j = 0; j < pieceWordBrand; j++)
                for (int i = 0; i < countWordBrand; i++)
                    this.player.add(new WordBrand(i + 1,(Image)WordbrandArray[i]));
        }
        /// <summary>
        /// 代刚睹计
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
        /// ミ睹计
        /// </summary>
        private void creatRandomTable()
        {
            randomNumber = new Random((int)System.DateTime.Now.Ticks%int.MaxValue);
            randomTable = new ArrayList();
            randomTable.Capacity = sumBrands;         

            for (int i = 0; i < randomTable.Capacity; i++)
                randomTable.Add(makeRandomNumber(randomNumber.Next(randomTable.Capacity))) ;
        }
        /// <summary>
        /// 眔礟紅產
        /// </summary>
        /// <returns>肚產</returns>
        public BrandPlayer getBrands()
        {
            return this.player;
        }
        /// <summary>
        /// 玻ネぃ狡睹计
        /// </summary>
        /// <param name="number">﹍</param>
        /// <returns>ぃ狡睹计</returns>
        private int makeRandomNumber(int number)
        {
            for (int i = 0; i < randomTable.Count;i++ )
                if (randomTable.Contains(number))
                    number = randomNumber.Next(randomTable.Capacity);
            return number;
        }
        /// <summary>
        /// ミ瓜皚
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
        /// <summary>
        /// 礟羆计
        /// </summary>
        public int SumBrands
        {
            get
            {
                return sumBrands;
            }
        }
        void creatImageArray_Flower()
        {
            creatImage(FlowerbrandArray, Resources.flower1);
            creatImage(FlowerbrandArray, Resources.flower2);
            creatImage(FlowerbrandArray, Resources.flower3);
            creatImage(FlowerbrandArray, Resources.flower4);
            creatImage(FlowerbrandArray, Resources.flower5);
            creatImage(FlowerbrandArray, Resources.flower6);
            creatImage(FlowerbrandArray, Resources.flower7);
            creatImage(FlowerbrandArray, Resources.flower8);
        }
        void creatImageArray_Rope()
        {
            creatImage(RopebrandArray, Resources.rope1);
            creatImage(RopebrandArray, Resources.rope2);
            creatImage(RopebrandArray, Resources.rope3);
            creatImage(RopebrandArray, Resources.rope4);
            creatImage(RopebrandArray, Resources.rope5);
            creatImage(RopebrandArray, Resources.rope6);
            creatImage(RopebrandArray, Resources.rope7);
            creatImage(RopebrandArray, Resources.rope8);
            creatImage(RopebrandArray, Resources.rope9);
        }
        void creatImageArray_TenThousand()
        {
            creatImage(TenthousandbrandArray, Resources.ten1);
            creatImage(TenthousandbrandArray, Resources.ten2);
            creatImage(TenthousandbrandArray, Resources.ten3);
            creatImage(TenthousandbrandArray, Resources.ten4);
            creatImage(TenthousandbrandArray, Resources.ten5);
            creatImage(TenthousandbrandArray, Resources.ten6);
            creatImage(TenthousandbrandArray, Resources.ten7);
            creatImage(TenthousandbrandArray, Resources.ten8);
            creatImage(TenthousandbrandArray, Resources.ten9);
        }
        void creatImageArray_Tobe()
        {
            creatImage(TobebrandArray, Resources.tobe1);
            creatImage(TobebrandArray, Resources.tobe2);
            creatImage(TobebrandArray, Resources.tobe3);
            creatImage(TobebrandArray, Resources.tobe4);
            creatImage(TobebrandArray, Resources.tobe5);
            creatImage(TobebrandArray, Resources.tobe6);
            creatImage(TobebrandArray, Resources.tobe7);
            creatImage(TobebrandArray, Resources.tobe8);
            creatImage(TobebrandArray, Resources.tobe9);
        }
        void creatImageArray_Word()
        {
            creatImage(WordbrandArray, Resources.word1);
            creatImage(WordbrandArray, Resources.word2);
            creatImage(WordbrandArray, Resources.word3);
            creatImage(WordbrandArray, Resources.word4);
            creatImage(WordbrandArray, Resources.word5);
            creatImage(WordbrandArray, Resources.word6);
            creatImage(WordbrandArray, Resources.word7);
        }
        void creatImage(ArrayList array, Image image)
        {
            array.Add(image);
        }       
    }
}
