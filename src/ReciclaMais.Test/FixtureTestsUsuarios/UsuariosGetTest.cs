using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ReciclaMais.API.Controllers;
using ReciclaMais.API.Data;
using ReciclaMais.API.Models;
using ReciclaMais.API.Models.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReciclaMais.Tests
{
    [TestFixture]
    internal class UsuariosControllerTest
    {
        private AppDbContext _context;
        private UsuariosController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("UsuariosTestDb")
                .Options;

            _context = new AppDbContext(options);

            var usuario = new Usuario
            {
                Id = 1,
                Nome = "João Silva",
                Estado = "SP",
                Cidade = "São Paulo",
                Bairro = "Centro",
                Rua = "Rua A",
                CEP = 01000-000,
                Numero = 100,
                Complemento = "Apto 1",
                Username = "joaosilva",
                Password = "senha123",
                Tipo = TipoUsuario.Municipe,
                Municipe = new Municipe { Cpf = "12345678900" }
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            _controller = new UsuariosController(_context, configuration: null);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetUsuarios_DeveRetornarListaComUsuarios()
        {
            // Act
            var actionResult = await _controller.GetUsuarios();
            var resultado = actionResult.Result as OkObjectResult;

            // Assert
            Assert.That(resultado, Is.Not.Null, "Retorno: 200 OK esperado.");
            var lista = resultado.Value as List<Usuario>;
            Assert.That(lista, Is.Not.Null, "Lista de usuários não pode ser nula.");
            Assert.That(lista, Is.Not.Empty, "Deveria conter pelo menos um usuário.");
        }

        [Test]
        public async Task GetUsuarioExistente_DeveRetornarUsuario()
        {
            // Arrange
            int idExistente = 1;

            // Act
            var actionResult = await _controller.GetUsuario(idExistente);
            var resultado = actionResult.Result as OkObjectResult;

            // Assert
            Assert.That(resultado, Is.Not.Null, "Retorno: 200 OK esperado.");
            var usuario = resultado.Value as Usuario;
            Assert.That(usuario, Is.Not.Null, "Usuário retornado não deveria ser nulo.");
            Assert.That(usuario.Id, Is.EqualTo(idExistente), "ID retornado não confere com o requisitado.");
        }

        [Test]
        public async Task GetUsuarioInexistente_DeveRetornarNotFound()
        {
            // Arrange
            int idInexistente = 9999;

            // Act
            var resultado = await _controller.GetUsuario(idInexistente);
            var notFoundResult = resultado.Result;

            // Assert
            Assert.That(notFoundResult, Is.InstanceOf<NotFoundResult>(), "Deveria retornar 404 Not Found.");
        }

    }
}
