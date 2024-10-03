using WebAPI.Data;
using WebAPI.Services;
using WebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register the connection factory
builder.Services.AddSingleton<DbConnectionFactory>();

// Register the repositories
builder.Services.AddScoped<ICustomer, CustomerRepository>();
builder.Services.AddScoped<IOrder, OrderRepository>();
builder.Services.AddScoped<IEmployee, EmployeeRepository>();
builder.Services.AddScoped<IProduct, ProductRepository>();
builder.Services.AddScoped<IShipper, ShipperRepository>();
builder.Services.AddScoped<INewOrder, NewOrderRepository>();
builder.Services.AddScoped<INewOrderDetail, NewOrderDetailRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder => builder.WithOrigins(allowedOrigins) // Permite orígenes desde la configuración
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
});

// Add configuration to access the connection string
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var app = builder.Build();

// Enable CORS
app.UseCors("AllowSpecificOrigins"); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS
app.UseCors("AllowAngularApp"); // Aplica la política de CORS

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
