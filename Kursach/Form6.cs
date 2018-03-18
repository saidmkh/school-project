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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        private string index;
        private void Form6_Load(object sender, EventArgs e)
        {
            try
            {
                string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                         "Data Source=kursovaya.mdb";
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = ConnectionString;

                string sql = "SELECT *  FROM Чек";
                OleDbCommand myCommand = new OleDbCommand(sql, connection);
                connection.Open();

                OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
                DataSet ds = new DataSet();
                da.Fill(ds, "Чек");
                dataGridView1.DataSource = ds.Tables["Чек"].DefaultView;
                connection.Close();
            }
            catch { }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                  "Data Source=kursovaya.mdb";
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = ConnectionString;

                string sql = "SELECT Товари.Найменування, Продажі.Кількість, Товари.Ціна, Продажі.Дата_продажу, Персонал.ПІБ, Продажі.код_чеку From Товари, Продажі, Персонал, Чек Where Товари.код_товару = Продажі.код_товару and Персонал.код_персоналу = Продажі.код_персоналу and Чек.Код_чеку = Продажі.код_чеку and Чек.Код_чеку = " + index + " ";
                OleDbCommand myCommand = new OleDbCommand(sql, connection);
                connection.Open();

                OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
                DataSet ds = new DataSet();
                da.Fill(ds, "Продажі");
                dataGridView2.DataSource = ds.Tables["Продажі"].DefaultView;
                connection.Close();
            }
            catch { }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
