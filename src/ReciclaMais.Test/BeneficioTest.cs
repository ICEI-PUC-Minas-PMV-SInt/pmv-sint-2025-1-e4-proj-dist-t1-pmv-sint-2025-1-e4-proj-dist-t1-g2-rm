using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ReciclaMais.API.Controllers;
using ReciclaMais.API.Data;
using ReciclaMais.API.Models;
using ReciclaMais.API.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReciclaMais.Tests
{
    [TestFixture]
    internal class BeneficiosControllerTest
    {
        private AppDbContext _context;
        private BeneficiosController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("BeneficiosTestDb")
                .Options;

            _context = new AppDbContext(options);

            // Popula o banco com múltiplos benefícios para os testes.
            var beneficios = new List<Beneficio>
    {
        new Beneficio
        {
            Id = 1,
            Titulo = "R$50 Supermercados BH",
            Descricao = "Vale R$ 50 em compras nos Supermercados BH",
            PontosNecessarios = 500
        },
        new Beneficio
        {
            Id = 2,
            Titulo = "R$100 Supermercados BH",
            Descricao = "Vale R$ 100 em compras nos Supermercados BH",
            PontosNecessarios = 1000
        },
        new Beneficio
        {
            Id = 3,
            Titulo = "R$150 Araújo",
            Descricao = "Vale R$ 150 em compras nas farmácias Araújo",
            PontosNecessarios = 1500
        } };
            _context.Beneficios.AddRange(beneficios);
            _context.SaveChanges();

            _controller = new BeneficiosController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetAll_DeveRetornarListaDeBeneficios()
        {
            // Act.
            var actionResult = await _controller.GetAll();
            var resultado = actionResult.Result as OkObjectResult;

            // Assert.
            Assert.That(resultado, Is.Not.Null, "Deveria retornar 200 OK.");
            var lista = resultado.Value as List<BeneficioResponseDTO>;
            Assert.That(lista, Is.Not.Null, "A lista não pode ser nula.");
            Assert.That(lista, Is.Not.Empty, "A lista deveria conter pelo menos um benefício.");
        }

        [Test]
        public async Task GetByIdExistente_DeveRetornarBeneficio()
        {
            // Arrange.
            int idExistente = 1;

            // Act.

            var actionResult = await _controller.GetById(idExistente);
            var resultado = actionResult.Result as OkObjectResult;

            // Assert.
            Assert.That(resultado, Is.Not.Null, "Deveria retornar 200 OK.");
            var beneficio = resultado.Value as BeneficioResponseDTO;
            Assert.That(beneficio, Is.Not.Null, "O benefício retornado não deveria ser nulo.");
            Assert.That(beneficio.Id, Is.EqualTo(idExistente), "O ID retornado não confere com o requisitado.");
        }

      

        [Test]
        public async Task Create_DeveAdicionarNovoBeneficio()
        {
            // Arrange.
            var novoBeneficio = new BeneficioCreateDTO
            {
                Titulo = "R$100 Supermercados BH",
                Descricao = "Vale R$ 100 em compras nos Supermercados BH",
                PontosNecessarios = 200
            };

            // Act.
            var resultado = await _controller.Create(novoBeneficio) as CreatedAtActionResult;

            // Assert.
            Assert.That(resultado, Is.Not.Null, "Deveria retornar 201 Created.");
            var beneficioCriado = resultado.Value as Beneficio;
            Assert.That(beneficioCriado, Is.Not.Null, "O benefício criado não deveria ser nulo.");
            Assert.That(beneficioCriado.Titulo, Is.EqualTo(novoBeneficio.Titulo), "O título do benefício não confere.");
        }

        [Test]
        public async Task Update_DeveAtualizarBeneficioExistente()
        {
            // Arrange.
            int idExistente = 1;
            var beneficioAtualizado = new BeneficioCreateDTO
            {
                Titulo = "R$60 Supermercados BH",
                Descricao = "Vale R$ 60 em compras nos Supermercados BH",
                PontosNecessarios = 120
            };

            // Act.
            var resultado = await _controller.Update(idExistente, beneficioAtualizado);

            // Assert.
            Assert.That(resultado, Is.InstanceOf<NoContentResult>(), "Deveria retornar 204 No Content.");
            var beneficioDb = await _context.Beneficios.FindAsync(idExistente);
            Assert.That(beneficioDb.Titulo, Is.EqualTo(beneficioAtualizado.Titulo), "O título do benefício não foi atualizado corretamente.");
        }

        [Test]
        public async Task Delete_DeveRemoverBeneficioExistente()
        {
            // Arrange.
            int idExistente = 1;

            // Act.
            var resultado = await _controller.Delete(idExistente);

            // Assert.
            Assert.That(resultado, Is.InstanceOf<OkObjectResult>(), "Deveria retornar 200 OK.");
            var beneficioDb = await _context.Beneficios.FindAsync(idExistente);
            Assert.That(beneficioDb, Is.Null, "O benefício deveria ter sido removido.");
        }
    }
}