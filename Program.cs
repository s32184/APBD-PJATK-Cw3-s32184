using ExampleApp.Repositories;
using ExampleApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddSingleton<IRoomRepository, RoomRepository>();
builder.Services.AddSingleton<IReservationRepository, ReservationRepository>();

builder.Services.AddTransient<IRoomService, RoomService>();
builder.Services.AddTransient<IReservationService, ReservationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/openapi/v1.json", "v1");   
    });
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();