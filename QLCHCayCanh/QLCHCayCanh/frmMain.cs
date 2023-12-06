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
    public partial class frmMain : Form
    {
        //private string taiKhoanDangNhap;

        public frmMain(/*string taiKhoan*/)
        {
            InitializeComponent();
            //taiKhoanDangNhap = taiKhoan;
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            Functions.Connect();
            
               
        }
        public void AnMenu()
        {
            mnuDanhMuc.Visible = false;
            mnuThongKe.Visible = false;
            mnuTaiKhoan.Visible = false;
        }

        private void loạiSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDMLoaiCay frmloaicay = new frmDMLoaiCay(); //Khởi tạo đối tượng
            frmloaicay.MdiParent = this;
            frmloaicay.Show();
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Functions.Disconnect(); //Đóng kết nối
            this.Hide();
            frmLogin loginForm = new frmLogin();
            loginForm.Show();

        }

        private void mnuTaiKhoan_Click(object sender, EventArgs e)
        {
            frmTaiKhoan frmtaikhoan = new frmTaiKhoan(/*taiKhoanDangNhap*/); //Khởi tạo đối tượng
            frmtaikhoan.MdiParent = this;
            frmtaikhoan.Show();
        }

        private void mnuNCC_Click(object sender, EventArgs e)
        {
            frmNhaCungCap frmncc = new frmNhaCungCap(); //Khởi tạo đối tượng
            frmncc.MdiParent = this;
            frmncc.Show();

        }

        private void mnuNV_Click(object sender, EventArgs e)
        {
            frmNhanVien frmnv = new frmNhanVien(); //Khởi tạo đối tượng
            frmnv.MdiParent = this;
            frmnv.Show();
        }

        private void mnuKH_Click(object sender, EventArgs e)
        {
            frmKhachHang frmkh = new frmKhachHang(); //Khởi tạo đối tượng
            frmkh.MdiParent = this;
            frmkh.Show();
        }

        private void mnuCay_Click(object sender, EventArgs e)
        {
            frmCay frmcay = new frmCay(); //Khởi tạo đối tượng
            frmcay.MdiParent = this;
            frmcay.Show();
        }


        private void mnuThongKe_Click(object sender, EventArgs e)
        {
            frmThongKe frmTKe = new frmThongKe(); //Khởi tạo đối tượng
            frmTKe.MdiParent = this;
            frmTKe.Show();
        }

        private void mnuHoaDon_Click(object sender, EventArgs e)
        {
            frmHoaDon frmHD = new frmHoaDon(); //Khởi tạo đối tượng
            frmHD.MdiParent = this;
            frmHD.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
                
        }
    }
}
