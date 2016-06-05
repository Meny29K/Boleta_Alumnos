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
    public partial class Alumno : Form
    {
        public Alumno()
        {
            InitializeComponent();
        }

        private void mToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.Show();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            txtAlumno.Enabled = true;
            cmbSemestre.Enabled = true;
            string _Alumno = txtAlumno.Text;           
            if (txtAlumno.Text.Equals(""))
            {
                errorProvider1.SetError(txtAlumno, "ERROR");
            }
            else
            {
                string _Semestre = cmbSemestre.SelectedItem.ToString();
                Conexion oConexion = new Conexion();

                List<SqlParameter> Parametro = new List<SqlParameter>();

                SqlParameter Alumno = new SqlParameter()
                {
                    Value = _Alumno,
                    ParameterName = "nombre_alumno"
                };
                Parametro.Add(Alumno);

                SqlParameter Semestre = new SqlParameter()
                {
                    Value = _Semestre,
                    ParameterName = "semestre"
                };
                Parametro.Add(Semestre);

                oConexion.EjecutaNonQuery("sp_InsertarAlumnos", Parametro);
                dataGridView1.DataSource = oConexion.ObtenerDatos("sp_VerAlumnos");
                txtAlumno.Clear();
                cmbSemestre.Text = "";
                errorProvider1.Clear();
                txtAlumno.Enabled = false;
                cmbSemestre.Enabled = false;
            }
        }


        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            Conexion oConexion = new Conexion();
            dataGridView1.DataSource = oConexion.ObtenerDatos("sp_VerAlumnos");
        }



        private void btnEliminar_Click(object sender, EventArgs e)
        {
            cmbMatricula.Enabled = true;
            if (cmbMatricula.Text.Equals(""))
            {
                MessageBox.Show("Ingresa una 'MATRICULA'","ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                MessageBox.Show("Estas seguro que la matricula que desea eliminar es correcta??","ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                string _Matricula = cmbMatricula.SelectedItem.ToString();
                Conexion oConexion = new Conexion();

                List<SqlParameter> Parametro = new List<SqlParameter>();

                SqlParameter Matricula = new SqlParameter()
                {
                    Value = _Matricula,
                    ParameterName = "matricula_Alu"
                };
                Parametro.Add(Matricula);

                oConexion.EjecutaNonQuery("sp_EliminarAlumnos", Parametro);
                dataGridView1.DataSource = oConexion.ObtenerDatos("sp_VerAlumnos");
                cmbMatricula.Text = "";
                txtAlumno.Enabled = false;
                cmbSemestre.Enabled = false;
            }
        }

        private void Alumno_Load(object sender, EventArgs e)
        {
            Conexion oConexion = new Conexion();
            dataGridView1.Enabled = false;
            txtAlumno.Enabled = false;
            cmbSemestre.Enabled = false;
            cmbMatricula.Enabled = false;
            dataGridView1.DataSource = oConexion.ObtenerDatos("sp_VerAlumnos");
        }

        private void cmbMatricula_Click(object sender, EventArgs e)
        {
            Conexion oConexion = new Conexion();
            List<string> Matricula = oConexion.TraerDatos("sp_MostrarMatricula");
            cmbMatricula.Items.Clear();
            foreach (var _matricula in Matricula)
            {
                cmbMatricula.Items.Add(_matricula);
            }
        }

        private void cmbMatricula_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*Conexion oConexion = new Conexion();
            cmbMatricula.SelectedItem = oConexion.ObtenerDatos("sp_MostrarMatricula").ToString();*/
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            txtAlumno.Enabled = true;
            cmbSemestre.Enabled = true;
            string _Matricula = txtAlumno.Text;
            if (txtAlumno.Text.Equals(""))
            {
                MessageBox.Show("Ingresa una Materia para modificar", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
    }
}
