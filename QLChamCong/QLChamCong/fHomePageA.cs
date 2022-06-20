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
    public partial class fHomePageA : Form
    {
        fQLNhanVien fQLNV= new fQLNhanVien() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        fKhaiBao fKB= new fKhaiBao() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        fQLChamCong fQLCC= new fQLChamCong() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        fChamCong fCC = new fChamCong() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        fTinhTienLuong fTTL= new fTinhTienLuong() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        List<Form> listform = new List<Form>();
        public fHomePageA()
        {
            InitializeComponent();
            this.Size = new Size(1245, 750);
            loadView();
        }
        public void loadView()
        {
            fQLNV.FormBorderStyle = (FormBorderStyle)FormBorderStyle.None;
            fKB.FormBorderStyle = (FormBorderStyle)FormBorderStyle.None;
            fQLCC.FormBorderStyle = (FormBorderStyle)FormBorderStyle.None;
            fCC.FormBorderStyle = (FormBorderStyle)FormBorderStyle.None;
            fTTL.FormBorderStyle = (FormBorderStyle)FormBorderStyle.None;
            this.pnView.Controls.Add(fQLNV);
            this.pnView.Controls.Add(fKB);
            this.pnView.Controls.Add(fQLCC);
            this.pnView.Controls.Add(fCC);
            this.pnView.Controls.Add(fTTL);
            listform.Add(fQLNV);
            listform.Add(fKB);
            listform.Add(fQLCC);
            listform.Add(fCC);
            listform.Add(fTTL);
        }
        public void dongtrc()
        {
            pc.Hide();
            lb.Hide();
            foreach(Form x in listform)
            {
                x.Hide();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dongtrc();
            fQLNV.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dongtrc();
            fKB.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dongtrc();
            fQLCC.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dongtrc();
            fCC.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dongtrc();
            fTTL.Show();
        }
    }
}
