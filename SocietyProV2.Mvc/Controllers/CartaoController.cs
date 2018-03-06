using Microsoft.AspNetCore.Mvc;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using Vereyon.Web;

namespace SocietyProV2.Mvc.Controllers
{
    public class CartaoController : Controller
    {
        private readonly ICampeonatoRepository _campeonatoRepository;
        private readonly ICartaoRepository _cartaRepository;
        private readonly IFlashMessage _flashMessage;


        public CartaoController(ICartaoRepository cartaRepository, IFlashMessage flashMessage,
            ICampeonatoRepository campeonatoRepository)
        {
            _campeonatoRepository = campeonatoRepository;
            _cartaRepository = cartaRepository;
            _flashMessage = flashMessage;
        }

        public IActionResult Index(int id)
        {
            ViewBag.Campeonato = _campeonatoRepository.GetById(id);
            return View(_cartaRepository.GetAllByCampeonato(id));
        }


        public IActionResult Create()
        {
            //ViewBag.ListaPessoa = _pessoaRepository.GetAllPessoaDrop();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IDJogadorSumula,iTipoCartao")]  Cartao _cartao)
        {
            if (ModelState.IsValid)
            {
                _cartaRepository.Add(_cartao);
                _flashMessage.Confirmation("Operação realizada com sucesso!");

                return RedirectToAction(nameof(Index));
            }

            return View(_cartao);
        }

        public IActionResult Delete(int? id)
        {
            //Delete
            if (id == null)
                return NotFound();

            var _campeonato = _cartaRepository.GetById(id);
            if (_campeonato == null)
                return NotFound();

            return View(_campeonato);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var _campeonato = _cartaRepository.GetById(id);
            _cartaRepository.Remove(_campeonato);

            _flashMessage.Confirmation("Operação realizada com sucesso!");

            return RedirectToAction(nameof(Index));
        }

        private bool CampeonatoExists(int id) =>
    _cartaRepository.GetById(id) != null;
    }

}
