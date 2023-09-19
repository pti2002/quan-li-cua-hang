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
    public partial class formDMHDBan : Form
    {
        DataTable CTHD; 
        public formDMHDBan()
        {
            InitializeComponent();
        }

        private void formDMHDBan_Load(object sender, EventArgs e)
        {
            Functions.FillCombo("SELECT MAKHACH, TENKHACH FROM KHACH", cboMaKhach, "MAKHACH", "MAKHACH");
            cboMaKhach.SelectedIndex = -1;
            Functions.FillCombo("SELECT MANHANVIEN, TENNHANVIEN FROM NHANVIEN", cboMaNhanVien, "MANHANVIEN", "MANHANVIEN");
            cboMaNhanVien.SelectedIndex = -1;
            Functions.FillCombo("SELECT MAHANG, TENHANG FROM HANG", cboMaHang, "MAHANG", "MAHANG");
            cboMaHang.SelectedIndex = -1;
        }
      

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadData()
        {
            string sql;
            sql = "SELECT MAHDBAN,a.MAHANG, b.TENHANG, a.SOLUONGMUA, b.DONGIABAN, a.GIAMGIA,a.THANHTIEN FROM CHITIETHDBAN AS a, HANG AS b WHERE a.MAHDBAN = N'" + txtMaHDBan.Text + "' AND a.MAHANG=b.MAHANG";
            CTHD = Functions.GetDataToTable(sql);
            dgvHDBanHang.DataSource = CTHD;
            dgvHDBanHang.Columns[0].HeaderText = "Mã hóa đơn";
            dgvHDBanHang.Columns[1].HeaderText = "Mã hàng";
            dgvHDBanHang.Columns[2].HeaderText = "Tên hàng";
            dgvHDBanHang.Columns[3].HeaderText = "Số lượng";
            dgvHDBanHang.Columns[4].HeaderText = "Đơn giá";
            dgvHDBanHang.Columns[5].HeaderText = "Giảm giá %";
            dgvHDBanHang.Columns[6].HeaderText = "Thành tiền";
            
            dgvHDBanHang.AllowUserToAddRows = false;
            dgvHDBanHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void LoadInfoHoaDon()
        {
            string str;
            str = "SELECT NGAYBAN FROM HDBAN WHERE MAHDBAN = N'" + txtMaHDBan.Text + "'";
            txtNgayBan.Text = Functions.ConvertDateTime(Functions.GetFieldValues(str));
            str = "SELECT MANHANVIEN FROM HDBAN WHERE MAHDBAN = N'" + txtMaHDBan.Text + "'";
            cboMaNhanVien.Text = Functions.GetFieldValues(str);
            str = "SELECT MAKHACH FROM HDBAN WHERE MAHDBAN = N'" + txtMaHDBan.Text + "'";
            cboMaKhach.Text = Functions.GetFieldValues(str);
            str = "SELECT TONGTIEN FROM HDBAN WHERE MAHDBAN = N'" + txtMaHDBan.Text + "'";
            txtTongTien.Text = Functions.GetFieldValues(str);
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnHuy.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaHDBan.Text = Functions.CreateKey("HDB");
            LoadData();
        }

        private void ResetValues()
        {
            txtMaHDBan.Text = "";
            txtNgayBan.Text = DateTime.Now.ToShortDateString();
            cboMaNhanVien.Text = "";
            cboMaKhach.Text = "";
            txtTongTien.Text = "0";
            cboMaHang.Text = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
        }

        private void cboMaHang_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaHang.Text == "")
            {
                txtTenHang.Text = "";
                txtDonGia.Text = "";
            }
            str = "SELECT TENHANG FROM HANG WHERE MAHANG =N'" + cboMaHang.SelectedValue + "'";
            txtTenHang.Text = Functions.GetFieldValues(str);
            str = "SELECT DONGIABAN FROM HANG WHERE MAHANG =N'" + cboMaHang.SelectedValue + "'";
            txtDonGia.Text = Functions.GetFieldValues(str);
        }

        private void cboMaKhach_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaKhach.Text == "")
            {
                txtTenKhach.Text = "";
                txtDiaChi.Text = "";
                txtDienThoai.Text = "";
            }     
            str = "SELECT TENKHACH FROM KHACH WHERE MAKHACH = N'" + cboMaKhach.SelectedValue + "'";
            txtTenKhach.Text = Functions.GetFieldValues(str);
            str = "SELECT DIACHI_KH FROM KHACH WHERE MAKHACH = N'" + cboMaKhach.SelectedValue + "'";
            txtDiaChi.Text = Functions.GetFieldValues(str);
            str = "SELECT DIENTHOAI_KH FROM KHACH WHERE MAKHACH= N'" + cboMaKhach.SelectedValue + "'";
            txtDienThoai.Text = Functions.GetFieldValues(str);
        }

        private void cboMaNhanVien_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaNhanVien.Text == "")
                txtTenNhanVien.Text = "";
            str = "SELECT TENNHANVIEN FROM NHANVIEN WHERE MANHANVIEN =N'" + cboMaNhanVien.SelectedValue + "'";
            txtTenNhanVien.Text = Functions.GetFieldValues(str);
        }

        private void cboMaHD_DropDown(object sender, EventArgs e)
        {
            Functions.FillCombo("SELECT MAHDBAN FROM HDBAN", cboMaHD, "MAHDBAN", "MAHDBAN");
            cboMaHD.SelectedIndex = -1;
        }
        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT MAHDBAN FROM HDBAN WHERE MAHDBAN=N'" + txtMaHDBan.Text + "'";
            if (!Functions.CheckKey(sql))
            {
                // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                // Mã HDBan được sinh tự động do đó không có trường hợp trùng khóa
                if (txtNgayBan.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập ngày bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNgayBan.Focus();
                    return;
                }
                if (cboMaNhanVien.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaNhanVien.Focus();
                    return;
                }
                if (cboMaKhach.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaKhach.Focus();
                    return;
                }
                sql = "INSERT INTO HDBAN(MAHDBAN, NGAYBAN, MANHANVIEN, MAKHACH, TONGTIEN) VALUES (N'" + txtMaHDBan.Text.Trim() + "','" +
                        Functions.ConvertDateTime(txtNgayBan.Text.Trim()) + "',N'" + cboMaNhanVien.SelectedValue + "',N'" +
                        cboMaKhach.SelectedValue + "'," + txtTongTien.Text + ")";
                Functions.RunSQL(sql);
            }
            // Lưu thông tin của các mặt hàng
            if (cboMaHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaHang.Focus();
                return;
            }
            if (txtSoLuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            if (txtGiamGia.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giảm giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiamGia.Focus();
                return;
            }
            sql = "SELECT MAHANG FROM CHITIETHDBAN WHERE MAHANG=N'" + cboMaHang.SelectedValue + "' AND MAHDBAN = N'" + txtMaHDBan.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValuesHang();
                cboMaHang.Focus();
                return;
            }
            // Thêm và kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?Nếu ko đủ sẽ ROLLBACK
            sql = "exec KTSLMua N'" + txtMaHDBan.Text + "',N'" + cboMaHang.SelectedValue + "'," + txtSoLuong.Text + "," + txtDonGia.Text + "," + txtGiamGia.Text + "," + txtThanhTien.Text + "";
            Functions.RunSQL(sql);            
            LoadData();

            // Cập nhật lại số lượng của mặt hàng vào bảng HANG(DO TRIGGER UPDATE_THEMHD_TRIGGER XỬ LÝ)
         

            // CẬP NHẬT LẠI TỔNG TIỀN CHO HÓA ĐƠN(DO TRIGGER UPDATE_TONGTIEN XỬ LÝ)
            string str;
            str = "SELECT TONGTIEN FROM HDBAN WHERE MAHDBAN =N'" + txtMaHDBan.Text + "'";   //Lấy tổng tiền lên từ CSDL
            txtTongTien.Text = Functions.GetFieldValues(str);
            Functions.RunSQL(str);
            ResetValuesHang();
            btnHuy.Enabled = true;
            btnThem.Enabled = true;
        }
        private void ResetValuesHang()
        {
            cboMaHang.Text = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
           
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
             float tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = float.Parse(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = float.Parse(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = float.Parse(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {          
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "SELECT MAHANG,SOLUONGMUA FROM CHITIETHDBAN WHERE MAHDBAN = N'" + txtMaHDBan.Text + "'";
                //Xóa hóa đơn
                sql = "DELETE HDBAN WHERE MAHDBAN=N'" + txtMaHDBan.Text + "'";
                Functions.RunSqlDel(sql);    
                //Xóa chi tiết hóa đơn
                sql = "DELETE CHITIETHDBAN WHERE MAHDBAN=N'" + txtMaHDBan.Text + "'";
                Functions.RunSqlDel(sql);                           
                ResetValues();
                LoadData();
                btnHuy.Enabled = false;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboMaHD.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaHD.Focus();
                return;
            }
            txtMaHDBan.Text = cboMaHD.Text;
            LoadInfoHoaDon();
            LoadData();
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            cboMaHD.SelectedIndex = -1; 
        }
        private void formHoadonBan_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetValues();
        }

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboMaKhach_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvHDBanHang_DoubleClick(object sender, EventArgs e)
        {
            string MaHangxoa, sql;
            float ThanhTienxoa, SoLuongxoa;
            if (CTHD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Xóa hàng 
                MaHangxoa = dgvHDBanHang.CurrentRow.Cells["MAHANG"].Value.ToString();
                SoLuongxoa = float.Parse(dgvHDBanHang.CurrentRow.Cells["SOLUONGMUA"].Value.ToString());
                ThanhTienxoa = float.Parse(dgvHDBanHang.CurrentRow.Cells["THANHTIEN"].Value.ToString());
                sql = "exec Delete_CTHDBAN_Func '" + txtMaHDBan.Text + "' , '" + MaHangxoa + "'";
                Functions.RunSQL(sql);
                // Cập nhật lại số lượng cho các mặt hàng(TRIGGER UPDATE_XOAHANG XỬ LÝ)


                //// Cập nhật lại tổng tiền cho hóa đơn bán(DO UPDATE_TONGTIEN_AFTER_XOAHANG XỬ LÝ)               
                string str;
                str = "SELECT TONGTIEN FROM HDBAN WHERE MAHDBAN =N'" + txtMaHDBan.Text + "'";   //Lấy tổng tiền lên từ CSDL
                txtTongTien.Text = Functions.GetFieldValues(str);
                LoadData();
            }
        }

        private void txtDienThoai_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenKhach_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenNhanVien_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboMaNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtNgayBan_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtMaHDBan_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dgvHDBanHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
