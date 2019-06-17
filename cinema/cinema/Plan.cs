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
    public partial class Plan : Form
    {
        static string serverName = "localhost", userName = "root", dbName = "arm_rab_kinoteatra", port = "330", password = "12345678";
        static string connStr = "server=" + serverName + ";user=" + userName + ";database=" + dbName + ";port=" + port + ";password=" + password + ";";

        Plan p;
        DataGridView[] tab = new DataGridView[9];
        DataGridView[] tab21 = new DataGridView[7];
        string per;
        int select = 0;
        ToolStripLabel dateLabel;
        ToolStripLabel timeLabel;
        ToolStripLabel infoLabel;
        Timer timer;

        private MySqlConnection con;
        private MySqlCommand cmd;
        private MySqlDataAdapter da;
        private DataTable dt;
        BindingSource bis;
        int proverka = 0;
        void timer_Tick(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.ToLongDateString();
            timeLabel.Text = DateTime.Now.ToLongTimeString();
        }

        public Plan()
        {
            InitializeComponent();
            con = new MySqlConnection(connStr);
            //con = new SqlConnection(@"Data Source=USERPC\SQLEXPRESS;Initial Catalog=dbcin;Integrated Security=True;Pooling=False");
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

            dataGridView11.RowCount = 9;
            dataGridView11.ColumnCount = 21;
          

             for (int i = 0; i < 9; i++)
             {
                 dataGridView11.Rows[i].Height = 32;
                 for (int j = 0; j < 21; j++)
                 {

                     dataGridView11.Columns[j].Width = 30;
                     if (i == 0)
                     {
                         if (j == 17) { break; }
                         else { dataGridView11.Rows[i].Cells[j + 2].Value = j + 1; }
                         
                     }
                     if (i == 1)
                     {
                         if (j == 19) { break; }
                         else { dataGridView11.Rows[i].Cells[j + 1].Value = j + 1; }

                     }
                     if (i>1)
                     {
                         dataGridView11.Rows[i].Cells[j].Value = j + 1;
                     }
                     

                 }
             }

             dataGridView11.Rows[0].Cells[0].Style.BackColor = Color.White;
             dataGridView11.Rows[1].Cells[0].Style.BackColor = Color.White;
             dataGridView11.Rows[0].Cells[1].Style.BackColor = Color.White;
             dataGridView11.Rows[0].Cells[20].Style.BackColor = Color.White;
             dataGridView11.Rows[1].Cells[20].Style.BackColor = Color.White;
             dataGridView11.Rows[0].Cells[19].Style.BackColor = Color.White;

            label1.Text = Form1.l1;

            con.Open();



            cmd = new MySqlCommand("SELECT DISTINCT film.id_film FROM film WHERE film.naimenovanie_film='" + label1.Text + "'");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            label2.Text = dt.Rows[0]["id_film"].ToString().Trim();
            da.Update(dt);


            cmd = new MySqlCommand("SELECT DISTINCT zal.value AS cost FROM zal, seans, film WHERE film.naimenovanie_film='" + label1.Text + "' AND seans.id_film = film.id_film AND seans.id_zal = zal.id_zal");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            bSource.DataSource = dt;
            label11.Text = dt.Rows[0]["cost"].ToString().Trim();
            da.Update(dt);

            cmd = new MySqlCommand("SELECT film.naimenovanie_film, seans.data_pokaza, seans.time_seans  FROM film, seans WHERE film.id_film=seans.id_film AND seans.id_film='" + label2.Text + "' ");
            cmd.Connection = con;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            bSource.DataSource = dt;
            dataGridView10.DataSource = bSource;
            da.Update(dt);

            con.Close();

            if (dataGridView10.RowCount != 1)
            {
                label3.Text = dataGridView10.Rows[0].Cells[1].Value.ToString();
                label4.Text = dataGridView10.Rows[0].Cells[2].Value.ToString();
            }
            else { MessageBox.Show("Нет сеансов !");  }
            bileti();
            kolbilet();
        }

        public void bileti()
        {
            con.Open();
            cmd = new MySqlCommand("SELECT  DISTINCT  film.naimenovanie_film, seans.data_pokaza, seans.time_seans, mesta.status, mesta.row, mesta.mesto  " +
                "FROM film, seans, mesta " +
                "WHERE seans.time_seans='" + label4.Text + "' AND seans.data_pokaza='" + label3.Text + "' AND film.id_film='" + label2.Text + "' AND  film.id_film=seans.id_film");
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

        public void kolbilet()
        {
            for (int i = 0; i < dataGridView1.RowCount - 1; i++) 
            {
                if (dataGridView1.Rows[i].Cells[4].Value.ToString() == "1")
                {
                    if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "куплен")
                    {
                        dataGridView11.Rows[Convert.ToInt16(dataGridView1.Rows[i].Cells[4].Value) - 1].Cells[Convert.ToInt16(dataGridView1.Rows[i].Cells[5].Value) +1].Style.BackColor = Color.Red;
                    }
                    if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "забронирован")
                    {
                        dataGridView11.Rows[Convert.ToInt16(dataGridView1.Rows[i].Cells[4].Value) - 1].Cells[Convert.ToInt16(dataGridView1.Rows[i].Cells[5].Value) +1].Style.BackColor = Color.Yellow;
                    }
                }
                if (dataGridView1.Rows[i].Cells[4].Value.ToString() == "2")
                {
                    if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "оплачен")
                    {
                        dataGridView11.Rows[Convert.ToInt16(dataGridView1.Rows[i].Cells[4].Value) - 1].Cells[Convert.ToInt16(dataGridView1.Rows[i].Cells[5].Value) ].Style.BackColor = Color.Red;
                    }
                    if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "забронирован")
                    {
                        dataGridView11.Rows[Convert.ToInt16(dataGridView1.Rows[i].Cells[4].Value) - 1].Cells[Convert.ToInt16(dataGridView1.Rows[i].Cells[5].Value) ].Style.BackColor = Color.Yellow;
                    }
                }
                if (dataGridView1.Rows[i].Cells[4].Value.ToString() != "2" && dataGridView1.Rows[i].Cells[4].Value.ToString() != "1")
                {
                    if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "оплачен")
                    {
                        dataGridView11.Rows[Convert.ToInt16(dataGridView1.Rows[i].Cells[4].Value) - 1].Cells[Convert.ToInt16(dataGridView1.Rows[i].Cells[5].Value) - 1].Style.BackColor = Color.Red;
                    }
                    if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "забронирован")
                    {
                        dataGridView11.Rows[Convert.ToInt16(dataGridView1.Rows[i].Cells[4].Value) - 1].Cells[Convert.ToInt16(dataGridView1.Rows[i].Cells[5].Value) - 1].Style.BackColor = Color.Yellow;
                    }
                }
            }
        }

        public void chistka ()
        {
            for (int i=0; i<dataGridView11.RowCount;i++)
                for (int j = 0; j < dataGridView11.ColumnCount; j++)
                {
                    dataGridView11.Rows[i].Cells[j].Style.BackColor = Color.White;
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 21; j++)
                {
                    if (dataGridView11.Rows[i].Cells[j].Selected == true && dataGridView11.Rows[i].Cells[j].Value != null)
                    {

                        if (dataGridView11.Rows[i].Cells[j].Style.BackColor == Color.Yellow)
                        {
                            if (MessageBox.Show("Билет забронирован, пометить его как купленный ?", "Подтверждение действия", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                con.Open();


                                int mesto = Convert.ToInt16(dataGridView11.Rows[i].Cells[j].Value.ToString());
                                int rad = i + 1;
                                cmd = new MySqlCommand("SELECT  DISTINCT mesto.id_mesto  FROM mesto WHERE mesto.rad='" + rad + "' and mesto.mesto='" + mesto + "'");
                                cmd.Connection = con;
                                da = new MySqlDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                BindingSource bSource = new BindingSource();
                                bSource.DataSource = dt;
                                label15.Text = dt.Rows[0]["id_mesto"].ToString().Trim();
                                da.Update(dt);
                                cmd = new MySqlCommand("SELECT  DISTINCT seansi.id_seansi  FROM seansi WHERE seansi.date='" + label3.Text + "' and seansi.time='" + label4.Text + "'");
                                cmd.Connection = con;
                                da = new MySqlDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                bSource.DataSource = dt;
                                dataGridView2.DataSource = bSource;
                                label9.Text = dt.Rows[0]["id_seansi"].ToString().Trim();
                                cmd = new MySqlCommand("Update bilet set id_sostojanie='2'  where bilet.id_mesto='" + Convert.ToInt16(label15.Text) + "' and bilet.id_seans='" + Convert.ToInt16(label9.Text) + "'");
                                cmd.Connection = con;
                                da = new MySqlDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                bSource.DataSource = dt;
                                da.Update(dt);
                                con.Close();

                                string name = label1.Text;
                                string time = label4.Text;
                                string[] datetime = label3.Text.Split(' ');
                                string date = datetime[0];
                                int cost = Convert.ToInt16(label11.Text);
                                ticket t = new ticket(name, time, date, cost, rad, mesto);
                                t.Show();

                                bileti();
                                kolbilet();
                                proverka = 1;
                            }
                        }
                        if (proverka != 1)
                        {
                            if (dataGridView11.Rows[i].Cells[j].Style.BackColor != Color.Red)
                            {
                                if (dataGridView11.Rows[i].Cells[j].Style.BackColor != Color.Yellow)
                                {
                                    con.Open();
                                    cmd = new MySqlCommand("SELECT DISTINCT seans.id_seans AS id_seansi FROM seans WHERE seans.data_pokaza='" + label3.Text + "' AND seans.time_seans='" + label4.Text + "'");
                                    cmd.Connection = con;
                                    da = new MySqlDataAdapter(cmd);
                                    dt = new DataTable();
                                    da.Fill(dt);
                                    BindingSource bSource = new BindingSource();
                                    bSource.DataSource = dt;
                                    dataGridView2.DataSource = bSource;
                                    label9.Text = dt.Rows[0]["id_seansi"].ToString().Trim();
                                    da.Update(dt);

                                    int mesto = Convert.ToInt16(dataGridView11.Rows[i].Cells[j].Value.ToString());
                                    int rad = i + 1;
                                    cmd = new MySqlCommand("SELECT  DISTINCT mesta.id_mesta  FROM mesta WHERE mesto.row='" + rad + "' AND mesta.mesta='" + mesto + "'");
                                    cmd.Connection = con;
                                    da = new MySqlDataAdapter(cmd);
                                    dt = new DataTable();
                                    da.Fill(dt);

                                    bSource.DataSource = dt;
                                    dataGridView2.DataSource = bSource;
                                    label10.Text = dt.Rows[0]["id_mesta"].ToString().Trim();
                                    da.Update(dt);

                                    /*cmd = new MySqlCommand("INSERT INTO bilet(id_seans,id_mesto,id_sostojanie) values ('" + Convert.ToInt16(label9.Text) + "','" + Convert.ToInt16(label10.Text) + "','2')");
                                    cmd.Connection = con;
                                    da = new MySqlDataAdapter(cmd);
                                    dt = new DataTable();
                                    da.Fill(dt);
                                    bSource.DataSource = dt;
                                    dataGridView2.DataSource = bSource;
                                    da.Update(dt);
                                    con.Close();*/

                                    string name = label1.Text;
                                    string time = label4.Text;
                                    string[] datetime = label3.Text.Split(' ');
                                    string date = datetime[0];
                                    int cost = Convert.ToInt16(label11.Text);
                                    ticket t = new ticket(name, time, date, cost, rad, mesto);
                                    t.Show();
                                }
                            }
                            else { MessageBox.Show("Билет уже куплен !"); }
                        }
                       
                    }
                }
            proverka = 0;
            bileti();
            kolbilet();
        
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
          
            if (select != 0)
            {
                chistka();
                dataGridView10.Rows[select].Selected = false;
                select--;
                dataGridView10.Rows[select].Selected = true;

                label3.Text = dataGridView10.Rows[select].Cells[1].Value.ToString();
                label4.Text = dataGridView10.Rows[select].Cells[2].Value.ToString();
                bileti();
                kolbilet();
            }
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
            if (select != dataGridView10.RowCount - 1)
            {
                
                dataGridView10.Rows[select].Selected = false;
                select++;
                if (select != dataGridView10.RowCount - 1)
                {
                    dataGridView10.Rows[select].Selected = true;

                    label3.Text = dataGridView10.Rows[select].Cells[1].Value.ToString();
                    label4.Text = dataGridView10.Rows[select].Cells[2].Value.ToString();
                    chistka();
                    bileti();
                    kolbilet();

                }
                else
                {
                    select--;
                    dataGridView10.Rows[select].Selected = true;

                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 21; j++)
                {
                    if (dataGridView11.Rows[i].Cells[j].Selected == true && dataGridView11.Rows[i].Cells[j].Value != null )
                    {
                        if (dataGridView11.Rows[i].Cells[j].Style.BackColor != Color.Yellow)
                        {
                            con.Open();
                            cmd = new MySqlCommand("SELECT  DISTINCT seans.id_seans FROM seans WHERE seans.data_pokaza='" + label3.Text + "' AND seans.time_seans='" + label4.Text + "'");
                            cmd.Connection = con;
                            da = new MySqlDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            BindingSource bSource = new BindingSource();
                            bSource.DataSource = dt;
                            dataGridView2.DataSource = bSource;
                            label9.Text = dt.Rows[0]["id_seansi"].ToString().Trim();
                            da.Update(dt);

                            int mesto = Convert.ToInt16(dataGridView11.Rows[i].Cells[j].Value.ToString());
                            int rad = i + 1;
                            cmd = new MySqlCommand("SELECT  DISTINCT mesto.id_mesto  FROM mesto WHERE mesto.rad='" + rad + "' AND mesto.mesto='" + mesto + "'");
                            cmd.Connection = con;
                            da = new MySqlDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            bSource.DataSource = dt;
                            dataGridView2.DataSource = bSource;
                            label10.Text = dt.Rows[0]["id_mesto"].ToString().Trim();
                            da.Update(dt);

                            cmd = new MySqlCommand("insert into bilet(id_seans,id_mesto,id_sostojanie) values ('" + Convert.ToInt16(label9.Text) + "','" + Convert.ToInt16(label10.Text) + "','1')");
                            cmd.Connection = con;
                            da = new MySqlDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            bSource.DataSource = dt;
                            dataGridView2.DataSource = bSource;
                            da.Update(dt);

                            con.Close();
                        }
                        else { MessageBox.Show("Билет уже забронирован!"); }
                    }
                    
                }
            bileti();
            kolbilet();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
             
        }

        public void id()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 21; j++)
                {
                    if (dataGridView11.Rows[i].Cells[j].Selected == true && dataGridView11.Rows[i].Cells[j].Value != null)
                    {
                        con.Open();
                        int mesto = Convert.ToInt16(dataGridView11.Rows[i].Cells[j].Value.ToString());
                        int rad = i + 1;
                        cmd = new MySqlCommand("SELECT  DISTINCT mesto.id_mesto  FROM mesto WHERE mesto.rad='" + rad + "' and mesto.mesto='" + mesto + "'");
                        cmd.Connection = con;
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        BindingSource bSource = new BindingSource();
                        bSource.DataSource = dt;
                        dataGridView2.DataSource = bSource;
                        label10.Text = dt.Rows[0]["id_mesto"].ToString().Trim();
                        da.Update(dt);
                        con.Close();
                    }
                }
        }

        private void Plan_Load(object sender, EventArgs e)
        {

        }
    }
}
