using HotelLibrary.Databases;
using HotelLibrary.Interfaces;
using HotelLibrary.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Adding Database support by dependency injection
switch (builder.Configuration.GetValue<string>("DBToUse"))
{
    case "SQLServer":
        builder.Services.AddTransient<IDatabaseData, SqlData>();
        break;
    case "SQLite":
        builder.Services.AddTransient<IDatabaseData, SqliteData>();
        break;
    default:
        builder.Services.AddTransient<IDatabaseData, SqlData>();
        break;
}
builder.Services.AddTransient<ISqlDataAccess, SqlServerDataAccess>();
builder.Services.AddTransient<ISqliteDataAccess, SqliteDataAccess>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
