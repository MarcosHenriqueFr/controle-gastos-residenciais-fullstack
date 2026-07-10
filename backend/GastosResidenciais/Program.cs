
using GastosResidenciais.Data;
using GastosResidenciais.Exceptions;
using GastosResidenciais.Services;
using Microsoft.EntityFrameworkCore;

const string CorsPolicy = "FrontendCorsPolicy";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<ITransacaoService, TransacaoService>();
builder.Services.AddScoped<IResumoService, ResumoService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("DataSource=app.db"));

// Com essa configuração, o backend permite a comunicação do frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandler>();
app.UseCors(CorsPolicy);

app.UseAuthorization();
app.MapControllers();

app.Run();