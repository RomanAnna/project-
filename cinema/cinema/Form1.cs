using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace cinema
{
  
    public partial class Form1 : Form
    {
        static string serverName = "localhost", userName = "root", dbName = "arm_rab_kinoteatra", port = "330", password = "12345678";
        static string connStr = "server=" + serverName + ";user=" + userName + ";database=" + dbName + ";port=" + port + ";password=" + password + ";";

        public static int a;
        public static int user_id;
        public static string l1;
        private MySqlConnection con;
        private MySqlCommand cmd;
        private MySqlDataAdapter da;
        private DataTable dt;
        BindingSource bis;
        ToolStripLabel dateLabel;
        ToolStripLabel timeLabel;
        ToolStripLabel infoLabel;
        Timer timer;
        int select = 0;
        string akt;
        int kol = 0;
        int index = 0;

        public void janr()
        {

            con.Open();
            cmd = new MySqlCommand("SELECT naimenovanie_genre FROM genre ");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dataGridView4.DataSource = bSource;
            da.Update(dt);
            comboBox1.Items.Clear(); 
            con.Close();
            for (int i = 0; i < dataGridView4.RowCount - 1; i++)
            { comboBox1.Items.Add(dataGridView4.Rows[i].Cells[0].Value); }
   
        }

        public void dt2()
        {
            akt = "";
            textBox3.Text ="";
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                akt += dataGridView2.Rows[i].Cells[0].Value.ToString();
                if (i != dataGridView2.RowCount - 2) { akt += ","; }
                else { akt += "."; }
            }
            textBox3.Text = akt;

        }
        public void indexf()
        {
            string fname = dataGridView1.Rows[select].Cells[0].Value.ToString();
            cmd = new MySqlCommand("SELECT id_film FROM film WHERE  film.naimenovanie_film= '" + fname + "' ");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dataGridView3.DataSource = bSource;
            index = Convert.ToInt16(dataGridView3.Rows[0].Cells[0].Value);
            label6.Text = index.ToString();
            da.Update(dt);
        }
        public void akter()
        {
            con.Open();
            indexf();
            cmd = new MySqlCommand("SELECT DISTINCT actor FROM film where id_film='" + index + "';");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dataGridView2.DataSource = bSource;
            da.Update(dt);
            dt2();
            con.Close();
        }

        public void ConnectDB()
        { 
          
        }

        public void ShowTable()
        {
            con.Open();
            cmd = new MySqlCommand("SELECT naimenovanie_film, DATE_FORMAT(data_premiere, '%d.%m.%Y'), age_estrictions, naimenovanie_genre, actor, lasting, opisanie_film FROM film, genre WHERE film.id_genre = genre.id_genre;");
            //cmd = new MySqlCommand("SELECT fname , long, start, finish, cost, vozrast,  janr,  proizvodstva , produser.name,format,description  FROM film,vozrast,janr,strana,produser,format where film.id_vozrastogr=vozrast.id_vozrast and  film.id_janr=janr.id_janr and film.id_strana=strana.id_proizvodstva and film.id_produser=produser.id_produser and film.id_format=format.id_format ");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dataGridView1.DataSource = bSource;
            da.Update(dt);

          
            con.Close();
            
        }

         public void Search()
         {

         }

       

        public Form1()
        {
            InitializeComponent();
            pictureBox2.Visible = false;
            con = new MySqlConnection(connStr);
            //con = new SqlConnection(@"Data Source=USERPC\SQLEXPRESS;Initial Catalog=dbcin;Integrated Security=True;Pooling=False");
            ShowTable();
            label1.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
            label3.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
            label4.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
            label5.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
            label19.Text = dataGridView1.Rows[0].Cells[5].Value.ToString();
            textBox2.Text = dataGridView1.Rows[0].Cells[6].Value.ToString();
            infoLabel = new ToolStripLabel();
            infoLabel.Text = "Текущие дата и время:";
            dateLabel = new ToolStripLabel();
            timeLabel = new ToolStripLabel();
            statusStrip1.Items.Add(infoLabel);
            statusStrip1.Items.Add(dateLabel);
            statusStrip1.Items.Add(timeLabel);
            timer = new Timer() { Interval = 100 };
            timer.Tick += timer_Tick;
            timer.Start();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1.Columns[0].Width = 260;
                    dataGridView1.Columns[1].Width = 120;
                    dataGridView1.Columns[2].Width = 120;
                    dataGridView1.Columns[3].Width = 120;
                    dataGridView1.Columns[4].Width = 60;
                    /*dataGridView1.Columns[5].Width = 80;
                    dataGridView1.Columns[6].Width = 130;
                    dataGridView1.Columns[7].Width = 120;
                    dataGridView1.Columns[8].Width = 200;
                    dataGridView1.Columns[9].Width = 100;
                    dataGridView1.Columns[10].Width = 180;*/
                 }
            }

        }

        void timer_Tick(object sender, EventArgs e)
        {
            
            dateLabel.Text = DateTime.Now.ToLongDateString();
            timeLabel.Text = DateTime.Now.ToLongTimeString();
           if (kol == 0)
            {
                akter();
                dt2();
                kol++;
            }

           if (addkino.ob == 1)
           {
               ShowTable();
               addkino.ob = 1;
           }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fgdgfdgToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*DialogResult dialog = MessageBox.Show("Вы действительно хотите выйти ?", "Выход", MessageBoxButtons.YesNo);
            if (dialog==DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else if (dialog==DialogResult.No)
            {
                e.Cancel = true;
            }*/
        }

        private void уToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (select != dataGridView1.RowCount - 1) 
            {
                dataGridView1.Rows[select].Selected = false;
                select++;
                if (select != dataGridView1.RowCount - 1)
                {
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = true;
                    dataGridView1.Rows[select].Selected = true;
                    label1.Text = dataGridView1.Rows[select].Cells[0].Value.ToString();
                    label3.Text = dataGridView1.Rows[select].Cells[1].Value.ToString();
                    label4.Text = dataGridView1.Rows[select].Cells[2].Value.ToString();
                    label5.Text = dataGridView1.Rows[select].Cells[3].Value.ToString();
                    label19.Text = dataGridView1.Rows[select].Cells[5].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[select].Cells[6].Value.ToString();
                    akter();
                }
                else
                {
                    pictureBox1.Visible = false;
                    select--;
                    dataGridView1.Rows[select].Selected = true;
                    
                }
                              
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            if (select != 0 )
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                dataGridView1.Rows[select].Selected = false;
                select--;
                dataGridView1.Rows[select].Selected = true;
                label1.Text = dataGridView1.Rows[select].Cells[0].Value.ToString();
                label3.Text = dataGridView1.Rows[select].Cells[1].Value.ToString();
                label4.Text = dataGridView1.Rows[select].Cells[2].Value.ToString();
                label5.Text = dataGridView1.Rows[select].Cells[3].Value.ToString();
                label19.Text = dataGridView1.Rows[select].Cells[5].Value.ToString();
                textBox2.Text = dataGridView1.Rows[select].Cells[6].Value.ToString();
                akter();
            }
            else { pictureBox2.Visible = false; }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (a == 0) { MessageBox.Show("Авторизируйтесь"); }
            else
            {
                addkino ad = new addkino();
                ad.Show();
            }
          
        }

        private void label2_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new MySqlCommand("SELECT DISTINCT naimenovanie_film, seans.data_pokaza, seans.time_seans FROM film, seans WHERE film.id_film = seans.id_film AND film.id_film='" + index + "' ");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dataGridView6.DataSource = bSource;
            da.Update(dt);
            con.Close();

            if (dataGridView6.RowCount != 0)
            {
                l1 = label1.Text;
                Plan plan = new Plan();
                plan.Show();
            }
            else { MessageBox.Show("Нет сеансов !"); }

            
        }

        private void label2_MouseUp(object sender, MouseEventArgs e)
        {
           

        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            label2.ForeColor = System.Drawing.Color.Blue;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = System.Drawing.Color.Black;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_MouseDown(object sender, MouseEventArgs e)
        {
            janr();
        }

        private void comboBox2_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked)
            {
                label2.Visible = true;
                if (checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked)
                {

                    label16.Text = dateTimePicker1.Value.ToShortDateString();
                    con.Open();
                    cmd = new MySqlCommand("SELECT naimenovanie_film, DATE_FORMAT(data_premiere, '%d.%m.%Y'), age_estrictions, naimenovanie_genre, actor, lasting, opisanie_film, DATE_FORMAT(data_pokaza, '%d.%m.%Y') FROM film, genre, seans WHERE film.id_genre = genre.id_genre AND seans.id_film = film.id_film AND DATE_FORMAT(seans.data_pokaza, '%d.%m.%Y') = '" + dateTimePicker1.Text + "' ");
                    //cmd = new MySqlCommand("SELECT fname , long, start, finish, cost, vozrast,  janr,  proizvodstva , produser.name,format,description  FROM film,vozrast,janr,strana,produser,format,seansi where film.id_vozrastogr=vozrast.id_vozrast and  film.id_janr=janr.id_janr and film.id_strana=strana.id_proizvodstva and film.id_produser=produser.id_produser and film.id_format=format.id_format and  seansi.date='" + label16.Text + "' and seansi.id_film=film.id_film");
                    cmd.Connection = con;
                    da = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = dt;
                    dataGridView1.DataSource = bSource;
                    da.Update(dt);
                    con.Close();
                }
                if (!checkBox1.Checked && checkBox2.Checked && !checkBox3.Checked)
                {
                    con.Open();
                    cmd = new MySqlCommand("SELECT naimenovanie_film, DATE_FORMAT(data_premiere, '%d.%m.%Y'), age_estrictions, naimenovanie_genre, actor, lasting, opisanie_film,DATE_FORMAT(data_pokaza, '%d.%m.%Y') FROM film, genre, seans WHERE film.id_genre = genre.id_genre AND seans.id_film = film.id_film AND genre.naimenovanie_genre = '" + comboBox1.Text + "'");
                    //cmd = new MySqlCommand("SELECT fname , long, start, finish, cost, vozrast,  janr,  proizvodstva , produser.name,format,description  FROM film,vozrast,janr,strana,produser,format where film.id_vozrastogr=vozrast.id_vozrast and  film.id_janr=janr.id_janr and film.id_strana=strana.id_proizvodstva and film.id_produser=produser.id_produser and film.id_format=format.id_format and janr.janr='" + comboBox1.Text + "'");
                    cmd.Connection = con;
                    da = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = dt;
                    dataGridView1.DataSource = bSource;
                    da.Update(dt);
                    con.Close();
                }
                /*if (!checkBox1.Checked && !checkBox2.Checked && checkBox3.Checked)
                {
                    con.Open();
                    cmd = new MySqlCommand("SELECT fname , long, start, finish, cost, vozrast,  janr,  proizvodstva , produser.name,format,description  FROM film,vozrast,janr,strana,produser,format where film.id_vozrastogr=vozrast.id_vozrast and  film.id_janr=janr.id_janr and film.id_strana=strana.id_proizvodstva and film.id_produser=produser.id_produser and film.id_format=format.id_format and format.format='" + comboBox2.Text + "'");
                    cmd.Connection = con;
                    da = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = dt;
                    dataGridView1.DataSource = bSource;
                    da.Update(dt);
                    con.Close();
                }*/

                if (checkBox1.Checked && checkBox2.Checked && !checkBox3.Checked)
                {
                    label16.Text = dateTimePicker1.Value.ToShortDateString();
                    con.Open();
                    cmd = new MySqlCommand("SELECT naimenovanie_film, DATE_FORMAT(data_premiere, '%d.%m.%Y'), age_estrictions, naimenovanie_genre, actor, lasting, opisanie_film,DATE_FORMAT(data_pokaza, '%d.%m.%Y') FROM film, genre, seans WHERE film.id_genre = genre.id_genre AND seans.id_film = film.id_film AND genre.naimenovanie_genre = '" + comboBox1.Text + "' AND DATE_FORMAT(seans.data_pokaza, '%d.%m.%Y') = '" + dateTimePicker1.Text + "' ");
                    cmd.Connection = con;
                    da = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = dt;
                    dataGridView1.DataSource = bSource;
                    da.Update(dt);
                    con.Close();
                }

                /*if (checkBox1.Checked && !checkBox2.Checked && checkBox3.Checked)
                {
                    label16.Text = dateTimePicker1.Value.ToShortDateString();
                    con.Open();
                    cmd = new MySqlCommand("SELECT fname , long, start, finish, cost, vozrast,  janr,  proizvodstva , produser.name,format,description  FROM film,vozrast,janr,strana,produser,format,seansi where film.id_vozrastogr=vozrast.id_vozrast and  film.id_janr=janr.id_janr and film.id_strana=strana.id_proizvodstva and film.id_produser=produser.id_produser and film.id_format=format.id_format and seansi.date=='" + label16.Text  +"'and format.format='" + comboBox2.Text + "'and seansi.id_film=film.id_film");
                    cmd.Connection = con;
                    da = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = dt;
                    dataGridView1.DataSource = bSource;
                    da.Update(dt);
                    con.Close();
                }

                if (!checkBox1.Checked && checkBox2.Checked && checkBox3.Checked)
                {
                    con.Open();
                    cmd = new MySqlCommand("SELECT fname , long, start, finish, cost, vozrast,  janr,  proizvodstva , produser.name,format,description  FROM film,vozrast,janr,strana,produser,format where film.id_vozrastogr=vozrast.id_vozrast and  film.id_janr=janr.id_janr and film.id_strana=strana.id_proizvodstva and film.id_produser=produser.id_produser and film.id_format=format.id_format and janr.janr='" + comboBox1.Text + "' and format.format='" + comboBox2.Text + "'");
                    cmd.Connection = con;
                    da = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = dt;
                    dataGridView1.DataSource = bSource;
                    da.Update(dt);
                    con.Close();
                }
                if (checkBox1.Checked && checkBox2.Checked && checkBox3.Checked)
                {
                    label16.Text = dateTimePicker1.Value.ToShortDateString();
                    con.Open();
                    cmd = new MySqlCommand("SELECT fname , long, start, finish, cost, vozrast,  janr,  proizvodstva , produser.name,format,description  FROM film,vozrast,janr,strana,produser,format,seansi where film.id_vozrastogr=vozrast.id_vozrast and  film.id_janr=janr.id_janr and film.id_strana=strana.id_proizvodstva and film.id_produser=produser.id_produser and film.id_format=format.id_format and janr.janr='" + label16.Text + "' and janr.janr='" + comboBox1.Text + "' and format.format='" + comboBox2.Text + "'and seansi.id_film=film.id_film");
                    cmd.Connection = con;
                    da = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = dt;
                    dataGridView1.DataSource = bSource;
                    da.Update(dt);
                    con.Close();
                }*/

                if (dataGridView1.RowCount != 1)
                {
                    /*label1.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
                    label3.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                    label4.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                    label5.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    label19.Text = dataGridView1.Rows[0].Cells[5].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[0].Cells[6].Value.ToString();*/

                    select = 0;
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = true;

                    label1.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
                    label3.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                    label4.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                    label5.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    label19.Text = dataGridView1.Rows[0].Cells[5].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[0].Cells[6].Value.ToString();
                }
                else 
                {
                    label1.Text = "";
                    label3.Text = "";
                    label4.Text = "";
                    label5.Text = "";
                    label19.Text = "";
                    textBox2.Text ="";
                    textBox3.Text = "";
                    label2.Visible = false;
                    MessageBox.Show("Фильм не найден");
                }
            }

            else { ShowTable(); }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            user user = new user();
            user.ShowDialog();
          
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "HTML.chm");
        }

    }
}
