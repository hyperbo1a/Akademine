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
using System.Windows.Input;

namespace Akademine
{
    public partial class admin_grupes : Form
    {
        public admin_grupes()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
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

        private void sukurtiG_Click(object sender, EventArgs e)
        {
            string grupe = sukurtGrupeLogin.Text;
            

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand comand2 = new MySqlCommand("SELECT * FROM `grupes` WHERE `pavadinimas` = @g", db.getConnection());
            comand2.Parameters.Add("@g", MySqlDbType.VarChar).Value = grupe;

            adapter.SelectCommand = comand2;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
                MessageBox.Show("tokia grupe jau yra");
            else
            {
                MySqlCommand comand = new MySqlCommand("INSERT INTO `grupes` (`pavadinimas`) VALUES (@g);", db.getConnection());
                comand.Parameters.Add("@g", MySqlDbType.VarChar).Value = grupe;
                db.openConnection();
                adapter.SelectCommand = comand;
                comand.ExecuteNonQuery();
                db.closeConnection();
                MessageBox.Show("Uzregistruotas!");
            }
        }

        private void PasalintiG_Click(object sender, EventArgs e)
        {
            string grupe = sukurtGrupeLogin.Text;

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand comand2 = new MySqlCommand("SELECT * FROM `grupes` WHERE `pavadinimas` = @g", db.getConnection());
            comand2.Parameters.Add("@g", MySqlDbType.VarChar).Value = grupe;

            adapter.SelectCommand = comand2;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MySqlCommand comand = new MySqlCommand("DELETE FROM `grupes` WHERE `pavadinimas` = @gg", db.getConnection());
                comand.Parameters.Add("@gg", MySqlDbType.VarChar).Value = grupe;

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
                MessageBox.Show("Tokios grupės nėra!");
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
