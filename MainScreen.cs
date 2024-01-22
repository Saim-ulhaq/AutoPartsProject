using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPartsProject
{
    public partial class MainScreen : Form
    {
        int key = 0;
        public MainScreen()
        {
            InitializeComponent();
            DisplayMainScreenStock();
        }
        SqlConnection con = new SqlConnection(@"Data Source=MUQEETRANA3802\SQLEXPRESS;Initial Catalog=Projectdb;Integrated Security=True;Encrypt=False");
        
        private void DisplayMainScreenStock()
        {
            con.Open();
            string Query = "Select * from ProductTbl";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SDGV.DataSource = ds.Tables[0];
            con.Close();

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

        private void btnMSP_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Show();
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

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gunaPictureBox1_Click(object sender, EventArgs e)
        {
            con.Open();
            string Query = "Select * from ProductTbl where PName=@PN";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@PN", txtMSSP.Text);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SDGV.DataSource = ds.Tables[0];
            con.Close();
           
        }

        private void SDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMSSP.Text = SDGV.SelectedRows[0].Cells[1].Value.ToString();
            
            if (txtMSSP.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(SDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddStock addStock = new AddStock();
            addStock.Show();
            this.Hide();
        }

        private void txtMSSP_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string querry = "select PID, PName,PCategory,PQuantity, PPrice from ProductTbl " +
                                "where PName LIKE '%" + txtMSSP.Text + "%'";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                SDGV.DataSource = ds.Tables[0];
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
