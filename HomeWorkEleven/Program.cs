using System.Text;
using System.Text.Json.Serialization;
using HomeWorkEleven.Data.EF;
using HomeWorkEleven.JSONConverter.Policies;
using HomeWorkEleven.JSONSettings.Converter;
using HomeWorkEleven.ModelMappers;
using HomeWorkEleven.Models.Services;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        //options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCasePolicy();
        //options.JsonSerializerOptions.Converters.Add(new ProductTypeJsonConverter());
        //options.JsonSerializerOptions.Converters.Add(new DoubleJsonConverter());
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()); //Почему порядок их действия влияет на преоброзования?Если эту строку перставить
        //вверъ то он не преобразует ProductType в русский язык
    });
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policyBuilder =>
        {
            policyBuilder.WithOrigins
                ("http://localhost:5249",
                    "http://www.contoso.com")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

builder.Services.AddSingleton<IProductModelMapper, ProductModelMapper>();
builder.Services.AddSingleton<ICustomerModelMapper, CustomerModelMapper>();
builder.Services.AddSingleton<OrderModelMapper, OrderModelMapper>();
builder.Services.AddSingleton<SingletonService>();
builder.Services.AddScoped<ScopedService>();
builder.Services.AddTransient<TrancientService>();

/*
builder.Services.AddDbContext<EfDataContext>(options =>
    options.UseMySQL("Datasource=localhost;Database=shop;User=root;Password=americanes_one;"));
    */


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Use(async (context, next) =>
{
    Console.WriteLine("Request");
    var body = context.Request.Body;
    using (var stream = new StreamReader(body))
    {
        var text = await stream.ReadToEndAsync();
        Console.WriteLine(Encoding.UTF8.GetString(Encoding.Default.GetBytes(text)));
    }

    await next.Invoke();
    Console.WriteLine("Response");
    var request = context.Request.Headers;
    foreach (var pair in request)
    {
        Console.WriteLine(pair.ToString());
    }
    //context.Response.StatusCode = 404;
});
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/Table")
    {
        context.Request.QueryString = context.Request.QueryString.Add("arg", "101");
    }

    await next.Invoke();
});
var singleton = app.Services.GetService<SingletonService>();
singleton?.WriteWords();
Console.WriteLine();

using (var scope = app.Services.CreateScope())
{
    Console.WriteLine("Scope 1 ");
    var word = scope.ServiceProvider.GetService<ScopedService>();
    singleton?.WriteWords();
    word?.WriteWord();
    Console.WriteLine();
}

using (var scope = app.Services.CreateScope())
{
    Console.WriteLine("Scope 2 ");
    var word2 = scope.ServiceProvider.GetService<ScopedService>();
    singleton?.WriteWords();
    word2?.WriteWord();
    Console.WriteLine();
}

using (var scope = app.Services.CreateScope())
{
    Console.WriteLine("Transient");
    var word2 = scope.ServiceProvider.GetService<ScopedService>();
    var transient = scope.ServiceProvider.GetService<TrancientService>();

    singleton?.WriteWords();
    word2?.WriteWord();
    transient?.WriteWord();
    Console.WriteLine();

    var transient2 = scope.ServiceProvider.GetService<TrancientService>();

    singleton?.WriteWords();
    word2?.WriteWord();
    transient2?.WriteWord();
    Console.WriteLine();
}

using (var scope = app.Services.CreateScope())
{
    Console.WriteLine("Transient 2 ");
    var word2 = scope.ServiceProvider.GetService<ScopedService>();
    var transient = scope.ServiceProvider.GetService<TrancientService>();

    singleton?.WriteWords();
    word2?.WriteWord();
    transient?.WriteWord();
    Console.WriteLine();

    var transient2 = scope.ServiceProvider.GetService<TrancientService>();

    singleton?.WriteWords();
    word2?.WriteWord();
    transient2?.WriteWord();
    Console.WriteLine();
}

app.Run();