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
using System.Configuration;

namespace Cl_MS_13_12_17
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conex = new SqlConnection(ConfigurationManager.ConnectionStrings["DBNorthwind"].ConnectionString);

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.ValueMember.Equals("EmployeeID"))
            {
                SqlDataAdapter datos = new SqlDataAdapter("select * from Products where CategoryID=@id", conex);
                datos.SelectCommand.Parameters.Add("@id", SqlDbType.Int).Value = listBox1.SelectedValue;

                DataSet DB = new DataSet();
                datos.Fill(DB, "Clientes");
                dataGridView1.DataSource = DB.Tables["Clientes"];
                //textBox1.Text = listBox1.SelectedValue.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlDataAdapter Emple = new SqlDataAdapter("select * from Categories", conex);
            DataSet DB = new DataSet();
            Emple.Fill(DB, "paises");
            listBox1.DataSource = DB.Tables[0];
            listBox1.ValueMember = "Categoryid";
            listBox1.DisplayMember = "categoryname";  //El valor que se muestra en el listbo
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            SqlDataAdapter pedidos = new SqlDataAdapter("select * from [order details] where ProductID= @id ", conex);
            pedidos.SelectCommand.Parameters.Add("@id", SqlDbType.Int).Value = Int16.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            DataSet DB = new DataSet();

            //DataSet DB = new DataSet();
            pedidos.Fill(DB, "ped");
            dataGridView2.DataSource = DB.Tables["ped"];
            // label2.Text = "Producto " + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            label3.Text = "El producto  " + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + " ordenes";
        }
    }
}
