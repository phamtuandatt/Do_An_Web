
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DoAn_MonHoc.Models.Process
{
    public class HomeProcess
    {
        //Khởi tạo biến dữ liệu : db
        NHASACHEntities db = null;

        //constructor :  khởi tạo đối tượng
        public HomeProcess()
        {
            db = new NHASACHEntities();
        }
        /// <summary>
        /// hàm lưu phản hồi từ khách hàng vào db
        /// </summary>
        /// <param name="entity">LienHe</param>
        /// <returns>int</returns>
        public int InsertContact(LIENHE entity)
        {
            db.LIENHEs.Add(entity);
            db.SaveChanges();

            return entity.MaLH;
        }


       
    }
}