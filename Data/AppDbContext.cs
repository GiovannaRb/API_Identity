using APIProdutos.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIProdutos.Data
{
    public class AppDbContext(DbContextOptions options) 
        : IdentityDbContext<User>(options);
}
