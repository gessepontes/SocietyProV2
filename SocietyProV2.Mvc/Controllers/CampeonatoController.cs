using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocietyProV2.Domain.Diversos;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System;
using Vereyon.Web;

namespace SocietyProV2.Mvc.Controllers
{
    public class CampeonatoController : Controller
    {
        private readonly ICampeonatoRepository _campeonatoRepository;
        private readonly IFotoInforCampeonatoRepository _fotoInforCampeonatoRepository;
        private readonly ICampoRepository _campoRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IFlashMessage _flashMessage;


        public CampeonatoController(ICampeonatoRepository campeonatoRepository, 
            IFotoInforCampeonatoRepository fotoInforCampeonatoRepository, 
            ICampoRepository campoRepository, IFlashMessage flashMessage,
            IPessoaRepository pessoaRepository)
        {
            _campeonatoRepository = campeonatoRepository;
            _fotoInforCampeonatoRepository = fotoInforCampeonatoRepository;
            _flashMessage = flashMessage;
            _campoRepository = campoRepository;
            _pessoaRepository = pessoaRepository;
        }

        public IActionResult Index() =>
            View(_campeonatoRepository.GetAll());

        public IActionResult Create()
        {
            ViewBag.ListaCampo = _campoRepository.GetAllCampoDrop();
            ViewBag.ListaPessoa = _pessoaRepository.GetAllPessoaDrop();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("sNome,dDataInicio,dDataFim,iTipoCampeonato,iTipoArbitragem,TIPO,iCodCampo,iQuantidadeTimes,bPreInscricao,bIdaVolta,bDisponivelTransmissao,bDisponivelInscricao,IDPESSOA")]  Campeonato _campeonato, IFormFile LOGO)
        {
            if (ModelState.IsValid)
            {
                if (LOGO != null)
                {
                    _campeonato.LOGO = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                    Diverso.SaveImage(LOGO, "CAMPEONATO", _campeonato.LOGO);
                }

                _campeonatoRepository.Add(_campeonato);
                _flashMessage.Confirmation("Operação realizada com sucesso!");

                return RedirectToAction(nameof(Index));
            }

            return View(_campeonato);
        }

        public IActionResult CreateInfor(int? IDCampeonato)
        {
            if (IDCampeonato == null)
                return NotFound();

            ViewBag.Sequencia = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            FotoInforCampeonato _campeonatoInfor = new FotoInforCampeonato();
            _campeonatoInfor.IDCAMPEONATO = IDCampeonato ?? 0;

            return PartialView("_CreateInfor", _campeonatoInfor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateInfor([Bind("NOME,ISEQUENCIA,IDCAMPEONATO")]  FotoInforCampeonato _campeonatoInfor, IFormFile FOTO)
        {
            if (ModelState.IsValid)
            {
                if (FOTO != null)
                {
                    _campeonatoInfor.FOTO = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                    Diverso.SaveImage(FOTO, "CAMPEONATOINFOR", _campeonatoInfor.FOTO);
                }

                _fotoInforCampeonatoRepository.Add(_campeonatoInfor);

                return RedirectToAction(nameof(Edit),new { id = _campeonatoInfor.IDCAMPEONATO });
            }

            return View(_campeonatoInfor);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var _campeonato = _campeonatoRepository.GetById(id);
            if (_campeonato == null)
                return NotFound();

            ViewBag.ListaCampo = _campoRepository.GetAllCampoDrop();
            ViewBag.ListaPessoa = _pessoaRepository.GetAllPessoaDrop();

            return View(_campeonato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IDCampeonato,sNome,dDataInicio,dDataFim,iTipoCampeonato,iTipoArbitragem,TIPO,iCodCampo,iQuantidadeTimes,bPreInscricao,bIdaVolta,bDisponivelTransmissao,bDisponivelInscricao,IDPESSOA")]  Campeonato _campeonato, IFormFile LOGO)
        {
            if (id != _campeonato.IDCampeonato)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (LOGO != null)
                    {
                        _campeonato.LOGO = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        Diverso.SaveImage(LOGO, "CAMPEONATO", _campeonato.LOGO);
                    }

                    _campeonatoRepository.Update(_campeonato);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampeonatoExists(_campeonato.IDCampeonato))
                        return NotFound();
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_campeonato);
        }

        public IActionResult Delete(int? id)
        {
            //Delete
            if (id == null)
                return NotFound();

            var _campeonato = _campeonatoRepository.GetById(id);
            if (_campeonato == null)
                return NotFound();

            return View(_campeonato);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var _campeonato = _campeonatoRepository.GetById(id);
            _campeonatoRepository.Remove(_campeonato);

            _flashMessage.Confirmation("Operação realizada com sucesso!");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult DeleteConfirmedInfor(int id)
        {
            var _campeonatoInfor = _fotoInforCampeonatoRepository.GetById(id);
            _fotoInforCampeonatoRepository.Remove(_campeonatoInfor);

            _flashMessage.Confirmation("Operação realizada com sucesso!");

            return RedirectToAction(nameof(Edit), new { id = _campeonatoInfor.IDCAMPEONATO });
        }

        private bool CampeonatoExists(int id) =>
    _campeonatoRepository.GetById(id) != null;
    }

}
