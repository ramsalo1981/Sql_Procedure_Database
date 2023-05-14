using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Database_Procedure
{
    public partial class Form1 : Form
    {
        static string strConn = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            //***** Display data by using DataSet ******

            string sql = "SELECT TB_Users.Name, TB_Users.Email FROM TB_Users; " +
                         "SELECT TB_Users.Name, TB_Users.Password FROM TB_Users;";

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ds.Tables[0].TableName = "NameEmail";
            ds.Tables[1].TableName = "NamePassword";
            dgvUsers.DataSource = ds.Tables["NameEmail"];
            dgvEmail.DataSource = ds.Tables["NamePassword"];


            //***** Display Data by using sqlDataAdaptar *****

            //string sql = "SELECT * FROM TB_Users;";
            //SqlDataAdapter ad = new SqlDataAdapter(sql,conn);
            //DataTable dt = new DataTable();
            //ad.Fill(dt);
            //dgvUsers.DataSource = dt;


            //***** Display data by using datatable *****

            //string sql = "SELECT * FROM TB_Users;";
            //SqlCommand sqlComm = new SqlCommand(sql, conn);

            //DataTable dt = new DataTable();
            //dt.Columns.Add("Name");
            //dt.Columns.Add("Email");
            //dt.Columns.Add("Password");

            //conn.Open();

            //using (SqlDataReader dr = sqlComm.ExecuteReader())
            //{
            //    while (dr.Read())
            //    {
            //        DataRow row = dt.NewRow();
            //        row["Name"] = dr[0];
            //        row["Email"] = dr[1];
            //        row["Password"] = dr[2];

            //        dt.Rows.Add(row);

            //    }

            //    dgvUsers.DataSource = dt;
            //}


            //********Display data on datagridview*****

            //string sql = "Select * FROM TB_Users;";
            //SqlCommand sqlComm = new SqlCommand(sql, conn);
            //conn.Open();

            //using (SqlDataReader dr = sqlComm.ExecuteReader())
            //{
            //    dgvUsers.Columns.Add("Name", "Name");
            //    dgvUsers.Columns.Add("Email", "Email");
            //    dgvUsers.Columns.Add("Password", "Password");

            //    while (dr.Read())
            //    {
            //        dgvUsers.Rows.Add(dr[0], dr[1], dr[2]);
            //    }
            //}



            // *******Display data in Messegebox******

            //SqlCommand sqlComm = new SqlCommand("sp_selectData", conn);
            //sqlComm.CommandType = CommandType.StoredProcedure;


            //try
            //{
            //    conn.Open();
            //    var dr = sqlComm.ExecuteReader();
            //    while (dr.Read())
            //    {
            //        MessageBox.Show($"{dr[0].ToString()} {dr[1].ToString()} {dr[2].ToString()}");
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show($"Error!! \n {ex}");
            //}
            //finally { conn.Close(); }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //use using
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("sp_InsertData", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@name", txtName.Text);
                sqlComm.Parameters.AddWithValue("@email", txtEmail.Text);
                sqlComm.Parameters.AddWithValue("@password", txtPassword.Text);
                conn.Open();
                sqlComm.ExecuteNonQuery();
            }

            //use try and catch*********

            //SqlCommand sqlComm = new SqlCommand("sp_InsertData", conn);
            //sqlComm.CommandType = CommandType.StoredProcedure;
            //sqlComm.Parameters.AddWithValue("@name", txtName.Text);
            //sqlComm.Parameters.AddWithValue("@email", txtEmail.Text);
            //sqlComm.Parameters.AddWithValue("@password", txtPassword.Text);

            //try
            //{
            //    conn.Open();
            //    sqlComm.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show($"Error!! \n {ex}");

            //}
            //finally { conn.Close(); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sql = $"DELETE FROM TB_Users WHERE name = @pName; ";
            SqlCommand sqlComm = new SqlCommand(sql,conn);
            sqlComm.Parameters.AddWithValue("@pName",txtName.Text);
            conn.Open();

            sqlComm.ExecuteNonQuery();

            conn.Close();

            //string sql = $"DELETE FROM TB_Users WHERE name = '{txtName.Text}'; ";
            //SqlCommand sqlComm = new SqlCommand(sql,conn);
            //conn.Open();
            //sqlComm.ExecuteNonQuery();
            //conn.Close();
        }
    }
}
