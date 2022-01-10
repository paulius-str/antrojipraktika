using API.Data;
using API.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Interfaces;
using API.Services;
using API.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ServiceContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var identityBuilder = builder.Services.AddIdentityCore<AppUser>();
identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);
identityBuilder.AddEntityFrameworkStores<ServiceContext>();
identityBuilder.AddSignInManager<SignInManager<AppUser>>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidateIssuer = true,
            ValidateAudience = false
        };
    });

// builder.Services.AddDbContext<AppIdentityDbContext>(x => 
// {
//     x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
// });

builder.Services.AddCors(opt => 
{
    opt.AddPolicy(name: "CorsPolicy", policy => 
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:44433", "http://localhost:44433");
    });
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var userManager = builder.Services.BuildServiceProvider().GetRequiredService<UserManager<AppUser>>();
        var identityContext = services.GetRequiredService<ServiceContext>();
        await identityContext.Database.MigrateAsync();
        await AppIdentityDbContextSeed.SeedUsersAsync(userManager);

        var context = services.GetRequiredService<ServiceContext>();
        await context.Database.MigrateAsync();
        await ServiceContextSeed.SeedSubjects(context);
        await ServiceContextSeed.SeedAssignments(context);
    }
    catch(Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occured during migration");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseStaticFiles();
app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();
