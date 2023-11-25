using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComprESI.Presentacion
{
    /// <summary>
    /// Lógica de interacción para Compresion.xaml
    /// </summary>
    public partial class Compresion : Page
    {
        public Compresion()
        {
            InitializeComponent();
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] archivos = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (archivos != null && archivos.Length > 0)
                {
                    string rutaArchivo = archivos[0]; // Tomamos solo el primer archivo en este ejemplo

                    MessageBox.Show("Archivo seleccionado: " + rutaArchivo);
                }
            }
        }
    }
}
