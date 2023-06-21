using STGeneticsService.Domain.Model.Entity;

namespace STGeneticsService.Domain.Repository
{
    public interface IUserRepository
    {
        Users GetUser(string userName, string password);
    }
}