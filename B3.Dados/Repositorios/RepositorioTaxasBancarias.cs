﻿using B3.Dominio.Entidades;
using B3.Dominio.Interfaces.Repositorios;

namespace B3.Dados.Repositorios
{
    /// <summary>
    /// Repositório de taxas bancárias
    /// </summary>
    public class RepositorioTaxasBancarias : IRepositorioTaxasBancarias
    {
        /// <summary>
        /// Obtém as taxas bancárias vigentes no período atual
        /// </summary>
        /// <returns>Taxas bancárias</returns>
        public TaxasBancarias ObterTaxasBancariasVigentes()
        {
            return new TaxasBancarias
            {
                TaxaBancaria = 1.08M,
                TaxaCDI = 0.009M
            };
        }
    }
}
