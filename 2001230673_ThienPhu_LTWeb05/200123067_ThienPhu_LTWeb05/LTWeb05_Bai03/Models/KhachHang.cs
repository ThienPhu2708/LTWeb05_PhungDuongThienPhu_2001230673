using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTWeb05_Bai03.Models
{
	public class KhachHang
	{
		public string MaKhachHang { get; set; }
		public string TenKhachHang { get; set; }
        public string GioiTinh { get; set; }
        public string SoThich { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
    }
}