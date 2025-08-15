using ApiResponseExamplesDemo.Models;
using Authentication.API.Swagger;
using Authentication.Login.Util;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerExamplesFromAssemblyOf<SucessDetailsExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<ProblemDetailsBadRequestExample>();
builder.Services.AddTransient<FluentValidation.IValidator<MyPayload>, AccountPayloadValidator>();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.ExampleFilters();
});

var app = builder.Build();

app.UseMiddleware<Authentication.API.Middleware.SwaggerAuthMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Use(async (context, next) =>
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/swagger");
            return;
        }
        await next();
    });
}

app.MapControllers();

app.Run();
