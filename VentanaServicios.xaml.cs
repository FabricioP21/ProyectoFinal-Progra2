using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Proy_Fin
{
    public partial class VentanaServicios : Window
    {
        public VentanaServicios()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Seleccionar imagen";
            op.Filter = "Imágenes|*.jpg;*.jpeg;*.png";

            if (op.ShowDialog() == true)
            {
                imgComprobante.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            if (imgComprobante.Source != null)
            {
                MessageBox.Show("¡Comprobante enviado!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Carga una imagen primero.");
            }
        }
    }
}