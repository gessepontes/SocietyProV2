using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using Vereyon.Web;

namespace SocietyProV2.Mvc.Controllers
{
    public class HorarioController : Controller
    {
        private readonly IHorarioRepository _horarioRepository;
        private readonly ICampoItemRepository _campoItemRepository;
        private readonly IFlashMessage _flashMessage;


        public HorarioController(IHorarioRepository horarioRepository, IFlashMessage flashMessage, ICampoItemRepository campoItemRepository)
        {
            _campoItemRepository = campoItemRepository;
            _horarioRepository = horarioRepository;
            _flashMessage = flashMessage;
        }

        public IActionResult Index() =>
            View(_horarioRepository.GetAll());

        public IActionResult Create()
        {
            ViewBag.ListaCampo = _campoItemRepository.GetAllCampoItemDrop();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IDITEMCAMPO,DIASEMANA,HORARIO,STATUS")] Horario _horario)
        {
            if (ModelState.IsValid)
            {
                _horarioRepository.Add(_horario);
                _flashMessage.Confirmation("Operação realizada com sucesso!");

                return RedirectToAction(nameof(Index));
            }

            return View(_horario);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var campoValor = _horarioRepository.GetById(id);
            if (campoValor == null)
                return NotFound();

            ViewBag.ListaCampo = _campoItemRepository.GetAllCampoItemDrop();

            return View(campoValor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,IDITEMCAMPO,DIASEMANA,HORARIO,STATUS")] Horario _horario)
        {
            if (id != _horario.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _horarioRepository.Update(_horario);
                    _flashMessage.Confirmation("Operação realizada com sucesso!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioExists(_horario.ID))
                        return NotFound();
                    else
                    {
                        _flashMessage.Danger("Erro ao realizar a operação!");
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_horario);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var horario = _horarioRepository.GetById(id);
            if (horario == null)
                return NotFound();

            return View(horario);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var horario = _horarioRepository.GetById(id);
            _horarioRepository.Remove(horario);

            _flashMessage.Confirmation("Operação realizada com sucesso!");

            return RedirectToAction(nameof(Index));
        }

        private bool HorarioExists(int id) =>
            _horarioRepository.GetById(id) != null;
    }

}
