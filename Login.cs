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
using System.Runtime.InteropServices;

namespace AutoPartsProject
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=MUQEETRANA3802\SQLEXPRESS;Initial Catalog=Projectdb;Integrated Security=True;Encrypt=False");

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnR_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtUN.Clear();
            txtUP.Clear();
        }

        private void btnL_Click(object sender, EventArgs e)
        {
            
            if (txtUN.Text == "" || txtUP.Text == "")
            {
                MessageBox.Show("Missing login data");
            }

            else
            {


                String userName, userPasward;
                userName = txtUN.Text;
                userPasward = txtUP.Text;
                try {

                    String Query = "Select * from loginTbl where UserName= '" + txtUN.Text + "' AND UserPasward= '" + txtUP.Text + "'";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        userName = txtUN.Text;
                        userPasward = txtUP.Text;

                        MainScreen mainScreen = new MainScreen();
                        mainScreen.Show();
                        this.Hide();
                    }
                }
                catch
                {
                    MessageBox.Show("invalid login details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUN.Clear();
                    txtUP.Clear();

                    txtUN.Focus();
                }
                finally
                {
                    con.Close();
                }
            }
            


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateAccount createAccount = new CreateAccount();
            createAccount.Show();
            this.Hide();
        }
    }
}
