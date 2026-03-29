using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentasLimpieza.core.Dtos;
using VentasLimpieza.core.Entities;
using VentasLimpieza.core.Interfaces;
using VentasLimpieza.Core.Interfaces;

namespace VentasLimpieza.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionController : ControllerBase
    {
        private readonly IDireccionRepository _direccionRepository;
        private readonly IMapper _mapper;

        public DireccionController(IDireccionRepository direccionRepository, IMapper mapper)
        {
            _direccionRepository = direccionRepository;
            _mapper = mapper;
        }

        #region sin dtos
        [HttpGet]
        public async Task<IActionResult> GetDirecciones()
        {
            var direcciones = await _direccionRepository.GetDireccionesAsync();
            return Ok(direcciones);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDireccionesById(int id)
        {
            var direccion = await _direccionRepository.GetDireccionByIdAsync(id);
            return Ok(direccion);
        }

        [HttpPost]
        public async Task<IActionResult> InsertDireccion(Direccion direccion)
        {
            await _direccionRepository.InsertDireccion(direccion);
            return Created($"api/direccion/{direccion.Id}", direccion);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDireccion(Direccion direccion)
        {
            await _direccionRepository.UpdateDireccion(direccion);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDireccion(Direccion direccion)
        {
            await _direccionRepository.DeleteDireccion(direccion);
            return NoContent();
        }
        #endregion

        #region conDtos
        [HttpGet("dto")]
        public async Task<IActionResult> GetDtoDirecciones()
        {
            var direcciones = await _direccionRepository.GetDireccionesAsync();
            var direccionesDto = direcciones.Select(d => new DireccionDto
            {
                Id = d.Id,
                UsuarioId = d.UsuarioId,
                Direccion1 = d.Direccion1,
                Ciudad = d.Ciudad,
                Provincia = d.Provincia,
                Pais = d.Pais,
                Principal = d.Principal
            });
            return Ok(direccionesDto);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetDtoDireccionById(int id)
        {
            var direccion = await _direccionRepository.GetDireccionByIdAsync(id);
            var direccionDto = new DireccionDto
            {
                Id = direccion.Id,
                UsuarioId = direccion.UsuarioId,
                Direccion1 = direccion.Direccion1,
                Ciudad = direccion.Ciudad,
                Provincia = direccion.Provincia,
                Pais = direccion.Pais,
                Principal = direccion.Principal
            };
            return Ok(direccionDto);
        }

        [HttpPost("dto")]
        public async Task<IActionResult> InsertDtoDireccion(DireccionDto direccionDto)
        {
            var direccion = new Direccion
            {
                Id = direccionDto.Id,
                UsuarioId = direccionDto.UsuarioId,
                Direccion1 = direccionDto.Direccion1,
                Ciudad = direccionDto.Ciudad,
                Provincia = direccionDto.Provincia,
                Pais = direccionDto.Pais,
                Principal = direccionDto.Principal
            };
            await _direccionRepository.InsertDireccion(direccion);
            return Created($"api/direccion/{direccion.Id}", direccion);
        }

        [HttpPut("dto/{id}")]
        public async Task<IActionResult> UpdateDtoDireccion(int id, [FromBody] DireccionDto direccionDto)
        {
            if (id != direccionDto.Id)
                return BadRequest("El ID de la dirección no coincide");

            var direccion = await _direccionRepository.GetDireccionByIdAsync(id);
            if (direccion == null)
                return NotFound("Dirección no encontrada");

            // Mapear valor del DTO a la entidad
            direccion.UsuarioId = direccionDto.UsuarioId;
            direccion.Direccion1 = direccionDto.Direccion1;
            direccion.Ciudad = direccionDto.Ciudad;
            direccion.Provincia = direccionDto.Provincia;
            direccion.Pais = direccionDto.Pais;
            direccion.Principal = direccionDto.Principal;

            await _direccionRepository.UpdateDireccion(direccion);
            return NoContent();
        }

        [HttpDelete("dto/{id}")]
        public async Task<IActionResult> DeleteDtoDireccion(int id)
        {
            var direccion = await _direccionRepository.GetDireccionByIdAsync(id);
            if (direccion == null)
                return NotFound("Dirección no encontrada");

            await _direccionRepository.DeleteDireccion(direccion);
            return NoContent();
        }
        #endregion

        #region Mapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetDtoMapperDirecciones()
        {
            var direcciones = await _direccionRepository.GetDireccionesAsync();
            var direccionesDto = _mapper.Map<IEnumerable<DireccionDto>>(direcciones);
            return Ok(direccionesDto);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetDtoMapperDireccionById(int id)
        {
            var direccion = await _direccionRepository.GetDireccionByIdAsync(id);
            var direccionDto = _mapper.Map<DireccionDto>(direccion);
            return Ok(direccionDto);
        }

        [HttpPost("dto/mapper")]
        public async Task<IActionResult> InsertDtoMapperDireccion(DireccionDto direccionDto)
        {
            var direccion = _mapper.Map<Direccion>(direccionDto);
            await _direccionRepository.InsertDireccion(direccion);
            return Created($"api/direccion/{direccion.Id}", direccion);
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateDtoMapperDireccion(int id, [FromBody] DireccionDto direccionDto)
        {
            if (id != direccionDto.Id)
                return BadRequest("El ID de la dirección no coincide");

            var direccion = await _direccionRepository.GetDireccionByIdAsync(id);
            if (direccion == null)
                return NotFound("Dirección no encontrada");

            // Mapear valores del DTO a la entidad existente
            _mapper.Map(direccionDto, direccion);

            await _direccionRepository.UpdateDireccion(direccion);
            return NoContent();
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteDtoMapperDireccion(int id)
        {
            var direccion = await _direccionRepository.GetDireccionByIdAsync(id);
            if (direccion == null)
                return NotFound("Dirección no encontrada");

            await _direccionRepository.DeleteDireccion(direccion);
            return NoContent();
        }
        #endregion
    }
}
