// LINQ declaración de variables
using System;

var ListaNombres = new String[] {"Juan", "Carlos", "Torres", "James", "John" };

// Declarativa
// Imprime el nombre de torres
var item = ListaNombres.FirstOrDefault(p => p == "Torres");
Console.WriteLine("---Imprime los nombres que empiezan con J---");
Console.WriteLine(item);

// Imperativa
// Solo obtiene los nombres que empeizan con J
Console.WriteLine("---Imprime los nombres que empiezan con J---");
var nombres = ListaNombres.Where(a => a.StartsWith("J"));
foreach (var nomb in nombres)
{
    Console.WriteLine(nomb);
}

// Declaraciones where 
var ListaNumeros = new String[]{"Uno", "Dos", "Tres", "Cuatro", "Cinco", "Seis", "Siete", "Ocho", "Nueve", "Diez" };

// Encuentra aquellos cadenas que solo tengan res letras
var listaNumerosFiltrada = ListaNumeros.Where(ListaNumeros => ListaNumeros.Length == 4).ToList();

// Imprime los numeros que solo tengan 4 letras
Console.WriteLine("---Imprime los números con 4 letras---");
listaNumerosFiltrada.ForEach(num => Console.WriteLine(num));


Console.WriteLine();

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