using BusinessObjects;
using DAOs;
using Microsoft.EntityFrameworkCore;
using Repositories.Implement;
using Repositories;
using Services.Implement;
using Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using DiamondStoreAPI;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.JsonWebTokens;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

var services = builder.Services;

// Add services to the container.
services.AddDbContext<DiamondStoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")).EnableSensitiveDataLogging();
});

services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

// Configure JWT authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Jwt:Day_la_key_JWT"));
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        // Custom validation logic
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
        {
            var jwtToken = securityToken as JwtSecurityToken;
            if (jwtToken == null)
            {
                var jsonWebToken = securityToken as JsonWebToken;
                return jsonWebToken != null && expires != null && expires > DateTime.UtcNow && !TokenBlacklist.IsBlacklisted(jsonWebToken.EncodedToken);
            }
            return expires != null && expires > DateTime.UtcNow && !TokenBlacklist.IsBlacklisted(jwtToken.RawData);
        }
    };
});


// Add dependencies
services.AddScoped<AccountDAO>();
services.AddScoped<IAccountRepository, AccountRepository>();
services.AddScoped<IAccountService, AccountService>();

services.AddScoped<ProductDAO>();
services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<IProductService, ProductService>();

services.AddScoped<ProductMaterialDAO>();
services.AddScoped<IProductMaterialRepository, ProductMaterialRepository>();
services.AddScoped<IProductMaterialService, ProductMaterialService>();

services.AddScoped<MaterialCategoryDAO>();
services.AddScoped<IMaterialCategoryRepository, MaterialCategoryRepository>();
services.AddScoped<IMaterialCategoryService, MaterialCategoryService>();

services.AddScoped<ProductCategoryDAO>();
services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
services.AddScoped<IProductCategoryService, ProductCategoryService>();

services.AddScoped<OrderDAO>();
services.AddScoped<IOrderRepository, OrderRepository>();
services.AddScoped<IOrderService, OrderService>();

services.AddScoped<OrderDetailDAO>();
services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
services.AddScoped<IOrderDetailService, OrderDetailService>();

services.AddScoped<CustomerDAO>();
services.AddScoped<ICustomerRepository, CustomerRepository>();
services.AddScoped<ICustomerService, CustomerService>();

services.AddScoped<PaymentDAO>();
services.AddScoped<IPaymentRepository, PaymentRepository>();
services.AddScoped<IPaymentService, PaymentService>();

services.AddScoped<SaleStaffDAO>();
services.AddScoped<ISaleStaffRepository, SaleStaffRepository>();
services.AddScoped<ISaleStaffService, SaleStaffService>();

services.AddScoped<ShipperDAO>();
services.AddScoped<IShipperRepository, ShipperRepository>();
services.AddScoped<IShipperService, ShipperService>();

services.AddScoped<GemDAO>();
services.AddScoped<IGemRepository, GemRepository>();
services.AddScoped<IGemService, GemService>();

services.AddScoped<MaterialPriceListDAO>();
services.AddScoped<IMaterialPriceListRepository, MaterialPriceListRepository>();
services.AddScoped<IMaterialPriceListService, MaterialPriceListService>();

services.AddScoped<GemPriceListDAO>();
services.AddScoped<IGemPriceListRepository, GemPriceListRepository>();
services.AddScoped<IGemPriceListService, GemPriceListService>();

services.AddScoped<WarrantyDAO>();
services.AddScoped<IWarrantyRepository, WarrantyRepository>();
services.AddScoped<IWarrantyService, WarrantyService>();

services.AddScoped<MembershipDAO>();
services.AddScoped<IMembershipRepository, MembershipRepository>();
services.AddScoped<IMembershipService, MembershipService>();

services.AddScoped<RefundDAO>();
services.AddScoped<IRefundRepository, RefundRepository>();
services.AddScoped<IRefundService, RefundService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// Configure Swagger
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DiamondStoreAPI", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "DiamondStoreAPI", Version = "v2" }); //swagger v2

    // Configure JWT authentication for Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });

    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.DocInclusionPredicate((docName, apiDesc) => apiDesc.GroupName == docName);
});

// Add CORS policy
services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "http://localhost:5173", "https://diamond-store-eta.vercel.app", "https://diamond-manager.vercel.app")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowReact");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DiamondStoreAPI v1");
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "DiamondStoreAPI v2"); //config v2
});

app.UseDefaultFiles(); // Để sử dụng index.html làm tệp mặc định
app.UseStaticFiles(); // Để phục vụ các tệp tĩnh trong wwwroot

app.MapControllers();

app.Run();