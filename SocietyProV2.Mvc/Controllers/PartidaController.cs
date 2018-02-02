using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Interfaces.Repositories;
using SocietyProV2.Mvc.Models;

namespace SocietyProV2.Mvc.Controllers
{
    public class PartidaController : Controller
    {
        private readonly IPartidaRepository _partidaRepository;
        private readonly ITimeRepository _timeRepository;
        private readonly ICampoRepository _campoRepository;
        private readonly IGolPartidaRepository _golPartidaRepository;
        private readonly IJogadorPartidaRepository _jogadorPartidaRepository;

        //HttpContext.Session.SetString("IDTime", "1");
        //ViewBag.IDTime = HttpContext.Session.GetString("IDTime");

        public PartidaController(IPartidaRepository partidaRepository, ITimeRepository timeRepository, ICampoRepository campoRepository, IGolPartidaRepository golPartidaRepository, IJogadorPartidaRepository jogadorPartidaRepository)
        {
            _partidaRepository = partidaRepository;
            _timeRepository = timeRepository;
            _campoRepository = campoRepository;
            _golPartidaRepository = golPartidaRepository;
            _jogadorPartidaRepository = jogadorPartidaRepository;
        }

        public IActionResult Index() => View(_partidaRepository.GetAll());

        public IActionResult Autorizar()
        {
            return View(_partidaRepository.GetAllAutorizar());
        }


        public IActionResult Create()
        {
            ViewBag.ListaTime = _timeRepository.GetAllTimeDrop();
            ViewBag.ListaCampo = _campoRepository.GetAllCampoDrop();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IDTIME1,IDTIME2,DATAPARTIDA,HORAPARTIDA,IDCAMPO,GOL1,GOL2,TIMENAOCADASTRADO,STATUSPARTIDA,STATUS,DATACADASTRO")] Partida partida)
        {
            if (ModelState.IsValid)
            {
                _partidaRepository.Add(partida);

                return RedirectToAction(nameof(Index));
            }

            return View(partida);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var partida = _partidaRepository.GetById(id);
            if (partida == null)
                return NotFound();

            ViewBag.ListaTime = _timeRepository.GetAllTimeDrop();
            ViewBag.ListaCampo = _campoRepository.GetAllCampoDrop();

            return View(partida);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,IDTIME1,IDTIME2,DATAPARTIDA,HORAPARTIDA,IDCAMPO,GOL1,GOL2,TIMENAOCADASTRADO,STATUSPARTIDA,STATUS,DATACADASTRO")] Partida partida)
        {
            if (id != partida.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _partidaRepository.Update(partida);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartidaExists(partida.ID))
                        return NotFound();
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(partida);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var partida = _partidaRepository.GetById(id);
            if (partida == null)
                return NotFound();

            return View(partida);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var partida = _partidaRepository.GetById(id);
            _partidaRepository.Remove(partida);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Aprovar(int id)
        {
            _partidaRepository.Aprovar(id);

            return RedirectToAction(nameof(Autorizar));
        }

        [HttpPost]
        public IActionResult Cancelar(int id)
        {
            _partidaRepository.Cancelar(id);

            return RedirectToAction(nameof(Autorizar));
        }

        private bool PartidaExists(int id) =>
            _partidaRepository.GetById(id) != null;


        public IActionResult Jogador(int id, int IDTime)
        {
            ViewBag.IDTime = IDTime;
            ViewBag.IDPartida = id;
            ViewBag.Partida = _partidaRepository.GetById(id);
            return View(_jogadorPartidaRepository.GetAll(IDTime, id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateJogadorPartida([Bind("IDJOGADOR,IDPARTIDA")] JogadorPartida jogadorPartida, int IDTime)
        {
            if (ModelState.IsValid)
            {
                _jogadorPartidaRepository.Add(jogadorPartida);

                return RedirectToAction(nameof(Jogador), new { id = IDTime, IDPartida = jogadorPartida.IDPARTIDA });
            }

            return View(jogadorPartida);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteJogadorPartida(int id, int IDTime, int IDPARTIDA)
        {
            var jogador = _jogadorPartidaRepository.GetById(id);
            _jogadorPartidaRepository.Remove(jogador);

            return RedirectToAction(nameof(Jogador), new { id = IDTime, IDPartida = IDPARTIDA });
        }

        public IActionResult Gol(int id, int idTime)
        {
            ViewBag.QuantidadeGols = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ViewBag.Partida = _partidaRepository.GetById(id);
            return View(_golPartidaRepository.GetAllPartida(id, idTime));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddGol([Bind("IDJOGADORPARTIDA,QTDGOL")] GolPartida golPartida, int IDTime, int IDPARTIDA)
        {
            if (ModelState.IsValid)
            {
                _golPartidaRepository.Add(golPartida, IDPARTIDA, IDTime);

                return RedirectToAction(nameof(Gol), new { idTime = IDTime, id = IDPARTIDA });
            }

            return View(golPartida);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteGol(int id, int IDTime, int IDPARTIDA)
        {
            var gol = _golPartidaRepository.GetById(id);
            _golPartidaRepository.Remove(gol);

            return RedirectToAction(nameof(Gol), new { idTime = IDTime, id = IDPARTIDA });
        }
    }

}
