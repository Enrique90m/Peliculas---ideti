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
        //Listas dinamicos para guardar los datos de busqueda y del archivo
        List<Pelicula> peliculas = new List<Pelicula>();
        List<Pelicula> desplegar = new List<Pelicula>();

        public MainWindow()
        {
            InitializeComponent();
            leerArchivo();
        }

        public void leerArchivo() 
        {
            StreamReader leer = new StreamReader("peliculas.txt");
            String linea;
            String[] array;
            String titulo;
            String genero;
            int anio;
            while((linea = leer.ReadLine())!= null)
            {
                array = linea.Split('#');
                titulo = array[1];
                genero = array[0];
                anio = int.Parse(array[2]);
                peliculas.Add(new Pelicula(titulo,genero,anio));
            }

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
            //Valida que seleccione al menos un genero, o en caso contrario, que capture un año de busqueda
            if (checkBox_Comedia.IsChecked == false && checkBox_Drama.IsChecked == false && checkBox_Ficcion.IsChecked == false && groupBox_Generos.IsEnabled == true)
            {
                MessageBox.Show("Selecciona al menos un genero!","Error de busqueda",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            else
                if(string.IsNullOrEmpty(textBox_Ano2.Text))
                {
                    MessageBox.Show("Captura el año a buscar!", "Error de busqueda", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


        }

       
    }
}
