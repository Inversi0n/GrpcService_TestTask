# GrpcService_TestTask
For starting the service, you need to run prepare srypt with powerShall and accept what it's asking for, it will generate grpctest file.
You need to open powerShall from the root folder(where README is located) and run these commands:

docker build -f GrpcService_TestTask\Dockerfile -t grpc-server .

 docker run -e "ASPNETCORE_Kestrel__Certificates__Default__Path=/https/grpctest.pfx" -e "ASPNETCORE_Kestrel__Certificates__Default__Password=Qwerty12" -v "$(PWD)\grpctest.pfx:/https/grpctest.pfx"
 -p 5001:8081 -e "ASPNETCORE_ENVIRONMENT=Development" -e "ASPNETCORE_HTTPS_PORTS=8081" --name grpctest grpc-server 
 
You can use GrpcService_TestTask\Protos\mydata.proto to test the programm in postman
Or you could run a console application ClientTester. But you have to build it first

For postman:
Create a new gRPC request, use localhost:5001 as a URL, and proto file as a template

For console applicatoin:
Use command Add, Get or Remove to change the states of the further commands. GetAll doesn't change the state, but show you the resuls immidiatley.
While Adding use {int string} template without brackets, and replace int and string with you any values
While Get and Remove use {int} template without brackets, and replace int with the values you want to get or remove

What could be improved in project on my side:
I wanted to add a factory for further mappers (didn't use automapper on purpose)
I didn't really bother with the clean-code in the client application, it was just for testing
I did't added Swagger comments
I didn't really add any logs(only critical) and saving them
Client application could be also running in container
Kestrel certificates are built uses simple password
I didn't implement tests
Could be added a qeue with validation and saving, instead of saving data directly
I don't save anything after service is stopping, so all data become lost.
Since we'are saving data im RAM, we can run out of it, i didn't add any validation or restrictions for that.
It works as a single instance, but it could've be duplicated and synchronizing  with each other for balancing the high-load.

I work with GRPC for the first time and i didn't properly estimated the time for this. Most of the time i spent setting up the ports and certificaes