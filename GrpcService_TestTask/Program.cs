using GrpcService_TestTask.Core;
using GrpcService_TestTask.DAL.Core;
using GrpcService_TestTask.DAL.Core.Models;
using GrpcService_TestTask.DAL.DataAccessors;
using GrpcService_TestTask.Helpers;
using GrpcService_TestTask.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.Add(ServiceDescriptor.Describe(typeof(IMapper<IExternalMyData, IMyData>), provider => new MyDataMapper(), ServiceLifetime.Transient));
builder.Services.Add(ServiceDescriptor.Describe(typeof(IDataAccess<IExternalMyData, IMyData>), typeof(MyDataAccess), ServiceLifetime.Singleton));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<MyDataService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();