using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Model.MODEL;
using View.Data;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<RecipeService>();
builder.Services.AddHttpClient<RecipeService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5227/");
});
builder.Services.AddMudServices();

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
