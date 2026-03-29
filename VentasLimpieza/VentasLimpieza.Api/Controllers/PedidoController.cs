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
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public PedidoController(IPedidoRepository pedidoRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        #region sin dtos
        [HttpGet]
        public async Task<IActionResult> GetPedidos()
        {
            var pedidos = await _pedidoRepository.GetPedidosAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoById(int id)
        {
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(id);
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPedido(Pedido pedido)
        {
            await _pedidoRepository.InsertPedido(pedido);
            return Created($"api/pedido/{pedido.Id}", pedido);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePedido(Pedido pedido)
        {
            await _pedidoRepository.UpdatePedido(pedido);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePedido(Pedido pedido)
        {
            await _pedidoRepository.DeletePedido(pedido);
            return NoContent();
        }
        #endregion

        #region conDtos
        [HttpGet("dto")]
        public async Task<IActionResult> GetDtoPedidos()
        {
            var pedidos = await _pedidoRepository.GetPedidosAsync();
            var pedidosDto = pedidos.Select(p => new PedidoDto
            {
                Id = p.Id,
                UsuarioId = p.UsuarioId,
                ProductoId = p.ProductoId,
                FechaPedido = p.FechaPedido,
                Estado = p.Estado,
                CostoEnvio = p.CostoEnvio,
                MetodoPago = p.MetodoPago,
                CantidadProducto = p.CantidadProducto,
                PrecioUnitario = p.PrecioUnitario,
                Total = p.Total
            });
            return Ok(pedidosDto);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetDtoPedidoById(int id)
        {
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(id);
            var pedidoDto = new PedidoDto
            {
                Id = pedido.Id,
                UsuarioId = pedido.UsuarioId,
                ProductoId = pedido.ProductoId,
                FechaPedido = pedido.FechaPedido,
                Estado = pedido.Estado,
                CostoEnvio = pedido.CostoEnvio,
                MetodoPago = pedido.MetodoPago,
                CantidadProducto = pedido.CantidadProducto,
                PrecioUnitario = pedido.PrecioUnitario,
                Total = pedido.Total
            };
            return Ok(pedidoDto);
        }

        [HttpPost("dto")]
        public async Task<IActionResult> InsertDtoPedido(PedidoDto pedidoDto)
        {
            var pedido = new Pedido
            {
                Id = pedidoDto.Id,
                UsuarioId = pedidoDto.UsuarioId,
                ProductoId = pedidoDto.ProductoId,
                FechaPedido = pedidoDto.FechaPedido,
                Estado = pedidoDto.Estado,
                CostoEnvio = pedidoDto.CostoEnvio,
                MetodoPago = pedidoDto.MetodoPago,
                CantidadProducto = pedidoDto.CantidadProducto,
                PrecioUnitario = pedidoDto.PrecioUnitario,
                Total = pedidoDto.Total
            };
            await _pedidoRepository.InsertPedido(pedido);
            return Created($"api/pedido/{pedido.Id}", pedido);
        }

        [HttpPut("dto/{id}")]
        public async Task<IActionResult> UpdateDtoPedido(int id, [FromBody] PedidoDto pedidoDto)
        {
            if (id != pedidoDto.Id)
                return BadRequest("El ID del pedido no coincide");

            var pedido = await _pedidoRepository.GetPedidoByIdAsync(id);
            if (pedido == null)
                return NotFound("Pedido no encontrado");

            // Mapear valor del DTO a la entidad
            pedido.UsuarioId = pedidoDto.UsuarioId;
            pedido.ProductoId = pedidoDto.ProductoId;
            pedido.FechaPedido = pedidoDto.FechaPedido;
            pedido.Estado = pedidoDto.Estado;
            pedido.CostoEnvio = pedidoDto.CostoEnvio;
            pedido.MetodoPago = pedidoDto.MetodoPago;
            pedido.CantidadProducto = pedidoDto.CantidadProducto;
            pedido.PrecioUnitario = pedidoDto.PrecioUnitario;
            pedido.Total = pedidoDto.Total;

            await _pedidoRepository.UpdatePedido(pedido);
            return NoContent();
        }

        [HttpDelete("dto/{id}")]
        public async Task<IActionResult> DeleteDtoPedido(int id)
        {
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(id);
            if (pedido == null)
                return NotFound("Pedido no encontrado");

            await _pedidoRepository.DeletePedido(pedido);
            return NoContent();
        }
        #endregion

        #region Mapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetDtoMapperPedidos()
        {
            var pedidos = await _pedidoRepository.GetPedidosAsync();
            var pedidosDto = _mapper.Map<IEnumerable<PedidoDto>>(pedidos);
            return Ok(pedidosDto);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetDtoMapperPedidoById(int id)
        {
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(id);
            var pedidoDto = _mapper.Map<PedidoDto>(pedido);
            return Ok(pedidoDto);
        }

        [HttpPost("dto/mapper")]
        public async Task<IActionResult> InsertDtoMapperPedido(PedidoDto pedidoDto)
        {
            var pedido = _mapper.Map<Pedido>(pedidoDto);
            await _pedidoRepository.InsertPedido(pedido);
            return Created($"api/pedido/{pedido.Id}", pedido);
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateDtoMapperPedido(int id, [FromBody] PedidoDto pedidoDto)
        {
            if (id != pedidoDto.Id)
                return BadRequest("El ID del pedido no coincide");

            var pedido = await _pedidoRepository.GetPedidoByIdAsync(id);
            if (pedido == null)
                return NotFound("Pedido no encontrado");

            // Mapear valores del DTO a la entidad existente
            _mapper.Map(pedidoDto, pedido);

            await _pedidoRepository.UpdatePedido(pedido);
            return NoContent();
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteDtoMapperPedido(int id)
        {
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(id);
            if (pedido == null)
                return NotFound("Pedido no encontrado");

            await _pedidoRepository.DeletePedido(pedido);
            return NoContent();
        }
        #endregion
    }
}