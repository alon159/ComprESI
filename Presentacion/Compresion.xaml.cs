using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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
using System.Xml.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.Processing;

namespace ComprESI.Presentacion
{
    /// <summary>
    /// Lógica de interacción para Compresion.xaml
    /// </summary>
    public partial class Compresion : Page
    {
        public string rutaArchivo;

        public Compresion()
        {
            InitializeComponent();
            rutaArchivo="";
            gridDrop.AllowDrop = true;
            bttnGuardar.IsEnabled = false;
            listAlgoritmos.IsEnabled = false;
            txtFactor.IsEnabled = false;
            var metodos= new List<string> { "Bicubic", "Box", "CatmullRom","Hermite", 
                "Lanczos2","Lanczos3","Lanczos5","Lanczos8","MitchellNetravali","NearestNeighbor",
                "Robidoux","RobidouxSharp","Spline","Triangle", "Welch"};
            listAlgoritmos.ItemsSource = metodos;
            listAlgoritmos.SelectedIndex = 0;
        }

        private void gridDrop_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] archivos = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (archivos != null && archivos.Length > 0)
                {
                    rutaArchivo = archivos[0]; // Tomamos solo el primer archivo en este ejemplo
                }
            }
            gridDrop.AllowDrop = false;
            imgDrop.Source = new BitmapImage(new Uri(rutaArchivo));
            bttnGuardar.IsEnabled = true;
            listAlgoritmos.IsEnabled = true;
            txtFactor.IsEnabled = true;
            
        }

        private void bttnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Int32 factor = 0;
            try
            {
                factor = Int32.Parse(txtFactor.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            using (SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(rutaArchivo))
            {
                int width = image.Width / factor;
                int height = image.Height / factor;
                image.Mutate(x => x.Resize(width, height));

                //cuadro para guardar archivo
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                //como obtener el nombre del archivo desde la ruta
                string[] ruta = rutaArchivo.Split('\\');
                string[] nombre = ruta[ruta.Length - 1].Split('.');
                dlg.FileName = nombre[0]+"_compressed"; // Nombre por defecto
                dlg.DefaultExt = "." + nombre[1]; // Extensión por defecto
                dlg.Title = "Guardar imagen comprimida"; // Título de la ventana
                dlg.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp"; // Filtro de archivos
                dlg.ShowDialog(); // Mostrar ventana
                image.Save(dlg.FileName); // Guardar imagen
            }
        }
    }
}
