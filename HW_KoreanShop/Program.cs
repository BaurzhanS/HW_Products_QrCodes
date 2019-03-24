using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW_KoreanShop.Repositories;
using HW_KoreanShop.Entities;
using QRCoder;
using System.IO;
using System.Drawing;
using HW_KoreanShop;

namespace HW_KoreanShop
{
    class Program
    {
        static void Main(string[] args)
        {
            QrCodeGeneratorService qs = new QrCodeGeneratorService();
            QrCodeGeoRepository qr = new QrCodeGeoRepository();
            //QrCodeRepository qr = new QrCodeRepository();
            //qs.InsertQrCodePurchaseInfo(2, 2);
            //QrCodeEntity entity = qr.Read(1);
            //List<QrCodeEntity> qrs = qr.ReadAll().ToList();

            //List<Bitmap> images = new List<Bitmap>();

            //int count = 0;

            //foreach (var item in qrs)
            //{
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        ms.Write(item.Content, 0, item.Content.Length);
            //        images.Add(new Bitmap(ms));
            //    }
            //}

            //foreach (var item in images)
            //{
            //    item.Save($@"D:\Академия шаг\ADO.NET\{count}.png");
            //    count++;
            //}
            //qs.InsertQrCodeGeolocation(1, "43.258377", "76.911575");
            //qs.InsertQrCodeGeolocation(1, "43.254656", "76.926423");

            //List<QrCodeGeoEntity> qrs = qr.ReadAll().ToList();

            //List<Bitmap> images = new List<Bitmap>();

            //int count = 0;

            //foreach (var item in qrs)
            //{
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        ms.Write(item.Content, 0, item.Content.Length);
            //        images.Add(new Bitmap(ms));
            //    }
            //}

            //foreach (var item in images)
            //{
            //    item.Save($@"D:\Академия шаг\ADO.NET\{count}.png");
            //    count++;
            //}

            //decimal d = CurrencyConverter.CurrencyConversion(120, "USD");
            //Console.WriteLine(d);

            //ProductRepository pr = new ProductRepository();
            //Product p = new Product();
            //p.ProductName = "Pen";
            //p.Cost = 120;
            //p.Currency = "USD";
            ////pr.Insert(p);
            //List<Product> ps = pr.ReadAll().ToList();
            //foreach (var item in ps)
            //{
            //    Console.WriteLine(item.ProductName);
            //    Console.WriteLine(item.Cost);
            //    Console.WriteLine(item.CostInTenge);
            //    Console.WriteLine(item.Currency);
            //}
           
        }
    }
}
