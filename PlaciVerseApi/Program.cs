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

            var sqlConnectionString = builder.Configuration["sqlConnectionString"];
            if (string.IsNullOrEmpty(sqlConnectionString))
            {
                throw new ArgumentNullException("sqlConnectionString is null");
            }

            builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 50;
            })
            .AddRoles<IdentityRole>()
            .AddDapperStores(options =>
            {
                options.ConnectionString = sqlConnectionString;
            });

            builder.Services.AddTransient<IUserRepository, UserRepository>(o => new UserRepository(sqlConnectionString));
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

            app.UseAuthorization();
            app.MapGroup("/account").MapIdentityApi<IdentityUser>();
            app.MapControllers().RequireAuthorization();

            app.Run();
        }
    }
}
