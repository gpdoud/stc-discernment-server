

using Microsoft.EntityFrameworkCore;

using stc_discernment_server.Middleware;
using stc_discernment_server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(x => {
    var connStrKey = "AppDbContext";
#if DEBUG
    connStrKey += Environment.OSVersion.Platform == PlatformID.Win32NT ? "Win" : "Mac";
#elif RIPPER
    connStrKey += "Ripper";
#endif
    x.UseSqlServer(builder.Configuration.GetConnectionString(connStrKey));
});

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.

// app.UseMiddleware<ApiKeyMiddleware>();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

using(var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope()) {
    scope.ServiceProvider.GetService<AppDbContext>()!.Database.Migrate();
}

app.Run();

