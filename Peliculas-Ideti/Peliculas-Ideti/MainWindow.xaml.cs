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
        int iIndice=0;

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

        /// <summary>
        /// Metodo para mostrar en los textbox de resultados, los datos de la pelicula buscada
        /// </summary>
        /// <param name="iIndice">Numero del item que se traera de la lista, es decir su indice</param>
        public void mMuestraDatosEnTextbox()
        {
            try
            {
                textBox_Nombre.Text = desplegar[iIndice].getTitulo();
                textBox_Genero.Text = desplegar[iIndice].getGenero();
                textBox_Ano.Text = desplegar[iIndice].getAnio().ToString();
            }
            catch(ArgumentOutOfRangeException x)
            {
                button_Atras.IsEnabled = false;
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
            //limplio la lista de peliculas a desplegar
            desplegar.Clear();

            //Valida que seleccione al menos un genero
            if (checkBox_Comedia.IsChecked == false && checkBox_Drama.IsChecked == false && checkBox_Ficcion.IsChecked == false && groupBox_Generos.IsEnabled == true)
            {
                MessageBox.Show("Selecciona al menos un genero!", "Error de busqueda", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox_Ano2.Text) && groupBox_Año.IsEnabled == true)
            {
                MessageBox.Show("Captura el año a buscar!", "Error de busqueda", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            //Ciclo para buscar peliculas que cumplan con el criterio
            for (int i = 0; i < peliculas.Count; i++ )
            {
                if (checkBox_Comedia.IsChecked == true)
                    if (peliculas.ElementAt(i).getGenero().Equals("comedia")) {
                        desplegar.Add(peliculas.ElementAt(i));
                    }

            }




            //Ya se tiene el arreglo con datos de busqueda, primero valido que tenga datos
            if (desplegar.Count == 0)
            {
                MessageBox.Show("No se encontraron peliculas!", "Mensaje de sistema", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //Si se tienen datos en el arreglo, por lo tanto muestro el primero en los textbox
            mMuestraDatosEnTextbox();

            //Activo el boton de siguiente
            button_Siguiente.IsEnabled = true;


        }

        private void button_Atras_Click(object sender, RoutedEventArgs e)
        {
            //Decrementa el indice que sirve como puntero en la lista y muestra el dato en el textbox, en caso de que sea menor que 0, desactiva este boton
            iIndice--;
            mMuestraDatosEnTextbox();
        }




       
    }
}
