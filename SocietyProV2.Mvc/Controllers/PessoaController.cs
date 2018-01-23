using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocietyProV2.Domain.Entities;
using SocietyProV2.Domain.Diversos;
using SocietyProV2.Domain.Interfaces.Repositories;
using System;
using Microsoft.Extensions.Configuration;

namespace SocietyProV2.Mvc.Controllers
{
    public class PessoaController : Controller
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IConfiguration _configuration;

        public PessoaController(IPessoaRepository pessoaRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _pessoaRepository = pessoaRepository;
        }


        public IActionResult Index() => View(_pessoaRepository.GetAll());
        
        public IActionResult Create() =>
            View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,NOME,RG,CPF,DATANASCIMENTO,TELEFONE,EMAIL,FOTO,SENHA,ATIVO,CONFIRMACAO,PERFILSELECIONADO,SECURITYSTAMP,STATUS,DATACADASTRO")] Pessoa pessoa, IFormFile FOTO)
        {
            if (ModelState.IsValid)
            {
                if (FOTO != null)
                {
                    pessoa.FOTO = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                    Diverso.SaveImage(FOTO, "PESSOA", _configuration.GetSection("ResourcesPath")["Foto"] + pessoa.FOTO);
                }

                _pessoaRepository.Add(pessoa);

                return RedirectToAction(nameof(Index));
            }

            return View(pessoa);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var pessoa = _pessoaRepository.GetByIdPessoaPerfil(id);
            if (pessoa == null)
                return NotFound();

            return View(pessoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,NOME,RG,CPF,DATANASCIMENTO,TELEFONE,EMAIL,FOTO,SENHA,ATIVO,CONFIRMACAO,PERFILSELECIONADO,SECURITYSTAMP,STATUS,DATACADASTRO")] Pessoa pessoa, IFormFile FOTO)
        {

            //_configuration.GetSection("AppConfiguration")["ResourcesPath:Foto"]

            if (id != pessoa.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (FOTO != null)
                    {
                        pessoa.FOTO = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        Diverso.SaveImage(FOTO, "PESSOA", pessoa.FOTO);
                    }

                    _pessoaRepository.Update(pessoa);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.ID))
                        return NotFound();
                    else {
                        throw;
                    }                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        public IActionResult EditUser(int? id)
        {
            id = 4167;

            if (id == null)
                return NotFound();

            var pessoa = _pessoaRepository.GetById(id);
            if (pessoa == null)
                return NotFound();

            pessoa.FOTO = _configuration.GetSection("AppConfiguration")["ResourcesPath:Foto"] + pessoa.FOTO;

            return View(pessoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(int id, [Bind("ID,NOME,RG,DATANASCIMENTO,TELEFONE,EMAIL,FOTO,SENHA")] Pessoa pessoa, IFormFile FOTO)
        {

            if (id != pessoa.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (FOTO != null)
                    {
                        pessoa.FOTO = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        Diverso.SaveImage(FOTO, "PESSOA", pessoa.FOTO);
                    }

                    _pessoaRepository.UpdateUser(pessoa);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.ID))
                        return NotFound();
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(EditUser), new { pessoa.ID });
            }
            return View(pessoa);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var pessoa = _pessoaRepository.GetById(id);
            if (pessoa == null)
                return NotFound();

            return View(pessoa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var pessoa = _pessoaRepository.GetById(id);
            _pessoaRepository.Remove(pessoa);

            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(int id) =>
            _pessoaRepository.GetById(id) != null;
    }
}
