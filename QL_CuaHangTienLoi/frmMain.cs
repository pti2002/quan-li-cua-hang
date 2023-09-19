using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_CuaHangTienLoi
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect(); //Mở kết nối
        }

        private void btnLoaiHang_Click(object sender, EventArgs e)
        {
            formDMLoaiHang lh = new formDMLoaiHang();
            lh.Show();
        }

        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            formDMHangHoa hh = new formDMHangHoa();
            hh.Show();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            formDMNhanVien nv = new formDMNhanVien();
            nv.Show();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            formDMKhachHang kh=new formDMKhachHang();
            kh.Show();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            formDMHDBan hd = new formDMHDBan();
            hd.Show();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            formTimHDBan tk = new formTimHDBan();
            tk.Show();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            formThongKe thongKe = new formThongKe();
            thongKe.Show();
        }
    }
}
