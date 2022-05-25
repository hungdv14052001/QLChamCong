using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using QLChamCong.Model;

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
                nv.ChucVu = r["TenPB"].ToString();
                nv.PhongBan= r["TenCV"].ToString();
                nv.HsLuong = float.Parse(r["HSLuong"].ToString());
                nv.HinhAnh = r["HinhAnh"].ToString();
                listNV.Add(nv);
            }
            return listNV;
        }
    }
}
