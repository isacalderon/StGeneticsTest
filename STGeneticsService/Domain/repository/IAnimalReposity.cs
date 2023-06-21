using STGeneticsService.Domain.Model.Entity;

namespace STGeneticsService.Domain.Repository
{
    public interface IAnimalRepository{

        IEnumerable<Animal> GetAnimalsbySex(int sex);

        IEnumerable<Animal> GetAnimalsbyName(string name);

        IEnumerable<Animal> GetAnimalsbyStatus(int status); 

        Animal GetAnimalById(int id); 

        Task<bool> InsertAnimal(Animal animal);

        Task<bool> UpdateAnimal(Animal animal);

        Task<bool> DeleteAnimal(int id);


    }
}