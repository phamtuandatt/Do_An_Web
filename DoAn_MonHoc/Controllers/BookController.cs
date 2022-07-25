using DoAn_MonHoc.Models.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DoAn_MonHoc.Models;

namespace DoAn_MonHoc.Controllers
{
    public class BookController : Controller
    {
        //
        // GET: /Book/

        public ActionResult Index()
        {
            return View();
        }
        NHASACHEntities db = new NHASACHEntities();

        //GET : /Book/TopDateBook : hiển thị ra 8 cuốn sách mới 
        //Parital View : TopDateBook
        public ActionResult TopDateBook()
        {
            var result = new BookProcess().NewDateBook();
          
            return PartialView(result);
        }

        public double? GiaSach(int masach)
        {
            THONGTINSACH sach = db.THONGTINSACHes.Single(s => s.MaSach == masach);
            if (sach.GiamGia <= 0)
                return sach.GiaSach;
            else
            {
                return sach.GiaSach - (sach.GiamGia * sach.GiaSach);
            }

        }
        //khi sach co giam giá theo phần trăm 
        public ActionResult giasachgiam(int masach)
        {
            var re = GiaSach(masach);
            ViewBag.giagiam = re;
            return PartialView();
        }

        //GET : /Book/Favorite : hiển thị ra 4 cuốn sách bán chạy
        //Parital View : FavoriteBook
        //GET : /Book/Favorite : hiển thị ra 3 cuốn sách bán chạy theo ngày cập nhật (silde trên cùng)
        //Parital View : FavoriteBook
        public ActionResult FavoriteBook()
        {
            var result = new BookProcess().TakeBook();
            return PartialView(result);
        }
     
        //GET : /Book/ShowTheLoai: hiển thị chu đề sách danh mục phía bên trái trang chủ
        //Parital View : ShowTheLoai
        public ActionResult ShowTheLoai()
        {
            //gọi hàm xuất danh sách thể loại
            var result = new BookProcess().ListLoaiSach();

            return PartialView(result);
        }
        //GET : /Book/ShowCategory : hiển thị danh mục nhà xuất bản phía bên trái trang chủ
        //Parital View : ShowHXB
        public ActionResult ShowNXB()
        {
            //gọi hàm xuất danh sách thể loại
            var result = new BookProcess().ListNXB();

            return PartialView(result);
        }

        //GET : /Book/SachtheoCD :hien thi sach theo ma CD
        //Parital View : SachTheoCD
        public ActionResult SachTheoCD(int maCD)
        {

            var tenloai = new BookProcess().LaymaloaiCD(maCD);
            ViewBag.TenLoai = tenloai.TenLoai;
            var ListSach = new BookProcess().SachtheoCD(maCD);
            if (ListSach.Count == 0)
            {
                ViewBag.Sach = "khong co sach nao thuoc chu de nay !";
            }
            return View(ListSach);
        }
        //GET : /Book/SachtheoNXB :hien thi sach theo ma NXB
        //Parital View : SachTheoNXB
        public ActionResult SachTheoNXB(int maNXB)
        {

            var tenNXB = new BookProcess().LaymaloaiNXB(maNXB);
            ViewBag.TenNXB = tenNXB.TenNXB;
            var ListSach = new BookProcess().SachtheoNXB(maNXB);
            if (ListSach.Count == 0)
            {
                ViewBag.ThongBaoNXB = "khong co NXB nao thuoc chu de nay !";
            }
            return View(ListSach);
        }
        //GET : /Home/SearchResult : trang tìm kiếm sách
        [HttpGet]
        public ActionResult SearchResult(int? page, string key)
        {
            ViewBag.Key = key;

            //phân trang
            int pageNumber = (page ?? 1);
            int pageSize = 6;

            var result = new BookProcess().Search(key).ToPagedList(pageNumber, pageSize);

            if (result.Count == 0 || key == null || key == "")
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(result);
            }
            ViewBag.ThongBao = "Hiện có " + result.Count + " kết quả ở trang này";

            return View(result);
        }

        //POST : /Home/SearchResult : thực hiện việc tìm kiếm sách
        [HttpPost]
        public ActionResult SearchResult(int? page, FormCollection f)
        {
            //gán từ khóa tìm kiếm được nhập từ client
            string key = f["txtSearch"].ToString();

            ViewBag.Key = key;

            //phân trang
            int pageNumber = (page ?? 1);
            int pageSize = 6;

            var result = new BookProcess().Search(key).ToPagedList(pageNumber, pageSize);

            if (result.Count == 0 || key == null || key == "")
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(result);
            }
            ViewBag.ThongBao = "Hiện có " + result.Count + " kết quả ở trang này";

            return View(result);
        }

        //GET : /Book/Details/:id : hiển thị chi tiết thông tin sách
        public ActionResult ChiTietSach(int id)
        {
            var result = new BookProcess().GetIdBook(id);
            ViewBag.maloaisach = result.MaLoai;
            return View(result);
        }
        //GET : /Book/SachLienQuan :hien thi sach theo ma loai sach
        //Parital View : SachLienQuan
        public ActionResult SachLienQuan(int LoaiSach)
        {
            var LSach = new BookProcess().SachLienQuan(LoaiSach);
            if (LSach.Count == 0)
            {
                ViewBag.Sach = "khong co sach nao thuoc chu de nay !";
            }
            return View(LSach);
        }

      
       
    }
}
