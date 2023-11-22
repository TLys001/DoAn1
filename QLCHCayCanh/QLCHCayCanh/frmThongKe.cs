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
        }
        
        private void frmThongKe_Load(object sender, EventArgs e)
        {
            /* Connect(); */ // Gọi hàm Connect() để mở kết nối
                             //string query = "SELECT NgayBan, SUM(TongTien) as DoanhThu FROM tbHoaDon GROUP BY NgayBan";
                             //var revenueData = new Dictionary<DateTime, double>();
                             //SqlCommand command = new SqlCommand(query, Con);
                             //SqlDataReader reader = command.ExecuteReader();
                             //if (reader.HasRows)
                             //{
                             //    while (reader.Read())
                             //    {
                             //        DateTime date = reader.GetDateTime(0);
                             //        double revenue = reader.GetDouble(1);
                             //        revenueData[date] = revenue;
                             //    }
                             //}

            //reader.Close()
            //// Cấu hình biểu đồ
            //BDDoanhThu.Series.Clear();
            //var series1 = new Series
            //{
            //    Name = "Doanh thu",
            //    Color = System.Drawing.Color.Teal,
            //    IsVisibleInLegend = false,
            //    IsXValueIndexed = true,
            //    ChartType = SeriesChartType.Column
            //};

            //BDDoanhThu.Series.Add(series1);

            //// Thêm dữ liệu vào biểu đồ
            //foreach (var data in revenueData)
            //{
            //    series1.Points.AddXY(data.Key.ToShortDateString(), data.Value);
            //}
            //BDDoanhThu.Invalidate();

            //// Đóng kết nối
            //Con.Close();

            //GetRevenueData();
            //this.rpvTKe.RefreshReport();
            Con = new SqlConnection();   //Khởi tạo đối tượng
            Con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                        AttachDbFilename=D:\HK5\DoAn1\QLCHCayCanh\QLCHCayCanh\QLCHCAY.mdf;
                        Integrated Security=True;Connect Timeout=30";
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

            //if (reader.HasRows)
            //{
            //    while (reader.Read())
            //    {
            //        DateTime date = reader.GetDateTime(0);
            //        double revenue = reader.GetDouble(1);
            //        revenueData[date] = revenue;
            //    }
            //}

            //reader.Close();

            //bdDoanhThu.Series.Clear();
            //var ChartDT = new Series
            //{
            //    Name = "Doanh thu",
            //    Color = System.Drawing.Color.Teal,
            //    IsVisibleInLegend = false,
            //    IsXValueIndexed = true,
            //    ChartType = SeriesChartType.Column
            //};

            //bdDoanhThu.Series.Add(ChartDT);

            //foreach (var data in revenueData)
            //{
            //    ChartDT.Points.AddXY(data.Key.ToShortDateString(), data.Value);
            //}

            //bdDoanhThu.Invalidate();


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

    }
}
