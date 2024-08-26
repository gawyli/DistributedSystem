using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);


//builder.AddProject<Projects.DistributedSystem_Aspire_Web>("webfrontend")
//    .WithExternalHttpEndpoints();

#region MessageBus
var rabbit_username = builder.AddParameter("rabbitusername", secret: true);
var rabbit_password = builder.AddParameter("rabbitpassword", secret: true);

var messagebus = builder.AddRabbitMQ("messagebus", userName: rabbit_username, password: rabbit_password)
    .WithManagementPlugin()
    .WithHttpEndpoint(port: 15672, targetPort: 15672);
#endregion

#region Postgres

var pg_username = builder.AddParameter("pgusername", secret: true);
var pg_password = builder.AddParameter("pgpassword", secret: true);

var postgres = builder.AddPostgres("postgres", userName: pg_username, password: pg_password)
    .WithPgAdmin();

var document_db = postgres.AddDatabase("messagebrokerdb");

#endregion

#region Storage

var storage = builder.AddAzureStorage("storage");

if (builder.Environment.IsDevelopment())
{
    storage.RunAsEmulator(configure =>
    {
        configure.WithBlobPort(10000)
            .WithQueuePort(10001)
            .WithTablePort(10002);
    });
}
var blobs = storage.AddBlobs("blobconnection");
var queues = storage.AddQueues("queueconnection");

#endregion

#region MessageBroker Api

var messagebroker_api = builder.AddProject<Projects.DistributedSystem_MessageBroker_Api>("messagebroker-api")
    .WithReference(document_db)
    .WithReference(messagebus);

var messagebroker_migration = builder.AddProject<Projects.DistributedSystem_MessageBroker_MigrationService>("messagebroker-migrationservice")
    .WithReference(document_db);

#endregion

#region Product Api

var product_db = builder.AddConnectionString("productdb");

var project_api = builder.AddProject<Projects.DistributedSystem_Product_Api>("product-api")
    .WithReference(product_db)
    .WithReference(document_db)
    .WithReference(messagebus);

var product_migration = builder.AddProject<Projects.DistributedSystem_Product_MigrationService>("product-migrationservice")
    .WithReference(product_db);

var product_workerservice = builder.AddProject<Projects.DistributedSystem_Product_WorkerService>("product-workerservice")
    .WithReference(messagebus);
#endregion

#region Inventory Control Api

var inventory_api = builder.AddProject<Projects.DistributedSystem_InventoryControl_Api>("inventorycontrol-api")
    .WithReference(messagebus);


var inventory_workerservice = builder.AddProject<Projects.DistributedSystem_InventoryControl_WorkerService>("inventorycontrol-workerservice")
    .WithReference(messagebus);

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



