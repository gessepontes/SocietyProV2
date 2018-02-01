using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocietyProV2.Domain.Diversos;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System;

namespace SocietyProV2.Mvc.Controllers
{
    public class JogadorController : Controller
    {
        private readonly ITimeRepository _timeRepository;
        private readonly IJogadorRepository _jogadorRepository;
        private readonly IConfiguration _configuration;

        public JogadorController(ITimeRepository timeRepository, IConfiguration configuration, IJogadorRepository jogadorRepository)
        {
            _configuration = configuration;
            _timeRepository = timeRepository;
            _jogadorRepository = jogadorRepository;
        }

        public IActionResult Index() =>
            View(_jogadorRepository.GetAll());

        public IActionResult Create()
        {
            ViewBag.ListaTime = _timeRepository.GetAllTimeDrop();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NOME,IDTIME,RG,DATANASCIMENTO,TELEFONE,POSICAO,DISPENSADO,DATADISPENSA")] Jogador jogador, IFormFile FOTO)
        {
            if (ModelState.IsValid)
            {
                if (FOTO != null)
                {
                    jogador.FOTO = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                    Diverso.SaveImage(FOTO, "JOGADOR", _configuration.GetSection("ResourcesPath")["Foto"] + jogador.FOTO);
                }

                _jogadorRepository.Add(jogador);

                return RedirectToAction(nameof(Index));
            }

            return View(jogador);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var jogador = _jogadorRepository.GetById(id);
            if (jogador == null)
                return NotFound();

            ViewBag.ListaTime = _timeRepository.GetAllTimeDrop();

            return View(jogador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,NOME,IDTIME,RG,DATANASCIMENTO,TELEFONE,POSICAO,DISPENSADO,DATADISPENSA")] Jogador jogador, IFormFile FOTO)
        {
            if (id != jogador.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (FOTO != null)
                    {
                        jogador.FOTO = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        Diverso.SaveImage(FOTO, "JOGADOR", jogador.FOTO);
                    }

                    _jogadorRepository.Update(jogador);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogadorExists(jogador.ID))
                        return NotFound();
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jogador);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var jogador = _jogadorRepository.GetById(id);
            if (jogador == null)
                return NotFound();

            return View(jogador);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var jogador = _jogadorRepository.GetById(id);
            _jogadorRepository.Remove(jogador);
            return RedirectToAction(nameof(Index));
        }

        private bool JogadorExists(int id) =>
            _jogadorRepository.GetById(id) != null;
    }

}
