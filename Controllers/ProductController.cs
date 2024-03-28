using MediatR;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI.MediatR.Сontainer;
using PracticeAPI.Model;

namespace PracticeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        DataContext _context = new DataContext();

        [HttpGet]
        public IActionResult GetProduct()
        {
            var result = _context.ProductsTs;

            return Ok(result);
        }


        //private readonly IMediator _mediator;

        //public ProductController(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    var result = await _mediator.Send(new GetProductsQuery());

        //    return Ok(result);
        //}
    }
}
