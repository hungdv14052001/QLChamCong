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
            this.pnView.Controls.Add(fQLNV);
            listform.Add(fQLNV);
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
    }
}
