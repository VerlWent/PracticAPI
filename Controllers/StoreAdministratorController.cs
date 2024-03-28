using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI.Model;

namespace PracticeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreAdministratorController : Controller
    {
        private readonly ILogger<StoreAdministratorController> logger;

        public StoreAdministratorController(ILogger<StoreAdministratorController> logger)
        {
            this.logger = logger;
        }

        DataContext _context = new DataContext();

        [Authorize(Roles = "2")]
        [HttpGet("GetAllProduct")]
        public IActionResult GetAllProduct()
        {
            var result = _context.ProductsTs.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                DateAdded = x.DateAdded,
                Image = x.Image,
                Price = x.Price,
                Category = x.Category.Name,
                CountInStock = x.CountInStock
            });

            return Ok(result);
        }

        [Authorize(Roles = "2")]
        [HttpPut("EditProduct")]
        public IActionResult EditProduct(int Id, string Name, string Description, string Image, double Price, String Category, int CountInStock)
        {
            var GetProduct = _context.ProductsTs.FirstOrDefault(x => x.Id == Id);

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

            GetProduct.Name = Name;
            GetProduct.Description = Description;
            GetProduct.DateAdded = currentDate;
            GetProduct.Image = Image;
            GetProduct.Price = Price;
            
            if (Category == "Посуда")
            {
                GetProduct.CategoryId = 1;
            }

            GetProduct.CountInStock = CountInStock;

            _context.Update(GetProduct);
            _context.SaveChanges();

            return Ok();
        }

        [Authorize(Roles = "2")]
        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(string Name, string Description, string Image, double Price, String Category, int CountInStock)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

            ProductsT newProduct = new ProductsT
            {
                Name = Name,
                Description = Description,
                DateAdded = currentDate,
                Image = Image,
                Price = Price,
                CountInStock = CountInStock
            };

            if (Category == "Посуда")
            {
                newProduct.CategoryId = 1;
            }

            _context.ProductsTs.Add(newProduct);
            _context.SaveChanges();
            return Ok();
        }

        [Authorize(Roles = "2")]
        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int Id)
        {
            var result = _context.ProductsTs.FirstOrDefault(x => x.Id == Id);

            _context.ProductsTs.Remove(result);
            _context.SaveChanges();

            return Ok();
        }
    }
}
