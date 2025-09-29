using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace LTWeb05_Bai03.Models
{
	public class DuLieu
	{
        static string strcon = "Data Source=LAPTOP-L3MTTMQ3\\SQLEXPRESS;Initial Catalog=QL_DTDD1;Integrated Security=True;TrustServerCertificate=True;";

        SqlConnection con = new SqlConnection(strcon);
        //san pham
        public List<SanPham> dsSP = new List<SanPham>();
        //phan loai sp
        public List<Loai> dsLoai = new List<Loai>();

        public DuLieu()
        {
            ThietLap_PhanLoai();
            ThietLap_Sp();
        }


        //danh sach phan loai san pham
        public void ThietLap_PhanLoai()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM LOAI", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                var loai = new Loai();
                loai.MaPhanLoai = dr["MALOAI"].ToString();
                loai.TenPhanLoai = dr["TENLOAI"].ToString();
                dsLoai.Add(loai);
            }
        }

        //danh sach chi tiet san pham
        public void ThietLap_Sp()
        {
            //Tạo dữ liệu cho danh sách san pham
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SANPHAM", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                var sp = new SanPham();

                sp.MaSanPham = (dr["MASP"]).ToString();
                sp.TenSP = dr["TENSP"].ToString();
                sp.DuongDan = dr["DUONGDAN"].ToString();
                sp.GiaBan = (decimal)dr["GIA"];
                sp.MoTa = dr["MOTA"].ToString();
                sp.MaPhanLoai = dr["MALOAI"].ToString();
                dsSP.Add(sp);
            }
        }


        //xem chi tiết loại sản phẩm
        public Loai XemChiTiet_Loai(string MaLoai)
        {
            Loai loai= new Loai();
            string sqlScript = string.Format("select * from Loai where MALOAI = '{0}'", MaLoai);
            SqlDataAdapter da = new SqlDataAdapter(sqlScript, con);

            DataTable dt = new DataTable();
            da.Fill(dt);
            loai.MaPhanLoai = dt.Rows[0]["MALOAI"].ToString();
            loai.TenPhanLoai = dt.Rows[0]["TENLOAI"].ToString();
            return loai;
        }



        public bool Them_LoaiSP(Loai phanLoai)
        {
            try
            {
                // ❌ KHÔNG dùng nối chuỗi
                // string sqlScript = string.Format("INSERT INTO LOAI(MALOAI,TENLOAI) VALUES('{0}','{1}')", phanLoai.MaPhanLoai, phanLoai.TenPhanLoai);

                // ✅ DÙNG SqlParameter
                string sqlScript = "INSERT INTO LOAI(MALOAI,TENLOAI) VALUES(@MaLoai, @TenLoai)";
                SqlCommand cmd = new SqlCommand(sqlScript, con);

                // Thêm tham số với giá trị tiếng Việt
                cmd.Parameters.AddWithValue("@MaLoai", phanLoai.MaPhanLoai);
                cmd.Parameters.AddWithValue("@TenLoai", phanLoai.TenPhanLoai); // C# sẽ tự động xử lý Unicode

                con.Open();
                int kq = cmd.ExecuteNonQuery();
                con.Close();

                return kq > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public bool CapNhat_LoaiSP(Loai loai)
        {
            try
            {
                // ❌ KHÔNG dùng nối chuỗi
                // string sqlScript = string.Format("UPDATE LOAI SET TENLOAI = '{0}' WHERE MALOAI = '{1}'", loai.TenPhanLoai, loai.MaPhanLoai);

                // ✅ DÙNG SqlParameter
                string sqlScript = "UPDATE LOAI SET TENLOAI = @TenLoai WHERE MALOAI = @MaLoai";
                SqlCommand cmd = new SqlCommand(sqlScript, con);

                // Thêm tham số với giá trị tiếng Việt
                cmd.Parameters.AddWithValue("@TenLoai", loai.TenPhanLoai);
                cmd.Parameters.AddWithValue("@MaLoai", loai.MaPhanLoai);

                con.Open();
                int kq = cmd.ExecuteNonQuery();
                con.Close();

                return kq > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }


        //xóa loại sản phẩm
        public bool Xoa_Loai(string MaLoai)
        {
            try
            {
                string sqlScript = "DELETE FROM LOAI WHERE MALOAI = @MaLoai";
                SqlCommand cmd = new SqlCommand(sqlScript, con);
                cmd.Parameters.AddWithValue("@MaLOAI", MaLoai);

                con.Open();
                int kq = cmd.ExecuteNonQuery();
                con.Close();

                return kq > 0;
            }
            catch (SqlException ex)
            {
                // Nếu lỗi do ràng buộc khóa ngoại
                if (ex.Number == 547) // Lỗi FK constraint
                {
                    
                    return false;
                }
                return false;
            }
        }


        // Xem danh sách sản phẩm theo mã loại
        public List<SanPham> XemSanPhamTheoLoai(string MaLoai)
        {
            List<SanPham> dsSPTheoLoai = new List<SanPham>();
            string sqlScript = string.Format("SELECT * FROM SANPHAM WHERE MALOAI = '{0}'", MaLoai);
            SqlDataAdapter da = new SqlDataAdapter(sqlScript, con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                var sp = new SanPham();
                sp.MaSanPham = (dr["MASP"]).ToString();
                sp.TenSP = dr["TENSP"].ToString();
                sp.DuongDan = dr["DUONGDAN"].ToString();
                sp.GiaBan = (decimal)dr["GIA"];
                sp.MoTa = dr["MOTA"].ToString();
                sp.MaPhanLoai = dr["MALOAI"].ToString();
                dsSPTheoLoai.Add(sp);
            }
            return dsSPTheoLoai;
        }


        // Tìm kiếm danh sách sản phẩm theo tên gần đúng
        public List<SanPham> TimKiemSanPham(string tuKhoa)
        {
            List<SanPham> dsKetQua = new List<SanPham>();

            // Sử dụng LIKE và SqlParameter để tìm kiếm gần đúng và an toàn
            string sqlScript = "SELECT * FROM SANPHAM WHERE TENSP LIKE @TuKhoa";

            SqlCommand cmd = new SqlCommand(sqlScript, con);

            // Thêm tham số @TuKhoa, bọc từ khóa trong dấu % để tìm kiếm bất kỳ đâu trong tên
            // Ví dụ: nếu người dùng nhập "phone", SQL sẽ tìm kiếm "%phone%"
            cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");

            SqlDataAdapter da = new SqlDataAdapter(cmd); // Sử dụng SqlCommand đã có tham số
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                var sp = new SanPham();
                sp.MaSanPham = (dr["MASP"]).ToString();
                sp.TenSP = dr["TENSP"].ToString();
                sp.DuongDan = dr["DUONGDAN"].ToString();
                sp.GiaBan = (decimal)dr["GIA"];
                sp.MoTa = dr["MOTA"].ToString();
                sp.MaPhanLoai = dr["MALOAI"].ToString();
                dsKetQua.Add(sp);
            }
            return dsKetQua;
        }



    }
}