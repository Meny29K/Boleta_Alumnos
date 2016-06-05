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

namespace BoletaAlumno
{
    public partial class Materias : Form
    {
        public Materias()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            txtMaterias.Enabled = true;
            cmbSemestre.Enabled = true;
            string _Materias = txtMaterias.Text;
            
            if (txtMaterias.Text.Equals(""))
            {
                errorProvider1.SetError(txtMaterias, "ERROR");
            }
            else
            {
                string _Semestre = cmbSemestre.SelectedItem.ToString();
                Conexion oConexion = new Conexion();

                List<SqlParameter> Parametro = new List<SqlParameter>();
                SqlParameter Materias = new SqlParameter()
                {
                    Value = _Materias,
                    ParameterName = "nombre_materia"
                };
                Parametro.Add(Materias);

                SqlParameter Semestre = new SqlParameter()
                {
                    Value = _Semestre,
                    ParameterName = "semestre"
                };
                Parametro.Add(Semestre);

                oConexion.EjecutaNonQuery("sp_InsertarMaterias", Parametro);
                dataGridView1.DataSource = oConexion.ObtenerDatos("sp_VerMaterias");
                txtMaterias.Clear();
                cmbSemestre.Text = "";
                errorProvider1.Clear();
                txtMaterias.Enabled = false;
                cmbSemestre.Enabled = false;
                
            }
        }

        

        private void mToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.Show();
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
          
            Conexion oConexion = new Conexion();
            dataGridView1.DataSource = oConexion.ObtenerDatos("sp_VerMaterias");
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            txtMaterias.Enabled = true;
            cmbSemestre.Enabled = false;
            string _Materias = txtMaterias.Text;
            if (txtMaterias.Text.Equals(""))
            {
                MessageBox.Show("Escribe una materia para eliminar", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                MessageBox.Show("La materia que desea eliminar esta correctamente escrita??","ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Conexion oConexion = new Conexion();
                List<SqlParameter> Parametro = new List<SqlParameter>();
                SqlParameter Materias = new SqlParameter()
                {
                    Value = _Materias,
                    ParameterName = "nombre_materia"
                };
                Parametro.Add(Materias);

                oConexion.EjecutaNonQuery("sp_EliminarMaterias", Parametro);

                dataGridView1.DataSource = oConexion.ObtenerDatos("sp_VerMaterias");

                txtMaterias.Clear();
                cmbSemestre.Text = "";
                cmbSemestre.Enabled = false;
                txtMaterias.Enabled = false;
            }
        }
        
        private void txtMaterias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space) && (e.KeyChar != (char)Keys.Enter))
            {
                e.Handled = true;
                MessageBox.Show("Solo puede introducir texto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);   
            }            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            dataGridView1.Enabled = true;
            txtMaterias.Enabled = true;
            cmbSemestre.Enabled = true;
            string _Matricula = txtMatricula.Text;
            if (txtMaterias.Text.Equals (""))
            {
                MessageBox.Show("Ingresa una Materia para modificar","ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                MessageBox.Show("La materia que desea Modificar esta correctamente escrita??", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                string _Materia = txtMaterias.Text;
                string _Semestre = cmbSemestre.SelectedItem.ToString();
                Conexion oConexion = new Conexion();
                List<SqlParameter> Parametro = new List<SqlParameter>();

                SqlParameter Semestre = new SqlParameter()
                {
                    Value = _Semestre,
                    ParameterName = "semestre"
                };
                Parametro.Add(Semestre);

                SqlParameter Materia = new SqlParameter()
                {
                    Value = _Materia,
                    ParameterName = "nombre_materia"
                };
                Parametro.Add(Materia);

                SqlParameter Matricula = new SqlParameter()
                {
                    Value = _Matricula,
                    ParameterName = "matricula_Mat"
                };
                Parametro.Add(Matricula);

                oConexion.EjecutaNonQuery("sp_ModificarMaterias", Parametro);

                dataGridView1.DataSource = oConexion.ObtenerDatos("sp_VerMaterias");

                txtMaterias.Clear();
                cmbSemestre.Text = "";
                txtMatricula.Clear();
                txtMaterias.BackColor = Color.AliceBlue;

                txtMaterias.Enabled = true;
            }
        }
    
    

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //MessageBox.Show( dataGridView1.CurrentRow.Cells[1].Value.ToString() );
            txtMatricula.Enabled = false;
            txtMaterias.Enabled = true;
            txtMaterias.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cmbSemestre.SelectedItem = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtMatricula.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            //btnModificar.Enabled = false;
            //txtMaterias.Enabled = false;

            dataGridView1.Enabled = true;
            string _Semestre = cmbSemestre.SelectedItem.ToString();

            Conexion oConexion = new Conexion();
            List<SqlParameter> Parametro = new List<SqlParameter>();
            SqlParameter Semestre = new SqlParameter()
            {
                Value = _Semestre,
                ParameterName = "semestre"
            };
            Parametro.Add(Semestre);
            dataGridView1.DataSource = oConexion.TraerDatosConParametros("sp_BuscarMaterias", Parametro);
            //string DatoRecibido =  oConexion.ObtenerDato("sp_BuscarMaterias", Parametro);

            //dataGridView1.DataSource = oConexion.ObtenerDatos("sp_BuscarMaterias");

            cmbSemestre.SelectedItem = "";
            dataGridView1.Enabled = false;
        }

        private void cmbSemestre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtMatricula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space) && (e.KeyChar != (char)Keys.Enter))
            {
                e.Handled = true;
                MessageBox.Show("Solo puede introducir texto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Materias_Load(object sender, EventArgs e)
        {
            btnBuscar.Enabled = false;
            txtMaterias.Enabled = false;           
            txtMatricula.Enabled = false;
            cmbSemestre.Enabled = false;
            Conexion oConexion = new Conexion();
            dataGridView1.DataSource = oConexion.ObtenerDatos("sp_VerMaterias");
            dataGridView1.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                btnBuscar.Enabled = true;
                MessageBox.Show("Se ha habilitado el boton buscar");
                btnAgregar.Enabled = false;
                btnEliminar.Enabled = false;
                btnModificar.Enabled = false;
                btnVisualizar.Enabled = false;
                txtMaterias.Enabled = false;
                txtMatricula.Enabled = false;
                cmbSemestre.Enabled = true;
                dataGridView1.Enabled = false;
                
            }
            if(checkBox1.Checked == false)
            {
                btnBuscar.Enabled = false;
                MessageBox.Show("Se ha deshabilitado el boton buscar");
                btnAgregar.Enabled = true;
                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;
                btnVisualizar.Enabled = true;
                txtMaterias.Enabled = false;
                txtMatricula.Enabled = false;
                cmbSemestre.Text = "";
                cmbSemestre.Enabled = false;
            }
        }
    }
}
