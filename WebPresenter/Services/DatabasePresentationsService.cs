using System;
using WebPresenter.Data;

namespace WebPresenter.Services {
    public class DatabasePresentationsService : IPresentationsService {
        private PresentationContext db;

        public DatabasePresentationsService(PresentationContext db) {
            this.db = db;
        }
        
        public Presentation GetPresentation(string id) {
            return db.Presentations.Find(id);
        }

        public string CreatePresentation() {
            string id = $"{Guid.NewGuid()}";

            try {
                db.Presentations.AddAsync(new Presentation("1"));
                db.SaveChanges();
            }
            catch (Exception e) {
                Console.Error.WriteLine(e);
            }

            return id;
        }
    }
}