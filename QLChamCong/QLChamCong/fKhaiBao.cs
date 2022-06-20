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

namespace QLChamCong
{
    public partial class fKhaiBao : Form
    {
        string str = @"Data Source=DESKTOP-LICKT66;Initial Catalog=QLChamCong;Integrated Security=True";
        SqlConnection con;
        SqlCommand com = new SqlCommand();
        SqlDataAdapter dad = new SqlDataAdapter();
        List<PhongBan> listPB = new List<PhongBan>();
        List<ChucVu> listCV = new List<ChucVu>();
        private DAO dao = new DAO();

        public fKhaiBao()
        {
            InitializeComponent();
            listCV = dao.getListChucVu();
            listPB = dao.getListPhongBan();
            con = new SqlConnection(str);
            con.Open();
            loadBangPB();
            loadBangCV();
            reset();
        }
        public void loadBangPB()
        {
            listPB = dao.getListPhongBan();
            List<Label> listlb = this.pnBangPB.Controls.OfType<Label>().ToList();
            foreach (Label l in listlb)
            {
                pnBangPB.Controls.Remove(l);
            }
            int i = 0;
            foreach (PhongBan pb in listPB)
            {
                Label lbMaPB = setLb("P " + pb.MaPB.ToString(), pb.MaPB.ToString(), 150, 24, 0, 24 * i);
                pnBangPB.Controls.Add(lbMaPB);
                Label lbTenPB = setLb("P " + pb.MaPB.ToString(), pb.TenPB, 328, 24, 150, 24 * i);
                pnBangPB.Controls.Add(lbTenPB);
                i++;
            }
            //com = con.CreateCommand();
            //com.CommandText = "select * from tblPhongBan";
            //dad.SelectCommand = com;
            //DataTable dt = new DataTable();
            //dad.Fill(dt);
            //int i = 0;
            //foreach(DataRow r in dt.Rows)
            //{
            //    Label lbMaPB = setLb("P "+r["MaPB"].ToString(), r["MaPB"].ToString(), 150, 24, 0, 24 * i);
            //    pnBangPB.Controls.Add(lbMaPB);
            //    Label lbTenPB = setLb("P " + r["MaPB"].ToString(), r["TenPB"].ToString(), 328, 24, 150, 24 * i);
            //    pnBangPB.Controls.Add(lbTenPB);
            //    i++;
            //}
        }
        public void loadBangCV()
        {
            listCV = dao.getListChucVu();
            List<Label> listlb = this.pnBangCV.Controls.OfType<Label>().ToList();
            foreach (Label l in listlb)
            {
                pnBangCV.Controls.Remove(l);
            }
            int i = 0;
            foreach (ChucVu cv in listCV)
            {
                Label lbMaCV = setLb("C " + cv.MaCV.ToString(), cv.MaCV.ToString(), 150, 24, 0, 24 * i);
                pnBangCV.Controls.Add(lbMaCV);
                Label lbTenCV = setLb("C " + cv.MaCV.ToString(), cv.TenCV, 330, 24, 150, 24 * i);
                pnBangCV.Controls.Add(lbTenCV);
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
            lb.Click += new EventHandler(lb_Click);
            return lb;
        }
        private void lb_Click(object sender, EventArgs e)
        {
            var lb = sender as Label;
            
            List<Label> listlb = this.pnBangPB.Controls.OfType<Label>().ToList();
            foreach (Label l in listlb)
            {
                l.BackColor = Color.White;
            }
            foreach (Label l in listlb)
            {
                if (lb.Tag.Equals(l.Tag))
                {
                    l.BackColor = Color.FromArgb(92, 153, 215);
                }
            }
            List<Label> listlbc = this.pnBangCV.Controls.OfType<Label>().ToList();
            foreach (Label l in listlbc)
            {
                l.BackColor = Color.White;
            }
            foreach (Label l in listlbc)
            {
                if (lb.Tag.Equals(l.Tag))
                {
                    l.BackColor = Color.FromArgb(92, 153, 215);
                }
            }
            setData(lb.Tag.ToString());
        }
        public void setData(string t)
        {
            listCV = dao.getListChucVu();
            listPB = dao.getListPhongBan();
            string[] T = t.Split(' ');
            if (T[0].Equals("P"))
            {
                foreach(PhongBan pb in listPB)
                {
                    if (T[1].Equals(pb.MaPB.ToString()))
                    {
                        txtMaPB.Text = pb.MaPB.ToString();
                        txtMaPB.Enabled = false;
                        txtTenPB.Text = pb.TenPB;
                    }
                }
            }
            else
            {
                foreach (ChucVu cv in listCV)
                {
                    if (T[1].Equals(cv.MaCV.ToString()))
                    {
                        txtMaCV.Text = cv.MaCV.ToString();
                        txtMaCV.Enabled = false;
                        txtTenCV.Text = cv.TenCV;
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string TenPB = txtTenPB.Text;
            if (TenPB.Length==0||txtMaPB.Text.Length!=0)
            {
                MessageBox.Show("Vui lòng làm tươi rồi nhập thông tin để thêm");
                return;
            }
            try
            {
                PhongBan pb = new PhongBan(0, TenPB);
                dao.addPhongBan(pb);
                MessageBox.Show("Thêm thành công");
                loadBangPB();
                reset();
            }
            catch
            {
                MessageBox.Show("Thêm thất bại");
            }
        }
        public void reset()
        {
            txtMaCV.Text = "";
            txtTenCV.Text = "";
            txtMaPB.Text = "";
            txtTenPB.Text = "";
            txtMaPB.Enabled = false;
            txtMaCV.Enabled = false;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            loadBangPB();
            reset();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            loadBangCV();
            reset();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtMaPB.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng chọn Phòng Ban để xóa!");
                return;
            }
            try
            {
                PhongBan pb = new PhongBan(int.Parse(txtMaPB.Text), txtTenPB.Text);
                dao.deletePhongBan(pb);
                MessageBox.Show("Xóa thành công");
                loadBangPB();
                reset();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Xóa thất bại");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtMaPB.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng chọn Phòng Ban để sửa!");
                return;
            }
            string TenPB = txtTenPB.Text;
            if (TenPB.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập thông tin để sửa");
                return;
            }
            try
            {
                PhongBan pb = new PhongBan(int.Parse(txtMaPB.Text), TenPB);
                dao.updatePhongBan(pb);
                MessageBox.Show("Sửa thành công");
                loadBangPB();
                reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sửa thất bại");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string TenCV = txtTenCV.Text;
            if (TenCV.Length == 0 || txtMaCV.Text.Length != 0)
            {
                MessageBox.Show("Vui lòng làm tươi rồi nhập thông tin để thêm");
                return;
            }
            try
            {
                ChucVu cv = new ChucVu(0, txtTenCV.Text);
                dao.addChucVu(cv);
                MessageBox.Show("Thêm thành công");
                loadBangCV();
                reset();
            }
            catch
            {
                MessageBox.Show("Thêm thất bại");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtMaCV.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng chọn Chức Vụ để xóa!");
                return;
            }
            try
            {
                ChucVu cv = new ChucVu(int.Parse(txtMaCV.Text), txtTenCV.Text);
                dao.deleteChucVu(cv);
                MessageBox.Show("Xóa thành công");
                loadBangCV();
                reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa thất bại");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMaCV.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng chọn Chức Vụ để sửa!");
                return;
            }
            string TenCV = txtTenCV.Text;
            if (TenCV.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập thông tin để sửa");
                return;
            }
            try
            {
                ChucVu cv = new ChucVu(int.Parse(txtMaCV.Text), TenCV);
                dao.updateChucVu(cv);
                MessageBox.Show("Sửa thành công");
                loadBangCV();
                reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sửa thất bại");
            }
        }
    }
}
