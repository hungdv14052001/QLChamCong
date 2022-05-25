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
    public partial class fQLNhanVien : Form
    {
        private List<NhanVien> listNV = new List<NhanVien>();
        private DAO dao = new DAO();
        public fQLNhanVien()
        {
            InitializeComponent();
            listNV = dao.getListNV();
            loadBangNV();

        }
        private void lb_Click(object sender, EventArgs e)
        {
            var lb = sender as Label;
            List<Label> listlb = this.pnBangNV.Controls.OfType<Label>().ToList();
            foreach(Label l in listlb)
            {
                if (lb.Tag.Equals(l.Tag))
                {
                    l.BackColor = Color.FromArgb(92, 153, 215);
                }
            }
        }
        private Label setLb(string Tag, string Text, int w, int h, int x, int y)
        {
            Label lb = new Label();
            lb.Tag = Tag;
            lb.Text = Text;
            lb.Size = new Size(w, h);
            lb.Location = new Point(x, y);
            lb.BackColor = Color.White;
            lb.TextAlign=ContentAlignment.MiddleLeft;
            lb.BorderStyle = BorderStyle.FixedSingle;
            lb.Click+= new EventHandler(lb_Click);
            return lb;
        }
        public void loadBangNV()
        {
            listNV = dao.getListNV();
            int i = 0;
            foreach(NhanVien nv in listNV)
            {
                i++;
                Label lbMaNV = setLb(nv.MaNV.ToString(), nv.MaNV.ToString(), 53, 24, 0, 24*i);
                pnBangNV.Controls.Add(lbMaNV);
                Label lbTenNV = setLb(nv.MaNV.ToString(), nv.TenNV, 149, 24, 53, 24 * i);
                pnBangNV.Controls.Add(lbTenNV);
                Label lbCCCD = setLb(nv.MaNV.ToString(), nv.Cccd, 113, 24, 202, i * 24);
                pnBangNV.Controls.Add(lbCCCD);
                Label lbNgaySinh = setLb(nv.MaNV.ToString(), nv.NgaySinh.ToString("dd-MM-yyyy"), 90, 24, 315, i * 24);
                pnBangNV.Controls.Add(lbNgaySinh);
                Label lbGioiTinh = setLb(nv.MaNV.ToString(), getGT(nv.GioiTinh), 76, 24, 405, 24 * i);
                pnBangNV.Controls.Add(lbGioiTinh);
                Label lbDiaChi = setLb(nv.MaNV.ToString(), nv.DiaChi, 238, 24, 481, 24 * i);
                pnBangNV.Controls.Add(lbDiaChi);
                Label lbPhongBan = setLb(nv.MaNV.ToString(), nv.PhongBan, 82, 24, 719, 24 * i);
                pnBangNV.Controls.Add(lbPhongBan);
                Label lbChucVu = setLb(nv.MaNV.ToString(), nv.ChucVu, 83, 24, 801, 24 * i);
                pnBangNV.Controls.Add(lbChucVu);
                Label lbHSLuong = setLb(nv.MaNV.ToString(), nv.HsLuong.ToString(), 82, 24, 884, 24 * i);
                pnBangNV.Controls.Add(lbHSLuong);
            }
        }
        public string getGT(bool gt)
        {
            if (gt)
            {
                return "Nam";
            }
            else
            {
                return "Nữ";
            }
        }
        private void fQLNhanVien_Load(object sender, EventArgs e)
        {

        }
    }
}
