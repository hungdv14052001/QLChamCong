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
    public partial class fQLChamCong : Form
    {
        string str = @"Data Source=DESKTOP-LICKT66;Initial Catalog=QLChamCong;Integrated Security=True";
        SqlConnection con;
        SqlCommand com = new SqlCommand();
        SqlDataAdapter dad = new SqlDataAdapter();
        private List<NhanVien> listNV = new List<NhanVien>();
        private DAO dao = new DAO();
        public fQLChamCong()
        {
            InitializeComponent();
            listNV = dao.getListNV();
            con = new SqlConnection(str);
            con.Open();
            loadBangCong();
            pnBangCong.AutoScroll = true;
            lbTime.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }
        public void loadBangCong()
        {
            listNV = dao.getListNV();
            int i = 0;
            List<Label> listlb = this.pnBangCong.Controls.OfType<Label>().ToList();
            foreach (Label l in listlb)
            {
                pnBangCong.Controls.Remove(l);
            }
            foreach (NhanVien nv in listNV)
            {
                
                Label lbMaNV = setLb(nv.MaNV.ToString(), nv.MaNV.ToString(), 53, 24, 0, 24 * i);
                pnBangCong.Controls.Add(lbMaNV);
                Label lbTenNV = setLb(nv.MaNV.ToString(), nv.TenNV, 149, 24, 53, 24 * i);
                pnBangCong.Controls.Add(lbTenNV);
                Label lbPhongBan = setLb(nv.MaNV.ToString(), nv.PhongBan, 83, 24, 203, 24 * i);
                pnBangCong.Controls.Add(lbPhongBan);
                Label lbChucVu = setLb(nv.MaNV.ToString(), nv.ChucVu, 83, 24, 285, 24 * i);
                pnBangCong.Controls.Add(lbChucVu);
                Label lbHSLuong = setLb(nv.MaNV.ToString(), nv.HsLuong.ToString(), 83, 24, 368, 24 * i);
                pnBangCong.Controls.Add(lbHSLuong);
                Label lbSoNgay = setLb(nv.MaNV.ToString(), getSoNgay(nv.MaNV), 150, 24, 451, 24 * i);
                pnBangCong.Controls.Add(lbSoNgay);
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
            
            List<Label> listlb = this.pnBangCong.Controls.OfType<Label>().ToList();
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
            foreach(NhanVien nv in listNV)
            {
                if (nv.MaNV.ToString().Equals(lb.Tag))
                {
                    txtMaNV.Text = nv.MaNV.ToString();
                    txtTenNV.Text = nv.TenNV;
                }
            }
        }
        public string getSoNgay(int MaNV)
        {
            com = con.CreateCommand();
            com.CommandText = "select count(MaCC) as SoNgay from tblChamCong where MaNV=" + MaNV.ToString();
            dad.SelectCommand = com;
            DataTable dt = new DataTable();
            dad.Fill(dt);
            string kq = "0";
            foreach(DataRow r in dt.Rows)
            {
                kq = r["SoNgay"].ToString();
            }
            return kq;
        }

        private void btnXemCong_Click(object sender, EventArgs e)
        {
            string MaNV = txtMaNV.Text;
            if (MaNV.Length == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xem chi tiết công!");
                return;
            }
            string startTime = dtStart.Value.ToString("yyyy-MM-dd");
            string finishTime = dtFinish.Value.ToString("yyyy-MM-dd");
            com = con.CreateCommand();
            com.CommandText = "select MaNV, NgayCham, convert(varchar, TGVe, 108) as TGVe, convert(varchar, TGDen, 108) as TGDen, (select TenNV from tblNhanVien where tblNhanVien.MaNV = tblChamCong.MaNV) as TenNV from tblChamCong where NgayCham>='" + startTime + "' and NgayCham<='" + finishTime + "' and MaNV= "+ MaNV;
            dad.SelectCommand = com;
            DataTable dt = new DataTable();
            dad.Fill(dt);
            setCTBC(dt);
        }
        public void setCTBC(DataTable dt)
        {
            List<Label> listlb = this.pnChiTietBangCong.Controls.OfType<Label>().ToList();
            foreach (Label l in listlb)
            {
                pnChiTietBangCong.Controls.Remove(l);
            }
            int i = 0;
            foreach (DataRow r in dt.Rows)
            {
                Label lbMaNV = setLb(r["MaNV"].ToString(), r["MaNV"].ToString(), 53, 24, 0, 24 * i);
                pnChiTietBangCong.Controls.Add(lbMaNV);
                Label lbTenNV = setLb(r["MaNV"].ToString(), r["TenNV"].ToString(), 149, 24, 53, 24 * i);
                pnChiTietBangCong.Controls.Add(lbTenNV);
                Label lbNgayCham = setLb(r["MaNV"].ToString(), r["NgayCham"].ToString(), 83, 24, 202, 24 * i);
                pnChiTietBangCong.Controls.Add(lbNgayCham);
                Label lbThu = setLb(r["MaNV"].ToString(), getThu(r["NgayCham"].ToString()), 60, 24, 285, 24 * i);
                pnChiTietBangCong.Controls.Add(lbThu);
                Label lbVao = setLb(r["MaNV"].ToString(), r["TGDen"].ToString(), 60, 24, 345, 24 * i);
                pnChiTietBangCong.Controls.Add(lbVao);
                Label lbRa = setLb(r["MaNV"].ToString(), r["TGVe"].ToString(), 60, 24, 405, 24 * i);
                pnChiTietBangCong.Controls.Add(lbRa);
                Label lbTre = setLb(r["MaNV"].ToString(), getTre(r["TGDen"].ToString()).ToString(), 60, 24, 465, 24 * i);
                pnChiTietBangCong.Controls.Add(lbTre);
                Label lbSom = setLb(r["MaNV"].ToString(), getSom(r["TGVe"].ToString()).ToString(), 60, 24, 525, 24 * i);
                pnChiTietBangCong.Controls.Add(lbSom);
                Label lbGio = setLb(r["MaNV"].ToString(), getSoGio(r["TGDen"].ToString(), r["TGVe"].ToString()).ToString(), 60, 24, 585, 24 * i);
                pnChiTietBangCong.Controls.Add(lbGio);
                Label lbTong = setLb(r["MaNV"].ToString(), getTongCong(r["TGDen"].ToString(), r["TGVe"].ToString()).ToString(), 60, 24, 645, 24 * i);
                pnChiTietBangCong.Controls.Add(lbTong);
                i++;
            }
        }
        public float getTongCong(string timeDen, string timeVe)
        {
            string[] tgDen = timeDen.Split(':');
            string[] tgVe = timeVe.Split(':');
            float kq = float.Parse(tgVe[0]) * 60 + float.Parse(tgVe[1]) - float.Parse(tgDen[0]) * 60 - float.Parse(tgDen[1]);
            return (kq / 60 - 2)/8.0f;
        }
        public float getSoGio(string timeDen, string timeVe)
        {
            string[] tgDen = timeDen.Split(':');
            string[] tgVe = timeVe.Split(':');
            float kq= float.Parse(tgVe[0]) * 60 + float.Parse(tgVe[1])- float.Parse(tgDen[0]) * 60 - float.Parse(tgDen[1]);
            return (kq/60 - 2.0f);
        }
        public int getTre(string time)
        {
            string[] tg = time.Split(':');
            int kq =int.Parse(tg[0]) * 60 + int.Parse(tg[1]) - 7 * 60;
            if (kq > 0)
            {
                return kq;
            }
            else
            {
                return 0;
            }
        }
        public int getSom(string time)
        {
            string[] tg = time.Split(':');
            int kq = 17 * 60 - int.Parse(tg[0]) * 60 + int.Parse(tg[1]);
            if (kq > 0)
            {
                return kq;
            }
            else
            {
                return 0;
            }
        }
        public string getThu(string d)
        {
            string[] sDate = d.Split('/', ' ', ':');
            DateTime date = new DateTime(int.Parse(sDate[2]), int.Parse(sDate[0]), int.Parse(sDate[1]));
            return date.DayOfWeek.ToString() ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
