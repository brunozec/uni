using System.Reflection;
using AtividadePratica.Data;
using AtividadePratica.Middleware;
using Microsoft.OpenApi.Models;

//nome da politica
var cors = "_atividadePraticaCorsConfig";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//adiciona suporte ao swagger
builder.Services.AddSwaggerGen(options =>
{
    // adiciona a configuração da documentação dos metodos e classes para exibição no swagger
    var apiXml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, apiXml));

    var domainXml = $"AtividadePratica.Domain.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, domainXml));

    //adiciona configuração para autentica basica (usuario + senha) pela pagina do swagger
    options.SwaggerDoc("v1"
        , new OpenApiInfo
        {
            Title = "BasicAuth"
            , Version = "v1"
        });
    options.AddSecurityDefinition("basic"
        , new OpenApiSecurityScheme
        {
            Name = "Authorization"
            , Type = SecuritySchemeType.Http
            , Scheme = "basic"
            , In = ParameterLocation.Header
            , Description = "Autorização básica utilizando usuário + senha"
        });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme
                    , Id = "basic"
                }
            }
            , new string[]
                { }
        }
    });
});

// politica CORS com metodo de extensso - RequireCors
builder.Services.AddCors(options => { options.AddPolicy(name: cors, policy => { policy.WithOrigins("http://example.com", "http://contoso.com"); }); });

//configuracao para injecao de dependencias dos repositorios
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();

//configura o middleware de autenticacao
builder.Services.AddTransient<AtividadePraticaAuthenticationMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //adicionar pagina do swagger
    app.UseSwaggerUI();
}

app.UseCors(cors); // aplica a politica CORS padr�o a todos os pontos de extremidade do controlador.

app.UseHttpsRedirection();

//adiciona o middleware de autenticação
app.UseMiddleware<AtividadePraticaAuthenticationMiddleware>();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();