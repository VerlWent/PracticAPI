using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.MediatR.Classes;
using PracticeAPI.MediatR.Сontainer;
using PracticeAPI.Model;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PracticeAPI.MediatR.Handler
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductMedia>>
    {
        DataContext _context = new DataContext();

        public async Task<IEnumerable<ProductMedia>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.ProductsTs
                .Select(x => new ProductMedia
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Image = x.Image,
                    Price = x.Price,
                    DateAdded = x.DateAdded,
                    CountInStock = x.CountInStock,
                    CategoryName = x.Category.Name
                })
                .ToListAsync(cancellationToken);

            return products;
        }
    }
}
