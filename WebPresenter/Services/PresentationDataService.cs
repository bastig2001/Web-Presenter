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

        public IEnumerable<PresentationData> GetPresentations() {
            return db.Presentations;
        }

        public IEnumerable<PresentationData> GetPresentations(string ownerName) {
            return db.Presentations.Where(pres => pres.OwnerName == ownerName);
        }

        public bool CreatePresentation(PresentationFundamentals fundamentals) {
            return CreatePresentation(fundamentals.Name, fundamentals.OwnerName, fundamentals.Title);
        }

        public bool CreatePresentation(string name, string ownerName, string title = "New Presentation") {
            return AddPresentation(new PresentationData(name, ownerName, title));
        }

        public bool AddPresentation(PresentationData presentation) {
            if (presentation.Name == "" || presentation.OwnerName == "" || 
                presentation.Name == null || presentation.OwnerName == null) {
                return false;
            }
            
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

        public bool SavePresentation(PresentationData presentation) {
            if (presentation == null) {
                return false;
            }

            try {
                db.Presentations.Update(presentation);
                db.SaveChanges();
                return true;
            }
            catch (Exception e) {
                Console.Error.WriteLine($"Error while trying to save presentation {{{presentation}}}. " +
                                        $"\nException: {e}");
                return false;
            }
        }
    }
}