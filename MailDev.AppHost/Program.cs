var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Berrevoets_Aspire_TestApp>("berrevoets-aspire-testapp");

builder.Build().Run();
