using GrpcService_TestTask.Core;
using GrpcService_TestTask.DAL.Core;
using GrpcService_TestTask.DAL.Core.Models;

namespace GrpcService_TestTask.DAL.Models
{
    public class InnerMyData : IMyData
    {
        public int Id { get; }

        public string Text { get; set; }

        public InnerMyData(int id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}
