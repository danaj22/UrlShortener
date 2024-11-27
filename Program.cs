using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.DTOs;
using UrlShortener.DTOs.Validators;
using UrlShortener.Entities;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUrlShortenerService, UrlShortenerService>();

builder.Services.AddDbContext<UrlDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("UrlShortenerConnectionString"))
    );

builder.Services.AddValidatorsFromAssemblyContaining<UrlRequestValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/shorter", async (UrlRequest request, 
                                UrlDbContext db, 
                                IUrlShortenerService urlShorteningService, 
                                IValidator<UrlRequest> validator) =>
{
    var validation = await validator.ValidateAsync(request);
    if (!validation.IsValid)
    {
        return Results.ValidationProblem(validation.ToDictionary());
    }  

    var code = urlShorteningService.GenerateCode();

    var result = new UrlShorter
    {
        Code = code,
        OriginUrl = request.Url,
        ShortUrl = $"http://daniel.pl/{code}"
    };

    await db.UrlShorteners.AddAsync(result);
    await db.SaveChangesAsync();

    return Results.Ok(result.ShortUrl);
});

app.MapGet("/{code}", async (string code, UrlDbContext db) =>
{
    var redirectUrl = await db.UrlShorteners.SingleOrDefaultAsync(x => x.Code == code);

    if (redirectUrl == null)
        return Results.NotFound();

    return Results.Redirect(redirectUrl.OriginUrl);
});

app.Run();

