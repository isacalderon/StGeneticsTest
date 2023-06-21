

using STGeneticsService.Domain.Model.Entity;
using STGeneticsService.Domain.Repository;

public class AnimalService : IAnimalService
{
    IConfiguration _configuration;
    ILogger<AnimalService> _logger;

    IAnimalRepository _animalRepository;

    Dictionary<string, int> generos = new Dictionary<string, int>()
    {
        { "Femenino", 1 },
        { "Masculino", 2 }
    };

    public AnimalService(IConfiguration configuration, ILogger<AnimalService> logger, IAnimalRepository animalRepository)
    {
        _configuration = configuration;
        _logger = logger;
        _animalRepository = animalRepository;
    }
    
    public bool DeleteAnimal(int id)
    {
        var resultDelete = _animalRepository.DeleteAnimal(id); 
        resultDelete.Wait();
        _logger.LogInformation("task estatus: " + resultDelete.IsCompletedSuccessfully);
        if(resultDelete.IsCompleted)
        {
            _logger.LogInformation("AnimalService.DeleteAnimal success");
            return resultDelete.IsCompletedSuccessfully;
        }
        _logger.LogInformation("AnimalService.DeleteAnimal: Fail " );
        return false;
    }

    public List<AnimalDto> GetAnimalsByFilter(string filter, string value)
    {
         List<Animal> queryList = new List<Animal>();

         if(filter == "status")
             queryList = _animalRepository.GetAnimalsbyStatus(value.Equals("active") ? 1 : 0).ToList();
             
         if(filter == "name")    
             queryList = _animalRepository.GetAnimalsbyName(value).ToList();

        if(filter == "sexo" )
        {
            int sexo = 0; 
            generos.TryGetValue(value, out sexo); 
            queryList = _animalRepository.GetAnimalsbySex(sexo).ToList(); 
           
        }

        return  ToListAnimalDto(queryList); ;
    }

    public bool SaveAnimal(AnimalDto animal)
    {
        Animal animalEntity = new Animal();
        animalEntity.name = animal.name;
        animalEntity.breed = animal.breed;
        animalEntity.birthDate = animal.birthDate;
        animalEntity.sex = 1; 
        animalEntity.price = (double)animal.price;
        animalEntity.status = 1;
        var resultInsert = _animalRepository.InsertAnimal(animalEntity);
        resultInsert.Wait();
        _logger.LogInformation("task estatus: " + resultInsert.IsCompletedSuccessfully);
        if(resultInsert.IsCompleted)
        {
            _logger.LogInformation("AnimalService.SaveAnimal: " + resultInsert);
            return resultInsert.IsCompletedSuccessfully;
        }
        _logger.LogInformation("AnimalService.SaveAnimal: " + resultInsert);
        return false;
    }

     public bool UpdateAnimal(AnimalDto animal)
     {
        Animal animalEntity = new Animal();
        animalEntity.name = animal.name;
        animalEntity.breed = animal.breed;
        animalEntity.birthDate = animal.birthDate;
        animalEntity.sex = 1; 
        animalEntity.price = (double)animal.price;
        animalEntity.status = 1;
        animalEntity.animalId = animal.animalId;
        var resultUpdate = _animalRepository.UpdateAnimal(animalEntity);
        resultUpdate.Wait();
          _logger.LogInformation("task estatus: " + resultUpdate.IsCompletedSuccessfully);
        if(resultUpdate.IsCompleted)
        {
            _logger.LogInformation("AnimalService.Update: " + resultUpdate);
            return resultUpdate.IsCompletedSuccessfully;
        }
        _logger.LogInformation("AnimalService.Update: " + resultUpdate);
        return false;
     }

     private List<AnimalDto> ToListAnimalDto (List<Animal> originList)
     {
         List<AnimalDto> resultList = new List<AnimalDto>();

         if(originList.Count == 0)
            return resultList;
                
           originList.ForEach(val => {
                AnimalDto animal = new AnimalDto();
                animal.name = val.name;
                animal.breed = val.breed;
                animal.birthDate = val.birthDate;
                animal.price = (decimal)val.price;
                resultList.Add(animal);
            }); 
            
        return resultList;
     }
}