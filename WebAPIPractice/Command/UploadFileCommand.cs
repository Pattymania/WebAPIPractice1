using MediatR;
using WebAPIPractice.Models.Domain;

namespace WebAPIPractice.Command
{
    public record UploadFileCommand(List<IFormFile> files, string subDirectory) : IRequest<bool>;
}
