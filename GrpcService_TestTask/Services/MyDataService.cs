using Grpc.Core;
using GrpcService_TestTask.Core;
using GrpcService_TestTask.DAL.Core.Models;
using GrpcService_TestTask.DAL.Core;
using GrpcService_TestTask.Models;
using GrpcService_TestTask.Exceptions;

namespace GrpcService_TestTask.Services
{
    public class MyDataService : Dater.DaterBase
    {
        private readonly ILogger<MyDataService> _logger;
        private readonly IDataAccess<IExternalMyData, IMyData> _dataAccess;
        public MyDataService(ILogger<MyDataService> logger, IDataAccess<IExternalMyData, IMyData> dataAccess)
        {
            _logger = logger;
            _dataAccess = dataAccess;
        }

        public override Task<AddReply> Add(AddRequest request, ServerCallContext context)
        {
            return Task<AddReply>.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(request.Data))
                    return new AddReply() { IsAdded = false };

                try
                {
                    var isSuccsess = _dataAccess.Add(new ExternalMyData(request.Id, request.Data));
                    return new AddReply { IsAdded = isSuccsess };
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message + e.InnerException?.Message);
                    return new AddReply() { IsAdded = false };
                }
            });
        }
        public override Task<RemoveReply> Remove(RemoveRequest request, ServerCallContext context)
        {
            return Task<RemoveReply>.Run(() =>
            {
                var isSuccsess = _dataAccess.Remove(request.Id);
                return new RemoveReply { IsRemoved = isSuccsess };
            });
        }

        public override Task<GetReply> Get(GetRequest request, ServerCallContext context)
        {
            return Task<GetReply>.Run(() =>
            {
                IExternalMyData res;
                try
                {
                    res = _dataAccess.GetModel(request.Id);
                }
                catch (UnknownDataRequested<IExternalMyData> e)
                {
                    _logger.LogError(e.Message);
                    return new GetReply { Id = request.Id, IsSuccess = false };
                }
                return new GetReply { Id = res.Id, Data = res.Text, IsSuccess = true };
            });
        }
        public override Task<GetAllReply> GetAll(GetAllRequest request, ServerCallContext context)
        {
            return Task<GetAllReply>.Run(() =>
            {
                var res = _dataAccess.GetAll();
                var reppl = new GetAllReply();
                reppl.Replies.AddRange(res.Select(r => new GetReply() { Id = r.Id, Data = r.Text, IsSuccess = true }));

                return reppl;
            });
        }
    }
}
