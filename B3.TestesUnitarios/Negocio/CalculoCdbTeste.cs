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
            #region Setup repositório taxas bancárias
            repositorioTaxasBancarias.Setup(repositorio => repositorio.ObterTaxasBancariasVigentes())
                .Returns(new TaxasBancarias
                {
                    TaxaBancaria = 1.08M,
                    TaxaCDI = 0.009M
                });
            #endregion

            servicoCdb = new ServicoCdb(repositorioTaxasBancarias.Object, repositorioFaixaImpostoRenda.Object);
        }
        #endregion

        #region Testes
        [Theory(DisplayName = "Cálculo da rentabilidade CDB")]
        [Trait("Cálculo CDB", "Serviços")]
        [InlineData(1000, 6)]
        [InlineData(1000, 12)]
        [InlineData(1000, 24)]
        [InlineData(1000, 36)]
        public void CalcularRendimentoCdb(decimal valorInicial, int prazoMeses)
        {
            #region Faixas de Imposto para Mock
            var faixasImposto = new Dictionary<int, decimal>
            {
                {6, 0.225M},
                {12, 0.2M},
                {24, 0.175M},
                {36, 0.15M},
            };
            #endregion

            #region Valores esperados para meses aplicados
            var resultadosEsperados = new Dictionary<int, decimal[] >
            {
                { 6, new decimal[]{ 1059.76M, 1046.31M } },
                {12, new decimal[]{ 1123.08M, 1098.47M } },
                {24, new decimal[]{ 1261.31M, 1215.58M } },
                {36, new decimal[]{ 1416.56M, 1354.07M } }
            };
            #endregion

            #region Setup repositório faixas de imposto de renda
            repositorioFaixaImpostoRenda.Setup(repositorio => repositorio.ObterFaixaImpostoRendaPorPrazo(It.IsAny<int>()))
                .Returns(new FaixaImpostoRenda
                {
                    PrazoMeses = prazoMeses,
                    PercentualImposto = faixasImposto[prazoMeses]
                });
            #endregion

            Assert.NotNull(servicoCdb);

            var rentabilidade = servicoCdb?.CalcularInvestimento(valorInicial, prazoMeses);
            
            Assert.Equal(resultadosEsperados[prazoMeses][0], rentabilidade?.ResultadoBruto);
            Assert.Equal(resultadosEsperados[prazoMeses][1], rentabilidade?.ResultadoLiquido);
        }
        #endregion
    }
}
