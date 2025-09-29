using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LTWeb05.Models
{
    public class Employee
    {
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public string GioiTinh { get; set; }
        public string Tinh {  get; set; }
        public string MaPhong { get; set; }
    }
}
