using GrpcService_TestTask.DAL.Core.Models;

namespace GrpcService_TestTask.Exceptions
{
    public class UnknownDataRequested<T> : ApplicationException where T : IHasId
    {
        public UnknownDataRequested(int id) : base($"Item {typeof(T)} wasn't found")
        {

        }
    }
}
