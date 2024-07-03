var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.DistributedSystem_Aspire_HostApp_ApiService>("apiservice");

builder.AddProject<Projects.DistributedSystem_Aspire_HostApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
