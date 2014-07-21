using System;
using System.Collections.Generic;
using System.Linq;
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
using System.IO;
namespace Peliculas_Ideti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void leerArchivo() 
        {
           // StreamReader leer = new StreamReader();
        }

        private void radioButton_genero2_Checked(object sender, RoutedEventArgs e)
        {
            //Activo gropubox de genero y desactivo el de año
            groupBox_Generos.IsEnabled = true;
            groupBox_Año.IsEnabled = false;
        }

        private void radioButton_ano2_Checked(object sender, RoutedEventArgs e)
        {
            //Activo gropubox de año y desactivo el de genero
            groupBox_Año.IsEnabled = true;
            groupBox_Generos.IsEnabled = false;
        }

        private void button_Buscar_Click(object sender, RoutedEventArgs e)
        {
            //Valida que seleccione al menos un genero
            if (checkBox_Comedia.IsChecked == false && checkBox_Drama.IsChecked == false && checkBox_Ficcion.IsChecked == false)
            {
                MessageBox.Show("Selecciona al menos un genero!","Error de busqueda",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }

            //HOLAAAAAAAAA
            ///ASDASDADAD/AD
            ////AD/ADASDASD}
            //ASDASDASDASD
        }

       
    }
}
