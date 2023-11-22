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
    public partial class frmNhanVien : Form
    {
        DataTable tbNhanVien;
        public frmNhanVien()
        {
            InitializeComponent();
            cboGTinh.Items.Add("Nam");
            cboGTinh.Items.Add("Nữ");

            // Đặt mục được chọn mặc định
            cboGTinh.SelectedIndex = 0;
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM tbNhanVien";
            tbNhanVien = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvDSNV.DataSource = tbNhanVien; //Nguồn dữ liệu            
            dgvDSNV.Columns[0].Name = "Mã nhân viên";
            dgvDSNV.Columns[1].Name = "Tên nhân viên";
            dgvDSNV.Columns[2].Name = "Số điện thoại";
            dgvDSNV.Columns[3].Name = "Ngày sinh";
            dgvDSNV.Columns[4].Name = "Giới tính";
            dgvDSNV.Columns[5].Name = "Địa chỉ";
            dgvDSNV.Columns[6].Name = "Ngày nhận";

            dgvDSNV.Columns[0].HeaderText = "Mã nhân viên";
            dgvDSNV.Columns[1].HeaderText = "Tên nhân viên";
            dgvDSNV.Columns[2].HeaderText = "Số điện thoại";
            dgvDSNV.Columns[3].HeaderText = "Ngày sinh";
            dgvDSNV.Columns[4].HeaderText = "Giới tính";
            dgvDSNV.Columns[5].HeaderText = "Địa chỉ";
            dgvDSNV.Columns[6].HeaderText = "Ngày nhận";

            dgvDSNV.Columns[0].Width = 70;
            dgvDSNV.Columns[1].Width = 130;
            dgvDSNV.Columns[2].Width = 100;
            dgvDSNV.Columns[3].Width = 100;
            dgvDSNV.Columns[4].Width = 50;
            dgvDSNV.Columns[5].Width = 150;
            dgvDSNV.Columns[6].Width = 100;

            dgvDSNV.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvDSNV.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
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


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtDchi_TextChanged(object sender, EventArgs e)
        {

        }


        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            txtMaNV.Enabled = true;
            txtTenNV.Enabled = true;
            mtbSdt.Enabled = true;
            dtNgSinh.Enabled = true;
            cboGTinh.Enabled = true;
            txtDchi.Enabled = true;
            dtNgNhan.Enabled = true;

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnTim.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            LoadDataGridView();
        }

        private void dgvDSNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
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
            txtMaNV.Enabled = true; //cho phép nhập mới
            txtMaNV.Focus();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ResetValue()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            mtbSdt.Text = "";
            dtNgSinh.Value = DateTime.Today;
            cboGTinh.SelectedIndex = 0;
            txtDchi.Text = "";
            dtNgNhan.Value = DateTime.Today;
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
            txtMaNV.Enabled = true;
            LoadDataGridView();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (txtMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNV.Focus();
                return;
            }
            if (txtTenNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNV.Focus();
                return;
            }
            if (mtbSdt.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbSdt.Focus();
                return;
            }

            string gioitinh = cboGTinh.SelectedItem.ToString();         
            if (gioitinh == "Nam")
            {
                gt = "Nam";
            }
            else
            {
                gt = "Nữ";
            }
            if (txtDchi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDchi.Focus();
                return;
            }

            DateTime nn = dtNgNhan.Value;
            if (!CheckDate(nn))
            {
                MessageBox.Show("Không được lớn hơn ngày hôm nay!");
                return;
            }
            

            sql = "SELECT MaNV FROM tbNhanVien WHERE MaNV=N'" + txtMaNV.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNV.Focus();
                txtMaNV.Text = "";
                return;
            }
            sql = "INSERT INTO tbNhanVien(MaNV, TenNV, Sdt, NgaySinh, GioiTinh, DChi, NgayNhan) " +
                "VALUES (N'" + txtMaNV.Text.Trim() + "',N'" + txtTenNV.Text.Trim() + "','" + mtbSdt.Text + "','" + dtNgSinh.Value + "', N'" + gt + "',N'" + txtDchi.Text.Trim() + "','" + dtNgNhan.Value +"')";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            txtMaNV.Enabled = false;
            cboGTinh.SelectedIndex = 0;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (txtMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNV.Focus();
                return;
            }
            if (txtTenNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNV.Focus();
                return;
            }
            if (mtbSdt.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbSdt.Focus();
                return;
            }

            string gioitinh = cboGTinh.SelectedItem.ToString();
            if (gioitinh == "Nam")
            {
                gt = "Nam";
            }
            else
            {
                gt = "Nữ";
            }
            if (txtDchi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDchi.Focus();
                return;
            }

            DateTime nn = dtNgNhan.Value;
            if (!CheckDate(nn))
            {
                MessageBox.Show("Không được lớn hơn ngày hôm nay!");
                return;
            }

            //sql = "UPDATE tbNhanVien SET TenNV = N'" + txtTenNV.Text.Trim() + "', Sdt = '" + mtbSdt.Text + 
            //    "', NgaySinh = '" + dtNgSinh.Value + "', GioiTinh = '" + "N'" + gt + 
            //    "', DChi = N'" + txtDchi.Text.Trim() + "', NgayNhan = '" + dtNgNhan.Value + 
            //    "' WHERE MaNV=N'" + txtMaNV.Text + "'";
            sql = "UPDATE tbNhanVien SET TenNV = N'" + txtTenNV.Text.Trim() + "', Sdt = '" + mtbSdt.Text +
                "', NgaySinh = '" + dtNgSinh.Value.ToString("yyyy-MM-dd") + "', GioiTinh = N'" + gt +
                "', DChi = N'" + txtDchi.Text.Trim() + "', NgayNhan = '" + dtNgNhan.Value.ToString("yyyy-MM-dd") +
                "' WHERE MaNV=N'" + txtMaNV.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = true;
            btnLuu.Enabled = false;
            txtMaNV.Enabled = false;
            cboGTinh.SelectedIndex = 0;
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tbNhanVien.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tbNhanVien WHERE MaNV=N'" + txtMaNV.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void txtMaNV_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTenNV_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void mtbSdt_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mtbSdt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }


        private void txtDchi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }


        private void btnTim_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaNV.Text == "") && (txtTenNV.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện cần tìm ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * from tbNhanVien WHERE 1=1";
            if (txtMaNV.Text != "")
                sql += " AND MaNV LIKE N'%" + txtMaNV.Text + "%'";
            if (txtTenNV.Text != "")
                sql += " AND TenNV LIKE N'%" + txtTenNV.Text + "%'";
            tbNhanVien = Functions.GetDataToTable(sql);
            if (tbNhanVien.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tbNhanVien.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvDSNV.DataSource = tbNhanVien;
            ResetValue();
            btnHuy.Enabled = true;
            txtMaNV.Enabled = true;
        }

        private void cboGTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            string gioitinh = cboGTinh.SelectedItem.ToString();
        }

        private void dgvDSNV_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNV.Focus();
                return;
            }
            if (tbNhanVien.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaNV.Text = dgvDSNV.CurrentRow.Cells["Mã nhân viên"].Value.ToString();
            txtTenNV.Text = dgvDSNV.CurrentRow.Cells["Tên nhân viên"].Value.ToString();
            mtbSdt.Text = dgvDSNV.CurrentRow.Cells["Số điện thoại"].Value.ToString();

            if (dgvDSNV.CurrentRow.Cells["Giới Tính"].Value.ToString() == "Nam") cboGTinh.SelectedIndex = 0;
            else cboGTinh.SelectedIndex = 1;

            txtDchi.Text = dgvDSNV.CurrentRow.Cells["Địa chỉ"].Value.ToString();

            string dateStringNgSinh = dgvDSNV.CurrentRow.Cells["Ngày sinh"].Value.ToString();

            DateTime ns;
            if (DateTime.TryParse(dateStringNgSinh, out ns))
            {
                dtNgSinh.Value = ns; // Giả sử dtNgNhan là một DateTimePicker
            }
            else
            {
                MessageBox.Show("Không được lớn hơn ngày hôm nay!");
                return;
            }
            string dateStringNgNhan = dgvDSNV.CurrentRow.Cells["Ngày nhận"].Value.ToString();
            DateTime nn;
            if (DateTime.TryParse(dateStringNgNhan, out nn))
            {
                dtNgNhan.Value = nn; // Giả sử dtNgNhan là một DateTimePicker
            }
            else
            {
                MessageBox.Show("Không được lớn hơn ngày hôm nay!");
                return;
            }

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
            btnTim.Enabled = false;
            txtMaNV.Enabled = false;
        }
    }
}
