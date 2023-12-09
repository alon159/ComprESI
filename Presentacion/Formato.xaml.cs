using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Lógica de interacción para Formato.xaml
    /// </summary>
    public partial class Formato : Page
    {
        public string rutaArchivo;

        public Formato()
        {
            InitializeComponent();
            rutaArchivo = "";
            GridDrop.AllowDrop = true;
            BttnGuardar.IsEnabled = false;
            BttnGuardarMenu.IsEnabled = false;
            ListFormatos.IsEnabled = false;
            var formatos = new List<string> {"Bmp","Gif","Jpeg","Pbm","Png","Tiff","Tga","WebP"};
            ListFormatos.ItemsSource = formatos;
            ListFormatos.SelectedIndex = 0;
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
            ListFormatos.IsEnabled = true;

        }

        private void BttnGuardar_Click(object sender, RoutedEventArgs e)
        {
            using SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(rutaArchivo);
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
            ImgDrop.Source = new BitmapImage(new Uri("/Assets/add.png", UriKind.RelativeOrAbsolute));
            BttnGuardar.IsEnabled = false;
            BttnGuardarMenu.IsEnabled = false;
            ListFormatos.IsEnabled = false;
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
                ListFormatos.IsEnabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo abrir la imagen", "Error al abrir", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
