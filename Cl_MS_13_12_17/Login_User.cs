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
    public partial class Login_User : Form
    {
        public Login_User()
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "//img//0.jpg");
        }

        SqlConnection conex = new SqlConnection(ConfigurationManager.ConnectionStrings["DBNorthwind"].ConnectionString);

        private void Login_User_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand query = new SqlCommand("select count(*) from Usuarios where Usuario = @u and Password = @p", conex);
            query.Parameters.Add("@u", SqlDbType.VarChar, 50).Value = textBox1.Text;
            query.Parameters.Add("@p", SqlDbType.VarChar, 50).Value = textBox2.Text;
            conex.Open();
            int existe = int.Parse(query.ExecuteScalar().ToString());
            conex.Close();

            if (existe == 1)
            {
                 
                REG_Estudiante frm = new REG_Estudiante();
                frm.Show();
                //buscar solucion mas adecuada
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Usuario incorrecto!!!");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NClave frm = new NClave();
            frm.Show();
        }
    }
}
