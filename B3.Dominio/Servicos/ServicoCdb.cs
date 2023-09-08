using B3.Dominio.Entidades;
using B3.Dominio.Interfaces.Repositorios;
using B3.Dominio.Interfaces.Servicos;

namespace B3.Dominio.Servicos
{
    /// <summary>
    /// Serviço de negócio relacionado ao cálculo de CDB
    /// </summary>
    public class ServicoCdb : IServicoCdb
    {
        private readonly IRepositorioTaxasBancarias repositorioTaxasBancarias;
        private readonly IRepositorioFaixaImpostoRenda repositorioFaixaImpostoRenda;

        #region Construtor
        public ServicoCdb(IRepositorioTaxasBancarias repositorioTaxasBancarias, IRepositorioFaixaImpostoRenda repositorioFaixaImpostoRenda)
        {
            this.repositorioTaxasBancarias = repositorioTaxasBancarias;
            this.repositorioFaixaImpostoRenda = repositorioFaixaImpostoRenda;
        }
        #endregion


        /// <summary>
        /// Cálculo da rentabilidade do CDB
        /// </summary>
        /// <param name="valorInicial">Valor inicial da aplicação</param>
        /// <param name="prazoMeses">Prazo em meses da aplicação</param>
        /// <returns>Rentabilidade</returns>
        public RentabilidadeCdb CalcularInvestimento(decimal valorInicial, int prazoMeses)
        {
            RentabilidadeCdb rentabilidade = new()
            {
                ResultadoBruto = 0,
                ResultadoLiquido = 0
            };

            if (valorInicial <= 0 || prazoMeses <= 1)
                return rentabilidade;

            var taxasBancarias = repositorioTaxasBancarias.ObterTaxasBancariasVigentes();
            var faixaImpostoRenda = repositorioFaixaImpostoRenda.ObterFaixaImpostoRendaPorPrazo(prazoMeses);

            if (taxasBancarias == null || faixaImpostoRenda == null)
                return rentabilidade;


            decimal valorAplicacao = valorInicial;
            decimal valorFinal = 0;

            for (int i = 0; i < prazoMeses; i++)
            {
                valorFinal = Math.Round(valorAplicacao * (1 + (taxasBancarias.TaxaBancaria * taxasBancarias.TaxaCDI)), 4);
                valorAplicacao = valorFinal;
            }

            decimal valorRendimento = valorFinal - valorInicial;
            decimal valorLiquido = valorInicial + (valorRendimento * (1 - faixaImpostoRenda.PercentualImposto));

            rentabilidade.ResultadoBruto = Math.Round(valorFinal, 2);
            rentabilidade.ResultadoLiquido = Math.Round(valorLiquido, 2);

            return rentabilidade;
        }
    }
}
