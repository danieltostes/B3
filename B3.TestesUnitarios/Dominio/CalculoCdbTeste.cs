using B3.Dominio.Interfaces.Repositorios;
using B3.Dominio.Interfaces.Servicos;
using B3.Dominio.Servicos;
using B3.TestesUnitarios.Setups;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace B3.TestesUnitarios.Dominio
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

        #region Testes
        [Theory(DisplayName = "Cálculo da rentabilidade CDB")]
        [Trait("Cálculo CDB", "Serviços")]
        [InlineData(0, 1)]
        [InlineData(1000, 6)]
        [InlineData(1000, 12)]
        [InlineData(1000, 24)]
        [InlineData(1000, 36)]
        public void CalcularRendimentoCdb(decimal valorInicial, int prazoMeses)
        {
            #region Valores esperados para meses aplicados
            var resultadosEsperados = new Dictionary<int, decimal[]>
            {
                { 1, new decimal[]{ 0, 0 } },
                { 6, new decimal[]{ 1059.76M, 1046.31M } },
                {12, new decimal[]{ 1123.08M, 1098.47M } },
                {24, new decimal[]{ 1261.31M, 1215.58M } },
                {36, new decimal[]{ 1416.56M, 1354.07M } }
            };
            #endregion

            SetupRepositorioTaxasBancarias.Setup(repositorioTaxasBancarias);
            SetupRepositorioFaixasImpostoRenda.Setup(repositorioFaixaImpostoRenda, prazoMeses);

            Assert.NotNull(servicoCdb);

            var rentabilidade = servicoCdb?.CalcularInvestimento(valorInicial, prazoMeses);

            Assert.Equal(resultadosEsperados[prazoMeses][0], rentabilidade?.ResultadoBruto);
            Assert.Equal(resultadosEsperados[prazoMeses][1], rentabilidade?.ResultadoLiquido);
        }

        [Fact]
        [Trait("Crítica Taxas Bancarias Nula", "Serviços")]
        public void CriticarTaxasBancariasNula()
        {
            SetupRepositorioFaixasImpostoRenda.Setup(repositorioFaixaImpostoRenda, 6);

            Assert.NotNull(servicoCdb);

            var rentabilidade = servicoCdb?.CalcularInvestimento(1000, 6);

            Assert.Equal(0, rentabilidade?.ResultadoBruto);
            Assert.Equal(0, rentabilidade?.ResultadoLiquido);
        }

        [Fact]
        [Trait("Crítica Faixa Imposto de Renda Nula", "Serviços")]
        public void CriticarFaixaImpostoRendaNula()
        {
            SetupRepositorioTaxasBancarias.Setup(repositorioTaxasBancarias);

            Assert.NotNull(servicoCdb);

            var rentabilidade = servicoCdb?.CalcularInvestimento(1000, 6);

            Assert.Equal(0, rentabilidade?.ResultadoBruto);
            Assert.Equal(0, rentabilidade?.ResultadoLiquido);
        }
        #endregion
    }
}
