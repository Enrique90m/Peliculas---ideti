using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peliculas_Ideti
{
    class Pelicula
    {
        private String titulo;
        private String genero;
        private int anio;

        public Pelicula() 
        {

        }

        public Pelicula(String titulo, String genero, int anio) 
        {
            this.titulo = titulo;
            this.genero = genero;
            this.anio = anio;
        }

        public String getTitulo() 
        {
            return titulo;
        }

        public String getGenero() 
        {
            return genero;
        }

        public int getAnio() 
        {
            return anio;
        }

        public void setTitulo(String titulo )
        {
            this.titulo = titulo;
        }

        public void setGenero(String genero) 
        {
            this.genero = genero;
        }

        public void setAnio(int anio) 
        {
            this.anio = anio;
        }

    }
}
