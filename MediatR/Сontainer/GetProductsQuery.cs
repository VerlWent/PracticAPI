using MediatR;
using PracticeAPI.MediatR.Classes;
using PracticeAPI.Model;

namespace PracticeAPI.MediatR.Сontainer
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductMedia>>
    {

    }
}
