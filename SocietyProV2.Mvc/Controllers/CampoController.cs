using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocietyProV2.Domain.Diversos;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using SocietyProV2.Mvc.Models;
using System;

namespace SocietyProV2.Mvc.Controllers
{
    public class CampoController : Controller
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ICampoRepository _campoRepository;
        private readonly ICidadeRepository _cidadeRepository;

        private readonly IConfiguration _configuration;

        public CampoController(IPessoaRepository pessoaRepository, IConfiguration configuration, ICampoRepository campoRepository, ICidadeRepository cidadeRepository)
        {
            _configuration = configuration;
            _campoRepository = campoRepository;
            _pessoaRepository = pessoaRepository;
            _cidadeRepository = cidadeRepository;
        }

        public IActionResult Index() =>
            View(_campoRepository.GetAll());

        public IActionResult Create()
        {
            ViewBag.ListaCidade = _cidadeRepository.GetAll();
            ViewBag.ListaPessoa = _pessoaRepository.GetAllPessoaDrop();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NOME,ENDERECO,TELEFONE,VALOR,VALORMENSAL,STATUS,DATACADASTRO,SOCIETY,CAMPO11,AGENDAMENTO,LOGO,IDPESSOA,IDCIDADE")] Campo campo, IFormFile FOTO)
        {
            if (ModelState.IsValid)
            {
                if (FOTO != null)
                {
                    campo.LOGO = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                    Diverso.SaveImage(FOTO, "CAMPO", _configuration.GetSection("ResourcesPath")["Foto"] + campo.LOGO);
                }

                _campoRepository.Add(campo);

                return RedirectToAction(nameof(Index));
            }

            return View(campo);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var campo = _campoRepository.GetById(id);
            if (campo == null)
                return NotFound();

            ViewBag.ListaPessoa = _pessoaRepository.GetAllPessoaDrop();
            ViewBag.ListaCidade = _cidadeRepository.GetAll();

            return View(campo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,NOME,ENDERECO,TELEFONE,VALOR,VALORMENSAL,STATUS,DATACADASTRO,SOCIETY,CAMPO11,AGENDAMENTO,LOGO,IDPESSOA,IDCIDADE")] Campo campo, IFormFile FOTO)
        {
            if (id != campo.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (FOTO != null)
                    {
                        campo.LOGO = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        Diverso.SaveImage(FOTO, "CAMPO", campo.LOGO);
                    }

                    _campoRepository.Update(campo);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampoExists(campo.ID))
                        return NotFound();
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(campo);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var campo = _campoRepository.GetById(id);
            if (campo == null)
                return NotFound();

            return View(campo);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var campo = _campoRepository.GetById(id);
            if (campo == null)
                return NotFound();

            return View(campo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var campo = _campoRepository.GetById(id);
            _campoRepository.Remove(campo);
            return RedirectToAction(nameof(Index));
        }

        private bool CampoExists(int id) =>
            _campoRepository.GetById(id) != null;
    }

}
