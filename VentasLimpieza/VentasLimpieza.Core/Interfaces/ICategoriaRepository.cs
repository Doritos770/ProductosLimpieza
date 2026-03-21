using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.core.Entities;

namespace VentasLimpieza.Core.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetCategoriasAsync();
        Task<Categoria> GetCategoriaAsync(int id);
        Task InsertUsuario(Categoria categoria);
        Task UpdateUsuario(Categoria categoria);
        Task DeleteUsuario(Categoria categoria);
    }
}
