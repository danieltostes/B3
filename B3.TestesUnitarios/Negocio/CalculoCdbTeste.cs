using B3.Dominio.Entidades;
using B3.Dominio.Interfaces.Repositorios;
using B3.Dominio.Interfaces.Servicos;
using B3.Dominio.Servicos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace B3.TestesUnitarios.Negocio
{
    public class CalculoCdbTeste
    {
        #region Mocks
        private readonly Mock<IRepositorioTaxasBancarias> repositorioTaxasBancarias = new();
        private readonly Mock<IRepositorioFaixaImpostoRenda> repositorioFaixaImpostoRenda = new();
        #endregion

        #region Serviços
        private readonly IServicoCdb? servicoCdb;
        #endregion

        #region Construtor
        public CalculoCdbTeste()
        {
            servicoCdb = new ServicoCdb(repositorioTaxasBancarias.Object, repositorioFaixaImpostoRenda.Object);
        }
        #endregion

        #region Mocks
        private static Dictionary<int, decimal> ListarFaixasImpostoRenda()
        {
            return new Dictionary<int, decimal>
            {
                {0, 0.225M},
                {6, 0.225M},
                {12, 0.2M},
                {24, 0.175M},
                {36, 0.15M},
            };
        }

        private void SetupMockTaxasBancarias()
        {
            repositorioTaxasBancarias.Setup(repositorio => repositorio.ObterTaxasBancariasVigentes())
                .Returns(new TaxasBancarias
                {
                    TaxaBancaria = 1.08M,
                    TaxaCDI = 0.009M
                });
        }

        private void SetupMockFaixasImpostoRenda(int prazoMeses)
        {
            var faixasImposto = ListarFaixasImpostoRenda();

            repositorioFaixaImpostoRenda.Setup(repositorio => repositorio.ObterFaixaImpostoRendaPorPrazo(It.IsAny<int>()))
                .Returns(new FaixaImpostoRenda
                {
                    PrazoMeses = prazoMeses,
                    PercentualImposto = faixasImposto[prazoMeses]
                });
        }
        #endregion

        #region Testes
        [Theory(DisplayName = "Cálculo da rentabilidade CDB")]
        [Trait("Cálculo CDB", "Serviços")]
        [InlineData(1000, 6)]
        [InlineData(1000, 12)]
        [InlineData(1000, 24)]
        [InlineData(1000, 36)]
        [InlineData(0, 0)]
        public void CalcularRendimentoCdb(decimal valorInicial, int prazoMeses)
        {
            #region Valores esperados para meses aplicados
            var resultadosEsperados = new Dictionary<int, decimal[] >
            {
                { 0, new decimal[]{ 0, 0 } },
                { 6, new decimal[]{ 1059.76M, 1046.31M } },
                {12, new decimal[]{ 1123.08M, 1098.47M } },
                {24, new decimal[]{ 1261.31M, 1215.58M } },
                {36, new decimal[]{ 1416.56M, 1354.07M } }
            };
            #endregion

            SetupMockTaxasBancarias();
            SetupMockFaixasImpostoRenda(prazoMeses);

            Assert.NotNull(servicoCdb);

            var rentabilidade = servicoCdb?.CalcularInvestimento(valorInicial, prazoMeses);
            
            Assert.Equal(resultadosEsperados[prazoMeses][0], rentabilidade?.ResultadoBruto);
            Assert.Equal(resultadosEsperados[prazoMeses][1], rentabilidade?.ResultadoLiquido);
        }

        [Fact]
        [Trait("Crítica Taxas Bancarias Nula", "Serviços")]
        public void CriticarTaxasBancariasNula()
        {
            SetupMockFaixasImpostoRenda(6);

            Assert.NotNull(servicoCdb);

            var rentabilidade = servicoCdb?.CalcularInvestimento(1000, 6);

            Assert.Equal(0, rentabilidade?.ResultadoBruto);
            Assert.Equal(0, rentabilidade?.ResultadoLiquido);
        }

        [Fact]
        [Trait("Crítica Faixa Imposto de Renda Nula", "Serviços")]
        public void CriticarFaixaImpostoRendaNula()
        {
            SetupMockTaxasBancarias();

            Assert.NotNull(servicoCdb);

            var rentabilidade = servicoCdb?.CalcularInvestimento(1000, 6);

            Assert.Equal(0, rentabilidade?.ResultadoBruto);
            Assert.Equal(0, rentabilidade?.ResultadoLiquido);
        }
        #endregion
    }
}
