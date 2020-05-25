using System;
using WebPresenter.Data;
using WebPresenter.Models;

namespace WebPresenter.Services {
    public class DatabasePresentationsService : IPresentationsService {
        private readonly WebPresenterContext db;
        private readonly User anyone = new User("anyone");

        public DatabasePresentationsService(WebPresenterContext db) {
            this.db = db;
        }
        
        public Presentation GetPresentation(string name) {
            return new Presentation(db.Presentations.Find(name, anyone.Name));
        }

        public string CreatePresentation() {
            string id = $"{Guid.NewGuid()}";

            try {
                db.Presentations.AddAsync(new PresentationData(id, anyone));
                db.SaveChanges();
            }
            catch (Exception e) {
                Console.Error.WriteLine(e);
            }

            return id;
        }
    }
}