using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using View.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<IRecipeService,RecipeService>();
builder.Services.AddSingleton<WeatherService>();
builder.Services.AddHttpClient<WeatherService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5227/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
