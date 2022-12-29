using ConsoleApp1;

LinqQueries queries = new LinqQueries();

//todos los libros
//imprimirValores(queries.TodaLaColeccion());

//Console.WriteLine(queries.libroConFechaMasReciente().publishedDate.ToString());
//Console.WriteLine(queries.promedioPaginasLibrosMayoresA0());

imprimirValores(queries.joins());

//imprimirGrupo(queries.librosAgrupadosPorAnioApartirDel2000());

void imprimirValores(IEnumerable<Book> listadoLibros)
{
    Console.WriteLine("{0, -70} {1, 7} {2, 11} {3, 14}\n", "Titulo", "N. paginas", "Fecha publicacion", "estado");
    foreach(var item in listadoLibros)
    {
        Console.WriteLine("{0, -70} {1, 7} {2, 11} {3, 14}", item.title, item.pageCount, item.publishedDate.ToShortDateString(), item.status);
    }
}

//solo para imprimir group by
void imprimirGrupo(IEnumerable<IGrouping<int, Book>> listadoLibros)
{
    foreach (var grupo in listadoLibros)
    {
        Console.WriteLine("");
        Console.WriteLine($"Grupo: {grupo.Key}");
        Console.WriteLine("{0, -60}, {1, 15}, {2, 15}\n", "Titulo", "N. paginas", "Fecha publicacion");
        foreach(var item in grupo)
        {
            Console.WriteLine("{0, -60}, {1, 15}, {2, 15}", item.title, item.pageCount, item.publishedDate.Date);
        }
    }
}

void imprimirDiccionario(ILookup<char, Book> listaLibros, char letra)
{
    Console.WriteLine("{0, -60}, {1, 15}, {2, 15}\n", "Titulo", "N. paginas", "Fecha publicacion");
    foreach(var item in listaLibros[letra])
    {
        Console.WriteLine("{0, -60}, {1, 15}, {2, 15}", item.title, item.pageCount, item.publishedDate.Date.ToShortDateString());
    }
}
