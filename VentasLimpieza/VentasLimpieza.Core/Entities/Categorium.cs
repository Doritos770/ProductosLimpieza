using System;
using System.Collections.Generic;

namespace VentasLimpieza.Infrastructure.Data;

public partial class Categorium
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? ImagenUrl { get; set; }
}
