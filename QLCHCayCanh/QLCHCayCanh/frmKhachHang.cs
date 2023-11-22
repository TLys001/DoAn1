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
    public partial class frmKhachHang : Form
    {
        DataTable tbKhachHang;
        public frmKhachHang()
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
            sql = "SELECT * FROM tbKhachHang";
            tbKhachHang = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvDSKH.DataSource = tbKhachHang; //Nguồn dữ liệu            
            dgvDSKH.Columns[0].Name = "Mã khách hàng";
            dgvDSKH.Columns[1].Name = "Tên khách hàng";
            dgvDSKH.Columns[2].Name = "Giới tính";
            dgvDSKH.Columns[3].Name = "Số điện thoại";
            dgvDSKH.Columns[4].Name = "Địa chỉ";

            dgvDSKH.Columns[0].HeaderText = "Mã khách hàng";
            dgvDSKH.Columns[1].HeaderText = "Tên khách hàng";
            dgvDSKH.Columns[2].HeaderText = "Giới tính";
            dgvDSKH.Columns[3].HeaderText = "Số điện thoại";
            dgvDSKH.Columns[4].HeaderText = "Địa chỉ";

            dgvDSKH.Columns[0].Width = 70;
            dgvDSKH.Columns[1].Width = 150;
            dgvDSKH.Columns[2].Width = 70;
            dgvDSKH.Columns[3].Width = 130;
            dgvDSKH.Columns[4].Width = 200;


            dgvDSKH.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvDSKH.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }
        private void ResetValue()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            cboGTinh.SelectedIndex = 0;
            mtbSdt.Text = "";
            txtDchi.Text = "";
        }
        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            txtMaKH.Enabled = true;
            txtTenKH.Enabled = true;
            cboGTinh.Enabled = true;
            mtbSdt.Enabled = true;            
            txtDchi.Enabled = true;

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnTim.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            LoadDataGridView();
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
            txtMaKH.Enabled = true; //cho phép nhập mới
            txtMaKH.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (txtMaKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã Khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return;
            }
            if (txtTenKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
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
            if (mtbSdt.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbSdt.Focus();
                return;
            }

            if (txtDchi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDchi.Focus();
                return;
            }

            sql = "UPDATE tbKhachHang SET TenKH = N'" + txtTenKH.Text.Trim() + "', GioiTinh = N'" + gt + 
                "', Sdt = '" + mtbSdt.Text + "',  DChi = N'" + txtDchi.Text.Trim() + "' WHERE MaKH=N'" + txtMaKH.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = true;
            btnLuu.Enabled = false;
            txtMaKH.Enabled = false;
            cboGTinh.SelectedIndex = 0;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tbKhachHang.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tbNhanVien WHERE MaNV=N'" + txtMaKH.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaKH.Text == "") && (txtTenKH.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện cần tìm ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * from tbKhachHang WHERE 1=1";
            if (txtMaKH.Text != "")
                sql += " AND MaKH LIKE N'%" + txtMaKH.Text + "%'";
            if (txtTenKH.Text != "")
                sql += " AND TenKH LIKE N'%" + txtTenKH.Text + "%'";
            tbKhachHang = Functions.GetDataToTable(sql);
            if (tbKhachHang.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tbKhachHang.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvDSKH.DataSource = tbKhachHang;
            ResetValue();
            btnHuy.Enabled = true;
            txtMaKH.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (txtMaKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return;
            }
            if (txtTenKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
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

            sql = "SELECT MaKH FROM tbKhachHang WHERE MaKH=N'" + txtMaKH.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                txtMaKH.Text = "";
                return;
            }
            sql = "INSERT INTO tbKhachHang(MaKH, TenKH, GioiTinh, Sdt, DChi) " +
                "VALUES (N'" + txtMaKH.Text.Trim() + "',N'" + txtTenKH.Text.Trim() + 
                "', N'" + gt + "','" + mtbSdt.Text + "',N'" + txtDchi.Text.Trim() + "')";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            txtMaKH.Enabled = false;
            cboGTinh.SelectedIndex = 0;
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
            txtMaKH.Enabled = true;
            LoadDataGridView();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboGTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            string gioitinh = cboGTinh.SelectedItem.ToString();
        }

        private void dgvDSKH_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKH.Focus();
                return;
            }
            if (tbKhachHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaKH.Text = dgvDSKH.CurrentRow.Cells["Mã khách hàng"].Value.ToString();
            txtTenKH.Text = dgvDSKH.CurrentRow.Cells["Tên khách hàng"].Value.ToString();
            mtbSdt.Text = dgvDSKH.CurrentRow.Cells["Số điện thoại"].Value.ToString();

            if (dgvDSKH.CurrentRow.Cells["Giới Tính"].Value.ToString() == "Nam") cboGTinh.SelectedIndex = 0;
            else cboGTinh.SelectedIndex = 1;

            txtDchi.Text = dgvDSKH.CurrentRow.Cells["Địa chỉ"].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
            btnTim.Enabled = false;
            txtMaKH.Enabled = false;
        }
    }
}
