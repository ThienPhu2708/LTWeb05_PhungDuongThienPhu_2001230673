using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTWeb05_Bai03
{
	public class SanPhamViewModel
	{
        public Models.SanPham sanPham { get; set; }
        public List<Models.Loai> Loais { get; set; }
    }
}