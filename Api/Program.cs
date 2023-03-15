using CodeChops.Contracts.ExceptionHandlers;
using Fundalyzer.Application;
using Fundalyzer.Infrastructure.Api;

var builder = WebApplication.CreateBuilder(args);

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
    options.CacheProfiles.Add("5Seconds",
        new CacheProfile()
        {
            Duration = 5
        });
});

builder.Services.AddHealthChecks();

// Add (console) logging.
builder.Services.AddLogging();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// You cannot pass IOptions<> directly to a static method during startup configuration in C# .NET. :(
// This is because IOptions<> is typically used to inject configuration options into a class instance or service constructor.
var settings = builder.Configuration.GetSection(Settings.SectionName).Get<Settings>() ?? throw new ApplicationException("Configuration settings not found!");

// Add application layer registration.
builder.Services.AddApplicationLayer(settings);

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

app.UseExceptionHandler(a => a.Run(async context =>
    await context.RequestServices.GetRequiredService<RequestExceptionHandler>().HandleExceptionAsync()));

app.Run();
