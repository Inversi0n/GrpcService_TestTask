Invoke-Expression "dotnet dev-certs https -ep ./grpctest.pfx -p Qwerty12"
Invoke-Expression "dotnet dev-certs https --trust"