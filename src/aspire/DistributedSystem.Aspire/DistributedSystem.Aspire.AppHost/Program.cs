var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.DistributedSystem_Aspire_ApiService>("apiservice");

builder.AddProject<Projects.DistributedSystem_Aspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.AddProject<Projects.DistributedSystem_FinanceApproval_Api>("financeapproval-api");

builder.AddProject<Projects.DistributedSystem_InventoryControl_Api>("inventorycontrol-api");

builder.AddProject<Projects.DistributedSystem_LoyaltyCard_Api>("loyaltycard-api");

builder.AddProject<Projects.DistributedSystem_PriceControl_Api>("pricecontrol-api");

builder.AddProject<Projects.DistributedSystem_ReportsAnalysis_Api>("reportsanalysis-api");

builder.Build().Run();
