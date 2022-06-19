using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLChamCong.Model
{
    class PhongBan
    {
        private int maPB;
        private string tenPB;

        public PhongBan()
        {

        }

        public PhongBan(int maPB, string tenPB)
        {
            this.maPB = maPB;
            this.tenPB = tenPB;
        }

        public int MaPB { get => maPB; set => maPB = value; }
        public string TenPB { get => tenPB; set => tenPB = value; }
    }
}
