using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using QLCHCayCanh.Class; //Sử dụng class Functions.cs


namespace QLCHCayCanh
{
    public partial class frmThongKe : Form
    {
            
        public frmThongKe()
        {
            InitializeComponent();
            Con = new SqlConnection();   //Khởi tạo đối tượng
            Con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                            AttachDbFilename=D:\HK5\DoAn1\QLCHCayCanh\QLCHCayCanh\QLCHCAY.mdf;
                            Integrated Security=True;Connect Timeout=30";
        }
        
        private void frmThongKe_Load(object sender, EventArgs e)
        {
            
            Con.Open();
            DateTime startDate = dtNgDau.Value.Date;
            DateTime endDate = dtNgCuoi.Value.Date.AddDays(1);
            SqlCommand cmd = new SqlCommand("SELECT NgayBan, SUM(TongTien) as DoanhThu " +
                "FROM tbHoaDon WHERE NgayBan >= @startDate AND NgayBan < @endDate GROUP BY NgayBan", Con);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            bdDoanhThu.DataSource = dt;
            bdDoanhThu.ChartAreas["ChartArea1"].AxisX.Title = "Ngày bán";
            bdDoanhThu.ChartAreas["ChartArea1"].AxisY.Title = "Tổng tiền";

            bdDoanhThu.Series["ChartDT"].XValueMember = "NgayBan";
            bdDoanhThu.Series["ChartDT"].YValueMembers = "DoanhThu";

            // Đóng kết nối
            Con.Close();

        }
        public static SqlConnection Con;
        

        private void btnTKe_Click(object sender, EventArgs e)
        {
            //Con = new SqlConnection();   //Khởi tạo đối tượng
            //Con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
            //            AttachDbFilename=D:\HK5\DoAn1\QLCHCayCanh\QLCHCayCanh\QLCHCAY.mdf;
            //            Integrated Security=True;Connect Timeout=30";
            Con.Open();
            DateTime startDate = dtNgDau.Value.Date;
            DateTime endDate = dtNgCuoi.Value.Date.AddDays(1);
            SqlCommand cmd = new SqlCommand("SELECT NgayBan, SUM(TongTien) as DoanhThu " +
                "FROM tbHoaDon WHERE NgayBan >= @startDate AND NgayBan < @endDate GROUP BY NgayBan", Con);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            bdDoanhThu.DataSource = dt;
            bdDoanhThu.ChartAreas["ChartArea1"].AxisX.Title = "Ngày bán";
            bdDoanhThu.ChartAreas["ChartArea1"].AxisY.Title = "Tổng tiền";

            bdDoanhThu.Series["ChartDT"].XValueMember = "NgayBan";
            bdDoanhThu.Series["ChartDT"].YValueMembers = "DoanhThu";
            // Đóng kết nối
            Con.Close();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dtNgDau_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void dtNgCuoi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnTKe_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }
    }
}
