using System;

namespace Gravity.Application
{
    public interface ICommandBus
    {
        void Dispatch<T>(T command);
    }
}
