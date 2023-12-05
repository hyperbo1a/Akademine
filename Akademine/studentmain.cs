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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Akademine
{
    public partial class studentmain : Form
    {
        private string loginUser;
        private string passUser;
        private string stud_id;

        public studentmain(string user, string pass)
        {
            InitializeComponent();
            loginUser = user;
            passUser = pass;
        }

        private void studentmain_Load(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlConnection connection = db.getConnection();

            try
            {
                connection.Open();

                MySqlCommand comand2 = new MySqlCommand("SELECT `stud_id` FROM `studentas` WHERE `login` = @sL AND `pass` = @sP", db.getConnection());
                comand2.Parameters.Add("@sL", MySqlDbType.VarChar).Value = loginUser;
                comand2.Parameters.Add("@sP", MySqlDbType.VarChar).Value = passUser;

                db.openConnection();
                object result = comand2.ExecuteScalar();
                
                string stud_id = result.ToString();
                this.stud_id = stud_id;

                MySqlCommand comand = new MySqlCommand("SELECT pavadinimas FROM `grupes_studentai` WHERE stud_id = @sID", db.getConnection());
                comand.Parameters.Add("@sID", MySqlDbType.VarChar).Value = stud_id;
                object result1 = comand.ExecuteScalar();
                string pavadinimas_g = result1.ToString();
                db.closeConnection();

                string query = "SELECT dalyko_pav FROM grupes_dalykai WHERE `pavadinimas` = @pav";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@pav", pavadinimas_g);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dalykas = comboBox1.SelectedItem.ToString();
            string stud_id = this.stud_id;

            DB db = new DB();
            MySqlConnection connection = db.getConnection();

            string query = "SELECT pazymis FROM `pazymiai` WHERE `dalyko_pav` = @pav AND `stud_id` = @sID";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@pav", dalykas);
            cmd.Parameters.AddWithValue("@sID", stud_id);
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(cmd);
            System.Data.DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);

            dataGridView1.DataSource = dataTable2;
        }
    }
}

