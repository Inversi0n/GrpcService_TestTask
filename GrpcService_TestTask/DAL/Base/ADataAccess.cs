using GrpcService_TestTask.Core;
using GrpcService_TestTask.DAL.Core;
using GrpcService_TestTask.DAL.Core.Models;
using GrpcService_TestTask.Exceptions;
using System.Collections.Concurrent;

namespace GrpcService_TestTask.DAL.Base
{
    public class ADataAccess<TExternal, TInner> : IDataAccess<TExternal, TInner>
        where TExternal : IHasId
    {
        protected readonly IDictionary<int, TInner> Data = new ConcurrentDictionary<int, TInner>();
        protected readonly IMapper<TExternal, TInner> Mapper;

        public ADataAccess(IMapper<TExternal, TInner> mapper)
        {
            Mapper = mapper;
        }

        public virtual bool Add(TExternal model)
        {
            var m2 = Mapper.Cast(model);
            return Data.TryAdd(model.Id, m2);
        }      

        public virtual TExternal GetModel(int id)
        {
            var isFound = Data.TryGetValue(id, out var model);
                     
            if (!isFound || model == null)
            {
                throw new UnknownDataRequested<TExternal>(id);
            }

            return Mapper.Cast(model);
        }

        public virtual bool Remove(int id)
        {
            return Data.Remove(id);
        }

        public virtual IEnumerable<TExternal> GetAll()
        {
            return Data.Values.Select(Mapper.Cast);
        }
    }
}
