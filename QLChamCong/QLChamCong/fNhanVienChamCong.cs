using QLChamCong.Controller;
using QLChamCong.Dialog;
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

namespace QLChamCong
{
    public partial class fNhanVienChamCong : Form
    {
        private int currentId;
        private DateTime time = DateTime.Now;
        private int year;
        private int month;
        DAO dao = new DAO();
        List<ChamCong> listChamCong = new List<ChamCong>();
        public fNhanVienChamCong()
        {
            InitializeComponent();
        }
        public fNhanVienChamCong(int Ma) : this()
        {
            this.currentId = Ma;
            setLbTime();
            listChamCong = dao.getListChamCong();
            setBangCong();
            setTongHop();
        }

        public void setLbTime()
        {
            this.lbTime.Text = "Bảng chấm công tháng: " + time.ToString("MM-yyyy");
            this.year = int.Parse(this.time.ToString("yyyy"));
            this.month = int.Parse(this.time.ToString("MM"));
        }
        private void label8_Click(object sender, EventArgs e)
        {
            var dal = new dalMonthOfYear();
            dal.StartPosition = FormStartPosition.CenterParent;
            var result = dal.ShowDialog();
            if (result == DialogResult.OK)
            {
                int year = dal.Year;            
                int month = dal.Month;
                this.time = new DateTime(year, month, 1);
                setLbTime();
                setBangCong();
                setTongHop();
            }
        }
        public int NumDateInMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult.Day;
        }
        public int DateInWeek(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            return (int)dtResult.DayOfWeek;
        }
        private Label setLb(string Tag, string Text, int x, int y)
        {
            Label lb = new Label();
            lb.Tag = Tag;
            lb.Text = Text;
            lb.Size = new Size(90, 73);
            lb.Location = new Point(x, y);
            lb.BackColor = Color.White;
            lb.TextAlign = ContentAlignment.TopLeft;
            lb.BorderStyle = BorderStyle.FixedSingle;
            return lb;
        }
        public void setBangCong()
        {
            listChamCong = dao.getListChamCong();
            int startDay = 1;
            int currentDay = startDay;
            int endDay = NumDateInMonth(this.time);
            int startWeek = DateInWeek(new DateTime(year, month, startDay));
            int currentDayInWeek = startWeek;
            List<Label> listlb = this.pnNgayCong.Controls.OfType<Label>().ToList();
            foreach (Label l in listlb)
            {
                pnNgayCong.Controls.Remove(l);
            }
            for (int i=0; i < 6; i++)
            {
                for(int j= 0; j < 7; j++)
                {
                    if (currentDayInWeek==j)
                    {
                        if (currentDay <= endDay)
                        {
                            ChamCong chamcong = new ChamCong();
                            foreach(ChamCong cc in listChamCong)
                            {
                                if(cc.MaNV==currentId&&cc.NgayCham.ToString("dd-MM-yyyy").Equals(new DateTime(year, month, currentDay).ToString("dd-MM-yyyy"))){
                                    chamcong = cc;
                                }
                            }

                            string text = "";
                            if (chamcong.MaCC != 0)
                            {
                                text = currentDay.ToString() + "\n \n             " + getTongCong(chamcong.TgDen.ToString("HH:mm"), chamcong.TgVe.ToString("HH:mm")) + "\n \n    " + chamcong.TgDen.ToString("HH:mm") + "-" + chamcong.TgVe.ToString("HH:mm");
                            }
                            else
                            {
                                text = currentDay.ToString()+ "\n \n            0";
                            }
                            Label lb = setLb(chamcong.MaCC.ToString(), text, j * 90, i * 73);
                            pnNgayCong.Controls.Add(lb);
                        }
                        else
                        {
                            Label lb = setLb("", "", j * 90, i * 73);
                            lb.BackColor = Color.Gray;
                            pnNgayCong.Controls.Add(lb);
                        }
                        currentDay++;
                        if (currentDayInWeek == 6)
                        {
                            currentDayInWeek =0;
                        }
                        else
                        {
                            currentDayInWeek++;
                        }
                    }
                    else
                    {
                        Label lb = setLb("", "", j * 90, i * 73);
                        lb.BackColor = Color.Gray;
                        pnNgayCong.Controls.Add(lb);
                    }
                }
            }
        }
        public float getTongCong(string timeDen, string timeVe)
        {
            string[] tgDen = timeDen.Split(':');
            string[] tgVe = timeVe.Split(':');
            float kq = float.Parse(tgVe[0]) * 60 + float.Parse(tgVe[1]) - float.Parse(tgDen[0]) * 60 - float.Parse(tgDen[1]);
            if ((kq / 60 - 2) > 0)
            {
                return (kq / 60 - 2) / 8.0f;
            }
            else{
                return 0;
            }
            
        }
        public float getSoGio(string timeDen, string timeVe)
        {
            string[] tgDen = timeDen.Split(':');
            string[] tgVe = timeVe.Split(':');
            float kq = float.Parse(tgVe[0]) * 60 + float.Parse(tgVe[1]) - float.Parse(tgDen[0]) * 60 - float.Parse(tgDen[1]);
            if ((kq / 60 - 2) > 0)
            {
                return (kq / 60 - 2);
            }
            else
            {
                return 0;
            }

        }
        public void setTongHop()
        {
            float TongCong = 0;
            float SoGioLV = 0;
            foreach (ChamCong cc in listChamCong)
            {
                if (cc.MaNV == currentId && cc.NgayCham.ToString("MM-yyyy").Equals(time.ToString("MM-yyyy")))
                {
                    TongCong += getTongCong(cc.TgDen.ToString("HH:mm"), cc.TgVe.ToString("HH:mm"));
                    SoGioLV+= getSoGio(cc.TgDen.ToString("HH:mm"), cc.TgVe.ToString("HH:mm"));
                }
            }
            lbCongLV.Text = TongCong.ToString();
            lbGioLam.Text= SoGioLV.ToString(); 
        }
    }
}
