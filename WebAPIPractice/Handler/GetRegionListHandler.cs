using MediatR;
using WebAPIPractice.DataAccess;
using WebAPIPractice.Models.Domain;
using WebAPIPractice.Queries;

namespace WebAPIPractice.Handler
{
    public class GetRegionListHandler : IRequestHandler<GetRegionListQuery, List<Region>>
    {
        private readonly IDataAccess dataAccess;

        public GetRegionListHandler(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }
        public Task<List<Region>> Handle(GetRegionListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(dataAccess.GetRegions());
        }
    }
}
