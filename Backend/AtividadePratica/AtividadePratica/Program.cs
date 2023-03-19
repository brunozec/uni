using AtividadePratica.Data;

var cors = "_myCorsConfig"; // nome da pol�tica

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// politica CORS com m�todo de extens�o - RequireCors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: cors,
        policy =>
        {
            policy.WithOrigins("http://example.com",
                "http://contoso.com");
        });
});


builder.Services.AddDbContext<AtividadePraticaContext>();

//adicionar configura��o para inje��o de dependencias dos repositorios
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(cors);  // aplica a politica CORS padr�o a todos os pontos de extremidade do controlador.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();