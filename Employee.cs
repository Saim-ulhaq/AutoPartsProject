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
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
            DisplayEmployee();
        }
        SqlConnection con = new SqlConnection("Data Source=MUQEETRANA3802\\SQLEXPRESS;Initial Catalog=Projectdb;Integrated Security=True;Encrypt=False");
    
                    //Data showing in GridView//
        
        private void DisplayEmployee()
        {
            con.Open();
            string Query = "Select * from EmployeeTbl";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Clear()
        {
            txtEID.Text = "";
            txtEName.Text = "";
            txtEPhone.Text = "";
            txtEPasward.Text = "";
            txtEAge.Text = "";
        }
       

        //Added of Employee data//
        private void btnEAdd_Click(object sender, EventArgs e)
        {
            if (txtEID.Text == "" || txtEName.Text == "" || txtEPhone.Text == "" || txtEPasward.Text == "" || txtEAge.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl (EID,EName,EPhone,EPasward,EAge) values (@EI,@EN,@EPN,@EP,@EA)",con);
                    cmd.Parameters.AddWithValue("@EI",txtEID.Text);
                    cmd.Parameters.AddWithValue("@EN", txtEName.Text);
                    cmd.Parameters.AddWithValue("@EPN", txtEPhone.Text);
                    cmd.Parameters.AddWithValue("@EP", txtEPasward.Text);
                    cmd.Parameters.AddWithValue("@EA", txtEAge.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Added Successfully");
                    con.Close();
                    DisplayEmployee();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

                    //Cancel the page//
        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int Key = 0;
        private void EDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           // txtEID.Text = EDGV.SelectedRows[0].Cells[1] .Value.ToString();
           // txtEName.Text = EDGV.SelectedRows[0].Cells[2].Value.ToString();
            //  txtEPhone.Text = EDGV.SelectedRows[0].Cells[3].Value.ToString();
          //  txtEPasward.Text = EDGV.SelectedRows[0].Cells[4].Value.ToString();
        //    txtEAge.Text = EDGV.SelectedRows[0].Cells[5].Value.ToString();
           // if (txtEID.Text == "")
           // {
            //    Key = 0;
           // }
           // else
           // {
           //     Key = Convert.ToInt32(EDGV.SelectedRows[0].Cells[0].Value.ToString());
           // }
        }
                        
                            //update button//
        private void btnEEdit_Click(object sender, EventArgs e)
        {

            if (txtEID.Text == "" || txtEName.Text == "" || txtEPhone.Text == "" || txtEPasward.Text == "" || txtEAge.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update EmployeeTbl set EName=@EN,EPhone=@EPN,EPasward=@EP,EAge=@EA where EID=@EI", con);
                    cmd.Parameters.AddWithValue("@EI", txtEID.Text);
                    cmd.Parameters.AddWithValue("@EN", txtEName.Text);
                    cmd.Parameters.AddWithValue("@EPN", txtEPhone.Text);
                    cmd.Parameters.AddWithValue("@EP", txtEPasward.Text);
                    cmd.Parameters.AddWithValue("@EA", txtEAge.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Updated Successfully");
                    con.Close();
                    DisplayEmployee();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
            
                            //Delete Button//
        private void btnEDelete_Click(object sender, EventArgs e)
        {
            if (txtEID.Text == "")
            {
                MessageBox.Show("Please select any ID of Employee which you would like to delete");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from EmployeeTbl where EID=@EI", con);
                    cmd.Parameters.AddWithValue("@EI", txtEID.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted Successfully");
                    con.Close();
                    DisplayEmployee();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void btnMSC_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
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

        private void btnMSH_Click(object sender, EventArgs e)
        {
            MainScreen mainScreen = new MainScreen();
            mainScreen.Show();
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

        private void EDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            txtEName.Text = EDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtEPhone.Text = EDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtEPasward.Text = EDGV.SelectedRows[0].Cells[3].Value.ToString();
            txtEAge.Text = EDGV.SelectedRows[0].Cells[4].Value.ToString();

            if (txtEName.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(EDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void gunaPictureBox1_Click(object sender, EventArgs e)
        {
            con.Open();
            string Query = "Select * from EmployeeTbl where EName=@EN";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@EN", txtSE.Text);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddStock addStock = new AddStock();
            addStock.Show();
            this.Hide();
        }

        private void txtSE_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string querry = "select EID, EName,EPhone,EPasward, EAge from EmployeeTbl " +
                                "where EName LIKE '%" + txtSE.Text + "%'";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                EDGV.DataSource = ds.Tables[0];
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
