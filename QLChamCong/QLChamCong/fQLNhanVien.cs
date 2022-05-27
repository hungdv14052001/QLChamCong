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
using System.IO;

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
                    dtNS.Value = nv.NgaySinh;
                    cbCV.SelectedItem = nv.ChucVu;
                    cbGT.SelectedItem = nv.GioiTinh;
                    cbPB.SelectedItem = nv.PhongBan;
                    try
                    {
                        pbHA.Image = Image.FromFile(@"" + Dir + @"imgNV\" + nv.HinhAnh);
                    }
                    catch(Exception exp)
                    {
                        pbHA.Image = Properties.Resources.User_Administrator_Blue_icon;
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
        public bool returnGT(string GT)
        {
            if (GT.Equals("Nam"))
            {
                return true;
            }
            else
            {
                return false;
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
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                pbHA.Image = new Bitmap(open.FileName);
                // image file path  
                lbFileName.Text = open.FileName;
            }
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
            lbFileName.Text = "";
            pbHA.Image = Properties.Resources.User_Administrator_Blue_icon;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reset();
            txtMaNV.Enabled = false;
            txtMaNV.Text = getNewMaV().ToString();
        }
        public int getNewMaV()
        {
            com = con.CreateCommand();
            com.CommandText = "select Max(MaNV) as newMaNV from tblNhanVien";
            dad.SelectCommand = com;
            DataTable dt = new DataTable();
            dad.Fill(dt);
            int kq = 0;
            foreach(DataRow r in dt.Rows)
            {
                kq = int.Parse(r["newMaNV"].ToString());
            }
            kq ++;
            return kq;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Enabled)
            {
                MessageBox.Show("Vui lòng thêm mới trước khi lưu");
                return;
            }
            string MaNV = txtMaNV.Text;
            string TenNV = txtTenNV.Text;
            string Cccd = txtCCCD.Text;
            DateTime NS = dtNS.Value;
            bool GT = returnGT(cbGT.SelectedItem.ToString());
            string DC = txtDiaChi.Text;
            string HSLuong = txtHSL.Text;
            if (TenNV.Length == 0 || Cccd.Length == 0 || DC.Length == 0 || HSLuong.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi lưu");
                return;
            }
            bool kt = false;
            try
            {
                int c = int.Parse(Cccd);
                float HS = float.Parse(HSLuong);
                kt =true;
            }
            catch(Exception exp)
            {
                kt = false;
            }
            if (!kt)
            {
                MessageBox.Show("Vui lòng nhập chính xác CCCD và HS Lương");
                return;
            }
            foreach(NhanVien nv in listNV)
            {
                if (nv.MaNV == int.Parse(MaNV))
                {
                    kt = false;
                }
            }
            if (!kt)
            {
                MessageBox.Show("Nhân viên đã tồn tại!");
                return;
            }
            NhanVien newNV = new NhanVien();
            newNV.MaNV = int.Parse(MaNV);
            newNV.TenNV = TenNV;
            newNV.Cccd = Cccd;
            newNV.NgaySinh = NS;
            newNV.GioiTinh = GT;
            newNV.DiaChi = DC;
            newNV.HsLuong = float.Parse(HSLuong);
            newNV.HinhAnh = saveImg();
            dao.addNhanVien(newNV, cbCV.SelectedIndex + 1, cbPB.SelectedIndex + 1);
            MessageBox.Show("Thêm nhân viên thành công");
            reset();
        }
        public string saveImg()
        {
            string kq = "";
            if (lbFileName.Text.Length != 0)
            {
                string Dir = System.IO.Directory.GetCurrentDirectory();
                Dir = Dir.Remove(Dir.Length - 10, 10);
                string newName = Path.GetFileName(lbFileName.Text);
                File.Copy(lbFileName.Text, Dir + "\\imgNV\\" + newName);
                kq = newName;
            }
            return kq;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Enabled&&txtTenNV.Text.Length==0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên trước khi xóa!");
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Sure", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                dao.delNhanVien(int.Parse(txtMaNV.Text));
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Hủy xóa thành công!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Enabled && txtTenNV.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên trước khi sửa!");
                return;
            }
            string MaNV = txtMaNV.Text;
            string TenNV = txtTenNV.Text;
            string Cccd = txtCCCD.Text;
            DateTime NS = dtNS.Value;
            bool GT = returnGT(cbGT.SelectedItem.ToString());
            string DC = txtDiaChi.Text;
            string HSLuong = txtHSL.Text;
            if (TenNV.Length == 0 || Cccd.Length == 0 || DC.Length == 0 || HSLuong.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi sửa");
                return;
            }
            bool kt = false;
            try
            {
                int c = int.Parse(Cccd);
                float HS = float.Parse(HSLuong);
                kt = true;
            }
            catch (Exception exp)
            {
                kt = false;
            }
            if (!kt)
            {
                MessageBox.Show("Vui lòng nhập chính xác CCCD và HS Lương");
                return;
            }
            NhanVien newNV = new NhanVien();
            newNV.MaNV = int.Parse(MaNV);
            newNV.TenNV = TenNV;
            newNV.Cccd = Cccd;
            newNV.NgaySinh = NS;
            newNV.GioiTinh = GT;
            newNV.DiaChi = DC;
            newNV.HsLuong = float.Parse(HSLuong);
            dao.updateNV(newNV, cbCV.SelectedIndex + 1, cbPB.SelectedIndex + 1);
            MessageBox.Show("Sửa nhân viên thành công");
            reset();
        }
    }
}
