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

namespace POS
{
    public partial class formLogin: Form
    {
        public formLogin()
        {
            InitializeComponent();
        }

        private void lblSignin_Click(object sender, EventArgs e)
        {
            try
            {
                DBConfig.conn.Open();
                //Field
                string sql;
                var adapter = new SqlDataAdapter();
                var empTB = new DataTable();

                sql = "SELECT * FROM Employees";
                adapter.SelectCommand = new SqlCommand(sql, DBConfig.conn);
                adapter.Fill(empTB);

                sql = $"username ='{txtUsername.Text}' AND " +
                      $"password = '{txtPassword.Text}'";
                try
                {
                    DataRow[] dr = empTB.Select(sql);
                    EmployeeData.empID = dr[0]["emp_id"].ToString();
                    EmployeeData.empFname = dr[0]["fname"].ToString();
                    EmployeeData.empLname = dr[0]["lname"].ToString();

                    DBConfig.conn.Close();
                    var Index = new formIndex();
                    Index.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Login Failed !", "Warning");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            DBConfig.conn.Close();
        }
    }
}
