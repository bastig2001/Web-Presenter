using System;
using System.Collections.Generic;
using System.Linq;
using WebPresenter.Data;
using WebPresenter.Models;

namespace WebPresenter.Services {
    public class PresentationDataService  {
        private readonly WebPresenterContext db;

        public PresentationDataService(WebPresenterContext db) {
            this.db = db;
        }

        public PresentationData GetPresentation(string name, string ownerName) {
            return db.Presentations.Find(name, ownerName);
        }

        public PresentationData GetPresentation(string name, User owner) {
            return GetPresentation(name, owner.Name);
        }

        public IEnumerable<PresentationData> GetPresentations() {
            return db.Presentations;
        }

        public IEnumerable<PresentationData> GetPresentations(string ownerName) {
            return db.Presentations.Where(pres => pres.OwnerName == ownerName);
        }

        public IEnumerable<PresentationData> GetPresentations(User owner) {
            return GetPresentations(owner.Name);
        }

        public bool CreatePresentation(string name, string ownerName, string title = "New Presentation") {
            return AddPresentation(new PresentationData(name, ownerName, title));
        }

        public bool CreatePresentation(string name, User owner, string title = "New Presentation") {
            return CreatePresentation(name, owner.Name, title);
        }

        public bool AddPresentation(PresentationData presentation) {
            if (db.Presentations.Find(presentation.Name, presentation.OwnerName) != null) {
                return false;
            }

            try {
                db.Presentations.Add(presentation);
                db.SaveChanges();
                return true;
            }
            catch (Exception e) {
                Console.Error.WriteLine($"Error while trying to create presentation {{{presentation}}}. " +
                                        $"\nException: {e}");
                return false;
            }
        }

        public bool Save() {
            try {
                db.SaveChanges();
                return false;
            }
            catch (Exception e) {
                Console.Error.WriteLine($"Error while trying to save changes. " +
                                        $"\nException: {e}");
                return false;
            }
        }

        public bool RemovePresentation(string name, User owner) {
            return RemovePresentation(name, owner.Name);
        }

        public bool RemovePresentation(string name, string ownerName) {
            return RemovePresentation(db.Presentations.Find(name, ownerName));
        }
        
        public bool RemovePresentation(PresentationData presentation) {
            if (presentation == null) {
                return false;
            }
            
            try {
                db.Presentations.Remove(presentation);
                db.SaveChanges();
                return true;
            }
            catch (Exception e) {
                Console.Error.WriteLine($"Error while trying to remove presentation {{{presentation}}}. " +
                                        $"\nException: {e}");
                return false;
            }
        }
    }
}