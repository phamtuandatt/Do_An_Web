﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn_MonHoc.Models.Process
{
    public class GioHangProcess
    {
          NHASACHEntities db = null;

        //constructor :  khởi tạo đối tượng
          public GioHangProcess()
        {
            db = new NHASACHEntities();
        }
        /// <summary>
        /// hàm lấy mã sách
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>Sach</returns>
        public THONGTINSACH LayMaSach(int id)
        {
            return db.THONGTINSACHes.Find(id);
        }

        /// <summary>
        /// hàm lấy mã đơn đặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PHIEUDATHANG layMaDDH(int id)
        {
            return db.PHIEUDATHANGs.Find(id);
        }

        /// <summary>
        /// hàm xuất danh sách đơn đặt hàng
        /// </summary>
        /// <returns>List</returns>
        public List<PHIEUDATHANG> DanhSachDDH()
        {
            return db.PHIEUDATHANGs.OrderBy(x => x.MaPhieuDH).ToList();
        }

        /// <summary>
        /// hàm thêm đơn hàng
        /// </summary>
        /// <param name="order">DonDatHang</param>
        /// <returns>int</returns>
        public int InsertDDH(PHIEUDATHANG order)
        {
            db.PHIEUDATHANGs.Add(order);
            db.SaveChanges();
            return order.MaPhieuDH;
        }

        /// hàm lấy mã chi tiết đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CT_PHIEUDATHANG GetIdCT_DDH(int id)
        {
            return db.CT_PHIEUDATHANG.Find(id);
        }

        /// <summary>
        /// hàm thêm sản phẩm vào đơn đặt hàng
        /// </summary>
        /// <param name="detail">ChiTietDDH</param>
        /// <returns>bool</returns>
        public bool InsertCT_DDH(CT_PHIEUDATHANG detail)
        {
            try
            {
                db.CT_PHIEUDATHANG.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
    }
}