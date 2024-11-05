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
using Application.Helpers;
using Infraestructure.Services.Helpers;
using Infraestructure.Services.Interfaces;
using Infraestructure.Services;
using Amazon.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MercadoPago.Config;

var builder = WebApplication.CreateBuilder(args);

ConfigurarInjecaoDeDependencia(builder);

ConfigurarServices(builder);

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

    MercadoPagoConfig.AccessToken = builder.Configuration["MercadoPago:AccessToken"];

    builder.Services.Configure<S3StorageOptions>(builder.Configuration.GetSection("S3Storage:Bucket-Name"));

    builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
    builder.Services.AddAWSService<IAmazonS3>();

    builder.Services.AddIdentity<Usuario, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationContext>()
        .AddDefaultTokenProviders();

    var config = new MapperConfiguration(configs => {
        configs.AddProfile<HospitalProfile>();
        configs.AddProfile<ServicoProfile>();
        configs.AddProfile<UsuarioProfile>();
        configs.AddProfile<PacienteProfile>();
        configs.AddProfile<MedicoProfile>();
        configs.AddProfile<AdminProfile>();
        configs.AddProfile<ProntuarioProfile>();
        configs.AddProfile<ExameProfile>();
        configs.AddProfile<ConsultaProfile>();
        configs.AddProfile<HospitalServicoProfile>();
    });

    builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

    IMapper mapper = config.CreateMapper();

    builder.Services
    .AddSingleton(builder.Configuration)
    .AddSingleton(builder.Environment)
    .AddSingleton(mapper)
    .AddHttpClient()
    .AddScoped<IEmailService, EmailService>()
    .AddScoped<TokenJWTService>()
    .AddScoped<IS3StorageService, S3StorageService>()
    .AddScoped<IUsuarioService, UsuarioService>()
    .AddScoped<IHospitalService, HospitalService>()
    .AddScoped<IHospitalRepository, HospitalRepository>()
    .AddScoped<IServicoService, ServicoService>()
    .AddScoped<IServicoRepository, ServicoRepository>()
    .AddScoped<IHospitalServicoService, HospitalServicoService>()
    .AddScoped<IHospitalServicoRepository, HospitalServicoRepository>()
    .AddScoped<IPacienteRepository, PacienteRepository>()
    .AddScoped<IPacienteService, PacienteService>()
    .AddScoped<IMedicoRepository, MedicoRepository>()
    .AddScoped<IMedicoService, MedicoService>()
    .AddScoped<IRegistroRepository, RegistroRepository>()
    .AddScoped<IProntuarioRepository, ProntuarioRepository>()
    .AddScoped<IProntuarioService, ProntuarioService>()
    .AddScoped<IAdminRepository, AdminRepository>()
    .AddScoped<IAdminService, AdminService>()
    .AddScoped<IExameRepository, ExameRepository>()
    .AddScoped<IExameService, ExameService>()
    .AddScoped<IConsultaRepository, ConsultaRepository>()
    .AddScoped<IConsultaService, ConsultaService>()
    .AddScoped<IDocumentoRepository, DocumentoRepository>()
    .AddScoped<IPixService, PixService>()
    .AddScoped<ICreditCardService, CreditCardService>()
    .AddScoped<IDocumentoService, DocumentoService>()
    .AddScoped<IGoogleMeetService, GoogleMeetService>();
}

// Configura o servi�os da API.
static void ConfigurarServices(WebApplicationBuilder builder)
{
    builder.Services
    .AddCors()
    .AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    }).AddNewtonsoftJson();

    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JTW Authorization header using the Beaerer scheme (Example: 'Bearer 12345abcdef')",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
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

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminOnly",
             policy => policy.RequireRole("Administrador"));
        options.AddPolicy("AdminOrMedico",
             policy => policy.RequireRole("Administrador", "Medico"));
    });

    builder.Services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
   {
       x.RequireHttpsMetadata = false;
       x.SaveToken = true;
       x.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["VItalAPI:KeySecret"])),
           ValidateIssuer = false,
           ValidateAudience = false
       };
       x.Events = new JwtBearerEvents
       {
           OnChallenge = context =>
           {
               // Suprimir o comportamento padrão do OnChallenge
               context.HandleResponse();

               // Definir o objeto de erro personalizado
               context.Response.StatusCode = 401;
               context.Response.ContentType = "application/json";
               var errorResponse = new 
               {
                   StatusCode = 401,
                   Title = "Unauthorized",
                   Description = "Você precisa estar autenticado para acessar este recurso.",
                   Date = DateTime.Now,
               };

               // Escrever o objeto de erro na resposta
               return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
           }
       };
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

    var devClient = "http://localhost:4200";
    app.UseCors(x => x
        .AllowAnyOrigin() // Permite todas as origens
        .AllowAnyMethod() // Permite todos os m�todos
        .AllowAnyHeader()) // Permite todos os cabe�alhos
        .UseAuthentication();

    app.UseAuthorization();

    app.UseEndpoints(endpoints => endpoints.MapControllers());

    app.MapControllers();
}

