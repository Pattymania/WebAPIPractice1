using MediatR;
using WebAPIPractice.Models.Domain;

namespace WebAPIPractice.Queries
{
    public class GetRegionQuery:IRequest<Region>
    {
        public Guid id { get;}

        public GetRegionQuery(Guid Id)
        {
            id = Id;
        }
    }
}
