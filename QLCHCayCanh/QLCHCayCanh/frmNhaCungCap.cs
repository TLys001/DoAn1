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
    public partial class frmNhaCungCap : Form
    {
        DataTable tbNCC;
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                    AttachDbFilename=D:\HK5\DoAn1\QLCHCayCanh\QLCHCayCanh\QLCHCAY.mdf;
                                    Integrated Security=True;Connect Timeout=30";
        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM tbNCC";
            tbNCC = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvDSNCC.DataSource = tbNCC; //Nguồn dữ liệu            
            dgvDSNCC.Columns[0].Name = "Mã nhà cung cấp";
            dgvDSNCC.Columns[1].Name = "Tên nhà cung cấp";
            dgvDSNCC.Columns[2].Name = "Số điện thoại";
            dgvDSNCC.Columns[3].Name = "Địa chỉ";
            dgvDSNCC.Columns[4].Name = "Email";
            dgvDSNCC.Columns[5].Name = "Ghi chú";           
            dgvDSNCC.Columns[0].HeaderText = "Mã nhà cung cấp";
            dgvDSNCC.Columns[1].HeaderText = "Tên nhà cung cấp";
            dgvDSNCC.Columns[2].HeaderText = "Số điện thoại";
            dgvDSNCC.Columns[3].HeaderText = "Địa chỉ";
            dgvDSNCC.Columns[4].HeaderText = "Email";
            dgvDSNCC.Columns[5].HeaderText = "Ghi chú";

            dgvDSNCC.Columns[0].Width = 70;
            dgvDSNCC.Columns[1].Width = 200;
            dgvDSNCC.Columns[2].Width = 100;
            dgvDSNCC.Columns[3].Width = 200;
            dgvDSNCC.Columns[4].Width = 150;
            dgvDSNCC.Columns[5].Width = 200;

            dgvDSNCC.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvDSNCC.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }
        private void txtDchi_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            cboMaNCC.Enabled = true;
            cboTenNCC.Enabled = true;
            mtbSdt.Enabled = true;
            txtDchi.Enabled = true;
            txtEmail.Enabled = true;
            txtGhiChu.Enabled = true;

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
            cboMaNCC.Enabled = true; //cho phép nhập mới
            cboMaNCC.Focus();


        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ResetValue()
        {
            cboMaNCC.Text = "";
            cboTenNCC.Text = "";
            mtbSdt.Text = "";
            txtDchi.Text = "";
            txtEmail.Text = "";
            txtGhiChu.Text = "";
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
            cboMaNCC.Enabled = true;
            LoadDataGridView();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (cboMaNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNCC.Focus();
                return;
            }
            if (cboTenNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenNCC.Focus();
                return;
            }
            if (mtbSdt.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbSdt.Focus();
                return;
            }
            if (txtDchi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDchi.Focus();
                return;
            }
            if (txtEmail.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập email", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }
            sql = "SELECT MaNCC FROM tbNCC WHERE MaNCC=N'" + cboMaNCC.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhà cung cấp này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNCC.Focus();
                cboMaNCC.Text = "";
                return;
            }
            sql = "INSERT INTO tbNCC(MaNCC, TenNCC, Sdt, DChi, Email, GhiChu) " +
                "VALUES ('" + cboMaNCC.Text.Trim() + "',N'" + cboTenNCC.Text.Trim() + "','" + mtbSdt.Text +
                "',N'" + txtDchi.Text.Trim() + "','" + txtEmail.Text.Trim() + "','" + txtGhiChu.Text.Trim() + "')";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            cboMaNCC.Enabled = false;
            

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (cboMaNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNCC.Focus();
                return;
            }
            if (cboTenNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenNCC.Focus();
                return;
            }
            if (mtbSdt.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbSdt.Focus();
                return;
            }
            if (txtDchi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDchi.Focus();
                return;
            }
            if (txtEmail.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập email", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            sql = "Update tbNCC set TenNCC = N'"+ cboTenNCC.Text.Trim() +"',Sdt ='" +mtbSdt.Text.Trim() + "',DChi = N'" + txtDchi.Text.Trim() +
                "',Email = '" + txtEmail.Text.Trim() + "', GhiChu = N'" + txtGhiChu.Text.Trim() + "' WHERE MaNCC='" + cboMaNCC.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = true;
            btnLuu.Enabled = false;

            cboMaNCC.Enabled = false;


        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tbNCC.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMaNCC.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tbNCC WHERE MaNCC=N'" + cboMaNCC.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string sql;
            if ((cboMaNCC.Text == "") && (cboTenNCC.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện cần tìm ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * from tbNCC WHERE 1=1";
            if (cboMaNCC.Text != "")
                sql += " AND MaNCC LIKE N'%" + cboMaNCC.Text + "%'";
            if (cboTenNCC.Text != "")
                sql += " AND TenNCC LIKE N'%" + cboTenNCC.Text + "%'";
            tbNCC = Functions.GetDataToTable(sql);
            if (tbNCC.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tbNCC.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvDSNCC.DataSource = tbNCC;
            ResetValue();
            btnHuy.Enabled = true;
            cboMaNCC.Enabled = true;
        }

        private void dgvDSNCC_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaNCC.Focus();
                return;
            }
            if (tbNCC.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            cboMaNCC.Text = dgvDSNCC.CurrentRow.Cells["Mã nhà cung cấp"].Value.ToString();
            cboTenNCC.Text = dgvDSNCC.CurrentRow.Cells["Tên nhà cung cấp"].Value.ToString();
            mtbSdt.Text = dgvDSNCC.CurrentRow.Cells["Số điện thoại"].Value.ToString();
            txtDchi.Text = dgvDSNCC.CurrentRow.Cells["Địa chỉ"].Value.ToString();
            txtEmail.Text = dgvDSNCC.CurrentRow.Cells["Email"].Value.ToString();
            txtGhiChu.Text = dgvDSNCC.CurrentRow.Cells["Ghi chú"].Value.ToString();
            
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
            btnTim.Enabled = false;
            cboMaNCC.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
