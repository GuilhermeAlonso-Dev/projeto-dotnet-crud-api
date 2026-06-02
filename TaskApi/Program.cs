// Program.cs - Ponto de entrada da API CRUD com ASP.NET Core
// Projeto: CRUD API .NET - Formacao .NET Developer
// Autor: Guilherme Alonso

using TaskApi.Models;
using TaskApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Registra servicos no container de DI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
                               {
                                     c.SwaggerDoc("v1", new()
                                                  {
                                                            Title = "Task CRUD API",
                                                            Version = "v1",
                                                            Description = "API de gerenciamento de tarefas com ASP.NET Core e C#.",
                                                            Contact = new() { Name = "Guilherme Alonso" }
                                                  });
                               });

// Repositorio em memoria (simulando um banco de dados)
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();

// CORS
builder.Services.AddCors(options =>
                         {
                               options.AddDefaultPolicy(policy =>
                                                        {
                                                                  policy.AllowAnyOrigin()
                                                                                  .AllowAnyHeader()
                                                                                  .AllowAnyMethod();
                                                        });
                         });

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task API v1"));
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();
