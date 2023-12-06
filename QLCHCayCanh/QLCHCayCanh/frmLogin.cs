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
using QLCHCayCanh.Class;

namespace QLCHCayCanh
{
    public partial class frmLogin : Form
    {
       
        
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {

            if (chkHienMatKhau.Checked)
            {
                txtMatKhau.PasswordChar = '\0'; // Hiển thị mật khẩu
            }
            else
            {
                txtMatKhau.PasswordChar = '*'; // Ẩn mật khẩu
            }

        }

        private int KiemTraDangNhap(string taiKhoan, string matKhau)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;
            AttachDbFilename = D:\HK5\DoAn1\QLCHCayCanh\QLCHCayCanh\QLCHCAY.mdf;
            Integrated Security = True; Connect Timeout = 30"))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT vaitro FROM tbTaiKhoan WHERE username = @username AND password = @password", conn))
                {
                    cmd.Parameters.AddWithValue("@username", taiKhoan);
                    cmd.Parameters.AddWithValue("@password", matKhau);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(reader.GetOrdinal("vaitro"));

                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
        }

    private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTaiKhoan.Text;
            string matKhau = txtMatKhau.Text;

            int vaitro = KiemTraDangNhap(taiKhoan, matKhau);

            if (vaitro != -1)
            {
                frmMain mainForm = new frmMain(/* taiKhoan*/);

                if (vaitro == 1)
                {
                    mainForm.AnMenu();
                }

                this.Hide();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Thông tin đăng nhập không chính xác. Vui lòng thử lại.");
            }

        }

        private void txtTaiKhoan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtMatKhau_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

    }
}
