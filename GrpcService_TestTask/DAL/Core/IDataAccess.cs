using GrpcService_TestTask.DAL.Core.Models;

namespace GrpcService_TestTask.DAL.Core
{
    public interface IDataAccess<TExternal, TInner> where TExternal : IHasId
    {
        bool Add(TExternal model);
        bool Remove(int id);
        TExternal GetModel(int id);
        IEnumerable<TExternal> GetAll();
    }
}
