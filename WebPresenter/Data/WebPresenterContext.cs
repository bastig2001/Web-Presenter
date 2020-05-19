using Microsoft.EntityFrameworkCore;
using WebPresenter.Models;

namespace WebPresenter.Data {
    public class WebPresenterContext : DbContext {
        public WebPresenterContext(DbContextOptions options): base (options) {}

        public DbSet<PresentationData> Presentations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<PresentationData>().HasKey(presData => presData.Id);
        }
    }
}