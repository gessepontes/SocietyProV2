using Microsoft.AspNetCore.Mvc;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using Vereyon.Web;

namespace SocietyProV2.Mvc.Controllers
{
    public class InscricaoController : Controller
    {
        private readonly ICampeonatoRepository _campeonatoRepository;
        private readonly IInscricaoRepository _inscricaoRepository;
        private readonly IFlashMessage _flashMessage;
        private readonly IJogadorInscritoRepository _jogadorInscritoRepository;
        private readonly ITimeRepository _timeRepository;

        public InscricaoController(ICampeonatoRepository campeonatoRepository, IFlashMessage flashMessage, ITimeRepository timeRepository,
           IInscricaoRepository inscricaoRepository, IJogadorInscritoRepository jogadorInscritoRepository)
        {
            _campeonatoRepository = campeonatoRepository;
            _inscricaoRepository = inscricaoRepository;
            _flashMessage = flashMessage;
            _jogadorInscritoRepository = jogadorInscritoRepository;
            _timeRepository = timeRepository;
        }

        public IActionResult IndexCampeonato() =>
    View(_campeonatoRepository.GetAll());

        public IActionResult Index(int id)
        {
            ViewBag.idCampeonato = id;
            return View(_inscricaoRepository.GetAll(id));
        }

        public IActionResult Create(int id)
        {
            ViewBag.idCampeonato = id;
            ViewBag.ListaTime = _timeRepository.GetAllTimeDrop();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IDTime,IDCampeonato")] Inscricao _inscricao)
        {
            if (ModelState.IsValid)
            {
                _inscricaoRepository.Add(_inscricao);
                _flashMessage.Confirmation("Operação realizada com sucesso!");

                return RedirectToAction(nameof(Index), new { id = _inscricao.IDCampeonato });
            }

            return View(_inscricao);
        }

        public IActionResult IndexJogador(int id, int idTime)
        {
            ViewBag.idCampeonato = id;
            return View(_jogadorInscritoRepository.GetAll(idTime, id));
        }

        public IActionResult Delete(int id, int idCampeonato)
        {
            var _inscricao = _inscricaoRepository.GetById(id);

            try
            {
                _inscricaoRepository.Remove(_inscricao);

                _flashMessage.Confirmation("Operação realizada com sucesso!");
            }
            catch (System.Exception)
            {
                _flashMessage.Danger("Este registro não pode ser apagado, sua inscrição ja foi efetivada!");
            }


            return RedirectToAction(nameof(Index), new { id = idCampeonato });
        }

        public IActionResult Ativar(int id, int idCampeonato)
        {        
            try
            {
                _inscricaoRepository.Ativar(id); ;

                _flashMessage.Confirmation("Operação realizada com sucesso!");
            }
            catch (System.Exception)
            {
                _flashMessage.Danger("Erro ao realizar a operação!");
            }


            return RedirectToAction(nameof(Index), new { id = idCampeonato });
        }


        [ValidateAntiForgeryToken]
        public IActionResult CreateJogador([Bind("IDJogador,IDInscrito,dDataDispensa")] JogadorInscrito _jogadorInscrito, int idTime,int idCampeonato)
        {
            if (ModelState.IsValid)
            {
                _jogadorInscritoRepository.Add(_jogadorInscrito);
                _flashMessage.Confirmation("Operação realizada com sucesso!");

                return RedirectToAction(nameof(IndexJogador), new { id = idCampeonato, idTime });
            }

            return View(_jogadorInscrito);
        }

        public IActionResult DeleteJogador(int id)
        {
            var _inscricao = _jogadorInscritoRepository.GetById(id);

            try
            {
                _jogadorInscritoRepository.Remove(_inscricao);

                _flashMessage.Confirmation("Operação realizada com sucesso!");
            }
            catch (System.Exception)
            {
                _flashMessage.Danger("Este registro não pode ser apagado, sua inscrição ja foi efetivada!");
            }


            return RedirectToAction(nameof(IndexJogador), new { id = _inscricao.Inscricao.IDCampeonato, idTime = _inscricao.Inscricao.IDTime });
        }
    }

}
