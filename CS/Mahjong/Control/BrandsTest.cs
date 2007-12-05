using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Mahjong.Brands;
using Mahjong.Players;
using Mahjong.AIs;
using System.Drawing;

namespace Mahjong.Control
{
    class BrandsTest
    {
        public BrandsTest()
        {
            BrandPlayer a = new BrandPlayer();

            BrandFactory x = new BrandFactory();
            x.createBrands();
            x.randomBrands();

            //x.PrintRadomTable();
            a = x.getBrands();

            Iterator ai;
            ai = a.creatIterator(10);
            print(ai);

            ai = a.creatIterator(3);
            a=removefromplayer(ai,a);

            ai = a.creatIterator(10);
            print(ai);

            
            //SimpleAI sa = new SimpleAI(a);
            //sa.getReadyBrand();
            //Console.WriteLine();
            //Console.Write("{0},{1}\n", sa.getReadyBrand().getClass(),sa.getReadyBrand().getNumber());

            //chackBrands();
            //chackArrayList();

            Console.WriteLine("Over");
            x.creatImageArray();

            //Console.ReadLine();
            //Image aa = Mahjong.BrandsPicture.a1;
            
        }
        BrandPlayer removefromplayer(Iterator iterator,BrandPlayer re)
        {
            while(iterator.hasNext())
            {
                Brand brand = (Brand)iterator.next();
                //re.remove(brand);
                Console.WriteLine(">>{0}",re.remove(brand));
            }
            return re;
        }
        private void print(Iterator iterator)
        {
            Console.WriteLine();
            //TeamBrands t;
            while (iterator.hasNext())
            {
                Brand brand = (Brand)iterator.next();
                //if (brand.getClass==t.getClass)
                //    Console.WriteLine("==Get a TeamBrands==");
                Console.Write("{0},{1}\t", brand.getNumber(),brand.getClass());
            }
        }
        private void chackBrands()
        {            
            FlowerBrand f1 = new FlowerBrand(1);
            FlowerBrand f2 = new FlowerBrand(1);
            
            Console.WriteLine();
            //Console.WriteLine(f1.Equals(f2));
            Console.WriteLine(chackBrandClass(f1,f2));
            Console.WriteLine(chackBrandNumber(f1,f2));
            Console.WriteLine(f1.getNumber() == f2.getNumber());
        }
        private bool chackBrandNumber(Brand b1,Brand b2)
        {
            if (b1.getNumber() == b2.getNumber())
                return true;
            else
                return false;
        }
        private bool chackBrandClass(Brand b1, Brand b2)
        {
            if (b1.getClass() == b2.getClass())
                return true;
            else
            {
                return false;
            }
        }
        private void chackArrayList()
        {
            ArrayList a = new ArrayList();
            a.Add(5);
            a.Add(4);
            a.Add(3);
            a.Add(2);
            a.Add(7);
            Console.WriteLine(a.Count);
            for (int i = 0; i < a.Count;i++ )
                Console.WriteLine(a[i]);            
        }
    }
}
