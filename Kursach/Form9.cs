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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
           
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " +
                                     "Data Source=kursovaya.mdb";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConnectionString;

            string sql = "SELECT Товари.[Найменування], Товари.[Бренд], Товари.[Ціна] FROM Товари";

            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            connection.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(myCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "Товари");
            ds.WriteXmlSchema("schema7.xml");

            connection.Close();
            CrystalReport2 rtp = new CrystalReport2();
            rtp.SetDataSource(ds);
            crystalReportViewer1.ReportSource = rtp;
            this.crystalReportViewer1.RefreshReport();
        }
    }
}
