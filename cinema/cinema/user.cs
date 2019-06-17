using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;

namespace cinema
{
    public partial class user : Form
    {

        static string serverName = "localhost", userName = "root", dbName = "arm_rab_kinoteatra", port = "330", password = "12345678";
        static string connStr = "server=" + serverName + ";user=" + userName + ";database=" + dbName + ";port=" + port + ";password=" + password + ";";

        private MySqlConnection con;
        private MySqlCommand cmd;
        private MySqlDataAdapter da;
        private DataTable dt;

        public static List<registrationclass> rg = new List<registrationclass>();
        XmlSerializer formatter = new XmlSerializer(typeof(List<registrationclass>));

        public void PRIBET()
        {
            con.Open();
            cmd = new MySqlCommand("SELECT id_associate, user, password FROM associate WHERE user = '" + textBox1.Text + "' AND password='" + textBox2.Text + "' ");

            //cmd = new MySqlCommand("SELECT fname , long, start, finish, cost, vozrast,  janr,  proizvodstva , produser.name,format,description  FROM film,vozrast,janr,strana,produser,format where film.id_vozrastogr=vozrast.id_vozrast and  film.id_janr=janr.id_janr and film.id_strana=strana.id_proizvodstva and film.id_produser=produser.id_produser and film.id_format=format.id_format ");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            
            da.Fill(dt);
            da.Update(dt);
            DataRow[] myData= dt.Select();

            if (dt.Rows.Count == 1)
                Form1.user_id = Convert.ToInt32(myData[0].ItemArray[0]);
            con.Close();

        }

        public user()
        {
            InitializeComponent();

            con = new MySqlConnection(connStr);
            //PRIBET();

            if (Form1.a == 0)
            {
                menuStrip1.Visible = false;
            }

            if (Form1.a == 1)
            {
                menuStrip1.Visible = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text;
        }

        private void user_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PRIBET();

            if (dt.Rows.Count == 1)
            {
                MessageBox.Show("Авторизация прошла успешно");

                Form1.a = 1;
                menuStrip1.Visible = true;
                this.Close();
            }
            else
                MessageBox.Show("Авторизация не прошла");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.a = 0;
            button3.Visible = false;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Login = textBox1.Text;
            string Password = textBox2.Text;
            rg.Clear();
            rg.Add(new registrationclass(Login, Password));


            using (FileStream fs = new FileStream("pas", FileMode.Create))
            {
                formatter.Serialize(fs, rg);
                fs.Close();
            }
        }

        private void изменитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Введите новые параметры");
            foreach (registrationclass rgn in rg)
            {
                textBox1.Text = rgn.Login;
                textBox2.Text = rgn.Password;
                button3.Visible = true;
            }
        }
    }
}
