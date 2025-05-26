using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ReciclaMais.API.Controllers;
using ReciclaMais.API.Data;
using ReciclaMais.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReciclaMais.Tests
{
    [TestFixture]
    public class NoticiaTest
    {
        private AppDbContext _context;
        private NoticiasController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "noticiasdb")
                .Options;

            _context = new AppDbContext(options);

            _context.Noticias.AddRange(
                new Noticia { Id = 1, Titulo = "Coleta de Pilhas", Conteudo = "Nova campanha de coleta inicia hoje", DataPublicacao = DateTime.UtcNow.AddDays(-2) },
                new Noticia { Id = 2, Titulo = "Reciclagem em Escolas", Conteudo = "Educação ambiental será promovida", DataPublicacao = DateTime.UtcNow.AddDays(-1) }
            );
            _context.SaveChanges();

            _controller = new NoticiasController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetAll_DeveRetornarTodasAsNoticias()
        {
            var resultado = await _controller.GetAll() as OkObjectResult;

            Assert.That(resultado, Is.Not.Null);
            var noticias = resultado.Value as List<Noticia>;

            Assert.That(noticias, Is.Not.Null);
            Assert.That(noticias, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task GetById_QuandoIdExiste_DeveRetornarNoticia()
        {
            var resultado = await _controller.GetById(1) as OkObjectResult;

            Assert.That(resultado, Is.Not.Null);
            var noticia = resultado.Value as Noticia;

            Assert.That(noticia, Is.Not.Null);
            Assert.That(noticia.Id, Is.EqualTo(1));
            Assert.That(noticia.Titulo, Is.EqualTo("Coleta de Pilhas"));
        }

        [Test]
        public async Task GetById_QuandoIdNaoExiste_DeveRetornarNotFound()
        {
            var resultado = await _controller.GetById(999);

            Assert.That(resultado, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Create_QuandoTituloOuConteudoVazio_DeveRetornarBadRequest()
        {
            var noticiaInvalida = new Noticia { Titulo = "", Conteudo = "" };

            var resultado = await _controller.Create(noticiaInvalida);

            Assert.That(resultado, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task Create_QuandoValida_DeveRetornarCreatedAtAction()
        {
            var novaNoticia = new Noticia { Titulo = "Nova Lei Ambiental", Conteudo = "Foi sancionada hoje no Senado." };

            var resultado = await _controller.Create(novaNoticia);

            Assert.That(resultado, Is.InstanceOf<CreatedAtActionResult>());
            var criado = (resultado as CreatedAtActionResult)?.Value as Noticia;

            Assert.That(criado, Is.Not.Null);
            Assert.That(criado.Titulo, Is.EqualTo("Nova Lei Ambiental"));
            Assert.That(criado.Conteudo, Is.EqualTo("Foi sancionada hoje no Senado."));
            Assert.That(criado.DataPublicacao.Date, Is.EqualTo(DateTime.UtcNow.Date));
        }

        [Test]
        public async Task Update_QuandoIdDiferente_DeveRetornarBadRequest()
        {
            var noticia = new Noticia { Id = 10, Titulo = "Fake", Conteudo = "Fake news" };

            var resultado = await _controller.Update(99, noticia);

            Assert.That(resultado, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task Update_QuandoNaoExiste_DeveRetornarNotFound()
        {
            var noticia = new Noticia { Id = 100, Titulo = "Não existe", Conteudo = "Sem conteúdo" };

            var resultado = await _controller.Update(100, noticia);

            Assert.That(resultado, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Update_QuandoValido_DeveRetornarNoContent()
        {
            var noticia = await _context.Noticias.FirstOrDefaultAsync(n => n.Id == 1);
            noticia.Titulo = "Atualizado";
            noticia.Conteudo = "Conteúdo atualizado";

            var resultado = await _controller.Update(1, noticia);

            Assert.That(resultado, Is.InstanceOf<NoContentResult>());

            var atualizada = await _context.Noticias.FindAsync(1);
            Assert.That(atualizada.Titulo, Is.EqualTo("Atualizado"));
        }

        [Test]
        public async Task Delete_QuandoNaoExiste_DeveRetornarNotFound()
        {
            var resultado = await _controller.Delete(500);

            Assert.That(resultado, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Delete_QuandoExiste_DeveRetornarNoContent()
        {
            var resultado = await _controller.Delete(2);

            Assert.That(resultado, Is.InstanceOf<NoContentResult>());

            var excluida = await _context.Noticias.FindAsync(2);
            Assert.That(excluida, Is.Null);
        }
    }
}
