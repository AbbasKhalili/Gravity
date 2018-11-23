using Gravity.Core;

namespace Gravity.Application
{
    public class CommandBus : ICommandBus
    {
        public void Dispatch<T>(T command)
        {
            var execute = ServiceLocator.Current.GetInstance<TransactionalCommandHandlerDecorator<T>>();
            execute.Handle(command);
            ServiceLocator.Current.Release(execute);
        }
    }
}
