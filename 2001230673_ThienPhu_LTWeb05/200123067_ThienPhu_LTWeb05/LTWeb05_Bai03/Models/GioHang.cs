using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTWeb05_Bai03.Models
{
	public class GioHang
	{
		public string MaGioHang { get; set; }
        public string MaKhachHang { get; set; }
        public string MaSanPham { get; set; }
        public decimal SoLuong { get; set; }
        public DateTime Ngay { get; set; }
    }
}