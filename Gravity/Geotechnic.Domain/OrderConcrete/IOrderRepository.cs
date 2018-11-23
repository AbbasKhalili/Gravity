using Gravity.Domain;

namespace Geotechnic.Domain.OrderConcrete
{
    public interface IOrderRepository : IRepository<Order, OrderId>
    {
        long GetExampleNumber(long companyId);

    }
}