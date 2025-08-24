using Microsoft.EntityFrameworkCore;
using PagilaGraphQL.Data;
using PagilaGraphQL.GraphQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PagilaContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=pagila;Username=postgres;Password=password")
    .UseSnakeCaseNamingConvention());

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddFiltering()
    .AddSorting()
    .AddProjections();

var app = builder.Build();

app.MapGraphQL("/graphql");

app.Run();
