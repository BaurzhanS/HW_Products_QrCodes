using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW_KoreanShop.Entities;


namespace HW_KoreanShop.Entities
{
    public class QrCodeGeoEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] Content { get; set; }
        public QrCodeType QrCodeType { get; set; }
    }
}
