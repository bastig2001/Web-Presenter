using Microsoft.EntityFrameworkCore;
using WebPresenter.Models;

namespace WebPresenter.Data {
    public class WebPresenterContext : DbContext {
        public DbSet<PresentationData> Presentations { get; set; }
        public DbSet<User> Users { get; set; }
        
        public WebPresenterContext(DbContextOptions options): base (options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<PresentationData>()
                .HasKey(pres => new {pres.Name, pres.OwnerName});
        }
    }
}