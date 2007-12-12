using System;
using System.Collections.Generic;
using System.Text;
using Mahjong.Players;
using System.Collections;
using System.Drawing;

namespace Mahjong.Brands
{
    /// <summary>
    /// �P�u�t 
    /// </summary>
    public class BrandFactory
    {
        /// <summary>
        /// �ü�
        /// </summary>
        Random randomNumber;
        /// <summary>
        /// ���a
        /// </summary>
        BrandPlayer player;
        /// <summary>
        /// ��P���i��
        /// </summary>
        int countFlowerBrand;
        /// <summary>
        /// ��P���Ӽ�
        /// </summary>
        int pieceFlowerBrand;
        /// <summary>
        /// ���P���i��
        /// </summary>
        int countRopeBrand;
        /// <summary>
        /// ���P���Ӽ�
        /// </summary>
        int pieceRopeBrand;
        /// <summary>
        /// ���P���i��
        /// </summary>
        int countTubeBrand;
        /// <summary>
        /// ���P���Ӽ�
        /// </summary>
        int pieceTubeBrand;
        /// <summary>
        /// �P�ժ��i��
        /// </summary>
        int countTenThousandBrand;
        /// <summary>
        /// �P�ժ��Ӽ�
        /// </summary>
        int pieceTenThousandBrand;
        /// <summary>
        /// �r�P���i��
        /// </summary>
        int countWordBrand;
        /// <summary>
        /// �r�P���Ӽ�
        /// </summary>
        int pieceWordBrand;
        /// <summary>
        /// �üƪ��
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
        /// �إߵP��
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
        /// �Q�ζüƧ�P����
        /// </summary>
        public void randomBrands()
        {
            creatRandomTable();

            BrandPlayer tempplayer = new BrandPlayer();
            //�̷Ӷüƪ��v�@��P��J���a��
            for (int i = 0; i < this.player.getCount(); i++)
                tempplayer.add( this.player.getBrand( (int)randomTable[i] ) );
            this.player = tempplayer;
        }
        /// <summary>
        /// �إߪ�P
        /// </summary>
        private void creatFlowerBrand()
        {
            for (int j = 0; j < pieceFlowerBrand; j++)
                for (int i = 0; i < countFlowerBrand; i++)
                    this.player.add(new FlowerBrand(i + 1,(Image)FlowerbrandArray[i]));
        }
        /// <summary>
        /// �إ߯��P
        /// </summary>
        private void creatRopeBrand()
        {
            for (int j = 0; j < pieceRopeBrand; j++)
                for (int i = 0; i < countRopeBrand; i++)
                    this.player.add(new RopeBrand(i + 1,(Image)RopebrandArray[i]));
        }
        /// <summary>
        /// �إߵ��P
        /// </summary>
        private void creatTubeBrand()
        {
            for (int j = 0; j < pieceTubeBrand; j++)
                for (int i = 0; i < countTubeBrand; i++)
                    this.player.add(new TubeBrand(i + 1, (Image)TobebrandArray[i]));
        }
        /// <summary>
        /// �إ߸U�r�P
        /// </summary>
        private void creatTenThousandBrand()
        {
            for (int j = 0; j < pieceTenThousandBrand; j++)
                for (int i = 0; i < countTenThousandBrand; i++)
                    this.player.add(new TenThousandBrand(i + 1, (Image)TenthousandbrandArray[i]));
        }
        /// <summary>
        /// �إߦr�P
        /// </summary>
        private void creatWordBrand()
        {
            for (int j = 0; j < pieceWordBrand; j++)
                for (int i = 0; i < countWordBrand; i++)
                    this.player.add(new WordBrand(i + 1,(Image)WordbrandArray[i]));
        }
        /// <summary>
        /// ���նüƪ�
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
        /// �إ߶üƪ�
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
        /// �o��P�u�t�����a
        /// </summary>
        /// <returns>�Ǧ^���a</returns>
        public BrandPlayer getBrands()
        {
            return this.player;
        }
        /// <summary>
        /// ���ͤ����ƪ��ü�
        /// </summary>
        /// <param name="number">��l��</param>
        /// <returns>�����ƶüƭ�</returns>
        private int makeRandomNumber(int number)
        {
            for (int i = 0; i < randomTable.Count;i++ )
                if (randomTable.Contains(number))
                    number = randomNumber.Next(randomTable.Capacity);
            return number;
        }
        /// <summary>
        /// �إ߹Ϥ��}�C�C��
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
