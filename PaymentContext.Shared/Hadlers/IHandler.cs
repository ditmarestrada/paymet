
using PaymentContext.Shared.Commands;

namespace PaymentContext.Shared.Hadlers
{
    public interface IHandler<T> where T  : ICommand
    {
         ICommandResult Handle(T command);
    }
}