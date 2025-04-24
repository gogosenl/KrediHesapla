using Common.KrediPortal.Interfaces;
using DataLayer.KrediPortal;
using Service.KrediPortal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<KrediHesaplaDataAccess>();
builder.Services.AddScoped<KrediHesaplaService>();
builder.Services.AddScoped<IKrediService, KrediService>();
builder.Services.AddScoped<IKrediDetay,KrediDetayService>();
builder.Services.AddScoped<IBasvuru, KullaniciBasvuruService>();





builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:Configuration"];
    options.Configuration = builder.Configuration.GetSection("Redis")["Configuration"];
    options.InstanceName = builder.Configuration["Redis:InstanceName"];
});
builder.Services.AddControllers();

builder.Services.AddCors(options =>

{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin() // veya .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
