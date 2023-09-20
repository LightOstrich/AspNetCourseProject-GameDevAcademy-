using System.Text.Json.Serialization;
using HomeWorkEleven.JSONConverter.Policies;
using HomeWorkEleven.JSONSettings.Converter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        //options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCasePolicy();
        //options.JsonSerializerOptions.Converters.Add(new ProductTypeJsonConverter());
        //options.JsonSerializerOptions.Converters.Add(new DoubleJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());//Почему порядок их действия влияет на преоброзования?Если эту строку перставить
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


app.Run();