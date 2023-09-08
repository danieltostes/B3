using B3.Dominio.Entidades;
using B3.Dominio.Interfaces.Repositorios;
using Moq;
using System.Collections.Generic;

namespace B3.TestesUnitarios.Setups
{
    public static class SetupRepositorioFaixasImpostoRenda
    {
        public static void Setup(Mock<IRepositorioFaixaImpostoRenda> repositorio, int prazoMeses)
        {
            var faixasImposto = new Dictionary<int, decimal>
            {
                {1, 0.225M},
                {6, 0.225M},
                {12, 0.2M},
                {24, 0.175M},
                {36, 0.15M},
            };

            repositorio.Setup(repositorio => repositorio.ObterFaixaImpostoRendaPorPrazo(It.IsAny<int>()))
                .Returns(new FaixaImpostoRenda
                {
                    PrazoMeses = prazoMeses,
                    PercentualImposto = faixasImposto[prazoMeses]
                });
        }
    }
}
