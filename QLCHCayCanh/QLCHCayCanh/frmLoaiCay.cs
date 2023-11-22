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
    public partial class frmDMLoaiCay : Form
    {
        DataTable tbLoaiSP;
        //private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
        //                            AttachDbFilename=D:\HK5\DoAn1\QLCHCayCanh\QLCHCayCanh\QLCHCAY.mdf;
        //                            Integrated Security=True;Connect Timeout=30";

        public frmDMLoaiCay()
        {
            InitializeComponent();
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM tbLoaiSP";
            tbLoaiSP = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvDSLoai.DataSource = tbLoaiSP; //Nguồn dữ liệu            
            dgvDSLoai.Columns[0].Name = "Mã loại cây";
            dgvDSLoai.Columns[1].Name = "Tên loại cây";
            dgvDSLoai.Columns[2].Name = "Ghi chú";
            dgvDSLoai.Columns[0].HeaderText = "Mã loại cây";
            dgvDSLoai.Columns[1].HeaderText = "Tên loại cây";
            dgvDSLoai.Columns[2].HeaderText = "Ghi chú";
            
            dgvDSLoai.Columns[0].Width = 100;
            dgvDSLoai.Columns[1].Width = 150;
            dgvDSLoai.Columns[2].Width = 190;
            
            dgvDSLoai.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvDSLoai.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }
        private void frmDMLoaiSP_Load(object sender, EventArgs e)
        {
            cboMaLoai.Enabled = true;
            cboTenLoai.Enabled = true;
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
            Functions.FillCombo(sql, cboMaLoai, "MaLoai", "TenLoai");
            cboMaLoai.SelectedIndex = -1;
            
            sql = "SELECT * from tbLoaiSP";
            Functions.FillCombo(sql, cboTenLoai, "TenLoai", "TenLoai");
            cboTenLoai.SelectedIndex = -1;

            ResetValue();
        }


        private void ResetValue()
        {
            cboMaLoai.Text = "";
            cboTenLoai.Text = "";
            txtGhiChu.Text = "";
        }


        private void dgvDSLoai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }


        private void txtGhiChu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void cboMaLoai_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void cboTenLoai_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void dgvDSLoai_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaLoai.Focus();
                return;
            }
            if (tbLoaiSP.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            cboMaLoai.Text = dgvDSLoai.CurrentRow.Cells["Mã loại cây"].Value.ToString();
            cboTenLoai.Text = dgvDSLoai.CurrentRow.Cells["Tên loại cây"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnTim.Enabled = true;
            btnHuy.Enabled = true;
            cboMaLoai.Enabled = false;
        }

        private void btnTim_Click_1(object sender, EventArgs e)
        {
            string sql;
            if ((cboMaLoai.Text == "") && (cboTenLoai.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện cần tìm ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * from tbLoaiSP WHERE 1=1";
            if (cboMaLoai.Text != "")
                sql += " AND MaLoai LIKE N'%" + cboMaLoai.Text + "%'";
            if (cboTenLoai.Text != "")
                sql += " AND TenLoai LIKE N'%" + cboTenLoai.Text + "%'";
            tbLoaiSP = Functions.GetDataToTable(sql);
            if (tbLoaiSP.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tbLoaiSP.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvDSLoai.DataSource = tbLoaiSP;
            ResetValue();
            btnHuy.Enabled = true;
            cboMaLoai.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tbLoaiSP.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMaLoai.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tbLoaiSP WHERE MaLoai=N'" + cboMaLoai.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (tbLoaiSP.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMaLoai.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboTenLoai.Text.Trim().Length == 0) //nếu chưa nhập tên loại
            {
                MessageBox.Show("Bạn chưa nhập tên loại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE tbLoaiSP SET TenLoai=N'" + cboTenLoai.Text.ToString() + "',GhiChu='" + txtGhiChu.Text.ToString() +
                "' WHERE MaLoai=N'" + cboMaLoai.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            cboMaLoai.Enabled = false;
            btnHuy.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (cboMaLoai.Text.Trim().Length == 0) //Nếu chưa nhập mã loại
            {
                MessageBox.Show("Bạn phải nhập mã loại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaLoai.Focus();
                return;
            }
            if (cboTenLoai.Text.Trim().Length == 0) //Nếu chưa nhập tên loại
            {
                MessageBox.Show("Bạn phải nhập tên loại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboTenLoai.Focus();
                return;
            }
            sql = "Select MaLoai From tbLoaiSP where MaLoai=N'" + cboMaLoai.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã loại này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaLoai.Focus();
                return;
            }

            sql = "INSERT INTO tbLoaiSP(MaLoai, TenLoai, GhiChu) " +
                "VALUES(N'" + cboMaLoai.Text.Trim() + "',N'" + cboTenLoai.Text + "',N'" + txtGhiChu.Text + "')";
            Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            cboMaLoai.Enabled = false;
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
            cboMaLoai.Enabled = true;
            LoadDataGridView();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
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
            cboMaLoai.Enabled = true; //cho phép nhập mới
            cboMaLoai.Focus();
        }
    }
}
