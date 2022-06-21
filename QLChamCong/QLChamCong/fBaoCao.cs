using QLChamCong.Controller;
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
    public partial class fBaoCao : Form
    {
        DAO dao = new DAO();
        List<NhanVien> listNhanVien = new List<NhanVien>();
        List<ChamCong> listChamCong = new List<ChamCong>();
        DataTable dataTable = new DataTable();
        private bool isXuatBaoCao = false;
        public fBaoCao()
        {
            InitializeComponent();
            listChamCong = dao.getListChamCong();
            listNhanVien = dao.getListNV();
            dtTime.CustomFormat = "MM/yyyy";
            setData();
        }
        public void setData()
        {
            DataColumn col1 = new DataColumn("MaNV");
            DataColumn col2 = new DataColumn("Ho Ten");
            DataColumn col3 = new DataColumn("Ngay Sinh");
            DataColumn col4 = new DataColumn("Gioi Tinh");
            DataColumn col5 = new DataColumn("Que Quan");
            DataColumn col6 = new DataColumn("Dia chi");
            DataColumn col7 = new DataColumn("Dien Thoai");
            DataColumn col8 = new DataColumn("Email");
            DataColumn col9 = new DataColumn("Tổng");

            dataTable.Columns.Add(col1);
            dataTable.Columns.Add(col2);
            dataTable.Columns.Add(col3);
            dataTable.Columns.Add(col4);
            dataTable.Columns.Add(col5);
            dataTable.Columns.Add(col6);
            dataTable.Columns.Add(col7);
            dataTable.Columns.Add(col8);
            dataTable.Columns.Add(col9);
        }
        public void loadBangCong(DateTime time)
        {
            listNhanVien = dao.getListNV();
            List<Label> listlb = this.pnBangLuong.Controls.OfType<Label>().ToList();
            foreach (Label l in listlb)
            {
                pnBangLuong.Controls.Remove(l);
            }
            int i = 0;
            foreach (NhanVien nv in listNhanVien)
            {
                DataRow dtrow = dataTable.NewRow();
                Label lbMaNV = setLb(nv.MaNV.ToString(), nv.MaNV.ToString(), 53, 24, 0, 24 * i);
                pnBangLuong.Controls.Add(lbMaNV);
                Label lbTenNV = setLb(nv.MaNV.ToString(), nv.TenNV, 149, 24, 53, 24 * i);
                pnBangLuong.Controls.Add(lbTenNV);
                Label lbNgayLam = setLb(nv.MaNV.ToString(), getSoNgayLam(nv.MaNV, time).ToString(), 106, 24, 202, 24 * i);
                pnBangLuong.Controls.Add(lbNgayLam);
                Label lbNgayNghi = setLb(nv.MaNV.ToString(), (26 - getSoNgayLam(nv.MaNV, time)).ToString(), 106, 24, 308, 24 * i);
                pnBangLuong.Controls.Add(lbNgayNghi);
                Label lbHSLuong = setLb(nv.MaNV.ToString(), nv.HsLuong.ToString(), 83, 24, 414, 24 * i);
                pnBangLuong.Controls.Add(lbHSLuong);
                Label lbLuongCB = setLb(nv.MaNV.ToString(), getLuongCB(nv.ChucVu), 83, 24, 497, 24 * i);
                pnBangLuong.Controls.Add(lbLuongCB);
                Label lbThuong = setLb(nv.MaNV.ToString(), "0", 83, 24, 580, 24 * i);
                pnBangLuong.Controls.Add(lbThuong);
                Label lbPhat = setLb(nv.MaNV.ToString(), "0", 83, 24, 663, 24 * i);
                pnBangLuong.Controls.Add(lbPhat);
                Label lbTongLuong = setLb(nv.MaNV.ToString(), String.Format("{0:n0}", getTongLuong(nv.ChucVu, getSoNgayLam(nv.MaNV, time))), 83, 24, 746, 24 * i); ;
                pnBangLuong.Controls.Add(lbTongLuong);
                dtrow[0] = lbMaNV.Text;
                dtrow[1] = lbTenNV.Text;
                dtrow[2] = lbNgayLam.Text;
                dtrow[3] = lbNgayNghi.Text;
                dtrow[4] = lbHSLuong.Text;
                dtrow[5] = lbLuongCB.Text;
                dtrow[6] = lbThuong.Text;
                dtrow[7] = lbPhat.Text;
                dtrow[8] = lbTongLuong.Text;
                dataTable.Rows.Add(dtrow);
                i++;
            }
            isXuatBaoCao = true;
        }
        public double getTongLuong(string ChucVu, int soNgayLamViec)
        {
            if (ChucVu.Equals("Nhân Viên"))
            {
                return (8000000 / 26) * soNgayLamViec;
            }
            else if (ChucVu.Equals("Trưởng Phòng"))
            {
                return (10000000 / 26) * soNgayLamViec;
            }
            else if (ChucVu.Equals("Giám Đốc"))
            {
                return (20000000);
            }
            else
            {
                return (5000000 / 26) * soNgayLamViec;
            }
        }
        public string getLuongCB(string ChucVu)
        {
            if (ChucVu.Equals("Nhân Viên"))
            {
                return "8,000,000";
            }
            else if (ChucVu.Equals("Trưởng Phòng"))
            {
                return "10,000,000";
            }
            else if (ChucVu.Equals("Giám Đốc"))
            {
                return "20,000,000";
            }
            else
            {
                return "5,000,000";
            }
        }
        public int getSoNgayLam(int MaNV, DateTime time)
        {
            listChamCong = dao.getListChamCong();
            int result = 0;
            foreach (ChamCong cc in listChamCong)
            {
                if (cc.MaNV == MaNV && cc.NgayCham.ToString("MM-yyyy").Equals(time.ToString("MM-yyyy")))
                {
                    result++;
                }
            }
            return result;
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
            return lb;
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            DateTime time = dtTime.Value;
            loadBangCong(time);
        }
        public void ExportFile(DataTable dataTable, string sheetName, string title)
        {
            // tạo các đối tượng trong excel
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks oBooks;
            Microsoft.Office.Interop.Excel.Sheets oSheets;
            Microsoft.Office.Interop.Excel.Workbook oBook;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            //tạo mới một excel workbook
            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;
            oBooks = oExcel.Workbooks;
            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));
            oSheets = oBook.Worksheets;
            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oSheet.Name = sheetName;

            //tạo tiêu đề
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "I1");
            head.MergeCells = true;
            head.Value2 = title;
            head.Font.Bold = true;
            head.Font.Name = "Time New Roman";
            head.Font.Size = "20";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            //tạo tiêu đề cột
            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "Mã NV";
            cl1.ColumnWidth = 12;
            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "Ten NV";
            cl2.ColumnWidth = 18;
            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "Số ngày làm";
            cl3.ColumnWidth = 12;
            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
            cl4.Value2 = "số ngày nghỉ";
            cl4.ColumnWidth = 12;
            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
            cl5.Value2 = "Hệ số lương";
            cl5.ColumnWidth = 12;
            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
            cl6.Value2 = "Lương CB";
            cl6.ColumnWidth = 12;
            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");
            cl7.Value2 = "Thưởng";
            cl7.ColumnWidth = 12;
            Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");
            cl8.Value2 = "Phạt";
            cl8.ColumnWidth = 25;
            Microsoft.Office.Interop.Excel.Range cl9 = oSheet.get_Range("I3", "I3");
            cl9.Value2 = "Tổng";
            cl9.ColumnWidth = 25;

            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "H3");
            rowHead.Font.Bold = true;
            //kẻ viền
            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            //Thiết lập màu nền
            rowHead.Interior.ColorIndex = 6;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            //Tạo mảng datatable
            object[,] arr = new object[dataTable.Rows.Count, dataTable.Columns.Count];
            //Chuyển dữ liệu từ Datatable vào mảng đối tượng
            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                DataRow dataRow = dataTable.Rows[row];
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    arr[row, col] = dataRow[col];
                }
            }
            //thiết lập vùng dữ liệu
            int rowStart = 4;
            int columnStart = 1;
            int rowEnd = rowStart + dataTable.Rows.Count - 1;
            int columnEnd = dataTable.Columns.Count;
            //ô điền dữ liệu
            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];
            //ô kết thúc điền dữ liệu
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];
            //Lấy vùng dữ liệu
            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);
            //điền dữ liệu vào vùng đã thiết lập
            range.Value2 = arr;
            //kẻ viền
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            //căn giữa cả bảng
            oSheet.get_Range(c1, c2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            if (isXuatBaoCao)
            {
                ExportFile(dataTable, "Danh Sách", "Danh Sách Sinh Viên");
            }
            else
            {
                MessageBox.Show("Vui lòng thống kê trước khi in báo cáo");
                return;
            }
        }
    }
}
