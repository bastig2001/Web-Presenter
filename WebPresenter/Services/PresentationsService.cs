using System;
using System.Collections.Generic;
using System.Linq;
using WebPresenter.Models;

namespace WebPresenter.Services {
    public class PresentationsService {
        private readonly PresentationDataService data;
        private readonly Dictionary<string, Presentation> presentations;
        
        public PresentationsService(PresentationDataService data) {
            this.data = data;
            presentations = new Dictionary<string, Presentation>();
        }

        public Presentation GetPresentation(string id) {
            return presentations.GetValueOrDefault(id);
        }

        public IEnumerable<Presentation> GetPresentations() {
            return presentations.Values;
        }
        
        public IEnumerable<Presentation> GetPresentations(string ownerName) {
            return presentations.Values.Where(pres => pres.OwnerName == ownerName);
        }
        
        public IEnumerable<Presentation> GetPresentations(User owner) {
            return GetPresentations(owner.Name);
        }
        
        public IEnumerable<Presentation> GetPresentations(string name, string ownerName) {
            return presentations.Values.Where(pres => pres.OwnerName == ownerName && pres.Name == name);
        }
        
        public IEnumerable<Presentation> GetPresentations(string name, User owner) {
            return GetPresentations(name, owner.Name);
        }

        public string StartPresentation(string name, User owner) {
            return StartPresentation(name, owner.Name);
        }
        
        public string StartPresentation(string name, string ownerName) {
            var presentationData = data.GetPresentation(name, ownerName);

            return presentationData == null ? "" : StartPresentation((PresentationData) presentationData.Clone());
        }
        
        public string StartPresentation(PresentationData presentationData) {
            return StartPresentation(new Presentation(presentationData));
        }

        public string StartPresentation(Presentation presentation) {
            string id;

            do {
                id = $"{Guid.NewGuid()}";
            } while (presentations.ContainsKey(id));

            presentations.Add(id, presentation);

            return id;
        }

        public void EndPresentation(string id) {
            presentations.Remove(id);
        }
    }
}