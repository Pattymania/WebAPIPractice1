using MediatR;
using WebAPIPractice.Data;
using WebAPIPractice.DataAccess;
using WebAPIPractice.Models.Domain;
using WebAPIPractice.Queries;

namespace WebAPIPractice.Handler
{
    public class GetRegionHandler : IRequestHandler<GetRegionQuery, Region>
    {
        private readonly IDataAccess dataAccess;

        public GetRegionHandler(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }
        public Task<Region> Handle(GetRegionQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(dataAccess.GetRegion(request.id));
        }
    }
}
