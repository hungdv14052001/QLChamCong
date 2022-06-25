using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLChamCong
{
    public partial class fHomePageNV : Form
    {
        private int currentId;
        fNhanVienChamCong fNVCC;
        fNhanVienBangLuong fNVBL;
        fNhanVienHoSo fNVHS;
        List<Form> listform = new List<Form>();
        Thread th;
        public fHomePageNV()
        {
            InitializeComponent();
            this.Size = new Size(1245, 750);
            
        }
        public fHomePageNV(int Ma) : this()
        {
            this.currentId = Ma;
            loadView();
        }
        public void loadView()
        {
            fNVCC = new fNhanVienChamCong(this.currentId) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            fNVBL= new fNhanVienBangLuong(this.currentId) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            fNVHS= new fNhanVienHoSo(this.currentId) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            fNVCC.FormBorderStyle = (FormBorderStyle)FormBorderStyle.None;
            fNVBL.FormBorderStyle = (FormBorderStyle)FormBorderStyle.None;
            fNVHS.FormBorderStyle = (FormBorderStyle)FormBorderStyle.None;
            this.pnView.Controls.Add(fNVCC);
            this.pnView.Controls.Add(fNVBL);
            this.pnView.Controls.Add(fNVHS);
            listform.Add(fNVCC);
            listform.Add(fNVBL);
            listform.Add(fNVHS);
        }
        public void dongtrc()
        {
            pc.Hide();
            foreach (Form x in listform)
            {
                x.Hide();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(Logout);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        public void Logout(object obj)
        {
            Application.Run(new fDangNhap());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dongtrc();
            fNVCC.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dongtrc();
            fNVBL.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dongtrc();
            fNVHS.Show();
        }
    }
}
