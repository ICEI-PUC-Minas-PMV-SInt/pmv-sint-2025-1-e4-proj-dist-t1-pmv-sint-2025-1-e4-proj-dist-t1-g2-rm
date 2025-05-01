using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ReciclaMaisAPI.Controllers;
using ReciclaMaisAPI.Data;
using ReciclaMaisAPI.Models;
using ReciclaMaisAPI.Models.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReciclaMais.Tests
{
    [TestFixture]
    internal class ItemColetaTest
    {
        private AppDbContext _context;
        private ItemColetaController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("ItemColetaTestDb")
                .Options;

            _context = new AppDbContext(options);

            // Popula o banco com dados válidos para os testes.
            var produto = new ProdutoResiduo
            {
                Id = 1,
                Nome = "Celular",
                Descricao = "Celular antigo",
                Pontuacao = 100
            };

            var agendamento = new Agendamento
            {
                Id = 1,
                Data = DateTime.Today,
                Hora = new TimeSpan(14, 0, 0),
                PontuacaoTotal = 0
            };

            var item = new ItemColeta
            {
                Id = 1,
                ProdutoId = produto.Id,
                Produto = produto,
                Quantidade = 1,
                Estado = EstadoConservacao.Descarte,
                AgendamentoId = agendamento.Id,
                Agendamento = agendamento
            };

            _context.ProdutosResiduos.Add(produto);
            _context.Agendamentos.Add(agendamento);
            _context.ItensColeta.Add(item);

            _context.SaveChanges();

            _controller = new ItemColetaController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetAll_DeveRetornarListaComItens()
        {
            // Act.
            var resultado = await _controller.GetAll() as OkObjectResult;

            // Assert.
            Assert.That(resultado, Is.Not.Null, "Retorno: 200 OK com uma lista.");
            var lista = resultado.Value as List<ItemColeta>;
            Assert.That(lista, Is.Not.Null, "A lista não pode ser nula.");
            Assert.That(lista.Count, Is.GreaterThan(0), "A lista deveria conter ao menos um item.");
        }

        [Test]
        public async Task GetByIdExistente_DeveRetornarItem()
        {
            // Arrange.
            int idExistente = 1;

            // Act.
            var resultado = await _controller.GetById(idExistente) as OkObjectResult;
            
            // Assert.
            Assert.That(resultado, Is.Not.Null, "Retorno: 200 OK.");
            var item = resultado.Value as ItemColeta;
            Assert.That(item, Is.Not.Null, "O item retornado não deveria ser nulo.");
            Assert.That(item.Id, Is.EqualTo(idExistente), "O ID retornado não confere com o requisitado.");
        }

        [Test]
        public async Task GetByIdInexistente_DeveRetornarNotFound()
        {
            // Arrange.
            int idInexistente = 99999;

            // Act.
            var resultado = await _controller.GetById(idInexistente);

            // Assert.
            Assert.That(resultado, Is.InstanceOf<NotFoundResult>(), "Deveria retornar 404 Not Found.");
        }
    }
}
