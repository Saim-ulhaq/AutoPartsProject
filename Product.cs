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
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
            DisplayProduct();
            Clear();
        }

        SqlConnection con = new SqlConnection("Data Source=MUQEETRANA3802\\SQLEXPRESS;Initial Catalog=Projectdb;Integrated Security=True;Encrypt=False");

        //Products data show in gridview//
        private void DisplayProduct()
        {
            con.Open();
            string Query = "Select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Clear()
        {
            txtPID.Text = "";
            txtPN.Text = "";
            txtPC.Text = "";
            txtPQ.Text = "";
            txtPP.Text = "";
            txtSP.Text = "";
        }

        private void btnPAdd_Click(object sender, EventArgs e)
        {
            if (txtPID.Text == "" || txtPN.Text == "" || txtPC.Text == "" || txtPQ.Text == "" || txtPP.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ProductTbl (PID,PName,PCategory,PQuantity,PPrice) values (@PI,@PN,@PC,@PQ,@PP)", con);
                    cmd.Parameters.AddWithValue("@PI", txtPID.Text);
                    cmd.Parameters.AddWithValue("@PN", txtPN.Text);
                    cmd.Parameters.AddWithValue("@PC", txtPC.Text);
                    cmd.Parameters.AddWithValue("@PQ", txtPQ.Text);
                    cmd.Parameters.AddWithValue("@PP", txtPP.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product added Successfully");
                    con.Close();
                    DisplayProduct();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void btnSP_Click(object sender, EventArgs e)
        {
           
        }
        int Key = 0;
        private void PDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnPDelete_Click(object sender, EventArgs e)
        {
            if (txtPID.Text == "")
            {
                MessageBox.Show("Please select any ID of Employee which you would like to delete");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from ProductTbl where PID=@PI", con);
                    cmd.Parameters.AddWithValue("@PI", txtPID.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted Successfully");
                    con.Close();
                    DisplayProduct();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        //Update data of product//

        private void btnPEdit_Click(object sender, EventArgs e)
        {

            if (txtPID.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update ProductTbl set PName=@PN,PCategory=@PC,PQuantity=@PQ,PPrice=@PP where PID=@PI", con);
                    cmd.Parameters.AddWithValue("@PI", txtPID.Text);
                    cmd.Parameters.AddWithValue("@PN", txtPN.Text);
                    cmd.Parameters.AddWithValue("@PC", txtPC.Text);
                    cmd.Parameters.AddWithValue("@PQ", txtPQ.Text);
                    cmd.Parameters.AddWithValue("@PP", txtPP.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Updated Successfully");
                    con.Close();
                    DisplayProduct();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMSH_Click(object sender, EventArgs e)
        {
            MainScreen mainScreen = new MainScreen();
            mainScreen.Show();
            this.Hide();
        }

        private void btnMSE_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.Show();
            this.Hide();
        }

        private void btnMSC_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
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

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            string Query = "Select * from ProductTbl where PName=@PN";
            SqlCommand cmd = new SqlCommand(Query,con);
            cmd.Parameters.AddWithValue("@PN",txtPN.Text);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PDGV.DataSource = ds.Tables[0];
            con.Close();
            
        }

        private void btnSP_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void PDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtPID.Text = PDGV.SelectedRows[0].Cells[0].Value.ToString();
            txtPN.Text = PDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtPC.Text = PDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtPQ.Text = PDGV.SelectedRows[0].Cells[3].Value.ToString();
            txtPP.Text = PDGV.SelectedRows[0].Cells[4].Value.ToString();


            if (txtPN.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(PDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void gunaPictureBox1_Click(object sender, EventArgs e)
        {
            con.Open();
            string Query = "Select * from ProductTbl where PName=@PN";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@PN", txtSP.Text);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PDGV.DataSource = ds.Tables[0];
            con.Close();
            Clear();
        }

        private void txtSP_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string querry = "select PID, PName,PCategory,PQuantity, PPrice from ProductTbl " +
                                "where PName LIKE '%" + txtSP.Text + "%'";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                PDGV.DataSource = ds.Tables[0];
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

        private void btnMSP_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Show();
            this.Hide();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            AddStock addStock = new AddStock();
            addStock.Show();
            this.Hide();
        }
    }
}
