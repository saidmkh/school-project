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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private string index;
        private string indexprovider; 

        private void Form3_Load(object sender, EventArgs e)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                     "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

            string sql = "SELECT Назва_механізму, Опис FROM Механізм";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Механізм");
            dataGridView1.DataSource = ds.Tables["Механізм"].DefaultView;
            connection.Close();

            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                indexprovider = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                label2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            catch { }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                indexprovider = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                label2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            catch { }
        }

        private void label2_Click(object sender, EventArgs e)
        {

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


                string name = textBox2.Text;
                string opis = textBox3.Text;


                sql = " INSERT INTO Механізм ( Назва_механізму, Опис ) VALUES ('" + name + "', '" + opis + "')";

                myCommand = new OleDbCommand(sql, connection);
                connection.Open();
                myCommand.ExecuteNonQuery();
                connection.Close();

                connection = new OleDbConnection();
                connection.ConnectionString = ConnectionString;
                sql = "SELECT  Назва_механізму, Опис FROM Механізм";
                myCommand = new OleDbCommand(sql, connection);
                connection.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
                DataSet ds = new DataSet();
                da.Fill(ds, "Механізм");
                dataGridView1.DataSource = ds.Tables["Механізм"].DefaultView;
                connection.Close();
            }
            catch
            {
                textBox2.Focus();
            }
        }

        private void додаванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            this.Height = 570;
            groupBox1.Visible = true;
            groupBox1.Text = "Додавання";
            button1.Visible = true;
            button2.Visible = false;
            
        }

        private void редагуванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            this.Height = 570;
            groupBox1.Visible = true;
            groupBox1.Text = "Редагування";
            button2.Visible = true;
            button1.Visible = false;
        }

        private void закінчитиОброблюванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            this.Height = 275;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                      "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;
            string sql = " UPDATE Механізм SET Назва_механізму = '" + textBox2.Text + "', Опис  = '" + textBox3.Text + "' " +
                         " WHERE Назва_механізму = '" + indexprovider + "'";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();
            connection.Close();

            connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;
            sql = "SELECT  Назва_механізму, Опис FROM Механізм";
            myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Механізм");
            dataGridView1.DataSource = ds.Tables["Механізм"].DefaultView;
            connection.Close();
        }

        private void видаленняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                     "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;
            string sql = "DELETE * FROM Механізм WHERE Назва_механізму = '" + index + "'";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();
            connection.Close();

            connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;
            sql = "SELECT  Назва_механізму, Опис FROM Механізм";
            myCommand = new OleDbCommand(sql, connection);
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Механізм");
            dataGridView1.DataSource = ds.Tables["Механізм"].DefaultView;
            connection.Close();
        }
    }
}
