using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPartsProject
{
    public partial class Progress : Form
    {
        public Progress()
        {
            InitializeComponent();
            timer1.Start();
        }
        int startP = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startP += 2;
            progressBar1.Value= startP;
            Percentages.Text =startP+"%";
            if (progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
                Login login = new Login();
                login.Show();
                this.Hide();
                timer1.Stop();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
