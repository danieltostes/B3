using B3.Dominio.Entidades;

namespace B3.Dominio.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para o repositório de taxas bancárias
    /// </summary>
    public interface IRepositorioTaxasBancarias
    {
        /// <summary>
        /// Obtém as taxas bancárias vigentes no período atual
        /// </summary>
        /// <returns>Taxas bancárias</returns>
        TaxasBancarias ObterTaxasBancariasVigentes();
    }
}
