using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Akademine
{
    public partial class admin_studentas : Form
    {
        public admin_studentas()
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

        private void Uzregistruoti_Click(object sender, EventArgs e)
        {
            string studLog = sukurtStudLogin.Text;
            string studPass = sukurtStudPass.Text;

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand comand2 = new MySqlCommand("SELECT * FROM `studentas` WHERE `login` = @sL AND `pass` = @sP", db.getConnection());
            comand2.Parameters.Add("@sL", MySqlDbType.VarChar).Value = studLog;
            comand2.Parameters.Add("@sP", MySqlDbType.VarChar).Value = studPass;

            adapter.SelectCommand = comand2;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
                MessageBox.Show("toks jau yra");
            else
            {
                MySqlCommand comand = new MySqlCommand("INSERT INTO `studentas` (`stud_id`, `login`, `pass`) VALUES (NULL, @sL, @sP);", db.getConnection());
                comand.Parameters.Add("@sL", MySqlDbType.VarChar).Value = studLog;
                comand.Parameters.Add("@sP", MySqlDbType.VarChar).Value = studPass;
                db.openConnection();
                adapter.SelectCommand = comand;
                comand.ExecuteNonQuery();
                db.closeConnection();
                MessageBox.Show("Uzregistruotas!");
            }
        }

        private void Pasalinti_Click(object sender, EventArgs e)
        {
            string studLogP = pasalintStudLog.Text;
            string studPassP = PasalintStudPass.Text;
            

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand comand2 = new MySqlCommand("SELECT * FROM `studentas` WHERE `login` = @sLP AND `pass` = @sPP", db.getConnection());
            comand2.Parameters.Add("@sLP", MySqlDbType.VarChar).Value = studLogP;
            comand2.Parameters.Add("@sPP", MySqlDbType.VarChar).Value = studPassP;

            adapter.SelectCommand = comand2;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MySqlCommand comand = new MySqlCommand("DELETE FROM `studentas` WHERE `studentas`.`login`=@sLP AND `pass` = @sPP", db.getConnection());
                comand.Parameters.Add("@sLP", MySqlDbType.VarChar).Value = studLogP;
                comand.Parameters.Add("@sPP", MySqlDbType.VarChar).Value = studPassP;
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

        private void Priskirti_Click(object sender, EventArgs e)
        {
            string studLogP = priskirtStudLog.Text; 
            string studPassP = priskirtStudPass.Text; 
            string grupes_id = grupe.Text; 

            DB db = new DB();

            MySqlCommand comand2 = new MySqlCommand("SELECT `stud_id` FROM `studentas` WHERE `login` = @sLP AND `pass` = @sPP", db.getConnection());
            comand2.Parameters.Add("@sLP", MySqlDbType.VarChar).Value = studLogP;
            comand2.Parameters.Add("@sPP", MySqlDbType.VarChar).Value = studPassP;

            try
            {
                db.openConnection();
                object result = comand2.ExecuteScalar();
                db.closeConnection();

                if (result != null)
                {
                    string stud_id = result.ToString();

                    MySqlCommand comand = new MySqlCommand("INSERT INTO `grupes_studentai` (`grupes_studentai_id`, `stud_id`, `pavadinimas`) VALUES (NULL, @sID1, @g);", db.getConnection());
                    comand.Parameters.Add("@sID1", MySqlDbType.VarChar).Value = stud_id;
                    comand.Parameters.Add("@g", MySqlDbType.VarChar).Value = grupes_id;

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




