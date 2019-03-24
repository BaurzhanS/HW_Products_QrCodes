using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using HW_KoreanShop.Entities;
using HW_KoreanShop.Repositories;
using QRCoder;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using static QRCoder.PayloadGenerator;

namespace HW_KoreanShop
{
    public class QrCodeGeneratorService
    {
        private readonly ProductRepository productRepository;
        private readonly UserRepository userRepository;
        private readonly QrCodeRepository qrCodeRepository;
        private readonly QrCodeGeoRepository qrCodeGeoRepository;

        public QrCodeGeneratorService()
        {
            productRepository = new ProductRepository();
            userRepository = new UserRepository();
            qrCodeRepository = new QrCodeRepository();
            qrCodeGeoRepository = new QrCodeGeoRepository();
        }

        public void InsertQrCodePurchaseInfo(int userId, int productId)
        {
            var product = productRepository.Read(productId);
            var user = userRepository.Read(userId);
            string purchaseInfo = $"At {DateTime.Now.ToShortTimeString()} {user.UserName} " +
                $"purchased {product.ProductName}";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(purchaseInfo, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            using (MemoryStream ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, ImageFormat.Png);
                qrCodeRepository.Insert(new QrCodeEntity()
                {
                    UserId = userId,
                    QrCodeType = QrCodeType.TextEncodedQrCode,
                    Content = ms.ToArray()
                });
            }
        }

        public void InsertQrCodeGeolocation(int userId,string latitude, string longtitude)
        {
            Geolocation generator = new Geolocation(latitude, longtitude);
            string location = generator.ToString();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(location, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);         

            using (MemoryStream ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, ImageFormat.Png);
                qrCodeGeoRepository.Insert(new QrCodeGeoEntity()
                {
                    UserId = userId,
                    QrCodeType = QrCodeType.LocationEncodedQrCode,
                    Content = ms.ToArray()
                });
            }
        }


    }
}
