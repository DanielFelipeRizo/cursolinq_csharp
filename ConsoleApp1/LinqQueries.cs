using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class LinqQueries
    {
        private List<Book> librosCollection = new List<Book>();

        public LinqQueries()
        {
            using (StreamReader reader = new StreamReader("books.json"))
            {
                string json = reader.ReadToEnd();
                this.librosCollection = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(json, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;
            }
        }
        public IEnumerable<Book> TodaLaColeccion()
        {
            return librosCollection;
        }

        public IEnumerable<Book> librosDespuesAnio2000()
        {
            //extension method
            //return librosCollection.Where( p => p.publishedDate.Year>2000);

            //con query expresion
            return from l in librosCollection where l.publishedDate.Year > 2000 select l;
        }

        public IEnumerable<Book> librosMayores250PagTituloContieneInAction()
        {
            //extencion method
            return librosCollection.Where(p => p.pageCount > 250).Where(p => p.title.Contains("in Action"));
            //return librosCollection.Where(p => p.pageCount > 250 && p.title.Contains("in Action"));

            //query expresion
            //return from l in librosCollection where l.pageCount > 250 && l.title.Contains("in Action") select l;
        }

        public bool verificarLibrosConEstado()
        {
            return librosCollection.All(p => p.status != "");
        }

        public bool verificarAlgunLibroPublicadoEn2005()
        {
            return librosCollection.Any(p => p.publishedDate.Year == 2005);
        }

        public IEnumerable<Book> librosCategoriaPython()
        {
            return librosCollection.Where(p => p.categories.Contains("Python"));
        }

        public IEnumerable<Book> librosCategoriaJavaXnombreAsc()
        {
            return librosCollection.Where(p => p.categories.Contains("Java")).
                OrderBy(p => p.title);
        }
        public IEnumerable<Book> librosMayores450PagOrdXnumPaginaDesc()
        {
            return librosCollection.Where(p => p.pageCount > 450).
                OrderByDescending(p => p.pageCount);

        }
        public IEnumerable<Book> Primeros3LibrosCategoriaJavaXfechaPublicacion()
        {
             return librosCollection.Where(p => p.categories.Contains("Java"))
                .OrderByDescending(p => p.publishedDate).Take(3);
        }

        public IEnumerable<Book> libros3y4DelosMayoresA400Pag()
        {
            return librosCollection.Where(p => p.pageCount > 400)
               .OrderBy(p => p.pageCount).Take(4).Skip(2);
        }

        //seleccionando las columnas requeridas
        public IEnumerable<Book> tresPrimerosLibros()
        {
            return librosCollection.Take(3).Select(p => new Book() { title = p.title, pageCount = p.pageCount});
        }

        public int cantidadDeLibrosEntre200y500Paginas()
        {
            //return librosCollection.Where(p => p.pageCount >= 200 && p.pageCount <= 500).Count();
            
            //forma optima
            return librosCollection.Count(p => p.pageCount >= 200 && p.pageCount <= 500);
        }

        public DateTime menorFechaPublicacion()
        {
            return librosCollection.Min(p => p.publishedDate);
        }

        public int PaginasDeLibroConMasPaginas()
        {
            return librosCollection.Max(p => p.pageCount);
        }

        public Book libroConMenorNumDePaginas()
        {
            //return librosCollection.Where(p => p.pageCount>0)
            //.OrderBy(p => p.pageCount).Take(1);
            return librosCollection.Where(p => p.pageCount > 0)
            .MinBy(p => p.pageCount);
        }

        public Book libroConFechaMasReciente()
        {
            return librosCollection.MaxBy(p => p.publishedDate);
        }

        public int sumaPaginasLibrosEntre0a500Pag()
        {
            return librosCollection.Where(p => p.pageCount >=0 && p.pageCount <= 500)
                .Sum(p => p.pageCount);
        }

        public string tituloLibrosDespuesDe2015()
        {
            return librosCollection.Where(p => p.publishedDate.Year > 2010)
                .Aggregate("", (titulosLibros, next) =>
                {
                    if (titulosLibros != string.Empty)
                        titulosLibros = titulosLibros + " - " + next.title;
                    else
                        titulosLibros = titulosLibros + next.title;

                    return titulosLibros;
                });
        }

        public double promedioCaracteresTitulos()
        {
            return librosCollection.Average(p => p.title.Length);
        }

        public double promedioPaginasLibrosMayoresA0()
        {
            return librosCollection.Where(p => p.pageCount>0).Average(p => p.pageCount);
        }

        public IEnumerable<IGrouping<int, Book>> librosAgrupadosPorAnioApartirDel2000()
        {
            return librosCollection.Where(p => p.publishedDate.Year >= 2000)
                .GroupBy(p => p.publishedDate.Year);
        }

        public ILookup<char, Book> buscarLibroPorLetra()
        {
            return librosCollection.ToLookup(p => p.title[0], p => p);
        }

        public IEnumerable<Book> joins()
        {
            return librosCollection.Where(p => p.pageCount > 500)
                .Join(librosCollection.Where(p => p.publishedDate.Year > 2005), 
                mayorA500=>mayorA500.title, despues2005=>despues2005.title, (despues2005, mayorA500) => despues2005);
        }
    }
}
