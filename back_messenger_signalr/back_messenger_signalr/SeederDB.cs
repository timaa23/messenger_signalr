using back_messenger_signalr.Constants;
using back_messenger_signalr.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace back_messenger_signalr
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();

                if (!roleManager.Roles.Any())
                {
                    var result = roleManager.CreateAsync(new RoleEntity
                    {
                        Name = Roles.Admin
                    }).Result;

                    result = roleManager.CreateAsync(new RoleEntity
                    {
                        Name = Roles.User
                    }).Result;
                }

                if (!userManager.Users.Any())
                {
                    string adminEmail = "admin@gmail.com";
                    var entity = new UserEntity
                    {
                        Email = adminEmail,
                        UserName = "admin",
                        Name = "admin",
                        Image = "https://www.gravatar.com/avatar/?d=mp"
                    };

                    var result = userManager.CreateAsync(entity, "123456").Result;

                    result = userManager.AddToRoleAsync(entity, Roles.Admin).Result;
                }
            }
        }
    }
}
