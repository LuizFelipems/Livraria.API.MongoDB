using Livraria.Domain.Entidades;
using Livraria.Domain.Query;
using System;
using System.Collections.Generic;

namespace Livraria.Domain.Interfaces.Repositories
{
    public interface ILivroRepository
    {
        void Inserir(Livro livro);
        void Alterar(Livro livro);
        void Deletar(Guid id);
        List<LivroQueryResult> Listar();
        LivroQueryResult ObterPorId(Guid id);
        bool CheckId(Guid id);
    }
}