using LocalVibes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Agregar DatabaseService como servicio
builder.Services.AddSingleton<DatabaseService>();

// Agregar la disponibilidad de Session y ajustar las opciones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(90); // 90 dias = 3 meses aprox
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// Autoriza el uso de Session
app.UseSession(); 

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Ruta por defecto de la app
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Landing}/{id?}");

app.Run();
