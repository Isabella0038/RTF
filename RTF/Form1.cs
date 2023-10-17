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

namespace RTF
{
    public partial class Form1 : Form
    {
        private string rtfFilePath; // Ruta del archivo RTF

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            // Mostrar el cuadro de diálogo de apertura de archivo
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos RTF|*.rtf";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                rtfFilePath = openFileDialog.FileName;
                LoadAndDisplayRtfData();
                MessageBox.Show("Archivo RTF cargado exitosamente.");
            }
        }

        private void LoadAndDisplayRtfData()
        {
            try
            {
                // Leer todos los datos del archivo RTF y mostrarlos en un cuadro de texto
                using (FileStream fileStream = new FileStream(rtfFilePath, FileMode.Open))
                {
                    richTextBox1.LoadFile(fileStream, RichTextBoxStreamType.RichText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar y mostrar desde el archivo RTF: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Guardar el contenido del RichTextBox txtContent de vuelta al archivo RTF
                using (FileStream fileStream = new FileStream(rtfFilePath, FileMode.Open))
                {
                    richTextBox1.SaveFile(fileStream, RichTextBoxStreamType.RichText);
                }

                MessageBox.Show("Guardado en el archivo RTF.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar en el archivo RTF: " + ex.Message);
            }
        }

        // Evento para el botón "Editar"
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Verificar si hay algo seleccionado en el RichTextBox
            if (!string.IsNullOrWhiteSpace(richTextBox1.SelectedText))
            {
                // Obtener el texto seleccionado
                string selectedText = richTextBox1.SelectedText;

                // Mostrar un cuadro de diálogo para editar el texto seleccionado
                string editedText = Microsoft.VisualBasic.Interaction.InputBox("Editar texto:", "Edición", selectedText);

                // Reemplazar el texto seleccionado con el texto editado
                richTextBox1.Text = richTextBox1.Text.Replace(selectedText, editedText);
            }
            else
            {
                MessageBox.Show("Seleccione el texto que desea editar.");
            }
        }

        // Evento para el botón "Borrar"
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Verificar si hay algo seleccionado en el RichTextBox
            if (!string.IsNullOrWhiteSpace(richTextBox1.SelectedText))
            {
                // Borrar el texto seleccionado
                richTextBox1.Text = richTextBox1.Text.Replace(richTextBox1.SelectedText, "");
            }
            else
            {
                MessageBox.Show("Seleccione el texto que desea borrar.");
            }
        }
    }
}
