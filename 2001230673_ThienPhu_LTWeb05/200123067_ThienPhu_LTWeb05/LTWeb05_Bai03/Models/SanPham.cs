using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTWeb05_Bai03.Models
{
	public class SanPham
	{
		public string MaSanPham { get; set; }

        public string TenSP { get; set; }
        public string DuongDan { get; set; }
        public decimal GiaBan { get; set; }
        public string MaPhanLoai {get; set; }

        public string MoTa { get; set; }
       
    }
}