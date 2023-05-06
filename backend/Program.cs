using backend;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient(_ => new Database(builder.Configuration.GetConnectionString(System.Environment.GetEnvironmentVariable("DATABASE_URL"))));
builder.Services.AddControllers();
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
//setting the connection string for development environment
if (app.Environment.IsDevelopment()){
app.Use(async (contex, next)=>
{
    System.Environment.SetEnvironmentVariable("DATABASE_URL", "server=127.0.0.1;user id=netuser;password=netpass;port=3306;database=university;");

    Console.WriteLine(System.Environment.GetEnvironmentVariable("DATABASE_URL"));
    await next();
}
);
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
