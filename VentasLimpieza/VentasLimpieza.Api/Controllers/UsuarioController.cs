using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentasLimpieza.core.Entities;
using VentasLimpieza.core.Interfaces;

namespace VentasLimpieza.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository) 
        {
            _usuarioRepository = usuarioRepository;
        }

        #region sin dtos
        [HttpGet]
        public async Task<IActionResult> GetUsuario()
        {
            var usuarios = await _usuarioRepository.GetUsuariosAsync();
            return Ok(usuarios);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUsuario(Usuario usuario)
        {
            await _usuarioRepository.InsertUsuario(usuario);
            return Created($"api/usuario/{usuario.Id}", usuario);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUsuario(Usuario usuario)
        {
            await _usuarioRepository.UpdateUsuario(usuario);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsuario(Usuario usuario)
        {
            await _usuarioRepository.DeleteUsuario(usuario);
            return NoContent();
        }
        #endregion
    }
}
