// Crea el builder de la aplicación web.
// Este objeto se usa para configurar servicios y middleware
var builder = WebApplication.CreateBuilder(args);

// Registra los servicios necesarios para usar Controllers
// Permite que ASP.NET pueda manejar endpoints de API
builder.Services.AddControllers();
// Construye la aplicación con la configuración anterior
var app = builder.Build();
// Habilita el sistema de rutas de ASP.NET
app.UseRouting();
// Define la ruta por defecto para los controladores
// En tu caso no es muy importante porque usas API endpoints
app.MapControllers();
//inica la aplicación web
app.Run();