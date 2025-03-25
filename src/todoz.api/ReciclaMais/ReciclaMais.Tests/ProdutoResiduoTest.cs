using Microsoft.EntityFrameworkCore;
using ReciclaMaisAPI.Data;
using ReciclaMaisAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ReciclaMais.Tests
{
    [TestFixture]
    public class ProdutoResiduoTest
    {
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "reciclamaisdb")
                .Options;

            _context = new AppDbContext(options);

            _context.ProdutosResiduos.AddRange(
                new ProdutoResiduo { Id = 1, Nome = "Celular", Descricao = "Celular velho", Pontuacao = 100 },
                new ProdutoResiduo { Id = 2, Nome = "Notebook", Descricao = "Notebook quebrado", Pontuacao = 150 }
            );
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void AdicionarProduto_DeveAumentarContagem()
        {
            var novo = new ProdutoResiduo { Nome = "Tablet", Descricao = "Tablet antigo", Pontuacao = 80 };
            _context.ProdutosResiduos.Add(novo);
            _context.SaveChanges();

            Assert.That(_context.ProdutosResiduos.Count(), Is.EqualTo(3));
        }

        [Test]
        public void ProdutoComPontuacaoZero_DeveFalharNaValidacaoManual()
        {
            var produto = new ProdutoResiduo
            {
                Nome = "Item",
                Descricao = "Teste",
                Pontuacao = 0
            };

            var context = new ValidationContext(produto);
            var results = new List<ValidationResult>();

            bool valido = Validator.TryValidateObject(produto, context, results, true);

            Assert.That(valido, Is.False);
            Assert.That(results.Any(r => r.MemberNames.Contains("Pontuacao")));
        }



        [Test]
        public void GetAllProdutos_DeveRetornarTodos()
        {

        }

        [Test]
        public void GetProdutoPorId_DeveRetornarProdutoCorreto()
        {

        }

        [Test]
        public void AtualizarProduto_DeveModificarDados()
        {

        }

        [Test]
        public void RemoverProduto_DeveExcluirDoBanco()
        {

        }
    }
}
