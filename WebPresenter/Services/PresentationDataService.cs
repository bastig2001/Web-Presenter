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
                return true;
            }
            catch (Exception e) {
                Console.Error.WriteLine($"Error while trying to create Presentation {{{presentation}}}. " +
                                        $"\nException: {e}");
                return false;
            }
        }
    }
}