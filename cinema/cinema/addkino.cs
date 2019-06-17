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
using MySql.Data.MySqlClient;

namespace cinema
{
    public partial class addkino : Form
    {
        static string serverName = "localhost", userName = "root", dbName = "arm_rab_kinoteatra", port = "3306", password = "admin";
        static string connStr = "server=" + serverName + ";user=" + userName + ";database=" + dbName + ";port=" + port + ";password=" + password + ";";

        public static int ob=0;
        private MySqlConnection con;
        private MySqlCommand cmd;
        private MySqlDataAdapter da;
        private DataTable dt;
        BindingSource bis;
        ToolStripLabel dateLabel;
        ToolStripLabel timeLabel;
        ToolStripLabel infoLabel;
        Timer timer;
        int prov = 0;
        void timer_Tick(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.ToLongDateString();
            timeLabel.Text = DateTime.Now.ToLongTimeString();
            }
        
        public addkino()
        {
            InitializeComponent();
            con = new MySqlConnection(connStr);
            //con = new SqlConnection(@"Data Source=USERPC\SQLEXPRESS;Initial Catalog=dbcin;Integrated Security=True;Pooling=False");
            comboBox4.Visible = false;
            infoLabel = new ToolStripLabel();
            infoLabel.Text = "Текущие дата и время:";
            dateLabel = new ToolStripLabel();
            timeLabel = new ToolStripLabel();

            statusStrip1.Items.Add(infoLabel);
            statusStrip1.Items.Add(dateLabel);
            statusStrip1.Items.Add(timeLabel);
            timer = new Timer() { Interval = 100 };
            //     timer = new Timer() { Interval = 1000 };
            timer.Tick += timer_Tick;
            timer.Start();


            button4.Visible = false;
            textBox3.Visible = false;
            label9.Visible = false;
            comboBox3.Visible = false;
            label25.Visible = false;
            label24.Visible = false;
            comboBox7.Visible = false;
            comboBox8.Visible = false;
            label20.Visible = false;
            label21.Visible = false;
            comboBox5.Visible = false;
            checkBox1.Visible = false;
            textBox4.Visible = false;
            dateTimePicker1.Visible = false;
            //dateTimePicker2.Visible = false;
            textBox1.Visible = false;

            //textBox3.Visible = false;
            //textBox7.Visible = false;
            //textBox8.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            //comboBox3.Visible = false;

            label3.Visible = false;
            label4.Visible = false;
            //label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            //label9.Visible = false;
            //label10.Visible = false;

            label1.Visible = false;
            comboBox4.Visible = false;
            textBox2.Visible = false;
            label2.Visible = false;

            button1.Visible = false;
            button2.Visible = false;
            label26.Visible = false;
            label27.Visible = false;
            dataGridView10.Visible = false;
        }

        public string   jn;
        public int index;

        public void filmakter()
        {
            con.Open();
          //  indexf();
            cmd = new MySqlCommand("SELECT DISTINCT film.naimenovanie_film, film.actor FROM film");

            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dataGridView1.DataSource = bSource;
            da.Update(dt);
            // dt2();
            dataGridView1.Columns[0].HeaderText = "Фильм";
            dataGridView1.Columns[1].HeaderText = "В ролях";
            con.Close();
        }

        public void film()
        {
            con.Open();
            cmd = new MySqlCommand("SELECT naimenovanie_film FROM film ");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dataGridView5.DataSource = bSource;
            //  index = Convert.ToInt16(dataGridView3.Rows[0].Cells[0].Value);
            //label6.Text = index.ToString();
            da.Update(dt);

            con.Close();
            comboBox4.Items.Clear();
            for (int i = 0; i < dataGridView5.RowCount - 1; i++)
            { comboBox4.Items.Add(dataGridView5.Rows[i].Cells[0].Value); }
        }

        public void janr()
        {

            con.Open();
            cmd = new MySqlCommand("SELECT naimenovanie_genre FROM genre");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dataGridView4.DataSource = bSource;
            da.Update(dt);

            con.Close();
            comboBox2.Items.Clear();
            for (int i = 0; i < dataGridView4.RowCount - 1; i++)
            {
                comboBox2.Items.Add(dataGridView4.Rows[i].Cells[0].Value);
            }
        }
        
        public void zal()
        {
            con.Open();
            cmd = new MySqlCommand("SELECT nazvanie_zala FROM zal");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dataGridView9.DataSource = bSource;
            da.Update(dt);

            con.Close();
            comboBox1.Items.Clear();
            for (int i = 0; i < dataGridView9.RowCount - 1; i++)
            {
                comboBox1.Items.Add(dataGridView9.Rows[i].Cells[0].Value);
            }
        }

        public void indexf()
        {
           
            string fname = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            cmd = new MySqlCommand("SELECT id_film   FROM film WHERE  film.naimenovanie_film= '" + fname + "' ");
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
            cmd = new MySqlCommand("SELECT DISTINCT actor FROM film WHERE  film.id_film='" + index + "' ");
           
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dataGridView2.DataSource = bSource;
            da.Update(dt);
            // dt2();
            //dataGridView2.Columns[0].HeaderText = "В ролях";
            textBox5.Visible = true;
            textBox5.Text = "В ролях:\n\n\t\t\t" + dataGridView2.Rows[0].Cells[0].Value.ToString();
            con.Close();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true || radioButton2.Checked == true || radioButton4.Checked == true)
            {
                if (radioButton1.Checked)
                {
                    if (textBox1.Text != "" && comboBox7.Text != "" && comboBox8.Text != "" && textBox4.Text != "" && comboBox1.Text != "" && comboBox2.Text != "")
                    {
                        ob = 1;
                        con.Open();

                        /*cmd = new MySqlCommand("SELECT DISTINCT age_estrictions FROM film WHERE age_estrictions='" + comboBox1.Text + "'");
                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        BindingSource bSource = new BindingSource();
                        bSource.DataSource = dt;
                        label13.Text = dt.Rows[0]["age_estrictions"].ToString().Trim();
                        da.Update(dt);*/

                        cmd = new MySqlCommand("SELECT DISTINCT genre.id_genre FROM genre WHERE  genre.naimenovanie_genre='" + comboBox2.Text + "'");
                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        BindingSource bSource = new BindingSource();
                        bSource.DataSource = dt;
                        label14.Text = dt.Rows[0]["id_genre"].ToString().Trim();
                        da.Update(dt);

                        /*cmd = new MySqlCommand("SELECT strana.proizvodstva FROM strana ");
                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        bSource.DataSource = dt;
                        dataGridView6.DataSource = bSource;
                        da.Update(dt);
                        prov = 0;
                        /*for (int i = 0; i < dataGridView6.RowCount; i++)
                        {
                            if (dataGridView6.Rows[i].Cells[0].Value.ToString() == textBox7.Text)
                            {
                                cmd = new MySqlCommand("SELECT DISTINCT strana.id_proizvodstva FROM strana where  strana.proizvodstva='" + textBox7.Text + "'");
                                cmd.Connection = con;
                                da = new MySqlDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                bSource.DataSource = dt;
                                label15.Text = dt.Rows[0]["id_proizvodstva"].ToString().Trim();
                                da.Update(dt);
                                prov = 1;
                            }
                            else
                            {
                                if (i == dataGridView6.RowCount - 1 && prov != 1)
                                {
                                    cmd = new MySqlCommand("insert into strana (proizvodstva)  values ('" + textBox7.Text + "')");
                                    cmd.Connection = con;
                                    da = new MySqlDataAdapter(cmd);
                                    dt = new DataTable();
                                    da.Fill(dt);
                                    bSource.DataSource = dt;
                                    da.Update(dt);

                                    cmd = new MySqlCommand("SELECT DISTINCT strana.id_proizvodstva FROM strana where  strana.proizvodstva='" + textBox7.Text + "'");
                                    cmd.Connection = con;
                                    da = new MySqlDataAdapter(cmd);
                                    dt = new DataTable();
                                    da.Fill(dt);
                                    bSource.DataSource = dt;
                                    label15.Text = dt.Rows[0]["id_proizvodstva"].ToString().Trim();
                                    da.Update(dt);
                                }
                            }
                        }*/

                        /* prov = 0;

                           for (int i = 0; i < dataGridView6.RowCount; i++)
                           {
                               if (dataGridView6.Rows[i].Cells[0].Value.ToString() == textBox7.Text)
                               {
                                   cmd = new MySqlCommand("SELECT DISTINCT produser.id_produser FROM produser where  produser.name='" + textBox8.Text + "'");
                                   cmd.Connection = con;
                                   da = new MySqlDataAdapter(cmd);
                                   dt = new DataTable();
                                   da.Fill(dt);
                                   bSource.DataSource = dt;
                                   label16.Text = dt.Rows[0]["id_produser"].ToString().Trim();
                                   da.Update(dt);
                                   prov = 1;
                               }
                               else
                               {
                                   if (i == dataGridView6.RowCount - 1 && prov != 1)
                                   {
                                       cmd = new MySqlCommand("insert into produser (name)  values ('" + textBox8.Text + "')");
                                       cmd.Connection = con;
                                       da = new MySqlDataAdapter(cmd);
                                       dt = new DataTable();
                                       da.Fill(dt);
                                       bSource.DataSource = dt;
                                       da.Update(dt);

                                       cmd = new MySqlCommand("SELECT DISTINCT produser.id_produser FROM produser where  produser.name='" + textBox8.Text + "'");
                                       cmd.Connection = con;
                                       da = new MySqlDataAdapter(cmd);
                                       dt = new DataTable();
                                       da.Fill(dt);
                                       bSource.DataSource = dt;
                                       label16.Text = dt.Rows[0]["id_produser"].ToString().Trim();
                                       da.Update(dt);
                                       con.Close();
                                   }
                               }
                           }

                           cmd = new MySqlCommand("SELECT DISTINCT format.id_format FROM format where format.format='" + comboBox3.Text + "'");
                           cmd.Connection = con;
                           da = new MySqlDataAdapter(cmd);
                           dt = new DataTable();
                           da.Fill(dt);
                           bSource.DataSource = dt;
                           label17.Text = dt.Rows[0]["id_format"].ToString().Trim();
                           da.Update(dt);*/


                        label22.Text = dateTimePicker1.Value.ToShortDateString();
                        //label23.Text = dateTimePicker2.Value.ToShortDateString();


                        cmd = new MySqlCommand("INSERT INTO film (naimenovanie_film, lasting, data_premiere, age_estrictions, id_genre, opisanie_film, actor) values ('" + textBox1.Text + "', '" + comboBox7.Text + ":" + comboBox8.Text + ":00', '" + label22.Text + "', '" + comboBox3.Text + "', '" + label14.Text + "', '" + textBox4.Text + "', '" + textBox6.Text + "') ");
                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        bSource.DataSource = dt;
                        da.Update(dt);

                        //SELECT MAX(id), * FROM table

                        cmd = new MySqlCommand("SELECT MAX(id_film) FROM film ");
                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        bSource = new BindingSource();
                        bSource.DataSource = dt;
                        dataGridView2.DataSource = bSource;
                        da.Update(dt);
                        label5.Text = dataGridView2.Rows[0].Cells[0].Value.ToString();
                        con.Close();

                        cmd = new MySqlCommand("SELECT id_zal FROM zal WHERE zal.nazvanie_zala = '" + comboBox1.Text + "' ");
                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        bSource = new BindingSource();
                        bSource.DataSource = dt;
                        dataGridView2.DataSource = bSource;
                        da.Update(dt);
                        label10.Text = dataGridView2.Rows[0].Cells[0].Value.ToString();
                        con.Close();

                        cmd = new MySqlCommand("INSERT INTO seans (id_zal, id_film, time_seans, data_pokaza) values ('" + label10.Text + "','" + label5.Text + "','12:30:00','" + label22.Text + "') ");
                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        bSource.DataSource = dt;
                        da.Update(dt);

                        cmd = new MySqlCommand("SELECT naimenovanie_film, lasting, age_estrictions, genre.naimenovanie_genre, opisanie_film FROM film, genre, seans WHERE film.id_genre = genre.id_genre AND seans.id_film = film.id_film");
                        //cmd = new MySqlCommand("SELECT fname , long, start, finish, cost, vozrast,  janr,  proizvodstva , produser.name,format,description  FROM film,vozrast,janr,strana,produser,format where film.id_vozrastogr=vozrast.id_vozrast and  film.id_janr=janr.id_janr and film.id_strana=strana.id_proizvodstva and film.id_produser=produser.id_produser and film.id_format=format.id_format ");
                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        bSource.DataSource = dt;
                        dataGridView1.DataSource = bSource;
                        da.Update(dt);
                        con.Close();
                    }
                    else { MessageBox.Show("Введите данные !"); }
                }
                if (radioButton2.Checked)
                {
                    if (comboBox7.Text != "" && comboBox8.Text != "" && comboBox4.Text != "")
                    {
                        con.Open();

                        cmd = new MySqlCommand("SELECT DISTINCT film.id_film FROM film WHERE  film.naimenovanie_film='" + comboBox4.Text + "'");
                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        BindingSource bSource = new BindingSource();
                        bSource.DataSource = dt;
                        label11.Text = dt.Rows[0]["id_film"].ToString().Trim();
                        da.Update(dt);

                        label12.Text = dateTimePicker1.Value.ToShortDateString();
                        //cmd = new MySqlCommand("INSER INTO seans(id_zal, id_film, time_seans, data_pokaza) VALUES (1, '" + Convert.ToInt16(label11.Text) + "', '" + comboBox7.Text + ":" + comboBox8.Text + ":00"
                        cmd = new MySqlCommand("INSERT INTO seans(id_zal, id_film, time_seans, data_pokaza) values (1, '" + Convert.ToInt16(label11.Text) + "', '" + comboBox7.Text + ":" + comboBox8.Text + ":00" + "', '" + label12.Text + "')");
                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        da.Update(dt);

                        cmd = new MySqlCommand("SELECT film.naimenovanie_film, seans.data_pokaza, seans.time_seans FROM film, seans WHERE film.id_film=seans.id_film ");
                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        bSource.DataSource = dt;
                        dataGridView1.DataSource = bSource;
                        da.Update(dt);

                        con.Close();
                    }
                    else { MessageBox.Show("Введите данные !"); }
                }

                if (radioButton4.Checked)
                {

                    if (comboBox4.Text != "" && textBox2.Text != "")
                    {
                        con.Open();
                        con.Close();
                        con.Close();
                        con.Open();
                        ob = 1;





                        cmd = new MySqlCommand("SELECT film.actor FROM film");

                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        BindingSource bSource = new BindingSource();
                        bSource.DataSource = dt;
                        dataGridView8.DataSource = bSource;
                        da.Update(dt);

                        for (int i = 0; i < dataGridView8.RowCount; i++)
                        {
                            if (dataGridView8.Rows[i].Cells[0].Value.ToString() == textBox2.Text)
                            { break; }
                            else
                            {
                                if (i == dataGridView8.RowCount - 1)
                                {
                                    cmd = new MySqlCommand("INSERT INTO film (actor) values ('" + textBox2.Text + "')");
                                    cmd.Connection = con;
                                    da = new MySqlDataAdapter(cmd);
                                    dt = new DataTable();
                                    da.Fill(dt);
                                    //BindingSource bSource = new BindingSource();
                                    bSource.DataSource = dt;
                                    dataGridView1.DataSource = bSource;
                                    da.Update(dt);
                                }
                            }
                        }
                        cmd = new MySqlCommand("SELECT DISTINCT film.id_film FROM film WHERE film.naimenovanie_film='" + comboBox4.Text + "'");
                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        bSource.DataSource = dt;
                        label11.Text = dt.Rows[0]["id_film"].ToString().Trim();
                        da.Update(dt);

                        cmd = new MySqlCommand("SELECT DISTINCT film.id_film FROM film WHERE film.actor = '" + textBox2.Text + "'");
                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        label12.Text = dt.Rows[0]["id_film"].ToString().Trim();
                        da.Update(dt);


                        // dataGridView8.RowCount = 0;
                        /*cmd = new MySqlCommand("SELECT  *   FROM uchastniki ");
                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        // BindingSource bSource = new BindingSource();
                        bSource.DataSource = dt;
                        dataGridView8.DataSource = bSource;
                        da.Update(dt);*/

                        /*for (int i = 0; i < dataGridView8.RowCount; i++)
                        {
                            if (dataGridView8.Rows[i].Cells[0].Value.ToString() == label11.Text && dataGridView8.Rows[i].Cells[1].Value.ToString() == label12.Text)
                            { break; }
                            else
                            {
                                if (i == dataGridView8.RowCount - 1)
                                {

                                    cmd = new MySqlCommand("insert into uchastniki values ('" + label11.Text + "','" + label12.Text + "')");
                                    cmd.Connection = con;
                                    da = new MySqlDataAdapter(cmd);
                                    dt = new DataTable();
                                    da.Fill(dt);
                                    da.Update(dt);
                                    con.Close();
                                    filmakter();
                                }
                            }
                        }*/

                    }
                    else { MessageBox.Show("Введите данные !"); }
                    con.Close();

                }

            }
            else { MessageBox.Show("Не выбрана операция"); }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            if (radioButton1.Checked)
            {
                con.Open();
                ob = 1;

                cmd = new MySqlCommand("SELECT DISTINCT film.id_film FROM film WHERE  film.naimenovanie_film='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");
                cmd.Connection = con;
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dt;
                label11.Text = dt.Rows[0]["id_film"].ToString().Trim();
                da.Update(dt);

                cmd = new MySqlCommand("SELECT seans.id_seans, film.naimenovanie_film, seans.data_pokaza, seans.time_seans FROM film, seans WHERE film.id_film = seans.id_film AND seans.id_film='" + label11.Text + "' ");
                cmd.Connection = con;
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                bSource.DataSource = dt;
                dataGridView7.DataSource = bSource;
                da.Update(dt);

                if (dataGridView7.RowCount != 0)
                {
                    /*for (int i = 0; i < dataGridView7.RowCount; i++)
                    {
                        cmd = new MySqlCommand("DELETE FROM bilet where bilet.id_seans='" + dataGridView7.Rows[i].Cells[0].Value.ToString() + "'");
                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        da.Update(dt);
                    }*/
                    

                    /*cmd = new MySqlCommand("DELETE FROM uchastniki where id_film='" + label11.Text + "'");
                    cmd.Connection = con;
                    da = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    da.Update(dt);*/
                }

                cmd = new MySqlCommand("DELETE FROM seans WHERE id_film='" + label11.Text + "'");
                cmd.Connection = con;
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                da.Update(dt);

                cmd = new MySqlCommand("DELETE FROM film WHERE id_film='" + label11.Text + "'");
                cmd.Connection = con;
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                da.Update(dt);

                cmd = new MySqlCommand("SELECT naimenovanie_film, lasting, zal.value, age_estrictions, genre.naimenovanie_genre, opisanie_film FROM film, genre, seans, zal WHERE film.id_genre = genre.id_genre AND seans.id_film = film.id_film AND seans.id_zal = zal.id_zal");
                //cmd = new MySqlCommand("SELECT fname , long, start, finish, cost, vozrast,  janr,  proizvodstva , produser.name,format,description  FROM film,vozrast,janr,strana,produser,format where film.id_vozrastogr=vozrast.id_vozrast and  film.id_janr=janr.id_janr and film.id_strana=strana.id_proizvodstva and film.id_produser=produser.id_produser and film.id_format=format.id_format ");
                cmd.Connection = con;
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                bSource.DataSource = dt;
                dataGridView1.DataSource = bSource;
                da.Update(dt);
                con.Close();

            }
            if (radioButton2.Checked)
            {
                con.Open();

                cmd = new MySqlCommand("SELECT DISTINCT film.id_film FROM film WHERE film.naimenovanie_film='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");
                cmd.Connection = con;
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dt;
                label11.Text = dt.Rows[0]["id_film"].ToString().Trim();
                da.Update(dt);

                cmd = new MySqlCommand("SELECT DISTINCT seans.id_seans FROM seans, film WHERE seans.id_film = '" + label11.Text + "'");
                cmd.Connection = con;
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                label12.Text = dt.Rows[0]["id_seans"].ToString().Trim();
                da.Update(dt);

                /*cmd = new MySqlCommand("delete from bilet where id_seans='" + label12.Text + "'");
                cmd.Connection = con;
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                da.Update(dt);*/

                cmd = new MySqlCommand("DELETE FROM seans WHERE id_seans='" + label12.Text + "'");
                cmd.Connection = con;
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                da.Update(dt);

                cmd = new MySqlCommand("SELECT film.naimenovanie_film, seans.data_pokaza, seans.time_seans FROM film, seans WHERE film.id_film=seans.id_film ");
                cmd.Connection = con;
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                bSource.DataSource = dt;
                dataGridView1.DataSource = bSource;
                da.Update(dt);

                con.Close();

            }
          
            if (radioButton4.Checked)
            {
                con.Open();

                ob = 1;
                cmd = new MySqlCommand("SELECT DISTINCT film.id_film FROM film where  film.fname='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");
                cmd.Connection = con;
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dt;
                label11.Text = dt.Rows[0]["id_film"].ToString().Trim();
                da.Update(dt);

                cmd = new MySqlCommand("SELECT DISTINCT akter.id_akter FROM akter where  akter.name = '" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'");
                cmd.Connection = con;
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                label12.Text = dt.Rows[0]["id_akter"].ToString().Trim();
                da.Update(dt);

                cmd = new MySqlCommand("delete from uchastniki where id_film='" + label11.Text + "' and id_akter='" + label12.Text + "'");
                cmd.Connection = con;
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                da.Update(dt);
                con.Close();

                filmakter();


            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label9.Visible = true;
            comboBox3.Visible = true;
            label25.Visible = true;
            label24.Visible = true;
            comboBox7.Visible = true;
            comboBox8.Visible = true;
            label20.Visible = false;
            label21.Visible = false;
            comboBox5.Visible = false;
            checkBox1.Visible = false;
            button1.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = false;
            //textBox3.Visible = true;
            textBox4.Visible = true;
            textBox6.Visible = true;
            textBox5.Visible = false;
            //textBox7.Visible = true;
            //textBox8.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            //comboBox3.Visible = true;
            dateTimePicker1.Visible = true;
            //dateTimePicker2.Visible = true;
            label2.Text = "Продолжительность сеанса";
            label3.Text = "Дата начала проката";
            //label4.Text = "Дата окончания проката";
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            //label5.Visible = true;
            dataGridView1.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;

            label26.Visible = false;
            label27.Visible = false;
            dataGridView10.Visible = false;
            button4.Visible = false;
            textBox3.Visible = false;

            button4.Visible = false;
            textBox3.Visible = false;

            //label9.Visible = true;
            button2.Visible = true;
            //label10.Visible = true;
            //dataGridView2.Visible = true;
            //textBox5.Visible = true;
            radioButton2.Checked = false;
            //radioButton3.Checked = false;
            comboBox4.Visible = false;
            con.Open();
            cmd = new MySqlCommand("SELECT naimenovanie_film, lasting, age_estrictions, genre.naimenovanie_genre, opisanie_film FROM film, genre, seans WHERE film.id_genre = genre.id_genre AND seans.id_film = film.id_film");
            //cmd = new MySqlCommand("SELECT fname , long, start, finish, cost, vozrast,  janr,  proizvodstva , produser.name,format,description  FROM film,vozrast,janr,strana,produser,format WHERE film.id_vozrastogr=vozrast.id_vozrast and  film.id_janr=janr.id_janr and film.id_strana=strana.id_proizvodstva and film.id_produser=produser.id_produser and film.id_format=format.id_format ");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dataGridView1.DataSource = bSource;
            da.Update(dt);
            dataGridView1.Columns[0].HeaderText = "Название фильма";
            dataGridView1.Columns[1].HeaderText = "Длительность";
            dataGridView1.Columns[2].HeaderText = "Возрастное ограничение";
            dataGridView1.Columns[3].HeaderText = "Жанр";
            dataGridView1.Columns[4].HeaderText = "Описание";
            //dataGridView1.Columns[5].HeaderText = "Описание";
            //dataGridView1.Columns[6].HeaderText = "Жанр";
            //dataGridView1.Columns[7].HeaderText = "Производство";
            //dataGridView1.Columns[8].HeaderText = "Продюсер";
            //dataGridView1.Columns[9].HeaderText = "Формат";
            //dataGridView1.Columns[10].HeaderText = "Описание";

            con.Close();
            janr();
            zal();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label9.Visible = false;
            comboBox3.Visible = false;
            textBox2.Visible = false;
            label25.Visible = false;
            label24.Visible = true;
            dataGridView10.Visible = false;
            textBox6.Visible = false;
            comboBox7.Visible = true;
            comboBox8.Visible = true;
            button4.Visible = false;
            textBox3.Visible = false;
            label20.Visible = false;
            label21.Visible = false;
            comboBox5.Visible = false;
            checkBox1.Visible = false;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            button1.Visible = true;
            dateTimePicker1.Visible = true;
            //dateTimePicker2.Visible = false;
            //textBox3.Visible = false;
            //textBox7.Visible = false;
            //textBox8.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            //comboBox3.Visible = false;
            comboBox4.Visible = true;
            label3.Text = "Дата начала сеанса";
            label2.Text = "Время начала сеанса";
            label4.Visible = false;
            //label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            //label9.Visible = false;
            //label10.Visible = false;
            dataGridView2.Visible = false;
            dataGridView1.Visible = true;
            textBox5.Visible = false;
            radioButton1.Checked = false;
            //radioButton3.Checked = false;
            textBox4.Visible = false;
            button2.Visible = true;

            label26.Visible = false;
            label27.Visible = false;
            dataGridView10.Visible = false;
            button4.Visible = false;
            textBox3.Visible = false;

            con.Open();
            cmd = new MySqlCommand("SELECT film.naimenovanie_film, seans.data_pokaza, seans.time_seans FROM film, seans WHERE film.id_film=seans.id_film ");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dataGridView1.DataSource = bSource;
            da.Update(dt);

            dataGridView1.Columns[0].HeaderText = "Название фильма";
            dataGridView1.Columns[1].HeaderText = "Дата начала сеанса";
            dataGridView1.Columns[2].HeaderText = "Время начала сеанса";

            con.Close();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label9.Visible = false;
            comboBox3.Visible = false;
            label25.Visible = false;
            label20.Visible = true;
            label21.Visible = true;
            comboBox5.Visible = true;
            textBox6.Visible = false;
            checkBox1.Visible = true;
            label24.Visible = false;
            comboBox7.Visible = false;
            comboBox8.Visible = false;
            textBox4.Visible = false;
            dateTimePicker1.Visible = false;
            //dateTimePicker2.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            //textBox3.Visible = false;
            //textBox7.Visible = false;
            // textBox8.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            //comboBox3.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            //label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            //label9.Visible = false;
            // label10.Visible = false;
            button1.Visible = false;
            dataGridView2.Visible = false;
            textBox5.Visible = false;
            radioButton2.Checked = false;
            radioButton1.Checked = false;
            comboBox4.Visible = false;
            button2.Visible = true;
            con.Open();
            /*cmd = new MySqlCommand("SELECT  DISTINCT  film.fname,seansi.date ,seansi.time,sostojanie.sostojanie,mesto.rad,mesto.mesto  FROM film,sostojanie,seansi,bilet,mesto where  film.id_film=seansi.id_film and  seansi.id_seansi=bilet.id_seans and bilet.id_sostojanie=sostojanie.id_sostojanie and bilet.id_mesto=mesto.id_mesto ");
            Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dataGridView1.DataSource = bSource;
            da.Update(dt);
          
            dataGridView1.Columns[0].HeaderText = "Название фильма";
            dataGridView1.Columns[1].HeaderText = "Дата начала сеанса";
            dataGridView1.Columns[2].HeaderText = "Время начала сеанса";
            dataGridView1.Columns[3].HeaderText = "Состояние";
            dataGridView1.Columns[4].HeaderText = "Ряд";
            dataGridView1.Columns[5].HeaderText = "Место";*/

            con.Close();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            akter();
        }

        private void comboBox4_MouseDown(object sender, MouseEventArgs e)
        {
            film();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            filmakter();
            label9.Visible = false;
            comboBox3.Visible = false;
            button4.Visible = false;
            textBox3.Visible = false;
            label25.Visible = false;
            label24.Visible = false;
            dataGridView10.Visible = false;
            comboBox7.Visible = false;
            comboBox8.Visible = false;
            textBox6.Visible = false;
            //label1.Visible = true;
            comboBox4.Visible = false;
            //textBox2.Visible = true;
            //label2.Visible = true;
            //label2.Text = "Актер";
            button1.Visible = false;
            button2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            textBox4.Visible = false;
            dateTimePicker1.Visible = false;
            // dateTimePicker2.Visible = false;
            textBox1.Visible = false;
            button4.Visible = true;
            textBox3.Visible = true;
            textBox3.Text = "";

            //textBox3.Visible = false;
            //textBox7.Visible = false;
            //textBox8.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            // comboBox3.Visible = false;

            label3.Visible = false;
            label4.Visible = false;
            //label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            //label9.Visible = false;
            //label10.Visible = false;

            label26.Visible = false;
            label27.Visible = false;
            dataGridView10.Visible = false;

            dataGridView1.Visible = true;

            dataGridView2.Visible = false;
            textBox5.Visible = false;
            radioButton2.Checked = false;
            radioButton1.Checked = false;

            label20.Visible = false;
            label21.Visible = false;
            comboBox5.Visible = false;
            checkBox1.Visible = false;
            
        }

        private void addkino_Load(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            //filmakter();
            label9.Visible = false;
            comboBox3.Visible = false;
            label25.Visible = false;
            label24.Visible = false;
            comboBox7.Visible = false;
            comboBox8.Visible = false;
            textBox6.Visible = false;
            button2.Visible = false;
            textBox4.Visible = false;
            dateTimePicker1.Visible = false;
            textBox1.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            dataGridView2.Visible = false;
            dataGridView1.Visible = false;
            textBox5.Visible = false;
            radioButton2.Checked = false;
            radioButton1.Checked = false;
            label20.Visible = false;
            label21.Visible = false;
            comboBox5.Visible = false;
            checkBox1.Visible = false;
            label26.Visible = true;
            label27.Visible = true;
            dataGridView10.Visible = true;
            button4.Visible = true;
            textBox3.Visible = true;
            label1.Visible = false;
            label2.Visible = false;
            textBox3.Text = "";


            con.Open();
            cmd = new MySqlCommand("SELECT surname, name, nasvanie FROM associate, dolznost WHERE dolznost.id_dolznost = associate.id_dolznost AND associate.id_associate = '" + Form1.user_id + "' ");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            DataRow[] myData = dt.Select();


            label26.Text = "Вы вошли как: " + myData[0].ItemArray[0] + " " + myData[0].ItemArray[1];
            label27.Text = "" + myData[0].ItemArray[2];
            con.Close();

            con.Open();
            cmd = new MySqlCommand("SELECT surname, name, nasvanie, data_dezhyrstva FROM associate, dezhyrstva, dolznost WHERE dolznost.id_dolznost = associate.id_dolznost AND associate.id_associate = dezhyrstva.id_associate;");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dataGridView10.DataSource = bSource;
            da.Update(dt);

            dataGridView10.Columns[0].HeaderText = "Имя";
            dataGridView10.Columns[1].HeaderText = "Фамилия";
            dataGridView10.Columns[2].HeaderText = "Должность";
            dataGridView10.Columns[3].HeaderText = "Дата дежурства";

            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                con.Open();
                cmd = new MySqlCommand("SELECT surname, name, nasvanie, data_dezhyrstva FROM associate, dezhyrstva, dolznost WHERE dolznost.id_dolznost = associate.id_dolznost AND associate.id_associate = dezhyrstva.id_associate AND (surname LIKE '%" + textBox3.Text + "%' OR name LIKE '%" + textBox3.Text + "%' OR nasvanie LIKE '%" + textBox3.Text + "%');");
                cmd.Connection = con;
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dt;
                dataGridView10.DataSource = bSource;
                da.Update(dt);

                dataGridView10.Columns[0].HeaderText = "Имя";
                dataGridView10.Columns[1].HeaderText = "Фамилия";
                dataGridView10.Columns[2].HeaderText = "Должность";
                dataGridView10.Columns[3].HeaderText = "Дата дежурства";

                con.Close();
            }
            if (radioButton4.Checked)
            {
                con.Open();
                cmd = new MySqlCommand("SELECT naimenovanie_film, actor FROM film WHERE naimenovanie_film LIKE '%" + textBox3.Text + "%' OR actor LIKE '%" + textBox3.Text + "%'");

                cmd.Connection = con;
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dt;
                dataGridView1.DataSource = bSource;
                da.Update(dt);
                dataGridView1.Columns[0].HeaderText = "Фильм";
                dataGridView1.Columns[1].HeaderText = "В ролях";
                con.Close();
            }
           

            
        }

        private void comboBox5_MouseDown(object sender, MouseEventArgs e)
        {

            con.Open();
            cmd = new MySqlCommand("SELECT fname FROM film ");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dataGridView5.DataSource = bSource;
          
            da.Update(dt);

            con.Close();
            comboBox5.Items.Clear();
            for (int i = 0; i < dataGridView5.RowCount - 1; i++)
            { comboBox5.Items.Add(dataGridView5.Rows[i].Cells[0].Value); }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                con.Open();
                cmd = new MySqlCommand("SELECT  DISTINCT  film.fname,seansi.date ,seansi.time,sostojanie.sostojanie,mesto.rad,mesto.mesto  FROM film,sostojanie,seansi,bilet,mesto where  film.id_film=seansi.id_film and  seansi.id_seansi=bilet.id_seans and bilet.id_sostojanie=sostojanie.id_sostojanie and bilet.id_mesto=mesto.id_mesto ");
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
        }

        private void comboBox5_SelectedValueChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new MySqlCommand("SELECT  DISTINCT  film.fname,seansi.date ,seansi.time,sostojanie.sostojanie,mesto.rad,mesto.mesto  FROM film,sostojanie,seansi,bilet,mesto where  film.id_film=seansi.id_film and  seansi.id_seansi=bilet.id_seans and bilet.id_sostojanie=sostojanie.id_sostojanie and bilet.id_mesto=mesto.id_mesto and film.fname= '" + comboBox5.Text + "' ");
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8) && !((e.KeyChar == '.' )))
            {
                e.Handled = true;
            }
        }
    }
}
