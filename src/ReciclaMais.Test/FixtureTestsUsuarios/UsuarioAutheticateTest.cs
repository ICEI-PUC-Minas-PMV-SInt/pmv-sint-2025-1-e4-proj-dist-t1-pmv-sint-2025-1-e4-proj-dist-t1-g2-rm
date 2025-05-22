using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using ReciclaMais.API.Controllers;
using ReciclaMais.API.Data;
using ReciclaMais.API.Models;
using ReciclaMais.API.Models.Dto;
using ReciclaMais.API.Models.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ReciclaMais.Tests.FixtureUsuarios
{
    [TestFixture]
    internal class UsuarioAutheticateTest
    {
        private AppDbContext _context;
        private UsuariosController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("UsuarioAuthenticateTestDb")
                .Options;

            _context = new AppDbContext(options);

          
            var configValues = new Dictionary<string, string>
            {
                { "Jwt:Issuer", "TestIssuer" },
                { "Jwt:Audience", "TestAudience" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configValues)
                .Build();

            _controller = new UsuariosController(_context, configuration);

            var usuario = new Usuario
            {
                Id = 1,
                Username = "joaosilva",
                Password = BCrypt.Net.BCrypt.HashPassword("senha123"),
                Tipo = TipoUsuario.Municipe,
                Nome = "João Silva",
                Estado = "SP",
                Cidade = "São Paulo",
                Bairro = "Centro",
                Rua = "Rua A",
                CEP = 01000-000,
                Numero = 100,
                Complemento = ""
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Authenticate_InvalidUsername_DeveRetornarUnauthorized()
        {
            // Act:
            var dto = new AuthenticateDTO
            {
                Username = "usuario_invalido",
                Password = "senha123"
            };

            // Act:
            var result = await _controller.Authenticate(dto);

            // Assert: 
            Assert.That(result, Is.InstanceOf<UnauthorizedObjectResult>(), "Deveria retornar 401 Unauthorized.");
        }

        [Test]
        public async Task Authenticate_InvalidPassword_DeveRetornarUnauthorized()
        {
            // Arrange:
            var dto = new AuthenticateDTO
            {
                Username = "joaosilva",
                Password = "senha_errada"
            };

            // Act: 
            var result = await _controller.Authenticate(dto);

            // Assert: 
            Assert.That(result, Is.InstanceOf<UnauthorizedObjectResult>(), "Deveria retornar 401 Unauthorized.");
        }

        [Test]
        public async Task Authenticate_ValidCredentials_DeveRetornarToken()
        {
            // Arrange:
            var dto = new AuthenticateDTO
            {
                Username = "joaosilva",
                Password = "senha123"
            };

            // Act:
            var result = await _controller.Authenticate(dto) as OkObjectResult;

            // Assert:
            Assert.That(result, Is.Not.Null, "Deveria retornar 200 OK.");
            Assert.That(result.Value, Is.Not.Null, "O valor de retorno não pode ser nulo.");
            Assert.That(result.Value.ToString(), Does.Contain("token"), "O retorno deveria conter um token.");
        }
    }
}
