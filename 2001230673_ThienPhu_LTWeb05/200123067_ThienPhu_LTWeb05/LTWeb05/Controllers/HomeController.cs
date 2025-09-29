using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTWeb05.Models;
using LTWeb05.ViewModels;

namespace LTWeb05.Controllers
{
    public class HomeController : Controller
    {


        Dulieu csdl = new Dulieu();
        //tạo ra danh sách nhân viên
        public ActionResult HienThiNhanVien()
        {
            List<Employee> dsNhanVien = csdl.dsNV;
            return View(dsNhanVien);
        }

        //hiển thị ra danh sách phòng ban
        public ActionResult HienThiPhongBan()
        {
            List<PhongBan>  dsPB = csdl.dsPB;
            return View(dsPB);
        }


        //nhân viên của từng phòng bàn theo ID
        [HttpGet] 
        public ActionResult ThongTin_PhongBan(string id)
        {
            PhongBanViewModels phongBan = new PhongBanViewModels();
            PhongBan ds = csdl.XemChiTiet_Phong(id);
            List<Employee> dsnv = csdl.DSNhanVienTheoMaPhong(id);
            phongBan.phongBan = ds;
            phongBan.Employees = dsnv;
            return View(phongBan);
        }

        //thêm phòng ban
        [HttpGet]
        public ActionResult Them_PhongBan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Them_PhongBan(string ID, string Name)
        {
            PhongBan phongBan = new PhongBan();
            phongBan.MaPhong = ID;
            phongBan.TenPhong = Name;

            bool Result = csdl.Them_PhongBan(phongBan);

            if (Result)
                return RedirectToAction("HienThiPhongBan");   
            else
            {
                ViewBag.Message = "Thêm thất bại";
                return View();
            }
        }



        //chỉnh sửa phòng ban
        [HttpGet]
        public ActionResult Edit_PhongBan(string ID)
        {
            PhongBan ds = csdl.XemChiTiet_Phong(ID);
            return View(ds);
        }

        [HttpPost]
        public ActionResult Edit_PhongBan(string ID, string Name)
        {
            PhongBan phongBan = new PhongBan();
            phongBan.MaPhong = ID;
            phongBan.TenPhong = Name;

            bool result = csdl.CapNhat_PhongBan(phongBan);

            if (result)
                return RedirectToAction("HienThiPhongBan");   // Cập nhật xong về danh sách
            else
            {
                ViewBag.Message = "FAILURE";
                return View(phongBan);
            }
        }



        //xóa phòng ban
        [HttpGet]
        public ActionResult Delete_PhongBan(string ID)
        {
            PhongBan ds = csdl.XemChiTiet_Phong(ID);
            return View(ds);
        }
        [HttpPost]
        public ActionResult Delete_PhongBan(string ID, FormCollection f)
        {
            bool result = csdl.Xoa_PhongBan(ID);
            if (result)
                return RedirectToAction("HienThiPhongBan");   // Cập nhật xong về danh sách
            else
            {
                ViewBag.Message = "Phòng ban còn nhân viên, không thể xóa!";
                PhongBan ds = csdl.XemChiTiet_Phong(ID);
                return View(ds);
            }
        }





    }
}