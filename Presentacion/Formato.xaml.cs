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
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Pbm;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tga;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

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
            Tuple<IImageEncoder, String> config = formatoImagen();
            using SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(rutaArchivo);
            string[] ruta = rutaArchivo.Split('\\');
            string[] nombre = ruta[^1].Split('.');
            //cuadro para guardar archivo
            Microsoft.Win32.SaveFileDialog dlg = new();
            //como obtener el nombre del archivo desde la ruta
            dlg.FileName = nombre[0] + "_formatted"; // Nombre por defecto
            dlg.DefaultExt = "." + config.Item2; // Extensión por defecto
            dlg.Title = "Guardar imagen comprimida"; // Título de la ventana
            dlg.Filter = "Archivos de imagen|*.bmp;*.gif;*.jpeg;*.pbm;*.png;*.tiff;*.tga;*.webp"; // Filtro de archivos
            dlg.ShowDialog(); // Mostrar ventana
            try
            {
                image.Save(dlg.FileName, config.Item1); // Guardar imagen
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo guardar la imagen", "Error al guardar", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        //Menu

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
                Filter = "Archivos de imagen|*.bmp;*.gif;*.jpeg;*.pbm;*.png;*.tiff;*.tga;*.webp" // Filtro de archivos
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
                return;
            }
        }

        private void BttnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Metodos auxiliares

        private Tuple<IImageEncoder,String> formatoImagen()
        {
            Tuple<IImageEncoder,String> config = new Tuple<IImageEncoder, String>(new BmpEncoder(), "bmp");
            switch (ListFormatos.SelectedItem)
            {
                case "Bmp":
                    return new Tuple<IImageEncoder, String>(new BmpEncoder(), "bmp");
                case "Gif":
                    return new Tuple<IImageEncoder, String>(new GifEncoder(), "gif");
                case "Jpeg":
                    return new Tuple<IImageEncoder, String>(new JpegEncoder(), "jpeg");
                case "Pbm":
                    return new Tuple<IImageEncoder, String>(new PbmEncoder(), "pbm");
                case "Png":
                    return new Tuple<IImageEncoder, String>(new PngEncoder(), "png");
                case "Tiff":
                    return new Tuple<IImageEncoder, String>(new TiffEncoder(), "tiff");
                case "Tga":
                    return new Tuple<IImageEncoder, String>(new TgaEncoder(), "tga");
                case "WebP":
                    return new Tuple<IImageEncoder, String>(new WebpEncoder(), "webp");
                default:
                    return config;
            }
        }
    }
}
