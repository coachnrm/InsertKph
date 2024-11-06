using System.Security.Cryptography;
using InsertKph.Models;
using Microsoft.EntityFrameworkCore;

namespace InsertKph.Data 
{
    public class MyWorldDbContext : DbContext
    {
        public MyWorldDbContext(DbContextOptions<MyWorldDbContext> options):base(options)
        {

        }

        public DbSet<IpdProgressNote> IpdProgressNote {get; set;}
        public DbSet<IpdProgressNoteItem> IpdProgressNoteItems {get; set;}

    }
}