using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QLChamCong.Model;
using QLChamCong.Controller;
using System.Globalization;

namespace QLChamCong
{
    public partial class fChamCong : Form
    {
        string str = @"Data Source=DESKTOP-FA5AISU\SQLEXPRESS;Initial Catalog=QLChamCong;Integrated Security=True";
        SqlConnection con;
        SqlCommand com = new SqlCommand();
        SqlDataAdapter dad = new SqlDataAdapter();
        private List<NhanVien> listNV = new List<NhanVien>();
        private DAO dao = new DAO();
        public fChamCong()
        {
            InitializeComponent();
            con = new SqlConnection(str);
            con.Open();
            setCbChonDuyet();
            listNV = dao.getListNV();
            loadBang();
        }
        public void setCbChonDuyet()
        {
            cbChonDuyet.Items.Add("Mã NV");
            cbChonDuyet.SelectedIndex = 0;
            cbChonDuyet.Enabled = false;
            txtTenNV.Enabled = false;
            lbTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void rdbTimeIn_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTimeIn.Checked)
            {
                rdbTimeOut.Checked = false;
            }
        }

        private void rdbTimeOut_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTimeOut.Checked)
            {
                rdbTimeIn.Checked = false;
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            listNV = dao.getListNV();
            string MaNV = txtMaNV.Text;
            if (MaNV.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập Mã NV!");
                return;
            }
            try
            {
                int.Parse(MaNV);
            }
            catch
            {
                MessageBox.Show("Mã NV phải là số!");
                return;
            }
            bool kt = false;
            foreach (NhanVien nv in listNV)
            {
                if (nv.MaNV == int.Parse(MaNV))
                {
                    txtTenNV.Text = nv.TenNV;
                    kt = true;
                }
            }
            if (!kt)
            {
                MessageBox.Show("Mã NV không tồn tại!");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtTenNV.Text.Length == 0)
            {
                MessageBox.Show("vui lòng nhập mã nhân viên và kiểm tra!");
                return;
            }
            string commandtext = "";
            if (rdbTimeIn.Checked)
            {
                commandtext = "insert into tblChamCong(MaNV, TGDen, TGVe, NgayCham) values(" + txtMaNV.Text + ", '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "', '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

            }
            else
            {
                commandtext = "update tblChamCong set TGVe= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where NgayCham='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and MaNV= " + txtMaNV.Text;
            }
            try
            {

                com = con.CreateCommand();
                com.CommandText = commandtext;
                com.ExecuteNonQuery();
                MessageBox.Show("Chấm thành công!");
                loadBang();
                txtMaNV.Text = "";
                txtTenNV.Text = "";
            }
            catch
            {
                MessageBox.Show("Chấm thất bại!");
            }
        }
        public void loadBang()
        {
            List<Label> listlb = this.pnBangCong.Controls.OfType<Label>().ToList();
            foreach (Label l in listlb)
            {
                pnBangCong.Controls.Remove(l);
            }
            com = con.CreateCommand();
            com.CommandText = "select MaNV, convert(varchar, TGVe, 108) as TGVe, convert(varchar, TGDen, 108) as TGDen,(select tblPhongBan.TenPB from tblNhanVien, tblPhongBan where tblNhanVien.MaNV = tblChamCong.MaNV and tblNhanVien.MaPB= tblPhongBan.MaPB) as TenPB ,(select tblChucVu.TenCV from tblNhanVien, tblChucVu where tblNhanVien.MaNV = tblChamCong.MaNV and tblChucVu.MaCV= tblNhanVien.MaCV) as TenCV, (select TenNV from tblNhanVien where tblNhanVien.MaNV = tblChamCong.MaNV) as TenNV from tblChamCong where NgayCham='"+DateTime.Now.ToString("yyyy-MM-dd")+"'";
            dad.SelectCommand = com;
            DataTable dt = new DataTable();
            dad.Fill(dt);
            int i = 0;
            foreach (DataRow r in dt.Rows) 
            {
                Label lbMaNV = setLb(r["MaNV"].ToString(), r["MaNV"].ToString(), 53, 24, 0, 24 * i);
                pnBangCong.Controls.Add(lbMaNV);
                Label lbTenNV = setLb(r["MaNV"].ToString(), r["TenNV"].ToString(), 149, 24, 53, 24 * i);
                pnBangCong.Controls.Add(lbTenNV);
                Label lbPB = setLb(r["MaNV"].ToString(), r["TenPB"].ToString(), 83, 24, 202, 24 * i);
                pnBangCong.Controls.Add(lbPB);
                Label lbCV = setLb(r["MaNV"].ToString(), r["TenCV"].ToString(), 83, 24, 285, 24 * i);
                pnBangCong.Controls.Add(lbCV);
                Label lbGioVao = setLb(r["MaNV"].ToString(), r["TGDen"].ToString(), 83, 24, 368, 24 * i);
                pnBangCong.Controls.Add(lbGioVao);
                Label lbGioRa = setLb(r["MaNV"].ToString(), r["TGVe"].ToString(), 83, 24, 451, 24 * i);
                pnBangCong.Controls.Add(lbGioRa);
                i++;
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
            lb.TextAlign = ContentAlignment.MiddleLeft;
            lb.BorderStyle = BorderStyle.FixedSingle;
            return lb;
        }
    }
}
