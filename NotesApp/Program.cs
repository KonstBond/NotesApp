using Microsoft.EntityFrameworkCore;
using NotesApp.Models.AutoMapper;
using NotesApp.Models.DB;
using NotesApp.Models.Manager;
using NotesApp.Models.Middleware;
using NotesApp.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<NoteDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddTransient<INoteManager, NoteManager>();
builder.Services.AddAutoMapper(typeof(NoteProfile));

var app = builder.Build();


app.UseMiddleware<ExceptionHandleMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Note}/{action=Index}");

app.Run();
