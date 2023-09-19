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
    public partial class formDMLoaiHang : Form
    {
        DataTable tblLH;

        public formDMLoaiHang()
        {
            InitializeComponent();
        }

        private void formDMLoaiHang_Load(object sender, EventArgs e)
        {
            txtMaLoaiHang.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView(); //Hiển thị bảng LOAIHANG
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM LOAIHANG";
            tblLH = Class.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvLoaiHang.DataSource = tblLH; //Nguồn dữ liệu            
            dgvLoaiHang.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvLoaiHang.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }


        private void dgvLoaiHang_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoaiHang.Focus();
                return;
            }
            if (tblLH.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaLoaiHang.Text = dgvLoaiHang.CurrentRow.Cells["MALOAIHANG"].Value.ToString();
            txtTenLoaiHang.Text = dgvLoaiHang.CurrentRow.Cells["TENLOAIHANG"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void ResetValue()
        {
            txtMaLoaiHang.Text = "";
            txtTenLoaiHang.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue(); 
            txtMaLoaiHang.Enabled = true; 
            txtMaLoaiHang.Focus();
        }
      

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql; 
            if (txtMaLoaiHang.Text.Trim().Length == 0) 
            {
                MessageBox.Show("Bạn phải nhập mã loại hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoaiHang.Focus();
                return;
            }
            if (txtTenLoaiHang.Text.Trim().Length == 0) 
            {
                MessageBox.Show("Bạn phải nhập tên loại hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenLoaiHang.Focus();
                return;
            }
            sql = "SELECT MALOAIHANG FROM LOAIHANG WHERE MALOAIHANG=N'" + txtMaLoaiHang.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã loại hàng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLoaiHang.Focus();
                return;
            }
            sql = "exec sp_InsertLoaiHang N'" + txtMaLoaiHang.Text + "', N'" + txtTenLoaiHang.Text + "'";
            Class.Functions.RunSQL(sql); 
            LoadDataGridView(); 
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaLoaiHang.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (tblLH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaLoaiHang.Text == "") //nếu chưa chọn bản ghi nào                   s
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenLoaiHang.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "exec sp_UpdateLoaiHang N'" + txtTenLoaiHang.Text.ToString() + "' ,N'" + txtMaLoaiHang.Text + "'";
            Class.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnBoQua.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblLH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaLoaiHang.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "exec sp_DeleteLoaiHang N'" + txtMaLoaiHang.Text + "'";
                Class.Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}
