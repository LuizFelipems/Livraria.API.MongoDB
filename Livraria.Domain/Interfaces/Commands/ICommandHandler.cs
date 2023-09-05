namespace Livraria.Domain.Interfaces.Commands
{
    public interface ICommandHandler<T> where T : ICommandPadrao
    {
        ICommandResult Handle(T command);
    }
}