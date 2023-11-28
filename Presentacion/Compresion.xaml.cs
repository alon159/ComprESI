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
            GridDrop.AllowDrop = true;
            BttnGuardar.IsEnabled = false;
            BttnGuardarMenu.IsEnabled = false;
            ListAlgoritmos.IsEnabled = false;
            TxtFactor.IsEnabled = false;
            var metodos= new List<string> { "Bicubic", "Box", "CatmullRom","Hermite", 
                "Lanczos2","Lanczos3","Lanczos5","Lanczos8","MitchellNetravali","NearestNeighbor",
                "Robidoux","RobidouxSharp","Spline","Triangle", "Welch"};
            ListAlgoritmos.ItemsSource = metodos;
            ListAlgoritmos.SelectedIndex = 0;
        }

        private void GridDrop_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] archivos = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (archivos != null && archivos.Length > 0)
                {
                    rutaArchivo = archivos[0]; // Tomamos solo el primer archivo en este ejemplo
                }
            }
            GridDrop.AllowDrop = false;
            ImgDrop.Source = new BitmapImage(new Uri(rutaArchivo));
            BttnGuardar.IsEnabled = true;
            BttnGuardarMenu.IsEnabled = true;
            ListAlgoritmos.IsEnabled = true;
            TxtFactor.IsEnabled = true;
            
        }

        private void BttnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Int32 factor = 0;
            try
            {
                factor = Int32.Parse(TxtFactor.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Se debe introducir un factor de compresión","Error al comprimir", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(rutaArchivo);
            int width = image.Width / factor;
            int height = image.Height / factor;
            image.Mutate(x => x.Resize(width, height));

            //cuadro para guardar archivo
            Microsoft.Win32.SaveFileDialog dlg = new();
            //como obtener el nombre del archivo desde la ruta
            string[] ruta = rutaArchivo.Split('\\');
            string[] nombre = ruta[^1].Split('.');
            dlg.FileName = nombre[0] + "_compressed"; // Nombre por defecto
            dlg.DefaultExt = "." + nombre[1]; // Extensión por defecto
            dlg.Title = "Guardar imagen comprimida"; // Título de la ventana
            dlg.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp"; // Filtro de archivos
            dlg.ShowDialog(); // Mostrar ventana
            try
            {
                image.Save(dlg.FileName); // Guardar imagen
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo guardar la imagen", "Error al guardar", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BttnNuevo_Click(object sender, RoutedEventArgs e)
        {
            rutaArchivo = "";
            GridDrop.AllowDrop = true;
            ImgDrop.Source = new BitmapImage(new Uri("/Assets/add.png",UriKind.RelativeOrAbsolute));
            BttnGuardar.IsEnabled = false;
            BttnGuardarMenu.IsEnabled = false;
            ListAlgoritmos.IsEnabled = false;
            TxtFactor.IsEnabled = false;
        }

        private void BttnAbrir_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new()
            {
                Title = "Abrir imagen", // Título de la ventana
                Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp" // Filtro de archivos
            };
            dlg.ShowDialog(); // Mostrar ventana
            try
            {
                rutaArchivo = dlg.FileName;
                ImgDrop.Source = new BitmapImage(new Uri(rutaArchivo));
                BttnGuardar.IsEnabled = true;
                BttnGuardarMenu.IsEnabled = true;
                ListAlgoritmos.IsEnabled = true;
                TxtFactor.IsEnabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo abrir la imagen", "Error al abrir", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
