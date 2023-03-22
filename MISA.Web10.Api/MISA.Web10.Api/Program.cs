using MISA.Web10.Core.Interfaces.Repository;
using MISA.Web10.Core.Interfaces.Services;
using MISA.Web10.Core.Services;
using MISA.Web10.Infrastructor.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//set data trả về dạng pascal case (MsM)
builder.Services.AddControllers().AddJsonOptions(jsonOption =>
{
    jsonOption.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmulationRepository, EmulationRepository>();
builder.Services.AddScoped<IEmulationService, EmulationService>();
builder.Services.AddScoped<ICommendationLevelRepository, CommendationLevelRepository>();
builder.Services.AddScoped<ICommendationLevelService, CommendationLevelService>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app cors
app.UseCors("corsapp");
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
