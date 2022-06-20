using QLChamCong.Controller;
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
    public partial class fBaoCao : Form
    {
        DAO dao = new DAO();
        List<NhanVien> listNhanVien = new List<NhanVien>();
        List<ChamCong> listChamCong = new List<ChamCong>();
        public fBaoCao()
        {
            InitializeComponent();
            listChamCong = dao.getListChamCong();
            listNhanVien = dao.getListNV();
            dtTime.CustomFormat = "MM/yyyy";
        }
        public void loadBangCong(DateTime time)
        {
            listNhanVien = dao.getListNV();
            List<Label> listlb = this.pnBangLuong.Controls.OfType<Label>().ToList();
            foreach (Label l in listlb)
            {
                pnBangLuong.Controls.Remove(l);
            }
            int i = 0;
            foreach (NhanVien nv in listNhanVien)
            {
                Label lbMaNV = setLb(nv.MaNV.ToString(), nv.MaNV.ToString(), 53, 24, 0, 24 * i);
                pnBangLuong.Controls.Add(lbMaNV);
                Label lbTenNV = setLb(nv.MaNV.ToString(), nv.TenNV, 149, 24, 53, 24 * i);
                pnBangLuong.Controls.Add(lbTenNV);
                Label lbNgayLam = setLb(nv.MaNV.ToString(), getSoNgayLam(nv.MaNV, time).ToString(), 106, 24, 202, 24 * i);
                pnBangLuong.Controls.Add(lbNgayLam);
                Label lbNgayNghi = setLb(nv.MaNV.ToString(), (26 - getSoNgayLam(nv.MaNV, time)).ToString(), 106, 24, 308, 24 * i);
                pnBangLuong.Controls.Add(lbNgayNghi);
                Label lbHSLuong = setLb(nv.MaNV.ToString(), nv.HsLuong.ToString(), 83, 24, 414, 24 * i);
                pnBangLuong.Controls.Add(lbHSLuong);
                Label lbLuongCB = setLb(nv.MaNV.ToString(), getLuongCB(nv.ChucVu), 83, 24, 497, 24 * i);
                pnBangLuong.Controls.Add(lbLuongCB);
                Label lbThuong = setLb(nv.MaNV.ToString(), "0", 83, 24, 580, 24 * i);
                pnBangLuong.Controls.Add(lbThuong);
                Label lbPhat = setLb(nv.MaNV.ToString(), "0", 83, 24, 663, 24 * i);
                pnBangLuong.Controls.Add(lbPhat);
                Label lbTongLuong = setLb(nv.MaNV.ToString(), String.Format("{0:n0}", getTongLuong(nv.ChucVu, getSoNgayLam(nv.MaNV, time))), 83, 24, 746, 24 * i); ;
                pnBangLuong.Controls.Add(lbTongLuong);
                i++;
            }
        }
        public double getTongLuong(string ChucVu, int soNgayLamViec)
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
        public string getLuongCB(string ChucVu)
        {
            if (ChucVu.Equals("Nhân Viên"))
            {
                return "8,000,000";
            }
            else if (ChucVu.Equals("Trưởng Phòng"))
            {
                return "10,000,000";
            }
            else if (ChucVu.Equals("Giám Đốc"))
            {
                return "20,000,000";
            }
            else
            {
                return "5,000,000";
            }
        }
        public int getSoNgayLam(int MaNV, DateTime time)
        {
            listChamCong = dao.getListChamCong();
            int result = 0;
            foreach (ChamCong cc in listChamCong)
            {
                if (cc.MaNV == MaNV && cc.NgayCham.ToString("MM-yyyy").Equals(time.ToString("MM-yyyy")))
                {
                    result++;
                }
            }
            return result;
        }
        private Label setLb(string Tag, string Text, int w, int h, int x, int y)
        {
            Label lb = new Label();
            lb.Tag = Tag;
            lb.Text = Text;
            lb.Size = new Size(w, h);
            lb.Location = new Point(x, y);
            lb.BackColor = Color.White;
            lb.TextAlign = ContentAlignment.MiddleLeft;
            lb.BorderStyle = BorderStyle.FixedSingle;
            return lb;
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            DateTime time = dtTime.Value;
            loadBangCong(time);
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {

        }
    }
}
