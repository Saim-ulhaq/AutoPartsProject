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

namespace AutoPartsProject
{
    public partial class CreateAccount : Form
    {
        public CreateAccount()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=MUQEETRANA3802\SQLEXPRESS;Initial Catalog=Projectdb;Integrated Security=True;Encrypt=False");

        private void btnDCA_Click(object sender, EventArgs e)
        {
            if (txtUN.Text == "")
            {
                MessageBox.Show("Please select any User Name which you would like to delete");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from LoginTbl where UserName=@UN", con);
                    cmd.Parameters.AddWithValue("@UN", txtUN.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted Successfully");
                    con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSCA_Click(object sender, EventArgs e)
        {
            if (txtUN.Text == "" || txtUP.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into LoginTbl (UserName,UserPasward) values (@UN,@UP)", con);
                    cmd.Parameters.AddWithValue("@UN", txtUN.Text);
                    cmd.Parameters.AddWithValue("@UP", txtUP.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("New account created Successfully");
                    con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void btnUCA_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
