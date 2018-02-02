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

        public InscricaoController(ICampeonatoRepository campeonatoRepository, IFlashMessage flashMessage,
           IInscricaoRepository inscricaoRepository, IJogadorInscritoRepository jogadorInscritoRepository)
        {
            _campeonatoRepository = campeonatoRepository;
            _inscricaoRepository = inscricaoRepository;
            _flashMessage = flashMessage;
            _jogadorInscritoRepository = jogadorInscritoRepository;

        }

        public IActionResult IndexCampeonato() =>
    View(_campeonatoRepository.GetAll());

        public IActionResult Index(int id)
        {
            ViewBag.idCampeonato = id;
            return View(_inscricaoRepository.GetAll(id));
        }

        public IActionResult IndexJogador(int id, int idTime)
        {
            ViewBag.idCampeonato = id;
            return View(_jogadorInscritoRepository.GetAll(idTime, id));
        }

        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IDPreInscrito")] Inscricao _inscricao, int idCampeonato)
        {
            if (ModelState.IsValid)
            {
                _inscricaoRepository.Add(_inscricao);
                _flashMessage.Confirmation("Operação realizada com sucesso!");

                return RedirectToAction(nameof(Index), new { id = idCampeonato });
            }

            return View(_inscricao);
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


            return RedirectToAction(nameof(IndexJogador), new { id = _inscricao.Inscricao.PreInscricao.IDCampeonato, idTime = _inscricao.Inscricao.PreInscricao.IDTime });
        }
    }

}
