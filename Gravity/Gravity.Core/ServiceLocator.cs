namespace Gravity.Core
{
    public class ServiceLocator
    {
        public static IServiceLocator Current { get; private set; }
        public static void Set(IServiceLocator serviceLocator)
        {
            Current = serviceLocator;
        }
    }
}
