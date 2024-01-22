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
    public partial class EDLogOut : Form
    {
        public EDLogOut()
        {
            InitializeComponent();
            DisplayEDLogOut();
        }

        SqlConnection con = new SqlConnection(@"Data Source=MUQEETRANA3802\SQLEXPRESS;Initial Catalog=Projectdb;Integrated Security=True;Encrypt=False");

        private void DisplayEDLogOut()
        {
            con.Open();
            string Query = "Select * from LoginTbl";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            LEDGV.DataSource = ds.Tables[0];
            
        }

        private void btnEDLS_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
