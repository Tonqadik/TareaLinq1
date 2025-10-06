using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

// Creación de la clase Book, que guarda la ínformación del archivo
public class Book
{
    private string? _titulo;
    private int _nPaginas;
    private DateOnly _fechaPublicacion;
    private string? _imagenURl;
    private string? _descripcion;
    private string? _status;
    private string[]? _autor;
    private string[]? _categorias;

    public string? Titulo { get; set; }
    public int NPaginas { get; set; }
    public DateOnly FechaPublicacion { get; set; }
    public string? ImagenURl { get; set; }
    public string? Descripcion { get; set; }
    public string? Status { get; set; }
    public string[]? Autor { get; set; }
    public string[]? Categorias { get; set; }

    public Book()
    {
        // Constructor vacío para inicialización básica
    }

    public override string ToString()
    {
        return string.Format("{0,-60} {1,15} {2,15}\n",
            this.Titulo,
            this.NPaginas,
            this.FechaPublicacion.ToString("yyyy-MM-dd"));
    }
}

// LINQ Clase querry
public class LinqQueries
{
    private readonly List<Book> coleccionLibros;
    private const string ruta = "Books.json";

    // Constructor que se encarga de inicializar la carga de datos del archivo Books.json
    public LinqQueries()
    {
        coleccionLibros = new List<Book>();
        if (File.Exists(ruta))
            using (StreamReader reader = new StreamReader(ruta))
            {
                string json = reader.ReadToEnd();
                List<Book> libros = JsonSerializer.Deserialize<List<Book>>(json, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
                if (libros != null && libros.Any())
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
    public IEnumerable<Book> ContienenEnTitulo(string campo)
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

    // LINQ para selector dinámico
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

// Clase principal
public class Program
{
    static void Main(string[] args)
    {
        // LINQ clases
        LinqQueries queries = new LinqQueries();
        Console.WriteLine("--- Impresión de todos los libros ---");
        Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Titulo", "Páginas", "Publicación");
        queries.ImprimirLibros();

        // LINQ que filtro solo los libros con más de 250 páginas
        Func<Book, bool> filtro = x => x.NPaginas > 250 && x.Titulo != null;
        var filtrado = queries.GetCustomFilter(filtro);
        Console.WriteLine("--- Impresión de todos los libros con más de 250 páginas ---");
        Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Titulo", "Páginas", "Publicación");
        foreach (var i in filtrado) Console.WriteLine(i.ToString());

        // LINQ con el operador ALL, que revisa si todos los libros tiene titulo
        Console.WriteLine("--- Todos los libros tiene el campo titulo ---");
        Console.WriteLine("Todos los libros tienen titulo? {0}\n", queries.TienenTitulo());

        // LINQ filtrado por titulo
        var filtradoTitulo = queries.ContienenEnTitulo("C#");
        Console.WriteLine("--- Impresión de todos los libros que contienen en el titulo la palabra C# ---");
        Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Titulo", "Páginas", "Publicación");
        foreach (var i in filtradoTitulo) Console.WriteLine(i.ToString());

        // LINQ filtrado por fecha asc
        var filtradoFechaAsc = queries.OrdenarPorFechaAsc();
        Console.WriteLine("--- Impresión de todos los libros por fecha de manera descendente ---");
        Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Titulo", "Páginas", "Publicación");
        foreach (var i in filtradoFechaAsc) Console.WriteLine(i.ToString());

        // LINQ filtrado por fecha desc
        var filtradoFechaDesc = queries.OrdenarPorFechaDesc();
        Console.WriteLine("--- Impresión de todos los libros por fecha de manera descendente ---");
        Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Titulo", "Páginas", "Publicación");
        foreach (var i in filtradoFechaDesc) Console.WriteLine(i.ToString());

        // LINQ filtrado por los primeros libros prestados, excepto el primero
        var filtradoTakeSkip = queries.ObtenerPrestadoRecientes();
        Console.WriteLine("--- Impresión de los 3 libros recientes prestados ---");
        Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Titulo", "Páginas", "Publicación");
        foreach (var i in filtradoTakeSkip) Console.WriteLine(i.ToString());

        // LINQ selección dinámica de datos
        var seleccion = queries.GetCustomBooks(
            select: x => new Book { Titulo = x.Titulo, NPaginas = x.NPaginas, FechaPublicacion = x.FechaPublicacion },
            where: x => x.NPaginas >= 250,
            order: x => x.FechaPublicacion,
            take: 10,
            skip: 0
            );
        Console.WriteLine("--- Impresión dinámica ---");
        Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Titulo", "Páginas", "Publicación");
        foreach (var i in seleccion) Console.WriteLine(i.ToString());
    }
}

