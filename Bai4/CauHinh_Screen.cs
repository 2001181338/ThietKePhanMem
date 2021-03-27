using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;

namespace Bai4
{
    public partial class CauHinh_Screen : Form
    {
        public CauHinh_Screen()
        {
            InitializeComponent();
        }

        private void CauHinh_Screen_Load(object sender, EventArgs e)
        {
            DataTable db = GetServerName();
            cboServer.DataSource = db;
            cboServer.DisplayMember = "ServerName";
         
        }
        
        public DataTable GetServerName()
        {
            DataTable dt = new DataTable();
            dt = SqlDataSourceEnumerator.Instance.GetDataSources();
            return dt;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            CauHinh.SaveConfig(cboServer.Text, txtUserName.Text, txtPassword.Text, cboDatabase.Text);
            this.Close();
            DanhNhap dangNhap = new DanhNhap();
            dangNhap.Show();
            this.Hide();

        }

        private void cboDatabase_MouseClick(object sender, MouseEventArgs e)
        {
            cboDatabase.DataSource = CauHinh.GetDBName(cboServer.Text, txtUserName.Text, txtPassword.Text);
            cboDatabase.DisplayMember = "name";
        }
    }
}
