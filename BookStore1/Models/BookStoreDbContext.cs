//  DB خاص بتحويل الكلاسات الى 
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore1.Models
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext()
        {
        }

        // to generate the data base 
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
            // here to added tables 
        }

        public DbSet<Author> Authors { get; set; }   // table 1
        public DbSet<Book> Books { get; set; }    // table 2 



    }
}
