using Gravity.Tools;

namespace Geotechnic.Domain.OrderConcrete.Exceptions
{
    public class ExampleNumberException : InternalException 
    {
        public ExampleNumberException() : base(1001)
        {
        }
    }

    public class ProjectException : InternalException
    {
        public ProjectException() : base(1002)
        {
        }
    }

    public class ExampleDateException : InternalException
    {
        public ExampleDateException() : base(1003)
        {
        }
    }

    public class ExamplePlaceException : InternalException
    {
        public ExamplePlaceException() : base(1004)
        {
        }
    }

    public class CementTypeException : InternalException
    {
        public CementTypeException() : base(1005)
        {
        }
    }

    public class BreakTemplateException : InternalException
    {
        public BreakTemplateException() : base(1006)
        {
        }
    }
}
