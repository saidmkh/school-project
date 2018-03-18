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
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
        }

        private string index;
        private string indexprovider; 

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
     

        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
            string ConnectionString1 = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                          "Data Source=kursovaya.mdb";
            OleDbConnection connection1 = new OleDbConnection();
            connection1.ConnectionString = ConnectionString1;
            OleDbCommand myCommand1;
            string sql1;

            sql1 = " SELECT *  FROM Механізм";

            myCommand1 = new OleDbCommand(sql1, connection1);
            connection1.Open();
            myCommand1.ExecuteNonQuery();
            connection1.Close();
            OleDbDataAdapter da1 = new OleDbDataAdapter(myCommand1);
            DataSet dss = new DataSet();
            da1.Fill(dss, "Механізм");
            dataGridView2.DataSource = dss.Tables["Механізм"].DefaultView;
            connection1.Close();

            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                comboBox2.Items.Add(dataGridView2[1, i].Value.ToString());
                comboBox1.Items.Add(dataGridView2[1, i].Value.ToString());
            }


            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                      "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

            string sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Краіна_виготовлювач, Товари.Код_товару  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товари");
            dataGridView1.DataSource = ds.Tables["Товари"].DefaultView;
            connection.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                      "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

            string sql = "SELECT * FROM Товари where Найменування like '" + textBox1.Text + "'";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товари");
            dataGridView1.DataSource = ds.Tables["Товари"].DefaultView;
            connection.Close();
        }

        private void додаванняТоваруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox2.Text = "";
            label1.Text = "";
            this.Width = 900;
            button1.Visible = true;
            button2.Visible = false;
            button1.Text = "Додати";
            groupBox1.Text = "Додавання товару";
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            
                    index = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    indexprovider = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();



                
               

            }
            catch { }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                index = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                indexprovider = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                
            }
            catch { }
        }

        private void видаленняТоваруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                      "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;
            string sql = "DELETE * FROM Товари WHERE Найменування = '" + index + "'";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();
            connection.Close();

            connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;
            sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Краіна_виготовлювач, Товари.Код_товару  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму";
            myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товари");
            dataGridView1.DataSource = ds.Tables["Товари"].DefaultView;
            connection.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                          "Data Source=kursovaya.mdb";
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = ConnectionString;
                OleDbCommand myCommand;
                string sql;

                string meh = label1.Text;
                string price = textBox6.Text;
                string name = textBox4.Text;
                string brend = textBox7.Text;
                string cntr = textBox2.Text;
                string photo = textBox5.Text;


                sql = " INSERT INTO Товари ( Найменування, Код_механізму, Ціна, Бренд, Краіна_виготовлювач, Фото ) VALUES ('" + name + "', " + meh + ", " + price + ", '" + brend + "', '" + cntr + "', '" + photo + "')";

                myCommand = new OleDbCommand(sql, connection);
                connection.Open();
                myCommand.ExecuteNonQuery();
                connection.Close();

                connection = new OleDbConnection();
                connection.ConnectionString = ConnectionString;
                sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Краіна_виготовлювач, Товари.Код_товару  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму";
                myCommand = new OleDbCommand(sql, connection);
                connection.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
                DataSet ds = new DataSet();
                da.Fill(ds, "Товари");
                dataGridView1.DataSource = ds.Tables["Товари"].DefaultView;
                connection.Close();
            }
            catch
            {
               MessageBox.Show("Введіть правильно данні");
            }
        }

        private void редагуванняТоваруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox2.Text = "";
            label1.Text = "";
            this.Width = 900;
            button1.Visible = false;
            button2.Visible = true;
            button2.Text = "Редагувати";
            groupBox1.Text = "Редагування товару";
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                          "Data Source=kursovaya.mdb";
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = ConnectionString;
                string sql = " UPDATE Товари SET Найменування = '" + textBox4.Text + "', Код_механізму = " + label1.Text + ", Ціна  = " + textBox6.Text + " , Бренд  = '" + textBox7.Text + "', Фото = '" + textBox5.Text + "', Краіна_виготовлювач = '" + textBox2.Text + "'  WHERE Найменування = '" + indexprovider + "'";
                OleDbCommand myCommand = new OleDbCommand(sql, connection);
                connection.Open();
                myCommand.ExecuteNonQuery();
                connection.Close();

                connection = new OleDbConnection();
                connection.ConnectionString = ConnectionString;
                sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Краіна_виготовлювач, Товари.Код_товару  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму";
                myCommand = new OleDbCommand(sql, connection);
                connection.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
                DataSet ds = new DataSet();
                da.Fill(ds, "Товари");
                dataGridView1.DataSource = ds.Tables["Товари"].DefaultView;
                connection.Close();
            }
            catch { MessageBox.Show("Введіть правильно дані"); }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                     "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;


            string sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Код_товару  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму and Товари.Найменування like '%" + textBox1.Text + "%' ";

            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товары");
            dataGridView1.DataSource = ds.Tables["Товары"].DefaultView;
            connection.Close();

        }

        private void закінчитиОброблюванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox2.Text = "";
            label1.Text = "";
            this.Width = 572;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = dataGridView2[0, comboBox2.SelectedIndex].Value.ToString();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                    "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

           
            string sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Код_товару  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму and Механізм = '" + comboBox1.Text + "' ";

            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товары");
            dataGridView1.DataSource = ds.Tables["Товары"].DefaultView;
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                   "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

            
            string sql = "SELECT Товари.Найменування, Механізм.Назва_механізму, Товари.Ціна, Товари.Бренд, Товари.Код_товару  FROM Товари, Механізм where Механізм.Код_механізму = Товари.Код_механізму and Бренд like '%" + textBox3.Text + "%' ";

            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товары");
            dataGridView1.DataSource = ds.Tables["Товары"].DefaultView;
            connection.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = dataGridView2[0, comboBox2.SelectedIndex].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath + "pic/";
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pict = openFileDialog1.SafeFileName;
            path = openFileDialog1.FileName;
            textBox5.Text = pict;

            Directory.SetCurrentDirectory(Application.StartupPath); 
        }

        public string pict { get; set; }

        public string path { get; set; }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form9 f = new Form9();
            f.Show();
        }
    }
}
