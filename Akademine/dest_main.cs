using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
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
    public partial class dest_main : Form
    {
        private string loginUser;
        private string passUser;
        private string dest_id;

        public dest_main(string user, string pass)
        {
            InitializeComponent();
            loginUser = user;
            passUser = pass;
        }
       

        private void dest_main_Load(object sender, EventArgs e)
        {
            DB db = new DB(); 
            MySqlConnection connection = db.getConnection();

            try
            {
                connection.Open();

                MySqlCommand comand2 = new MySqlCommand("SELECT `dest_id` FROM `destytojas` WHERE `login` = @dL AND `pass` = @dP", db.getConnection());
                comand2.Parameters.Add("@dL", MySqlDbType.VarChar).Value = loginUser;
                comand2.Parameters.Add("@dP", MySqlDbType.VarChar).Value = passUser;

               
                db.openConnection();
                object result = comand2.ExecuteScalar();
                db.closeConnection();
                string dest_id = result.ToString();
                this.dest_id = dest_id;

                
                string query = "SELECT dalyko_pav FROM dest_dalykai WHERE `dest_id` = @dest_id"; 
                
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@dest_id", dest_id);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                System.Data.DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                
                dataGridView1.DataSource = dataTable;
                foreach (DataRow row in dataTable.Rows)
                {
                    comboBox1.Items.Add(row["dalyko_pav"].ToString());
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Klaida: " + ex.Message);
            }
            finally
            {
                
                connection.Close();
            }
        }



        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string dalykas = comboBox1.SelectedItem.ToString();

            DB db = new DB();
            MySqlConnection connection = db.getConnection();

            string query = "SELECT * FROM `grupes_dalykai` WHERE `dalyko_pav` = @dp";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@dp", dalykas);

            MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd);
            System.Data.DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);

            dataGridView1.DataSource = dataTable1;

            // Išvalyti comboBox2 turinį
            comboBox2.Items.Clear();

            foreach (DataRow row in dataTable1.Rows)
            {
                comboBox2.Items.Add(row["pavadinimas"].ToString());
            }
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            string grupe = comboBox2.SelectedItem.ToString();
            DB db = new DB();
            MySqlConnection connection = db.getConnection();

            string query = "SELECT grupes_studentai.pavadinimas, studentas.stud_id, studentas.login, studentas.pass FROM `grupes_studentai` JOIN studentas ON grupes_studentai.stud_id = studentas.stud_id  WHERE `pavadinimas` = @dg";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@dg", grupe);

            MySqlDataAdapter adapter2 = new MySqlDataAdapter(cmd);
            System.Data.DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);

            dataGridView1.DataSource = dataTable2;

            foreach (DataRow row in dataTable2.Rows)
            {
                comboBox3.Items.Add(row["stud_id"].ToString());
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable dataTable3 = new DataTable();
            
            string s_id = comboBox3.SelectedItem.ToString();
            DB db = new DB();
            MySqlConnection connection = db.getConnection();
            string dalykas = comboBox1.SelectedItem.ToString();
            

            string query = "SELECT pazymiai.pazymis, pazymiai.pazymio_id, pazymiai.dalyko_pav FROM `pazymiai` JOIN studentas ON pazymiai.stud_id = studentas.stud_id WHERE `dalyko_pav` = @dp AND `pazymiai`.`stud_id` = @sID";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@dp", dalykas);
            cmd.Parameters.AddWithValue("@sID", s_id);

            MySqlDataAdapter adapter3 = new MySqlDataAdapter(cmd);
            
            adapter3.Fill(dataTable3);
            dataGridView1.DataSource = dataTable3;


            // Prieš pridedant naujus duomenis, įsitikinkime, ar yra duomenų
            if (dataGridView1.DataSource != null)
            {
                
                DataTable existingDataTable = (DataTable)dataGridView1.DataSource;
                existingDataTable.Merge(dataTable3); // Pridėti naujus duomenis į esamą DataTable
                 
            }
            else
            {
                dataGridView1.DataSource = dataTable3; // Jei nėra duomenų, priskirti naują duomenų šaltinį
            }

            foreach (DataRow row in dataTable3.Rows)
            {
                comboBox4.Items.Add(row["pazymio_id"].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pazString = pazymisBut.Text; // Gaukite tekstą iš pazymisBut elemento
            if (int.TryParse(pazString, out int paz)) // Bandykite konvertuoti į int
            {
                string dal_pav = comboBox1.SelectedItem.ToString();
                string s_id = comboBox3.SelectedItem.ToString();

                DB db = new DB();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand comand = new MySqlCommand("INSERT INTO `pazymiai` (`pazymio_id`, `dalyko_pav`, `stud_id`, `pazymis`) VALUES (NULL, @dP, @sID, @p);", db.getConnection());
                comand.Parameters.Add("@dP", MySqlDbType.VarChar).Value = dal_pav;
                comand.Parameters.Add("@sID", MySqlDbType.VarChar).Value = s_id;
                comand.Parameters.Add("@p", MySqlDbType.Int64).Value = paz;

                db.openConnection();
                adapter.SelectCommand = comand;
                comand.ExecuteNonQuery();
                db.closeConnection();
                MessageBox.Show("pazymis irasytas");
            }
            else
            {
                MessageBox.Show("Įveskite skaitinę reikšmę į pažymio lauką.");
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string paz_id = comboBox4.SelectedItem.ToString();
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM `pazymiai` WHERE `pazymio_id` = @pID", db.getConnection());
            command.Parameters.Add("@pID", MySqlDbType.VarChar).Value = paz_id;

            db.openConnection();
            int count = Convert.ToInt32(command.ExecuteScalar());
            db.closeConnection();

            if (count > 0)
            {
                
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand deleteCommand = new MySqlCommand("DELETE FROM `pazymiai` WHERE `pazymiai`.`pazymio_id`=@pID", db.getConnection());
                deleteCommand.Parameters.Add("@pID", MySqlDbType.VarChar).Value = paz_id;

                db.openConnection();
                adapter.SelectCommand = deleteCommand;
                deleteCommand.ExecuteNonQuery();
                db.closeConnection();

                MessageBox.Show("Pazymis pasalintas");
                comboBox4.Items.Clear();
            }
            else
            {
                MessageBox.Show("Tokio pazymio ID nera");
            }
        }

    }
}

