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
        private readonly IJogadorPartidaCampeonatoRepository _jogadorPartidaCampeonatoRepository;

        public PartidaCampeonatoController(ICampeonatoRepository campeonatoRepository, IFlashMessage flashMessage, ICampoRepository campoRepository,
            ICampoItemRepository campoItemRepository, IJogadorPartidaCampeonatoRepository jogadorPartidaCampeonatoRepository,
        IInscricaoRepository inscricaoRepository, IPartidaCampeonatoRepository partidaCampeonatoRepository)
        {
            _campeonatoRepository = campeonatoRepository;
            _campoRepository = campoRepository;
            _campoItemRepository = campoItemRepository;
            _inscricaoRepository = inscricaoRepository;
            _flashMessage = flashMessage;
            _partidaCampeonatoRepository = partidaCampeonatoRepository;
            _jogadorPartidaCampeonatoRepository = jogadorPartidaCampeonatoRepository;
        }

        public IActionResult IndexCampeonato() =>
    View(_campeonatoRepository.GetAll());

        public IActionResult Index(int id)
        {
            ViewBag.idCampeonato = id;
            return View(_partidaCampeonatoRepository.GetAll(id));
        }

        public IActionResult Sumula(int id)
        {
            ViewBag.IDPartida = id;
            ViewBag.Time1 = _jogadorPartidaCampeonatoRepository.GetJogadorSumula(id, 1);
            ViewBag.Time2 = _jogadorPartidaCampeonatoRepository.GetJogadorSumula(id, 2);
            return View(_partidaCampeonatoRepository.GetSumulaById(id));
        }

        public IActionResult JogadorPartida([Bind("ID,IDJogadorInscrito,IDPartidaCampeonato,iNumCamisa")] JogadorPartidaCampeonato _jogadorPartidaCampeonato, int tipo)
        {
            if (tipo == 1)
            {
                _jogadorPartidaCampeonatoRepository.Add(_jogadorPartidaCampeonato);
            }
            else
            {
                var _jogadorPartida = _jogadorPartidaCampeonatoRepository.GetById(_jogadorPartidaCampeonato.ID);
                if (_jogadorPartida == null)
                    return NotFound();

                _jogadorPartidaCampeonatoRepository.Remove(_jogadorPartida);

            }

            _flashMessage.Confirmation("Operação realizada com sucesso!");
            return RedirectToAction(nameof(Sumula), new { id = _jogadorPartidaCampeonato.IDPartidaCampeonato });

        }

        public IActionResult Create(int id)
        {
            ViewBag.idCampeonato = id;
            ViewBag.ListaCampo = _campoRepository.GetAllCampoDrop();
            ViewBag.ListaInscrito = _inscricaoRepository.GetDropAll(id);
            ViewBag.ListaRodada = Diverso.listaRodada();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IDInscrito1,IDInscrito2,iQntGols1,iQntGols2,iRodada,dDataPartida,sHoraPartida,IDCAMPO,IDCAMPOITEM,CLASSIFICACAO")] PartidaCampeonato _partidaCampeonato, int idCampeonato)
        {
            if (ModelState.IsValid)
            {
                _partidaCampeonatoRepository.Add(_partidaCampeonato);
                _flashMessage.Confirmation("Operação realizada com sucesso!");

                return RedirectToAction(nameof(Index), new { id = idCampeonato });
            }

            return View(_partidaCampeonato);
        }

        public IActionResult CreateAutomatico(int IDCampeonato)
        {
            int iRetorno = _partidaCampeonatoRepository.CreateAutomatico(IDCampeonato);

            if (iRetorno == 1)
            {
                _flashMessage.Confirmation("Operação realizada com sucesso!");
            }
            else if (iRetorno == 2)
            {
                _flashMessage.Warning("Os grupos ja foram geados para este campeonato!");
            }
            else if (iRetorno == 3)
            {
                _flashMessage.Danger("Erro ao realizar a operação!");
            }

            return Json("");

        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var _partidaCampeonato = _partidaCampeonatoRepository.GetById(id);
            if (_partidaCampeonato == null)
                return NotFound();

            ViewBag.idCampeonato = _partidaCampeonato.Inscricao.IDCampeonato;
            ViewBag.ListaCampo = _campoRepository.GetAllCampoDrop();
            ViewBag.ListaCampoItem = _campoItemRepository.GetByIdCampo(_partidaCampeonato.IDCAMPO);
            ViewBag.ListaInscrito = _inscricaoRepository.GetDropAll(_partidaCampeonato.Inscricao.IDCampeonato);
            ViewBag.ListaRodada = Diverso.listaRodada();

            return View(_partidaCampeonato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,IDInscrito1,IDInscrito2,iQntGols1,iQntGols2,iRodada,dDataPartida,sHoraPartida,IDCAMPO,IDCAMPOITEM,CLASSIFICACAO")] PartidaCampeonato _partidaCampeonato, int idCampeonato)
        {
            if (id != _partidaCampeonato.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _partidaCampeonatoRepository.Update(_partidaCampeonato);
                    _flashMessage.Confirmation("Operação realizada com sucesso!");

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

                return RedirectToAction(nameof(Index), new { id = idCampeonato });
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
        public IActionResult DeleteConfirmed(int id)
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


            return RedirectToAction(nameof(Index), new { id = _partidaCampeonato.Inscricao.IDCampeonato });
        }

        public IActionResult ListaCampoItem(int IDCAMPO) => Json(_campoItemRepository.GetByIdCampo(IDCAMPO));

        private bool PartidaCampeonatoExists(int id) =>
            _partidaCampeonatoRepository.GetById(id) != null;


    }

}
