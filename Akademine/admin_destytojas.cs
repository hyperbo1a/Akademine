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
    public partial class admin_destytojas : Form
    {
        public admin_destytojas()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
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

        private void UzregistruotiD_Click(object sender, EventArgs e)
        {
            string destlog = sukurtDestLogin.Text;
            string destpass = sukurtDestPass.Text;

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand comand2 = new MySqlCommand("SELECT * FROM `destytojas` WHERE `login` = @dL AND `pass` = @dP", db.getConnection());
            comand2.Parameters.Add("@dL", MySqlDbType.VarChar).Value = destlog;
            comand2.Parameters.Add("@dP", MySqlDbType.VarChar).Value = destpass;

            adapter.SelectCommand = comand2;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
                MessageBox.Show("toks jau yra");
            else
            {
                MySqlCommand comand = new MySqlCommand("INSERT INTO `destytojas` (`dest_id`, `login`, `pass`) VALUES (NULL, @dL, @dP);", db.getConnection());
                comand.Parameters.Add("@dL", MySqlDbType.VarChar).Value = destlog;
                comand.Parameters.Add("@dP", MySqlDbType.VarChar).Value = destpass;
                db.openConnection();
                adapter.SelectCommand = comand;
                comand.ExecuteNonQuery();
                db.closeConnection();
                MessageBox.Show("Uzregistruotas!");
            }
        }

        private void PasalintiD_Click(object sender, EventArgs e)
        {
            string destLogP = pasalintDestLog.Text;
            string destPassP = PasalintDestPass.Text;


            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand comand2 = new MySqlCommand("SELECT * FROM `destytojas` WHERE `login` = @dLP AND `pass` = @dPP", db.getConnection());
            comand2.Parameters.Add("@dLP", MySqlDbType.VarChar).Value = destLogP;
            comand2.Parameters.Add("@dPP", MySqlDbType.VarChar).Value = destPassP;

            adapter.SelectCommand = comand2;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MySqlCommand comand = new MySqlCommand("DELETE FROM `destytojas` WHERE `destytojas`.`login`=@dLP AND `pass` = @dPP", db.getConnection());
                comand.Parameters.Add("@dLP", MySqlDbType.VarChar).Value = destLogP;
                comand.Parameters.Add("@dPP", MySqlDbType.VarChar).Value = destPassP;
                db.openConnection();
                adapter.SelectCommand = comand;
                comand.ExecuteNonQuery();
                db.closeConnection();
                MessageBox.Show("Pasalintas");
            }
            else
            {
                MessageBox.Show("Tokio nera");
            }
        }

        private void PriskirtiD_Click(object sender, EventArgs e)
        {
            string destLogP = priskirtDestLog.Text;
            string destPassP = priskirtDestPass.Text;
            string dalyko_pav = dalykas.Text;

            DB db = new DB();

            MySqlCommand comand2 = new MySqlCommand("SELECT `dest_id` FROM `destytojas` WHERE `login` = @dLP AND `pass` = @dPP", db.getConnection());
            comand2.Parameters.Add("@dLP", MySqlDbType.VarChar).Value = destLogP;
            comand2.Parameters.Add("@dPP", MySqlDbType.VarChar).Value = destPassP;

            try
            {
                db.openConnection();
                object result = comand2.ExecuteScalar();
                db.closeConnection();

                if (result != null)
                {
                    string dest_id = result.ToString();

                    MySqlCommand comand = new MySqlCommand("INSERT INTO `dest_dalykai` (`dest_dalykai`, `dalyko_pav`, `dest_id`) VALUES (NULL, @d, @dID1);", db.getConnection());
                    comand.Parameters.Add("@d", MySqlDbType.VarChar).Value = dalyko_pav;
                    comand.Parameters.Add("@dID1", MySqlDbType.VarChar).Value = dest_id;
                    

                    db.openConnection();
                    comand.ExecuteNonQuery();
                    db.closeConnection();

                    MessageBox.Show("Pridėtas");
                }
                else
                {
                    MessageBox.Show("Tokio studento nėra arba duomenys neteisingi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Klaida: " + ex.Message);
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
