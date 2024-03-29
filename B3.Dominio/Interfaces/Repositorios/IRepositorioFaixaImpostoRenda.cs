﻿using B3.Dominio.Entidades;

namespace B3.Dominio.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para o repositório de faixa de imposto de renda
    /// </summary>
    public interface IRepositorioFaixaImpostoRenda
    {
        /// <summary>
        /// Obtém uma faixa de imposto de renda em função do número de meses da aplicação
        /// </summary>
        /// <param name="meses">Número de meses da aplicação</param>
        /// <returns>Faixa de imposto de renda</returns>
        FaixaImpostoRenda ObterFaixaImpostoRendaPorPrazo(int meses);
    }
}
