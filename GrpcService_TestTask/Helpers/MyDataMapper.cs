using GrpcService_TestTask.Core;
using GrpcService_TestTask.DAL.Core.Models;
using GrpcService_TestTask.DAL.Models;
using GrpcService_TestTask.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace GrpcService_TestTask.Helpers
{
    public class MyDataMapper : IMapper<IExternalMyData, IMyData>
    {
        public IExternalMyData Cast(IMyData value)
        {
            var model = value as InnerMyData;
            if (model == null)
            {
                var er = $"Unable to cast {value.GetType()} to {typeof(InnerMyData)}";
                throw new InvalidCastException(er);
            }
            return new ExternalMyData(model.Id, model.Text);
        }

        public IMyData Cast(IExternalMyData value)
        {
            return new InnerMyData(value.Id, value.Text);
        }
    }
}
