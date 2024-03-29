﻿using B3.Aplicacao;
using B3.Aplicacao.Interfaces;
using B3.Dominio.Interfaces.Repositorios;
using B3.Dominio.Interfaces.Servicos;
using B3.Dominio.Servicos;
using B3.Infraestrutura.CrossCuting.Mapper;
using B3.TestesUnitarios.Setups;
using Moq;
using System.Collections.Generic;
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

            SetupRepositorioTaxasBancarias.Setup(repositorioTaxasBancarias);

            servicoCdb = new ServicoCdb(repositorioTaxasBancarias.Object, repositorioFaixaImpostoRenda.Object);
            cdbApi = new CdbApi(servicoCdb, mapper);
        }
        #endregion

        #region Testes
        [Theory]
        [Trait("Cálculo CDB", "Aplicação")]
        [InlineData(0, 1)]
        [InlineData(3500, 6)]
        [InlineData(3500, 12)]
        [InlineData(3500, 24)]
        [InlineData(3500, 36)]
        public void CalcularRendimentoCdb(decimal valorInicial, int prazoMeses)
        {
            SetupRepositorioFaixasImpostoRenda.Setup(repositorioFaixaImpostoRenda, prazoMeses);

            #region Valores esperados para meses aplicados
            var resultadosEsperados = new Dictionary<int, decimal[]>
            {
                { 1, new decimal[]{ 0, 0 } },
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
