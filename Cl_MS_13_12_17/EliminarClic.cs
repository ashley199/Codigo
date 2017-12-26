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
    public partial class EliminarClic : Form
    {
        public EliminarClic()
        {
            InitializeComponent();
        }

        SqlConnection conex = new SqlConnection(ConfigurationManager.ConnectionStrings["DBNorthwind"].ConnectionString);

        private void EliminarClic_Load(object sender, EventArgs e)
        {
            SqlDataAdapter pedidos = new SqlDataAdapter("select * from orders", conex);
            DataSet DB = new DataSet();
            pedidos.Fill(DB, "ped");
            dataGridView1.DataSource = DB.Tables["ped"];
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            SqlDataAdapter pedidos = new SqlDataAdapter("select * from [order details] where orderId= @id ", conex);
            pedidos.SelectCommand.Parameters.Add("@id", SqlDbType.Int).Value = Int16.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            DataSet DB = new DataSet();
            pedidos.Fill(DB, "ped");
            dataGridView2.DataSource = DB.Tables["ped"];
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            int index = dataGridView2.CurrentRow.Index;
            DialogResult resp;
            resp = MessageBox.Show("ok", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resp == DialogResult.Yes)
            {
                SqlDataAdapter ord = new SqlDataAdapter("delete From [Order Details] where OrderID = @o and ProductID = @p", conex);
                ord.SelectCommand.Parameters.Add("@o", SqlDbType.Int).Value = dataGridView2.Rows[index].Cells[0].Value;
                ord.SelectCommand.Parameters.Add("@p", SqlDbType.Int).Value = dataGridView2.Rows[index].Cells[1].Value;

                conex.Open();
                /*int ok = ord.ExecuteNonQuery();
                conex.Close();

                if (ok == 1)
                {
                    MessageBox.Show("Registro Borado...");
                }*/
            }
        }
    }
}
