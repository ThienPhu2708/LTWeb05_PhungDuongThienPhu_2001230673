using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTWeb05_Bai03.Models;

namespace LTWeb05_Bai03.Controllers
{
    public class HomeController : Controller
    {


        DuLieu csdl = new DuLieu();
        //tạo ra danh sach loai san pham
        public ActionResult PhanLoai()
        {
            List<Loai> dsLoai = csdl.dsLoai;
            return View(dsLoai);
        }

        //hiển thị ra danh sach tat ca san pham
        public ActionResult DanhSachSanPham()
        {
            List<SanPham> dsSP = csdl.dsSP;
            return View(dsSP);
        }

        //thêm loại sản phẩm
        [HttpGet]
        public ActionResult Them_LoaiSP()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Them_LoaiSP (string ID, string Name)
        {
            Loai phanLoai = new Loai();
            phanLoai.MaPhanLoai = ID;
            phanLoai.TenPhanLoai = Name;

            bool Result = csdl.Them_LoaiSP(phanLoai);

            if (Result)
                return RedirectToAction("PhanLoai");
            else
            {
                ViewBag.Message = "Thêm thất bại";
                return View();
            }
        }


        //chỉnh sửa loai san pham
        [HttpGet]
        public ActionResult Edit_LoaiSP(string ID)
        {
            Loai loai = csdl.XemChiTiet_Loai(ID);
            return View(loai);
        }

        [HttpPost]
        public ActionResult Edit_LoaiSP(string ID, string Name)
        {
            Loai loai = new Loai();
            loai.MaPhanLoai = ID;
            loai.TenPhanLoai = Name;

            bool result = csdl.CapNhat_LoaiSP(loai);

            if (result)
                return RedirectToAction("PhanLoai");   // Cập nhật xong về danh sách
            else
            {
                ViewBag.Message = "Không thêm được";
                return View(loai);
            }
        }



        //xóa loại sản phẩm
        [HttpGet]
        public ActionResult Delete_LoaiSP(string ID)
        {
            Loai loai = csdl.XemChiTiet_Loai(ID);
            return View(loai);
        }
        [HttpPost]
        public ActionResult Delete_LoaiSP(string ID, FormCollection f)
        {
            bool result = csdl.Xoa_Loai(ID);
            if (result)
                return RedirectToAction("PhanLoai");   // Cập nhật xong về danh sách
            else
            {
                ViewBag.Message = "Loại còn sản phẩm, không thể xóa";
                Loai ds = csdl.XemChiTiet_Loai(ID);
                return View(ds);
            }
        }


        public ActionResult SanPham_Loai(string MaLoai)
        {
            // 1. Lấy danh sách Loại (cho Menu)
            ViewBag.DSLoai = csdl.dsLoai;

            // 2. Lấy danh sách Sản phẩm
            List<SanPham> dsSP;
            if (string.IsNullOrEmpty(MaLoai))
            {
                // Hiển thị tất cả sản phẩm
                dsSP = csdl.dsSP;
                ViewBag.TieuDe = "Tất Cả Sản Phẩm";
            }
            else
            {
                // Hiển thị sản phẩm theo loại
                dsSP = csdl.XemSanPhamTheoLoai(MaLoai);

                // Lấy tên loại để hiển thị tiêu đề
                Loai loai = csdl.XemChiTiet_Loai(MaLoai);
                ViewBag.TieuDe = "Sản Phẩm: " + loai.TenPhanLoai;
            }

            // Trả về danh sách sản phẩm cho View
            return View(dsSP);
        }




        // Tìm kiếm sản phẩm
        // Tham số tuKhoa sẽ nhận giá trị từ ô textbox trên View
        public ActionResult TimKiemSanPham(string tuKhoa)
        {
            List<SanPham> dsKetQua = new List<SanPham>();
            ViewBag.TuKhoa = tuKhoa; // Lưu lại từ khóa để hiển thị trên ô input

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                // Nếu có từ khóa, gọi hàm tìm kiếm trong Model
                dsKetQua = csdl.TimKiemSanPham(tuKhoa);
                ViewBag.TongSo = dsKetQua.Count;
            }
            else
            {
                // Nếu không có từ khóa (lần đầu vào trang), danh sách rỗng
                ViewBag.TongSo = 0;
            }

            // Trả về danh sách kết quả tìm kiếm cho View
            return View(dsKetQua);
        }







    }
}