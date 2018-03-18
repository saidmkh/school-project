using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;


namespace Kursach
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string index;
        public int n;
        public int sum, sum2;
        public int dbsum, dbsum2;
        public string chek;
        public string prod;
        public int prod2;

        private void товарівToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
        }

        private void каToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
        }

        private void працівниківToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Show();
        }

        private void продажToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            f.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            textBox5.ForeColor = Color.Silver;
            textBox5.Text = "назва...";
            comboBox2.ForeColor = Color.Silver;
            comboBox2.Text = "бренд...";
            comboBox3.ForeColor = Color.Silver;
            comboBox3.Text = "механізм...";

            button3.Visible = false;
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kursovayaDataSet.Персонал". При необходимости она может быть перемещена или удалена.
        //   this.персоналTableAdapter.Fill(this.kursovayaDataSet.Персонал);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kursovayaDataSet.Чек". При необходимости она может быть перемещена или удалена.
          //  this.чекTableAdapter.Fill(this.kursovayaDataSet.Чек);

            string ConnectionString1 = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                         "Data Source=kursovaya.mdb";
            OleDbConnection connection1 = new OleDbConnection();
            connection1.ConnectionString = ConnectionString1;
            OleDbCommand myCommand1;
            string sql1;

            sql1 = " SELECT *  FROM Персонал";

            myCommand1 = new OleDbCommand(sql1, connection1);
            connection1.Open();
            myCommand1.ExecuteNonQuery();
            connection1.Close();
            OleDbDataAdapter da1 = new OleDbDataAdapter(myCommand1);
            DataSet dss = new DataSet();
            da1.Fill(dss, "Персонал");
            dataGridView4.DataSource = dss.Tables["Персонал"].DefaultView;
            connection1.Close();

            for (int i = 0; i < dataGridView4.RowCount - 1; i++)
            {
                comboBox1.Items.Add(dataGridView4[1, i].Value.ToString());
            }


            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                      "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

            string sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Краіна_виготовлювач, Товари.Код_товару, Товари.Фото  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товари");
            dataGridView1.DataSource = ds.Tables["Товари"].DefaultView;
            connection.Close();

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label12.Text = "0";
            button1.Enabled = false;
            comboBox1.Enabled = false;
            button3.Visible = true;
            this.Width = 850;
            groupBox1.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            dateTimePicker1.Visible = true;

            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                   "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

            
            string sql = "SELECT max(Код_чеку) as [Chek]   FROM Чек";

            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Чек");
            dataGridView3.DataSource = ds.Tables["Чек"].DefaultView;
            connection.Close();

           

            chek = (Convert.ToInt32(dataGridView3[0, 0].Value) + 1).ToString();
            label2.Text = chek.ToString();

            groupBox1.Enabled = true;
            string ConnectionString1 = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                      "Data Source=kursovaya.mdb";
            OleDbConnection connection1 = new OleDbConnection();
            connection1.ConnectionString = ConnectionString1;
            OleDbCommand myCommand1;
            string sql1;

            sql1 = " INSERT INTO Чек ( Дата_продажу, сума ) VALUES (" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + ", " + label12.Text + ")";

            myCommand1 = new OleDbCommand(sql1, connection1);
            connection1.Open();
            myCommand1.ExecuteNonQuery();
            connection1.Close();

            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label10.Text = (Convert.ToInt32(label6.Text) * Convert.ToInt32(numericUpDown1.Value)).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                     "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

             
            string sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Код_товару  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму and Товари.Найменування like '%" + textBox5.Text + "%' and Механізм.Назва_механізму like '%"+ comboBox3.Text +"%' and Товари.Бренд like '%"+ comboBox2.Text +"%'";

            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товары");
            dataGridView1.DataSource = ds.Tables["Товары"].DefaultView;
            connection.Close();
            textBox5.ForeColor = Color.Silver;
            textBox5.Text = "назва...";
            comboBox2.ForeColor = Color.Silver;
            comboBox2.Text = "бренд...";
            comboBox3.ForeColor = Color.Silver;
            comboBox3.Text = "механізм...";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                     "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

             
            string sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Код_товару  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму and Механізм.Назва_механізму like '%" + comboBox3.Text + "%' ";

            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товары");
            dataGridView1.DataSource = ds.Tables["Товары"].DefaultView;
            connection.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                  "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

             
            string sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Код_товару  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму and Товари.Бренд like '%" + comboBox2.Text + "%' ";

            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товары");
            dataGridView1.DataSource = ds.Tables["Товары"].DefaultView;
            connection.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                numericUpDown1.Value = 1;
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                label6.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                label13.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
            catch{ }
           
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {


            try
            {
                
                string ConnectionString1 = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                 "Data Source=kursovaya.mdb";
                OleDbConnection connection1 = new OleDbConnection();
                connection1.ConnectionString = ConnectionString1;


                string sql1 = "SELECT max(Код_продажу) as [Chek]   FROM Продажі";

                OleDbCommand myCommand1 = new OleDbCommand(sql1, connection1);
                connection1.Open();
                OleDbDataAdapter da1 = new OleDbDataAdapter(myCommand1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "Продажі");
                dataGridView5.DataSource = ds1.Tables["Продажі"].DefaultView;
                connection1.Close();

                if (numericUpDown1.Value == 0)
                {
                    MessageBox.Show("Введіть кількість");
                }
                else
                {
                    string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                              "Data Source=kursovaya.mdb";
                    OleDbConnection connection = new OleDbConnection();
                    connection.ConnectionString = ConnectionString;
                    OleDbCommand myCommand;
                    string sql;

                    sql = " INSERT INTO Продажі ( Дата_продажу, Кількість, код_чеку, код_товару, Код_персоналу, Ціна ) VALUES (" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + ", " + numericUpDown1.Value + ", " + label2.Text + ", " + label13.Text + ", " + label8.Text + ", " + label10.Text + " )";


                    myCommand = new OleDbCommand(sql, connection);
                    connection.Open();
                    myCommand.ExecuteNonQuery();
                    connection.Close();



                    string ConnectionString2 = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                            "Data Source=kursovaya.mdb";
                    OleDbConnection connection2 = new OleDbConnection();
                    connection2.ConnectionString = ConnectionString2;
                    string sql2 = " UPDATE Чек SET Дата_продажу =" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + ", сума = " + label12.Text + " where Код_чеку = " + label2.Text + " ";
                    OleDbCommand myCommand2 = new OleDbCommand(sql2, connection2);
                    connection2.Open();
                    myCommand2.ExecuteNonQuery();
                    connection2.Close();

                    dataGridView2.Rows.Add(textBox1.Text, numericUpDown1.Value, label10.Text);


                    int balans = 0;
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        int incom;
                        int.TryParse((row.Cells[2].Value ?? "0").ToString().Replace(".", ","), out incom);
                        balans += incom;
                    }
                    label12.Text = balans.ToString();
                    label17.Text = dataGridView5[0, 0].Value.ToString();
                }

            }

            catch
            {
                MessageBox.Show("Виберіть товар");
            }

          
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void чекToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            f.Show();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                label10.Text = (Convert.ToInt32(label6.Text) * Convert.ToInt32(numericUpDown1.Value)).ToString();
            }
            catch 
            {
                numericUpDown1.Value = 1;
                MessageBox.Show("Виберіть товар");
            }

        }

        private void label6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                label10.Text = (Convert.ToInt32(label6.Text) * Convert.ToInt32(numericUpDown1.Value)).ToString();
            }
            catch { label12.Text = ""; }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label12_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {

            button1.Enabled = true;
             string ConnectionString1 = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                    "Data Source=kursovaya.mdb";
             OleDbConnection connection1 = new OleDbConnection();
             connection1.ConnectionString = ConnectionString1;
             string sql1 = " UPDATE Чек SET Дата_продажу =" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + ", сума = " + label12.Text + " where Код_чеку = " + label2.Text + " ";
             OleDbCommand myCommand1 = new OleDbCommand(sql1, connection1);
             connection1.Open();
             myCommand1.ExecuteNonQuery();
             connection1.Close();

             //dataGridView2.Rows.Add(textBox1.Text, numericUpDown1.Value, label10.Text);
             label12.Text = "0" ;
             textBox1.Text = "";
             label8.Text = "";
             numericUpDown1.Value = 1;
             dataGridView2.Rows.Clear();
             label6.Text = "";
             label10.Text = "";
             label2.Text = "";
             this.Width = 475;
             button1.Enabled = false;
             groupBox1.Visible = false;

             comboBox1.Enabled = true;
             comboBox1.Text = "";
             Form8 f1 = new Form8();
             f1.Show();

            
        }

        private void label12_Click(object sender, EventArgs e)
        {
          
        }

        private void dataGridView2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
              try
            {

                int ind = dataGridView2.SelectedCells[0].RowIndex;
                dataGridView2.Rows.RemoveAt(ind);
                string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                              "Data Source=kursovaya.mdb";
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = ConnectionString;
                OleDbCommand myCommand;
                string sql;

                sql = " Delete  * from Продажі where Код_продажу = " + label17.Text + "";


                myCommand = new OleDbCommand(sql, connection);
                connection.Open();
                myCommand.ExecuteNonQuery();
                connection.Close();
            }

              catch { MessageBox.Show("Корзина порожня"); }

            string ConnectionString1 = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                             "Data Source=kursovaya.mdb";
            OleDbConnection connection1 = new OleDbConnection();
            connection1.ConnectionString = ConnectionString1;


            string sql1 = "SELECT max(Код_продажу) as [Chek]   FROM Продажі";

            OleDbCommand myCommand1 = new OleDbCommand(sql1, connection1);
            connection1.Open();
            OleDbDataAdapter da1 = new OleDbDataAdapter(myCommand1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1, "Продажі");
            dataGridView5.DataSource = ds1.Tables["Продажі"].DefaultView;
            connection1.Close();
            label17.Text = dataGridView5[0, 0].Value.ToString();
        
            int balans = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                int incom;
                int.TryParse((row.Cells[2].Value ?? "0").ToString().Replace(".", ","), out incom);
                balans += incom;
            }
            label12.Text = balans.ToString();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            index = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void comboBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            label8.Text = dataGridView4[0, comboBox1.SelectedIndex].Value.ToString();

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void dataGridView2_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
       
        }

        private void dataGridView2_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox5.ForeColor = Color.Silver;
            textBox5.Text = "назва...";
            comboBox2.ForeColor = Color.Silver;
            comboBox2.Text = "бренд...";
            comboBox3.ForeColor = Color.Silver;
            comboBox3.Text = "механізм...";
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                      "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

            string sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Код_товару  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товари");
            dataGridView1.DataSource = ds.Tables["Товари"].DefaultView;
            connection.Close();
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                     "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;


            string sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Код_товару  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму and Товари.Найменування like '%" + textBox5.Text + "%'";

            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товары");
            dataGridView1.DataSource = ds.Tables["Товары"].DefaultView;
            connection.Close();
        }

        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            
            textBox5.ForeColor = Color.Black;
            textBox5.Text = "";
            comboBox3.ForeColor = Color.Silver;
            comboBox3.Text = "механізм...";
            comboBox2.ForeColor = Color.Silver;
            comboBox2.Text = "бренд...";
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                      "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

            string sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Краіна_виготовлювач, Товари.Код_товару, Товари.Фото  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товари");
            dataGridView1.DataSource = ds.Tables["Товари"].DefaultView;
            connection.Close();
            
        }

        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
          
            comboBox3.ForeColor = Color.Black;
            comboBox3.Text = "";
            comboBox2.ForeColor = Color.Silver;
            comboBox2.Text = "бренд...";
            textBox5.ForeColor = Color.Silver;
            textBox5.Text = "назва...";
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                    "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

            string sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Краіна_виготовлювач, Товари.Код_товару, Товари.Фото  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товари");
            dataGridView1.DataSource = ds.Tables["Товари"].DefaultView;
            connection.Close();

        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            
            comboBox2.ForeColor = Color.Black;
            comboBox2.Text = "";
            textBox5.ForeColor = Color.Silver;
            textBox5.Text = "назва...";
            
            comboBox3.ForeColor = Color.Silver;
            comboBox3.Text = "механізм...";
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                      "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

            string sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Краіна_виготовлювач, Товари.Код_товару, Товари.Фото  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товари");
            dataGridView1.DataSource = ds.Tables["Товари"].DefaultView;
            connection.Close();

        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                     "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;


            string sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Код_товару  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму and Механізм.Назва_механізму like '%" + comboBox3.Text + "%'";

            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товары");
            dataGridView1.DataSource = ds.Tables["Товары"].DefaultView;
            connection.Close();
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                     "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;


            string sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Код_товару  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму and Товари.Бренд like '%" + comboBox2.Text + "%'";

            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товары");
            dataGridView1.DataSource = ds.Tables["Товары"].DefaultView;
            connection.Close();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pict = openFileDialog1.SafeFileName;
            path = openFileDialog1.FileName;
         
            Directory.SetCurrentDirectory(Application.StartupPath); //установка рабочей папки чтобы не сбивалась база
           
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath + "pic/";
            openFileDialog1.ShowDialog();
        }

    

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                numericUpDown1.Value = 1;
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                label6.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                label13.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                label14.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            catch { }
           
        }

        public string clikb { get; set; }

        public string pict { get; set; }

        public string path { get; set; }

        private void textBox5_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
             try
            {

                int ind = dataGridView2.SelectedCells[0].RowIndex;
                dataGridView2.Rows.RemoveAt(ind);
                string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                              "Data Source=kursovaya.mdb";
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = ConnectionString;
                OleDbCommand myCommand;
                string sql;

                sql = " Delete  * from Продажі where Код_продажу = " + label17.Text + "";


                myCommand = new OleDbCommand(sql, connection);
                connection.Open();
                myCommand.ExecuteNonQuery();
                connection.Close();
            }

              catch { MessageBox.Show("Корзина порожня"); }

            string ConnectionString1 = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                             "Data Source=kursovaya.mdb";
            OleDbConnection connection1 = new OleDbConnection();
            connection1.ConnectionString = ConnectionString1;


            string sql1 = "SELECT max(Код_продажу) as [Chek]   FROM Продажі";

            OleDbCommand myCommand1 = new OleDbCommand(sql1, connection1);
            connection1.Open();
            OleDbDataAdapter da1 = new OleDbDataAdapter(myCommand1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1, "Продажі");
            dataGridView5.DataSource = ds1.Tables["Продажі"].DefaultView;
            connection1.Close();
            label17.Text = dataGridView5[0, 0].Value.ToString();
        
            int balans = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                int incom;
                int.TryParse((row.Cells[2].Value ?? "0").ToString().Replace(".", ","), out incom);
                balans += incom;
            }
            label12.Text = balans.ToString();
        }
        }
    }

