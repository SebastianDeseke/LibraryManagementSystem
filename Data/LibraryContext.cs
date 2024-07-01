using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data;

public class LibraryContext : DbContext {
    private readonly IConfiguration _config;
    private readonly ILogger<LibraryContext> _logger;
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) {}
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
}