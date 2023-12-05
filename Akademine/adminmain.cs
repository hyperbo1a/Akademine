using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Akademine
{
    public partial class adminmain : Form
    {
        public adminmain()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void studentai_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_studentas admin_Studentas = new admin_studentas();
            admin_Studentas.Show();
        }

        private void destytojai_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_destytojas admin_dest = new admin_destytojas();
            admin_dest.Show();
        }

        private void studentuGrupes_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_grupes admin_grupes = new admin_grupes();
            admin_grupes.Show();
        }

        private void destomiDalykai_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_dalykai admin_Dalykai = new admin_dalykai();
            admin_Dalykai.Show();
        }

       
    }
}
