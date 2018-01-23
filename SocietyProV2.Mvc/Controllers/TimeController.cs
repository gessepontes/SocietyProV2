using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocietyProV2.Domain.Diversos;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System;

namespace SocietyProV2.Mvc.Controllers
{
    public class TimeController : Controller
    {
        private readonly ITimeRepository _timeRepository;
        private readonly IPessoaRepository _pessoaRepository;

        public TimeController(ITimeRepository timeRepository, IPessoaRepository pessoaRepository)
        {
            _timeRepository = timeRepository;
            _pessoaRepository = pessoaRepository;
        }

        public IActionResult Index() =>
            View(_timeRepository.GetAll());

        public IActionResult Create()
        {
            ViewBag.ListaPessoa = _pessoaRepository.GetAllPessoaDrop();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NOME,IDPESSOA,OBSERVACAO,DATAFUNDACAO,TIPO")] Time time, IFormFile SIMBOLO)
        {
            if (ModelState.IsValid)
            {
                if (SIMBOLO != null)
                {
                    time.SIMBOLO = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                    Diverso.SaveImage(SIMBOLO, "TIME", time.SIMBOLO);
                }

                _timeRepository.Add(time);

                return RedirectToAction(nameof(Index));
            }

            return View(time);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var time = _timeRepository.GetById(id);
            if (time == null)
                return NotFound();

            ViewBag.ListaPessoa = _pessoaRepository.GetAllPessoaDrop();

            return View(time);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,NOME,IDPESSOA,OBSERVACAO,DATAFUNDACAO,TIPO")] Time time, IFormFile SIMBOLO)
        {
            if (id != time.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (SIMBOLO != null)
                    {
                        time.SIMBOLO = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        Diverso.SaveImage(SIMBOLO, "TIME", time.SIMBOLO);
                    }

                    _timeRepository.Update(time);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeExists(time.ID))
                        return NotFound();
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(time);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var time = _timeRepository.GetById(id);
            if (time == null)
                return NotFound();

            return View(time);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var time = _timeRepository.GetById(id);
            _timeRepository.Remove(time);

            return RedirectToAction(nameof(Index));
        }

        private bool TimeExists(int id) =>
            _timeRepository.GetById(id) != null;
    }

}
