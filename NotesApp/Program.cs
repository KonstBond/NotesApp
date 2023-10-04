using Microsoft.EntityFrameworkCore;
using NotesApp.Models.DB;
using NotesApp.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<NoteDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});
builder.Services.AddTransient<INoteRepository, NoteRepository>();

var app = builder.Build();



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Note}/{action=Index}");

app.Run();
