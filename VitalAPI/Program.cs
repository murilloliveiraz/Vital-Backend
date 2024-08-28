using Domain;
using Infraestructure.Contexts;
using MongoDB.Driver;
using Infraestructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using AutoMapper;
using Application.Mapper;
using Application.Services.Classes;
using Application.Services.Interfaces;
using Infraestructure.Repositories.Interfaces;
using Infraestructure.Repositories.Classes;

var builder = WebApplication.CreateBuilder(args);

ConfigurarServices(builder);

ConfigurarInjecaoDeDependencia(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

ConfigurarAplicacao(app);

app.Run();


// Metodo que configrua as inje��es de dependencia do projeto.
static void ConfigurarInjecaoDeDependencia(WebApplicationBuilder builder)
{
    string? connectionString = builder.Configuration.GetConnectionString("Localhost");
    builder.Services.AddDbContext<ApplicationContext>(options =>
        options.UseNpgsql(connectionString), ServiceLifetime.Transient, ServiceLifetime.Transient
    );

    builder.Services.Configure<MongoDatabaseConfig>(builder.Configuration.GetSection("MongoDatabaseConfig"));
    builder.Services.AddSingleton<IMongoClient>(sp =>
    {
        var mongoConfig = sp.GetRequiredService<IOptions<MongoDatabaseConfig>>().Value;
        return new MongoClient(mongoConfig.ConnectionString);
    });

    builder.Services.AddIdentity<Usuario, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationContext>()
        .AddDefaultTokenProviders();

    var config = new MapperConfiguration(configs => {
        configs.AddProfile<HospitalProfile>();
    });

    IMapper mapper = config.CreateMapper();

    builder.Services
    .AddSingleton(builder.Configuration)
    .AddSingleton(builder.Environment)
    .AddSingleton(mapper)
    .AddScoped<IHospitalService, HospitalService>()
    .AddScoped<IHospitalRepository, HospitalRepository>();
}

// Configura o servi�os da API.
static void ConfigurarServices(WebApplicationBuilder builder)
{
    builder.Services
    .AddCors()
    .AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JTW Authorization header using the Beaerer scheme (Example: 'Bearer 12345abcdef')",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hospital.Api", Version = "v1" });
    });
}

// Configura os servi�os na aplica��o.
static void ConfigurarAplicacao(WebApplication app)
{
    // Configura o contexto do postgreSql para usar timestamp sem time zone.
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    app.UseDeveloperExceptionPage()
        .UseRouting();

    app.UseSwagger()
        .UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hospital.Api v1");
            c.RoutePrefix = string.Empty;
        });

    app.UseCors(x => x
        .AllowAnyOrigin() // Permite todas as origens
        .AllowAnyMethod() // Permite todos os m�todos
        .AllowAnyHeader()) // Permite todos os cabe�alhos
        .UseAuthentication();

    app.UseAuthorization();

    app.UseEndpoints(endpoints => endpoints.MapControllers());

    app.MapControllers();
}

