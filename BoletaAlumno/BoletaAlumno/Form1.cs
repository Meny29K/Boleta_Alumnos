using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoletaAlumno
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Alumno alum = new Alumno();
            this.Hide();
            alum.Show();
        }

        private void materiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Materias Mat = new Materias();
            this.Hide();
            Mat.Show();
        }

        private void cmbAlumno_Click(object sender, EventArgs e)
        {
            Conexion oConexion = new Conexion();
            List<string> Alumno = oConexion.TraerDatos("sp_CMBAlumnos");
            cmbAlumno.Items.Clear();
            foreach (var _Alumno in Alumno)
            {
                cmbAlumno.Items.Add(_Alumno);
            }
        }

        private void cmbMatricula_Click(object sender, EventArgs e)
        {
            Conexion oConexion = new Conexion();
            List<string> Matricula = oConexion.TraerDatos("sp_MostrarMatricula");
            cmbMatricula.Items.Clear();
            foreach (var _Matricula in Matricula)
            {
                cmbMatricula.Items.Add(_Matricula);
            }
        }

        private void cmbMateria_Click(object sender, EventArgs e)
        {
            Conexion oConexion = new Conexion();
            List<string> Materia = oConexion.TraerDatos("sp_CMBMateria");
            cmbMateria.Items.Clear();
            foreach (var _Materia in Materia)
            {
                cmbMateria.Items.Add(_Materia);
            }
        }
    }
}
