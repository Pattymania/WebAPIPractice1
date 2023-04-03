using MediatR;
using WebAPIPractice.Command;
using WebAPIPractice.DataAccess;

namespace WebAPIPractice.Handler
{
    public class UploadFileHandler:IRequestHandler<UploadFileCommand,bool>
    {
        private readonly IDataAccess dataAccess;

        public UploadFileHandler(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public Task<bool> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(dataAccess.UploadFile(request.files,request.subDirectory));
        }
    }
}
