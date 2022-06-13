using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using QLChamCong.Model;
using System.Text.RegularExpressions;

namespace QLChamCong.Controller
{
    class DAO
    {
        string str = @"Data Source=DESKTOP-FA5AISU\SQLEXPRESS;Initial Catalog=QLChamCong;Integrated Security=True";
        SqlConnection con;
        SqlCommand com = new SqlCommand();
        SqlDataAdapter dad = new SqlDataAdapter();
        
        public DAO()
        {
            con = new SqlConnection(str);
            con.Open();
        }
        private DateTime getDate(string date)
        {
            string[] time = date.Split('/', ' ', ':');
            int nam = int.Parse(time[2]);
            int thang = int.Parse(time[0]);
            int ngay = int.Parse(time[1]);
            DateTime kq = new DateTime(nam,thang,ngay );
            return kq;
        }
        public List<NhanVien> getListNV()
        {
            List<NhanVien> listNV = new List<NhanVien>();
            com = con.CreateCommand();
            com.CommandText = "select NV.*, PB.TenPB, CV.TenCV from tblNhanVien as NV, tblPhongBan as PB, tblChucVu as CV where NV.MaPB= PB.MaPB and NV.MaCV= CV.MaCV;";
            dad.SelectCommand = com;
            DataTable dt = new DataTable();
            dad.Fill(dt);
            foreach(DataRow r in dt.Rows)
            {
                NhanVien nv = new NhanVien();
                nv.MaNV = int.Parse(r["MaNV"].ToString());
                nv.TenNV = r["TenNV"].ToString();
                nv.Cccd = r["CCCD"].ToString();
                nv.NgaySinh = getDate(r["NgaySinh"].ToString());
                nv.GioiTinh = bool.Parse(r["GioiTinh"].ToString());
                nv.DiaChi = r["DiaChi"].ToString();
                nv.ChucVu = r["TenCV"].ToString();
                nv.PhongBan= r["TenPB"].ToString();
                nv.HsLuong = float.Parse(r["HSLuong"].ToString());
                nv.HinhAnh = r["HinhAnh"].ToString();
                listNV.Add(nv);
            }
            return listNV;
        }
        public string ConvertToUnSign(string text)
        {
            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            text = text.Replace(" ", "");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public bool addNhanVien(NhanVien nv, int CV, int PB)
        {
            try
            {
                com = con.CreateCommand();
                com.CommandText = "insert into tblTaiKhoan(Username,Password) values('" + ConvertToUnSign(nv.TenNV) + nv.MaNV + "','" + nv.Cccd + "'); " +
                    " insert into tblNhanVien(TenNV, CCCD, NgaySinh, GioiTinh, DiaChi, MaPB, MaCV, MaTK, HSLuong, HinhAnh) " +
                    "values(N'" + nv.TenNV + "', '" + nv.Cccd + "', '" + nv.NgaySinh.ToString("MM-dd-yyyy") + "', '" + nv.GioiTinh + "', N'" + nv.DiaChi + "', " + PB + ", " + CV + ", " + nv.MaNV + ", 1.0, '" + nv.HinhAnh + "')";
                com.ExecuteNonQuery();
                return true;
            }
            catch(Exception exp)
            {
                return false;
            }
        }
        public bool delNhanVien(int MaNV)
        {
            try
            {
                com = con.CreateCommand();
                com.CommandText = "delete from tblNhanVien where MaNV="+MaNV+"; " +
                    "delete from tblTaiKhoan where MaTK=" + MaNV + ";  ";
                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }
        public bool updateNV(NhanVien nv, int CV, int PB)
        {
            try
            {
                com = con.CreateCommand();
                com.CommandText = "update tblNhanVien set TenNV=N'"+nv.TenNV+"', CCCD= '"+nv.Cccd+"', NgaySinh= '"+ nv.NgaySinh.ToString("MM-dd-yyyy") + "', DiaChi= N'"+nv.DiaChi+"', MaCV="+CV+", MaPB="+PB+", HSLuong="+nv.HsLuong+" where MaNV="+nv.MaNV+" ;";
                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }
    }
}
