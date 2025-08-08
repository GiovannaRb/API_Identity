using APIProdutos.Data;
using APIProdutos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer("Server=localhost,1433;Database=API_Indentity;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;"));

builder.Services.AddAuthentication(); // queem vc é
builder.Services.AddAuthorization(); // oq vc pode fazer

builder.Services
    .AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapSwagger();

app.UseHttpsRedirection();


app.MapGet("/", (ClaimsPrincipal user) => user.Identity!.Name).RequireAuthorization();

app.MapPost("/logout",
    async (SignInManager<User> singInManager, [FromBody] object empty) => {
        await singInManager.SignOutAsync();
        return Results.Ok();
});

app.MapIdentityApi<User>();

app.Run();
