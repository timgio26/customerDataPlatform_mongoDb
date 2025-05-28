using CustomerDataPlatform.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CustomerService>(builder.Configuration.GetSection("CdpDatabase"));
builder.Services.AddSingleton<CustomerService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
                      policyBuilder =>
                      {
                          policyBuilder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173","http://localhost:3000");
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

 app.Run();
//app.Run("http://0.0.0.0:5144");


