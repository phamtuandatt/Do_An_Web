
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_MonHoc.Areas.Admin;
using DoAn_MonHoc.Models;
using DoAn_MonHoc.Models.Process;
using System.IO;

namespace DoAn_MonHoc.Areas.Admin.Controllers
{
   
    public class HomeController : Controller
    {
        //Trang quản lý

        //Khởi tạo biến dữ liệu : db
        NHASACHEntities db = new NHASACHEntities();

        // GET: Admin/Home : trang chủ Admin

        public ActionResult Index()
        {
            return View();
        }

        #region Admin_ThemXoaSua_ThongTinSach

        //GET : Admin/Home/AD_ShowAllBook : Trang quản lý sách
        [HttpGet]
        public ActionResult AD_ShowAllBook()
        {
            //Gọi hàm Ad_ThongTinSach và truyền vào model trả về View
            var model = new AdminProcess().Ad_ThongTinSach();

            return View(model);
        }
        //DELETE : Admin/Home/DeleteBook/:id : thực hiện xóa 1 cuốn sách
        [HttpDelete]
        public ActionResult DeleteBook(int id)
        {
            //gọi hàm DeleteBook để thực hiện xóa
            new AdminProcess().DeleteBook(id);

            //trả về trang quản lý sách
            return RedirectToAction("AD_ShowAllBook");
        }
        //GET : Admin/Home/DetailsBook/:id : Trang xem chi tiết 1 cuốn sách
        [HttpGet]
        public ActionResult DetailsBook(int id)
        {
            //gọi hàm lấy id sách và truyền vào View
            var sach = new AdminProcess().GetIdBook(id);

            return View(sach);
        }
        public ActionResult UpdateBook(int id)
        {
            //gọi hàm lấy mã sách
            var sach = new AdminProcess().GetIdBook(id);

            //thực hiện việc lấy mã nhưng hiển thị tên và đúng tại mã đang chỉ định và gán vào ViewBag
            ViewBag.MaLoai = new SelectList(db.LOAISACHes.ToList().OrderBy(x => x.TenLoai), "MaLoai", "TenLoai", sach.MaLoai);
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(x => x.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            ViewBag.MaTG = new SelectList(db.TACGIAs.ToList().OrderBy(x => x.TenTG), "MaTG", "TenTG", sach.MaTG);

            return View(sach);
        }

        //POST : /Admin/Home/UpdateBook : thực hiện việc cập nhật sách
        //Tương tự như thêm sách
        [HttpPost]
        public ActionResult UpdateBook(THONGTINSACH sach, HttpPostedFileBase fileUpload)
        {
            //thực hiện việc lấy mã nhưng hiển thị tên ngay đúng mã đã chọn và gán vào ViewBag
            ViewBag.MaLoai = new SelectList(db.LOAISACHes.ToList().OrderBy(x => x.TenLoai), "MaLoai", "TenLoai", sach.MaLoai);
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(x => x.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            ViewBag.MaTG = new SelectList(db.TACGIAs.ToList().OrderBy(x => x.TenTG), "MaTG", "TenTG", sach.MaTG);

            //Nếu không thay đổi ảnh bìa thì làm
            if (fileUpload == null)
            {
                //kiểm tra hợp lệ dữ liệu
                if (ModelState.IsValid)
                {
                    //gọi hàm UpdateBook cho việc cập nhật sách
                    var result = new AdminProcess().UpdateBook(sach);

                    if (result == 1)
                    {
                        ViewBag.Success = "Cập nhật thành công";
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật không thành công.");
                    }
                }
            }
            //nếu thay đổi ảnh bìa thì làm
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("/HinhAnhSach"), fileName);

                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Alert = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }

                    sach.HinhAnh = fileName;
                    var result = new AdminProcess().UpdateBook(sach);
                    if (result == 1)
                    {
                        ViewBag.Success = "Cập nhật thành công";
                    }
                    else
                    {
                        ModelState.AddModelError("", "cập nhật không thành công.");
                    }
                }
            }

            return View(sach);
        }
        //GET : Admin/Home/InsertBook : Trang thêm sách mới
        public ActionResult InsertBook()
        {
            //lấy mã mà hiển thị tên
            ViewBag.MaLoai = new SelectList(db.LOAISACHes.ToList().OrderBy(x => x.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(x => x.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaTG = new SelectList(db.TACGIAs.ToList().OrderBy(x => x.TenTG), "MaTG", "TenTG");
            return View();
        }

        //POST : Admin/Home/InsertBook : thực hiện thêm sách
        [HttpPost]
        public ActionResult InsertBook(THONGTINSACH sach, HttpPostedFileBase fileUpload)
        {
            //lấy mã mà hiển thị tên
            ViewBag.MaLoai = new SelectList(db.LOAISACHes.ToList().OrderBy(x => x.TenLoai), "MaLoai", "TenLoai", sach.MaLoai);
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(x => x.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            ViewBag.MaTG = new SelectList(db.TACGIAs.ToList().OrderBy(x => x.TenTG), "MaTG", "TenTG", sach.MaTG);

            //kiểm tra việc upload ảnh
            if (fileUpload == null)
            {
                ViewBag.Alert = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                //kiểm tra dữ liệu db có hợp lệ?
                if (ModelState.IsValid)
                {
                    //lấy file đường dẫn
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //chuyển file đường dẫn và biên dịch vào /images
                    var path = Path.Combine(Server.MapPath("/HinhAnhSach"), fileName);

                    //kiểm tra đường dẫn ảnh có tồn tại?
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Alert = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }

                    //thực hiện việc lưu đường dẫn ảnh vào link ảnh bìa
                    sach.HinhAnh = fileName;
                    //thực hiện lưu vào db
                    var result = new AdminProcess().Insertbook(sach);
                    if (result > 0)
                    {
                        ViewBag.Success = "Thêm mới thành công";
                        //xóa trạng thái để thêm mới
                        ModelState.Clear();
                    }
                    else
                    {
                        ModelState.AddModelError("", "thêm không thành công.");
                    }
                }
            }

            return View();
        }
        #endregion



        #region Admin_QuanLy_Phản hồi

        //Contact/Feedback : Liên hệ / phản hồi khách hàng

        [HttpGet]
        //GET : Admin/Home/FeedBack_KH : xem danh sách thông báo phản hồi
        public ActionResult FeedBack_KH()
        {
            var result = new AdminProcess().ShowListContact();

            return View(result);
        }

        //GET : Admin/Home/FeedDetail_KH/:id : xem nội dung phản hồi khách hàng
        public ActionResult FeedDetail_KH(int id)
        {
            var result = new AdminProcess().GetIdContact(id);

            return View(result);
        }

        //DELETE : Admin/Home/DeleteFeedBack_KH/:id : xóa thông tin phản hồi khách hàng
        [HttpDelete]
        public ActionResult DeleteFeedBack_KH(int id)
        {
            new AdminProcess().deleteContact(id);

            return RedirectToAction("FeedBack_KH");
        }

        #endregion


        #region Admin_QuanLy_Người dùng

        //GET : /Admin/Home/AD_ShowAllKH : trang quản lý người dùng
        public ActionResult AD_ShowAllKH()
        {
            var result = new AdminProcess().ListUser();

            return View(result);
        }

        //GET : /Admin/Home/DetailsUserKH/:id : trang xem chi tiết người dùng
        public ActionResult DetailsUserKH(int id)
        {
            var result = new AdminProcess().GetIdKH(id);

            return View(result);
        }

        //DELETE : Admin/Home/DeleteUserKH/:id : xóa thông tin người dùng
        [HttpDelete]
        public ActionResult DeleteUserKH(int id)
        {
            new AdminProcess().DeleteUser(id);

            return RedirectToAction("AD_ShowAllKH");
        }

        #endregion


        #region Admin_ThemXoaSua_TheLoai

        //GET : /Admin/Home/AD_ShowListLoaiSach: trang quản lý thể loại
        [HttpGet]
        public ActionResult AD_ShowAllLoaiSach()
        {
            //gọi hàm ListAllCategory để hiện những thể loại trong db
            var model = new AdminProcess().Ad_ThongTinLoaiSach();

            return View(model);
        }

        //GET : Admin/Home/InsertLoaiSach : trang thêm thể loại
        [HttpGet]
        public ActionResult  InsertLoaiSach()
        {
            return View();
        }

        //POST : Admin/Home/InsertLoaiSach/:model : thực hiện việc thêm thể loại vào db
        [HttpPost]
        public ActionResult InsertLoaiSach(LOAISACH model)
        {
            //kiểm tra dữ liệu hợp lệ
            if (ModelState.IsValid)
            {
                //khởi tao biến admin trong WebBanSach.Models.Process
                var admin = new AdminProcess();

                //khởi tạo biến thuộc đối tượng thể loại trong db
                var tl = new LOAISACH();

                //gán thuộc tính tên thể loại
                tl.TenLoai = model.TenLoai;

                //gọi hàm thêm thể loại (InsertLoaiSach) trong biến admin
                var result = admin.InsertLoaisach(tl);

                //kiểm tra hàm
                if (result > 0)
                {
                    ViewBag.Success = "Thêm mới thành công";
                    //xóa trạng thái
                    ModelState.Clear();

                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công.");
                }
            }

            return View(model);
        }

        //GET : Admin/Home/UpdateLoaiSach/:id : trang cập nhật thể loại
        [HttpGet]
        public ActionResult UpdateLoaiSach(int id)
        {
            //gọi hàm lấy mã thể loại
            var tl = new AdminProcess().GetIdLoaiSach(id);

            //trả về dữ liệu View tương ứng
            return View(tl);
        }

        //POST : /Admin/Home/UpdateLoaiSach/:id : thực hiện việc cập nhật thể loại
        [HttpPost]
        public ActionResult UpdateLoaiSach(LOAISACH tl)
        {
            //kiểm tra tính hợp lệ dữ liệu
            if (ModelState.IsValid)
            {
                //khởi tạo biến admin
                var admin = new AdminProcess();

                //gọi hàm cập nhật thể loại
                var result = admin.UpdateLoaiSach(tl);

                //thực hiện kiểm tra
                if (result == 1)
                {
                    return RedirectToAction("AD_ShowAllLoaiSach");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công.");
                }
            }

            return View(tl);
        }

        //DELETE : /Admin/Home/DeleteLoaiSach:id : thực hiện xóa thể loại
        [HttpDelete]
        public ActionResult DeleteLoaiSach(int id)
        {
            // gọi hàm xóa thể loại
            new AdminProcess().DeleteLoaiSach(id);

            //trả về trang quản lý thể loại
            return RedirectToAction("AD_ShowAllLoaiSach");
        }
        #endregion


        #region Admin_ThemXoaSua_TacGia

        //GET : /Admin/Home/AD_ShowAllTacGia : trang quản lý tác giả
        [HttpGet]
        public ActionResult AD_ShowAllTacGia()
        {
            //gọi hàm xuất danh sách tác giả trong db
            var model = new AdminProcess().AD_ShowAllTacgia();

            //trả về View tương ứng
            return View(model);
        }

        //GET : /Admin/Home/InsertTacGia : trang thêm tác giả
        public ActionResult InsertTacGia()
        {
            return View();
        }

        //POST : /Admin/Home/InsertTacGia/:model : thực hiện việc thêm tác giả
        [HttpPost]
        public ActionResult InsertTacGia(TACGIA model)
        {
            //kiểm tra tính hợp lệ dữ liệu
            if (ModelState.IsValid)
            {
                //khởi tạo biến admin
                var admin = new AdminProcess();

                //khởi tạo đối tượng tg
                var tg = new TACGIA();

                //gán dữ liệu
                tg.TenTG = model.TenTG;
                tg.QueQuan = model.QueQuan;
                tg.NgaySinh = model.NgaySinh;
                tg.NgayMat = model.NgayMat;
                tg.TieuSu = model.TieuSu;

                //gọi hàm thêm tác giả
                var result = admin.InsertTacgia(tg);

                //kiểm tra hàm
                if (result > 0)
                {
                    ViewBag.Success = "Thêm mới thành công";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công.");
                }
            }

            return View(model);
        }

        //GET : /Admin/Home/UpdateTacGia/:id : trang thêm tác giả 
        [HttpGet]
        public ActionResult UpdateTacGia(int id)
        {
            //gọi hàm lấy mã tác giả
            var tg = new AdminProcess().GetIdTacGia(id);

            return View(tg);
        }

        //POST : /Admin/Home/UpdateTacGia/:id : thực hiện việc thêm tác giả
        [HttpPost]
        public ActionResult UpdateTacGia(TACGIA tg)
        {
            //kiểm tra hợp lệ dữ liệu
            if (ModelState.IsValid)
            {
                //khởi tạo biến admin
                var admin = new AdminProcess();

                //gọi hàm cập nhật tác giả
                var result = admin.UpdateTacgia(tg);
                //thực hiển kiểm tra
                if (result == 1)
                {
                    ViewBag.Success = "Cập nhật thành công";
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công.");
                }
            }

            return View(tg);
        }

        //DELETE : /Admin/Home/DeleteTacGia/:id : thực hiện xóa tác giả
        [HttpDelete]
        public ActionResult DeleteTacGia(int id)
        {
            //gọi hàm xóa tác giả
            new AdminProcess().DeleteTacgia(id);

            return RedirectToAction("AD_ShowAllTacGia");
        }
         #endregion



        #region Admin_ThemXoaSua_NhaXuatBan
        //GET : /Admin/Home/AD_ShowAllNhaXuatBan : trang quản lý nhà xuất bản
        [HttpGet]
        public ActionResult AD_ShowAllNhaXuatBan()
        {
            //gọi hàm xuất danh sách nhà xuất bản
            var model = new AdminProcess().AD_ShowAllNhaXuatban();

            return View(model);
        }

        //GET : /Admin/Home/InsertNXB : trang quản lý nhà xuất bản
        public ActionResult InsertNhaXuatBan()
        {
            return View();
        }

        //POST : /Admin/Home/ InsertNXB/:model : thực hiện việc thêm nhà xuất bản
        [HttpPost]
        public ActionResult InsertNhaXuatBan(NHAXUATBAN model)
        {
            //kiểm tra tính hợp lệ dữ liệu
            if (ModelState.IsValid)
            {
                //khởi tạo biến admin
                var admin = new AdminProcess();

                //khởi tạo object(đối tượng) nhà xuất bản
                var nxb = new NHAXUATBAN();

                //gán dữ liệu
                nxb.TenNXB = model.TenNXB;
                nxb.DiaChi = model.DiaChi;
                nxb.DienThoai = model.DienThoai;

                //gọi hàm thêm nhà xuất bản
                var result = admin.InsertNhaXuatban(nxb);
                //kiểm tra hàm
                if (result > 0)
                {
                    ViewBag.Success = "Thêm mới thành công";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công.");
                }
            }

            return View(model);
        }

        //GET : /Admin/Home/UpdateNhaXuatBan/:id : trang update nhà xuất bản
        [HttpGet]
        public ActionResult UpdateNhaXuatBan(int id)
        {
            //gọi hàm lấy mã nhà xuất bản
            var nxb = new AdminProcess().GetIdNXB(id);

            return View(nxb);
        }

        //GET : /Admin/Home/UpdateNhaXuatBan/:id : thực hiện update nhà xuất bản
        [HttpPost]
        public ActionResult UpdateNhaXuatBan(NHAXUATBAN nxb)
        {
            //kiểm tra tính hợp lệ dữ liệu
            if (ModelState.IsValid)
            {
                //khởi tạo biến admin
                var admin = new AdminProcess();

                //gọi hàm cập nhật nhà xuất bản
                var result = admin.UpdateNhaXuatban(nxb);
                //kiểm tra hàm
                if (result == 1)
                {
                    ViewBag.Success = "Cập nhật nhật thành công";
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công.");
                }
            }

            return View(nxb);
        }

        //DELETE : Admin/Home/DeleteNXB/:id : thực hiện xóa nhà xuất bản
        [HttpDelete]
        public ActionResult DeleteNhaXuatBan(int id)
        {
            //gọi hàm xóa hàm xuất bản
            new AdminProcess().DeleteNhaXuatban(id);

            //trả về trang quản lý nhà xuất bản
            return RedirectToAction("AD_ShowAllNhaXuatBan");
        }

        #endregion


        #region Đơn đặt hàng

        //GET : Admin/Home/D_ShowAllPhieuDatHang : trang quản lý đơn đặt hàng
        public ActionResult AD_ShowAllPhieuDatHang()
        {
            var result = new AdminProcess().AD_ShowAllphieudathang();

            return View(result);
        }

        //GET : /Admin/Home/DetailsCT_PDDH : trang xem chi tiết đơn hàng
        public ActionResult DetailsCT_PDDH(int id)
        {
            var result = new AdminProcess().detailsCT_PDDH(id);

            return View(result);
        }

        #endregion


        #region Nhà Cung Cấp

        //GET : Admin/Home/AD_ShowNhaCungCap : trang quản lý nha cung cấp
        [HttpGet]
        public ActionResult AD_ShowNhaCungCap()
        {
            var result = new AdminProcess().AD_ShowNhaCungcap();

            return View(result);
        }

        //GET : /Admin/Home/InsertNCC : trang insert nhà cung cấp
        public ActionResult InsertNCC()
        {
            return View();
        }

        //POST : /Admin/Home/ InsertNCC/:model : thực hiện việc thêm nhà cung cấp
        [HttpPost]
        public ActionResult InsertNCC(NHACUNGCAP model)
        {
            //kiểm tra tính hợp lệ dữ liệu
            if (ModelState.IsValid)
            {
                //khởi tạo biến admin
                var admin = new AdminProcess();

                //khởi tạo object(đối tượng) nhà cung cap
                var ncc = new NHACUNGCAP();

                //gán dữ liệu
                ncc.MaNCC = model.MaNCC;
                ncc.TenNCC = model.TenNCC;
                ncc.DiaChi = model.DiaChi;
                ncc.DienThoai = model.DienThoai;

                //gọi hàm thêm nhà xuất bản
                var result = admin.InsertNcc(ncc);
                //kiểm tra hàm
                if (result==1)
                {
                    ViewBag.Success = "Thêm mới thành công";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công.");
                }
            }

            return View(model);
        }

        //GET : /Admin/Home/UpdateNCC/:id : trang update nhà cung cấp
        [HttpGet]
        public ActionResult UpdateNCC(string id)
        {
            //gọi hàm lấy mã nhà xuất bản
            var nxb = new AdminProcess().GetIdNCC(id);

            return View(nxb);
        }

        //GET : /Admin/Home/UpdateNCC/:id : thực hiện thêm nhà xuất bản
        [HttpPost]
        public ActionResult UpdateNCC(NHACUNGCAP ncc)
        {
            //kiểm tra tính hợp lệ dữ liệu
            if (ModelState.IsValid)
            {
                //khởi tạo biến admin
                var admin = new AdminProcess();

                //gọi hàm cập nhật nhà xuất bản
                var result = admin.UpdateNcc(ncc);
                //kiểm tra hàm
                if (result == 1)
                {
                    ViewBag.Success = "Cập nhật nhật thành công";
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công.");
                }
            }

            return View(ncc);
        }

        //DELETE : Admin/Home/DeleteNCC/:id : thực hiện xóa nhà cung cấp
  
        [HttpDelete]
        public ActionResult DeleteNhaCungCap(string id )
        {
            //gọi hàm xóa hàm xuất bản
            new AdminProcess().DeleteNcc(id.TrimEnd());
            return RedirectToAction("AD_ShowNhaCungCap");
        }
        #endregion



        #region Phiếu Nhập Hàng

        //GET : Admin/Home/D_ShowAllPhieuNhapHang : trang quản lý phiếu nhập hàng
        public ActionResult AD_ShowAllPhieuNhapHang()
        {
            var result = new AdminProcess().AD_ShowAllphieunhaphang();

            return View(result);
        }

        //GET : /Admin/Home/DetailsCT_PhieuNhapHang : trang xem chi tiết phiếu nhập hàng
        public ActionResult DetailsCT_PhieuNhapHang(string id)
        {
            var result = new AdminProcess().detailsCT_PNhaphang(id.Trim());

            return View(result);
        }

        //GET : Admin/Home/InsertDonNhapHang : Trang thêm đơn nhập hàng
        public ActionResult InsertDonNhapHang()
        {
            //lấy mã mà hiển thị tên
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.ToList().OrderBy(x => x.MaNCC), "MaNCC", "TenNCC");
            return View();
        }

        //POST : Admin/Home/InsertDonNhapHang : thực hiện thêm đơn nhập hàng
        [HttpPost]
        public ActionResult InsertDonNhapHang(PHIEUNHAPHANG pnhaphang)
        {
            var list = new CT_PHIEUNHAPHANG();
            //lấy mã mà hiển thị tên
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.ToList().OrderBy(x => x.MaNCC), "MaNCC", "TenNCC",pnhaphang.MaNCC);
            pnhaphang.NgayLap_PN = DateTime.Now;
                //kiểm tra dữ liệu db có hợp lệ?
                if (ModelState.IsValid)
                {
                    //thực hiện lưu vào db
                    var result = new AdminProcess().Insertphieunhaphang(pnhaphang);
                    if (result > 0)
                    {
                        ViewBag.Success = "Thêm mới thành công";
                        if (ViewBag.TongSLNhap == 0)
                        {
                           pnhaphang.TongSL = 0;
                        }
                        else
                        {
                            pnhaphang.TongSL = ViewBag.TongSLNhap;
                        }
                        //xóa trạng thái để thêm mới
                        ModelState.Clear();
                    }
                    else
                    {
                        ModelState.AddModelError("", "thêm không thành công.");
                    }
                }
            return View();
        }

        #endregion

        public ActionResult InsertCTPhieuNhapHang()
        {
            //lấy mã mà hiển thị tên
            ViewBag.MaPhieuNhapHang = new SelectList(db.PHIEUNHAPHANGs.ToList().OrderBy(x => x.MaPhieuNhapHang), "MaPhieuNhaphang", "MaPhieuNhapHang");
            ViewBag.MaSach= new SelectList(db.THONGTINSACHes.ToList().OrderBy(x => x.MaSach), "MaSach", "TenSach");
            return View();
        }
        [HttpPost]
        public ActionResult InsertCTPhieuNhapHang(CT_PHIEUNHAPHANG ct_phieunhaphang)
        {
            var list = new CT_PHIEUNHAPHANG();
            //lấy mã mà hiển thị tên
            ViewBag.MaPhieuNhapHang = new SelectList(db.PHIEUNHAPHANGs.ToList().OrderBy(x => x.MaPhieuNhapHang), "MaPhieuNhaphang", "MaPhieuNhapHang", ct_phieunhaphang.MaPhieuNhapHang);
            ViewBag.MaSach = new SelectList(db.THONGTINSACHes.ToList().OrderBy(x => x.MaSach), "MaSach", "TenSach", ct_phieunhaphang.MaSach);
          
            //kiểm tra dữ liệu db có hợp lệ?
            if (ModelState.IsValid)
            {
                //thực hiện lưu vào db
                var result = new AdminProcess().InsertCT_PNH(ct_phieunhaphang);
                if (result == true)
                {
                    ViewBag.TongSLNhap += ct_phieunhaphang.Sluong;
                    ViewBag.Success = "Thêm mới thành công";
                    //xóa trạng thái để thêm mới
                    ModelState.Clear();
                }
                else
                {
                    ModelState.AddModelError("", "thêm không thành công.");
                }
            }
            return View();
        }

    }
}

       