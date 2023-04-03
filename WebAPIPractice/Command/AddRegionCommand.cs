using MediatR;
using WebAPIPractice.Models.Domain;

namespace WebAPIPractice.Command
{
    public record AddRegionCommand(Region Region) : IRequest<Region>;
}
