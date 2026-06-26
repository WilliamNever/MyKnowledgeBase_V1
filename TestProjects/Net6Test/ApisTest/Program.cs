using ApisTest.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAntiforgery(options =>
{
    /* 
     * this is working for [ValidateAntiForgeryToken] 
     * is called by ajax.
     * when it is called by ajax, adding RequestVerificationToken in request header.
     * headers: { "RequestVerificationToken": json["__RequestVerificationToken"] },
     * */
    options.HeaderName = "RequestVerificationToken";
})
    .AddControllers()
    .AddNewtonsoftJson(x =>
    {
        x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    })
    ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<HandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
