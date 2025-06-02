using app_horarios_BackEnd.Data;
using app_horarios_BackEnd.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy.AllowAnyOrigin() // ⛔ Liberado só para desenvolvimento
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddScoped<ExcelImportService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();
builder.Services.AddSession();

builder.Services.AddDbContext<HorarioDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.

// CORS deve ser configurado APENAS uma vez, dependendo do ambiente
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors");
}
else
{
    // Em produção: usar CORS restrito
    app.UseCors(policy =>
        policy.WithOrigins("https://horarios.com") // ⚠️ Ajusta o domínio real
              .AllowAnyHeader()
              .AllowAnyMethod());
}

// app.UseHttpsRedirection(); // Habilita se tiver certificado HTTPS
app.UseStaticFiles();
app.UseRouting();

// ⚠️ Não repete app.UseCors() aqui
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapRazorPages();

// Configuração de rotas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
