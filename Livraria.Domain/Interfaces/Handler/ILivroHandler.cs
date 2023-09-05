using Livraria.Domain.Commands.Livro.Input;
using Livraria.Domain.Interfaces.Commands;

namespace Livraria.Domain.Interfaces.Handler
{
    public interface ILivroHandler : ICommandHandler<AdicionarLivroCommand>,
                                     ICommandHandler<AtualizarLivroCommand>,
                                     ICommandHandler<ApagarLivroCommand>
    {
    }
}