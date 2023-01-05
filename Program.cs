using System.Reflection;
using kuro_desserts.Data;
using kuro_desserts.Middlewares;
using kuro_desserts.Helpers;
using kuro_desserts.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddLocalization();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<ProfileService>();
// API services
builder.Services.AddDbContext<Context>(optionsBuilder => optionsBuilder.UseSqlite("Data Source=db.db"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Kuro Desserts API",
        Description = "API for the kuro desserts' blazor server, this API was made for learning purposes only"
    });
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
    options.AddSecurityDefinition("KuroSecurityDefinition", new OpenApiSecurityScheme
    {
        Description = "Enter your JWT token",
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    options.OperationFilter<AuthenticationRequirementsOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseRequestLocalization("en-US");

// Public swagger ui
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<JwtMiddleware>();

// API Endpoints
app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Create and seed Database
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Context>();
    if (db.Database.EnsureCreated())
    {
        SeedData.Initialize(db);
    }
}

app.Run();
