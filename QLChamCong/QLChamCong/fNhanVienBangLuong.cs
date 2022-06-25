using QLChamCong.Controller;
using QLChamCong.Dialog;
using QLChamCong.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLChamCong
{
    public partial class fNhanVienBangLuong : Form
    {
        private int currentId;
        private DateTime time = DateTime.Now;
        private int year;
        private int month;
        private string chucvu;
        DAO dao = new DAO();
        List<ChamCong> listChamCong = new List<ChamCong>();
        List<NhanVien> listNhanVien = new List<NhanVien>();
        public fNhanVienBangLuong()
        {
            InitializeComponent();
        }
        public fNhanVienBangLuong(int Ma) : this()
        {
            this.currentId = Ma;
            setLbTime();
            listChamCong = dao.getListChamCong();
            listNhanVien = dao.getListNV();
            setThongTin();
            setBangLuong();
        }
        public void setLbTime()
        {
            this.lbTime.Text = "Bảng lương tháng: " + time.ToString("MM-yyyy");
            this.year = int.Parse(this.time.ToString("yyyy"));
            this.month = int.Parse(this.time.ToString("MM"));
        }

        private void label8_Click(object sender, EventArgs e)
        {
            var dal = new dalMonthOfYear();
            dal.StartPosition = FormStartPosition.CenterParent;
            var result = dal.ShowDialog();
            if (result == DialogResult.OK)
            {
                int year = dal.Year;
                int month = dal.Month;
                this.time = new DateTime(year, month, 1);
                setLbTime();
                setBangLuong();
            }
        }
        private void setBangLuong()
        {
            float TongCong = 0;
            foreach (ChamCong cc in listChamCong)
            {
                if (cc.MaNV == this.currentId && cc.NgayCham.ToString("MM-yyyy").Equals(time.ToString("MM-yyyy")))
                {
                    TongCong += getTongCong(cc.TgDen.ToString("HH:mm"), cc.TgVe.ToString("HH:mm"));
                }
            }
            lbTongSoCong.Text = TongCong.ToString();
            lbTongLuong.Text= String.Format("{0:n0}", getTongLuong(this.chucvu, TongCong));
        }
        private void setThongTin()
        {
            lbMaNV.Text = this.currentId.ToString();
            listNhanVien = dao.getListNV();
            foreach(NhanVien nv in listNhanVien)
            {
                if (nv.MaNV == this.currentId)
                {
                    lbTenNV.Text = nv.TenNV;
                    lbPhongBan.Text = nv.PhongBan;
                    lbChucVu.Text = nv.ChucVu;
                    this.chucvu= nv.ChucVu;
                    lbluongCB.Text = String.Format("{0:n0}", getLuongCB(nv.ChucVu));
                    lbTroCap.Text= String.Format("{0:n0}", getTroCap(nv.ChucVu));
                    lbHSLuong.Text = nv.HsLuong.ToString();
                }
            }
        }
        public double getTongLuong(string ChucVu, float soNgayLamViec)
        {
            if (ChucVu.Equals("Nhân Viên"))
            {
                return (8000000 / 26) * soNgayLamViec;
            }
            else if (ChucVu.Equals("Trưởng Phòng"))
            {
                return (10000000 / 26) * soNgayLamViec;
            }
            else if (ChucVu.Equals("Giám Đốc"))
            {
                return (20000000);
            }
            else
            {
                return (5000000 / 26) * soNgayLamViec;
            }
        }
        private float getLuongCB(string chucvu)
        {
            if (chucvu.Equals("Giám Đốc"))
            {
                return 20000000;
            }
            else if(chucvu.Equals("Trưởng Phòng"))
            {
                return 10000000;
            }
            else if (chucvu.Equals("Nhân Viên"))
            {
                return 8000000;
            }
            else
            {
                return 5000000;
            }
        }
        private float getTroCap(string chucvu)
        {
            if (chucvu.Equals("Giám Đốc"))
            {
                return 0;
            }
            else if (chucvu.Equals("Trưởng Phòng"))
            {
                return 2000000;
            }
            else if (chucvu.Equals("Nhân Viên"))
            {
                return 0;
            }
            else
            {
                return 0;
            }
        }
        public float getTongCong(string timeDen, string timeVe)
        {
            string[] tgDen = timeDen.Split(':');
            string[] tgVe = timeVe.Split(':');
            float kq = float.Parse(tgVe[0]) * 60 + float.Parse(tgVe[1]) - float.Parse(tgDen[0]) * 60 - float.Parse(tgDen[1]);
            if ((kq / 60 - 2) > 0)
            {
                return (kq / 60 - 2) / 8.0f;
            }
            else
            {
                return 0;
            }
        }
    }
}
