using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Parking.WebAPI.CoreHelper.Filters;
using Parking.WebAPI.CoreHelper.Middlewares;
using Parking.WebAPI.CoreHelper.Swagger;
using Parking.WebAPI.CoreHelper;
using Parking.WebAPI.UserService;
using Serilog;
using Parking.WebAPI.MasterService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});
builder.Services.AddLocalization(option => option.ResourcesPath = "Resources");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config => {
    config.OperationFilter<AcceptLanguageHeaderFilter>();
});


builder.Services.ConfigureCoreDependancyInjections();
builder.Services.CongigureUSDependancyInjections();
builder.Services.CongigureVSDependancyInjections();


builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure supported cultures
var supportedCultures = new[] { new CultureInfo("en-GB"), new CultureInfo("ta") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-GB"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/swagger/v1/swagger.json", "Parking APIs");
        option.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<JWTAuthMiddleware>();

app.MapControllers();

app.UseSerilogRequestLogging();

app.Run();

