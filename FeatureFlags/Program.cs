using FeatureFlags;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
       .AddAzureAppConfiguration(options => options.Connect(builder.Configuration["ConnectionStrings:AppConfig"])
                                                   .UseFeatureFlags(p => p.CacheExpirationInterval = TimeSpan.FromSeconds(5)));

builder.Services.AddControllers();

builder.Services.AddFeatureManagement()
                .AddFeatureFilter<PercentageFilter>();

builder.Services.AddAzureAppConfiguration();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAzureAppConfiguration();
app.UseAuthorization();
app.MapControllers();
app.UseMiddlewareForFeature<FeatureFlagMiddleware>(FeatureFlagsNames.MidFlag);

app.Run();
