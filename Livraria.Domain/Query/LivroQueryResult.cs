using System;

namespace Livraria.Domain.Query
{
    public class LivroQueryResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Autor { get; set; }
        public int Edicao { get; set; }
        public string Isbn { get; set; }
        public string Imagem { get; set; }
    }
}