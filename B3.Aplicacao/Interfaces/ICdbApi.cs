using B3.Aplicacao.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3.Aplicacao.Interfaces
{
    /// <summary>
    /// Interface para a API de Serviços CDB
    /// </summary>
    public interface ICdbApi
    {
        /// <summary>
        /// Cálculo do investimento CDB
        /// </summary>
        /// <param name="valorInicial">Valor inicial da aplicação</param>
        /// <param name="prazoMeses">Prazo em meses da aplicação</param>
        /// <returns>Rentabilidade do investimento</returns>
        RentabilidadeCdbDto CalcularInvestimento(decimal valorInicial, int prazoMeses);
    }
}
