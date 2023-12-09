using ComprESI.Presentacion;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComprESI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameMain.Navigate(new Home());
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            Menu();
        }

        private void Menu()
        {
            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_compresion.Visibility = Visibility.Collapsed;
                tt_formato.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_compresion.Visibility = Visibility.Visible;
                tt_formato.Visibility = Visibility.Visible;
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            BG.Opacity = 1;
            FrameMain.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            BG.Opacity = 0.7;
            FrameMain.Opacity = 0.7;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void BttnCompresion_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new Compresion());
            if (Tg_Btn.IsChecked == true)
                Tg_Btn.IsChecked = false;
            Menu();
        }

        private void BttnHome_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new Home());
            if (Tg_Btn.IsChecked == true)
                Tg_Btn.IsChecked = false;
            Menu();
        }

        private void BttnFormato_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new Formato());
            if (Tg_Btn.IsChecked == true)
                Tg_Btn.IsChecked = false;
            Menu();
        }
    }
}