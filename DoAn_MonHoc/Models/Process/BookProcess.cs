using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn_MonHoc.Models.Process
{
    public class BookProcess
    {
        //Khởi tạo biến dữ liệu : db
        NHASACHEntities db = null;

        //constructor :  khởi tạo đối tượng
        public BookProcess()
        {
            db = new NHASACHEntities();
        }

        /// <summary>
        /// lay 8 cuon sach moi
        /// </summary>
        /// <param name="count">int</param>
        /// <returns>List</returns>
        public List<THONGTINSACH> NewDateBook()
        {
            return db.THONGTINSACHes.Take(8).OrderBy(x => x.MaLoai).ToList();
        }
       
        /// <summary>
        /// lay 4  cuon sach ban chay
        /// </summary>
        /// <param name="count">int</param>
        /// <returns>List</returns>
        public List<THONGTINSACH> TakeBook()
        {
            return db.THONGTINSACHes.Take(4).OrderByDescending(x => x.MaSach).ToList();
        }
        /// <summary>
        /// lay 4  cuon sach lien quan toi ma loai duoc truyen vao
        /// </summary>
        /// <param name="count">int</param>
        /// <returns>List</returns>
        public List<THONGTINSACH> SachLienQuan(int LoaiSach)
        {
            return db.THONGTINSACHes.Where(x => x.MaLoai == LoaiSach).ToList();
        }
        /// <summary>
        /// hàm xuất danh sách thể loại
        /// </summary>
        /// <returns></returns>
        public List<LOAISACH> ListLoaiSach()
        {
            return db.LOAISACHes.OrderBy(x => x.MaLoai).ToList();
        }
        /// <summary>
        /// hàm xuất danh sách NXB
        /// </summary>
        /// <returns></returns>
        public List<NHAXUATBAN> ListNXB()
        {
            return db.NHAXUATBANs.OrderBy(x => x.MaNXB).ToList();
        }
        /// <summary>
        /// Xem tất cả cuốn sách
        /// </summary>
        /// <returns>List</returns>
        public List<THONGTINSACH> ShowAllBook()
        {
            return db.THONGTINSACHes.OrderByDescending(x => x.MaSach).ToList();
        }
        /// <summary>
        /// hàm lấy mã thể loại
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>TheLoai</returns>
        public LOAISACH LaymaloaiCD(int maCD)
        {
            return db.LOAISACHes.Find(maCD);
        }
        /// <summary>
        /// lọc sách theo chủ đề
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>List</returns>
        public List<THONGTINSACH> SachtheoCD(int maCD)
        {
                 
            return db.THONGTINSACHes.Where(x => x.MaLoai == maCD).ToList();
        }
        /// <summary>
        /// hàm lấy mã thể loại
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>TheLoai</returns>
        public NHAXUATBAN LaymaloaiNXB(int maNXB)
        {
            return db.NHAXUATBANs.Find(maNXB);
        }
        /// <summary>
        /// lọc sách theo chủ đề
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>List</returns>
        public List<THONGTINSACH> SachtheoNXB(int maNXB)
        {

            return db.THONGTINSACHes.Where(x => x.MaNXB == maNXB).ToList();
        }
        /// <summary>
        /// hàm tìm kiếm tên sách
        /// </summary>
        /// <param name="key">string</param>
        /// <returns>List</returns>
        public List<THONGTINSACH> Search(string txt_Search)
        {
            return db.THONGTINSACHes.Where(x => x.TenSach.Contains(txt_Search)).OrderBy(x => x.TenSach).ToList();
        }

        /// <summary>
        /// hàm lấy mã sách
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>Sach</returns>
        public THONGTINSACH GetIdBook(int id)
        {
            return db.THONGTINSACHes.Find(id);
        }
    }
}