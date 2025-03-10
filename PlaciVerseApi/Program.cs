using Microsoft.AspNetCore.Identity;
using PlaciVerseApi.Repositories;

namespace PlaciVerseApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthorization();
            builder.Services.AddHttpContextAccessor();

            var sqlConnectionString = builder.Configuration["sqlConnectionString"];

            var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(sqlConnectionString);

            builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 10;
            })
            .AddRoles<IdentityRole>()
            .AddDapperStores(options =>
            {
                options.ConnectionString = sqlConnectionString;
            });

            //builder.Services.AddTransient<IUserRepository, UserRepository>(o => new UserRepository(sqlConnectionString));
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IObjectRepository, ObjectRepository>(o => new ObjectRepository(sqlConnectionString));
            builder.Services.AddTransient<IEnvironmentRepository, EnvironmentRepository>(o => new EnvironmentRepository(sqlConnectionString));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.MapGet("/", () => $"The API is up . Connection string found: {(sqlConnectionStringFound ? "Yes" : "No")}");

            app.UseAuthorization();
            app.MapGroup("/account").MapIdentityApi<IdentityUser>();
            app.MapControllers().RequireAuthorization();

            app.Run();
        }
    }
}
