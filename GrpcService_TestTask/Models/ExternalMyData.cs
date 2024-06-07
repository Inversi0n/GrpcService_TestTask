using GrpcService_TestTask.Core;
using GrpcService_TestTask.DAL.Core.Models;

namespace GrpcService_TestTask.Models
{
    public class ExternalMyData : IExternalMyData
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public ExternalMyData(int id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}
