using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SistemaVeterinaria
{
    public partial class MainWindow : Window
    {
        // Colecciones observables para los datos
        private ObservableCollection<Cliente> clientes = new ObservableCollection<Cliente>();
        private ObservableCollection<Animal> animales = new ObservableCollection<Animal>();
        private ObservableCollection<Doctor> doctores = new ObservableCollection<Doctor>();
        private ObservableCollection<Atencion> atenciones = new ObservableCollection<Atencion>();
        private ObservableCollection<Servicio> servicios = new ObservableCollection<Servicio>();

        // Variables para edición
        private Cliente clienteSeleccionado;
        private Animal animalSeleccionado;
        private Doctor doctorSeleccionado;
        private Atencion atencionSeleccionada;
        private Servicio servicioSeleccionado;

        public MainWindow()
        {
            InitializeComponent();
            InicializarDatos();
            CargarDatos();
        }

        private void InicializarDatos()
        {
            // Datos de ejemplo
            clientes.Add(new Cliente { Apellido = "García", CI = "12345678" });
            clientes.Add(new Cliente { Apellido = "López", CI = "87654321" });

            doctores.Add(new Doctor { Apellido = "Rodríguez", CI = "11223344" });
            doctores.Add(new Doctor { Apellido = "Martínez", CI = "44332211" });

            animales.Add(new Animal { TipoAnimal = "Perro", Raza = "Labrador", ClienteCI = "12345678" });
            animales.Add(new Animal { TipoAnimal = "Gato", Raza = "Persa", ClienteCI = "87654321" });

            servicios.Add(new Servicio { Nombre = "Vacunación", Tipo = "Preventivo", PrecioMac = 150.00m, Peso = 0, Edad = 0, Sexo = "N/A" });
            servicios.Add(new Servicio { Nombre = "Consulta General", Tipo = "Consulta", PrecioMac = 100.00m, Peso = 0, Edad = 0, Sexo = "N/A" });
        }

        private void CargarDatos()
        {
            // Cargar DataGrids
            dgClientes.ItemsSource = clientes;
            dgAnimales.ItemsSource = animales;
            dgDoctores.ItemsSource = doctores;
            dgAtenciones.ItemsSource = atenciones;
            dgServicios.ItemsSource = servicios;

            // Cargar ComboBoxes
            cmbClienteAnimal.ItemsSource = clientes;
            cmbAnimalAtencion.ItemsSource = animales;
            cmbDoctorAtencion.ItemsSource = doctores;
        }

        // ==================== CLIENTES ====================
        private void BtnNuevoCliente_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormularioCliente();
        }

        private void BtnGuardarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClienteApellido.Text) || string.IsNullOrWhiteSpace(txtClienteCI.Text))
            {
                MessageBox.Show("Por favor complete todos los campos", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (clienteSeleccionado != null)
            {
                // Editar
                clienteSeleccionado.Apellido = txtClienteApellido.Text;
                clienteSeleccionado.CI = txtClienteCI.Text;
                dgClientes.Items.Refresh();
            }
            else
            {
                // Nuevo
                var nuevoCliente = new Cliente
                {
                    Apellido = txtClienteApellido.Text,
                    CI = txtClienteCI.Text
                };
                clientes.Add(nuevoCliente);
            }

            LimpiarFormularioCliente();
            MessageBox.Show("Cliente guardado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnEliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (clienteSeleccionado != null)
            {
                var resultado = MessageBox.Show("¿Está seguro de eliminar este cliente?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultado == MessageBoxResult.Yes)
                {
                    clientes.Remove(clienteSeleccionado);
                    LimpiarFormularioCliente();
                    MessageBox.Show("Cliente eliminado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void DgClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clienteSeleccionado = dgClientes.SelectedItem as Cliente;
            if (clienteSeleccionado != null)
            {
                txtClienteApellido.Text = clienteSeleccionado.Apellido;
                txtClienteCI.Text = clienteSeleccionado.CI;
            }
        }

        private void LimpiarFormularioCliente()
        {
            txtClienteApellido.Clear();
            txtClienteCI.Clear();
            clienteSeleccionado = null;
            dgClientes.SelectedItem = null;
        }

        // ==================== ANIMALES ====================
        private void BtnNuevoAnimal_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormularioAnimal();
        }

        private void BtnGuardarAnimal_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTipoAnimal.Text) || string.IsNullOrWhiteSpace(txtRaza.Text) || cmbClienteAnimal.SelectedItem == null)
            {
                MessageBox.Show("Por favor complete todos los campos", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var clienteSelec = cmbClienteAnimal.SelectedItem as Cliente;

            if (animalSeleccionado != null)
            {
                // Editar
                animalSeleccionado.TipoAnimal = txtTipoAnimal.Text;
                animalSeleccionado.Raza = txtRaza.Text;
                animalSeleccionado.ClienteCI = clienteSelec.CI;
                dgAnimales.Items.Refresh();
            }
            else
            {
                // Nuevo
                var nuevoAnimal = new Animal
                {
                    TipoAnimal = txtTipoAnimal.Text,
                    Raza = txtRaza.Text,
                    ClienteCI = clienteSelec.CI
                };
                animales.Add(nuevoAnimal);
            }

            LimpiarFormularioAnimal();
            MessageBox.Show("Animal guardado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnEliminarAnimal_Click(object sender, RoutedEventArgs e)
        {
            if (animalSeleccionado != null)
            {
                var resultado = MessageBox.Show("¿Está seguro de eliminar este animal?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultado == MessageBoxResult.Yes)
                {
                    animales.Remove(animalSeleccionado);
                    LimpiarFormularioAnimal();
                    MessageBox.Show("Animal eliminado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void DgAnimales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            animalSeleccionado = dgAnimales.SelectedItem as Animal;
            if (animalSeleccionado != null)
            {
                txtTipoAnimal.Text = animalSeleccionado.TipoAnimal;
                txtRaza.Text = animalSeleccionado.Raza;
                cmbClienteAnimal.SelectedItem = clientes.FirstOrDefault(c => c.CI == animalSeleccionado.ClienteCI);
            }
        }

        private void LimpiarFormularioAnimal()
        {
            txtTipoAnimal.Clear();
            txtRaza.Clear();
            cmbClienteAnimal.SelectedItem = null;
            animalSeleccionado = null;
            dgAnimales.SelectedItem = null;
        }

        // ==================== DOCTORES ====================
        private void BtnNuevoDoctor_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormularioDoctor();
        }

        private void BtnGuardarDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDoctorApellido.Text) || string.IsNullOrWhiteSpace(txtDoctorCI.Text))
            {
                MessageBox.Show("Por favor complete todos los campos", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (doctorSeleccionado != null)
            {
                // Editar
                doctorSeleccionado.Apellido = txtDoctorApellido.Text;
                doctorSeleccionado.CI = txtDoctorCI.Text;
                dgDoctores.Items.Refresh();
            }
            else
            {
                // Nuevo
                var nuevoDoctor = new Doctor
                {
                    Apellido = txtDoctorApellido.Text,
                    CI = txtDoctorCI.Text
                };
                doctores.Add(nuevoDoctor);
            }

            LimpiarFormularioDoctor();
            MessageBox.Show("Doctor guardado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnEliminarDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (doctorSeleccionado != null)
            {
                var resultado = MessageBox.Show("¿Está seguro de eliminar este doctor?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultado == MessageBoxResult.Yes)
                {
                    doctores.Remove(doctorSeleccionado);
                    LimpiarFormularioDoctor();
                    MessageBox.Show("Doctor eliminado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void DgDoctores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            doctorSeleccionado = dgDoctores.SelectedItem as Doctor;
            if (doctorSeleccionado != null)
            {
                txtDoctorApellido.Text = doctorSeleccionado.Apellido;
                txtDoctorCI.Text = doctorSeleccionado.CI;
            }
        }

        private void LimpiarFormularioDoctor()
        {
            txtDoctorApellido.Clear();
            txtDoctorCI.Clear();
            doctorSeleccionado = null;
            dgDoctores.SelectedItem = null;
        }

        // ==================== ATENCIONES ====================
        private void BtnNuevaAtencion_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormularioAtencion();
        }

        private void BtnGuardarAtencion_Click(object sender, RoutedEventArgs e)
        {
            if (dpFechaAtencion.SelectedDate == null || cmbAnimalAtencion.SelectedItem == null || cmbDoctorAtencion.SelectedItem == null)
            {
                MessageBox.Show("Por favor complete todos los campos", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var animalSelec = cmbAnimalAtencion.SelectedItem as Animal;
            var doctorSelec = cmbDoctorAtencion.SelectedItem as Doctor;

            if (atencionSeleccionada != null)
            {
                // Editar
                atencionSeleccionada.Fecha = dpFechaAtencion.SelectedDate.Value;
                atencionSeleccionada.Hora = txtHoraAtencion.Text;
                atencionSeleccionada.AnimalInfo = $"{animalSelec.TipoAnimal} - {animalSelec.Raza}";
                atencionSeleccionada.DoctorInfo = $"Dr. {doctorSelec.Apellido}";
                dgAtenciones.Items.Refresh();
            }
            else
            {
                // Nueva
                var nuevaAtencion = new Atencion
                {
                    Fecha = dpFechaAtencion.SelectedDate.Value,
                    Hora = txtHoraAtencion.Text,
                    AnimalInfo = $"{animalSelec.TipoAnimal} - {animalSelec.Raza}",
                    DoctorInfo = $"Dr. {doctorSelec.Apellido}"
                };
                atenciones.Add(nuevaAtencion);
            }

            LimpiarFormularioAtencion();
            MessageBox.Show("Atención guardada exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnEliminarAtencion_Click(object sender, RoutedEventArgs e)
        {
            if (atencionSeleccionada != null)
            {
                var resultado = MessageBox.Show("¿Está seguro de eliminar esta atención?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultado == MessageBoxResult.Yes)
                {
                    atenciones.Remove(atencionSeleccionada);
                    LimpiarFormularioAtencion();
                    MessageBox.Show("Atención eliminada exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void DgAtenciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            atencionSeleccionada = dgAtenciones.SelectedItem as Atencion;
            if (atencionSeleccionada != null)
            {
                dpFechaAtencion.SelectedDate = atencionSeleccionada.Fecha;
                txtHoraAtencion.Text = atencionSeleccionada.Hora;
            }
        }

        private void LimpiarFormularioAtencion()
        {
            dpFechaAtencion.SelectedDate = DateTime.Now;
            txtHoraAtencion.Text = "00:00";
            cmbAnimalAtencion.SelectedItem = null;
            cmbDoctorAtencion.SelectedItem = null;
            atencionSeleccionada = null;
            dgAtenciones.SelectedItem = null;
        }

        // ==================== SERVICIOS ====================
        private void BtnNuevoServicio_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormularioServicio();
        }

        private void BtnGuardarServicio_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtServicioNombre.Text) || string.IsNullOrWhiteSpace(txtServicioTipo.Text))
            {
                MessageBox.Show("Por favor complete los campos obligatorios", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            decimal precioMac = 0;
            decimal.TryParse(txtServicioPrecioMac.Text, out precioMac);

            int peso = 0;
            int.TryParse(txtServicioPeso.Text, out peso);

            int edad = 0;
            int.TryParse(txtServicioEdad.Text, out edad);

            string sexo = (cmbServicioSexo.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "N/A";

            if (servicioSeleccionado != null)
            {
                // Editar
                servicioSeleccionado.Nombre = txtServicioNombre.Text;
                servicioSeleccionado.Tipo = txtServicioTipo.Text;
                servicioSeleccionado.PrecioMac = precioMac;
                servicioSeleccionado.Peso = peso;
                servicioSeleccionado.Edad = edad;
                servicioSeleccionado.Sexo = sexo;
                dgServicios.Items.Refresh();
            }
            else
            {
                // Nuevo
                var nuevoServicio = new Servicio
                {
                    Nombre = txtServicioNombre.Text,
                    Tipo = txtServicioTipo.Text,
                    PrecioMac = precioMac,
                    Peso = peso,
                    Edad = edad,
                    Sexo = sexo
                };
                servicios.Add(nuevoServicio);
            }

            LimpiarFormularioServicio();
            MessageBox.Show("Servicio guardado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnEliminarServicio_Click(object sender, RoutedEventArgs e)
        {
            if (servicioSeleccionado != null)
            {
                var resultado = MessageBox.Show("¿Está seguro de eliminar este servicio?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultado == MessageBoxResult.Yes)
                {
                    servicios.Remove(servicioSeleccionado);
                    LimpiarFormularioServicio();
                    MessageBox.Show("Servicio eliminado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void DgServicios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            servicioSeleccionado = dgServicios.SelectedItem as Servicio;
            if (servicioSeleccionado != null)
            {
                txtServicioNombre.Text = servicioSeleccionado.Nombre;
                txtServicioTipo.Text = servicioSeleccionado.Tipo;
                txtServicioPrecioMac.Text = servicioSeleccionado.PrecioMac.ToString();
                txtServicioPeso.Text = servicioSeleccionado.Peso.ToString();
                txtServicioEdad.Text = servicioSeleccionado.Edad.ToString();

                if (servicioSeleccionado.Sexo == "Macho")
                    cmbServicioSexo.SelectedIndex = 0;
                else if (servicioSeleccionado.Sexo == "Hembra")
                    cmbServicioSexo.SelectedIndex = 1;
            }
        }

        private void LimpiarFormularioServicio()
        {
            txtServicioNombre.Clear();
            txtServicioTipo.Clear();
            txtServicioPrecioMac.Clear();
            txtServicioPeso.Clear();
            txtServicioEdad.Clear();
            cmbServicioSexo.SelectedItem = null;
            servicioSeleccionado = null;
            dgServicios.SelectedItem = null;
        }
    }

    // ==================== MODELOS ====================
    public class Persona
    {
        public string Apellido { get; set; }
        public string CI { get; set; }
    }

    public class Cliente : Persona
    {
    }

    public class Doctor : Persona
    {
        public string Display => $"Dr. {Apellido} ({CI})";
    }

    public class Usuario
    {
        public string Field { get; set; }
    }

    public class Animal
    {
        public string TipoAnimal { get; set; }
        public string Raza { get; set; }
        public string ClienteCI { get; set; }
        public string Display => $"{TipoAnimal} - {Raza}";
    }

    public class Atencion
    {
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string AnimalInfo { get; set; }
        public string DoctorInfo { get; set; }
    }

    public class HistorialClinico
    {
        public int NroHistorial { get; set; }
        public string NombreAtencionDoc { get; set; }
    }

    public class Veterinaria
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
    }

    public class Servicio
    {
        public string Nombre { get; set; }
        public int Peso { get; set; }
        public decimal PrecioMac { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Tipo { get; set; }
    }
}