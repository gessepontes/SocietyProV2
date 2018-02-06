using Microsoft.AspNetCore.Mvc;
using SocietyProV2.Domain.Interfaces.Repositories;

namespace SocietyProV2.Mvc.Controllers
{
    public class BidController : Controller
    {
        private readonly ICampeonatoRepository _campeonatoRepository;
        private readonly IJogadorInscritoRepository _jogadorInscritoRepository;


        public BidController(ICampeonatoRepository campeonatoRepository,
            IJogadorInscritoRepository jogadorInscritoRepository)
        {
            _campeonatoRepository = campeonatoRepository;
            _jogadorInscritoRepository = jogadorInscritoRepository;
        }

        public IActionResult Index() =>
            View(_campeonatoRepository.GetAll());

        public IActionResult Details(int id) =>
            View(_jogadorInscritoRepository.BidDetails(id));
    }

}
