using SEGUROS.TesteTecnico.APPLICATION.UseCases;
using SEGUROS.TesteTecnico.DOMAIN.Ports;
using SEGUROS.TesteTecnico.INFRASTRUCTURE.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Proposta Service API",
        Version = "v1",
        Description = "API para gerenciamento de propostas de seguro"
    });
});

builder.Services.AddSingleton<IPropostaRepository, PropostaRepository>();
builder.Services.AddScoped<CriarPropostaUseCase>();
builder.Services.AddScoped<ListarPropostasUseCase>();
builder.Services.AddScoped<AtualizarStatusPropostaUseCase>();
builder.Services.AddScoped<ObterPropostaPorIdUseCase>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Proposta API V1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();