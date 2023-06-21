using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace STGeneticsService.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalController: ControllerBase
{
    private readonly ILogger<AnimalController> _logger;
    private readonly IConfiguration _configuration;

    private readonly IAnimalService _animalService;

    public AnimalController(ILogger<AnimalController> logger, IConfiguration configuration, IAnimalService animalService)
    {
        _logger = logger;
        _configuration = configuration;
        _animalService = animalService;
    }

    [HttpGet(Name = "GetAnimal")]
    [Authorize]
    public IActionResult GetAnimal([FromQuery] string filter, string value)
    { 
        _logger.LogInformation("AnimalController.GetAnimal filter = " + filter + " value = " + value);
        return Ok(_animalService.GetAnimalsByFilter(filter, value));
    }

    [HttpPost(Name = "CreateAnimal")]
    [Authorize]
    public IActionResult CreateAnimal(AnimalDto animal)
    {
        _logger.LogInformation("AnimalController.CreateAnimal");
        if(_animalService.SaveAnimal(animal)) 
           return Ok(); 
        else 
           return StatusCode(409); 
       
    }

    [HttpPut(Name = "UpdateAnimal")]
    [Authorize]
    public IActionResult UpdateAnimal(AnimalDto animal)
    {
        _logger.LogInformation("AnimalController.UpdateAnimal");
        if(_animalService.UpdateAnimal(animal)) 
            return Ok(); 
        else 
            return NotFound(); 
    } 

    [HttpDelete("animal/{id}")]
    [Authorize]
    public IActionResult DeleteAnimal([FromRoute] int id)
    {
        _logger.LogInformation("AnimalController.DeleteAnimal");
        if(_animalService.DeleteAnimal(id)) 
            return Ok(); 
        else 
            return NotFound(); 
    }
}
