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
            StreamReader leer = new StreamReader("peliculas.txt", Encoding.GetEncoding("iso8859-1"));
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
                button_Siguiente.IsEnabled = false;
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
            //limpio la lista de peliculas a desplegar
            desplegar.Clear();
            // limpio los campos de texto
            textBox_Nombre.Text = "";
            textBox_Genero.Text = "";
            textBox_Ano.Text = "";
            //se deshabilitan los botones 
            button_Atras.IsEnabled = false;
            button_Siguiente.IsEnabled = false;
            //Inicializo en 0 el indice 
            iIndice = 0;
            groupBox_Resultados.Header = "Resultados " ;
            //Valida que seleccione al menos un genero
            if (checkBox_Comedia.IsChecked == false && checkBox_Drama.IsChecked == false && checkBox_Ficcion.IsChecked == false && groupBox_Generos.IsEnabled == true)
            {
                MessageBox.Show("Selecciona al menos un genero!", "Error de busqueda", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Valida que halla capturado un ano, que no tenga caracteres y que no tenga demasiados numeros
            if (string.IsNullOrEmpty(textBox_Ano2.Text) && groupBox_Año.IsEnabled == true)
            {
                MessageBox.Show("Captura el año a buscar!", "Error de busqueda", MessageBoxButton.OK, MessageBoxImage.Error);
                textBox_Ano2.Focus();
                return;
            }
            else
                if (groupBox_Año.IsEnabled == true)
                    try
                    {
                        int.Parse(textBox_Ano2.Text);
                    }
                    catch (FormatException t)
                    {
                        MessageBox.Show("Solo puede ingresar carecteres numericos!", "Mensaje de sistema", MessageBoxButton.OK, MessageBoxImage.Information);
                        textBox_Ano2.Focus();
                        return;
                    }
                    catch (OverflowException t)
                    {
                        MessageBox.Show("Captura un numero mas pequeno!", "Mensaje de sistema", MessageBoxButton.OK, MessageBoxImage.Information);
                        textBox_Ano2.Focus();
                        return;
                    }


            //Agrego validacion que separa las busquedas, antes se realiaba la busqueda por año o por genero en el mismo ciclo, y lo separo
            //porque cuando buscabas por año y algun checkbox estaba checked, se traia todos los que cumplian con el genero y el año, siendo que solo deben de ser los del año
            if(groupBox_Generos.IsEnabled == true)
                //Busca por genero
                for (int i = 0; i < peliculas.Count; i++ )
                {
                    //si el checbox comedia esta habilitado y la pelicula contiene el genero comedia entonces se agrega a la lista de desplegar
                    if (checkBox_Comedia.IsChecked == true && peliculas.ElementAt(i).getGenero().ToLower().Equals("comedia"))
                    {
                        desplegar.Add(peliculas.ElementAt(i));
                        continue;
                    }
                    //si el checbox drama esta habilitado y la pelicula contiene el genero drama entonces se agrega a la lista de desplegar
                    if(checkBox_Drama.IsChecked == true && peliculas.ElementAt(i).getGenero().ToLower().Equals("drama"))
                    {
                        desplegar.Add(peliculas.ElementAt(i));
                        continue;
                    }
                    //si el checbox ficcion esta habilitado y la pelicula contiene el genero ficcion entonces se agrega a la lista de desplegar
                    if(checkBox_Ficcion.IsChecked == true && peliculas.ElementAt(i).getGenero().ToLower().Equals("ficcion"))
                    {
                        desplegar.Add(peliculas.ElementAt(i));
                        continue;
                    }
                }
            else
                //Busca por año
                for (int i = 0; i < peliculas.Count; i++)
                    //si el checbox anio esta habilitado y la pelicula contiene el genero anio entonces se agrega a la lista de desplegar
                    if (groupBox_Año.IsEnabled && int.Parse(textBox_Ano2.Text) == peliculas.ElementAt(i).getAnio())
                        desplegar.Add(peliculas.ElementAt(i));
                

            //Ya se tiene el arreglo con datos de busqueda, primero valido que tenga datos
            if (desplegar.Count == 0)
            {
                MessageBox.Show("No se encontraron peliculas!", "Mensaje de sistema", MessageBoxButton.OK, MessageBoxImage.Information);
                button_Siguiente.IsEnabled = false;
                return;
            }

            //Si se tienen datos en el arreglo, por lo tanto muestro el primero en los textbox
            mMuestraDatosEnTextbox();

            //Activo el boton de siguiente
            // si hay mas de un registro // rafaGithubero
            if (desplegar.Count > 1)
            button_Siguiente.IsEnabled = true;

            groupBox_Resultados.Header = "Resultados " + desplegar.Count;

        }

        private void button_Atras_Click(object sender, RoutedEventArgs e)
        {
            //Decrementa el indice que sirve como puntero en la lista y muestra el dato en el textbox, en caso de que sea menor que 0, desactiva este boton
            iIndice--;
            mMuestraDatosEnTextbox();
            // activa el botón siguiente
            button_Siguiente.IsEnabled = true;
            // si ya no hay mas datos se debe desactivar el botón  atras // rafaGithubero
            if (iIndice == 0)
                button_Atras.IsEnabled = false;
        }

        private void button_Siguiente_Click(object sender, RoutedEventArgs e)
        {
            //Incrementa el indice que sirve como puntero en la lista y muestra el dato en el textbox
            iIndice++;
            mMuestraDatosEnTextbox();
            // activa el botón atras
            button_Atras.IsEnabled = true;
            // si ya no hay mas datos se debe desactivar el botón  siguiente // rafaGithubero
            if (iIndice == desplegar.Count-1)
                button_Siguiente.IsEnabled = false;
        }

        private void button_Cerrar_Click(object sender, RoutedEventArgs e)
        {
            // cierra la aplicacion
            Close();
        }

    }
}
