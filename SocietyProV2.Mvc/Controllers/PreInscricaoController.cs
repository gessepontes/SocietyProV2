using Microsoft.AspNetCore.Mvc;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using Vereyon.Web;

namespace SocietyProV2.Mvc.Controllers
{
    public class PreInscricaoController : Controller
    {
        private readonly ICampeonatoRepository _campeonatoRepository;
        private readonly IPreInscricaoRepository _preinscricaoRepository;
        private readonly IFlashMessage _flashMessage;
        private readonly ITimeRepository _timeRepository;


        public PreInscricaoController(ICampeonatoRepository campeonatoRepository, IFlashMessage flashMessage, ITimeRepository timeRepository,
           IPreInscricaoRepository preinscricaoRepository)
        {
            _timeRepository = timeRepository;
            _campeonatoRepository = campeonatoRepository;
            _preinscricaoRepository = preinscricaoRepository;
            _flashMessage = flashMessage;
        }

        public IActionResult IndexCampeonato() =>
    View(_campeonatoRepository.GetAll());

        public IActionResult Index(int id)
        {
            ViewBag.idCampeonato = id;
            return View(_preinscricaoRepository.GetAll(id));
        }

        public IActionResult Create(int id)
        {
            ViewBag.idCampeonato = id;
            ViewBag.ListaTime = _timeRepository.GetAllTimeDrop();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IDTime,IDCampeonato")] PreInscricao _preinscricao)
        {
            if (ModelState.IsValid)
            {
                _preinscricaoRepository.Add(_preinscricao);
                _flashMessage.Confirmation("Operação realizada com sucesso!");

                return RedirectToAction(nameof(Index), new { id = _preinscricao.IDCampeonato });
            }

            return View(_preinscricao);
        }

        public IActionResult Delete(int id)
        {
            var _preinscricao = _preinscricaoRepository.GetById(id);

            try
            {
                _preinscricaoRepository.Remove(_preinscricao);

                _flashMessage.Confirmation("Operação realizada com sucesso!");
            }
            catch (System.Exception)
            {
                _flashMessage.Danger("Este registro não pode ser apagado, sua inscrição ja foi efetivada!");
            }


            return RedirectToAction(nameof(Index), new { id = _preinscricao.IDCampeonato });
        }
    }

}
