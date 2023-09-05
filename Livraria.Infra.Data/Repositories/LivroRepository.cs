using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Domain.Query;
using Livraria.Infra.Data.DataContexts;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Livraria.Infra.Data.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private BsonDocument document = new BsonDocument();
        private readonly DataContext _dataContext;

        public LivroRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Inserir(Livro livro)
        {
            try
            {
                IMongoCollection<BsonDocument> insert = _dataContext.MongoDBConexao.GetCollection<BsonDocument>("Livro");

                document.Add("_id", livro.Id);
                document.Add("Nome", livro.Nome);
                document.Add("Autor", livro.Autor);
                document.Add("Edicao", livro.Edicao);
                document.Add("Isbn", livro.Isbn);
                document.Add("Imagem", livro.Imagem);

                insert.InsertOne(document);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Alterar(Livro livro)
        {
            try
            {
                IMongoCollection<LivroQueryResult> update = _dataContext.MongoDBConexao.GetCollection<LivroQueryResult>("Livro");
                Expression<Func<LivroQueryResult, bool>> filter = x => x.Id.Equals(livro.Id);
                LivroQueryResult livroQR = update.Find(filter).FirstOrDefault();

                if (livroQR != null)
                {
                    livroQR.Nome = livro.Nome;
                    livroQR.Autor = livro.Autor;
                    livroQR.Edicao = livro.Edicao;
                    livroQR.Isbn = livro.Isbn;
                    livroQR.Imagem = livro.Imagem;

                    ReplaceOneResult result = update.ReplaceOne(filter, livroQR);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Deletar(Guid id)
        {
            try
            {
                IMongoCollection<LivroQueryResult> delete = _dataContext.MongoDBConexao.GetCollection<LivroQueryResult>("Livro");
                Expression<Func<LivroQueryResult, bool>> filtro = x => x.Id.Equals(id);
                DeleteResult result = delete.DeleteOne(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LivroQueryResult> Listar()
        {
            try
            {
                var collection = _dataContext.MongoDBConexao.GetCollection<LivroQueryResult>("Livro");

                var result = (from x in collection.AsQueryable()
                              orderby x.Id ascending
                              select x).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LivroQueryResult ObterPorId(Guid id)
        {
            try
            {
                var collection = _dataContext.MongoDBConexao.GetCollection<LivroQueryResult>("Livro");

                var result = (from x in collection.AsQueryable()
                              where x.Id == id
                              select x).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckId(Guid id)
        {            
            try
            {
                IMongoCollection<LivroQueryResult> query = _dataContext.MongoDBConexao.GetCollection<LivroQueryResult>("Livro");
                Expression<Func<LivroQueryResult, bool>> filtro = x => x.Id.Equals(id);
                IList<LivroQueryResult> usuario = query.Find(filtro).ToList();

                return usuario.Count != 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}