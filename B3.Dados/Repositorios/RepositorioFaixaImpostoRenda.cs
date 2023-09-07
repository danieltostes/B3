using B3.Dominio.Entidades;
using B3.Dominio.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3.Dados.Repositorios
{
    /// <summary>
    /// Repositório de faixa de imposto de renda
    /// </summary>
    public class RepositorioFaixaImpostoRenda : IRepositorioFaixaImpostoRenda
    {
        /// <summary>
        /// Obtém uma faixa de imposto de renda em função do número de meses da aplicação
        /// </summary>
        /// <param name="meses">Número de meses da aplicação</param>
        /// <returns>Faixa de imposto de renda</returns>
        public FaixaImpostoRenda ObterFaixaImpostoRendaPorPrazo(int meses)
        {
            var tabela = new Dictionary<int, decimal>
            {
                { 6, 0.225M },
                { 12, 0.20M },
                { 24, 0.175M },
                { 9999, 0.15M }
            };

            FaixaImpostoRenda faixaImposto = new ();
            var faixaAtribuida = false;

            foreach (var faixa in tabela)
            {
                if (meses <= faixa.Key)
                {
                    faixaImposto.PrazoMeses = faixa.Key;
                    faixaImposto.PercentualImposto = faixa.Value;
                    faixaAtribuida = true;
                    break;
                }
            }

            if (!faixaAtribuida)
            {
                faixaImposto.PrazoMeses = tabela.Last().Key;
                faixaImposto.PercentualImposto = tabela.Last().Value;
            }

            return faixaImposto;
        }
    }
}
