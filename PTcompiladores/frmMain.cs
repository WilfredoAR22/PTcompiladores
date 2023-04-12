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

namespace PTcompiladores
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        //nuevo documento de txt
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rtxt.Clear();
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
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName);
                Rtxt.Text = sr.ReadToEnd();
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
                sw.WriteLine(Rtxt.Text);
                sw.Close();
            }
        }

        //metodo que realiza la accion de eliminar el texto 
        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rtxt.Undo();
        }

        //metodo que realiza la accion de rehacer texto
        private void rehacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rtxt.Redo();
        }

        //metodo que realiza la accion de cortar texto
        private void cortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rtxt.Cut();
        }

        //metodo que realiza la accion de copiar texto
        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rtxt.Copy();
        }

        //metodo que realiza la accion de pegar texto
        private void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rtxt.Paste();
        }

        //metodo que realiza la accion de seleccionar todo el texto
        private void seleccionartodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rtxt.SelectAll();
        }


        //metodos para capturar la linea del rich

        //Para que almacenara el numero de linea
        int currentLineNumber = 1;

        private void UpdateLineNumber()
        {
            label1.Text = "";
            for (int i = 1; i <= currentLineNumber; i++)
            {
                label1.Text += i + "\n";
            }
        }

        //metodo para verificar que se estan recorriendo los caracteres
        private void opcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string contenido = "";
            foreach (string linea in Rtxt.Lines)
            {
                contenido += linea + "\n";
            }
            MessageBox.Show(contenido);
        }

        private void Rtxt_TextChanged(object sender, EventArgs e)
        {
            string[] lines = Rtxt.Lines;
            int lineNumber = lines.Length;

            // Actualiza el número de línea solo si ha cambiado
            if (lineNumber != currentLineNumber)
            {
                currentLineNumber = lineNumber;
                UpdateLineNumber();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
             Panel contenedor = new Panel();
             contenedor.Dock = DockStyle.Top;
             contenedor.Height = Rtxt.Height + label1.Height;

             Rtxt.Parent = contenedor;
             label1.Parent = contenedor;

             panel1.Controls.Add(contenedor);

        }

        //Variables para mover el panel de arriba
        int m, mx, my;
        private void MSArriba_MouseDown(object sender, MouseEventArgs e)
        {
            m = 1;
            mx = e.X;
            my = e.Y;
        }

        private void MSArriba_MouseMove(object sender, MouseEventArgs e)
        {
            if (m == 1)
            {
                this.SetDesktopLocation(MousePosition.X - mx, MousePosition.Y - my);
            }
        }

        private void MSArriba_MouseUp(object sender, MouseEventArgs e)
        {
            m = 0;
        }
    }
}
