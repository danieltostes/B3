using B3.Dominio.Entidades;

namespace B3.Dominio.Interfaces.Servicos
{
    /// <summary>
    /// Interface para os serviços de negócio relacionados ao cálculo do CDB
    /// </summary>
    public interface IServicoCdb
    {
        /// <summary>
        /// Cálculo do investimento CDB
        /// </summary>
        /// <param name="valorInicial">Valor inicial da aplicação</param>
        /// <param name="prazoMeses">Prazo em meses da aplicação</param>
        /// <returns>Rentabilidade do investimento</returns>
        RentabilidadeCdb CalcularInvestimento(decimal valorInicial, int prazoMeses);
    }
}
