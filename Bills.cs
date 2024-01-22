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
using System.Security.Cryptography;
using Guna.UI.WinForms;
using System.Reflection.Emit;
using System.Diagnostics;

namespace AutoPartsProject
{
    public partial class Bills : Form
    {

        public Bills()
        {
            InitializeComponent();
            DisplayProduct();
            DisplayTransaction();
            UpdateStock();
           
        }

        SqlConnection con = new SqlConnection(@"Data Source=MUQEETRANA3802\SQLEXPRESS;Initial Catalog=Projectdb;Integrated Security=True;Encrypt=False");


        //Display Product table in DGV//
        private void DisplayProduct()
        {
            con.Open();
            string Query = "Select * from ProductTbl";
            SqlCommand cmd = new SqlCommand(Query,con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BPDGV.DataSource = ds.Tables[0];
            con.Close();
        }


        //Display Bill table in dgv//
        private void DisplayTransaction()
        {
            con.Open();
            string Query = "Select * from BillTbl";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        //upadte your stock when add in bill some quantity//
        private void UpdateStock()
        {
            if (int.TryParse(txtBPQ.Text, out int quantity))
            {
            
            try
            {
                int newQty = stock - Convert.ToInt32(txtBPQ.Text);
                con.Open();
                SqlCommand cmd = new SqlCommand("Update ProductTbl set PQuantity=@PQ where PID=@PI", con);
                cmd.Parameters.AddWithValue("@PQ", newQty);
                cmd.Parameters.AddWithValue("@PI", Key);
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
        }


        //Add to items for bill in for print//

        int n = 0, GrdTotal = 0;
        private void btnAI_Click(object sender, EventArgs e)
        {

            if (txtBPQ.Text == "" || Convert.ToInt32(txtBPQ.Text) > stock)
            {
                MessageBox.Show("No enough");
            }
            else if (txtBPQ.Text == "" || Key == 0)
            {
                MessageBox.Show("Missing information");
            }

            else 
            {
                int quantity = Convert.ToInt32(txtBPQ.Text);
                int pricePerUnit = Convert.ToInt32(txtBPP.Text);
                int totaling = quantity * pricePerUnit;
                int decss = pricePerUnit / 100;
                int decD = decss * Convert.ToInt32(txtD.Text);
                int decQ = decD * quantity;
                int total = totaling - decQ;

                // Increment the currentMaxId before using it

                DataGridViewRow newRow = new DataGridViewRow();

                newRow.CreateCells(BDGV);
                newRow.Cells[0].Value = n + 1; // Set the ID to the currentMaxId
                newRow.Cells[1].Value = txtBPN.Text;
                newRow.Cells[2].Value = txtBPP.Text;
                newRow.Cells[3].Value = txtBPC.Text;
                newRow.Cells[4].Value = txtBPQ.Text;
                newRow.Cells[5].Value = total;
                newRow.Cells[6].Value = txtD.Text;

                GrdTotal += total;
                BDGV.Rows.Add(newRow);
                n++;

                totals.Text = GrdTotal.ToString();
                UpdateStock();
                Reset();

            }

            
        }

        //Click on any cell and atumatically data inserted in textboxes//
        private void BPDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBPN.Text = BPDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtBPC.Text = BPDGV.SelectedRows[0].Cells[2].Value.ToString();
            stock = Convert.ToInt32(BPDGV.SelectedRows[0].Cells[3].Value.ToString());
            txtBPP.Text = BPDGV.SelectedRows[0].Cells[4].Value.ToString();

            if (txtBPN.Text=="")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(BPDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
            
        }

        //reset or clear textboxes//

        int Key = 0, stock = 0;
        private void Reset()
        {
            txtBPN.Clear();
            txtBPC.Clear();
            txtBPP.Clear();
            txtBPQ.Clear();
            txtBPS.Clear();
            txtD.Clear();

            Key = 0;
            stock = 0;
        }
        private void Resets()
        {
            txtBCN.Clear();
            txtBPN.Clear();
            txtBPC.Clear();
            txtBPP.Clear();
            txtBPQ.Clear();
            txtBPS.Clear();
            txtD.Clear();

            Key = 0;
            stock = 0;
        }
        private void btnR_Click(object sender, EventArgs e)
        {
            Resets();
        }

        private void Bills_Load(object sender, EventArgs e)
        {
            
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

        //print button code//

        private void btnBP_Click(object sender, EventArgs e)
        {

            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm",285,600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }


        //add button code for insert data in bill table//
       
        string PName,PCategory, PDiscount;
        int Pid, PPrice, PQuantity, PTotal, pos = 60;

        private void btnMSC_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
            this.Hide();
        }

        private void txtBPS_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string querry = "select PID, PName, PCategory, PQuantity, PPrice from ProductTbl where PName LIKE '%" + txtBPS.Text + "%'";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                BPDGV.DataSource = ds.Tables[0];
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

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void btnAB_Click(object sender, EventArgs e)
        {
            if (txtBCN.Text == "")
            {
                MessageBox.Show("Missing Customer Name");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl (CusName,BDate,Amount) values (@CN,@DA,@Amt)", con);

                    cmd.Parameters.AddWithValue("@CN", txtBCN.Text);
                    cmd.Parameters.AddWithValue("@DA", DateTime.Today.Date);
                    cmd.Parameters.AddWithValue("@Amt", GrdTotal);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Saved Successfully");
                    con.Close();
                    DisplayTransaction();
                    txtBCN.Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

            private void button1_Click(object sender, EventArgs e)
        {
            AddStock addStock = new AddStock();
            addStock.Show();
            this.Hide();
        }

        //print document page styles and display//

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawString("Mann Auto Parts", new Font("Time new Romens", 12, FontStyle.Bold), Brushes.Red, new Point(80));

            e.Graphics.DrawString("ID Product  Price  Quantity Total  Discount", new Font("Time new Romens", 10, FontStyle.Bold), Brushes.Red, new Point(26,40));
               

            foreach (DataGridViewRow row in BDGV.Rows)
            {
                Pid = Convert.ToInt32(row.Cells["ID"].Value);
                PName = "" + row.Cells["Product"].Value;
                PPrice = Convert.ToInt32(row.Cells["Price"].Value);
                PCategory ="" +row.Cells["Category"].Value;
                PQuantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                PTotal = Convert.ToInt32(row.Cells["Total"].Value);
                PDiscount = "" + row.Cells["Discount"].Value;

                e.Graphics.DrawString("" + Pid, new Font("Time new Romens", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + PName, new Font("Time new Romens", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + PPrice, new Font("Time new Romens", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + PQuantity, new Font("Time new Romens", 8, FontStyle.Bold), Brushes.Blue, new Point(170,pos));
                e.Graphics.DrawString("" + PTotal, new Font("Time new Romens", 8, FontStyle.Bold), Brushes.Blue, new Point(235,pos));
                e.Graphics.DrawString("" + PDiscount, new Font("Time new Romens", 8, FontStyle.Bold), Brushes.Blue, new Point(270, pos));

            }

            e.Graphics.DrawString("Grand Total: Rs" + GrdTotal, new Font("Time new Romens", 10, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + 50));
            e.Graphics.DrawString("*****Okanwala Road Chichawatni*****", new Font("Time new Romens", 10, FontStyle.Bold), Brushes.Crimson, new Point(10, pos + 85));

            BDGV.Rows.Clear();
            BDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
            n = 0;

        }

        //search product button code//
        private void gunaImageButton1_Click(object sender, EventArgs e)
        {
            con.Open();
            string Query = "Select * from ProductTbl where PName=@PN";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@PN", txtBPS.Text);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BPDGV.DataSource = ds.Tables[0];
            con.Close();
            Reset();
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

    }
}
