using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;


namespace QL_CuaHangTienLoi
{
    public partial class formInThongKe : Form
    {
        public formInThongKe()
        {
            InitializeComponent();
        }

        private void formInThongKe_Load(object sender, EventArgs e)
        {

            CultureInfo cul = new CultureInfo("vi-VN");
            ReportParameter p1 = new ReportParameter("tongTien", formThongKe.tongTien.ToString("c", cul));
            ReportParameter p2 = new ReportParameter("SoLuongHD", formThongKe.SoLuongHD.ToString());

            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
            
            // TODO: This line of code loads data into the 'dsHoaDon.DataTable1' table. You can move, or remove it, as needed.
            this.DataTable1TableAdapter.Fill(this.dsHoaDon.DataTable1);

            this.reportViewer1.RefreshReport();
        }
    }
}
