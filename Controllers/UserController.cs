using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.Model;
using System.Security.Claims;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PracticeAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : Controller
	{
        private readonly ILogger<UserController> logger;

        public UserController(ILogger<UserController> logger)
        {
            this.logger = logger;
        }

        DataContext _context = new DataContext();

        //[Authorize]
        //[HttpPost("BuyFullCart")]
        //public IActionResult BuyFullCart(List<int> OrderList)
        //{
        //	UsersT usersT = new UsersT();
        //	usersT = GetCurrectUser();

        //	OrdersT ordersT = new OrdersT();

        //          DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

        //          foreach (var item in OrderList)
        //          {
        //              ordersT = new OrdersT()
        //              {
        //                  UserId = usersT.Id,
        //                  ProductKeyId = item,

        //                  DatePurchase = currentDate
        //              };
        //              _context.OrdersTs.Add(ordersT);
        //          }

        //	_context.SaveChanges();

        //	return Ok();
        //}

        [Authorize]
        [HttpPost("BuyFullCart")]
        public IActionResult BuyFullCart(List<NewOrderClass> order)
        {
            UsersT usersT = new UsersT();
            usersT = GetCurrectUser();

            OrdersT ordersT = new OrdersT();

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

            foreach (var item in order)
            {
                ordersT = new OrdersT()
                {
                    UserId = usersT.Id,
                    ProductKeyId = item.IdProduct,
                    Count = item.Count,

                    DatePurchase = currentDate
                };
                _context.OrdersTs.Add(ordersT);
            }

            _context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpGet("GetCart")]
        public IActionResult GetCart()
        {
            UsersT usersT = new UsersT();
            usersT = GetCurrectUser();

            //var result = _context.ProductsTs.Where(x => x.OrdersTs.Any(x => x.UserId == usersT.Id)).ToList();

            var result = _context.OrdersTs.Select(x => new
            {
                UserId = x.UserId,
                DatePurchase = x.DatePurchase,
                Count = x.Count,
                Name = x.ProductKey.Name,
                Image = x.ProductKey.Image,
                Price = x.ProductKey.Price
            }).Where(x => x.UserId == usersT.Id);
            return Ok(result);
        }

        [NonAction]
        public UsersT GetCurrectUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UsersT
                {
                    Id = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                    NickName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                };
            }
            return null;
        }

    }
}
