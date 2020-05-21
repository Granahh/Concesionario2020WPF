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

namespace Concesionario2020WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DAO_Coches_M conexion;
        DAO_Coches_S conexionS;
        int tipo = 0;
        public MainWindow()
        {
            InitializeComponent();

            conexion = new DAO_Coches_M();
            conexionS = new DAO_Coches_S();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            List<Coche> datos;

            try
            {
                if (tipo == 0)
                    datos = conexion.Select("select codcoche,cifm,nombre,modelo from coches;");
                else
                    datos = conexionS.Select("select codcoche,cifm,nombre,modelo from coches;");


                DataGrid.ItemsSource = datos;
            }
            catch (Exception ex)
            {
                
                lbEtiqueta.Content = "Estado: " + ex.Message;
            }

           
            
        }

        private void btnDesconectar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conexion.Desconectar();
                lbEtiqueta.Content = "Offline";
            }
            catch (Exception ex)
            {
                lbEtiqueta.Content = "Error: " + ex.Message;
            }
        }

        private void btnSP_Click(object sender, RoutedEventArgs e)
        {

            List<Coche> datos;
            try
            {
                datos = conexion.Rutina(5);

                DataGrid.ItemsSource = datos;
            }
            catch (Exception ex)
            {

                lbEtiqueta.Content = "Estado: " + ex.Message;
            }
           
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = null;
            DataGrid.Items.Clear();
        }

        private void rbtnMaria_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                conexion.Conectar(tbServidor.Text, tbBaseDatos.Text, tbUsuario.Text, tbContraseña.Password);

                lbEtiqueta.Content = "Estado: Conectado a MariaDB";
                tipo = 0;
                btnSP.IsEnabled = true;

            }
            catch (Exception ex)
            {
                lbEtiqueta.Content = "Estado: " + ex.Message;
            }
        }

        private void rbtnLite_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                conexionS.Conectar(null, tbBaseDatos.Text, null, null);
                tipo = 1;
                lbEtiqueta.Content = "Estado: Conectado a SQLite";
                btnSP.IsEnabled = false;
            }
            catch (Exception ex)
            {
                lbEtiqueta.Content = "Estado: " + ex.Message;
            }
        }
    }
}
