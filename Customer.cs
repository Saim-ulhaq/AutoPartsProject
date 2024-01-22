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
using Guna.UI.WinForms;

namespace AutoPartsProject
{
    public partial class Customer : Form
    {
       
        int Key = 0;
        
        public Customer()
        {
            InitializeComponent();
            DisplayCustomer();
            Clear();
        }
        SqlConnection con = new SqlConnection("Data Source=MUQEETRANA3802\\SQLEXPRESS;Initial Catalog=Projectdb;Integrated Security=True;Encrypt=False");

                    //show table in gridview//

        private void DisplayCustomer()
        {
            con.Open();
            string Query = "Select * from CustomerTbl";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Clear()
        {
            
            txtCName.Text = "";
            txtCAddress.Text = "";
            txtCPhone.Text = "";
        }

                //Add customer data//
        private void btnCSave_Click(object sender, EventArgs e)
        {
            if (txtCName.Text == "" || txtCAddress.Text == "" || txtCPhone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl (CusName,CusAddress,CusPhone) values (@CN,@CA,@CP)", con);
                   
                    cmd.Parameters.AddWithValue("@CN", txtCName.Text);
                    cmd.Parameters.AddWithValue("@CA", txtCAddress.Text);
                    cmd.Parameters.AddWithValue("@CP", txtCPhone.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Saved Successfully");
                    con.Close();
                    DisplayCustomer();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void CDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

                    //Delete customer data//

        private void btnCDelete_Click(object sender, EventArgs e)
        {
            if (txtCID.Text == "")
            {
                MessageBox.Show("Please select any ID of Customer which you would like to delete");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from CustomerTbl where CusID=@CI", con);
                    cmd.Parameters.AddWithValue("@CI", txtCID.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted Successfully");
                    con.Close();
                    DisplayCustomer();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

                //update customer data//

        private void btnCEdit_Click(object sender, EventArgs e)
        {

            if (txtCID.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update CustomerTbl set CusName=@CN,CusAddress=@CA,CusPhone=@CP where CusID=@CI", con);
                    cmd.Parameters.AddWithValue("@CI", txtCID.Text);
                    cmd.Parameters.AddWithValue("@CN", txtCName.Text);
                    cmd.Parameters.AddWithValue("@CA", txtCAddress.Text);
                    cmd.Parameters.AddWithValue("@CP", txtCPhone.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated Successfully");
                    con.Close();
                    DisplayCustomer();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void btnMSH_Click(object sender, EventArgs e)
        {
            MainScreen mainScreen = new MainScreen();
            mainScreen.Show();
            this.Hide();
        }

        private void btnMSP_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Show();
            this.Hide();
        }

        private void btnMSE_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.Show();
            this.Hide();
        }

        private void btnMSB_Click(object sender, EventArgs e)
        {
           Bills bills = new Bills();
            bills.Show();
            this.Hide();
        }

        private void btnMSL_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void btnCV_Click(object sender, EventArgs e)
        {
            con.Open();
            string Query = "Select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void CDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            txtCName.Text = CDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtCAddress.Text = CDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtCPhone.Text = CDGV.SelectedRows[0].Cells[3].Value.ToString();
           
            if (txtCName.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void gunaPictureBox1_Click(object sender, EventArgs e)
        {
            con.Open();
            string Query = "Select * from CustomerTbl where CusName=@CN";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@CN", txtSC.Text);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void btnMSC_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddStock addStock = new AddStock();
            addStock.Show();
            this.Hide();
        }

        private void txtSC_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string querry = "select CusID, CusName,CusAddress,CusPhone from CustomerTbl " +
                                "where CusName LIKE '%" + txtSC.Text + "%'";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                CDGV.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
