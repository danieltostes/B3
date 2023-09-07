using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3.Dominio.Entidades
{
    /// <summary>
    /// Taxas praticadas pelo Banco
    /// </summary>
    public class TaxasBancarias
    {
        /// <summary>
        /// Taxa paga pelo banco sobre o CDI
        /// </summary>
        public decimal TaxaBancaria { get; set; }

        /// <summary>
        /// Taxa CDI do último mês
        /// </summary>
        public decimal TaxaCDI { get; set; }
    }
}
