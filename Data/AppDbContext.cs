namespace testapi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure 1-many relationship: Author -> Books
            modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }
    }
}
