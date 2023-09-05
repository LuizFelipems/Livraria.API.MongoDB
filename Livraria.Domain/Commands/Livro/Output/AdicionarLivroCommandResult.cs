using Livraria.Domain.Interfaces.Commands;

namespace Livraria.Domain.Commands.Livro.Output
{
    public class AdicionarLivroCommandResult : ICommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public AdicionarLivroCommandResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}