using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
    public int NPaginas { get; set;  }
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
