using Logsquare.Infrastructure;
using Logsquare.Query;
using Logsquare.Command;
using Logsquare.AthenticationProvider;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthenticationRegistration(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddQuery(builder.Configuration);
builder.Services.AddCommand(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
