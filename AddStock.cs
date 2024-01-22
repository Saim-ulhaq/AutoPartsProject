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
    public partial class AddStock : Form
    {
        public AddStock()
        {
            InitializeComponent();
            DisplayStockProduct();

        }


        SqlConnection con = new SqlConnection(@"Data Source=MUQEETRANA3802\SQLEXPRESS;Initial Catalog=Projectdb;Integrated Security=True;Encrypt=False");

        //display product table in dgv//
      private void  DisplayStockProduct()
        {
            con.Open();
            string Query = "Select * from ProductTbl";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ASDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void AddStock_Load(object sender, EventArgs e)
        {
            
        }

        //Add quantity of item in stock button code//
        private void btnASP_Click(object sender, EventArgs e)
        {
            try
            {
                int newQty = Convert.ToInt32(txtSLQ.Text) + Convert.ToInt32(txtSAQ.Text);
                con.Open();
                SqlCommand cmd = new SqlCommand("Update ProductTbl set PQuantity=@PQ where PID=@PI", con);
                cmd.Parameters.AddWithValue("@PQ", newQty);
                cmd.Parameters.AddWithValue("@PI", Key);
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayStockProduct();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //autofill textboxes //

            int Key = 0;
        private void ASDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSPID.Text = ASDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtSPName.Text = ASDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtSLQ.Text = ASDGV.SelectedRows[0].Cells[3].Value.ToString();
          

            if (txtSPName.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ASDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }
        private void Reset()
        {
            txtSPID.Clear();
            txtSPName.Clear();
            txtSLQ.Clear();
            txtSAQ.Clear();
            txtSSI.Clear();

            Key = 0;
          
        }

        //search specific product//
        private void gunaPictureBox1_Click(object sender, EventArgs e)
        {
            con.Open();
            string Query = "Select * from ProductTbl where PName=@PN";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@PN", txtSSI.Text);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ASDGV.DataSource = ds.Tables[0];
            con.Close();
            Reset();

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

        private void txtSSI_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string querry = "select PID, PName,PCategory,PQuantity, PPrice from ProductTbl " +
                                "where PName LIKE '%" + txtSSI.Text + "%'";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                ASDGV.DataSource = ds.Tables[0];
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
