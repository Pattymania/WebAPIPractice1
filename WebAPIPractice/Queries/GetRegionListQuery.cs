using MediatR;
using WebAPIPractice.Models.Domain;

namespace WebAPIPractice.Queries
{
    public class GetRegionListQuery:IRequest<List<Region>>
    {
    }
}
