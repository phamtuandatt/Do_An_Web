using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn_MonHoc.Models.Process
{
    public class AdminProcess
    {

        ///HÀM QUẢN LÝ CUA ADMIN
        ///
        //Khởi tạo biến dữ liệu : db
        NHASACHEntities db = null;

        //constructor :  khởi tạo đối tượng
        public AdminProcess()
        {
            db = new NHASACHEntities();
        }

        /// <summary>
        /// Hàm đăng nhập
        /// </summary>
        /// <param name="username">string</param>
        /// <param name="password">string</param>
        /// <returns>int</returns>
        public int Login(string username, string password)
        {
            var result = db.NHANVIENs.SingleOrDefault(x => x.TenDN == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.MatKhau == password)
                {

                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }

        //Xử Lý Thông Tin Sách
        #region Xử Lý Thông Tin Sách
        /// <summary>
        /// hàm lấy mã sách
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>Sach</returns>
        public THONGTINSACH GetIdBook(int id)
        {
            return db.THONGTINSACHes.Find(id);
        }

        //Books : sách

        /// <summary>
        /// hàm xuất danh sách Sách
        /// </summary>
        /// <returns>List</returns>
        public List<THONGTINSACH> Ad_ThongTinSach()
        {
            return db.THONGTINSACHes.OrderBy(x => x.MaSach).ToList();
        }

        /// <summary>
        /// hàm xóa 1 cuốn sách
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>bool</returns>
        public bool DeleteBook(int id)
        {
            try
            {
                var sach = db.THONGTINSACHes.SingleOrDefault(x => x.MaSach == id);
                db.THONGTINSACHes.Remove(sach);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// hàm cập nhật sách
        /// </summary>
        /// <param name="entity">Sách</param>
        /// <returns>int</returns>
        public int UpdateBook(THONGTINSACH entity)
        {
            try
            {
                var sach = db.THONGTINSACHes.Find(entity.MaSach);
                sach.MaLoai = entity.MaLoai;
                sach.MaNXB = entity.MaNXB;
                sach.MaTG = entity.MaTG;
                sach.TenSach = entity.TenSach;
                sach.GiaSach = entity.GiaSach;
                sach.MoTa = entity.MoTa;
                sach.HinhAnh = entity.HinhAnh;
                sach.GiamGia = entity.GiamGia;
                sach.SLTon = entity.SLTon;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// hàm thêm sách
        /// </summary>
        /// <param name="entity">Sach</param>
        /// <returns>int</returns>
        public int Insertbook(THONGTINSACH entity)
        {
            db.THONGTINSACHes.Add(entity);
            db.SaveChanges();
            return entity.MaSach;
        }

        #endregion

        //Liên hệ từ khách hàng

        #region phản hồi khách hàng
        /// <summary>
        /// hàm lấy mã liên hệ
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>LienHe</returns>
        public LIENHE GetIdContact(int id)
        {
            return db.LIENHEs.Find(id);
        }

        /// <summary>
        /// hàm lấy danh sách những phản hồi từ khách hàng
        /// </summary>
        /// <returns>List</returns>
        public List<LIENHE> ShowListContact()
        {
            return db.LIENHEs.OrderBy(x => x.MaLH).ToList();
        }

        /// <summary>
        /// hàm xóa thông tin phản hồi khách hàng
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>bool</returns>
        public bool deleteContact(int id)
        {
            try
            {
                var contact = db.LIENHEs.Find(id);
                db.LIENHEs.Remove(contact);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion


        //Quản lý người dùng
        #region quản lý người dùng
        /// <summary>
        /// Hàm lấy mã khách hàng tham quan
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>KhachHang</returns>
        public KHACHHANG GetIdKH(int id)
        {
            return db.KHACHHANGs.Find(id);
        }
        /// <summary>
        /// hàm xuất danh sách người dùng
        /// </summary>
        /// <returns>List</returns>
        public List<KHACHHANG> ListUser()
        {
            return db.KHACHHANGs.OrderBy(x => x.MaKH).ToList();
        }

        /// <summary>
        /// hàm xóa người dùng
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>bool</returns>
        public bool DeleteUser(int id)
        {
            try
            {
                var user = db.KHACHHANGs.Find(id);
                db.KHACHHANGs.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion





        //Xu ly thong tin Loai Sach
        #region Xu Ly Thong Tin Loai Sach

        /// <summary>
        /// hàm lấy mã thể loại
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>TheLoai</returns>
        public LOAISACH GetIdLoaiSach(int id)
        {
            return db.LOAISACHes.Find(id);
        }
        /// <summary>
        /// hàm xuất danh sách thể loại
        /// </summary>
        /// <returns>List</returns>
        public List<LOAISACH> Ad_ThongTinLoaiSach()
        {
            return db.LOAISACHes.OrderBy(x => x.MaLoai).ToList();
        }

        /// <summary>
        /// hàm thêm thểm loại
        /// </summary>
        /// <param name="entity">TheLoai</param>
        /// <returns>int</returns>
        public int InsertLoaisach(LOAISACH entity)
        {
            db.LOAISACHes.Add(entity);
            db.SaveChanges();
            return entity.MaLoai;
        }

        /// <summary>
        /// hàm cập nhật thể loại
        /// </summary>
        /// <param name="entity">TheLoai</param>
        /// <returns>int</returns>
        public int UpdateLoaiSach(LOAISACH entity)
        {
            try
            {
                var tl = db.LOAISACHes.Find(entity.MaLoai);
                tl.TenLoai = entity.TenLoai;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// hàm xóa thể loại
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>bool</returns>
        public bool DeleteLoaiSach(int id)
        {
            try
            {
                var tl = db.LOAISACHes.Find(id);
                db.LOAISACHes.Remove(tl);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion


        //Xu ly thong tin Tac Gia
         #region Xu ly thong tin tac gia
        /// <summary>
        /// hàm lấy mã tác giả
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>TacGia</returns>
        public TACGIA GetIdTacGia(int id)
        {
            return db.TACGIAs.Find(id);
        }


        /// <summary>
        /// hàm xuất danh sách tác giả
        /// </summary>
        /// <returns>List</returns>
        public List<TACGIA> AD_ShowAllTacgia()
        {
            return db.TACGIAs.OrderBy(x => x.MaTG).ToList();
        }

        /// <summary>
        /// hàm thêm tác giả
        /// </summary>
        /// <param name="entity">TacGia</param>
        /// <returns></returns>
        public int InsertTacgia(TACGIA entity)
        {
            db.TACGIAs.Add(entity);
            db.SaveChanges();
            return entity.MaTG;
        }

        /// <summary>
        /// hàm cập nhật tác giả
        /// </summary>
        /// <param name="entity">TacGia</param>
        /// <returns>int</returns>
        public int UpdateTacgia(TACGIA entity)
        {
            try
            {
                var tg = db.TACGIAs.Find(entity.MaTG);
                tg.TenTG = entity.TenTG;
                tg.QueQuan = entity.QueQuan;
                tg.NgaySinh = entity.NgaySinh;
                tg.NgayMat = entity.NgayMat;
                tg.TieuSu= entity.TieuSu;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// hàm xóa tác giả
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>int</returns>
        public bool DeleteTacgia(int id)
        {
            try
            {
                var tg = db.TACGIAs.Find(id);
                db.TACGIAs.Remove(tg);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion


       //Xu ly Thong Tin nha Xuat Ban

        #region Xu ly thong tin nha xuat ban


        /// <summary>
        /// hàm lấy mã nhà xuất bản
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>NhaXuatBan</returns>
        public NHAXUATBAN GetIdNXB(int id)
        {
            return db.NHAXUATBANs.Find(id);
        }
        /// <summary>
        /// hàm xuất danh sách nhà xuất bản
        /// </summary>
        /// <returns>List</returns>
        public List<NHAXUATBAN> AD_ShowAllNhaXuatban()
        {
            return db.NHAXUATBANs.OrderBy(x => x.MaNXB).ToList();
        }

        /// <summary>
        /// hàm thêm nhà xuất bản
        /// </summary>
        /// <param name="entity">NhaXuatBan</param>
        /// <returns>int</returns>
        public int InsertNhaXuatban(NHAXUATBAN entity)
        {
            db.NHAXUATBANs.Add(entity);
            db.SaveChanges();
            return entity.MaNXB;
        }

        /// <summary>
        /// hàm cập nhật nhà xuất bản
        /// </summary>
        /// <param name="entity">NhaXuatBan</param>
        /// <returns>int</returns>
        public int UpdateNhaXuatban(NHAXUATBAN entity)
        {
            try
            {
                var nxb = db.NHAXUATBANs.Find(entity.MaNXB);
                nxb.TenNXB = entity.TenNXB;
                nxb.DiaChi = entity.DiaChi;
                nxb.DienThoai = entity.DienThoai;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// hàm xóa nhà xuất bản
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>bool</returns>
        public bool DeleteNhaXuatban(int id)
        {
            try
            {
                var nxb = db.NHAXUATBANs.Find(id);
                db.NHAXUATBANs.Remove(nxb);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion


        //Xu ly Thong Tin Phieu Dat Hang

         #region Xu ly Thong Tin PhieuDatHang
        /// <summary>
        /// hàm xuất danh sách đơn đặt hàng
        /// </summary>
        /// <returns>List</returns>
        public List<PHIEUDATHANG> AD_ShowAllphieudathang()
        {
            return db.PHIEUDATHANGs.OrderBy(x => x.MaPhieuDH).ToList();
        }

        /// <summary>
        /// Xem chi tiết đơn hàng
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>List</returns>
        public List<CT_PHIEUDATHANG> detailsCT_PDDH(int id)
        {
            return db.CT_PHIEUDATHANG.Where(x => x.MaPhieuDH == id).OrderBy(x => x.MaPhieuDH).ToList();
        }
         #endregion


        //Xu ly Thong Tin Nha Cung Cap
        #region
        /// <summary>
        /// hàm lấy mã nhà cung câp
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>NHACUNGCAP</returns>
        public NHACUNGCAP GetIdNCC(string id)
        {
            return db.NHACUNGCAPs.Find(id.TrimEnd());
        }
        /// <summary>
        /// hàm xuất danh sách nhà cung cấp
        /// </summary>
        /// <returns>List</returns>
        public List<NHACUNGCAP> AD_ShowNhaCungcap()
        {
            return db.NHACUNGCAPs.OrderBy(x => x.MaNCC.TrimEnd()).ToList();
        }
        /// <summary>
        /// hàm thêm nhà cung cấp
        /// </summary>
        /// <param name="entity">NhaXuatBan</param>
        /// <returns>string</returns>
        public int InsertNcc(NHACUNGCAP entity)
        {
            db.NHACUNGCAPs.Add(entity);
            db.SaveChanges();
            return 1;
        }
        /// <summary>
        /// hàm cập nhật nhà cung cấp
        /// </summary>
        /// <param name="entity">TacGia</param>
        /// <returns>string</returns>
        /// 
        public int UpdateNcc(NHACUNGCAP entity)
        {
            try
            {
                var tg = db.NHACUNGCAPs.Find(entity.MaNCC);
                tg.TenNCC = entity.TenNCC;
                tg.DiaChi = entity.DiaChi;
                tg.DienThoai = entity.DienThoai;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// hàm xóa nhà cung câp
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>string</returns>
        public bool DeleteNcc(string id)
        {
            try
            {
                var tg = db.NHACUNGCAPs.Find(id.TrimEnd());
                db.NHACUNGCAPs.Remove(tg);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion



        //Xu ly Thong Tin Phieu Nhập hàng
        #region Xu ly Thong Tin PhieuNhapHang
        /// <summary>
        /// hàm xuất danh sách phiếu nhập hàng
        /// </summary>
        /// <returns>List</returns>
        public List<PHIEUNHAPHANG> AD_ShowAllphieunhaphang()
        {
            return db.PHIEUNHAPHANGs.OrderBy(x => x.MaPhieuNhapHang).ToList();
        }

        /// <summary>
        /// Xem chi tiết đơn nhập  hàng
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>List</returns>
        public List<CT_PHIEUNHAPHANG> detailsCT_PNhaphang(string id)
        {
            return db.CT_PHIEUNHAPHANG.Where(x => x.MaPhieuNhapHang== id.Trim()).OrderBy(x => x.MaPhieuNhapHang).ToList();
        }
          /// <summary>
        /// hàm thêm phiếu nhập hàng
        /// </summary>
        /// <param name="entity">Sach</param>
        /// <returns>string</returns>
        public int Insertphieunhaphang(PHIEUNHAPHANG entity)
        {
            db.PHIEUNHAPHANGs.Add(entity);
            db.SaveChanges();
            return 1;
        }
        /// hàm lấy mã chi tiết phiếu nhập hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CT_PHIEUDATHANG GetIdCT_PNH(string id)
        {
            return db.CT_PHIEUDATHANG.Find(id.Trim());
        }

        /// <summary>
        /// hàm thêm sản phẩm vào đơn đặt hàng
        /// </summary>
        /// <param name="detail">ChiTietDDH</param>
        /// <returns>bool</returns>
        public bool InsertCT_PNH(CT_PHIEUNHAPHANG detail)
        {
            try
            {
                db.CT_PHIEUNHAPHANG.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
       
        #endregion
    }
}