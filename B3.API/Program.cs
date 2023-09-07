using AutoMapper;
using B3.Aplicacao;
using B3.Aplicacao.Interfaces;
using B3.Dados.Repositorios;
using B3.Dominio.Interfaces.Repositorios;
using B3.Dominio.Interfaces.Servicos;
using B3.Dominio.Servicos;
using B3.Infraestrutura.CrossCuting.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Injeção de dependência
builder.Services.AddSingleton<IMapper>(AutoMapperConfig.Mapper());
builder.Services.AddScoped<IRepositorioTaxasBancarias, RepositorioTaxasBancarias>();
builder.Services.AddScoped<IRepositorioFaixaImpostoRenda, RepositorioFaixaImpostoRenda>();
builder.Services.AddScoped<IServicoCdb, ServicoCdb>();
builder.Services.AddScoped<ICdbApi, CdbApi>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
