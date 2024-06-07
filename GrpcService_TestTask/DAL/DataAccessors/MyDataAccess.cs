using GrpcService_TestTask.Core;
using GrpcService_TestTask.DAL.Base;
using GrpcService_TestTask.DAL.Core;
using GrpcService_TestTask.DAL.Core.Models;

namespace GrpcService_TestTask.DAL.DataAccessors
{
    public class MyDataAccess : ADataAccess<IExternalMyData, IMyData>, IDataAccess<IExternalMyData, IMyData>
    {
        public MyDataAccess(IMapper<IExternalMyData, IMyData> mapper) : base(mapper)
        {
        }
    }
}
