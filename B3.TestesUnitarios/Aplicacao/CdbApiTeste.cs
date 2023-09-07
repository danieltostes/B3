using B3.Aplicacao;
using B3.Aplicacao.Interfaces;
using B3.Dominio.Entidades;
using B3.Dominio.Interfaces.Repositorios;
using B3.Dominio.Interfaces.Servicos;
using B3.Dominio.Servicos;
using B3.Infraestrutura.CrossCuting.Mapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace B3.TestesUnitarios.Aplicacao
{
    public class CdbApiTeste
    {
        #region Mocks
        private readonly Mock<IRepositorioTaxasBancarias> repositorioTaxasBancarias = new();
        private readonly Mock<IRepositorioFaixaImpostoRenda> repositorioFaixaImpostoRenda = new();
        #endregion

        #region APIs
        private readonly ICdbApi cdbApi;
        private readonly IServicoCdb servicoCdb;
        #endregion

        #region Construtor
        public CdbApiTeste()
        {
            var mapper = AutoMapperConfig.Mapper();

            #region Setup do mock Taxas Bancárias
            repositorioTaxasBancarias.Setup(repositorio => repositorio.ObterTaxasBancariasVigentes())
                .Returns(new TaxasBancarias
                {
                    TaxaBancaria = 1.08M,
                    TaxaCDI = 0.009M
                });
            #endregion

            servicoCdb = new ServicoCdb(repositorioTaxasBancarias.Object, repositorioFaixaImpostoRenda.Object);
            cdbApi = new CdbApi(servicoCdb, mapper);
        }
        #endregion

        #region Testes
        [Theory]
        [Trait("Cálculo CDB", "Aplicação")]
        [InlineData(0, 0)]
        [InlineData(3500, 6)]
        [InlineData(3500, 12)]
        [InlineData(3500, 24)]
        [InlineData(3500, 36)]
        public void CalcularRendimentoCdb(decimal valorInicial, int prazoMeses)
        {
            #region Setup do mock Faixa de imposto de renda
            var faixasImposto = new Dictionary<int, decimal>
            {
                {0, 0.225M},
                {6, 0.225M},
                {12, 0.2M},
                {24, 0.175M},
                {36, 0.15M},
            };

            repositorioFaixaImpostoRenda.Setup(repositorio => repositorio.ObterFaixaImpostoRendaPorPrazo(It.IsAny<int>()))
                .Returns(new FaixaImpostoRenda
                {
                    PrazoMeses = prazoMeses,
                    PercentualImposto = faixasImposto[prazoMeses]
                });
            #endregion

            #region Valores esperados para meses aplicados
            var resultadosEsperados = new Dictionary<int, decimal[]>
            {
                { 0, new decimal[]{ 0, 0 } },
                { 6, new decimal[]{ 3709.14M, 3662.09M } },
                {12, new decimal[]{ 3930.79M, 3844.63M } },
                {24, new decimal[]{ 4414.60M, 4254.54M } },
                {36, new decimal[]{ 4957.95M, 4739.26M } }
            };
            #endregion

            Assert.NotNull(cdbApi);

            var rentabilidade = cdbApi.CalcularInvestimento(valorInicial, prazoMeses);

            Assert.Equal(resultadosEsperados[prazoMeses][0], rentabilidade?.ResultadoBruto);
            Assert.Equal(resultadosEsperados[prazoMeses][1], rentabilidade?.ResultadoLiquido);
        }
        #endregion
    }
}
