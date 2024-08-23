var builder = DistributedApplication.CreateBuilder(args);


//builder.AddProject<Projects.DistributedSystem_Aspire_Web>("webfrontend")
//    .WithExternalHttpEndpoints();


#region Product Api

var product_db = builder.AddConnectionString("productdb");

var project_api = builder.AddProject<Projects.DistributedSystem_Product_Api>("product-api")
    .WithReference(product_db);

var product_migration = builder.AddProject<Projects.DistributedSystem_Product_MigrationService>("product-migrationservice")
    .WithReference(product_db);

#endregion


builder.Build().Run();

//builder.AddProject<Projects.DistributedSystem_InventoryControl_Api>("inventorycontrol-api");

//builder.AddProject<Projects.DistributedSystem_LoyaltyCard_Api>("loyaltycard-api");

//builder.AddProject<Projects.DistributedSystem_PriceControl_Api>("pricecontrol-api");

//builder.AddProject<Projects.DistributedSystem_ReportsAnalysis_Api>("reportsanalysis-api");

//builder.AddProject<Projects.DistributedSystem_ApiGateway_Api>("distributedsystem-apigateway-api");

//builder.AddProject<Projects.DistributedSystem_FinanceApproval_Worker>("distributedsystem-financeapproval-worker");

//builder.AddProject<Projects.DistributedSystem_Client_Web>("distributedsystem-client-web");

//builder.AddProject<Projects.DistributedSystem_SaleOffer_Worker>("distributedsystem-saleoffer-worker");



