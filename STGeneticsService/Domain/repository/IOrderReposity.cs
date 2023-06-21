using STGeneticsService.Domain.Model.Entity;

namespace STGeneticsService.Domain.Repository
{
    public interface IOrderRepository 
    {

        IEnumerable<Orders> GetOrdersByFilter(string filter, string value); 

        int SaveOrder(List<Orders> order);
    }
}