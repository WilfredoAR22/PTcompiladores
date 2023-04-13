using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions; //para validar mis expresiones

namespace PTcompiladores
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            RTtxt.Focus();

        }

        //salir de la aplicacion
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Variables para mover el panel de arriba
        int m, mx, my;
        private void pnlArriba_MouseDown(object sender, MouseEventArgs e)
        {
            m = 1;
            mx = e.X;
            my = e.Y;
        }

        private void pnlArriba_MouseMove(object sender, MouseEventArgs e)
        {
            if (m == 1)
            {
                this.SetDesktopLocation(MousePosition.X - mx, MousePosition.Y - my);
            }
        }

        
        private void pnlArriba_MouseUp(object sender, MouseEventArgs e)
        {
            m = 0;
        }
        
        //nuevo documento de txt
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RTtxt.Clear();
        }

        //cerrar aplicacion
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //metodo para abrir un archivo
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            ofd.Title = "Open a file...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName);
                RTtxt.Text = sr.ReadToEnd();
            }
        }

        //metodo para guardar un archivo
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf = new SaveFileDialog();
            svf.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            svf.Title = "Save a file...";
            if (svf.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(svf.FileName);
                sw.WriteLine(RTtxt.Text);
                sw.Close();
            }
        }

        //metodo que realiza la accion de eliminar el texto 
        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RTtxt.Undo();
        }

        //metodo que realiza la accion de rehacer texto
        private void rehacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RTtxt.Redo();
        }

        //metodo que realiza la accion de cortar texto
        private void cortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RTtxt.Cut();
        }

        //metodo que realiza la accion de copiar texto
        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RTtxt.Copy();
        }

        //metodo que realiza la accion de pegar texto
        private void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RTtxt.Paste();
        }

        //metodo que realiza la accion de seleccionar todo el texto
        private void seleccionartodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RTtxt.SelectAll();
        }

        //metodos para capturar la linea del rich

        //Para que almacenara el numero de linea
        int currentLineNumber = 1;

        private void UpdateLineNumber()
        {
            RTnum.Text = "";
            for (int i = 1; i <= currentLineNumber; i++)
            {
                RTnum.Text += i + "\n";
            }
        }

        private void RTtxt_TextChanged(object sender, EventArgs e)
        {
            string[] lines = RTtxt.Lines;
            int lineNumber = lines.Length;

            // Actualiza el número de línea solo si ha cambiado
            if (lineNumber != currentLineNumber)
            {
                currentLineNumber = lineNumber;
                UpdateLineNumber();
            }
        }

        //metodo para verificar que se estan recorriendo los caracteres
        private void herramientasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string patron = @"^W[a-z][a-z]*W$"; // Expresión regular que valida una cadena de letras
            Regex regex = new Regex(patron);

            bool hayErrores = false;
            int numeroLinea = 1;

            // Recorrer todas las líneas del RichTextBox
            foreach (string linea in RTtxt.Lines)
            {
                // Limpiar la línea eliminando los saltos de línea, espacios vacíos y tabulaciones
                string lineaLimpia = linea.Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim();

                // Validar la línea limpia utilizando la expresión regular
                if (!regex.IsMatch(lineaLimpia))
                {
                    // Si la línea no cumple con la expresión regular, generar un mensaje de error que incluya el número de línea
                    MessageBox.Show($"Error: la línea {numeroLinea} no cumple con el patrón ");
                    hayErrores = true;
                }

                numeroLinea++;
            }

            if (!hayErrores)
            {
                // Si no hubo errores, el programa ha compilado correctamente
                MessageBox.Show("Programa compilado correctamente");
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Panel contenedor = new Panel();
            contenedor.Dock = DockStyle.Top;
            contenedor.Height = RTtxt.Height + RTnum.Height;

            RTtxt.Parent = contenedor;
            RTnum.Parent = contenedor;

            pnlPrincipal.Controls.Add(contenedor);
        }
    }
}
