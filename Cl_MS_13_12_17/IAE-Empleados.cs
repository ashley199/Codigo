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
    public partial class IAE_Empleados : Form
    {
        public IAE_Empleados()
        {
            InitializeComponent();
        }

        SqlConnection conex = new SqlConnection(ConfigurationManager.ConnectionStrings["DBNorthwind"].ConnectionString);

        private void ver_empleados()
        {
            SqlDataAdapter emp = new SqlDataAdapter("select EmployeeID, LastName, FirstName, Address from Employees", conex);
            DataSet DB = new DataSet();
            emp.Fill(DB, "emp");//abre la conexion ejecuta y la cierra
            dataGridView1.DataSource = DB.Tables["emp"];
            dataGridView1.ReadOnly = true;
        }

        private void IAE_Empleados_Load(object sender, EventArgs e)
        {
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            ver_empleados();
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ind = dataGridView1.CurrentRow.Index;
            IAE_Menu_Emp01 frm = new IAE_Menu_Emp01();
            frm.id = int.Parse(dataGridView1.Rows[ind].Cells[0].Value.ToString());
            frm.ap = dataGridView1.Rows[ind].Cells[1].Value.ToString();
            frm.nom = dataGridView1.Rows[ind].Cells[2].Value.ToString();
            frm.dir = dataGridView1.Rows[ind].Cells[3].Value.ToString();
            frm.ShowDialog();
            ver_empleados();
        }

        private void insertarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
