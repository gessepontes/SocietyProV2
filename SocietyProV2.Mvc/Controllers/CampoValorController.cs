using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using Vereyon.Web;

namespace SocietyProV2.Mvc.Controllers
{
    public class CampoValorController : Controller
    {
        private readonly ICampoItemRepository _campoItemRepository;
        private readonly ICampoValorRepository _campoValorRepository;
        private readonly IFlashMessage _flashMessage;


        public CampoValorController(ICampoValorRepository campoValorRepository, ICampoItemRepository campoItemRepository, IFlashMessage flashMessage)
        {
            _campoValorRepository = campoValorRepository;
            _campoItemRepository = campoItemRepository;
            _flashMessage = flashMessage;
        }

        public IActionResult Index() =>
            View(_campoValorRepository.GetAll());

        public IActionResult Create()
        {
            ViewBag.ListaCampo = _campoItemRepository.GetAllCampoItemDrop();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IDCAMPOITEM,DATAINICIO,DATAFIM,VALOR,VALORMENSAL")] CampoValor campoValor)
        {
            if (ModelState.IsValid)
            {
                _campoValorRepository.Add(campoValor);
                _flashMessage.Confirmation("Operação realizada com sucesso!");

                return RedirectToAction(nameof(Index));
            }

            return View(campoValor);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var campoValor = _campoValorRepository.GetById(id);
            if (campoValor == null)
                return NotFound();

            ViewBag.ListaCampo = _campoItemRepository.GetAllCampoItemDrop();

            return View(campoValor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,IDCAMPOITEM,DATAINICIO,DATAFIM,VALOR,VALORMENSAL")] CampoValor campoValor)
        {
            if (id != campoValor.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _campoValorRepository.Update(campoValor);
                    _flashMessage.Confirmation("Operação realizada com sucesso!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampoValorExists(campoValor.ID))
                        return NotFound();
                    else
                    {
                        _flashMessage.Danger("Erro ao realizar a operação!");
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(campoValor);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var campoValor = _campoValorRepository.GetById(id);
            if (campoValor == null)
                return NotFound();

            return View(campoValor);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var campoValor = _campoValorRepository.GetById(id);
            _campoValorRepository.Remove(campoValor);

            _flashMessage.Confirmation("Operação realizada com sucesso!");

            return RedirectToAction(nameof(Index));
        }

        private bool CampoValorExists(int id) =>
            _campoValorRepository.GetById(id) != null;
    }

}
