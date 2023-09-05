using System;

namespace Livraria.Domain.Entidades
{
    public class Livro
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Autor { get; private set; }
        public int Edicao { get; private set; }
        public string Isbn { get; private set; }
        public string Imagem { get; private set; }

        public Livro(string nome, string autor, int edicao, string isbn, string imagem)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Autor = autor;
            Edicao = edicao;
            Isbn = isbn;
            Imagem = imagem;
        }

        public Livro(Guid id, string nome, string autor, int edicao, string isbn, string imagem)
        {
            Id = id;
            Nome = nome;
            Autor = autor;
            Edicao = edicao;
            Isbn = isbn;
            Imagem = imagem;
        }
    }
}