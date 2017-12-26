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
    public partial class INS_Emple : Form
    {
        public INS_Emple()
        {
            InitializeComponent();
        }

        SqlConnection conex = new SqlConnection(ConfigurationManager.ConnectionStrings["DBNorthwind"].ConnectionString);

        private void INS_Emple_Load(object sender, EventArgs e)
        {

        }

        void actualizar_gridview()
        {
            SqlDataAdapter datos = new SqlDataAdapter("select * from Employees", conex);
            DataSet DB = new DataSet();
            datos.Fill(DB, "Emp");
            dataGridView1.DataSource = DB.Tables["Emp"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand sql = new SqlCommand("insert into Employees (LastName, FirstName, Address, City) values (@a, @n, @d, @c)", conex);
            sql.Parameters.Add("@a", SqlDbType.VarChar, 20).Value = textBox1.Text;
            sql.Parameters.Add("@n", SqlDbType.VarChar, 10).Value = textBox2.Text;
            sql.Parameters.Add("@d", SqlDbType.VarChar, 60).Value = textBox3.Text;
            sql.Parameters.Add("@c", SqlDbType.VarChar, 15).Value = comboBox1.SelectedItem;
            conex.Open();
            int ok = sql.ExecuteNonQuery();
            conex.Close();
            if (ok == 1)
            {
                MessageBox.Show("Registro Grabado...");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox1.Focus();
                actualizar_gridview();
            }
        }
    }
}
