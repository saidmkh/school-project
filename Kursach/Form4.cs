using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


namespace Kursach
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        private string index;
        private string indexprovider; 

        private void закінчитиРоботуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            this.Height = 274;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                      "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;
            string sql = " UPDATE Персонал SET ПІБ = '" + textBox1.Text + "', Посада  = '" + textBox2.Text + "', Адреса  = '" + textBox3.Text + "' " +
                         " WHERE ПІБ = '" + indexprovider + "'";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();
            connection.Close();

            connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;
            sql = "SELECT ПІБ, Посада ,Адреса FROM Персонал";
            myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Механізм");
            dataGridView1.DataSource = ds.Tables["Механізм"].DefaultView;
            connection.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            
            
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            string ConnectionString1 = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                               "Data Source=kursovaya.mdb";
            OleDbConnection connection1 = new OleDbConnection();
            connection1.ConnectionString = ConnectionString1;

            string sql1 = "SELECT Товари.Найменування, Продажі.Кількість, Товари.Ціна, Продажі.Дата_продажу, Персонал.ПІБ, Продажі.код_чеку FROM Персонал INNER JOIN (Товари INNER JOIN Продажі ON Товари.код_товару = Продажі.код_товару) ON Персонал.код_персоналу = Продажі.Код_персоналу";
            OleDbCommand myCommand1 = new OleDbCommand(sql1, connection1);
            connection1.Open();

            OleDbDataAdapter da1 = new OleDbDataAdapter(myCommand1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1, "Продажі");
            dataGridView2.DataSource = ds1.Tables["Продажі"].DefaultView;
            connection1.Close();
           
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                    "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

            string sql = "SELECT ПІБ, Посада ,Адреса FROM Персонал";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Персонал");
            dataGridView1.DataSource = ds.Tables["Персонал"].DefaultView;
            connection.Close();
            for (int i = 0; i < dataGridView1.RowCount ; i++)
            {
                comboBox1.Items.Add(dataGridView1[0, i].Value.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

               

                if (textBox1.TextLength == 0 || textBox2.TextLength == 0 || textBox3.TextLength == 0)
                {
                    MessageBox.Show("Введіть дані");
                }
                else
                {
                    string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                     "Data Source=kursovaya.mdb";
                    OleDbConnection connection = new OleDbConnection();
                    connection.ConnectionString = ConnectionString;
                    OleDbCommand myCommand;
                    string sql;


                    string name = textBox1.Text;
                    string posada = textBox2.Text;
                    string adresa = textBox3.Text;


                    sql = " INSERT INTO Персонал ( ПІБ, Посада ,Адреса ) VALUES ('" + name + "', '" + posada + "', '" + adresa + "')";

                    myCommand = new OleDbCommand(sql, connection);
                    connection.Open();
                    myCommand.ExecuteNonQuery();
                    connection.Close();

                    connection = new OleDbConnection();
                    connection.ConnectionString = ConnectionString;
                    sql = "SELECT ПІБ, Посада ,Адреса FROM Персонал";
                    myCommand = new OleDbCommand(sql, connection);
                    connection.Open();
                    OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Персонал");
                    dataGridView1.DataSource = ds.Tables["Персонал"].DefaultView;
                    connection.Close();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
            }
            catch
            {
                textBox1.Focus();
            }
        }

        private void додаванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
           
        }

        private void редагуванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            groupBox1.Visible = true;
            groupBox1.Text = "Редагування";
            button2.Visible = true;
            button1.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                indexprovider = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch { }
         }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                indexprovider = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch { }
         }

        private void видаленняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                     "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;
            string sql = "DELETE * FROM Персонал WHERE ПІБ = '" + index + "'";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();
            connection.Close();

            connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;
            sql = "SELECT ПІБ, Посада ,Адреса FROM Персонал";
            myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Персонал");
            dataGridView1.DataSource = ds.Tables["Персонал"].DefaultView;
            connection.Close();
        }

        private void інформаціяПоПродажуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Height = 470;
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                               "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

            string sql = "SELECT Товари.Найменування, Продажі.Кількість, Товари.Ціна, Продажі.Дата_продажу, Персонал.ПІБ, Продажі.код_чеку FROM Персонал INNER JOIN (Товари INNER JOIN Продажі ON Товари.код_товару = Продажі.код_товару) ON Персонал.код_персоналу = Продажі.Код_персоналу";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Продажі");
            dataGridView2.DataSource = ds.Tables["Продажі"].DefaultView;
            connection.Close();


        }

        private void редагуванняToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            this.Height = 470;
            groupBox1.Text = "Редагування";
            button2.Visible = true;
            button1.Visible = false;
            this.Width = 835;
        }

        private void видаленняToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                    "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;
            string sql = "DELETE * FROM Персонал WHERE ПІБ = '" + index + "'";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();
            connection.Close();

            connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;
            sql = "SELECT ПІБ, Посада ,Адреса FROM Персонал";
            myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Персонал");
            dataGridView1.DataSource = ds.Tables["Персонал"].DefaultView;
            connection.Close();
        }

        private void додаванняToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            this.Width = 835;
            groupBox1.Text = "Додавання";
            button1.Visible = true;
            button2.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Height = 470;
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                               "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

            string sql = "SELECT Товари.Найменування, Продажі.Кількість, Товари.Ціна, Продажі.Дата_продажу, Персонал.ПІБ, Продажі.код_чеку From Товари, Продажі, Персонал, Чек Where Товари.код_товару = Продажі.код_товару and Персонал.код_персоналу = Продажі.код_персоналу and Чек.Код_чеку = Продажі.код_чеку and Персонал.ПІБ = '"+comboBox1.Text+"' ";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Продажі");
            dataGridView2.DataSource = ds.Tables["Продажі"].DefaultView;
            connection.Close();
            int balans = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                int incom;
                int.TryParse((row.Cells[2].Value ?? "0").ToString().Replace(".", ","), out incom);
                balans += incom;
            }
            label8.Text = balans.ToString();
            label6.Text = dataGridView2.Rows.Count.ToString();
        }

        private void закінчитиРоботуToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Width = 485;
        }
    }
}
