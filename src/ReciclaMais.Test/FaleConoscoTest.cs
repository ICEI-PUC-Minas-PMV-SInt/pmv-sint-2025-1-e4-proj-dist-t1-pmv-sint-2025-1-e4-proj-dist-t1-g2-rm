using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ReciclaMais.API.Controllers;
using ReciclaMais.API.Data;
using ReciclaMais.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReciclaMais.Tests
{
    [TestFixture]
    public class FaleConoscoTest
    {
        private AppDbContext _context;
        private FaleConoscoController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("faleconoscodb")
                .Options;

            _context = new AppDbContext(options);

            _context.FaleConosco.AddRange(
                new FaleConosco
                {
                    Id = 1,
                    Nome = "João",
                    Email = "joao@example.com",
                    Telefone = "31999990001"
                },

                new FaleConosco
                {
                    Id = 2,
                    Nome = "Maria",
                    Email = "maria@example.com",
                    Telefone = "31999990002"
                }
            );
            _context.SaveChanges();

            _controller = new FaleConoscoController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetAll_DeveRetornarTodosOsContatos()
        {
            var resultado = await _controller.GetAll() as OkObjectResult;

            Assert.That(resultado, Is.Not.Null);
            var contatos = resultado.Value as List<FaleConosco>;

            Assert.That(contatos, Is.Not.Null);
            Assert.That(contatos, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task GetById_QuandoExiste_DeveRetornarContato()
        {
            var resultado = await _controller.GetById(1) as OkObjectResult;

            Assert.That(resultado, Is.Not.Null);
            var contato = resultado.Value as FaleConosco;

            Assert.That(contato, Is.Not.Null);
            Assert.That(contato.Id, Is.EqualTo(1));
            Assert.That(contato.Nome, Is.EqualTo("João"));
        }

        [Test]
        public async Task GetById_QuandoNaoExiste_DeveRetornarNotFound()
        {
            var resultado = await _controller.GetById(999);

            Assert.That(resultado, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Create_QuandoNomeOuTelefoneVazio_DeveRetornarBadRequest()
        {
            var contatoInvalido = new FaleConosco
            {
                Nome = "",
                Email = "teste@example.com",
                Telefone = ""
            };

            var resultado = await _controller.Create(contatoInvalido);

            Assert.That(resultado, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task Create_QuandoValido_DeveRetornarCreatedAtAction()
        {
            var novoContato = new FaleConoscoCreateDTO
            {
                Nome = "Carlos",
                Telefone = "99999-8888"
            };
            var actionResult = await _controller.Create(novoContato);

            var resultado = actionResult.Result as CreatedAtActionResult;

            Assert.That(resultado, Is.InstanceOf<CreatedAtActionResult>());

            var criado = (resultado as CreatedAtActionResult)?.Value as FaleConosco;

            Assert.That(criado, Is.Not.Null);
            Assert.That(criado.Nome, Is.EqualTo("Carlos"));
            Assert.That(criado.Telefone, Is.EqualTo("99999-8888"));
        }
    }
}

