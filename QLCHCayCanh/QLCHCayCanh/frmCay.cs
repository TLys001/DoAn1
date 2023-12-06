using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //Sử dụng thư viện để làm việc SQL server
using QLCHCayCanh.Class; //Sử dụng class Functions.cs

namespace QLCHCayCanh
{
    public partial class frmCay : Form
    {
        DataTable tbCay;
        public frmCay()
        {
            InitializeComponent();
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM tbCay";
            tbCay = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvDSCay.DataSource = tbCay; //Nguồn dữ liệu            
            dgvDSCay.Columns[0].Name = "Mã cây";
            dgvDSCay.Columns[1].Name = "Tên cây";
            dgvDSCay.Columns[2].Name = "Mã loại";
            dgvDSCay.Columns[3].Name = "Mã nhà cung cấp";
            dgvDSCay.Columns[4].Name = "Ngày nhập";
            dgvDSCay.Columns[5].Name = "Số lượng";
            dgvDSCay.Columns[6].Name = "Giá nhập";
            dgvDSCay.Columns[7].Name = "Giá bán";
            dgvDSCay.Columns[8].Name = "Ghi chú";

            dgvDSCay.Columns[0].HeaderText = "Mã cây";
            dgvDSCay.Columns[1].HeaderText = "Tên cây";
            dgvDSCay.Columns[2].HeaderText = "Mã loại";
            dgvDSCay.Columns[3].HeaderText = "Mã nhà cung cấp";
            dgvDSCay.Columns[4].HeaderText = "Ngày nhập";
            dgvDSCay.Columns[5].HeaderText = "Số lượng";
            dgvDSCay.Columns[6].HeaderText = "Giá nhập";
            dgvDSCay.Columns[7].HeaderText = "Giá bán";
            dgvDSCay.Columns[8].HeaderText = "Ghi chú";

            dgvDSCay.Columns[0].Width = 60;
            dgvDSCay.Columns[1].Width = 100;
            dgvDSCay.Columns[2].Width = 70;
            dgvDSCay.Columns[3].Width = 70;
            dgvDSCay.Columns[4].Width = 100;
            dgvDSCay.Columns[5].Width = 50;
            dgvDSCay.Columns[6].Width = 70;
            dgvDSCay.Columns[7].Width = 70;
            dgvDSCay.Columns[8].Width = 130;

            dgvDSCay.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvDSCay.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }
        private bool CheckDate(DateTime dt)
        {
            if (dt.Year > DateTime.Today.Year)
                return false;
            if ((dt.Month > DateTime.Today.Month) && (dt.Year == DateTime.Today.Year))
                return false;
            if ((dt.Day > DateTime.Today.Day) && (dt.Month == DateTime.Today.Month) && (dt.Year == DateTime.Today.Year))
                return false;
            return true;
        }
        private void ResetValue()
        {
            txtMaCay.Text = "";
            txtTenCay.Text = "";
            cboTenLoai.Text = "";
            cboTenNCC.Text = "";
            dtNgNhap.Value = DateTime.Today;
            txtSoLuong.Text = "";
            txtGiaNhap.Text = "";
            txtGiaBan.Text = "";
            txtGhiChu.Text = "";


        }
        private void frmCay_Load(object sender, EventArgs e)
        {
            txtMaCay.Enabled = true;
            txtTenCay.Enabled = true;
            cboTenLoai.Enabled = true;
            cboTenNCC.Enabled = true;
            
            dtNgNhap.Enabled = true;
            txtSoLuong.Enabled = true;
            txtGiaNhap.Enabled = true;
            txtGiaBan.Enabled = true;
            txtGhiChu.Enabled = true;

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnTim.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            LoadDataGridView();

            string sql;
            sql = "SELECT * from tbLoaiSP";
            Functions.FillCombo(sql, cboTenLoai, "MaLoai", "TenLoai");
            cboTenLoai.SelectedIndex = -1;

            sql = "SELECT * from tbNCC";
            Functions.FillCombo(sql, cboTenNCC, "MaNCC", "TenNCC");
            cboTenNCC.SelectedIndex = -1;

            ResetValue();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnTim.Enabled = false;
            ResetValue(); //Xoá trắng các textbox
            txtMaCay.Enabled = true; //cho phép nhập mới
            txtMaCay.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaCay.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã cây", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaCay.Focus();
                return;
            }
            if (txtTenCay.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên cây", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenCay.Focus();
                return;
            }
            if (cboTenLoai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên loại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenLoai.Focus();
                return;
            }
            if (cboTenNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenNCC.Focus();
                return;
            }
            DateTime nn = dtNgNhap.Value;
            if (!CheckDate(nn))
            {
                MessageBox.Show("Không được lớn hơn ngày hôm nay!");
                return;
            }
            if (txtSoLuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }
            if (txtGiaNhap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giá nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaNhap.Focus();
                return;
            }
            if (txtGiaBan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giá bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaBan.Focus();
                return;
            }

            sql = "SELECT MaCay FROM tbCay WHERE MaCay=N'" + txtMaCay.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã cây này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaCay.Focus();
                txtMaCay.Text = "";
                return;
            }
            sql = "INSERT INTO tbCay(MaCay, TenCay, MaLoai, MaNCC, NgayNhap, SoLuong, GiaNhap, GiaBan, GhiChu) " +
                "VALUES (N'" + txtMaCay.Text.Trim() + "',N'" + txtTenCay.Text.Trim() + "',N'" + cboTenLoai.SelectedValue.ToString() +
                "', N'" + cboTenNCC.SelectedValue.ToString() + "','" + dtNgNhap.Value + "','" + txtSoLuong.Text.Trim() + 
                "','" + txtGiaNhap.Text.Trim() + "','" + txtGiaBan.Text.Trim() + "',N'" + txtGhiChu.Text.Trim() + "')";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            txtMaCay.Enabled = false;
        }

        private void dgvDSCay_Click(object sender, EventArgs e)
        {
            string sql;
            string MaLoai, MaNCC ;
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaCay.Focus();
                return;
            }
            if (tbCay.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgvDSCay.CurrentRow != null)
            {
                txtMaCay.Text = dgvDSCay.CurrentRow.Cells["Mã cây"].Value.ToString();
                txtTenCay.Text = dgvDSCay.CurrentRow.Cells["Tên cây"].Value.ToString();

                MaLoai = dgvDSCay.CurrentRow.Cells["Mã loại"].Value.ToString();
                sql = "SELECT TenLoai FROM tbLoaiSP WHERE MaLoai=@MaLoai";
                cboTenLoai.Text = Functions.GetFieldValues(sql, new SqlParameter("@MaLoai", MaLoai));

                MaNCC = dgvDSCay.CurrentRow.Cells["Mã nhà cung cấp"].Value.ToString();
                sql = "SELECT TenNCC FROM tbNCC WHERE MaNCC=@MaNCC";
                cboTenNCC.Text = Functions.GetFieldValues(sql, new SqlParameter("@MaNCC", MaNCC));
            }
            string dateStringNgNhap = dgvDSCay.CurrentRow.Cells["Ngày nhập"].Value.ToString();
            DateTime nn;
            if (DateTime.TryParse(dateStringNgNhap, out nn))
            {
                dtNgNhap.Value = nn; // Giả sử dtNgNhap là một DateTimePicker
            }
            else
            {
                MessageBox.Show("Không được lớn hơn ngày hôm nay!");
                return;
            }

            txtSoLuong.Text = dgvDSCay.CurrentRow.Cells["Số Lượng"].Value.ToString();
            txtGiaNhap.Text = dgvDSCay.CurrentRow.Cells["Giá Nhập"].Value.ToString();
            txtGiaBan.Text = dgvDSCay.CurrentRow.Cells["Giá Bán"].Value.ToString();

            sql = "SELECT GhiChu FROM tbCay WHERE MaCay =@MaCay";
            txtGhiChu.Text = Functions.GetFieldValues(sql, new SqlParameter("@MaCay", txtMaCay.Text));

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
            btnTim.Enabled = false;
            txtMaCay.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaCay.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã cây", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaCay.Focus();
                return;
            }
            if (txtTenCay.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên cây", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenCay.Focus();
                return;
            }
            if (cboTenLoai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên loại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenLoai.Focus();
                return;
            }
            if (cboTenNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenNCC.Focus();
                return;
            }
            DateTime nn = dtNgNhap.Value;
            if (!CheckDate(nn))
            {
                MessageBox.Show("Không được lớn hơn ngày hôm nay!");
                return;
            }
            if (txtSoLuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }
            if (txtGiaNhap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giá nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaNhap.Focus();
                return;
            }
            if (txtGiaBan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giá bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaBan.Focus();
                return;
            }

            sql = "UPDATE tbCay SET TenCay = N'" + txtTenCay.Text.Trim() + "',MaLoai = N'" + cboTenLoai.SelectedValue.ToString() +
                "', MaNCC = N'" + cboTenNCC.SelectedValue.ToString() + "', NgayNhap ='" + dtNgNhap.Value +
                "',SoLuong = '" + txtSoLuong.Text.Trim() + "', GiaNhap = '" + txtGiaNhap.Text.Trim() +
                "', GiaBan ='" + txtGiaBan.Text.Trim() + "', GhiChu =N'" + txtGhiChu.Text.Trim() +
                "' WHERE MaCay = N'" + txtMaCay.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = true;
            btnLuu.Enabled = false;
            txtMaCay.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tbCay.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaCay.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tbCay WHERE MaCay=N'" + txtMaCay.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaCay.Text == "") && (txtTenCay.Text == "") && (cboTenLoai.Text == "") && (cboTenLoai.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện cần tìm ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * from tbCay WHERE 1=1";
            if (txtMaCay.Text != "")
                sql += " AND MaCay LIKE N'%" + txtMaCay.Text + "%'";
            if (txtTenCay.Text != "")
                sql += " AND TenCay LIKE N'%" + txtTenCay.Text + "%'";
            if (cboTenLoai.Text != "")
                sql += " AND MaLoai LIKE N'%" + cboTenLoai.Text + "%'";
            if (cboTenNCC.Text != "")
                sql += " AND MaNCC LIKE N'%" + cboTenNCC.Text + "%'";
            tbCay = Functions.GetDataToTable(sql);
            if (tbCay.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else 
                MessageBox.Show("Có " + tbCay.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvDSCay.DataSource = tbCay;
            ResetValue();
            btnHuy.Enabled = true;
            txtMaCay.Enabled = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnTim.Enabled = true;
            btnLuu.Enabled = false;
            txtMaCay.Enabled = true;
            LoadDataGridView();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaCay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTenCay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void cboTenLoai_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void cboTenNCC_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtGhiChu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void dtNgNhap_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtSoLuong_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtGiaNhap_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtGiaBan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnLuu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnThem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnHuy_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnSua_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnXoa_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnDong_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }
    }
}
