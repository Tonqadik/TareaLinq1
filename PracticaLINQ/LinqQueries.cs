using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


public class LinqQueries
{
    private readonly List<Book> coleccionLibros;
    private const string ruta = "Books.json";

    // Constructor que se encarga de inicializar la carga de datos del archivo Books.json
    public LinqQueries()
    {
        coleccionLibros = new List<Book>();
        if(File.Exists(ruta))
            using(StreamReader reader  = new StreamReader(ruta))
            {
                string json = reader.ReadToEnd();
                List<Book> libros = JsonSerializer.Deserialize<List<Book>>(json, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
                if (libros != null && libros.Any() )
                    this.coleccionLibros = libros;
            }

    }

    // Retorna la colección completa
    public IEnumerable<Book> GetLibros => this.coleccionLibros;
    
    // Imprime todos los libros
    public void ImprimirLibros() => this.coleccionLibros.ForEach(x => Console.WriteLine(x.ToString()));

    public IEnumerable<Book> GetCustomFilter(Func<Book, bool> filtro)
        => this.coleccionLibros.Where(filtro).OrderBy(x => x.FechaPublicacion);
    
    // Linq con el operador ALL
    public bool TienenTitulo()
    {
        var resultado = this.coleccionLibros.All(b =>
            !string.IsNullOrWhiteSpace(b.Titulo)
            );

        return resultado;
    }

    // LINQ con el operador Contains
    public IEnumerable<Book> ContienenEnTitulo(String campo)
    {
        var resultado = from b in this.coleccionLibros where b.Titulo.Contains(campo) select b;
        return resultado;
    }

    // LINQ con el operador OrderByDescending
    public IEnumerable<Book> OrdenarPorFechaDesc()
    {
        return this.coleccionLibros.OrderByDescending(b => b.FechaPublicacion);

    }

    // LINQ con el operador OrderBy
    public IEnumerable<Book> OrdenarPorFechaAsc()
    {
        return this.coleccionLibros.OrderBy(b => b.FechaPublicacion);

    }

    // LINQ con el operador Take y Skip, para obtener los tres libros prestados más recientes, excepto el primero
    public IEnumerable<Book> ObtenerPrestadoRecientes()
    {
        return this.coleccionLibros
           .OrderByDescending(b => b.FechaPublicacion)
           .Take(3)
           .Skip(1);
    }

    public IEnumerable<dynamic> GetCustomBooks(
        Func<Book, dynamic> select,
        Func<Book, bool> where,
        Func<Book, object> order,
        int take,
        int skip = 0)
    {
        return this.coleccionLibros
            .Where(where)
            .OrderBy(order)
            .Take(take)
            .Select(select);
    }
}

