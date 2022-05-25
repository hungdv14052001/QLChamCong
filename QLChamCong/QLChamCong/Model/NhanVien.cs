using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QLChamCong.Model
{
    class NhanVien
    {
        private int maNV;
        private string tenNV;
        private string cccd;
        private DateTime ngaySinh;
        private bool gioiTinh;
        private string diaChi;
        private string phongBan;
        private string chucVu;
        private float hsLuong;
        private string hinhAnh;

        public NhanVien()
        {

        }

        public NhanVien(int maNV, string tenNV, string cccd, DateTime ngaySinh, bool gioiTinh, string diaChi, string phongBan, string chucVu, float hsLuong, string hinhAnh)
        {
            this.maNV = maNV;
            this.tenNV = tenNV;
            this.cccd = cccd;
            this.ngaySinh = ngaySinh;
            this.gioiTinh = gioiTinh;
            this.diaChi = diaChi;
            this.phongBan = phongBan;
            this.chucVu = chucVu;
            this.hsLuong = hsLuong;
            this.hinhAnh = hinhAnh;
        }

        public int MaNV { get => maNV; set => maNV = value; }
        public string TenNV { get => tenNV; set => tenNV = value; }
        public string Cccd { get => cccd; set => cccd = value; }
        public DateTime NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public bool GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string PhongBan { get => phongBan; set => phongBan = value; }
        public string ChucVu { get => chucVu; set => chucVu = value; }
        public float HsLuong { get => hsLuong; set => hsLuong = value; }
        public string HinhAnh { get => hinhAnh; set => hinhAnh = value; }
    }
        
}
