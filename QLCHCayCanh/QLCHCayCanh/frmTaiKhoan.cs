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
 

namespace QLCHCayCanh
{
    public partial class frmTaiKhoan : Form
    {
        DataTable tbTaiKhoan;
        //private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
        //                            AttachDbFilename=D:\HK5\DoAn1\QLCHCayCanh\QLCHCayCanh\QLCHCAY.mdf;
        //                            Integrated Security=True;Connect Timeout=30";
        
        
        public frmTaiKhoan(/*string taiKhoan*/)
        {
            InitializeComponent();
            //DangNhap(taiKhoan);
        }
        private void dgvDSTK_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra xem cột hiện tại có phải là cột "Vai trò" không
            if (dgvDSTK.Columns[e.ColumnIndex].Name == "Vai trò")
            {
                if (e.Value != null)
                {
                    // Chuyển đổi giá trị của cell thành int
                    if (int.TryParse(e.Value.ToString(), out int vaitro))
                    {
                        switch (vaitro)
                        {
                            case 0:
                                e.Value = "Chủ";
                                break;
                            case 1:
                                e.Value = "Nhân viên";
                                break;
                        }
                    }

                }
            }
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM tbTaiKhoan";
            tbTaiKhoan = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvDSTK.DataSource = tbTaiKhoan; //Nguồn dữ liệu            
            dgvDSTK.Columns[0].Name = "Tên đăng nhập";
            dgvDSTK.Columns[1].Name = "Mật khẩu";
            dgvDSTK.Columns[2].Name = "Tên tài khoản";
            dgvDSTK.Columns[3].Name = "Vai trò";
            dgvDSTK.Columns[4].Name = "Mã nhân viên";

            dgvDSTK.Columns[0].HeaderText = "Tên đăng nhập";
            dgvDSTK.Columns[1].HeaderText = "Mật khẩu";
            dgvDSTK.Columns[2].HeaderText = "Tên tài khoản";
            dgvDSTK.Columns[3].HeaderText = "Vai trò";
            dgvDSTK.Columns[4].HeaderText = "Mã nhân viên";

            dgvDSTK.Columns[0].Width = 110;
            dgvDSTK.Columns[1].Width = 110;
            dgvDSTK.Columns[2].Width = 130;
            dgvDSTK.Columns[3].Width = 90;
            dgvDSTK.Columns[4].Width = 70;

            dgvDSTK.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvDSTK.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp

            // Gắn kết sự kiện CellFormatting với DataGridView
            dgvDSTK.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvDSTK_CellFormatting);
        }
    
        
        private void frmTaiKhoan_Load(object sender, EventArgs e)
        {
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            txtTenTK.Enabled = true;
            cboVaiTro.Enabled = true;
            cboMaNV.Enabled = true;
            
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnTim.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            LoadDataGridView();

            // Tạo một từ điển để ánh xạ giữa giá trị số và tên vai trò
            Dictionary<int, string> roles = new Dictionary<int, string>
            {
                { 0, "Chủ" },
                { 1, "Nhân viên" }
            };

            // Điền dữ liệu vào cboVaiTro
            cboVaiTro.DataSource = new BindingSource(roles, null);
            cboVaiTro.DisplayMember = "Value";
            cboVaiTro.ValueMember = "Key";
            
            cboVaiTro.SelectedValue = "";  // Đặt vai trò được chọn là "trống"



            //// Hiển thị tên vai trò trong cboVaiTro
            //string sql = "SELECT DISTINCT vaitro FROM tbTaiKhoan";
            //Functions.FillCombo(sql, cboVaiTro, "vaitro", "vaitro");

            // Hiển thị mã nhân viên trong cboMaNV
            string sql = "SELECT DISTINCT MaNV FROM tbNhanVien";
            Functions.FillCombo(sql, cboMaNV, "MaNV", "MaNV");
            cboMaNV.SelectedValue = "";
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
            txtUsername.Enabled = true; //cho phép nhập mới
            txtUsername.Focus();
            
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
            txtUsername.Enabled = true;
            LoadDataGridView();
        }
        private void ResetValue()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtTenTK.Text = "";
            cboVaiTro.Text = "";
            cboMaNV.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtUsername.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập username", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }
            if (txtPassword.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập password", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }
            if (txtTenTK.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTK.Focus();
                return;
            }
            if (cboVaiTro.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập vai trò", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboVaiTro.Focus();
                return;
            }
            if (cboMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNV.Focus();
                return;
            }

            sql = "SELECT username FROM tbTaiKhoan WHERE username=N'" + txtUsername.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Username này đã có, bạn phải nhập username khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                txtUsername.Text = "";
                return;
            }

            sql = "INSERT INTO tbTaiKhoan(username, password, tentk, vaitro, MaNV) " +
                "VALUES ('" + txtUsername.Text.Trim() + "',N'" + txtPassword.Text.Trim() + "','" + txtTenTK.Text +
                "',N'" + cboVaiTro.SelectedValue.ToString() + "',N'" + cboMaNV.SelectedValue.ToString() + "')";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            txtUsername.Enabled = false;

            int selectedRole = (int)cboVaiTro.SelectedValue;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtUsername.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập username", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }
            if (txtPassword.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập password", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }
            if (txtTenTK.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTK.Focus();
                return;
            }
            if (cboVaiTro.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập vai trò", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboVaiTro.Focus();
                return;
            }
            if (cboMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNV.Focus();
                return;
            }

            sql = "Update tbTaiKhoan SET password = N'" + txtPassword.Text.Trim() + 
                "',tentk = N'" + txtTenTK.Text.Trim() + "',vaitro = N'" + cboVaiTro.SelectedValue.ToString() +
                "',MaNV = N'" + cboMaNV.SelectedValue.ToString() +
                "' WHERE username= N'" + txtUsername.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = true;
            btnLuu.Enabled = false;

            txtUsername.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tbTaiKhoan.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tbTaiKhoan WHERE username=N'" + txtUsername.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtUsername.Text == "") && (txtTenTK.Text == "") && (cboVaiTro.Text == "") && (cboMaNV.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện cần tìm ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * from tbTaiKhoan WHERE 1=1";
            if (txtUsername.Text != "")
                sql += " AND username LIKE N'%" + txtUsername.Text + "%'";
            if (txtTenTK.Text != "")
                sql += " AND tentk LIKE N'%" + txtTenTK.Text + "%'";
            if (cboVaiTro.Text != "")
                sql += " AND vaitro LIKE N'%" + cboVaiTro.Text + "%'";
            if (cboMaNV.Text != "")
                sql += " AND MaNV LIKE N'%" + cboMaNV.Text + "%'";

            tbTaiKhoan = Functions.GetDataToTable(sql);
            if (tbTaiKhoan.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tbTaiKhoan.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvDSTK.DataSource = tbTaiKhoan;
            ResetValue();
            btnHuy.Enabled = true;
            txtUsername.Enabled = true;
        }

        private void dgvDSTK_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.Focus();
                return;
            }
            if (tbTaiKhoan.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtUsername.Text = dgvDSTK.CurrentRow.Cells["Tên đăng nhập"].Value.ToString();
            txtTenTK.Text = dgvDSTK.CurrentRow.Cells["Tên tài khoản"].Value.ToString();
            txtPassword.Text = dgvDSTK.CurrentRow.Cells["Mật khẩu"].Value.ToString();
            cboVaiTro.Text = dgvDSTK.CurrentRow.Cells["Vai trò"].Value.ToString();
            cboMaNV.Text = dgvDSTK.CurrentRow.Cells["Mã nhân viên"].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
            btnTim.Enabled = false;
            txtUsername.Enabled = false;

            // Tạo một từ điển để ánh xạ giữa giá trị số và tên vai trò
            Dictionary<int, string> roles = new Dictionary<int, string>
            {
                { 0, "Chủ" },
                { 1, "Nhân viên" }
            };

            // Điền dữ liệu vào cboVaiTro
            cboVaiTro.DataSource = new BindingSource(roles, null);
            cboVaiTro.DisplayMember = "Value";
            cboVaiTro.ValueMember = "Key";

           
            cboVaiTro.SelectedValue = "";  // Đặt vai trò được chọn là "Nhân viên"
            if (dgvDSTK.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dgvDSTK.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgvDSTK.Rows[selectedRowIndex];

                cboVaiTro.SelectedValue = selectedRow.Cells["Vai trò"].Value;
            }
        }

    }
}
