using System;
using WebPresenter.Data;
using WebPresenter.Models;

namespace WebPresenter.Services {
    public class DatabasePresentationsService : IPresentationsService {
        private WebPresenterContext db;

        public DatabasePresentationsService(WebPresenterContext db) {
            this.db = db;
        }
        
        public Presentation GetPresentation(string id) {
            return new Presentation(db.Presentations.Find(id));
        }

        public string CreatePresentation() {
            string id = $"{Guid.NewGuid()}";

            try {
                db.Presentations.AddAsync(new PresentationData(id));
                db.SaveChanges();
            }
            catch (Exception e) {
                Console.Error.WriteLine(e);
            }

            return id;
        }
    }
}