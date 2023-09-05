using Flunt.Notifications;
using Livraria.Domain.Commands.Livro.Input;
using Livraria.Domain.Commands.Livro.Output;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Commands;
using Livraria.Domain.Interfaces.Handler;
using Livraria.Domain.Interfaces.Repositories;
using System;

namespace Livraria.Domain.Handlers
{
    public class LivroHandler : Notifiable, ILivroHandler 
    {
        private readonly ILivroRepository _repository;

        public LivroHandler(ILivroRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(AdicionarLivroCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new AdicionarLivroCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notifications);

                string nome = command.Nome;
                string autor = command.Autor;
                int edicao = command.Edicao;
                string isbn = command.Isbn;
                string imagem = command.Imagem;

                Livro livro = new Livro(nome, autor, edicao, isbn, imagem);

                _repository.Inserir(livro);
                                
                var retorno = new AdicionarLivroCommandResult(true, "Livro gravado com sucesso!", new
                {
                    Id = livro.Id,
                    Nome = livro.Nome,
                    Autor = livro.Autor,
                    Edicao = livro.Edicao,
                    Isbn = livro.Isbn,
                    Imagem = livro.Imagem
                });

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICommandResult Handle(AtualizarLivroCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new AtualizarLivroCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notifications);

                if (!_repository.CheckId(command.Id))
                {
                    AddNotification("Id", "Id inválido. Este id não está cadastrado!");
                    return new AtualizarLivroCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notifications);
                }

                Guid id = command.Id;
                string nome = command.Nome;
                string autor = command.Autor;
                int edicao = command.Edicao;
                string isbn = command.Isbn;
                string imagem = command.Imagem;

                Livro livro = new Livro(id, nome, autor, edicao, isbn, imagem);

                _repository.Alterar(livro);

                var retorno = new AtualizarLivroCommandResult(true, "Livro alterado com sucesso!", new
                {
                    Id = livro.Id,
                    Nome = livro.Nome,
                    Autor = livro.Autor,
                    Edicao = livro.Edicao,
                    Isbn = livro.Isbn,
                    Imagem = livro.Imagem
                });

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICommandResult Handle(ApagarLivroCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new ApagarLivroCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notifications);

                if (!_repository.CheckId(command.Id))
                {
                    AddNotification("Id", "Id inválido. Este id não está cadastrado!");
                    return new ApagarLivroCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notifications);
                }

                _repository.Deletar(command.Id);

                var retorno = new ApagarLivroCommandResult(true, "Livro excluído com sucesso!", new
                {
                    Id = command.Id
                });

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}