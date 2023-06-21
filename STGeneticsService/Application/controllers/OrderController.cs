using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace STGeneticsService.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<AnimalController> _logger;
    private readonly IConfiguration _configuration;

    private IOrderService _orderService;
    public OrderController(ILogger<AnimalController> logger, IConfiguration configuration, IOrderService orderService)
    {
        _logger = logger;
        _configuration = configuration;
        _orderService = orderService;
    }

    [HttpPost(Name = "CreateOrder")]
    [Authorize]
    public IActionResult CreateOrder([FromBody] List<OrderDto> orders)
    {
        
        _logger.LogInformation("OrderController.CreateOrder");
        if(orders == null)
        {
            _logger.LogInformation("OrderController.CreateOrder orders is null");
            return BadRequest();
        }

        OrderResponseDto response = _orderService.SaveOrder(orders);

        if (response.status.Equals("success"))
        {
            return Ok(response);
        }
        else
        {
            return StatusCode(500, response);
        }
       
    }

}