using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_CuaHangTienLoi
{
    public partial class formThongKe : Form
    {
        DataTable tblTK;
        public formThongKe()
        {
            InitializeComponent();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "exec sp_LayHDTheoNgay '" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "', '" + dtpToDate.Value.ToString("yyyy-MM-dd") + "'";
            tblTK = Class.Functions.GetDataToTable(sql);
            dgvThongKe.DataSource = tblTK; //Nguồn dữ liệu            
            dgvThongKe.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvThongKe.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
            SetNameWidthDgv();

            CultureInfo cul = new CultureInfo("vi-VN");
            txtDoanhThu.Text = DoanhThuTheoDate().ToString("c", cul);
            txtTongHD.Text = TongHoaDonTheoNgay().ToString();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "select MAHDBAN, TENNHANVIEN, NGAYBAN, TENKHACH, TONGTIEN from HDBAN, KHACH, NHANVIEN where HDBAN.MANHANVIEN = NHANVIEN.MANHANVIEN and HDBAN.MAKHACH = KHACH.MAKHACH";
            tblTK = Class.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvThongKe.DataSource = tblTK; //Nguồn dữ liệu            
            dgvThongKe.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvThongKe.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
            SetNameWidthDgv();

            CultureInfo cul = new CultureInfo("vi-VN");
            txtDoanhThu.Text = DoanhThuHoaDon().ToString("c", cul);

            txtTongHD.Text = TongHoaDon().ToString();


        }

        private void SetNameWidthDgv()
        {
            dgvThongKe.Columns["MAHDBAN"].HeaderText = "Mã HĐ";
            dgvThongKe.Columns["TENNHANVIEN"].HeaderText = "Tên Nhân Viên";
            dgvThongKe.Columns["NGAYBAN"].HeaderText = " Ngày Bán";
            dgvThongKe.Columns["TENKHACH"].HeaderText = "Tên Khách";
            dgvThongKe.Columns["TONGTIEN"].HeaderText = "Tổng Tiền";

            dgvThongKe.Columns["MAHDBAN"].Width = 128;
            dgvThongKe.Columns["TENNHANVIEN"].Width = 110;
            dgvThongKe.Columns["NGAYBAN"].Width = 100;
            dgvThongKe.Columns["TENKHACH"].Width = 110;
            dgvThongKe.Columns["TONGTIEN"].Width = 139;
        }

        private long DoanhThuHoaDon()
        {
            string sql;
            sql = "select dbo.TongAllHD()";       
            long tong = int.Parse(Class.Functions.ThucThiTruyVanScalar(sql));
            return tong;

        }

        private long DoanhThuTheoDate()
        {
            string sql;
            sql = "select dbo.TongHDTheoNgay('" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "', '" + dtpToDate.Value.ToString("yyyy-MM-dd") + "')";
            long tong = int.Parse(Class.Functions.ThucThiTruyVanScalar(sql));
            return tong;
        }

        private long TongHoaDon()
        {
            string sql;
            sql = "exec sp_tongSoHoaDon";
            long tong = int.Parse(Class.Functions.ThucThiTruyVanScalar(sql));
            return tong;
        }

        private long TongHoaDonTheoNgay()
        {
            string sql;
            sql = "exec sp_tongSoHDtheoNgay '" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "', '" + dtpToDate.Value.ToString("yyyy-MM-dd") + "'";
            long tong = int.Parse(Class.Functions.ThucThiTruyVanScalar(sql));
            return tong;
        }


        public static long tongTien;
        public static long SoLuongHD;

        private void btnIn_Click(object sender, EventArgs e)
        {
            tongTien = DoanhThuHoaDon();
            SoLuongHD = TongHoaDon();

            formInThongKe inTK = new formInThongKe();
            inTK.ShowDialog();
        }
    }
}
