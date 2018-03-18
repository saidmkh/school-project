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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.label1.Text = f.label2.Text;
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                     "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

            string sql = "SELECT top 1 Товари.Найменування, Продажі.Кількість, Продажі.Ціна, Персонал.ПІБ, Продажі.Дата_продажу, Чек.сума, Чек.Код_чеку FROM Чек INNER JOIN (Товари INNER JOIN (Персонал INNER JOIN Продажі ON Персонал.код_персоналу = Продажі.Код_персоналу) ON Товари.код_товару = Продажі.код_товару) ON Чек.Код_чеку = Продажі.код_чеку ORDER BY Чек.Код_чеку DESC";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товари");
            ds.WriteXmlSchema("schema4.xml");
          
            connection.Close();
            CrystalReport1 rtp = new CrystalReport1();
            rtp.SetDataSource(ds);
            crystalReportViewer1.ReportSource = rtp;
            this.crystalReportViewer1.RefreshReport();
        }
    }
}
