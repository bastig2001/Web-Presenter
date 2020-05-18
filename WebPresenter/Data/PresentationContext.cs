using Microsoft.EntityFrameworkCore;

namespace WebPresenter.Data {
    public class PresentationContext : DbContext {
        public PresentationContext(DbContextOptions options): base (options) {}
        
        public DbSet<Presentation> Presentations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Presentation>().HasKey(pres => pres.Id);
        }
    }
}