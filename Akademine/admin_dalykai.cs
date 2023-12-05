using MySql.Data.MySqlClient;
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
    public partial class admin_dalykai : Form
    {
        public admin_dalykai()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        Point lastPoint1;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint1.X;
                this.Top += e.Y - lastPoint1.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint1 = new Point(e.X, e.Y);
        }

        private void sukurtiD_Click(object sender, EventArgs e)
        {
            string dalykas = sukurtDalykaLogin.Text;


            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand comand2 = new MySqlCommand("SELECT * FROM `destomi_dalykai` WHERE `dalyko_pav` = @d", db.getConnection());
            comand2.Parameters.Add("@d", MySqlDbType.VarChar).Value = dalykas;

            adapter.SelectCommand = comand2;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("toksai dalykas jau yra");
            }
            else
            {
                MySqlCommand comand = new MySqlCommand("INSERT INTO `destomi_dalykai` (`dalyko_pav`) VALUES (@d);", db.getConnection());
                comand.Parameters.Add("@d", MySqlDbType.VarChar).Value = dalykas;
                db.openConnection();
                adapter.SelectCommand = comand;
                comand.ExecuteNonQuery();
                db.closeConnection();
                MessageBox.Show("sukurta!");
            }
        }

        private void PasalintiD_Click(object sender, EventArgs e)
        {
            string dalykas = sukurtDalykaLogin.Text;

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand comand2 = new MySqlCommand("SELECT * FROM `destomi_dalykai` WHERE `dalyko_pav` = @g", db.getConnection());
            comand2.Parameters.Add("@g", MySqlDbType.String).Value = dalykas;

            adapter.SelectCommand = comand2;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MySqlCommand comand = new MySqlCommand("DELETE FROM `destomi_dalykai` WHERE `dalyko_pav` = @gg", db.getConnection());
                comand.Parameters.Add("@gg", MySqlDbType.String).Value = dalykas;

                try
                {
                    db.openConnection();
                    comand.ExecuteNonQuery();
                    db.closeConnection();

                    MessageBox.Show("Grupė ištrinta sėkmingai!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Klaida: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Tokio dalyko nėra!");
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Hide();
            adminmain adminmain = new adminmain();
            adminmain.Show();
        }
    }
    
}
