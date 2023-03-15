using CodeChops.Contracts.ExceptionHandlers;
using Fundalyzer.Application;
using Fundalyzer.Infrastructure.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add swagger (and UI).
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(a => a.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Api.xml")));

builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
});

// Add response caching.
builder.Services.AddResponseCaching();
builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("30Seconds",
        new CacheProfile()
        {
            Duration = 30
        });
});

builder.Services.AddHealthChecks();

// Add (console) logging.
builder.Services.AddLogging();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// You cannot pass IOptions<> directly to a static method during startup configuration in C# .NET. :(
// This is because IOptions<> is typically used to inject configuration options into a class instance or service constructor.
var configRoot = builder.Configuration.GetRequiredSection(Settings.SectionName);

// Add application layer registration.
builder.Services.AddApplicationLayer(configRoot);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseResponseCaching();

app.UseHealthChecks("/health");

app.UseExceptionHandler(app => app.Run(async context =>
    await context.RequestServices.GetRequiredService<RequestExceptionHandler>().HandleExceptionAsync()));

app.Run();
