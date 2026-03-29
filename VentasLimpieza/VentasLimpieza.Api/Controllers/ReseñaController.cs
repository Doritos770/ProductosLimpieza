using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VentasLimpieza.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VentasLimpieza.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    //api/reseña
    public class ReseñaController : ControllerBase
    {
        private readonly IReseñaRepository _reseñaRepository;
        private readonly IMapper _mapper;

        public ReseñaController(IReseñaRepository reseñaRepository, IMapper mapper)
        {
            _reseñaRepository = reseñaRepository;
            _mapper = mapper;
        }
    }
}
