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
    public partial class REG_Estudiante : Form
    {
        public REG_Estudiante()
        {
            InitializeComponent();
        }

        SqlConnection conex = new SqlConnection(ConfigurationManager.ConnectionStrings["DBNorthwind"].ConnectionString);

        void insertar()
        {
            SqlCommand sql = new SqlCommand("insert into Alumnos (Nombres, Apellidos, Dirección, Ciudad, Escuela_Prof, DNI, Sunedu, Sexo) values (@n, @a, @d, @c, @e, @dn, @s,@sx)", conex);
            sql.Parameters.Add("@n", SqlDbType.VarChar, 25).Value = textBox1.Text;
            sql.Parameters.Add("@a", SqlDbType.VarChar, 30).Value = textBox2.Text;
            sql.Parameters.Add("@d", SqlDbType.VarChar, 50).Value = textBox3.Text;
            sql.Parameters.Add("@c", SqlDbType.VarChar, 30).Value = comboBox1.SelectedItem;
            sql.Parameters.Add("@e", SqlDbType.VarChar, 50).Value = listBox1.SelectedItem;
            sql.Parameters.Add("@dn", SqlDbType.VarChar, 8).Value = textBox4.Text;
            sql.Parameters.Add("@s", SqlDbType.VarChar, 10).Value = textBox5.Text;
            if (radioButton1.Checked)
                sql.Parameters.Add("@sx", SqlDbType.Char, 1).Value = "M";
            else if (radioButton2.Checked)
                sql.Parameters.Add("@sx", SqlDbType.Char, 1).Value = "F";
            conex.Open();
            int ok = sql.ExecuteNonQuery();
            conex.Close();

            if (ok == 1)
            {
                MessageBox.Show("Registro Grabado...");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                comboBox1.Text = "";
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                textBox1.Focus();
                actualizar_gridview();
            }
        }

        void actualizar_gridview()
        {
            SqlDataAdapter datos = new SqlDataAdapter("select * from Alumnos", conex);
            DataSet DB = new DataSet();
            datos.Fill(DB, "Alum");
            dataGridView1.DataSource = DB.Tables["Alum"];
        }

        private void REG_Estudiante_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            insertar();
        }
    }
}
