using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReciclaMaisAPI.Controllers;
using ReciclaMaisAPI.Data;
using ReciclaMaisAPI.Models;

namespace ReciclaMais.Tests
{
    [TestFixture]
    public class ProdutoResiduoTest
    {
        private AppDbContext _context;
        private ProdutosController _controller;

        [SetUp]
        public void Setup()
        {
            // ------------- REMOVER -------------
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "reciclamaisdb")
                .Options;

            _context = new AppDbContext(options);

            _context.ProdutosResiduos.AddRange(
                new ProdutoResiduo { Id = 1, Nome = "Celular", Descricao = "Celular velho", Pontuacao = 100 },
                new ProdutoResiduo { Id = 2, Nome = "Notebook", Descricao = "Notebook quebrado", Pontuacao = 150 }
            );
            _context.SaveChanges();

            // -----------------------------------
            _controller = new ProdutosController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task CreateProdutoValido_DeveRetornarCreatedAtAction()
        {
            // Arrange
            var novoProduto = new ProdutoResiduo
            {
                Nome = "Teclado",
                Descricao = "Teclado USB",
                Pontuacao = 50
            };

            // Act
            var resultado = await _controller.Create(novoProduto);

            // Assert
            Assert.That(resultado, Is.InstanceOf<CreatedAtActionResult>());
            var resultadoCriado = resultado as CreatedAtActionResult;
            Assert.That(resultadoCriado, Is.Not.Null);

            var produtoCriado = resultadoCriado!.Value as ProdutoResiduo;

            Assert.That(produtoCriado, Is.Not.Null);
            Assert.That(produtoCriado!.Nome, Is.EqualTo("Teclado"));
        }

        [Test]
        [TestCase("Teclado", "USB", 0)]      // BadRequest
        [TestCase("Mouse", "USB", -5)]       // BadRequest
        [TestCase("Pilha", "Alcalina", 10)]  // CreatedAtAction
        public async Task CreateProduto_ComportamentoDeveSerConformePontuacao(string nome, string descricao, int pontuacao)
        {
            // Arrange
            var produto = new ProdutoResiduo
            {
                Nome = nome,
                Descricao = descricao,
                Pontuacao = pontuacao
            };

            // Act
            var resultado = await _controller.Create(produto);

            // Assert
            if (pontuacao <= 0)
            {
                Assert.That(resultado, Is.InstanceOf<BadRequestObjectResult>());
            }
            else
            {
                Assert.That(resultado, Is.InstanceOf<CreatedAtActionResult>());

                if (resultado is CreatedAtActionResult created && created.Value is ProdutoResiduo produtoCriado)
                {
                    Assert.That(produtoCriado, Is.Not.Null);
                    Assert.That(produtoCriado.Pontuacao, Is.EqualTo(pontuacao));
                    Assert.That(produtoCriado.Nome, Is.EqualTo(nome));
                    Assert.That(produtoCriado.Descricao, Is.EqualTo(descricao));
                }
                else
                {
                    Assert.Fail("Resultado não contém um Produto válido.");
                }
            }
        }


        [Test]
        public void GetAllProdutos_DeveRetornarTodos()
        {
            // Act
            var resultado = _controller.GetAll().Result as OkObjectResult;

            // Assert
            Assert.That(resultado, Is.Not.Null);
            var lista = resultado.Value as List<ProdutoResiduo>;
            Assert.That(lista, Is.Not.Null);
            Assert.That(lista.Count, Is.EqualTo(2));

            Assert.That(lista.Any(p =>
             p.Nome == "Celular" &&
             p.Descricao == "Celular velho" &&
             p.Pontuacao == 100
             ), Is.True);

            Assert.That(lista.Any(p =>
             p.Nome == "Notebook" &&
             p.Descricao == "Notebook quebrado" &&
             p.Pontuacao == 150
             ), Is.True);
        }

        [Test]
        [TestCase(1, "Celular", "Celular velho", 100)] // id 1
        [TestCase(2, "Notebook", "Notebook quebrado", 150)] // id 2
        public void GetProdutoPorId_DeveRetornarProdutoCorreto(int id, string nome, string descricao, int pontuacao)
        {
            var produtoTeste = new ProdutoResiduo
            {
                Id = id,
                Nome = nome,
                Descricao = descricao,
                Pontuacao = pontuacao
            };

            var result =  _controller.GetById(id).Result as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            var produtoDb = result.Value as ProdutoResiduo;
            Assert.That(produtoDb.Id, Is.EqualTo(produtoTeste.Id));
            Assert.That(produtoDb.Nome, Is.EqualTo(produtoTeste.Nome));
            Assert.That(produtoDb.Descricao, Is.EqualTo(produtoTeste.Descricao));
            Assert.That(produtoDb.Pontuacao, Is.EqualTo(produtoTeste.Pontuacao));
        }

        [Test]
        public void AtualizarProduto_DeveModificarDados()
        {
            // Arrange
            var produto = new ProdutoResiduo { Id = 99, Nome = "Mouse", Descricao = "Mouse sem fio", Pontuacao = 50 };
            _context.ProdutosResiduos.Add(produto);
            _context.SaveChanges();

            // Act
            produto.Nome = "Mouse Gamer";
            produto.Descricao = "Mouse sem fio RGB";
            produto.Pontuacao = 70;
            _context.ProdutosResiduos.Update(produto);
            _context.SaveChanges();

            // Assert
            var produtoAtualizado = _context.ProdutosResiduos.Find(99);
            Assert.That(produtoAtualizado, Is.Not.Null);
            Assert.That(produtoAtualizado.Nome, Is.EqualTo("Mouse Gamer"));
            Assert.That(produtoAtualizado.Descricao, Is.EqualTo("Mouse sem fio RGB"));
            Assert.That(produtoAtualizado.Pontuacao, Is.EqualTo(70));
        }

        [Test]
        public void RemoverProduto_DeveExcluirDoBanco()
        {
            // Arrange
            var produto = new ProdutoResiduo { Id = 3, Nome = "Monitor", Descricao = "Monitor LCD", Pontuacao = 120 };
            _context.ProdutosResiduos.Add(produto);
            _context.SaveChanges();

            // Act
            _context.ProdutosResiduos.Remove(produto);
            _context.SaveChanges();

            // Assert
            var produtoRemovido = _context.ProdutosResiduos.Find(3);
            Assert.That(produtoRemovido, Is.Null);
        }
    }
}
