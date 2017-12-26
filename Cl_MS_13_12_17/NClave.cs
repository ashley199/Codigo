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
    public partial class NClave : Form
    {
        public NClave()
        {
            InitializeComponent();
        }

        SqlConnection conex = new SqlConnection(ConfigurationManager.ConnectionStrings["DBNorthwind"].ConnectionString);

        void usuario()
        {
            SqlCommand query = new SqlCommand("select count(*) from Usuarios where Usuario = @u and Password = @p", conex);
            query.Parameters.Add("@u", SqlDbType.VarChar, 50).Value = textBox1.Text;
            query.Parameters.Add("@p", SqlDbType.VarChar, 50).Value = textBox2.Text;
            conex.Open();
            int existe = int.Parse(query.ExecuteScalar().ToString());
            conex.Close();

            if (existe == 1)
            {
                if (textBox3.Text.Equals(textBox2.Text))
                {
                    MessageBox.Show("Ingrese nueva Clave!!!");
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox3.Focus();
                }
                else
                {
                    SqlCommand sql = new SqlCommand("update Usuarios set Password = @p where Usuario = @u", conex);

                    sql.Parameters.Add("@p", SqlDbType.NVarChar, 50).Value = textBox4.Text;
                    sql.Parameters.Add("@u", SqlDbType.NVarChar, 50).Value = textBox1.Text;
                    conex.Open();
                    int ok = sql.ExecuteNonQuery();
                    conex.Close();
                    if (ok == 1)
                    {
                        MessageBox.Show("Clave Modificada...");
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Usuario incorrecto!!!");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox1.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usuario();
        }
    }
}
