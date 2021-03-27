using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace Bai4
{
    class CauHinh
    {


        public static int Check_Config()
        {
            if (Properties.Settings.Default.Connect == string.Empty)
                return 1;
            SqlConnection _sqlCon = new SqlConnection(Properties.Settings.Default.Connect);
            try
            {
                if (_sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    _sqlCon.Open();
                    _sqlCon.Close();
                }
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public static LoginResult Check_User(string pUser, string pPass)
        {
           string str = "select * from NguoiDung where TenDangNhap = '" + pUser + "' and MatKhau = '" + pPass + "'";
           SqlDataAdapter daUser = new SqlDataAdapter(str, Properties.Settings.Default.Connect);
           DataTable dt = new DataTable();
           daUser.Fill(dt);
           if(dt.Rows.Count == 0)
           {
               return LoginResult.Invalid;
           }
               //check user active
           else if(dt.Rows[0][2] == null || dt.Rows[0][2].ToString() == "False")
           {
               return LoginResult.Disabled;
           }
           else
           {
               return LoginResult.Success;
           }
        }
        public static DataTable GetDBName(string pServer, string pUser, string pPass)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select name from sys.Databases",
            "Data Source=" + pServer + ";Initial Catalog=master;User ID=" + pUser + ";pwd = " +
            pPass + "");
            da.Fill(dt);
            return dt;
        }
        public static void SaveConfig(string pServer, string pUser, string pPass, string pDBname)
        {
            {
                Bai4.Properties.Settings.Default.Connect = "Data Source=" + pServer +
                ";Initial Catalog=" + pDBname + ";User ID=" + pUser + ";pwd = " + pPass + "";
                Bai4.Properties.Settings.Default.Save();
            }
        }
    }
}
