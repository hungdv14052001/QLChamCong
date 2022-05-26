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
using System.Data;
using System.Data.SqlClient;

namespace QLChamCong
{
    public partial class fQLNhanVien : Form
    {
        string str = @"Data Source=DESKTOP-FA5AISU\SQLEXPRESS;Initial Catalog=QLChamCong;Integrated Security=True";
        SqlConnection con;
        SqlCommand com = new SqlCommand();
        SqlDataAdapter dad = new SqlDataAdapter();
        private List<NhanVien> listNV = new List<NhanVien>();
        private DAO dao = new DAO();
        public fQLNhanVien()
        {
            InitializeComponent();
            listNV = dao.getListNV();
            loadBangNV();
            con = new SqlConnection(str);
            con.Open();

        }
        private void lb_Click(object sender, EventArgs e)
        {
            var lb = sender as Label;
            setNV(int.Parse(lb.Tag.ToString()));
            List<Label> listlb = this.pnBangNV.Controls.OfType<Label>().ToList();
            foreach (Label l in listlb)
            {
                if (!l.Tag.Equals("td"))
                {
                    l.BackColor = Color.White;
                }
            }
            foreach (Label l in listlb)
            {
                if (lb.Tag.Equals(l.Tag))
                {
                    l.BackColor = Color.FromArgb(92, 153, 215);
                }
            }
        }
        public void setNV(int Ma)
        {
            string Dir = System.IO.Directory.GetCurrentDirectory();
            Dir = Dir.Remove(Dir.Length - 9, 9);
            
            foreach (NhanVien nv in listNV)
            {
                if (nv.MaNV == Ma)
                {
                    txtMaNV.Text = nv.MaNV.ToString();
                    txtMaNV.Enabled = false;
                    txtTenNV.Text = nv.TenNV;
                    txtCCCD.Text = nv.Cccd;
                    txtDiaChi.Text = nv.DiaChi;
                    txtHSL.Text = nv.HsLuong.ToString();
                    cbCV.SelectedItem = nv.ChucVu;
                    cbGT.SelectedItem = nv.GioiTinh;
                    cbPB.SelectedItem = nv.PhongBan;
                    try
                    {
                        pbHA.Image = Image.FromFile(@"" + Dir + @"imgNV\" + nv.HinhAnh);
                    }
                    catch(Exception exp)
                    {

                    }
                    
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
            loadCB();
            reset();
        }
        public void loadCB()
        {
            DataTable dt = new DataTable();
            cbCV.Items.Clear();
            com = con.CreateCommand();
            com.CommandText = "select * from tblChucVu";
            dad.SelectCommand = com;
            dad.Fill(dt);
            foreach(DataRow r in dt.Rows)
            {
                cbCV.Items.Add(r["TenCV"].ToString());
            }
            cbPB.Items.Clear();
            com = con.CreateCommand();
            com.CommandText = "select * from tblPhongBan";
            dad.SelectCommand = com;
            dt.Clear();
            dad.Fill(dt);
            foreach (DataRow r in dt.Rows)
            {
                cbPB.Items.Add(r["TenPB"].ToString());
            }
            cbGT.Items.Clear();
            cbGT.Items.Add("Nam");
            cbGT.Items.Add("Nữ");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            reset();
        }
        public void reset()
        {
            loadCB();
            List<Label> listlb = this.pnBangNV.Controls.OfType<Label>().ToList();
            foreach (Label l in listlb)
            {
                if (!l.Tag.Equals("td"))
                {
                    l.BackColor = Color.White;
                }
            }
            loadBangNV();
            txtMaNV.Text = "";
            txtMaNV.Enabled = true;
            txtTenNV.Text = "";
            txtCCCD.Text = "";
            txtDiaChi.Text = "";
            txtHSL.Text = "";
            cbCV.SelectedIndex = 0;
            cbGT.SelectedIndex = 0;
            cbPB.SelectedIndex = 0;
        }
    }
}
