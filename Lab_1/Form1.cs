using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lab_2_Example
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        DataSet ds;
        SqlDataAdapter daClients, daOrders;
        SqlCommandBuilder cbOrders;
        BindingSource bsClients, bsOrders;

        private void saveDataButton_Click(object sender, EventArgs e)
        {
            daOrders.Update(ds, "Orders");
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=DESKTOP-OFELIA;Initial Catalog=Sem2_Tennis_Table;Integrated Security=True");
            ds = new DataSet();
            daClients = new SqlDataAdapter("SELECT * FROM Clients", conn);
            daOrders = new SqlDataAdapter("SELECT * FROM Orders", conn);
            cbOrders = new SqlCommandBuilder(daOrders);

            daClients.Fill(ds, "Clients");
            daOrders.Fill(ds, "Orders");

            Console.WriteLine(ds.Tables["Clients"].Constraints.Count);
            Console.WriteLine(ds.Tables["Orders"].Constraints.Count);

            DataRelation dr = new DataRelation("FK_Orders_Clients", ds.Tables["Clients"].Columns["CID"],
                ds.Tables["Orders"].Columns["CID"]);

            ds.Relations.Add(dr);

            Console.WriteLine(ds.Tables["Clients"].Constraints[0].GetType());
            Console.WriteLine(ds.Tables["Orders"].Constraints[0].GetType());

            // UniqueConstraint, ForeignKeyConstraint
            // GetChildRows, GetParentRow

            bsClients = new BindingSource();
            bsClients.DataSource = ds;
            bsClients.DataMember = "Clients";
            
            bsOrders = new BindingSource();
            bsOrders.DataSource = bsClients;
            bsOrders.DataMember = "FK_Orders_Clients";

            dgvClients.DataSource = bsClients;
            dgvOrders.DataSource = bsOrders;

        }
    }
}
