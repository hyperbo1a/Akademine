using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Akademine
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            this.passField.AutoSize = false;

            this.passField.Size = new Size(this.passField.Size.Width, 64);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        Point lastPoint;
        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            String loginUser = logField.Text;
            String passUser = passField.Text;

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter(); 
            
            

            MySqlCommand comand = new MySqlCommand("SELECT * FROM `admin` WHERE `login` = @aL AND `pass` = @aP", db.getConnection());
            comand.Parameters.Add("@aL", MySqlDbType.VarChar).Value = loginUser;
            comand.Parameters.Add("@aP", MySqlDbType.VarChar).Value = passUser;

            adapter.SelectCommand = comand;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                this.Hide();
                adminmain adminmain = new adminmain();
                adminmain.Show();

            }
            else
            {
                MySqlCommand comand2 = new MySqlCommand("SELECT * FROM `studentas` WHERE `login` = @studL AND `pass` = @studP", db.getConnection());
                comand2.Parameters.Add("@studL", MySqlDbType.VarChar).Value = loginUser;
                comand2.Parameters.Add("@studP", MySqlDbType.VarChar).Value = passUser;

                adapter.SelectCommand = comand2;
                adapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    this.Hide();
                    studentmain studentmain = new studentmain(loginUser, passUser);
                    studentmain.Show();

                }
                else
                {
                    MySqlCommand comand3 = new MySqlCommand("SELECT * FROM `destytojas` WHERE `login` = @destL AND `pass` = @destP", db.getConnection());
                    comand3.Parameters.Add("@destL", MySqlDbType.VarChar).Value = loginUser;
                    comand3.Parameters.Add("@destP", MySqlDbType.VarChar).Value = passUser;

                    adapter.SelectCommand = comand3;
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        this.Hide();
                        dest_main dest_Main = new  dest_main(loginUser, passUser); ;
                        dest_Main.Show();
                    }
                    else
                    {
                        MessageBox.Show("tokio naudotoje nera");
                    }
                }

            }


        }

    }
    
}

