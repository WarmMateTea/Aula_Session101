using DevOne.Security.Cryptography.BCrypt;
using Microsoft.EntityFrameworkCore;

namespace Session101.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            Context context = app.ApplicationServices.GetRequiredService<Context>();

            context.Database.Migrate();

            if (!context.Usuarios.Any())
            {
                context.Usuarios.Add(
                    new Usuario { Nome = "Teste A", Email = "aaa@aaa", Senha = BCryptHelper.HashPassword("aaa", BCryptHelper.GenerateSalt())});

                context.SaveChanges();
            }
        }
    }
}
