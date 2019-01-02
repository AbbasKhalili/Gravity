using Gravity.Application;
using Gravity.NHibernate;
using NHibernate;

namespace Geotechnic.Facade.Services.Tests.Unit
{
    internal class CommandBusFake<TCommand> : ICommandBus
    {
        private readonly ISession _session;
        private readonly ICommandHandler<TCommand> _commandHandler;
        
        public CommandBusFake(ICommandHandler<TCommand> commandHandler, ISession session)
        {
            _commandHandler = commandHandler;
            _session = session;
        }

        public void Dispatch<T>(T command) 
        {
            var uow = new NhUnitOfWork(_session);
            var execute = new TransactionalCommandHandlerDecorator<T>(uow, _commandHandler as ICommandHandler<T>);
            execute.Handle(command);
        }
    }
}