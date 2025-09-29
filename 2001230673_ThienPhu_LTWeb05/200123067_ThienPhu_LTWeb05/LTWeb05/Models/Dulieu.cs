using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace LTWeb05.Models
{
    public class Dulieu
    {
        static string strcon = "Data Source=LAPTOP-L3MTTMQ3\\SQLEXPRESS;Initial Catalog=QLNHANSU;Integrated Security=True;TrustServerCertificate=True;";

        SqlConnection con = new SqlConnection(strcon);
        //nhan vien
        public List<Employee> dsNV = new List<Employee>();
        //phong ban
        public List<PhongBan> dsPB = new List<PhongBan>();
        public Dulieu()
        {
            ThietLap_DSNV();
            ThietLap_DSPB();
        }
        //danh sach nhan vien
        public void ThietLap_DSNV()
        {
            //Tạo dữ liệu cho danh sách nhân viên
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM EMPLOYEE", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                var em = new Employee();

                em.MaNV = (dr["ID"]).ToString();
                em.TenNV = dr["TenNV"].ToString();
                em.GioiTinh = dr["GTINH"].ToString();
                em.Tinh = dr["CITY"].ToString();
                em.MaPhong = dr["DEPID"].ToString();
                dsNV.Add(em);
            }
        }

        //danh sach phong ban
        public void ThietLap_DSPB()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM DEPARTMENT", con);
            DataTable dt = new DataTable();
            da.Fill(dt);


            foreach (DataRow dr in dt.Rows)
            {
                var pb = new PhongBan();
                pb.MaPhong = dr["DEPID"].ToString();
                pb.TenPhong = dr["TEN"].ToString();
                dsPB.Add(pb);
            }
        }


        //xem chi tiet phong ban
        public PhongBan XemChiTiet_Phong(string MaPhong)
        {
            PhongBan phongBan = new PhongBan();
            string sqlScript = string.Format("select * from DEPARTMENT where DEPID = '{0}'", MaPhong);
            SqlDataAdapter da = new SqlDataAdapter(sqlScript, con);

            DataTable dt = new DataTable();
            da.Fill(dt);
            phongBan.MaPhong = dt.Rows[0]["DEPID"].ToString();
            phongBan.TenPhong = dt.Rows[0]["TEN"].ToString();
            return phongBan;
        }


        //XEM NHÂN VIÊN THEO MÃ PHÒNG
        public List<Employee> DSNhanVienTheoMaPhong(string MaPhong)
        {
            List<Employee> employee = new List<Employee>();
            string sqlScript = string.Format("select * from EMPLOYEE where DEPID= '{0}'", MaPhong);

            SqlDataAdapter da = new SqlDataAdapter(sqlScript, con);


            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                var em = new Employee();

                em.MaNV = dr["ID"].ToString();
                em.TenNV = dr["TenNV"].ToString();
                em.GioiTinh = dr["GTINH"].ToString();
                em.Tinh = dr["CITY"].ToString();
                em.MaPhong = dr["DEPID"].ToString();
                employee.Add(em);
            }
            return employee;
        }



        //thêm phòng ban
        public bool Them_PhongBan(PhongBan phongBan)
        {
            try
            {
                string sqlScript = string.Format("INSERT INTO DEPARTMENT(DEPID,TEN) VALUES('{0}','{1}')", phongBan.MaPhong, phongBan.TenPhong);
                SqlCommand cmd = new SqlCommand(sqlScript, con);
                con.Open();
                int kq = cmd.ExecuteNonQuery();
                con.Close();
                if (kq > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //cập nhật phòng ban
        public bool CapNhat_PhongBan(PhongBan phongBan)
        {
            try
            {
                string sqlScript = string.Format("UPDATE DEPARTMENT SET TEN = '{0}' WHERE DEPID = '{1}'", phongBan.TenPhong, phongBan.MaPhong);
                SqlCommand cmd = new SqlCommand(sqlScript, con);
                con.Open();
                int kq = cmd.ExecuteNonQuery();
                con.Close();
                if (kq > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }




        //xóa phòng ban
        public bool Xoa_PhongBan(string MaPhong)
        {
            try
            {
                string sqlScript = "DELETE FROM DEPARTMENT WHERE DEPID = @MaPhong";
                SqlCommand cmd = new SqlCommand(sqlScript, con);
                cmd.Parameters.AddWithValue("@MaPhong", MaPhong);

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
                    // bạn có thể log hoặc xử lý riêng, ví dụ: thông báo còn nhân viên trong phòng ban
                    return false;
                }
                return false;
            }
        }




    }
}
