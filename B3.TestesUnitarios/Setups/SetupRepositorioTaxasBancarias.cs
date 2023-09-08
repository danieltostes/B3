using B3.Dominio.Entidades;
using B3.Dominio.Interfaces.Repositorios;
using Moq;

namespace B3.TestesUnitarios.Setups
{
    public static class SetupRepositorioTaxasBancarias
    {
        public static void Setup(Mock<IRepositorioTaxasBancarias> repositorio)
        {
            repositorio.Setup(repositorio => repositorio.ObterTaxasBancariasVigentes())
                .Returns(new TaxasBancarias
                {
                    TaxaBancaria = 1.08M,
                    TaxaCDI = 0.009M
                });
        }
    }
}
