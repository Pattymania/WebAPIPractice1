using MediatR;
using WebAPIPractice.Command;
using WebAPIPractice.DataAccess;
using WebAPIPractice.Models.Domain;

namespace WebAPIPractice.Handler
{
    public class AddRegionHandler : IRequestHandler<AddRegionCommand, Region>
    {
        private readonly IDataAccess dataAccess;

        public AddRegionHandler(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }
        public Task<Region> Handle(AddRegionCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(dataAccess.AddRegion(request.Region));
        }
    }
}
