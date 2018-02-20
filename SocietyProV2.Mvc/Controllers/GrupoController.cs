using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using Vereyon.Web;

namespace SocietyProV2.Mvc.Controllers
{
    public class GrupoController : Controller
    {
        private readonly IFlashMessage _flashMessage;
        private readonly IGrupoRepository _grupoRepository;
        private readonly ICampeonatoRepository _campeonatoRepository;

        public GrupoController(IGrupoRepository grupoRepository,
            IFlashMessage flashMessage,
            ICampeonatoRepository campeonatoRepository)
        {
            _flashMessage = flashMessage;
            _grupoRepository = grupoRepository;
            _campeonatoRepository = campeonatoRepository;
        }

        public IActionResult IndexCampeonato() =>
            View(_campeonatoRepository.GetGrupoAll());

        public IActionResult Index(int id)
        {
            ViewBag.IdCampeonato = id;
            return View(_grupoRepository.GetAllByCampeonato(id));
        }

        public IActionResult Create(int id)
        {
            ViewBag.IdCampeonato = id;
            ViewBag.ListaTime = _grupoRepository.GetDropAll(id);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IDGrupo,IDInscrito")] Grupo _grupo, int id)
        {
            if (ModelState.IsValid)
            {
                _grupoRepository.Add(_grupo);
                _flashMessage.Confirmation("Operação realizada com sucesso!");

                return RedirectToAction(nameof(Index), new { id });
            }

            return View(_grupo);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var _grupo = _grupoRepository.GetById(id);
            if (_grupo == null)
                return NotFound();

            ViewBag.ListaTime = _grupoRepository.GetDropEdit(_grupo.IDInscrito);
            ViewBag.IdCampeonato = _grupo.Inscricao.PreInscricao.IDCampeonato;

            return View(_grupo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,IDGrupo,IDInscrito")] Grupo _grupo, int idCampeonato)
        {
            if (id != _grupo.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _grupoRepository.Update(_grupo);
                    _flashMessage.Confirmation("Operação realizada com sucesso!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupoExists(_grupo.ID))
                        return NotFound();
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = idCampeonato });
            }

            return View(_grupo);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var _grupo = _grupoRepository.GetById(id);
            if (_grupo == null)
                return NotFound();

            return View(_grupo);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var _grupo = _grupoRepository.GetById(id);

            try
            {
                _grupoRepository.Remove(_grupo);
                _flashMessage.Confirmation("Operação realizada com sucesso!");
            }
            catch (System.Exception)
            {
                _flashMessage.Danger("Este registro não pode ser apagado, o registro possui dependencias!");
            }

            return RedirectToAction(nameof(Index), new { id = _grupo.Inscricao.PreInscricao.IDCampeonato });
        }

        private bool GrupoExists(int id) =>
            _grupoRepository.GetById(id) != null;
    }

}
