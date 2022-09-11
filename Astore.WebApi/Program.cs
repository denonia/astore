using Astore.Application;
using Astore.Application.Services;
using Astore.WebApi.Extensions;
using Astore.WebApi.Validation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => { options.Filters.Add<ValidationFilter>(); })
    .AddFluentValidation(settings =>
    {
        settings.RegisterValidatorsFromAssemblyContaining<Program>();
        settings.DisableDataAnnotationsValidation = true;
    });
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IFavoritesService, FavoritesService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

await app.MigrateDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerWithUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();