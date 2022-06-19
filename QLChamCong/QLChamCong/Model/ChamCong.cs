using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLChamCong.Model
{
    class ChamCong
    {
        private int maCC;
        private int maNV;
        private DateTime tgDen;
        private DateTime tgVe;
        private DateTime ngayCham;

        public ChamCong()
        {

        }

        public ChamCong(int maCC, int maNV, DateTime tgDen, DateTime tgVe, DateTime ngayCham)
        {
            this.maCC = maCC;
            this.maNV = maNV;
            this.tgDen = tgDen;
            this.tgVe = tgVe;
            this.ngayCham = ngayCham;
        }

        public int MaCC { get => maCC; set => maCC = value; }
        public int MaNV { get => maNV; set => maNV = value; }
        public DateTime TgDen { get => tgDen; set => tgDen = value; }
        public DateTime TgVe { get => tgVe; set => tgVe = value; }
        public DateTime NgayCham { get => ngayCham; set => ngayCham = value; }
    }
}
