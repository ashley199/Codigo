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
    public partial class IAE_Menu_Emp01 : Form
    {
        public IAE_Menu_Emp01()
        {
            InitializeComponent();
        }

        SqlConnection conex = new SqlConnection(ConfigurationManager.ConnectionStrings["DBNorthwind"].ConnectionString);

        public int id { get; set;}
        public string ap { get; set;}
        public string nom { get; set;}
        public string dir { get; set;}

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand sql = new SqlCommand("update Employees set LastName = @a, FirstName = @n, Address = @d where EmployeeID = @e", conex);
            sql.Parameters.Add("@e", SqlDbType.Int).Value = textBox1.Text;
            sql.Parameters.Add("@a", SqlDbType.VarChar, 20).Value = textBox2.Text;
            sql.Parameters.Add("@n", SqlDbType.VarChar, 10).Value = textBox3.Text;
            sql.Parameters.Add("@d", SqlDbType.VarChar, 60).Value = textBox4.Text;
            conex.Open();
            int ok = sql.ExecuteNonQuery();
            conex.Close();
            if (ok == 1)
            {
                MessageBox.Show("Registro Actualizado...");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error de actualización!!!");
                this.Close();
            }
        }

        private void IAE_Menu_Emp01_Load(object sender, EventArgs e)
        {
            textBox1.Text = this.id.ToString();
            textBox2.Text = this.ap;
            textBox3.Text = this.nom;
            textBox4.Text = this.dir;
        }
    }
}
