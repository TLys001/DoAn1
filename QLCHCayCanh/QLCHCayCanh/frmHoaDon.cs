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
using QLCHCayCanh.Class;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QLCHCayCanh
{
    public partial class frmHoaDon : Form
    {
        public frmHoaDon()
        {
            InitializeComponent();
        }
        
        DataTable tbCTHD;
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT a.MaCay, b.TenCay, a.SoLuong, b.GiaBan, a.ThanhTien " +
                "FROM tbCTHD AS a, tbCay AS b " +
                "WHERE a.maHD = N'" + txtMaHD.Text + "' AND a.MaCay = b.MaCay";
            tbCTHD = Functions.GetDataToTable(sql);
            dgvDSHD.DataSource = tbCTHD;
            dgvDSHD.Columns[0].HeaderText = "Mã cây";
            dgvDSHD.Columns[1].HeaderText = "Tên cây";
            dgvDSHD.Columns[2].HeaderText = "Số lượng";
            dgvDSHD.Columns[3].HeaderText = "Đơn giá";
            dgvDSHD.Columns[4].HeaderText = "Thành tiền";
            dgvDSHD.Columns[0].Width = 80;
            dgvDSHD.Columns[1].Width = 130;
            dgvDSHD.Columns[2].Width = 80;
            dgvDSHD.Columns[3].Width = 90;
            dgvDSHD.Columns[4].Width = 90;
            dgvDSHD.AllowUserToAddRows = false;
            dgvDSHD.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            btnXuatHD.Enabled = false;
            txtMaHD.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtTenKH.ReadOnly = true;
            txtDchi.ReadOnly = true;
            mtbSdt.ReadOnly = true;
            txtTenCay.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtTongTien.Text = "0";

            Functions.FillCombo("SELECT MaKH, TenKH FROM tbKhachHang", cboMaKH, "MaKH", "MaKH");
            cboMaKH.SelectedIndex = -1;

            Functions.FillCombo("SELECT MaNV, TenNV FROM tbNhanVien", cboMaNV, "MaNV", "TenKH");
            cboMaNV.SelectedIndex = -1;

            Functions.FillCombo("SELECT MaCay, TenCay FROM tbCay", cboMaCay, "MaCay", "MaCay");
            cboMaCay.SelectedIndex = -1;
            
            //Hiển thị thông tin của một hóa đơn được gọi từ form tìm kiếm
            if (txtMaHD.Text != "")
            {
                LoadInfoHoaDon();
                btnXoa.Enabled = true;
                btnXuatHD.Enabled = true;
            }
            LoadDataGridView();
        }

        private void LoadInfoHoaDon()
        {
            string str;
            str = "SELECT NgayBan FROM tbHoaDon WHERE maHD = N'" + txtMaHD.Text + "'";
            dtNgBan.Value = DateTime.Parse(Functions.GetFieldValues(str));
            str = "SELECT MaNV FROM tbHoaDon WHERE maHD = N'" + txtMaHD.Text + "'";
            cboMaNV.Text = Functions.GetFieldValues(str);
            str = "SELECT MaKH FROM tbHoaDon WHERE maHD = N'" + txtMaHD.Text + "'";
            cboMaKH.Text = Functions.GetFieldValues(str);
            str = "SELECT TongTien FROM tbHoaDon WHERE maHD = N'" + txtMaHD.Text + "'";
            txtTongTien.Text = Functions.GetFieldValues(str);
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnXuatHD.Enabled = false;
            btnThem.Enabled = false;
            ResetValues();
            txtMaHD.Text = Functions.CreateKey("HD");
            LoadDataGridView();
        }

        private void ResetValues()
        {
            txtMaHD.Text = "";
            dtNgBan.Value = DateTime.Now;
            cboMaNV.Text = "";

            cboMaKH.Text = "";
            txtTongTien.Text = "0";
            cboMaCay.Text = "";
            txtTenCay.Text = "";
            txtSoLuong.Text = "";
            txtThanhTien.Text = "0";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            double sl, SLcon, tong, Tongmoi;
            sql = "SELECT maHD FROM tbHoaDon WHERE maHD=N'" + txtMaHD.Text + "'";
            if (!Functions.CheckKey(sql))
            {
                // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                // Mã HD được sinh tự động do đó không có trường hợp trùng khóa
                
                if (cboMaNV.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaNV.Focus();
                    return;
                }
                if (cboMaKH.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaKH.Focus();
                    return;
                }
                sql = "INSERT INTO tbHoaDon(maHD, NgayBan, MaNV, MaKH, TongTien) VALUES (N'" + txtMaHD.Text.Trim() + "','" +
                        dtNgBan.Value + "',N'" + cboMaNV.SelectedValue + "',N'" +
                        cboMaKH.SelectedValue + "'," + txtTongTien.Text + ")";
                Functions.RunSQL(sql);
            }
            // Lưu thông tin của các cây
            if (cboMaCay.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã cây", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaCay.Focus();
                return;
            }
            if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            
            sql = "SELECT MaCay FROM tbCTHD WHERE MaCay=N'" + cboMaCay.SelectedValue + "' AND maHD = N'" + txtMaHD.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã cây này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValuesCay();
                cboMaCay.Focus();
                return;
            }
            // Kiểm tra xem số lượng cây trong kho còn đủ để cung cấp không?
            sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM tbCay WHERE MaCay = N'" + cboMaCay.SelectedValue + "'"));
            if (Convert.ToDouble(txtSoLuong.Text) > sl)
            {
                MessageBox.Show("Số lượng cây này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            sql = "INSERT INTO tbCTHD(maHD, MaCay, SoLuong, DonGia, ThanhTien) " +
                "VALUES(N'" + txtMaHD.Text.Trim() + "',N'" + cboMaCay.SelectedValue + "'," + txtSoLuong.Text +
                "," + txtDonGia.Text + "," + txtThanhTien.Text + ")";
            Functions.RunSQL(sql);
            LoadDataGridView();
            
            // Cập nhật lại số lượng của cây vào bảng tbCay
            SLcon = sl - Convert.ToDouble(txtSoLuong.Text);
            sql = "UPDATE tbCay SET SoLuong =" + SLcon + " WHERE MaCay= N'" + cboMaCay.SelectedValue + "'";
            Functions.RunSQL(sql);
            
            // Cập nhật lại tổng tiền cho hóa đơn bán
            tong = Convert.ToDouble(Functions.GetFieldValues("SELECT TongTien FROM tbHoaDon WHERE maHD = N'" + txtMaHD.Text + "'"));
            Tongmoi = tong + Convert.ToDouble(txtThanhTien.Text);
            sql = "UPDATE tbHoaDon SET TongTien =" + Tongmoi + " WHERE maHD = N'" + txtMaHD.Text + "'";
            Functions.RunSQL(sql);
            txtTongTien.Text = Tongmoi.ToString();
            ResetValuesCay();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnXuatHD.Enabled = true;
        }

        private void ResetValuesCay()
        {
            cboMaCay.Text = "";
            txtSoLuong.Text = "";
            txtThanhTien.Text = "0";
        }

        private void cboMaNV_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaNV.Text == "")
                txtTenNV.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select TenNV from tbNhanVien where MaNV =N'" + cboMaNV.SelectedValue + "'";
            txtTenNV.Text = Functions.GetFieldValues(str);
        }

        private void cboMaKH_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaKH.Text == "")
            {
                txtTenKH.Text = "";
                txtDchi.Text = "";
                mtbSdt.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str = "Select TenKH from tbKhachHang where MaKH = N'" + cboMaKH.SelectedValue + "'";
            txtTenKH.Text = Functions.GetFieldValues(str);
            str = "Select DChi from tbKhachHang where MaKH = N'" + cboMaKH.SelectedValue + "'";
            txtDchi.Text = Functions.GetFieldValues(str);
            str = "Select Sdt from tbKhachHang where MaKH= N'" + cboMaKH.SelectedValue + "'";
            mtbSdt.Text = Functions.GetFieldValues(str);
        }

        private void cboMaCay_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaCay.Text == "")
            {
                txtTenCay.Text = "";
                txtDonGia.Text = "";
            }
            // Khi chọn mã cây thì các thông tin về cây hiện ra
            str = "SELECT TenCay FROM tbCay WHERE MaCay =N'" + cboMaCay.SelectedValue + "'";
            txtTenCay.Text = Functions.GetFieldValues(str);
            str = "SELECT GiaBan FROM tbCay WHERE MaCay =N'" + cboMaCay.SelectedValue + "'";
            txtDonGia.Text = Functions.GetFieldValues(str);
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi số lượng thì thực hiện tính lại thành tiền
            double tt, sl, dg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
           
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg;
            txtThanhTien.Text = tt.ToString();
        }

        private void btnXuatHD_Click(object sender, EventArgs e)
        {
            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tbThongtinHD, tbThongtinCay;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["A1:B3"].Font.Size = 14;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 10;
            exRange.Range["B1:B1"].ColumnWidth = 20;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Spring Graden";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Cần Thơ";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: 0123456789";
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN BÁN";
            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "SELECT a.maHD, a.NgayBan, a.TongTien, b.TenKH, b.DChi, b.Sdt, c.TenNV FROM tbHoaDon AS a, tbKhachHang AS b, tbNhanVien AS c " +
                "WHERE a.maHD = N'" + txtMaHD.Text + "' AND a.MaKH = b.MaKH AND a.MaNV = c.MaNV";
            tbThongtinHD = Functions.GetDataToTable(sql);
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C6:E6"].Value = tbThongtinHD.Rows[0][0].ToString();
            
            exRange.Range["B7:B7"].Value = "Khách hàng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C7:E7"].Value = tbThongtinHD.Rows[0][3].ToString();

            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C8:E8"].Value = tbThongtinHD.Rows[0][4].ToString();

            exRange.Range["B9:B9"].Value = "Số điện thoại:";
            exRange.Range["B9:B9"].NumberFormat = "@";
            // Gán giá trị cho ô C9, giữ số 0 đầu tiên 
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C9:E9"].Value = tbThongtinHD.Rows[0][5].ToString();
            //Lấy thông tin các cây
            sql = "SELECT b.TenCay, a.SoLuong, b.GiaBan,  a.ThanhTien " +
                  "FROM tbCTHD AS a , tbCay AS b WHERE a.maHD = N'" +
                  txtMaHD.Text + "' AND a.MaCay = b.MaCay";
            tbThongtinCay = Functions.GetDataToTable(sql);
            //Tạo dòng tiêu đề bảng
            exRange.Range["A12:F12"].Font.Bold = true;
            exRange.Range["A12:F16"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A12:F12"].ColumnWidth = 13;
            exRange.Range["A12:A12"].Value = "STT";
            exRange.Range["B12:B12"].Value = "Tên cây";
            exRange.Range["C12:C12"].Value = "Số lượng";
            exRange.Range["D12:D12"].Value = "Đơn giá";
            exRange.Range["E12:E12"].Value = "Thành tiền";
            for (hang = 0; hang < tbThongtinCay.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 13
                exSheet.Cells[1][hang + 13] = hang + 1;
                for (cot = 0; cot < tbThongtinCay.Columns.Count; cot++)
                //Điền thông tin cây từ cột thứ 2, dòng 13
                {
                    exSheet.Cells[cot + 2][hang + 13] = tbThongtinCay.Rows[hang][cot].ToString();
                }
            }
            exRange = exSheet.Cells[cot][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[cot + 1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = tbThongtinHD.Rows[0][2].ToString();
            exRange = exSheet.Cells[1][hang + 15]; //Ô A1 
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tbThongtinHD.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "Cần Thơ, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên bán";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tbThongtinHD.Rows[0][6];
            exSheet.Name = "Hóa đơn nhập";
            exApp.Visible = true;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (cboMaHD.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaHD.Focus();
                return;
            }
            txtMaHD.Text = cboMaHD.Text;
            LoadInfoHoaDon();
            LoadDataGridView();
            btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            btnXuatHD.Enabled = true;
            cboMaHD.SelectedIndex = -1;
        }

        private void cboMaHD_DropDown(object sender, EventArgs e)
        {
            Functions.FillCombo("SELECT maHD FROM tbHoaDon", cboMaHD, "maHD", "maHD");
            cboMaHD.SelectedIndex = -1;
        }

        private void frmHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Xóa dữ liệu trong các điều khiển trước khi đóng Form
            ResetValues();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void dgvDSHD_DoubleClick(object sender, EventArgs e)
        {
            string MaCayxoa, sql;
            Double ThanhTienxoa, SoLuongxoa, sl, slcon, tong, tongmoi;
            if (tbCTHD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Xóa cây và cập nhật lại số lượng cây 
                MaCayxoa = dgvDSHD.CurrentRow.Cells["MaCay"].Value.ToString();
                SoLuongxoa = Convert.ToDouble(dgvDSHD.CurrentRow.Cells["SoLuong"].Value.ToString());
                ThanhTienxoa = Convert.ToDouble(dgvDSHD.CurrentRow.Cells["ThanhTien"].Value.ToString());
                sql = "DELETE tbCTHD WHERE maHD=N'" + txtMaHD.Text + "' AND MaCay = N'" + MaCayxoa + "'";
                Functions.RunSQL(sql);
                // Cập nhật lại số lượng cho các cây
                sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM tbCay WHERE MaCay = N'" + MaCayxoa + "'"));
                slcon = sl + SoLuongxoa;
                sql = "UPDATE tbCay SET SoLuong =" + slcon + " WHERE MaCay= N'" + MaCayxoa + "'";
                Functions.RunSQL(sql);
                // Cập nhật lại tổng tiền cho hóa đơn bán
                tong = Convert.ToDouble(Functions.GetFieldValues("SELECT TongTien FROM tbHoaDon WHERE maHD = N'" + txtMaHD.Text + "'"));
                tongmoi = tong - ThanhTienxoa;
                sql = "UPDATE tbHoaDon SET TongTien =" + tongmoi + " WHERE maHD = N'" + txtMaHD.Text + "'";
                Functions.RunSQL(sql);
                txtTongTien.Text = tongmoi.ToString();
                LoadDataGridView();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            double sl, slcon, slxoa;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "SELECT MaCay,SoLuong FROM tbCTHD WHERE maHD = N'" + txtMaHD.Text + "'";
                DataTable tbCay = Functions.GetDataToTable(sql);
                for (int hang = 0; hang <= tbCay.Rows.Count - 1; hang++)
                {
                    // Cập nhật lại số lượng cho các cây
                    sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM tbCay WHERE MaCay = N'" + tbCay.Rows[hang][0].ToString() + "'"));
                    slxoa = Convert.ToDouble(tbCay.Rows[hang][1].ToString());
                    slcon = sl + slxoa;
                    sql = "UPDATE tbCay SET SoLuong =" + slcon + " WHERE MaCay= N'" + tbCay.Rows[hang][0].ToString() + "'";
                    Functions.RunSQL(sql);
                }

                //Xóa chi tiết hóa đơn
                sql = "DELETE tbCTHD WHERE maHD=N'" + txtMaHD.Text + "'";
                Functions.RunSqlDel(sql);

                //Xóa hóa đơn
                sql = "DELETE tbHoaDon WHERE maHD=N'" + txtMaHD.Text + "'";
                Functions.RunSqlDel(sql);
                ResetValues();
                LoadDataGridView();
                btnXoa.Enabled = false;
                btnXuatHD.Enabled = false;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadDataGridView();
        }

    }
}
