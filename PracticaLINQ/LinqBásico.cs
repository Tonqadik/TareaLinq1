// LINQ declaración de variables
using System;

// LINQ clases
LinqQueries queries = new LinqQueries();
Console.WriteLine("--- Impresión de todos los libros ---");
Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Titulo", "Páginas", "Publicación");
queries.ImprimirLibros();

Func<Book, bool> filtro = x => x.NPaginas > 250 && x.Titulo != null;
var filtrado = queries.GetCustomFilter(filtro);
Console.WriteLine("--- Impresión de todos los libros con más de 250 páginas ---");
Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Titulo", "Páginas", "Publicación");
foreach(var i in filtrado) Console.WriteLine(i.ToString());

Console.WriteLine("--- Todos los libros tiene el campo titulo ---");
Console.WriteLine("Todos los libros tienen titulo? {0}\n",queries.TienenTitulo());

// LINQ filtrado por titulo
var filtradoTitulo = queries.ContienenEnTitulo("JavaScript");
Console.WriteLine("--- Impresión de todos los libros que contienen en el titulo la palabra JavaScript ---");
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
    select: x => new Book{ Titulo = x.Titulo, NPaginas = x.NPaginas, FechaPublicacion = x.FechaPublicacion},
    where : x => x.NPaginas >= 250,
    order : x => x.FechaPublicacion,
    take  : 10,
    skip  : 0
    );
Console.WriteLine("--- Impresión dinámica ---");
Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Titulo", "Páginas", "Publicación");
foreach (var i in seleccion) Console.WriteLine(i.ToString());