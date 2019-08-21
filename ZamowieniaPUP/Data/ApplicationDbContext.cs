using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZamowieniaPUP.Models;

namespace ZamowieniaPUP.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ZamowieniaPUP.Models.RokZamowienie> RokZamowienie { get; set; }
        public DbSet<ZamowieniaPUP.Models.Zamowienie> Zamowienie { get; set; }
    }
}
