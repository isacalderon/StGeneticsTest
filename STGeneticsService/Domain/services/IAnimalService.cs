using STGeneticsService.Domain.Model.Entity;

public interface IAnimalService
{
    List<AnimalDto> GetAnimalsByFilter(string filter, string value); 

    bool SaveAnimal(AnimalDto animal);

    bool UpdateAnimal(AnimalDto animal);

    bool DeleteAnimal(int id);


}