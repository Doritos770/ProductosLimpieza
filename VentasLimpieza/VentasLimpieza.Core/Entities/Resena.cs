using System;
using System.Collections.Generic;

namespace VentasLimpieza.Infrastructure.Data;

public partial class Resena
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int ProductoId { get; set; }

    public int Calificacion { get; set; }

    public string? Comentario { get; set; }

    public DateOnly Fecha { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
