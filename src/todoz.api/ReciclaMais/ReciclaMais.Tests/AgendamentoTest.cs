using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReciclaMaisAPI.Controllers;
using ReciclaMaisAPI.Data;
using ReciclaMaisAPI.Models;
using ReciclaMaisAPI.Models.Dto;
using ReciclaMaisAPI.Models.Enum;

namespace ReciclaMais.Tests
{
    [TestFixture]
    public class AgendamentoTest
    {
        private AppDbContext _context;
        private AgendamentosController _controller;
        private int _produtoId;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("AgendamentoTestDb")
                .Options;

            _context = new AppDbContext(options);

            var produto = new ProdutoResiduo
            {
                Nome = "Bateria",
                Descricao = "Bateria comum",
                Pontuacao = 100
            };

            _context.ProdutosResiduos.Add(produto);
            _context.SaveChanges();

            _produtoId = produto.Id;
            _controller = new AgendamentosController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        [TestCase(1, TestName = "CreateComQuantidadeMaiorQueZero_DeveRetornarCreated")]
        [TestCase(0, TestName = "CreateComQuantidadeIgualAZero_DeveRetornarBadRequest")]
        [TestCase(-5, TestName = "CreateComQuantidadeNegativa_DeveRetornarBadRequest")]
        public async Task Create_ValidaQuantidadeProduto(int quantidade)
        {
            // Arrange.
            var dto = new AgendamentoCreateDTO
            {
                Data = new DateTime(2100, 1, 1),
                Hora = new TimeSpan(10, 0, 0),
                ItensColeta = new List<ItemColetaCreateDTO>
                {
                    new ItemColetaCreateDTO
                    {
                        ProdutoId = _produtoId,
                        Quantidade = quantidade,
                        Estado = (int)EstadoConservacao.Descarte
                    }
                }
            };

            // Act.
            var resultado = await _controller.Create(dto);

            // Assert.
            if (quantidade > 0)
            {
                Assert.That(resultado, Is.InstanceOf<CreatedAtActionResult>());
            }
            else
            {
                Assert.That(resultado, Is.InstanceOf<BadRequestObjectResult>());
            }
        }

        [Test]
        [TestCase(1, true, TestName = "CreateComDataFutura_DeveRetornarCreated")]
        [TestCase(-1, false, TestName = "CreateComDataPassada_DeveRetornarBadRequest")]
        public async Task Create_ValidaDataAgendamento(int diasParaAdicionar, bool esperadoSucesso)
        {
            // Arrange.
            var dto = new AgendamentoCreateDTO
            {
                Data = DateTime.Today.AddDays(diasParaAdicionar),
                Hora = new TimeSpan(10, 0, 0),
                ItensColeta = new List<ItemColetaCreateDTO>
                {
                    new ItemColetaCreateDTO
                    {
                        ProdutoId = _produtoId,
                        Quantidade = 1,
                        Estado = (int)EstadoConservacao.Doacao
                    }
                }
            };

            // Act.
            var resultado = await _controller.Create(dto);

            // Assert.
            if (esperadoSucesso)
            {
                Assert.That(resultado, Is.InstanceOf<CreatedAtActionResult>(), "Retorno: 201 Created");
            }
            else
            {
                Assert.That(resultado, Is.InstanceOf<BadRequestObjectResult>(), "Retorno: 400 BadRequest");
            }
        }


        [Test]
        public async Task GetAll_DeveRetornarLista()
        {
            // Arrange.
            await _controller.Create(new AgendamentoCreateDTO
            {
                Data = new DateTime(2100, 1, 1),
                Hora = new TimeSpan(10, 0, 0),
                ItensColeta = new List<ItemColetaCreateDTO>
                {
                    new ItemColetaCreateDTO
                    {
                        ProdutoId = _produtoId,
                        Quantidade = 1,
                        Estado = (int)EstadoConservacao.Doacao
                    }
                }
            });

            // Act.
            var resultado = await _controller.GetAll() as OkObjectResult;

            // Assert.
            Assert.That(resultado, Is.Not.Null);
            var lista = resultado!.Value as List<AgendamentoResponseDTO>;
            Assert.That(lista, Is.Not.Null);
            Assert.That(lista!.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task GetByIdExistente_DeveRetornarAgendamento()
        {
            // Arrange.
            var createResult = await _controller.Create(new AgendamentoCreateDTO
            {
                Data = new DateTime(2100, 1, 1),
                Hora = new TimeSpan(10, 0, 0),
                ItensColeta = new List<ItemColetaCreateDTO>
                {
                    new ItemColetaCreateDTO
                    {
                        ProdutoId = _produtoId,
                        Quantidade = 1,
                        Estado = (int)EstadoConservacao.Doacao
                    }
                }
            }) as CreatedAtActionResult;

            var agendamento = createResult!.Value as Agendamento;

            // Act.
            var resultado = await _controller.GetById(agendamento!.Id) as OkObjectResult;

            // Assert.
            Assert.That(resultado, Is.Not.Null);
            var dto = resultado!.Value as AgendamentoResponseDTO;
            Assert.That(dto, Is.Not.Null);
            Assert.That(dto!.Id, Is.EqualTo(agendamento.Id));
        }

        [Test]
        public async Task Update_DeveAlterarAgendamento()
        {
            // Arrange.
            var createResult = await _controller.Create(new AgendamentoCreateDTO
            {
                Data = new DateTime(2100, 1, 1),
                Hora = new TimeSpan(10, 0, 0),
                ItensColeta = new List<ItemColetaCreateDTO>
                {
                    new ItemColetaCreateDTO
                    {
                        ProdutoId = _produtoId,
                        Quantidade = 1,
                        Estado = (int)EstadoConservacao.Doacao
                    }
                }
            }) as CreatedAtActionResult;

            var agendamento = createResult!.Value as Agendamento;

            var dtoAtualizado = new AgendamentoCreateDTO
            {
                Data = agendamento!.Data.AddDays(1),
                Hora = new TimeSpan(15, 0, 0),
                ItensColeta = new List<ItemColetaCreateDTO>
                {
                    new ItemColetaCreateDTO
                    {
                        ProdutoId = _produtoId,
                        Quantidade = 5,
                        Estado = (int)EstadoConservacao.Doacao
                    }
                }
            };

            // Act.
            var resultado = await _controller.Update(agendamento.Id, dtoAtualizado);

            // Assert.
            Assert.That(resultado, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task Delete_DeveRemoverAgendamento()
        {
            // Arrange.
            var createResult = await _controller.Create(new AgendamentoCreateDTO
            {
                Data = new DateTime(2100, 1, 1),
                Hora = new TimeSpan(10, 0, 0),
                ItensColeta = new List<ItemColetaCreateDTO>
                {
                    new ItemColetaCreateDTO
                    {
                        ProdutoId = _produtoId,
                        Quantidade = 1,
                        Estado = (int)EstadoConservacao.Doacao
                    }
                }
            }) as CreatedAtActionResult;

            var agendamento = createResult!.Value as Agendamento;

            // Act.
            var resultado = await _controller.Delete(agendamento!.Id);

            // Assert.
            Assert.That(resultado, Is.InstanceOf<NoContentResult>());
            Assert.That(_context.Agendamentos.Any(a => a.Id == agendamento.Id), Is.False);
        }
    }
}
