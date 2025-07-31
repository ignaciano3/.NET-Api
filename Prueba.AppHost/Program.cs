var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.PruebaApi>("api");

builder.AddProject<Projects.Frontend>("frontend")
    .WithReference(api)
    .WaitFor(api);

builder.Build().Run();
