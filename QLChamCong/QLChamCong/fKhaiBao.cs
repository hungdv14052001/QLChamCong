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

namespace QLChamCong
{
    public partial class fKhaiBao : Form
    {
        string str = @"Data Source=DESKTOP-FA5AISU\SQLEXPRESS;Initial Catalog=QLChamCong;Integrated Security=True";
        SqlConnection con;
        SqlCommand com = new SqlCommand();
        SqlDataAdapter dad = new SqlDataAdapter();
        public fKhaiBao()
        {
            InitializeComponent();
            con = new SqlConnection(str);
            con.Open();
            loadBangPB();
            loadBangCV();
            reset();
        }
        public void loadBangPB()
        {
            List<Label> listlb = this.pnBangPB.Controls.OfType<Label>().ToList();
            foreach (Label l in listlb)
            {
                pnBangPB.Controls.Remove(l);
            }
            com = con.CreateCommand();
            com.CommandText = "select * from tblPhongBan";
            dad.SelectCommand = com;
            DataTable dt = new DataTable();
            dad.Fill(dt);
            int i = 0;
            foreach(DataRow r in dt.Rows)
            {
                Label lbMaPB = setLb("P "+r["MaPB"].ToString(), r["MaPB"].ToString(), 150, 24, 0, 24 * i);
                pnBangPB.Controls.Add(lbMaPB);
                Label lbTenPB = setLb("P " + r["MaPB"].ToString(), r["TenPB"].ToString(), 328, 24, 150, 24 * i);
                pnBangPB.Controls.Add(lbTenPB);
                i++;
            }
        }
        public void loadBangCV()
        {
            List<Label> listlb = this.pnBangCV.Controls.OfType<Label>().ToList();
            foreach (Label l in listlb)
            {
                pnBangCV.Controls.Remove(l);
            }
            com = con.CreateCommand();
            com.CommandText = "select * from tblChucVu";
            dad.SelectCommand = com;
            DataTable dt = new DataTable();
            dad.Fill(dt);
            int i = 0;
            foreach (DataRow r in dt.Rows)
            {
                Label lbMaCV = setLb("C " + r["MaCV"].ToString(), r["MaCV"].ToString(), 150, 24, 0, 24 * i);
                pnBangCV.Controls.Add(lbMaCV);
                Label lbTenCV = setLb("C " + r["MaCV"].ToString(), r["TenCV"].ToString(), 330, 24, 150, 24 * i);
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
            string[] T = t.Split(' ');

            if (T[0].Equals("P"))
            {
                com = con.CreateCommand();
                com.CommandText = "select * from tblPhongBan";
                dad.SelectCommand = com;
                DataTable dt = new DataTable();
                dad.Fill(dt);
                foreach(DataRow r in dt.Rows)
                {
                    if (T[1].Equals(r["MaPB"].ToString()))
                    {
                        txtMaPB.Text = r["MaPB"].ToString();
                        txtMaPB.Enabled = false;
                        txtTenPB.Text = r["TenPB"].ToString();
                    }
                }
            }
            else
            {
                com = con.CreateCommand();
                com.CommandText = "select * from tblChucVu";
                dad.SelectCommand = com;
                DataTable dt = new DataTable();
                dad.Fill(dt);
                foreach (DataRow r in dt.Rows)
                {
                    if (T[1].Equals(r["MaCV"].ToString()))
                    {
                        txtMaCV.Text = r["MaCV"].ToString();
                        txtMaCV.Enabled = false;
                        txtTenCV.Text = r["TenCV"].ToString();
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
                com = con.CreateCommand();
                com.CommandText = "insert into tblPhongBan(TenPB) values(N'" + TenPB + "')";
                com.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công");
                loadBangPB();
                reset();
            }
            catch(Exception ex)
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
                com = con.CreateCommand();
                com.CommandText = "delete from tblPhongBan where MaPB=" + txtMaPB.Text;
                com.ExecuteNonQuery();
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
                com = con.CreateCommand();
                com.CommandText = "update tblPhongBan set TenPB= N'"+TenPB+"' where MaPB= "+ txtMaPB.Text;
                com.ExecuteNonQuery();
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
                com = con.CreateCommand();
                com.CommandText = "insert into tblChucVu(TenCV) values(N'" + TenCV + "')";
                com.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công");
                loadBangCV();
                reset();
            }
            catch (Exception ex)
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
                com = con.CreateCommand();
                com.CommandText = "delete from tblChucVu where MaCV=" + txtMaCV.Text;
                com.ExecuteNonQuery();
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
                com = con.CreateCommand();
                com.CommandText = "update tblChucVu set TenCV= N'" + TenCV + "' where MaCV= " + txtMaCV.Text;
                com.ExecuteNonQuery();
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
