using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn_MonHoc.Models
{
    public class GioHang
    {
        NHASACHEntities db = new NHASACHEntities();
       
        public THONGTINSACH sach { get; set; }
        public int iSoLuong{ get; set; }
        public double? iThanhTien
        {
            get { 
                if(sach.GiamGia<=0)
                {
                    return iSoLuong * sach.GiaSach; 
                }
                else
                {
                    return iSoLuong * (sach.GiaSach-(sach.GiaSach*sach.GiamGia)); 
                }
            }
        }
    }
}