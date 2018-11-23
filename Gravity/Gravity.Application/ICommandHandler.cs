namespace Gravity.Application
{
    public interface ICommandHandler
    {

    }

    public interface ICommandHandler<in T> : ICommandHandler
    {
        void Handle(T handle);
    }
}
