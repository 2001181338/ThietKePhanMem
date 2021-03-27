using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai4
{
    public partial class DanhNhap : Form
    {
        public DanhNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                MessageBox.Show("Không được bỏ trống UserName");
                this.txtUsername.Focus();
                return;
            }
            if (string.IsNullOrEmpty(this.txtPassword.Text))
            {
                MessageBox.Show("Không được bỏ trống Password");
                this.txtPassword.Focus();
                return;
            }
            int kq = CauHinh.Check_Config(); //hàm Check_Config() thuộc Class QL_NguoiDung
            if (kq == 0)
            {
                ProcessLogin();// Cấu hình phù hợp xử lý đăng nhập
            }
            if (kq == 1)
            {
                MessageBox.Show("Chuỗi cấu hình không tồn tại");// Xử lý cấu hình
                ProcessConfig();
            }
            if (kq == 2)
            {
                MessageBox.Show("Chuỗi cấu hình không phù hợp");// Xử lý cấu hình
                ProcessConfig();
            }
        }

        public void ProcessLogin()
        {
            LoginResult result;
            result = CauHinh.Check_User(txtUsername.Text, txtPassword.Text);
            if (result == LoginResult.Invalid)
            {
                MessageBox.Show("Vui lòng kiểm tra lại Username hoặc Password");
                return;
            }
            else if (result == LoginResult.Disabled)
            {
                MessageBox.Show("Tài khoản bị khóa");
                return;
            }
            else
            {
                this.Visible = false;
                FormMain formMain = new FormMain();
                formMain.Visible = true;
            }
        }
        public void ProcessConfig()
        {
            this.Hide();
            CauHinh_Screen screen = new CauHinh_Screen();
            screen.ShowDialog();
        }
    }
}
