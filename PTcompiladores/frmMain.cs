using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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

        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rtxt.Undo();
        }

        private void rehacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rtxt.Redo();
        }

        private void cortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rtxt.Cut();
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rtxt.Copy();
        }

        private void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rtxt.Paste();
        }

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
    }
}
