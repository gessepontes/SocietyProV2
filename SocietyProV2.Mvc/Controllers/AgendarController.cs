using Microsoft.AspNetCore.Mvc;
using SocietyProV2.Domain.Diversos;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using System;
using Vereyon.Web;

namespace SocietyProV2.Mvc.Controllers
{
    public class AgendarController : Controller
    {
        private readonly IHorarioExtraRepository _horarioExtraRepository;
        private readonly IHorarioRepository _horarioRepository;
        private readonly IAgendarRepository _agendarRepository;
        private readonly ICampoItemRepository _campoItemRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IFlashMessage _flashMessage;


        public AgendarController(IHorarioExtraRepository horarioExtraRepository, IHorarioRepository horarioRepository, IFlashMessage flashMessage,
            IAgendarRepository agendarRepository, ICampoItemRepository campoItemRepository,
            IPessoaRepository pessoaRepository)
        {
            _agendarRepository = agendarRepository;
            _horarioExtraRepository = horarioExtraRepository;
            _horarioRepository = horarioRepository;
            _flashMessage = flashMessage;
            _campoItemRepository = campoItemRepository;
            _pessoaRepository = pessoaRepository;
        }

        public IActionResult Index() =>
            View(_agendarRepository.GetAllAgendamento());

        public IActionResult Create()
        {
            ViewBag.ListaCampo = _campoItemRepository.GetAllCampoItemDrop();
            ViewBag.ListaPessoa = _pessoaRepository.GetAllPessoaDrop();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DATA,IDHORARIO,TIPOHORARIO,TIPO,STATUS,IDPESSOA,CLIENTENAOCADASTRADO,TELEFONE,DATACADASTRO,IDPESSOACANCELAMENTO,MARCADOAPP")] Agendar _agendar)
        {
            if (ModelState.IsValid)
            {
                _agendarRepository.Add(_agendar);
                _flashMessage.Confirmation("Operação realizada com sucesso!");

                return RedirectToAction(nameof(Index));
            }

            return View(_agendar);
        }


        public IActionResult Delete(int? id)
        {
            //Delete
            if (id == null)
                return NotFound();

            var _agendar = _agendarRepository.GetByIdAgendamento(id);
            if (_agendar == null)
                return NotFound();

            return View(_agendar);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var _agendar = _agendarRepository.GetById(id);
            _agendarRepository.Remove(_agendar);

            _flashMessage.Confirmation("Operação realizada com sucesso!");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Status(int id, char status)
        {

            _agendarRepository.Status(id, status);
            _flashMessage.Confirmation("Operação realizada com sucesso!");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ListaHorario(DateTime DATA, int IDCAMPO, TipoHorario TIPOHORARIO) => Json(_agendarRepository.GetHorarios(DATA, IDCAMPO, TIPOHORARIO));

        private bool AgendarExists(int id) =>
        _agendarRepository.GetById(id) != null;
    }

}
