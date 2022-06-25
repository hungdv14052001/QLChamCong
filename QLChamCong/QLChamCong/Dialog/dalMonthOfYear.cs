using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLChamCong.Dialog
{
    public partial class dalMonthOfYear : Form
    {
        private int year;
        private int month;
        public int Year { get => year; set => year = value; }
        public int Month { get => month; set => month = value; }

        public dalMonthOfYear()
        {
            InitializeComponent();
            this.year = int.Parse(DateTime.Now.ToString("yyyy"));
            lbYear.Text = this.year.ToString();
            setClick();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.year--;
            lbYear.Text = this.year.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.year++;
            lbYear.Text = this.year.ToString();
        }
        public void setClick()
        {
            List<Label> listLb = this.pnMonth.Controls.OfType<Label>().ToList();
            foreach(Label lb in listLb)
            {
                lb.Click += new EventHandler(lb_Click);
            }
        }
        private void lb_Click(object sender, EventArgs e)
        {
            var currentLabel = sender as Label;
            this.month = int.Parse(currentLabel.Text);
            List<Label> listLb = this.pnMonth.Controls.OfType<Label>().ToList();
            foreach (Label lb in listLb)
            {
                lb.BackColor = Color.White;
            }
            currentLabel.BackColor= Color.FromArgb(92, 153, 215);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
