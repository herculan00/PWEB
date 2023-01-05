using Microsoft.AspNetCore.Identity;
using PWEB.Models;

namespace PWEB.Data
{
  
    public enum Roles
    {
        Admin,
        Funcionario,
        Cliente,
        Gestor
    }
    public static class Inicializacao
    {
        public static async Task CriaDadosIniciais(UserManager<Utilizador>
        userManager, RoleManager<IdentityRole> roleManager)
        {
            //Adicionar default Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Funcionario.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Cliente.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Gestor.ToString()));

            //Adicionar Default User - Admin
            var defaultUser = new Utilizador
            {
                UserName = "admin@localhost.com",
                Email = "admin@localhost.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Nome = "Admin",
                Apelido = "Admin",
                NIF = 0,
                Morada = "",
                DataNascimento = new DateTime(),
                NumeroCartaoMultibanco = 0,
                ValidadeCartaoMultibanco = new DateTime(),
                CvdCartaoMultibanco = 0,
                Disponivel = true

            };
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "Admin10.");
                await userManager.AddToRoleAsync(defaultUser,
                Roles.Admin.ToString());
            }
        }
    }
    
}
