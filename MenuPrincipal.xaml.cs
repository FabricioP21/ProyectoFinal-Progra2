using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Proy_Fin
{
    public partial class MenuPrincipal : Window
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void btnRegistro_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aquí va el registro.");
        }

        private void btnDoctor_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aquí se asigna el doctor.");
        }

        private void btnHistorial_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Mostrando Historial Clínico.");
        }

        private void btnServicios_Click(object sender, RoutedEventArgs e)
        {
            VentanaServicios servicios = new VentanaServicios();
            servicios.ShowDialog();
        }
    }
}