using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using Vereyon.Web;

namespace SocietyProV2.Mvc.Controllers
{
    public class CampoItemController : Controller
    {
        private readonly ICampoItemRepository _campoItemRepository;
        private readonly ICampoRepository _campoRepository;
        private readonly IFlashMessage _flashMessage;


        public CampoItemController(ICampoRepository campoRepository, ICampoItemRepository campoItemRepository, IFlashMessage flashMessage)
        {
            _campoRepository = campoRepository;
            _campoItemRepository = campoItemRepository;
            _flashMessage = flashMessage;
        }

        public IActionResult Index() =>
            View(_campoItemRepository.GetAll());

        public IActionResult Create()
        {
            ViewBag.ListaCampo = _campoRepository.GetAll();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IDCAMPO,DESCRICAO,ATIVO")] CampoItem campoItem)
        {
            if (ModelState.IsValid)
            {
                _campoItemRepository.Add(campoItem);
                _flashMessage.Confirmation("Operação realizada com sucesso!");

                return RedirectToAction(nameof(Index));
            }

            return View(campoItem);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var campoItem = _campoItemRepository.GetById(id);
            if (campoItem == null)
                return NotFound();

            ViewBag.ListaCampo = _campoRepository.GetAll();

            return View(campoItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,IDCAMPO,DESCRICAO,ATIVO")] CampoItem campoItem)
        {
            if (id != campoItem.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _campoItemRepository.Update(campoItem);
                    _flashMessage.Confirmation("Operação realizada com sucesso!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampoItemExists(campoItem.ID))
                        return NotFound();
                    else
                    {
                        _flashMessage.Danger("Erro ao realizar a operação!");
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(campoItem);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var campoItem = _campoItemRepository.GetById(id);
            if (campoItem == null)
                return NotFound();

            return View(campoItem);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var campoItem = _campoItemRepository.GetById(id);
            _campoItemRepository.Remove(campoItem);

            _flashMessage.Confirmation("Operação realizada com sucesso!");

            return RedirectToAction(nameof(Index));
        }

        private bool CampoItemExists(int id) =>
            _campoItemRepository.GetById(id) != null;
    }

}
