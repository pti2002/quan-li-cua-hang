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
    public partial class formDMHangHoa : Form
    {
        DataTable tblHH;
        public formDMHangHoa()
        {
            InitializeComponent();
        }

        private void formDMHangHoa_Load(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT * FROM LOAIHANG";
            txtMaHang.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            Functions.FillCombo(sql, cboMaLoaiHang, "MALOAIHANG", "TENLOAIHANG");
            cboMaLoaiHang.SelectedIndex = -1;
            ResetValues();
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM HANG";
            tblHH = Functions.GetDataToTable(sql);
            dgvHangHoa.DataSource = tblHH;
            dgvHangHoa.AllowUserToAddRows = false;
            dgvHangHoa.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValues()
        {
            txtMaHang.Text = "";
            txtTenHang.Text = "";
            cboMaLoaiHang.Text = "";
            txtSoLuong.Text = "0";
            txtDonGiaNhap.Text = "0";
            txtDonGiaBan.Text = "0";
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = false;
            txtDonGiaBan.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaHang.Enabled = true;
            txtMaHang.Focus();
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            if (txtTenHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenHang.Focus();
                return;
            }
            if (cboMaLoaiHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập loại hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaLoaiHang.Focus();
                return;
            }
            if (txtSoLuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Focus();
                return;
            }
            sql = "SELECT MAHANG FROM HANG WHERE MAHANG=N'" + txtMaHang.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã tồn tại, bạn phải chọn mã hàng khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            sql = "exec sp_InsertHangHoa N'" + txtMaHang.Text.Trim() + "',N'" + txtTenHang.Text.Trim() + "'," + txtSoLuong.Text.Trim() + ",N'" + cboMaLoaiHang.SelectedValue.ToString() +
                "'," + txtDonGiaNhap.Text +
                "," + txtDonGiaBan.Text + "";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaHang.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaHang.Enabled = false;
        } 

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT * FROM HANG";
            tblHH = Functions.GetDataToTable(sql);
            dgvHangHoa.DataSource = tblHH;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblHH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            if (txtTenHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenHang.Focus();
                return;
            }
            if (cboMaLoaiHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập loại hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaLoaiHang.Focus();
                return;
            }
            try
            {
                sql = "exec sp_UpdateHangHoa N'" + txtTenHang.Text.Trim().ToString() + "',N'" + cboMaLoaiHang.SelectedValue.ToString() + "', " + txtSoLuong.Text + ", N'" + txtMaHang.Text + "'";
                Functions.RunSQL(sql);
            }
            catch
            {
                MessageBox.Show("Bạn phải chọn lại mã hàng!");
            }
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblHH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "exec sp_DeleteHangHoa N'" + txtMaHang.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void dgvHangHoa_DoubleClick(object sender, EventArgs e)
        {
            string MaLoaiHang;
            string sql;
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            if (tblHH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaHang.Text = dgvHangHoa.CurrentRow.Cells["MAHANG"].Value.ToString();
            txtTenHang.Text = dgvHangHoa.CurrentRow.Cells["TENHANG"].Value.ToString();
            MaLoaiHang = dgvHangHoa.CurrentRow.Cells["MALOAIHANG"].Value.ToString();
            sql = "SELECT MALOAIHANG FROM LOAIHANG WHERE MALOAIHANG=N'" + MaLoaiHang + "'";
            cboMaLoaiHang.Text = Functions.GetFieldValues(sql);
            txtSoLuong.Text = dgvHangHoa.CurrentRow.Cells["SOLUONG"].Value.ToString();
            txtDonGiaNhap.Text = dgvHangHoa.CurrentRow.Cells["DONGIANHAP"].Value.ToString();
            txtDonGiaBan.Text = dgvHangHoa.CurrentRow.Cells["DONGIABAN"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (txtTimKiem.Text == "")
            {
                MessageBox.Show("Bạn hãy nhập tên hàng cần tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "exec sp_TimKiemTenHang N'%" + txtTimKiem.Text +"%'";

            tblHH = Functions.GetDataToTable(sql);
            if (tblHH.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tblHH.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvHangHoa.DataSource = tblHH;
            ResetValues();
        }
            
    }
}
