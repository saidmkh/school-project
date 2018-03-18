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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
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
            dataGridView1.DataSource = ds.Tables["Продажі"].DefaultView;
            connection.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
