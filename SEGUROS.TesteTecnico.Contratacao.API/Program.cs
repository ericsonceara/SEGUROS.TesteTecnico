using SEGUROS.TesteTecnico.ContratacaoService.Infrastructure.ExternalServices;
using SEGUROS.TesteTecnico.ContratacaoService.Infrastructure.Repositories;
using SEGUROS.TesteTecnico.ContratacaoService.UseCases;
using SEGUROS.TesteTecnico.DOMAIN.Ports;

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

builder.Services.AddSingleton<IContratacaoRepository, ContratacaoRepository>();

builder.Services.AddHttpClient<IPropostaServiceClient, PropostaServiceHttpClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["PropostaServiceUrl"] ?? "https://localhost:7047");
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddScoped<ContratarPropostaUseCase>();
builder.Services.AddScoped<ListarContratacoesUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contratação API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
