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
    public partial class ACT_Estudiante : Form
    {
        public ACT_Estudiante()
        {
            InitializeComponent();
        }

        SqlConnection conex = new SqlConnection(ConfigurationManager.ConnectionStrings["DBNorthwind"].ConnectionString);

        void consulta()
        {
            SqlDataAdapter datos = new SqlDataAdapter("select * from Alumnos", conex);
            DataSet DB = new DataSet();
            datos.Fill(DB, "alum");
            dataGridView1.DataSource = DB.Tables["alum"];
        }

        private void ACT_Estudiante_Load(object sender, EventArgs e)
        {
            consulta();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            SqlDataAdapter datos = new SqlDataAdapter("select * from Alumnos", conex);
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            listBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            if (dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString().Equals("M"))
                radioButton1.Checked = true;
            else if (dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString().Equals("F"))
                radioButton2.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand sql = new SqlCommand("update Alumnos set Nombres = @n, Apellidos = @a, Dirección = @d, Ciudad = @c, Escuela_Prof = @e, DNI = @dn, Sunedu = @s, Sexo = @sx where Id_estudiante = @id", conex);

            sql.Parameters.Add("@id", SqlDbType.Char, 1).Value = textBox1.Text;
            sql.Parameters.Add("@n", SqlDbType.VarChar, 25).Value = textBox2.Text;
            sql.Parameters.Add("@a", SqlDbType.VarChar, 30).Value = textBox3.Text;
            sql.Parameters.Add("@d", SqlDbType.VarChar, 50).Value = textBox4.Text;
            sql.Parameters.Add("@c", SqlDbType.VarChar, 30).Value = comboBox1.SelectedItem;
            sql.Parameters.Add("@e", SqlDbType.VarChar, 50).Value = listBox1.SelectedItem;
            sql.Parameters.Add("@dn", SqlDbType.VarChar, 8).Value = textBox5.Text;
            sql.Parameters.Add("@s", SqlDbType.VarChar, 10).Value = textBox6.Text;
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
            }
            consulta();
        }
    }
}
