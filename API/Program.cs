using API.Interfaces;
using API.Repositories;
using API.Services;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ITagsService, TagsService>();
builder.Services.AddScoped<IHasCategoriesService, HasCategoriesService>();
builder.Services.AddScoped<IOpinionsService, OpinionsService>();
builder.Services.AddScoped<IRecipesService,RecipesService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IIngridientsService, IngridientsService>();
builder.Services.AddScoped<IHasIngridientService, HasIngridientService>();

builder.Services.AddScoped<IEncryptor, Encryptor>();
builder.Services.AddScoped<IPasswordGetter, PasswordGetter>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtConfigure.ConfigureJwtBearerOptions);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerConfiguration.ConfigureSwaggerOptions);
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
