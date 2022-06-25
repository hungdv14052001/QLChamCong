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
    public partial class fNhanVienHoSo : Form
    {
        private int currentId;
        DAO dao = new DAO();
        List<NhanVien> listNhanVien = new List<NhanVien>();
        public fNhanVienHoSo()
        {
            InitializeComponent();
        }
        public fNhanVienHoSo(int Ma) : this()
        {
            this.currentId = Ma;
            listNhanVien = dao.getListNV();
            setHoSo();
            isUpdateFalse();
        }
        private void isUpdateTrue()
        {
            txtDiaChi.Enabled = true;
            dtNS.Enabled = true;
        }
        private void isUpdateFalse()
        {
            txtMaNV.Enabled = false;
            txtMaNV.Enabled = false;
            txtTenNV.Enabled = false;
            txtCCCD.Enabled = false;
            txtDiaChi.Enabled = false;
            txtHSL.Enabled = false;
            dtNS.Enabled = false;
            txtChucVu.Enabled = false;
            txtGioiTinh.Enabled = false;
            txtPhongBan.Enabled = false;
        }
        private void setHoSo()
        {
            string Dir = System.IO.Directory.GetCurrentDirectory();
            Dir = Dir.Remove(Dir.Length - 9, 9);

            foreach (NhanVien nv in listNhanVien)
            {
                if (nv.MaNV == this.currentId)
                {
                    txtMaNV.Text = nv.MaNV.ToString();
                    txtTenNV.Text = nv.TenNV;
                    txtCCCD.Text = nv.Cccd;
                    txtDiaChi.Text = nv.DiaChi;
                    txtHSL.Text = nv.HsLuong.ToString();
                    dtNS.Value = nv.NgaySinh;
                    txtChucVu.Text = nv.ChucVu;
                    txtGioiTinh.Text = getGT(nv.GioiTinh);
                    txtPhongBan.Text = nv.PhongBan;
                    try
                    {
                        pbHA.Image = Image.FromFile(@"" + Dir + @"imgNV\" + nv.HinhAnh);
                    }
                    catch (Exception exp)
                    {
                        pbHA.Image = Properties.Resources.User_Administrator_Blue_icon;
                    }

                }
            }
        }
        private string getGT(bool gt)
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

        private void button5_Click(object sender, EventArgs e)
        {
            isUpdateTrue();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cập nhật hồ sơ thành công!");
            isUpdateFalse();
        }
    }
}
