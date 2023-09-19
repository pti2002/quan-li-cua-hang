using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QL_CuaHangTienLoi.Class;

namespace QL_CuaHangTienLoi
{
    public partial class formTimHDBan : Form
    {
        Functions helper = new Functions();
        public formTimHDBan()
        {
            InitializeComponent();
        }

        private void formTimHDBan_Load(object sender, EventArgs e)
        {
            loaddgv();
        }
        private void loaddgv()
        {
            string sql = "Select * from HDBAN";
            dgvTimHDBan.DataSource = Class.Functions.GetDataToTable(sql);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            string nhantim = "MAHDBAN";
            string nhantim2 = "";
            string giatritim = txtMaHDBan.Text;
            string giatritimnam = txtNam.Text;
            if (txtMaKhachHang.Enabled == true)
            {
                giatritim = txtMaKhachHang.Text;
                nhantim = "MAKHACH";
            }
            else if (txtMaNhanVien.Enabled == true)
            {
                giatritim = txtMaNhanVien.Text;
                nhantim = "MANHANVIEN";
            }
            else if (txtThang.Enabled == true)
            {
                if (giatritimnam == "")
                    return;
                giatritim = txtThang.Text;
                nhantim = "month(NGAYBAN)";
                nhantim2 = "year(NGAYBAN)";
            }
            else if (txtTongTien.Enabled == true)
            {
                giatritim = txtTongTien.Text;
                nhantim = "TONGTIEN";
            }
            if (txtThang.Enabled == true)
            {
                sql = "Select * from HDBAN where " + nhantim + " = '" + giatritim + "' AND " + nhantim2 + " = '" + giatritimnam + "'";
            }
            else
            {
                sql = "Select * from HDBAN where " + nhantim + " = '" + giatritim + "'";
            }
            dgvTimHDBan.DataSource = Class.Functions.GetDataToTable(sql);
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            txtMaHDBan.Enabled = true;
            txtMaKhachHang.Enabled = true;
            txtMaNhanVien.Enabled = true;
            txtNam.Enabled = true;
            txtThang.Enabled = true;
            txtTongTien.Enabled = true;
            txtMaHDBan.Clear();
            txtMaKhachHang.Clear();
            txtMaNhanVien.Clear();
            txtNam.Clear();
            txtThang.Clear();
            txtTongTien.Clear();
            loaddgv();
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvTimHDBan_DoubleClick(object sender, EventArgs e)
        {

        } 


        private void txtMaHDBan_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (txtMaHDBan.Text != "")
                return;
            else
            {
                txtMaKhachHang.Enabled = false;
                txtMaNhanVien.Enabled = false;
                txtNam.Enabled = false;
                txtThang.Enabled = false;
                txtTongTien.Enabled = false;
            }
        }

        private void txtMaKhachHang_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (txtMaKhachHang.Text != "")
                return;
            else
            {
                txtMaHDBan.Enabled = false;
                txtMaNhanVien.Enabled = false;
                txtNam.Enabled = false;
                txtThang.Enabled = false;
                txtTongTien.Enabled = false;
            }
        }

        private void txtTongTien_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (txtTongTien.Text != "")
                return;
            else
            {
                txtMaHDBan.Enabled = false;
                txtMaKhachHang.Enabled = false;
                txtNam.Enabled = false;
                txtThang.Enabled = false;
                txtMaNhanVien.Enabled = false;
            }
        }

        private void txtMaNhanVien_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (txtMaNhanVien.Text != "")
                return;
            else
            {
                txtMaHDBan.Enabled = false;
                txtMaKhachHang.Enabled = false;
                txtNam.Enabled = false;
                txtThang.Enabled = false;
                txtTongTien.Enabled = false;
            }
        }

        private void txtNam_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (txtNam.Text != "")
                return;
            else
            {
                txtMaKhachHang.Enabled = false;
                txtMaNhanVien.Enabled = false;
                txtMaHDBan.Enabled = false;
                txtTongTien.Enabled = false;
            }
        }

        private void txtThang_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (txtThang.Text != "")
                return;
            else
            {
                txtMaKhachHang.Enabled = false;
                txtMaNhanVien.Enabled = false;
                txtMaHDBan.Enabled = false;
                txtTongTien.Enabled = false;
            }
        }
    }
}
