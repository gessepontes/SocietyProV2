using Microsoft.AspNetCore.Mvc;
using SocietyProV2.Domain.Diversos;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System;
using Vereyon.Web;

namespace SocietyProV2.Mvc.Controllers
{
    public class PartidaCampeonatoController : Controller
    {
        private readonly ICampeonatoRepository _campeonatoRepository;
        private readonly ICampoRepository _campoRepository;
        private readonly ICampoItemRepository _campoItemRepository;
        private readonly IInscricaoRepository _inscricaoRepository;
        private readonly IFlashMessage _flashMessage;
        private readonly IPartidaCampeonatoRepository _partidaCampeonatoRepository;

        public PartidaCampeonatoController(ICampeonatoRepository campeonatoRepository, IFlashMessage flashMessage, ICampoRepository campoRepository,
            ICampoItemRepository campoItemRepository,
           IInscricaoRepository inscricaoRepository, IPartidaCampeonatoRepository partidaCampeonatoRepository)
        {
            _campeonatoRepository = campeonatoRepository;
            _campoRepository = campoRepository;
            _campoItemRepository = campoItemRepository;
            _inscricaoRepository = inscricaoRepository;
            _flashMessage = flashMessage;
            _partidaCampeonatoRepository = partidaCampeonatoRepository;

        }

        public IActionResult IndexCampeonato() =>
    View(_campeonatoRepository.GetAll());

        public IActionResult Index(int id)
        {
            ViewBag.idCampeonato = id;
            return View(_partidaCampeonatoRepository.GetAll(id));
        }

        public IActionResult Create(int id)
        {
            ViewBag.idCampeonato = id;
            ViewBag.ListaCampo = _campoRepository.GetAllCampoDrop();
            ViewBag.ListaCampoItem = _campoItemRepository.GetAll();
            ViewBag.ListaInscrito = _inscricaoRepository.GetDropAll(id);
            ViewBag.ListaRodada = Diverso.listaRodada();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IDPreInscrito")] PartidaCampeonato _partidaCampeonato, int idCampeonato)
        {
            if (ModelState.IsValid)
            {
                _partidaCampeonatoRepository.Add(_partidaCampeonato);
                _flashMessage.Confirmation("Operação realizada com sucesso!");

                return RedirectToAction(nameof(Index), new { id = idCampeonato });
            }

            return View(_partidaCampeonato);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var _partidaCampeonato = _partidaCampeonatoRepository.GetById(id);
            if (_partidaCampeonato == null)
                return NotFound();

            //ViewBag.ListaPessoa = _pessoaRepository.GetAllPessoaDrop();
            //ViewBag.ListaCidade = _cidadeRepository.GetAll();

            return View(_partidaCampeonato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,NOME,ENDERECO,TELEFONE,VALOR,VALORMENSAL,STATUS,DATACADASTRO,SOCIETY,CAMPO11,AGENDAMENTO,LOGO,IDPESSOA,IDCIDADE")] PartidaCampeonato _partidaCampeonato, int idCampeonato)
        {
            if (id != _partidaCampeonato.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _partidaCampeonatoRepository.Update(_partidaCampeonato);

                }
                catch (Exception)
                {
                    if (!PartidaCampeonatoExists(_partidaCampeonato.ID))
                        return NotFound();
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_partidaCampeonato);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var _partidaCampeonato = _partidaCampeonatoRepository.GetById(id);
            if (_partidaCampeonato == null)
                return NotFound();

            return View(_partidaCampeonato);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, int idCampeonato)
        {
            var _partidaCampeonato = _partidaCampeonatoRepository.GetById(id);

            try
            {
                _partidaCampeonatoRepository.Remove(_partidaCampeonato);

                _flashMessage.Confirmation("Operação realizada com sucesso!");
            }
            catch (Exception)
            {
                _flashMessage.Danger("Este registro não pode ser apagado, sua inscrição ja foi efetivada!");
            }


            return RedirectToAction(nameof(Index), new { id = idCampeonato });
        }

        public IActionResult ListaCampoItem(int IDCAMPO) => Json(_campoItemRepository.GetByIdCampo(IDCAMPO));

        private bool PartidaCampeonatoExists(int id) =>
            _partidaCampeonatoRepository.GetById(id) != null;


    }

}
